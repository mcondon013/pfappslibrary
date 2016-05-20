using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using System.Configuration;
using System.IO;
using PFFileSystemObjects;
using PFPrinterObjects;
using PFAppUtils;
using PFTextFiles;
using PFDataAccessObjects;
using PFAppDataObjects;
using PFTextObjects;
using pfDataExtractorCPObjects;
using PFCollectionsObjects;
using PFConnectionObjects;
using PFConnectionStrings;

namespace pfDataExtractorCP
{
#pragma warning disable 1591

    public partial class DatabaseOptionsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";

        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _helpFilePath = string.Empty;

        //see InitWinFormsAppWithDialogs (profast templates) and TestprogAppUtils (winapplib solution)
        //for examples of using PFFolderBrowserDialog, PFOpenFileDialog, PFSaveFileDialog usage
        private PFFolderBrowserDialog _folderBrowserDialog = new PFFolderBrowserDialog();

        private class PFDatabaseOptions
        {
            private string _defaultQueryDefinitionsFolder = string.Empty;
            private string _defaultDataGridExportFolder = string.Empty;
            private bool _showInstalledDatabaseProvidersOnly = false;
            private int _batchSizeForDataImportsAndExports = 1000;
            private int _batchSizeForRandomDataGeneration = 5000;
            private string _defaultInputDatabaseType = string.Empty;
            private string _defaultInputDatabaseConnectionString = string.Empty;
            private string _defaultOutputDatabaseType = string.Empty;
            private string _defaultOutputDatabaseConnectionString = string.Empty;
            private bool _overwriteOutputDestinationIfAlreadyExists = true;

            /// <summary>
            /// DefaultQueryDefinitionsFolder Property.
            /// </summary>
            public string DefaultQueryDefinitionsFolder
            {
                get
                {
                    return _defaultQueryDefinitionsFolder;
                }
                set
                {
                    _defaultQueryDefinitionsFolder = value;
                }
            }

            /// <summary>
            /// defaultDataGridExportFolder Property.
            /// </summary>
            public string DefaultDataGridExportFolder
            {
                get
                {
                    return _defaultDataGridExportFolder;
                }
                set
                {
                    _defaultDataGridExportFolder = value;
                }
            }

            /// <summary>
            /// ShowInstalledDatabaseProvidersOnly Property.
            /// </summary>
            public bool ShowInstalledDatabaseProvidersOnly
            {
                get
                {
                    return _showInstalledDatabaseProvidersOnly;
                }
                set
                {
                    _showInstalledDatabaseProvidersOnly = value;
                }
            }

            /// <summary>
            /// BatchSizeForDataImportsAndExports Property.
            /// </summary>
            public int BatchSizeForDataImportsAndExports
            {
                get
                {
                    return _batchSizeForDataImportsAndExports;
                }
                set
                {
                    _batchSizeForDataImportsAndExports = value;
                }
            }

            /// <summary>
            /// BatchSizeForRandomDataGeneration Property.
            /// </summary>
            public int BatchSizeForRandomDataGeneration
            {
                get
                {
                    return _batchSizeForRandomDataGeneration;
                }
                set
                {
                    _batchSizeForRandomDataGeneration = value;
                }
            }

            /// <summary>
            /// DefaultInputDatabaseType Property.
            /// </summary>
            public string DefaultInputDatabaseType
            {
                get
                {
                    return _defaultInputDatabaseType;
                }
                set
                {
                    _defaultInputDatabaseType = value;
                }
            }

            /// <summary>
            /// DefaultInputDatabaseConnectionString Property.
            /// </summary>
            public string DefaultInputDatabaseConnectionString
            {
                get
                {
                    return _defaultInputDatabaseConnectionString;
                }
                set
                {
                    _defaultInputDatabaseConnectionString = value;
                }
            }

            /// <summary>
            /// DefaultOutputDatabaseType Property.
            /// </summary>
            public string DefaultOutputDatabaseType
            {
                get
                {
                    return _defaultOutputDatabaseType;
                }
                set
                {
                    _defaultOutputDatabaseType = value;
                }
            }

