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
using PFConnectionObjects;
using PFConnectionStrings;
using PFCollectionsObjects;
using PFAppDataObjects;
using PFTextObjects;

namespace pfDataViewerCP
{
#pragma warning disable 1591

    public partial class UserOptionsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";

        private StringBuilder _textToPrint = new StringBuilder();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _helpFilePath = string.Empty;


        //see InitWinFormsAppWithDialogs (profast templates) and TestprogAppUtils (winapplib solution)
        //for examples of using PFFolderBrowserDialog, PFOpenFileDialog, PFSaveFileDialog usage
        private PFFolderBrowserDialog _folderBrowserDialog = new PFFolderBrowserDialog();

        private class PFUserOptions
        {
            private string _defaultQueryDefinitionsFolder = string.Empty;
            private string _defaultDataGridExportFolder = string.Empty;
            private bool _saveFormLocationsOnExit = false;
            private bool _saveShowInstalledDatabaseProvidersOnly = true;
            private bool _saveShowApplicationLogWindow = true;
            private string _defaultInputDatabaseType = string.Empty;
            private string _defaultInputDatabaseConnectionString = string.Empty;
            private string _defaultOutputDatabaseType = string.Empty;
            private string _defaultOutputDatabaseConnectionString = string.Empty;
            private Font _defaultApplicationFont = System.Drawing.SystemFonts.DefaultFont;
            private int _batchSizeForDataImportsAndExports = 1000;
            private int _batchSizeForRandomDataGeneration = 5000;

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
            /// DefaultDataGridExportFolder Property.
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
            /// SaveFormLocationsOnExit Property.
            /// </summary>
            public bool SaveFormLocationsOnExit
            {
                get
                {
                    return _saveFormLocationsOnExit;
                }
                set
                {
                    _saveFormLocationsOnExit = value;
                }
            }

            /// <summary>
            /// ShowInstalledDatabaseProvidersOnly Property. Set to true to only show installed database providers when prompting for a database provider.
            /// If set to false, then provider prompts will show all supported database providers.cha
            /// </summary>
            public bool ShowInstalledDatabaseProvidersOnly
            {
                get
                {
                    return _saveShowInstalledDatabaseProvidersOnly;
                }
                set
                {
                    _saveShowInstalledDatabaseProvidersOnly = value;
                }
            }

            /// <summary>
            /// ShowApplicationLogWindow Property. Set to true to make the application log window visible.
            /// </summary>
            public bool ShowApplicationLogWindow
            {
                get
                {
                    return _saveShowApplicationLogWindow;
                }
                set
                {
                    _saveShowApplicationLogWindow = value;
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
            /// Specifies default font settings to use for the application.
            /// </summary>
            public Font DefaultApplicationtFont
            {
                get
                {
                    return _defaultApplicationFont;
                }
                set
                {
                    _defaultApplicationFont = value;
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


        }//end private class

        PFUserOptions _userOptions = new PFUserOptions();


        /// <summary>
        /// Constructor
        /// </summary>
        public UserOptionsForm()
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
            UpdateUserConfigItems();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            LoadUserConfigItems();
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

        private void cmdChangeDefaultApplicationFont_Click(object sender, EventArgs e)
        {
            ChangeDefaultApplicationFont();
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
            DialogResult res = AppMessages.DisplayMessage("Do you want to replace all user settings with their original installation values?", "User Settings ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
                LoadDefaultUserConfigItems();
                UpdateUserConfigItems(true);
            }
        }

        private void mnuSettingsCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        //form events
        private void UserOptionsForm_Load(object sender, EventArgs e)
        {
            string formCaption = string.Empty;

            _msg.Length = 0;
            _msg.Append(AppInfo.AssemblyProduct);
            _msg.Append(" User Options");
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
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "InitWinFormsHelpFile.chm");
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

            LoadDefaultApplicationFont();

            LoadUserConfigItems();

            EnableFormControls();
        }

        private void LoadDefaultApplicationFont()
        {
            if (pfDataViewerCP.Properties.Settings.Default.DefaultApplicationFont == null)
            {
                pfDataViewerCP.Properties.Settings.Default.DefaultApplicationFont = System.Drawing.SystemFonts.DefaultFont;
            }
            Font fnt = pfDataViewerCP.Properties.Settings.Default.DefaultApplicationFont;
            _msg.Length = 0;
            _msg.Append(fnt.Name);
            _msg.Append(",");
            _msg.Append(fnt.Size.ToString());
            _msg.Append(",");
            _msg.Append(fnt.Style.ToString());
            this.txtDefaultApplicationFont.Text = _msg.ToString();
        }

        private void SaveUserConfigItems()
        {
            _userOptions.DefaultQueryDefinitionsFolder = this.txtDefaultQueryDefinitionsFolder.Text;
            _userOptions.DefaultDataGridExportFolder = txtDefaultDataGridExportFolder.Text;
            _userOptions.SaveFormLocationsOnExit = this.chkSaveFormLocationsOnExit.Checked;
            _userOptions.ShowInstalledDatabaseProvidersOnly = chkShowInstalledDatabaseProvidersOnly.Checked;
            _userOptions.ShowApplicationLogWindow = this.chkShowApplicationLogWindow.Checked;
            _userOptions.DefaultInputDatabaseType = this.cboDefaultInputDatabaseType.Text;
            _userOptions.DefaultInputDatabaseConnectionString = this.txtDefaultInputDatabaseConnectionString.Text;
            _userOptions.DefaultOutputDatabaseType = this.cboDefaultOutputDatabaseType.Text;
            _userOptions.DefaultOutputDatabaseConnectionString = this.txtDefaultOutputDatabaseConnectionString.Text;
            //_userOptions.DefaultApplicationtFont is saved in ChangeDefaultApplicationFont method
            _userOptions.BatchSizeForDataImportsAndExports = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000);
            _userOptions.BatchSizeForRandomDataGeneration = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 5000);
        }

