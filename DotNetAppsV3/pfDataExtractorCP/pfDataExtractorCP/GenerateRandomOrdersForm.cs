using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;
using PFMessageLogs;
using PFFileSystemObjects;
//using PFTextFiles;
using PFPrinterObjects;
using PFAppDataObjects;
using PFRandomDataProcessor;
using PFRandomValueDataTables;
using PFRandomDataExt;
using KellermanSoftware.CompareNetObjects;
using PFTextObjects;
using PFRandomDataForms;
using pfDataExtractorCPObjects;
using PFDataOutputGrid;
using pfDataExtractorCPProcesxor;

namespace pfDataExtractorCP
{
#pragma warning disable 1591
    public partial class RandomOrdersGeneratorForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;
        PFAppProcessor _appProcessor = new PFAppProcessor();
        StringBuilder _generatorMessages = new StringBuilder();

        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\DataExports\";

        //fields for properties
        MessageLog _messageLog = null;
        bool _eraseLogBeforeExtracting = false;

        PFRandomOrdersDefinition _randomOrderDefinition = new PFRandomOrdersDefinition();

        //constructors

        public RandomOrdersGeneratorForm()
        {
            InitializeComponent();
        }


        //delegates
#pragma warning disable 1591
        public delegate void GenerateSalesOrderTransactionsDelegate(int maxTxsToGenerate, StringBuilder generatorMessages);
        public event GenerateSalesOrderTransactionsDelegate GenerateSalesOrderTransactions;

        public delegate void GeneratePurchaseOrderTransactionsDelegate(int maxTxsToGenerate, StringBuilder generatorMessages);
        public event GeneratePurchaseOrderTransactionsDelegate GeneratePurchaseOrderTransactions;

        public delegate void GenerateInternetSalesTransactionsDelegate(int maxTxsToGenerate, StringBuilder generatorMessages);
        public event GenerateInternetSalesTransactionsDelegate GenerateInternetSalesTransactions;

        public delegate void GenerateResellerSalesTransactionsDelegate(int maxTxsToGenerate, StringBuilder generatorMessages);
        public event GenerateResellerSalesTransactionsDelegate GenerateResellerSalesTransactions;

        public delegate void SaveExtractorDefinitionDelegate();
        public event SaveExtractorDefinitionDelegate SaveExtractorDefinition;

#pragma warning restore 1591

        //properties

        /// <summary>
        /// Message Log object.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }

        /// <summary>
        /// EraseLogBeforeExtracting Property.
        /// </summary>
        public bool EraseLogBeforeExtracting
        {
            get
            {
                return _eraseLogBeforeExtracting;
            }
            set
            {
                _eraseLogBeforeExtracting = value;
            }
        }

        /// <summary>
        /// RandomOrderDefinition Property.
        /// </summary>
        public PFRandomOrdersDefinition RandomOrderDefinition
        {
            get
            {
                return _randomOrderDefinition;
            }
            set
            {
                _randomOrderDefinition = value;
            }
        }


        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdGenerateTransactionData_Click(object sender, EventArgs e)
        {
            GenerateTransactionData();
        }

        private void cmdSaveExtractDeinition_Click(object sender, EventArgs e)
        {
            SaveExtractDefinitionFile();
        }

        private void cmdPreviewTransactionData_Click(object sender, EventArgs e)
        {
            PreviewTransactionData();
        }

        private void cmdEditOutputTableNames_Click(object sender, EventArgs e)
        {
            EditOutputTableNames();
        }

        private void cmdSpecifyTransactionDatesAndCounts_Click(object sender, EventArgs e)
        {
            SpecifyTransactionDatesAndCounts();
        }