            /// <summary>
            /// DefaultOutputDatabaseConnectionString Property.
            /// </summary>
            public string DefaultOutputDatabaseConnectionString
            {
                get
                {
                    return _defaultOutputDatabaseConnectionString;
                }
                set
                {
                    _defaultOutputDatabaseConnectionString = value;
                }
            }

            /// <summary>
            /// OverwriteOutputDestinationIfAlreadyExists Property.
            /// </summary>
            public bool OverwriteOutputDestinationIfAlreadyExists
            {
                get
                {
                    return _overwriteOutputDestinationIfAlreadyExists;
                }
                set
                {
                    _overwriteOutputDestinationIfAlreadyExists = value;
                }
            }


        }//end private class

        PFDatabaseOptions _databaseOptions = new PFDatabaseOptions();


        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseOptionsForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            UpdateAndCloseForm();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            UpdateDatabaseConfigItems();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            LoadDatabaseConfigItems();
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

        private void cmdGetQueryDefinitionsFolder_Click(object sender, EventArgs e)
        {
            GetQueryDefinitionsFolder();
        }

        private void cmdGetDataGridExportFolder_Click(object sender, EventArgs e)
        {
            GetDataGridExportFolder();
        }

        private void cmdDefineInputDatabaseConnectionString_Click(object sender, EventArgs e)
        {
            DefineConnectionString(this.cboDefaultInputDatabaseType, this.txtDefaultInputDatabaseConnectionString);
        }