        private void LoadUserConfigItems()
        {
            _userOptions.DefaultQueryDefinitionsFolder = pfDataViewerCP.Properties.Settings.Default.DefaultQueryDefinitionsFolder;
            _userOptions.DefaultDataGridExportFolder = pfDataViewerCP.Properties.Settings.Default.DefaultDataGridExportFolder;
            _userOptions.SaveFormLocationsOnExit = pfDataViewerCP.Properties.Settings.Default.SaveFormLocationsOnExit;
            _userOptions.ShowInstalledDatabaseProvidersOnly = pfDataViewerCP.Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            _userOptions.ShowApplicationLogWindow = pfDataViewerCP.Properties.Settings.Default.ShowApplicationLogWindow;
            _userOptions.DefaultInputDatabaseType = pfDataViewerCP.Properties.Settings.Default.DefaultInputDatabaseType;
            _userOptions.DefaultInputDatabaseConnectionString = pfDataViewerCP.Properties.Settings.Default.DefaultInputDatabaseConnectionString;
            _userOptions.DefaultOutputDatabaseType = pfDataViewerCP.Properties.Settings.Default.DefaultOutputDatabaseType;
            _userOptions.DefaultOutputDatabaseConnectionString = pfDataViewerCP.Properties.Settings.Default.DefaultOutputDatabaseConnectionString;
            _userOptions.DefaultApplicationtFont = pfDataViewerCP.Properties.Settings.Default.DefaultApplicationFont;
            _userOptions.BatchSizeForDataImportsAndExports = pfDataViewerCP.Properties.Settings.Default.BatchSizeForDataImportsAndExports;
            _userOptions.BatchSizeForRandomDataGeneration = pfDataViewerCP.Properties.Settings.Default.BatchSizeForRandomDataGeneration;

            if (_userOptions.DefaultQueryDefinitionsFolder.Trim().Length == 0)
                _userOptions.DefaultQueryDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\QueryDefs\";

            if (_userOptions.DefaultDataGridExportFolder.Trim().Length == 0)
                _userOptions.DefaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\DataExports\";

            this.txtDefaultQueryDefinitionsFolder.Text = _userOptions.DefaultQueryDefinitionsFolder;
            this.txtDefaultDataGridExportFolder.Text = _userOptions.DefaultDataGridExportFolder;
            this.chkSaveFormLocationsOnExit.Checked = _userOptions.SaveFormLocationsOnExit;
            this.chkShowInstalledDatabaseProvidersOnly.Checked = _userOptions.ShowInstalledDatabaseProvidersOnly;
            this.chkShowApplicationLogWindow.Checked = _userOptions.ShowApplicationLogWindow;
            this.cboDefaultInputDatabaseType.Text = _userOptions.DefaultInputDatabaseType;
            this.txtDefaultInputDatabaseConnectionString.Text = _userOptions.DefaultInputDatabaseConnectionString;
            this.cboDefaultOutputDatabaseType.Text = _userOptions.DefaultOutputDatabaseType;
            this.txtDefaultOutputDatabaseConnectionString.Text = _userOptions.DefaultOutputDatabaseConnectionString;
            LoadDefaultApplicationFont();
            this.txtBatchSizeForDataImportsAndExports.Text = _userOptions.BatchSizeForDataImportsAndExports.ToString();
            this.txtBatchSizeForRandomDataGeneration.Text = _userOptions.BatchSizeForRandomDataGeneration.ToString();

        }