        //Menu item clicks
        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            SaveExtractDefinitionFile();
        }

        private void mnuFilePageSetup_Click(object sender, EventArgs e)
        {
            ShowPageSettings();
        }

        private void mnuFilePrint_Click(object sender, EventArgs e)
        {
            FilePrint(false, true);
        }

        private void mnuFilePrintPreview_Click(object sender, EventArgs e)
        {
            FilePrint(true, false);
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void mnuHelpContents_Click(object sender, EventArgs e)
        {
            ShowHelpContents();
        }

        private void mnuHelpIndex_Click(object sender, EventArgs e)
        {
            ShowHelpIndex();
        }

        private void mnuHelpSearch_Click(object sender, EventArgs e)
        {
            ShowHelpSearch();
        }

        private void mnuHelpTutorial_Click(object sender, EventArgs e)
        {
            ShowHelpTutorial();
        }

        private void mnuHelpContact_Click(object sender, EventArgs e)
        {
            ShowHelpContact();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            ShowHelpAbout();
        }

        private void toolbarHelp_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }




        //Form Routines
        private void CloseForm()
        {
            this.Close();
        }

        private void HideForm()
        {
            this.Hide();
        }

        private void ShowHelpContents()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.TableOfContents);
        }

        private void ShowHelpIndex()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.Index);
        }

        private void ShowHelpSearch()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.Find, "");
        }

        private void ShowHelpTutorial()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Tutorial");
        }

        private void ShowHelpContact()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Contact Information");
        }

        private void ShowHelpAbout()
        {
            HelpAboutForm appHelpAboutForm = new HelpAboutForm();
            appHelpAboutForm.ShowDialog();

        }

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Generate Test Orders Overview");
        }

        private bool HelpFileExists()
        {
            bool ret = false;

            if (File.Exists(_helpFilePath))
            {
                ret = true;
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_helpFilePath);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

            return ret;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormHasUnsavedChanges())
            {
                DialogResult res = CheckExitRequest();
                if (res == DialogResult.Yes)
                {
                    SaveFormValues();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private DialogResult CheckExitRequest()
        {
            DialogResult res = AppMessages.DisplayMessage("Click Yes to save changes to the table names.", "Cancel Specify Table Names ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }

        private bool FormHasUnsavedChanges()
        {
            bool ret = false;

            if (this.chkReplaceExistingTables.Checked != _randomOrderDefinition.ReplaceExistingTables)
                ret = true;
            if (this.chkGenerateSalesOrders.Checked != _randomOrderDefinition.GenerateSalesOrders)
                ret = true;
            if (this.chkGeneratePurchaseOrders.Checked != _randomOrderDefinition.GeneratePurchaseOrders)
                ret = true;
            if (this.chkGenerateInternetSalesTable.Checked != _randomOrderDefinition.GenerateInternetSalesTable)
                ret = true;
            if (this.chkGenerateResellerSalesTable.Checked != _randomOrderDefinition.GenerateResellerSalesTable)
                ret = true;

            return ret;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                SetLoggingValues();

                SetHelpFileValues();

                SetFormValues();

                SetSchemaName();

                _printer = new FormPrinter(this);

                this.txtNumPreviewTransactions.Text = AppConfig.GetStringValueFromConfigFile("NumberOfTestOrderPreviewTransactions", "120");


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }


        internal void SetLoggingValues()
        {
            string configValue = string.Empty;

            configValue = AppGlobals.AppConfig.GetConfigValue("SaveErrorMessagesToErrorLog");
            if (configValue.ToUpper() == "TRUE")
                _saveErrorMessagesToAppLog = true;
            else
                _saveErrorMessagesToAppLog = false;
            _appLogFileName = AppGlobals.AppConfig.GetConfigValue("AppLogFileName");

            if (_appLogFileName.Trim().Length > 0)
                AppGlobals.AppMessages.AppLogFilename = _appLogFileName;

        }

        internal void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "RandomDataMasks.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        private void SetFormValues()
        {
            this.Text = "Random Orders Generator Utility";

            this.txtNumPreviewTransactions.Text = AppConfig.GetStringValueFromConfigFile("NumberOfTestOrderPreviewTransactions", "120");
            this.optPreviewSalesOrders.Checked = true;

            this.txtOutputDatabaseLocation.Text = _randomOrderDefinition.OutputDatabaseLocation;
            this.txtOutputDatabasePlatform.Text = _randomOrderDefinition.OutputDatabasePlatform;
            this.txtOutputDatabaseConnection.Text = _randomOrderDefinition.OutputDatabaseConnection;

            this.chkReplaceExistingTables.Checked = _randomOrderDefinition.ReplaceExistingTables;
            this.chkGenerateSalesOrders.Checked = _randomOrderDefinition.GenerateSalesOrders;
            this.chkGeneratePurchaseOrders.Checked = _randomOrderDefinition.GeneratePurchaseOrders;
            this.chkGenerateInternetSalesTable.Checked = _randomOrderDefinition.GenerateInternetSalesTable;
            this.chkGenerateResellerSalesTable.Checked = _randomOrderDefinition.GenerateResellerSalesTable;

        }

        private void SetSchemaName()
        {
            string defaultSchemaName = string.Empty;
            string keyName = string.Empty;

            keyName = "DefaultSchema_" + _randomOrderDefinition.OutputDatabasePlatform;
            defaultSchemaName = AppConfig.GetStringValueFromConfigFile(keyName, string.Empty);

            if (_randomOrderDefinition.TableSchema.Trim().Length == 0)
            {
                _randomOrderDefinition.TableSchema = defaultSchemaName;
            }

        }

        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;
            ListBox lst = null;
            ComboBox cbo = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = true;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = true;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = true;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = true;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = true;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = true;
                }
                if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    lst.Enabled = true;
                }
                if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    cbo.Enabled = true;
                }

            }//end foreach
        }//end method

        private void DisableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;
            ListBox lst = null;
            ComboBox cbo = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = false;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = false;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = false;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = false;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = false;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = false;
                }
                if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    lst.Enabled = false;
                }
                if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    cbo.Enabled = false;
                }

            }//end foreach control

        }//end method

        private void ShowPageSettings()
        {
            _printer.ShowPageSettings();
        }

        private void FilePrint(bool preview, bool showPrintDialog)
        {
            _printer.PageTitle = AppGlobals.AppInfo.AssemblyDescription;
            _printer.PageSubTitle = "Generate Random Orders Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }



        //application routines


        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }

        private void OutputDataTableToGrid(DataTable tab)
        {
            PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
            grid.ShowInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            grid.DefaultOutputDatabaseType = Properties.Settings.Default.DefaultOutputDatabaseType;
            grid.DefaultOutputDatabaseConnectionString = Properties.Settings.Default.DefaultOutputDatabaseConnectionString;
            if (Directory.Exists(Properties.Settings.Default.DefaultDataGridExportFolder) == false)
            {
                grid.DefaultGridExportFolder = _initDataGridExportFolder;
            }
            else
            {
                grid.DefaultGridExportFolder = Properties.Settings.Default.DefaultDataGridExportFolder;
            }
            grid.EnableExportMenu = true;
            grid.WriteDataToGrid(tab);
        }

        private void EditOutputTableNames()
        {
            EditTableNamesForm frm = new EditTableNamesForm();

            SaveFormValues();

            frm.MessageLogUI = _messageLog;
            frm.RandomOrderDefinition = _randomOrderDefinition;

            DialogResult res = frm.ShowDialog();

            if (res == DialogResult.OK)
            {
                AppMessages.DisplayInfoMessage("EditTableNamesForm accepted.");
            }
            else
            {
                AppMessages.DisplayInfoMessage("EditTableNamesForm cancelled.");
            }

            frm.Close();
        }

        private void SpecifyTransactionDatesAndCounts()
        {
            OrderTransactionDatesForm frm = new OrderTransactionDatesForm();

            SaveFormValues();

            frm.MessageLogUI = _messageLog;
            frm.RandomOrderDefinition = _randomOrderDefinition;
            frm.OutputTableToGrid += OutputDataTableToGrid;

            DialogResult res = frm.ShowDialog();

            if (res == DialogResult.OK)
            {
                AppMessages.DisplayInfoMessage("SpecifyTransactionDatesAndCounts accepted.");
            }
            else
            {
                AppMessages.DisplayInfoMessage("SpecifyTransactionDatesAndCounts cancelled.");
            }

            frm.OutputTableToGrid -= OutputDataTableToGrid;
            frm.Close();
        }

        private void PreviewTransactionData()
        {
            int maxTxsToGenerate = 0;

            try
            {
                DisableFormControls();
                maxTxsToGenerate = PFTextProcessor.ConvertStringToInt(this.txtNumPreviewTransactions.Text, 100);
                if (maxTxsToGenerate > 5000)
                {
                    this.txtNumPreviewTransactions.Text = "5000";
                    maxTxsToGenerate = 5000;
                }

                SaveFormValues();

                _generatorMessages.Length = 0;
                _generatorMessages.Append("Preview test orders running ...");
                _generatorMessages.Append(Environment.NewLine);

                //callbacks used below

                if (this.optPreviewSalesOrders.Checked)
                {
                    GenerateSalesOrderTransactions(maxTxsToGenerate, _generatorMessages);
                }

                if (this.optPreviewPurchaseOrders.Checked)
                {
                    GeneratePurchaseOrderTransactions(maxTxsToGenerate, _generatorMessages);
                }

                if (this.optPreviewDwInternet.Checked)
                {
                    GenerateInternetSalesTransactions(maxTxsToGenerate, _generatorMessages);
                }

                if (this.optPreviewDwReseller.Checked)
                {
                    GenerateResellerSalesTransactions(maxTxsToGenerate, _generatorMessages);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _generatorMessages.Append("... Preview test orders finsiehd.");
                _generatorMessages.Append(Environment.NewLine);
                AppMessages.DisplayInfoMessage(_generatorMessages.ToString());
                EnableFormControls();
            }


        }


        private void GenerateTransactionData()
        {

            try
            {
                DisableFormControls();
                SaveFormValues();

                if (_messageLog != null)
                {
                    if (_eraseLogBeforeExtracting)
                    {
                        _messageLog.Clear();
                    }
                }

                _generatorMessages.Length = 0;
                _generatorMessages.Append("Generation of test orders running ...");
                _generatorMessages.Append(Environment.NewLine);
                _generatorMessages.Append(Environment.NewLine);

                _msg.Length = 0;
                _msg.Append("Generation of test orders running ...");
                _msg.Append(Environment.NewLine);
                _msg.Append("Output destination: ");
                _msg.Append(this.txtOutputDatabasePlatform.Text);
                _msg.Append(Environment.NewLine);
                _msg.Append("Connection: ");
                _msg.Append(Environment.NewLine);
                _msg.Append(this.txtOutputDatabaseConnection.Text);
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());

                //callbacks used below

                if (this.chkGenerateSalesOrders.Checked)
                {
                    GenerateSalesOrderTransactions((int)-1, _generatorMessages);
                }

                if (this.chkGeneratePurchaseOrders.Checked)
                {
                    GeneratePurchaseOrderTransactions((int)-1, _generatorMessages);
                }

                if (this.chkGenerateInternetSalesTable.Checked)
                {
                    GenerateInternetSalesTransactions((int)-1, _generatorMessages);
                }

                if (this.chkGenerateResellerSalesTable.Checked)
                {
                    GenerateResellerSalesTransactions((int)-1, _generatorMessages);
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _generatorMessages.Append("... Generation of test orders finished.");
                _generatorMessages.Append(Environment.NewLine);
                AppMessages.DisplayInfoMessage(_generatorMessages.ToString());
                EnableFormControls();
            }
                 
        
        }
        
        private void SaveFormValues()
        {
            _randomOrderDefinition.ReplaceExistingTables = this.chkReplaceExistingTables.Checked;
            _randomOrderDefinition.GenerateSalesOrders = this.chkGenerateSalesOrders.Checked;
            _randomOrderDefinition.GeneratePurchaseOrders = this.chkGeneratePurchaseOrders.Checked;
            _randomOrderDefinition.GenerateInternetSalesTable = this.chkGenerateInternetSalesTable.Checked;
            _randomOrderDefinition.GenerateResellerSalesTable = this.chkGenerateResellerSalesTable.Checked;
        }

        private void SaveExtractDefinitionFile()
        {
            SaveFormValues();
            if(SaveExtractorDefinition != null)
                SaveExtractorDefinition();
        }




    }//end class
#pragma warning restore 1591
}//end namespace

