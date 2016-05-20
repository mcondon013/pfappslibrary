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

namespace pfAppConfigManager
{
    public partial class ApplicationOptionsForm : Form
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

        private PFOpenFileDialog _openFileDialog = new PFOpenFileDialog();
        private PFSaveFileDialog _saveFileDialog = new PFSaveFileDialog();

        //private fields for saving DataGridViewPrinter settings
        System.Drawing.Printing.PageSettings _savePageSettings = new System.Drawing.Printing.PageSettings();

        //private fields for properties
        private string _exeFileName = string.Empty;
        private string _helpFileName = string.Empty;
        private string _helpFileTopic = string.Empty;
        private string _origSettingsFileName = "pfSettingsOrig.xml";


        public ApplicationOptionsForm()
        {
            InitializeComponent();
        }

        //properties
        /// <summary>
        /// ExeFileName Property.
        /// </summary>
        public string ExeFileName
        {
            get
            {
                return _exeFileName;
            }
            set
            {
                _exeFileName = value;
            }
        }

        /// <summary>
        /// HelpFileName Property.
        /// </summary>
        public string HelpFileName
        {
            get
            {
                return _helpFileName;
            }
            set
            {
                _helpFileName = value;
            }
        }

        /// <summary>
        /// HelpFileTopic Property.
        /// </summary>
        public string HelpFileTopic
        {
            get
            {
                return _helpFileTopic;
            }
            set
            {
                _helpFileTopic = value;
            }
        }