        private void cboDefaultInputDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDefaultConnectionStringForDatabaseType(this.cboDefaultInputDatabaseType, this.txtDefaultInputDatabaseConnectionString);
        }

        private void cmdDefineOutputDatabaseConnectionString_Click(object sender, EventArgs e)
        {
            DefineConnectionString(this.cboDefaultOutputDatabaseType, this.txtDefaultOutputDatabaseConnectionString);
        }

        private void cboDefaultOutputDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDefaultConnectionStringForDatabaseType(this.cboDefaultOutputDatabaseType, this.txtDefaultOutputDatabaseConnectionString);
        }

        //menu item clicks

        private void mnuSettingsAccept_Click(object sender, EventArgs e)
        {
            UpdateAndCloseForm();
        }

        private void mnuSettingsPageSetup_Click(object sender, EventArgs e)
        {
            ShowPageSettings();
        }

        private void mnuSettingsPrint_Click(object sender, EventArgs e)
        {
            SettingsPrint(false, true);
        }

        private void mnuSettingsPrintPreview_Click(object sender, EventArgs e)
        {
            SettingsPrint(true, true);
        }

        private void mnuSettingsRestore_Click(object sender, EventArgs e)
        {
            DialogResult res = AppMessages.DisplayMessage("Do you want to replace all database settings with their original installation values?", "Database Settings ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
                LoadDefaultDatabaseConfigItems();
                UpdateDatabaseConfigItems(true);
            }
        }

        private void mnuSettingsCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        //form events

        private void DatabaseOptionsForm_Load(object sender, EventArgs e)
        {
            string formCaption = string.Empty;

            _msg.Length = 0;
            _msg.Append(AppInfo.AssemblyProduct);
            _msg.Append(" Database Options");
            formCaption = _msg.ToString();

            this.Text = formCaption;

            SetHelpFile();

            InitializeForm();

            _printer = new FormPrinter(this);

        }

        //common form processing routines

        private void SetHelpFile()
        {

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "pfDataExtractorCP.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        public void InitializeForm()
        {
            _saveErrorMessagesToErrorLog = AppConfig.GetBooleanValueFromConfigFile("SaveErrorMessagesToErrorLog", "false");
            _applicationLogFileName = System.Configuration.ConfigurationManager.AppSettings["ApplicationLogFileName"];

            InitDefaultDatabaseType(this.cboDefaultInputDatabaseType, this.txtDefaultInputDatabaseConnectionString);

            InitDefaultDatabaseType(this.cboDefaultOutputDatabaseType, this.txtDefaultOutputDatabaseConnectionString);

            LoadDatabaseConfigItems();

            EnableFormControls();
        }

        private void SaveDatabaseConfigItems()
        {
            _databaseOptions.DefaultQueryDefinitionsFolder = this.txtDefaultQueryDefinitionsFolder.Text;
            _databaseOptions.DefaultDataGridExportFolder = this.txtDefaultDataGridExportFolder.Text;
            _databaseOptions.ShowInstalledDatabaseProvidersOnly = this.chkShowInstalledDatabaseProvidersOnly.Checked;
            _databaseOptions.BatchSizeForDataImportsAndExports = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000);
            _databaseOptions.BatchSizeForRandomDataGeneration = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 5000);
            _databaseOptions.DefaultInputDatabaseType = this.cboDefaultInputDatabaseType.Text;
            _databaseOptions.DefaultInputDatabaseConnectionString = this.txtDefaultInputDatabaseConnectionString.Text;
            _databaseOptions.DefaultOutputDatabaseType = this.cboDefaultOutputDatabaseType.Text;
            _databaseOptions.DefaultOutputDatabaseConnectionString = this.txtDefaultOutputDatabaseConnectionString.Text;
            _databaseOptions.OverwriteOutputDestinationIfAlreadyExists = this.chkOverwriteOutputDestinationIfAlreadyExists.Checked;
        }

        private void LoadDatabaseConfigItems()
        {
            _databaseOptions.DefaultQueryDefinitionsFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultQueryDefinitionsFolder;
            _databaseOptions.DefaultDataGridExportFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDataGridExportFolder;
            _databaseOptions.ShowInstalledDatabaseProvidersOnly = pfDataExtractorCP.Properties.Settings.Default.SaveFormLocationsOnExit;
            _databaseOptions.BatchSizeForDataImportsAndExports = pfDataExtractorCP.Properties.Settings.Default.BatchSizeForDataImportsAndExports;
            _databaseOptions.BatchSizeForRandomDataGeneration = pfDataExtractorCP.Properties.Settings.Default.BatchSizeForRandomDataGeneration;
            _databaseOptions.DefaultInputDatabaseType = pfDataExtractorCP.Properties.Settings.Default.DefaultInputDatabaseType;
            _databaseOptions.DefaultInputDatabaseConnectionString = pfDataExtractorCP.Properties.Settings.Default.DefaultInputDatabaseConnectionString;
            _databaseOptions.DefaultOutputDatabaseType = pfDataExtractorCP.Properties.Settings.Default.DefaultOutputDatabaseType;
            _databaseOptions.DefaultOutputDatabaseConnectionString = pfDataExtractorCP.Properties.Settings.Default.DefaultOutputDatabaseConnectionString;
            _databaseOptions.OverwriteOutputDestinationIfAlreadyExists = pfDataExtractorCP.Properties.Settings.Default.OverwriteOutputDestinationIfAlreadyExists;

            this.txtDefaultQueryDefinitionsFolder.Text = _databaseOptions.DefaultQueryDefinitionsFolder;
            this.txtDefaultDataGridExportFolder.Text = _databaseOptions.DefaultDataGridExportFolder;
            this.chkShowInstalledDatabaseProvidersOnly.Checked = _databaseOptions.ShowInstalledDatabaseProvidersOnly;
            this.txtBatchSizeForDataImportsAndExports.Text = _databaseOptions.BatchSizeForDataImportsAndExports.ToString();
            this.txtBatchSizeForRandomDataGeneration.Text = _databaseOptions.BatchSizeForRandomDataGeneration.ToString();
            this.cboDefaultInputDatabaseType.Text = _databaseOptions.DefaultInputDatabaseType;
            this.txtDefaultInputDatabaseConnectionString.Text = _databaseOptions.DefaultInputDatabaseConnectionString;
            this.cboDefaultOutputDatabaseType.Text = _databaseOptions.DefaultOutputDatabaseType;
            this.txtDefaultOutputDatabaseConnectionString.Text = _databaseOptions.DefaultOutputDatabaseConnectionString;
            this.chkOverwriteOutputDestinationIfAlreadyExists.Checked = _databaseOptions.OverwriteOutputDestinationIfAlreadyExists;

        }

        private void LoadDefaultDatabaseConfigItems()
        {
            _databaseOptions.DefaultQueryDefinitionsFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultQueryDefinitionsFolder"].DefaultValue.ToString();
            _databaseOptions.DefaultDataGridExportFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["defaultDataGridExportFolder"].DefaultValue.ToString();
            _databaseOptions.ShowInstalledDatabaseProvidersOnly = Convert.ToBoolean(pfDataExtractorCP.Properties.Settings.Default.Properties["ShowInstalledDatabaseProvidersOnly"].DefaultValue);
            _databaseOptions.BatchSizeForDataImportsAndExports = Convert.ToInt32(pfDataExtractorCP.Properties.Settings.Default.Properties["BatchSizeForDataImportsAndExports"].DefaultValue);
            _databaseOptions.BatchSizeForRandomDataGeneration = Convert.ToInt32(pfDataExtractorCP.Properties.Settings.Default.Properties["BatchSizeForRandomDataGeneration"].DefaultValue);
            _databaseOptions.DefaultInputDatabaseType = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultInputDatabaseType"].DefaultValue.ToString();
            _databaseOptions.DefaultInputDatabaseConnectionString = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultInputDatabaseConnectionString"].DefaultValue.ToString();
            _databaseOptions.DefaultOutputDatabaseType = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultOutputDatabaseType"].DefaultValue.ToString();
            _databaseOptions.DefaultOutputDatabaseConnectionString = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultOutputDatabaseConnectionString"].DefaultValue.ToString();
            _databaseOptions.OverwriteOutputDestinationIfAlreadyExists = Convert.ToBoolean(pfDataExtractorCP.Properties.Settings.Default.Properties["OverwriteOutputDestinationIfAlreadyExists"].DefaultValue);

            if (_databaseOptions.DefaultQueryDefinitionsFolder.Trim().Length == 0)
                _databaseOptions.DefaultQueryDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\QueryDefs\";
            if (_databaseOptions.DefaultDataGridExportFolder.Trim().Length == 0)
                _databaseOptions.DefaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\DataExports\"; 

            this.txtDefaultQueryDefinitionsFolder.Text = _databaseOptions.DefaultQueryDefinitionsFolder;
            this.txtDefaultDataGridExportFolder.Text = _databaseOptions.DefaultDataGridExportFolder;
            this.chkShowInstalledDatabaseProvidersOnly.Checked = _databaseOptions.ShowInstalledDatabaseProvidersOnly;
            this.txtBatchSizeForDataImportsAndExports.Text = _databaseOptions.BatchSizeForDataImportsAndExports.ToString();
            this.txtBatchSizeForRandomDataGeneration.Text = _databaseOptions.BatchSizeForRandomDataGeneration.ToString();
            this.cboDefaultInputDatabaseType.Text = _databaseOptions.DefaultInputDatabaseType;
            this.txtDefaultInputDatabaseConnectionString.Text = _databaseOptions.DefaultInputDatabaseConnectionString;
            this.cboDefaultOutputDatabaseType.Text = _databaseOptions.DefaultOutputDatabaseType;
            this.txtDefaultOutputDatabaseConnectionString.Text = _databaseOptions.DefaultOutputDatabaseConnectionString;
            this.chkOverwriteOutputDestinationIfAlreadyExists.Checked = _databaseOptions.OverwriteOutputDestinationIfAlreadyExists;


        }

        private void InitDefaultDatabaseType(ComboBox cboDatabaseType, TextBox txtDefaultDatabseConnectionString)
        {
            bool showInstalledDatabaseProvidersOnly = pfDataExtractorCP.Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            PFConnectionManager connMgr = new PFConnectionManager();
            PFKeyValueList<string, PFProviderDefinition> provDefs = connMgr.GetListOfProviderDefinitions();

            cboDatabaseType.Items.Clear();
            foreach (stKeyValuePair<string, PFProviderDefinition> provDef in provDefs)
            {
                if (showInstalledDatabaseProvidersOnly)
                {
                    if (provDef.Value.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                    {
                        if (provDef.Value.AvailableForSelection)
                        {
                            cboDatabaseType.Items.Add(provDef.Key);
                        }
                        else
                        {
                            ;
                        }
                    }
                    else
                    {
                        ;
                    }
                }
                else
                {
                    if (provDef.Value.AvailableForSelection)
                    {
                        cboDatabaseType.Items.Add(provDef.Key);
                    }
                    else
                    {
                        ;
                    }
                }
            }

            if (DatabaseTypeIsInList(cboDatabaseType) == false)
            {
                //database type no longer in the list of database types available for use
                cboDatabaseType.Text = string.Empty;
                txtDefaultDatabseConnectionString.Text = string.Empty;
            }
            else
            {
                //database type is still in the list: do nothing
                ;
            }
        }

        private bool DatabaseTypeIsInList(ComboBox cboDatabaseType)
        {
            bool retval = false;

            foreach (string availableDatabase in cboDatabaseType.Items)
            {
                if (cboDatabaseType.Text == availableDatabase)
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }


        public void HideForm()
        {
            this.Hide();
        }

        public void CloseForm()
        {
            this.Hide();
        }

        private void UpdateAndCloseForm()
        {
            bool res = false;
            res = UpdateDatabaseConfigItems();
            if (res == true)
                CloseForm();
        }

        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;

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

            }//end foreach control

        }

        //Application routines

        private void DisplayHelp()
        {
            if (File.Exists(_helpFilePath))
            {
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Change Database Settings");
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_helpFilePath);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }
        }

        private DialogResult ShowFolderBrowserDialog(ref string selectedFolder)
        {
            DialogResult res = DialogResult.None;

            _folderBrowserDialog.InitialFolderPath = selectedFolder;
            _folderBrowserDialog.ShowNewFolderButton = false;

            res = _folderBrowserDialog.ShowFolderBrowserDialog();

            if (res != DialogResult.Cancel)
            {
                selectedFolder = _folderBrowserDialog.InitialFolderPath;
            }


            return res;
        }


        private void GetQueryDefinitionsFolder()
        {
            string initFolder = this.txtDefaultQueryDefinitionsFolder.Text;
            ShowFolderBrowserDialog(ref initFolder);
            this.txtDefaultQueryDefinitionsFolder.Text = initFolder;
        }

        private void GetDataGridExportFolder()
        {
            string initFolder = this.txtDefaultDataGridExportFolder.Text;
            ShowFolderBrowserDialog(ref initFolder);
            this.txtDefaultDataGridExportFolder.Text = initFolder;
        }

        private bool UpdateDatabaseConfigItems()
        {
            return UpdateDatabaseConfigItems(false);
        }

        private bool UpdateDatabaseConfigItems(bool forceUpdate)
        {
            bool updateSuccessful = true;
            int numErrors = 0;
            int numUpdates = 0;

            _msg.Length = 0;

            if (this.txtDefaultQueryDefinitionsFolder.Text.ToUpper().Trim() != _databaseOptions.DefaultQueryDefinitionsFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultQueryDefinitionsFolder.Text.ToUpper().Trim())
                    || this.txtDefaultQueryDefinitionsFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultQueryDefinitionsFolder"] = this.txtDefaultQueryDefinitionsFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultQueryDefinitionsFolder does not exist: ");
                    _msg.Append(this.txtDefaultQueryDefinitionsFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultDataGridExportFolder.Text.ToUpper().Trim() != _databaseOptions.DefaultDataGridExportFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultDataGridExportFolder.Text.ToUpper().Trim())
                    || this.txtDefaultDataGridExportFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["defaultDataGridExportFolder"] = this.txtDefaultDataGridExportFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("defaultDataGridExportFolder does not exist: ");
                    _msg.Append(this.txtDefaultDataGridExportFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.chkShowInstalledDatabaseProvidersOnly.Checked != _databaseOptions.ShowInstalledDatabaseProvidersOnly
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["ShowInstalledDatabaseProvidersOnly"] = this.chkShowInstalledDatabaseProvidersOnly.Checked;
            }

            if (PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000) != _databaseOptions.BatchSizeForDataImportsAndExports
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["BatchSizeForDataImportsAndExports"] = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000);
            }

            if (PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 1000) != _databaseOptions.BatchSizeForRandomDataGeneration
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["BatchSizeForRandomDataGeneration"] = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 5000);
            }

            if (this.cboDefaultInputDatabaseType.Text != _databaseOptions.DefaultInputDatabaseType
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultInputDatabaseType"] = this.cboDefaultInputDatabaseType.Text;
            }

            if (this.txtDefaultInputDatabaseConnectionString.Text != _databaseOptions.DefaultInputDatabaseConnectionString
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultInputDatabaseConnectionString"] = this.txtDefaultInputDatabaseConnectionString.Text;
            }

            if (this.cboDefaultOutputDatabaseType.Text != _databaseOptions.DefaultOutputDatabaseType
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultOutputDatabaseType"] = this.cboDefaultOutputDatabaseType.Text;
            }

            if (this.txtDefaultOutputDatabaseConnectionString.Text != _databaseOptions.DefaultOutputDatabaseConnectionString
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultOutputDatabaseConnectionString"] = this.txtDefaultOutputDatabaseConnectionString.Text;
            }

            if (this.chkOverwriteOutputDestinationIfAlreadyExists.Checked != _databaseOptions.OverwriteOutputDestinationIfAlreadyExists
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["OverwriteOutputDestinationIfAlreadyExists"] = this.chkOverwriteOutputDestinationIfAlreadyExists.Checked;
            }


            if (numErrors > 0)
            {
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }

            _msg.Length = 0;
            if (numUpdates > 0)
            {
                //save the changes
                pfDataExtractorCP.Properties.Settings.Default.Save();
                if (numErrors > 0)
                {
                    _msg.Append("Update results: ");
                    _msg.Append(numUpdates.ToString());
                    _msg.Append(" items updated.");
                    _msg.Append(numErrors.ToString());
                    _msg.Append(" update errors.");
                }
                else
                {
                    _msg.Append(numUpdates.ToString());
                    _msg.Append(" items were successfully updated.");
                    SaveDatabaseConfigItems();
                }
            }
            else
            {
                if (numErrors > 0)
                {
                    _msg.Append(numErrors.ToString());
                    _msg.Append(" encountered during update.");
                }
                else
                {
                    _msg.Append("No updates were needed. No data changes found.");
                }
            }

            this.lblUpdateResults.Text = _msg.ToString();

            if (numErrors == 0)
                updateSuccessful = true;
            else
                updateSuccessful = false;

            return updateSuccessful;
        }


        private void ShowPageSettings()
        {
            _printer.ShowPageSettings();
        }

        private void SettingsPrint(bool preview, bool showPrintDialog)
        {
            _printer.PageTitle = AppGlobals.AppInfo.AssemblyDescription;
            _printer.PageSubTitle = "Database Options Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }

        private void DefineConnectionString(ComboBox cboDatabaseType, TextBox txtConnectionString)
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ConnectionStringPrompt cp = null;

            if (cboDatabaseType.Text.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a database type before defining a connection string.");
                AppMessages.DisplayWarningMessage(_msg.ToString());
                return;
            }

            try
            {
                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), cboDatabaseType.Text);
                connMgr = new PFConnectionManager();
                cp = new ConnectionStringPrompt(dbPlat, connMgr);
                cp.ConnectionString = txtConnectionString.Text;
                System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
                if (res == DialogResult.OK)
                {
                    txtConnectionString.Text = cp.ConnectionString;
                }
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

        private void GetDefaultConnectionStringForDatabaseType(ComboBox cboDatabaseType, TextBox txtConnectionString)
        {
            txtConnectionString.Text = AppConfig.GetStringValueFromConfigFile("DefaultConnection_" + cboDatabaseType.Text, string.Empty);
        }


    }//end class
#pragma warning restore 1591

}//end namespace
