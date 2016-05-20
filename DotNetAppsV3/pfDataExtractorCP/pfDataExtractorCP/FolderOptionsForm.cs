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

namespace pfDataExtractorCP
{
#pragma warning disable 1591

    public partial class FolderOptionsForm : Form
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

        private class PFFolderOptions
        {
            private string _defaultExtractorDefinitionsSaveFolder = string.Empty;
            private string _defaultQueryDefinitionsFolder = string.Empty;
            private string _defaultDataGridExportFolder = string.Empty;

            private string _defaultSourceAccessDatabaseFolder = string.Empty;
            private string _defaultSourceExcelDataFileFolder = string.Empty;
            private string _defaultSourceDelimitedTextFileFolder = string.Empty;
            private string _defaultSourceFixedLengthTextFileFolder = string.Empty;
            private string _defaultSourceXmlFileFolder = string.Empty;

            private string _defaultDestinationAccessDatabaseFolder = string.Empty;
            private string _defaultDestinationExcelDataFileFolder = string.Empty;
            private string _defaultDestinationDelimitedTextFileFolder = string.Empty;
            private string _defaultDestinationFixedLengthTextFileFolder = string.Empty;
            private string _defaultDestinationXmlFileFolder = string.Empty;

            /// <summary>
            /// DefaultExtractorDefinitionsSaveFolder Property.
            /// </summary>
            public string DefaultExtractorDefinitionsSaveFolder
            {
                get
                {
                    return _defaultExtractorDefinitionsSaveFolder;
                }
                set
                {
                    _defaultExtractorDefinitionsSaveFolder = value;
                }
            }

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
            /// DefaultSourceAccessDatabaseFolder Property.
            /// </summary>
            public string DefaultSourceAccessDatabaseFolder
            {
                get
                {
                    return _defaultSourceAccessDatabaseFolder;
                }
                set
                {
                    _defaultSourceAccessDatabaseFolder = value;
                }
            }

            /// <summary>
            /// DefaultSourceExcelDataFileFolder Property.
            /// </summary>
            public string DefaultSourceExcelDataFileFolder
            {
                get
                {
                    return _defaultSourceExcelDataFileFolder;
                }
                set
                {
                    _defaultSourceExcelDataFileFolder = value;
                }
            }

            /// <summary>
            /// DefaultSourceDelimitedTextFileFolder Property.
            /// </summary>
            public string DefaultSourceDelimitedTextFileFolder
            {
                get
                {
                    return _defaultSourceDelimitedTextFileFolder;
                }
                set
                {
                    _defaultSourceDelimitedTextFileFolder = value;
                }
            }

            /// <summary>
            /// DefaultSourceFixedLengthTextFileFolder Property.
            /// </summary>
            public string DefaultSourceFixedLengthTextFileFolder
            {
                get
                {
                    return _defaultSourceFixedLengthTextFileFolder;
                }
                set
                {
                    _defaultSourceFixedLengthTextFileFolder = value;
                }
            }

            /// <summary>
            /// DefaultSourceXmlFileFolder Property.
            /// </summary>
            public string DefaultSourceXmlFileFolder
            {
                get
                {
                    return _defaultSourceXmlFileFolder;
                }
                set
                {
                    _defaultSourceXmlFileFolder = value;
                }
            }


            /// <summary>
            /// DefaultDestinationAccessDatabaseFolder Property.
            /// </summary>
            public string DefaultDestinationAccessDatabaseFolder
            {
                get
                {
                    return _defaultDestinationAccessDatabaseFolder;
                }
                set
                {
                    _defaultDestinationAccessDatabaseFolder = value;
                }
            }

            /// <summary>
            /// DefaultDestinationExcelDataFileFolder Property.
            /// </summary>
            public string DefaultDestinationExcelDataFileFolder
            {
                get
                {
                    return _defaultDestinationExcelDataFileFolder;
                }
                set
                {
                    _defaultDestinationExcelDataFileFolder = value;
                }
            }

            /// <summary>
            /// DefaultDestinationDelimitedTextFileFolder Property.
            /// </summary>
            public string DefaultDestinationDelimitedTextFileFolder
            {
                get
                {
                    return _defaultDestinationDelimitedTextFileFolder;
                }
                set
                {
                    _defaultDestinationDelimitedTextFileFolder = value;
                }
            }

