using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using AppGlobals;
using PFFileSystemObjects;
using PFPrinterObjects;
using PFAppUtils;
using PFTextFiles;
using PFDataAccessObjects;

namespace TestprogTextBoxRuler
{
    public partial class ShowAllAppOptionsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private struct SavedBooleanControl
        {
            public string Name;
            public bool Checked;
        }
        private List<SavedBooleanControl> _saveFormCheckBoxes = new List<SavedBooleanControl>();
        private List<SavedBooleanControl> _saveFormRadioButtons = new List<SavedBooleanControl>();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";
        private string[] _validBooleanValues = null;

        private PFSaveFileDialog _saveFileDialog = new PFSaveFileDialog();

        //private fields for saving DataGridViewPrinter settings
        System.Drawing.Printing.PageSettings _savePageSettings = new System.Drawing.Printing.PageSettings();


        public ShowAllAppOptionsForm()
        {
            InitializeComponent();
        }

        //Button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (GridDataIsValid())
            {
                if (UpdateConfigItems() == true)
                {
                    CloseForm();
                }
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (GridDataIsValid())
            {
                UpdateConfigItems();
            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            LoadAppConfigItems();
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            DisplayHelp();
            this.DialogResult = DialogResult.None;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        //menu click handlers

        private void mnuSettingsPageSetup_Click(object sender, EventArgs e)
        {
            ShowPageSettings();
        }

        private void mnuSettingsPrintPreview_Click(object sender, EventArgs e)
        {
            ShowPrintPreview();
        }

        private void mnuSettingsPrint_Click(object sender, EventArgs e)
        {
            ShowPrintDialog();
        }

        private void mnuSettingsExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void mnuSettingsExportToXls_Click(object sender, EventArgs e)
        {
            _saveFileDialog.Filter = "Excel Files|*.xls|All Files|*.*";
            _saveFileDialog.FilterIndex = 0;

            ExportToExcel(Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel8);
        }

        private void mnuSettingsExportToXlsx_Click(object sender, EventArgs e)
        {
            _saveFileDialog.Filter = "Excel Files|*.xlsx|All Files|*.*";
            _saveFileDialog.FilterIndex = 0;

            ExportToExcel(Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
        }

        private void mnuSettingsExportToCsv_Click(object sender, EventArgs e)
        {
            _saveFileDialog.Filter = "Csv Files|*.csv|All Files|*.*";
            _saveFileDialog.FilterIndex = 0;

            ExportToTextFile(",", "\r\n");
        }

        private void mnuSettingExportToDelimitedTextFile_Click(object sender, EventArgs e)
        {
            _saveFileDialog.Filter = "Text Files|*.txt;*.tab;*.dat|All Files|*.*";
            _saveFileDialog.FilterIndex = 0;

            ExportToTextFile(string.Empty, string.Empty);

        }

        private void mnuSettingsExportToAccess_Click(object sender, EventArgs e)
        {
            _saveFileDialog.Filter = "Access Files|*.mdb;*.accdb|All Files|*.*";
            _saveFileDialog.FilterIndex = 0;

            ExportToAccessDatabase();
        }

        private void mnuSettingsExportToXml_Click(object sender, EventArgs e)
        {
            _saveFileDialog.Filter = "Xml Files|*.xml|All Files|*.*";
            _saveFileDialog.FilterIndex = 0;

            ExportToXmlFile();
        }

        //Common form routines

        private void CloseForm()
        {
            this.Hide();
        }

        //Form events
        private void AppOptionsForm_Load(object sender, EventArgs e)
        {
            string formCaption = string.Empty;

            _msg.Length = 0;
            _msg.Append(AppInfo.AssemblyProduct);
            _msg.Append(" Application Options");
            formCaption = _msg.ToString();

            this.Text = formCaption;

            InitForm();
        }

        private void InitForm()
        {
            LoadAppConfigItems();

            InitSaveFileDialog();

            foreach (Control ctl in this.Controls)
            {
                if (ctl is CheckBox)
                {
                    CheckBox chk = (CheckBox)ctl;
                    SavedBooleanControl saveChk = new SavedBooleanControl();
                    saveChk.Name = chk.Name;
                    saveChk.Checked = chk.Checked;
                    _saveFormCheckBoxes.Add(saveChk);
                }
                if (ctl is RadioButton)
                {
                    RadioButton rdo = (RadioButton)ctl;
                    SavedBooleanControl saveRdo = new SavedBooleanControl();

                    saveRdo.Name = rdo.Name;
                    saveRdo.Checked = rdo.Checked;
                    _saveFormRadioButtons.Add(saveRdo);
                }
            }//end foreach

        }//end method

        private void LoadAppConfigItems()
        {

            GetLoggingSettings();

            _validBooleanValues = StaticKeysSection.Settings.ValidBooleanValues.Split(',');
            this.lblUpdateResults.Text = string.Empty;
            this.lblDataValidationMessage.Text = string.Empty;

            KeyValueConfigurationCollection appKeyVals = AppConfig.GetAllAppSettings();
            DataTable dt = keyValsDataSet.Tables["KeyValTable"];
            dt.Clear();

            foreach (KeyValueConfigurationElement ce in appKeyVals)
            {
                DataRow dr = dt.NewRow();
                dr["AppSetting"] = ce.Key;
                dr["SettingValue"] = ce.Value;
                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();

        }

        private void InitSaveFileDialog()
        {
            _saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\";
            _saveFileDialog.FileName = string.Empty;
            _saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx|Text Files|*.tab|CSV Files|*.csv|Access Files|*.mdb;*.accdb|Xml Files|*.xml|All Files|*.*";
            _saveFileDialog.FilterIndex = 0;
            _saveFileDialog.ShowCreatePrompt = true;
            _saveFileDialog.ShowOverwritePrompt = true;
        }

        private void GetLoggingSettings()
        {
            _saveErrorMessagesToErrorLog = AppConfig.GetBooleanValueFromConfigFile("SaveErrorMessagesToErrorLog", "false");
            _applicationLogFileName = System.Configuration.ConfigurationManager.AppSettings["ApplicationLogFileName"];

        }

        private bool UpdateConfigItems()
        {
            bool dataIsValid = true;

            this.lblUpdateResults.Text = string.Empty;

            System.Configuration.Configuration config =
                System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            DataTable dt = keyValsDataSet.Tables["KeyValTable"];
            int numUpdates = 0;
            string key = string.Empty;
            string val = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Modified)
                {
                    key = dr["AppSetting"].ToString();
                    val = dr["SettingValue"].ToString();
                    dataIsValid = VerifyColumnValue(key, val);
                    if (dataIsValid)
                    {
                        config.AppSettings.Settings[key].Value = val;
                        numUpdates++;
                    }
                }
                if (dataIsValid == false)
                    break;
            }

            if (dataIsValid)
            {
                if (numUpdates > 0)
                {
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    dt.AcceptChanges();
                    GetLoggingSettings();
                    _msg.Length = 0;
                    _msg.Append(numUpdates.ToString());
                    _msg.Append(" application ");
                    if (numUpdates == 1)
                        _msg.Append(" setting was ");
                    else
                        _msg.Append(" settings were ");
                    _msg.Append(" changed.");
                    this.lblUpdateResults.Text = _msg.ToString();
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("No changes were pending to the application settings.");
                    this.lblUpdateResults.Text = _msg.ToString();
                }
            }
            else
            {
                this.lblDataValidationMessage.Text = _verifyDataMessages.ToString();
            }

            return dataIsValid;
        }

        private void DisplayHelp()
        {
            string messageCaption = string.Empty;
            string messageText = string.Empty;

            _msg.Length = 0;
            _msg.Append("Help for ");
            _msg.Append(AppGlobals.AppInfo.AssemblyProduct);
            messageCaption = _msg.ToString();

            _msg.Length = 0;
            _msg.Append("Help not yet implemented for ");
            _msg.Append(AppGlobals.AppInfo.AssemblyProduct);
            _msg.Append(".");
            messageText = _msg.ToString();

            AppMessages.DisplayWarningMessage(messageText);

        }

        private bool GridDataIsValid()
        {
            bool isValid = true;

            this.lblDataValidationMessage.Text = string.Empty;
            this.lblUpdateResults.Text = string.Empty;


            for (int i = 0; i < dataGridAppSettings.Rows.Count; i++)
            {
                DataGridViewRow r = dataGridAppSettings.Rows[i];
                string appSettingKey = r.Cells["AppSettingKey"].Value.ToString();
                string appSettingValue = r.Cells["AppSettingValue"].Value.ToString();
                isValid = VerifyColumnValue(appSettingKey, appSettingValue);
                if (isValid == false)
                {
                    this.lblDataValidationMessage.Text = _verifyDataMessages.ToString();
                    break;
                }
            }

            //test
            //for (int i = 0; i < c1TrueDBGrid1.Splits[0].Rows.Count; i++)
            //{
            //    System.Data.DataRowView drv = (System.Data.DataRowView)c1TrueDBGrid1[i];
            //    _msg.Length = 0;
            //    _msg.Append(drv[0].ToString());
            //    _msg.Append(" value= ");
            //    _msg.Append(drv[1].ToString());
            //    Program._messageLog.WriteLine(_msg.ToString());
            //}
            //end test

            //for (int i = 0; i < c1TrueDBGrid1.Splits[0].Rows.Count; i++)
            //{
            //    System.Data.DataRowView drv = (System.Data.DataRowView)c1TrueDBGrid1[i];
            //    string appSettingKey = drv[0].ToString();
            //    string appSettingValue = drv[1].ToString();
            //    isValid = VerifyColumnValue(appSettingKey, appSettingValue);
            //    if (isValid == false)
            //    {
            //        this.lblDataValidationMessage.Text = _verifyDataMessages.ToString();
            //        break;
            //    }
            //}


            return isValid;
        }

        private StringBuilder _verifyDataMessages = new StringBuilder();

        private bool VerifyColumnValue(string currAppSetting, string currValue)
        {
            bool isValid = true;

            _verifyDataMessages.Length = 0;
            _verifyDataMessages.Append("Value for ");
            _verifyDataMessages.Append(currAppSetting);
            _verifyDataMessages.Append(" changed to ");
            _verifyDataMessages.Append(currValue);
            _verifyDataMessages.Append(".");

            switch (currAppSetting)
            {
                case "SaveErrorMessagesToErrorLog":
                    if (IsValidBoolean(currValue) == false)
                    {
                        _verifyDataMessages.Length = 0;
                        _verifyDataMessages.Append("Invalid value for ");
                        _verifyDataMessages.Append(currAppSetting);
                        _verifyDataMessages.Append(": ");
                        _verifyDataMessages.Append(currValue);
                        _verifyDataMessages.Append(".");
                        isValid = false;
                    }
                    break;
                case "ApplicationLogFileName":
                    if (PFFile.IsValidPath(currValue) == false)
                    {
                        _verifyDataMessages.Length = 0;
                        _verifyDataMessages.Append("Invalid value for ");
                        _verifyDataMessages.Append(currAppSetting);
                        _verifyDataMessages.Append(": ");
                        _verifyDataMessages.Append(currValue);
                        _verifyDataMessages.Append(".");
                        isValid = false;
                    }
                    break;
                default:
                    break;
            }

            return isValid;
        }

        private bool IsValidBoolean(string val)
        {
            bool isValid = false;

            if (_validBooleanValues != null)
            {
                if (_validBooleanValues.Length > 0)
                {
                    for (int i = 0; i < _validBooleanValues.Length; i++)
                    {
                        if (_validBooleanValues[i] == val)
                        {
                            isValid = true;
                            break;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                }
                else
                {
                    if (val == string.Empty)
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                }
            }
            else
            {
                //if no list of valid values provided, then all values are valid
                isValid = true;
            }

            return isValid;
        }


        private void ShowPageSettings()
        {
            PFDataGridViewPrinter _dgvPrinter = new PFDataGridViewPrinter();
            InitDataGridViewPrinter(_dgvPrinter);
            _dgvPrinter.ShowPageSettingsDialog();
            _savePageSettings = _dgvPrinter.Printer.printDoc.DefaultPageSettings;
        }

        private void ShowPrintPreview()
        {
            PFDataGridViewPrinter _dgvPrinter = new PFDataGridViewPrinter();
            InitDataGridViewPrinter(_dgvPrinter);
            _dgvPrinter.PrintPreview();
        }

        private void ShowPrintDialog()
        {
            PFDataGridViewPrinter _dgvPrinter = new PFDataGridViewPrinter();
            InitDataGridViewPrinter(_dgvPrinter);
            _dgvPrinter.Print();
        }

        private void InitDataGridViewPrinter(PFDataGridViewPrinter dgvPrinter)
        {
            dgvPrinter.Grid = this.dataGridAppSettings;

            dgvPrinter.Printer.printDoc.DefaultPageSettings = _savePageSettings;
            dgvPrinter.Printer.printDoc.DefaultPageSettings.PaperSource.RawKind = (int)System.Drawing.Printing.PaperSourceKind.AutomaticFeed;

            dgvPrinter.Title = "Testprog for DataGridViewPrinter";
            dgvPrinter.SubTitle = "AppSettings from config file";
            dgvPrinter.Footer = "ProFast Computing";
            dgvPrinter.PageNumbers = true;
            dgvPrinter.ShowTotalPageNumber = true;

        }


        private DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;

            res = _saveFileDialog.ShowSaveFileDialog();

            if (res == DialogResult.OK)
            {
                _saveFileDialog.InitialDirectory = Path.GetDirectoryName(_saveFileDialog.FileName) + @"\";
                _saveFileDialog.FilterIndex.ToString();
            }

            return res;
        }

        private void ExportToExcel(Microsoft.Office.Interop.Excel.XlFileFormat fileFormat)
        {
            string fileName = string.Empty;

            if (ShowSaveFileDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                excelApp.Workbooks.Add(Type.Missing);

                excelApp.Columns.ColumnWidth = 30;

                for (int i = 0; i < this.dataGridAppSettings.Rows.Count; i++)
                {
                    DataGridViewRow row = this.dataGridAppSettings.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        excelApp.Cells[i + 1, j + 1] = row.Cells[j].Value.ToString();
                    }
                }


                fileName = _saveFileDialog.FileName;

                if (File.Exists(fileName))
                    File.Delete(fileName);

                excelApp.ActiveWorkbook.SaveAs(fileName, fileFormat);

                excelApp.Quit();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToErrorLog);
            }
            finally
            {
                ;
            }



        }


        private void ExportToTextFile(string colDelimiter, string eol)
        {
            PFDataDelimitersPrompt delimitersPrompt = new PFDataDelimitersPrompt();
            string fileName = string.Empty;
            string columnDelimiter = string.Empty;
            string lineTerminator = string.Empty;
            PFTextFile outputFile = null;
            PFDelimitedDataLine dataLine = null;


            try
            {
                if (columnDelimiter == string.Empty && eol == string.Empty)
                {
                    if (delimitersPrompt.ShowDialog() != DialogResult.OK)
                    {
                        delimitersPrompt.Close();
                        return;
                    }
                    else
                    {
                        columnDelimiter = delimitersPrompt.ColumnDelimiter;
                        lineTerminator = delimitersPrompt.LineTerminator;
                        delimitersPrompt.Close();
                    }
                }
                else
                {
                    columnDelimiter = colDelimiter;
                    lineTerminator = eol;
                }


                if (ShowSaveFileDialog() != DialogResult.OK)
                {
                    return;
                }

                fileName = _saveFileDialog.FileName;

                outputFile = new PFTextFile(fileName, PFFileOpenOperation.OpenFileForWrite);
                dataLine = new PFDelimitedDataLine(2);
                dataLine.ColumnSeparator = columnDelimiter;
                dataLine.LineTerminator = lineTerminator;

                for (int i = 0; i < this.dataGridAppSettings.Rows.Count; i++)
                {
                    DataGridViewRow row = this.dataGridAppSettings.Rows[i];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        //excelApp.Cells[i + 1, j + 1] = row.Cells[j].Value.ToString();
                        dataLine.SetColumnData(j, row.Cells[j].Value.ToString());
                    }
                    outputFile.WriteData(dataLine.OutputColumnData());
                }

                outputFile.CloseFile();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToErrorLog);
            }
            finally
            {
                ;
            }



        }

        private void ExportToAccessDatabase()
        {
            PFMsAccess db = null;
            string fileName = string.Empty;
            string fileExtension = string.Empty;

            if (ShowSaveFileDialog() != DialogResult.OK)
            {
                return;
            }

            fileName = _saveFileDialog.FileName;
            fileExtension = Path.GetExtension(fileName);

            try
            {
                AccessVersion accver = AccessVersion.Access2003;

                if (fileExtension.ToLower() == ".mdb")
                    accver = AccessVersion.Access2003;
                else
                    accver = AccessVersion.Access2007;

                if (File.Exists(fileName) == false)
                {
                    PFMsAccess.CreateDatabase(fileName, accver, false, "Admin", string.Empty);
                }

                db = new PFMsAccess(fileName);
                DataTable dt = this.keyValsDataSet.Tables["KeyValTable"];

                db.CreateTable(dt);  //separate adox and adodb connections are made by this call

                db.OleDbProvider = fileExtension.ToLower() == ".mdb" ? PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0 : PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;

                db.OpenConnection();

                db.ImportDataFromDataTable(dt);

                db.CloseConnection();

                db = null;

                System.GC.Collect();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToErrorLog);
            }
            finally
            {
                if (db != null)
                {
                    if (db.IsConnected)
                        db.CloseConnection();
                    db = null;
                    System.GC.Collect();
                }
            }


        }

        private void ExportToXmlFile()
        {
            string fileName = string.Empty;
            PFDataExporter exporter = new PFDataExporter();

            if (ShowSaveFileDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                DataTable dt = this.keyValsDataSet.Tables["KeyValTable"];
                dt.AcceptChanges();

                fileName = _saveFileDialog.FileName;

                if (File.Exists(fileName))
                    File.Delete(fileName);

                exporter.ExportDataTableToXmlFile(dt, fileName);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToErrorLog);
            }
            finally
            {
                ;
            }


        }



    }//end class
}//end namespace