        /// <summary>
        /// Path to file containing original settings
        /// </summary>
        public string OrigSettingsFileName
        {
            get
            {
                return _origSettingsFileName;
            }
            set
            {
                _origSettingsFileName = value;
            }
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
                else
                {
                    AppMessages.DisplayErrorMessage("UpdateConfigItems encountered problem.");
                }
            }
            else
            {
                AppMessages.DisplayErrorMessage("GridDataIsValid = false");
            }
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (GridDataIsValid())
            {
                UpdateConfigItems();
            }
            else
            {
                AppMessages.DisplayErrorMessage("GridDataIsValid = false");
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

        //Common form routines

        private void CloseForm()
        {
            this.Close();
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

            AppConfigExt config = new AppConfigExt(this.ExeFileName);
            KeyValueConfigurationCollection appKeyVals = config.GetAllAppSettings();
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
            return UpdateConfigItems(false);
        }

        private bool UpdateConfigItems(bool forceUpdate)
        {
            bool dataIsValid = true;

            this.lblUpdateResults.Text = string.Empty;

            //System.Configuration.Configuration config =
            //    System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            System.Configuration.Configuration config =
                System.Configuration.ConfigurationManager.OpenExeConfiguration(_exeFileName);
            DataTable dt = keyValsDataSet.Tables["KeyValTable"];
            int numUpdates = 0;
            string key = string.Empty;
            string val = string.Empty;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Modified || forceUpdate == true)
                {
                    key = dr["AppSetting"].ToString();
                    val = dr["SettingValue"].ToString();
                    dataIsValid = VerifyColumnValue(key, val);
                    if (dataIsValid)
                    {
                        try
                        {
                            config.AppSettings.Settings[key].Value = val;
                            numUpdates++;
                        }
                        catch (System.Exception ex)
                        {
                            _msg.Length = 0;
                            _msg.Append("Attempt to update config item failed. Make sure config item exists in app config file.\r\n");
                            _msg.Append("Key: ");
                            _msg.Append(key);
                            _msg.Append(" Value: ");
                            _msg.Append(val);
                            _msg.Append("\r\nError message:\r\n");
                            _msg.Append(AppMessages.FormatErrorMessage(ex));
                            AppMessages.DisplayErrorMessage(_msg.ToString());
                        }
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

            if (File.Exists(_helpFileName))
            {
                if (_helpFileName.Length > 0 && _helpFileTopic.Length > 0)
                    Help.ShowHelp(this, _helpFileName, HelpNavigator.KeywordIndex, _helpFileTopic);
                else if (_helpFileName.Length > 0)
                    Help.ShowHelp(this, _helpFileName, HelpNavigator.TableOfContents);
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Application Options");
                    messageCaption = _msg.ToString();
                    _msg.Length = 0;
                    _msg.Append("Help not yet implemented for this application.");
                    messageText = _msg.ToString();
                    AppMessages.DisplayWarningMessage(messageText);
                }
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_helpFileName);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

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
            GridViewPrint(true);
        }

        private void ShowPrintDialog()
        {
            GridViewPrint(false);
        }

        private void GridViewPrint(bool showPreview)
        {

            try
            {
                if (PFSystemObjects.SysInfo.DefaultPrinterIsDefined() == false)
                {
                    _msg.Length = 0;
                    _msg.Append("No default printer specified. You must specify a default printer for print routines to work.");
                    throw new System.Exception(_msg.ToString());
                }

                PFDataGridViewPrinter _dgvPrinter = new PFDataGridViewPrinter();
                InitDataGridViewPrinter(_dgvPrinter);
                if (showPreview)
                {
                    _dgvPrinter.PrintPreview();
                }
                else
                {
                    _dgvPrinter.Print();
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToErrorLog);
            }
            finally
            {
                ;
            }
                 
        
        }

        private void InitDataGridViewPrinter(PFDataGridViewPrinter dgvPrinter)
        {
            dgvPrinter.Grid = this.dataGridAppSettings;

            dgvPrinter.Printer.printDoc.DefaultPageSettings = _savePageSettings;
            dgvPrinter.Printer.printDoc.DefaultPageSettings.PaperSource.RawKind = (int)System.Drawing.Printing.PaperSourceKind.AutomaticFeed;

            dgvPrinter.Title = AppGlobals.AppInfo.AssemblyDescription;
            dgvPrinter.SubTitle = "Application Settings for " + Path.GetFileName(Program._exeFileName); 
            dgvPrinter.Footer = AppGlobals.AppInfo.AssemblyProduct;
            dgvPrinter.PageNumbers = true;
            dgvPrinter.ShowTotalPageNumber = true;

        }


        private DialogResult ShowOpenFileDialog()
        {
            DialogResult res = DialogResult.None;
            string appName = AppInfo.AssemblyProduct;
            if (appName.Trim().Length == 0)
                appName = "pfApplication";

            _openFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\" + appName + @"\Settings");
            _openFileDialog.FileName = string.Empty;
            if (Directory.Exists(_openFileDialog.InitialDirectory) == false)
                Directory.CreateDirectory(_openFileDialog.InitialDirectory);
            res = _openFileDialog.ShowOpenFileDialog();

            if (res == DialogResult.OK)
            {
                _openFileDialog.InitialDirectory = Path.GetDirectoryName(_openFileDialog.FileName) + @"\";
                _openFileDialog.FilterIndex.ToString();
            }

            return res;
        }
        
        private DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;
            string appName = AppInfo.AssemblyProduct;
            if(appName.Trim().Length == 0)
                appName = "pfApplication";

            _saveFileDialog.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\" + appName + @"\Settings");
            _saveFileDialog.ShowCreatePrompt = false;
            _saveFileDialog.FileName = appName + DateTime.Now.ToString("_yyyyMMdd");
            if(Directory.Exists(_saveFileDialog.InitialDirectory)==false)
                Directory.CreateDirectory(_saveFileDialog.InitialDirectory);
            res = _saveFileDialog.ShowSaveFileDialog();

            if (res == DialogResult.OK)
            {
                _saveFileDialog.InitialDirectory = Path.GetDirectoryName(_saveFileDialog.FileName) + @"\";
                _saveFileDialog.FilterIndex.ToString();
            }

            return res;
        }



    }//end class
}//end namespace