        private void LoadDefaultUserConfigItems()
        {
            _userOptions.DefaultQueryDefinitionsFolder = pfDataViewerCP.Properties.Settings.Default.Properties["DefaultQueryDefinitionsFolder"].DefaultValue.ToString();
            _userOptions.DefaultDataGridExportFolder = pfDataViewerCP.Properties.Settings.Default.Properties["DefaultDataGridExportFolder"].DefaultValue.ToString();
            _userOptions.SaveFormLocationsOnExit = Convert.ToBoolean(pfDataViewerCP.Properties.Settings.Default.Properties["SaveFormLocationsOnExit"].DefaultValue);
            _userOptions.ShowInstalledDatabaseProvidersOnly = Convert.ToBoolean(pfDataViewerCP.Properties.Settings.Default.Properties["ShowInstalledDatabaseProvidersOnly"].DefaultValue);
            _userOptions.ShowApplicationLogWindow = Convert.ToBoolean(pfDataViewerCP.Properties.Settings.Default.Properties["ShowApplicationLogWindow"].DefaultValue);
            _userOptions.DefaultInputDatabaseType = pfDataViewerCP.Properties.Settings.Default.Properties["DefaultInputDatabaseType"].DefaultValue.ToString();
            _userOptions.DefaultInputDatabaseConnectionString = pfDataViewerCP.Properties.Settings.Default.Properties["DefaultInputDatabaseConnectionString"].DefaultValue.ToString();
            _userOptions.DefaultOutputDatabaseType = pfDataViewerCP.Properties.Settings.Default.Properties["DefaultOutputDatabaseType"].DefaultValue.ToString();
            _userOptions.DefaultOutputDatabaseConnectionString = pfDataViewerCP.Properties.Settings.Default.Properties["DefaultOutputDatabaseConnectionString"].DefaultValue.ToString();
            _userOptions.DefaultApplicationtFont = (Font)pfDataViewerCP.Properties.Settings.Default.Properties["DefaultApplicationFont"].DefaultValue;
            _userOptions.BatchSizeForDataImportsAndExports = Convert.ToInt32(pfDataViewerCP.Properties.Settings.Default.Properties["BatchSizeForDataImportsAndExports"].DefaultValue);
            _userOptions.BatchSizeForRandomDataGeneration = Convert.ToInt32(pfDataViewerCP.Properties.Settings.Default.Properties["BatchSizeForRandomDataGeneration"].DefaultValue);

            if (_userOptions.DefaultQueryDefinitionsFolder.Trim().Length == 0)
                _userOptions.DefaultQueryDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\QueryDefs\";

            if (_userOptions.DefaultDataGridExportFolder.Trim().Length == 0)
                _userOptions.DefaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\DataExports\";

            this.txtDefaultQueryDefinitionsFolder.Text = _userOptions.DefaultQueryDefinitionsFolder;
            this.txtDefaultDataGridExportFolder.Text = _userOptions.DefaultDataGridExportFolder;
            this.chkSaveFormLocationsOnExit.Checked = _userOptions.SaveFormLocationsOnExit;
            this.chkShowInstalledDatabaseProvidersOnly.Checked = _userOptions.ShowInstalledDatabaseProvidersOnly;
            this.chkShowApplicationLogWindow.Checked = _userOptions.ShowApplicationLogWindow;
            this.cboDefaultInputDatabaseType.Text = _userOptions.DefaultInputDatabaseType;
            this.txtDefaultInputDatabaseConnectionString.Text = _userOptions.DefaultInputDatabaseConnectionString;
            this.cboDefaultOutputDatabaseType.Text = _userOptions.DefaultOutputDatabaseType;
            this.txtDefaultOutputDatabaseConnectionString.Text = _userOptions.DefaultOutputDatabaseConnectionString;
            LoadDefaultApplicationFont();
            this.txtBatchSizeForDataImportsAndExports.Text = _userOptions.BatchSizeForDataImportsAndExports.ToString();
            this.txtBatchSizeForRandomDataGeneration.Text = _userOptions.BatchSizeForRandomDataGeneration.ToString();

        } 
        private void InitDefaultDatabaseType(ComboBox cboDatabaseType, TextBox txtDefaultDatabseConnectionString)
        {
            bool showInstalledDatabaseProvidersOnly = pfDataViewerCP.Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
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

            if (DatabaseTypeIsInList(cboDatabaseType)==false)
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
            res = UpdateUserConfigItems();
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

        private void ChangeDefaultApplicationFont()
        {
            FontDialog dialog = this.FontDialog1;
            dialog.Font = pfDataViewerCP.Properties.Settings.Default.DefaultApplicationFont;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.DefaultApplicationFont = dialog.Font;
                LoadDefaultApplicationFont();
                _msg.Length = 0;
                _msg.Append("Default application font updated.");
                this.lblUpdateResults.Text = _msg.ToString();
            }
            dialog = null;
            this.Focus();
        }