            /// <summary>
            /// DefaultDestinationFixedLengthTextFileFolder Property.
            /// </summary>
            public string DefaultDestinationFixedLengthTextFileFolder
            {
                get
                {
                    return _defaultDestinationFixedLengthTextFileFolder;
                }
                set
                {
                    _defaultDestinationFixedLengthTextFileFolder = value;
                }
            }

            /// <summary>
            /// DefaultDestinationXmlFileFolder Property.
            /// </summary>
            public string DefaultDestinationXmlFileFolder
            {
                get
                {
                    return _defaultDestinationXmlFileFolder;
                }
                set
                {
                    _defaultDestinationXmlFileFolder = value;
                }
            }


        }//end private class

        PFFolderOptions _folderOptions = new PFFolderOptions();


        /// <summary>
        /// Constructor
        /// </summary>
        public FolderOptionsForm()
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
            UpdateFolderConfigItems();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            LoadFolderConfigItems();
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

        private void cmdSeExtractorDefinitionsSaveFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultExtractorDefinitionsSaveFolder);
        }

        private void cmdGetQueryDefinitionsFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultQueryDefinitionsFolder);
        }

        private void cmdGetDataGridExportFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultDataGridExportFolder);
        }

        private void cmdSetDefaultSourceAccessDatabaseFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultSourceAccessDatabaseFolder);
        }

        private void cmdSetDefaultSourceExcelDataFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultSourceExcelDataFileFolder);
        }

        private void cmdSetDefaultSourceDelimitedTextFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultSourceDelimitedTextFileFolder);
        }

        private void cmdSetDefaultSourceFixedLengthTextFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultSourceFixedLengthTextFileFolder);
        }

        private void cmdSetDefaultSourceXmlFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultSourceXmlFileFolder);
        }

        private void cmdSetDefaultDestinationAccessDatabaseFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultDestinationAccessDatabaseFolder);
        }

        private void cmdSetDefaulDestinationExcelDataFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultDestinationExcelDataFileFolder);
        }

        private void cmdSetDefaultDestinationDelimitedTextFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultDestinationDelimitedTextFileFolder);
        }

        private void cmdSetDefaultDestinationFixedLengthTextFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultDestinationFixedLengthTextFileFolder);
        }

        private void cmdSetDefaultDestinationXmlFileFolder_Click(object sender, EventArgs e)
        {
            SetDefaultFolder(this.txtDefaultDestinationXmlFileFolder);
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
            SettingsInitializer si = new SettingsInitializer();
            DialogResult res = AppMessages.DisplayMessage("Do you want to replace all user settings with their original installation values?", "User Settings ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
                LoadDefaultFolderConfigItems();
                UpdateUserConfigItems(true);
                si.InitFolderOptionsSettings();
                LoadFolderConfigItems();
            }
        }

        private void mnuSettingsCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        //form events
        private void FolderOptionsForm_Load(object sender, EventArgs e)
        {
            string formCaption = string.Empty;

            _msg.Length = 0;
            _msg.Append(AppInfo.AssemblyProduct);
            _msg.Append(" Folder Options");
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

            InitDefaultDirectories();

            //InitDefaultFolderSettings();

            LoadFolderConfigItems();

            EnableFormControls();
        }

        private void InitDefaultDirectories()
        {
            SettingsInitializer si = new SettingsInitializer();

            si.InitFolderOptionsSettings();   //routine creates default directories if they do not already exist

        }

        //private void InitDefaultFolderSettings()
        //{
        //    SettingsInitializer si = new SettingsInitializer();

        //    si.InitFolderOptionsSettings();

        //}

        private void SaveFolderConfigItems()
        {
            _folderOptions.DefaultExtractorDefinitionsSaveFolder = this.txtDefaultExtractorDefinitionsSaveFolder.Text;
            _folderOptions.DefaultQueryDefinitionsFolder = this.txtDefaultQueryDefinitionsFolder.Text;
            _folderOptions.DefaultDataGridExportFolder = this.txtDefaultDataGridExportFolder.Text;
            _folderOptions.DefaultSourceAccessDatabaseFolder = this.txtDefaultSourceAccessDatabaseFolder.Text;
            _folderOptions.DefaultSourceExcelDataFileFolder = this.txtDefaultSourceExcelDataFileFolder.Text;
            _folderOptions.DefaultSourceDelimitedTextFileFolder = this.txtDefaultSourceDelimitedTextFileFolder.Text;
            _folderOptions.DefaultSourceFixedLengthTextFileFolder = this.txtDefaultSourceFixedLengthTextFileFolder.Text;
            _folderOptions.DefaultSourceXmlFileFolder = this.txtDefaultSourceXmlFileFolder.Text;
            _folderOptions.DefaultDestinationAccessDatabaseFolder = this.txtDefaultDestinationAccessDatabaseFolder.Text;
            _folderOptions.DefaultDestinationExcelDataFileFolder = this.txtDefaultDestinationExcelDataFileFolder.Text;
            _folderOptions.DefaultDestinationDelimitedTextFileFolder = this.txtDefaultDestinationDelimitedTextFileFolder.Text;
            _folderOptions.DefaultDestinationFixedLengthTextFileFolder = this.txtDefaultDestinationFixedLengthTextFileFolder.Text;
            _folderOptions.DefaultDestinationXmlFileFolder = this.txtDefaultDestinationXmlFileFolder.Text;
        }

        private void LoadFolderConfigItems()
        {
            _folderOptions.DefaultExtractorDefinitionsSaveFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultExtractorDefinitionsSaveFolder;
            _folderOptions.DefaultQueryDefinitionsFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultQueryDefinitionsFolder;
            _folderOptions.DefaultDataGridExportFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDataGridExportFolder;
            _folderOptions.DefaultSourceAccessDatabaseFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceAccessDatabaseFolder;
            _folderOptions.DefaultSourceExcelDataFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceExcelDataFileFolder;
            _folderOptions.DefaultSourceDelimitedTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceDelimitedTextFileFolder;
            _folderOptions.DefaultSourceFixedLengthTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceFixedLengthTextFileFolder;
            _folderOptions.DefaultSourceXmlFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceXmlFileFolder;
            _folderOptions.DefaultDestinationAccessDatabaseFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationAccessDatabaseFolder;
            _folderOptions.DefaultDestinationExcelDataFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationExcelDataFileFolder;
            _folderOptions.DefaultDestinationDelimitedTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationDelimitedTextFileFolder;
            _folderOptions.DefaultDestinationFixedLengthTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationFixedLengthTextFileFolder;
            _folderOptions.DefaultDestinationXmlFileFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationXmlFileFolder;

            this.txtDefaultExtractorDefinitionsSaveFolder.Text = _folderOptions.DefaultExtractorDefinitionsSaveFolder;
            this.txtDefaultQueryDefinitionsFolder.Text = _folderOptions.DefaultQueryDefinitionsFolder;
            this.txtDefaultDataGridExportFolder.Text = _folderOptions.DefaultDataGridExportFolder;
            this.txtDefaultSourceAccessDatabaseFolder.Text = _folderOptions.DefaultSourceAccessDatabaseFolder;
            this.txtDefaultSourceExcelDataFileFolder.Text = _folderOptions.DefaultSourceExcelDataFileFolder;
            this.txtDefaultSourceDelimitedTextFileFolder.Text = _folderOptions.DefaultSourceDelimitedTextFileFolder;
            this.txtDefaultSourceFixedLengthTextFileFolder.Text = _folderOptions.DefaultSourceFixedLengthTextFileFolder;
            this.txtDefaultSourceXmlFileFolder.Text = _folderOptions.DefaultSourceXmlFileFolder;
            this.txtDefaultDestinationAccessDatabaseFolder.Text = _folderOptions.DefaultDestinationAccessDatabaseFolder;
            this.txtDefaultDestinationExcelDataFileFolder.Text = _folderOptions.DefaultDestinationExcelDataFileFolder;
            this.txtDefaultDestinationDelimitedTextFileFolder.Text = _folderOptions.DefaultDestinationDelimitedTextFileFolder;
            this.txtDefaultDestinationFixedLengthTextFileFolder.Text = _folderOptions.DefaultDestinationFixedLengthTextFileFolder;
            this.txtDefaultDestinationXmlFileFolder.Text = _folderOptions.DefaultDestinationXmlFileFolder;

        }

        private void LoadDefaultFolderConfigItems()
        {
            _folderOptions.DefaultExtractorDefinitionsSaveFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultExtractorDefinitionsSaveFolder"].DefaultValue.ToString();
            _folderOptions.DefaultQueryDefinitionsFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultQueryDefinitionsFolder"].DefaultValue.ToString();
            _folderOptions.DefaultDataGridExportFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["defaultDataGridExportFolder"].DefaultValue.ToString();
            _folderOptions.DefaultSourceAccessDatabaseFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultSourceAccessDatabaseFolder"].DefaultValue.ToString();
            _folderOptions.DefaultSourceExcelDataFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultSourceExcelDataFileFolder"].DefaultValue.ToString();
            _folderOptions.DefaultSourceDelimitedTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultSourceDelimitedTextFileFolder"].DefaultValue.ToString();
            _folderOptions.DefaultSourceFixedLengthTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultSourceFixedLengthTextFileFolder"].DefaultValue.ToString();
            _folderOptions.DefaultSourceXmlFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultSourceXmlFileFolder"].DefaultValue.ToString();
            _folderOptions.DefaultDestinationAccessDatabaseFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultDestinationAccessDatabaseFolder"].DefaultValue.ToString();
            _folderOptions.DefaultDestinationExcelDataFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultDestinationExcelDataFileFolder"].DefaultValue.ToString();
            _folderOptions.DefaultDestinationDelimitedTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultDestinationDelimitedTextFileFolder"].DefaultValue.ToString();
            _folderOptions.DefaultDestinationFixedLengthTextFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultDestinationFixedLengthTextFileFolder"].DefaultValue.ToString();
            _folderOptions.DefaultDestinationXmlFileFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultDestinationXmlFileFolder"].DefaultValue.ToString();

            this.txtDefaultExtractorDefinitionsSaveFolder.Text = _folderOptions.DefaultExtractorDefinitionsSaveFolder;
            this.txtDefaultQueryDefinitionsFolder.Text = _folderOptions.DefaultQueryDefinitionsFolder;
            this.txtDefaultDataGridExportFolder.Text = _folderOptions.DefaultDataGridExportFolder;
            this.txtDefaultSourceAccessDatabaseFolder.Text = _folderOptions.DefaultSourceAccessDatabaseFolder;
            this.txtDefaultSourceExcelDataFileFolder.Text = _folderOptions.DefaultSourceExcelDataFileFolder;
            this.txtDefaultSourceDelimitedTextFileFolder.Text = _folderOptions.DefaultSourceDelimitedTextFileFolder;
            this.txtDefaultSourceFixedLengthTextFileFolder.Text = _folderOptions.DefaultSourceFixedLengthTextFileFolder;
            this.txtDefaultSourceXmlFileFolder.Text = _folderOptions.DefaultSourceXmlFileFolder;
            this.txtDefaultDestinationAccessDatabaseFolder.Text = _folderOptions.DefaultDestinationAccessDatabaseFolder;
            this.txtDefaultDestinationExcelDataFileFolder.Text = _folderOptions.DefaultDestinationExcelDataFileFolder;
            this.txtDefaultDestinationDelimitedTextFileFolder.Text = _folderOptions.DefaultDestinationDelimitedTextFileFolder;
            this.txtDefaultDestinationFixedLengthTextFileFolder.Text = _folderOptions.DefaultDestinationFixedLengthTextFileFolder;
            this.txtDefaultDestinationXmlFileFolder.Text = _folderOptions.DefaultDestinationXmlFileFolder;

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
            res = UpdateFolderConfigItems();
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
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Change Folder Settings");
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

        private void SetDefaultFolder(TextBox txtBox)
        {
            string initFolder = txtBox.Text;
            ShowFolderBrowserDialog(ref initFolder);
            txtBox.Text = initFolder;
        }

        private bool UpdateFolderConfigItems()
        {
            return UpdateUserConfigItems(false);
        }

        private bool UpdateUserConfigItems(bool forceUpdate)
        {
            bool updateSuccessful = true;
            int numErrors = 0;
            int numUpdates = 0;

            _msg.Length = 0;

            if (this.txtDefaultExtractorDefinitionsSaveFolder.Text.ToUpper().Trim() != _folderOptions.DefaultExtractorDefinitionsSaveFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultExtractorDefinitionsSaveFolder.Text.ToUpper().Trim())
                    || this.txtDefaultExtractorDefinitionsSaveFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultExtractorDefinitionsSaveFolder"] = this.txtDefaultExtractorDefinitionsSaveFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultExtractorDefinitionsSaveFolder does not exist: ");
                    _msg.Append(this.txtDefaultExtractorDefinitionsSaveFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultQueryDefinitionsFolder.Text.ToUpper().Trim() != _folderOptions.DefaultQueryDefinitionsFolder.ToUpper().Trim()
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

            if (this.txtDefaultDataGridExportFolder.Text.ToUpper().Trim() != _folderOptions.DefaultDataGridExportFolder.ToUpper().Trim()
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

            if (this.txtDefaultSourceAccessDatabaseFolder.Text.ToUpper().Trim() != _folderOptions.DefaultSourceAccessDatabaseFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultSourceAccessDatabaseFolder.Text.ToUpper().Trim())
                    || this.txtDefaultSourceAccessDatabaseFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceAccessDatabaseFolder"] = this.txtDefaultSourceAccessDatabaseFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultSourceAccessDatabaseFolder does not exist: ");
                    _msg.Append(this.txtDefaultSourceAccessDatabaseFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultSourceExcelDataFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultSourceExcelDataFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultSourceExcelDataFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultSourceExcelDataFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceExcelDataFileFolder"] = this.txtDefaultSourceExcelDataFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultSourceExcelDataFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultSourceExcelDataFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultSourceDelimitedTextFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultSourceDelimitedTextFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultSourceDelimitedTextFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultSourceDelimitedTextFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceDelimitedTextFileFolder"] = this.txtDefaultSourceDelimitedTextFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultSourceDelimitedTextFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultSourceDelimitedTextFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultSourceFixedLengthTextFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultSourceFixedLengthTextFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultSourceFixedLengthTextFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultSourceFixedLengthTextFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceFixedLengthTextFileFolder"] = this.txtDefaultSourceFixedLengthTextFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultSourceFixedLengthTextFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultSourceFixedLengthTextFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultSourceXmlFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultSourceXmlFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultSourceXmlFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultSourceXmlFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultSourceXmlFileFolder"] = this.txtDefaultSourceXmlFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultSourceXmlFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultSourceXmlFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }


            if (this.txtDefaultDestinationAccessDatabaseFolder.Text.ToUpper().Trim() != _folderOptions.DefaultDestinationAccessDatabaseFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultDestinationAccessDatabaseFolder.Text.ToUpper().Trim())
                    || this.txtDefaultDestinationAccessDatabaseFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationAccessDatabaseFolder"] = this.txtDefaultDestinationAccessDatabaseFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultDestinationAccessDatabaseFolder does not exist: ");
                    _msg.Append(this.txtDefaultDestinationAccessDatabaseFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultDestinationExcelDataFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultDestinationExcelDataFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultDestinationExcelDataFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultDestinationExcelDataFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationExcelDataFileFolder"] = this.txtDefaultDestinationExcelDataFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultDestinationExcelDataFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultDestinationExcelDataFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultDestinationDelimitedTextFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultDestinationDelimitedTextFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultDestinationDelimitedTextFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultDestinationDelimitedTextFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationDelimitedTextFileFolder"] = this.txtDefaultDestinationDelimitedTextFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultDestinationDelimitedTextFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultDestinationDelimitedTextFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultDestinationFixedLengthTextFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultDestinationFixedLengthTextFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultDestinationFixedLengthTextFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultDestinationFixedLengthTextFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationFixedLengthTextFileFolder"] = this.txtDefaultDestinationFixedLengthTextFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultDestinationFixedLengthTextFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultDestinationFixedLengthTextFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtDefaultDestinationXmlFileFolder.Text.ToUpper().Trim() != _folderOptions.DefaultDestinationXmlFileFolder.ToUpper().Trim()
                || forceUpdate == true)
            {
                if (Directory.Exists(this.txtDefaultDestinationXmlFileFolder.Text.ToUpper().Trim())
                    || this.txtDefaultDestinationXmlFileFolder.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    pfDataExtractorCP.Properties.Settings.Default["DefaultDestinationXmlFileFolder"] = this.txtDefaultDestinationXmlFileFolder.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("DefaultDestinationXmlFileFolder does not exist: ");
                    _msg.Append(this.txtDefaultDestinationXmlFileFolder.Text);
                    _msg.Append(Environment.NewLine);
                }
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
                    _msg.Append(" items were successfully updated. Defaults take effect next time the application is started.");
                    SaveFolderConfigItems();
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
            _printer.PageSubTitle = "Folder Options Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }


    }//end class
#pragma warning restore 1591

}//end namespace