        private void DisplayHelp()
        {
            if (File.Exists(_helpFilePath))
            {
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Change a User Setting");
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
                if (selectedFolder.EndsWith(@"\") == false)
                    selectedFolder += @"\";
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

        private bool UpdateUserConfigItems()
        {
            return UpdateUserConfigItems(false);
        }

        private bool UpdateUserConfigItems(bool forceUpdate)
        {
            bool updateSuccessful = true;
            int numErrors = 0;
            int numUpdates = 0;
            bool reloadDatabaseTypes = false;

            _msg.Length = 0;

            if (this.txtDefaultQueryDefinitionsFolder.Text.ToUpper().Trim() != _userOptions.DefaultQueryDefinitionsFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultQueryDefinitionsFolder.Text.ToUpper().Trim())
                    || this.txtDefaultQueryDefinitionsFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataViewerCP.Properties.Settings.Default["DefaultQueryDefinitionsFolder"] = this.txtDefaultQueryDefinitionsFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultQueryDefinitionsFolder does not exist: ");
                    _msg.Append(this.txtDefaultQueryDefinitionsFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultDataGridExportFolder.Text.ToUpper().Trim() != _userOptions.DefaultDataGridExportFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultDataGridExportFolder.Text.ToUpper().Trim())
                    || this.txtDefaultDataGridExportFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataViewerCP.Properties.Settings.Default["DefaultDataGridExportFolder"] = this.txtDefaultDataGridExportFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultDataGridExportFolder does not exist: ");
                    _msg.Append(this.txtDefaultDataGridExportFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.chkSaveFormLocationsOnExit.Checked != _userOptions.SaveFormLocationsOnExit
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["SaveFormLocationsOnExit"] = this.chkSaveFormLocationsOnExit.Checked;
            }

            if (this.chkShowInstalledDatabaseProvidersOnly.Checked != _userOptions.ShowInstalledDatabaseProvidersOnly
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["ShowInstalledDatabaseProvidersOnly"] = this.chkShowInstalledDatabaseProvidersOnly.Checked;
                reloadDatabaseTypes = true;
            }

            if (this.chkShowApplicationLogWindow.Checked != _userOptions.ShowApplicationLogWindow
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["ShowApplicationLogWindow"] = this.chkShowApplicationLogWindow.Checked;
            }

            if (this.cboDefaultInputDatabaseType.Text != _userOptions.DefaultInputDatabaseType
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["DefaultInputDatabaseType"] = this.cboDefaultInputDatabaseType.Text;
            }

            if (this.txtDefaultInputDatabaseConnectionString.Text != _userOptions.DefaultInputDatabaseConnectionString
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["DefaultInputDatabaseConnectionString"] = this.txtDefaultInputDatabaseConnectionString.Text;
            }

            if (this.cboDefaultOutputDatabaseType.Text != _userOptions.DefaultOutputDatabaseType
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["DefaultOutputDatabaseType"] = this.cboDefaultOutputDatabaseType.Text;
            }

            if (this.txtDefaultOutputDatabaseConnectionString.Text != _userOptions.DefaultOutputDatabaseConnectionString
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["DefaultOutputDatabaseConnectionString"] = this.txtDefaultOutputDatabaseConnectionString.Text;
            }

            if (PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 0) != _userOptions.BatchSizeForDataImportsAndExports
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["BatchSizeForDataImportsAndExports"] = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000);
            }

            if (PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 0) != _userOptions.BatchSizeForRandomDataGeneration 
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataViewerCP.Properties.Settings.Default["BatchSizeForRandomDataGeneration"] = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 5000);
            }

            


            if (numErrors > 0)
            {
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }

            _msg.Length = 0;
            if (numUpdates > 0)
            {
                //save the changes
                pfDataViewerCP.Properties.Settings.Default.Save();
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
                    SaveUserConfigItems();
                    if (reloadDatabaseTypes)
                    {
                        InitDefaultDatabaseType(this.cboDefaultInputDatabaseType, this.txtDefaultInputDatabaseConnectionString);
                        InitDefaultDatabaseType(this.cboDefaultOutputDatabaseType, this.txtDefaultOutputDatabaseConnectionString); //added 4/16/15 MC
                    }
                }
            }
            else
            {
                if (numErrors > 0)
                {
                    _msg.Append(numErrors.ToString());
                    if (numErrors == 1)
                        _msg.Append(" error ");
                    else
                        _msg.Append(" errors ");
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
            _printer.PageSubTitle = "User Options Form";
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
