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

namespace pfDataExtractorCP
{
#pragma warning disable 1591

    public partial class UserOptionsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";

        private FormPrinter _printer = null;
        private TextFormatter _textFormatter = new TextFormatter();

        private string _helpFilePath = string.Empty;

        //see InitWinFormsAppWithDialogs (profast templates) and TestprogAppUtils (winapplib solution)
        //for examples of using PFFolderBrowserDialog, PFOpenFileDialog, PFSaveFileDialog usage
        private PFFolderBrowserDialog _folderBrowserDialog = new PFFolderBrowserDialog();

        private class PFUserOptions
        {
            private bool _saveFormLocationsOnExit = false;
            private bool _showOutputLog = false;
            private bool _showInstalledDatabaseProvidersOnly = false;
            private bool _previewAllRows = false;
            private bool _limitPreviewRows = false;
            private int _maxNumberOfRows = 0;
            private string _defaultDataSourceType = string.Empty;
            private string _defaultDataDestinationType = string.Empty;
            private Font _defaultApplicationFont = System.Drawing.SystemFonts.DefaultFont;
            private int _batchSizeForDataImportsAndExports = 1000;
            private int _batchSizeForRandomDataGeneration = 5000;
            private string _defaultExtractorDefinitionsSaveFolder = string.Empty;
            private string _defaultExtractorDefinitionName = string.Empty;

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
            /// ShowOutputLog Property.
            /// </summary>
            public bool ShowOutputLog
            {
                get
                {
                    return _showOutputLog;
                }
                set
                {
                    _showOutputLog = value;
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
            /// PreviewAllRows Property.
            /// </summary>
            public bool PreviewAllRows
            {
                get
                {
                    return _previewAllRows;
                }
                set
                {
                    _previewAllRows = value;
                }
            }

            /// <summary>
            /// LimitPreviewRows Property.
            /// </summary>
            public bool LimitPreviewRows
            {
                get
                {
                    return _limitPreviewRows;
                }
                set
                {
                    _limitPreviewRows = value;
                }
            }

            /// <summary>
            /// MaxNumberOfRows Property.
            /// </summary>
            public int MaxNumberOfRows
            {
                get
                {
                    return _maxNumberOfRows;
                }
                set
                {
                    _maxNumberOfRows = value;
                }
            }


            /// <summary>
            /// DefaultDataSourceType Property.
            /// </summary>
            public string DefaultDataSourceType
            {
                get
                {
                    return _defaultDataSourceType;
                }
                set
                {
                    _defaultDataSourceType = value;
                }
            }

            /// <summary>
            /// DefaultDataDestinationType Property.
            /// </summary>
            public string DefaultDataDestinationType
            {
                get
                {
                    return _defaultDataDestinationType;
                }
                set
                {
                    _defaultDataDestinationType = value;
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
            /// DefaultExtractorDefinitionName Property.
            /// </summary>
            public string DefaultExtractorDefinitionName
            {
                get
                {
                    return _defaultExtractorDefinitionName;
                }
                set
                {
                    _defaultExtractorDefinitionName = value;
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

        private void optPreviewAllRows_CheckedChanged(object sender, EventArgs e)
        {
            PreviewRowsCheckChanged();
        }

        private void cmdChangeDefaultApplicationFont_Click(object sender, EventArgs e)
        {
            ChangeDefaultApplicationFont();
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
                LoadDefaultUserConfigItems();
                UpdateUserConfigItems(true);
                si.InitUserOptionsSettings();
                LoadUserConfigItems();
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

            InitDefaultUserSettings();

            LoadDefaultApplicationFont();

            LoadFormDropdowns();

            LoadUserConfigItems();

            EnableFormControls();
        }

        private void InitDefaultDirectories()
        {
            SettingsInitializer si = new SettingsInitializer();

            si.InitFolderOptionsSettings();   //routine creates default directories if they do not already exist

        }

        private void InitDefaultUserSettings()
        {
            SettingsInitializer si = new SettingsInitializer();

            si.InitUserOptionsSettings();

        }

        private void LoadDefaultApplicationFont()
        {
            if (pfDataExtractorCP.Properties.Settings.Default.DefaultApplicationFont == null)
            {
                pfDataExtractorCP.Properties.Settings.Default.DefaultApplicationFont = System.Drawing.SystemFonts.DefaultFont;
            }
            Font fnt = pfDataExtractorCP.Properties.Settings.Default.DefaultApplicationFont;
            _msg.Length = 0;
            _msg.Append(fnt.Name);
            _msg.Append(",");
            _msg.Append(fnt.Size.ToString());
            _msg.Append(",");
            _msg.Append(fnt.Style.ToString());
            this.txtDefaultApplicationFont.Text = _msg.ToString();
        }

        private void LoadFormDropdowns()
        {
            this.cboDefaultDataSourceType.Items.Clear();
            this.cboDefaultDataDestinationType.Items.Clear();

            for (int i = 0; i < ExtractorDataTypeList.ExtractorDataLocations.Length; i++)
            {
                this.cboDefaultDataSourceType.Items.Add(ExtractorDataTypeList.ExtractorDataLocations[i]);
                this.cboDefaultDataDestinationType.Items.Add(ExtractorDataTypeList.ExtractorDataLocations[i]);
            }

            this.cboDefaultDataSourceType.Text = this.cboDefaultDataSourceType.Items[0].ToString();
            this.cboDefaultDataDestinationType.Text = this.cboDefaultDataDestinationType.Items[0].ToString();

        }

        private void SaveUserConfigItems()
        {
            _userOptions.SaveFormLocationsOnExit = this.chkSaveFormLocationsOnExit.Checked;
            _userOptions.ShowOutputLog = this.chkShowOutputLog.Checked;
            _userOptions.ShowInstalledDatabaseProvidersOnly = this.chkShowInstalledDatabaseProvidersOnly.Checked;
            _userOptions.PreviewAllRows = this.optPreviewAllRows.Checked;
            _userOptions.LimitPreviewRows = this.optLimitPreviewRows.Checked;
            _userOptions.MaxNumberOfRows = PFTextProcessor.ConvertStringToInt(this.txtMaxNumberOfRows.Text, 1000);
            _userOptions.DefaultDataSourceType = this.cboDefaultDataSourceType.Text;
            _userOptions.DefaultDataDestinationType = this.cboDefaultDataDestinationType.Text;
            //_userOptions.DefaultApplicationtFont is saved in ChangeDefaultApplicationFont method
            _userOptions.BatchSizeForDataImportsAndExports = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000);
            _userOptions.BatchSizeForRandomDataGeneration = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 5000);
            _userOptions.DefaultExtractorDefinitionsSaveFolder = this.txtDefaultExtractorDefinitionsSaveFolder.Text;
            _userOptions.DefaultExtractorDefinitionName = this.txtDefaultExtractorDefinitionName.Text;
        }

        private void LoadUserConfigItems()
        {
            _userOptions.SaveFormLocationsOnExit = pfDataExtractorCP.Properties.Settings.Default.SaveFormLocationsOnExit;
            _userOptions.ShowOutputLog = pfDataExtractorCP.Properties.Settings.Default.ShowOutputLog;
            _userOptions.ShowInstalledDatabaseProvidersOnly = pfDataExtractorCP.Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            _userOptions.PreviewAllRows = pfDataExtractorCP.Properties.Settings.Default.PreviewAllRows;
            _userOptions.LimitPreviewRows = pfDataExtractorCP.Properties.Settings.Default.LimitPreviewRows;
            _userOptions.MaxNumberOfRows = pfDataExtractorCP.Properties.Settings.Default.MaxNumberOfRows;
            _userOptions.DefaultDataSourceType = pfDataExtractorCP.Properties.Settings.Default.DefaultDataSourceType;
            _userOptions.DefaultDataDestinationType = pfDataExtractorCP.Properties.Settings.Default.DefaultDataDestinationType;
            _userOptions.DefaultApplicationtFont = pfDataExtractorCP.Properties.Settings.Default.DefaultApplicationFont;
            _userOptions.BatchSizeForDataImportsAndExports = pfDataExtractorCP.Properties.Settings.Default.BatchSizeForDataImportsAndExports;
            _userOptions.BatchSizeForRandomDataGeneration = pfDataExtractorCP.Properties.Settings.Default.BatchSizeForRandomDataGeneration;
            _userOptions.DefaultExtractorDefinitionsSaveFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultExtractorDefinitionsSaveFolder;
            _userOptions.DefaultExtractorDefinitionName = pfDataExtractorCP.Properties.Settings.Default.DefaultExtractorDefinitionName;

            this.chkSaveFormLocationsOnExit.Checked = _userOptions.SaveFormLocationsOnExit;
            this.chkShowOutputLog.Checked = _userOptions.ShowOutputLog;
            this.chkShowInstalledDatabaseProvidersOnly.Checked = _userOptions.ShowInstalledDatabaseProvidersOnly;
            this.optPreviewAllRows.Checked = _userOptions.PreviewAllRows;
            this.optLimitPreviewRows.Checked = _userOptions.LimitPreviewRows;
            this.txtMaxNumberOfRows.Text = _userOptions.MaxNumberOfRows.ToString();
            this.cboDefaultDataSourceType.Text = _userOptions.DefaultDataSourceType;
            this.cboDefaultDataDestinationType.Text = _userOptions.DefaultDataDestinationType;
            LoadDefaultApplicationFont();
            this.txtBatchSizeForDataImportsAndExports.Text = _userOptions.BatchSizeForDataImportsAndExports.ToString();
            this.txtBatchSizeForRandomDataGeneration.Text = _userOptions.BatchSizeForRandomDataGeneration.ToString();
            this.txtDefaultExtractorDefinitionsSaveFolder.Text = _userOptions.DefaultExtractorDefinitionsSaveFolder;
            this.txtDefaultExtractorDefinitionName.Text = _userOptions.DefaultExtractorDefinitionName;
        }

        private void LoadDefaultUserConfigItems()
        {
            _userOptions.SaveFormLocationsOnExit = Convert.ToBoolean(pfDataExtractorCP.Properties.Settings.Default.Properties["SaveFormLocationsOnExit"].DefaultValue);
            _userOptions.ShowOutputLog = Convert.ToBoolean(pfDataExtractorCP.Properties.Settings.Default.Properties["ShowOutputLog"].DefaultValue);
            _userOptions.ShowInstalledDatabaseProvidersOnly = Convert.ToBoolean(pfDataExtractorCP.Properties.Settings.Default.Properties["ShowInstalledDatabaseProvidersOnly"].DefaultValue);
            _userOptions.PreviewAllRows = Convert.ToBoolean(pfDataExtractorCP.Properties.Settings.Default.Properties["PreviewAllRows"].DefaultValue);
            _userOptions.LimitPreviewRows = Convert.ToBoolean(pfDataExtractorCP.Properties.Settings.Default.Properties["LimitPreviewRows"].DefaultValue);
            _userOptions.MaxNumberOfRows = Convert.ToInt32(pfDataExtractorCP.Properties.Settings.Default.Properties["MaxNumberOfRows"].DefaultValue);
            _userOptions.DefaultDataSourceType = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultDataSourceType"].DefaultValue.ToString();
            _userOptions.DefaultDataDestinationType = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultDataDestinationType"].DefaultValue.ToString();
            _userOptions.DefaultApplicationtFont = (Font)pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultApplicationFont"].DefaultValue;
            _userOptions.BatchSizeForDataImportsAndExports = Convert.ToInt32(pfDataExtractorCP.Properties.Settings.Default.Properties["BatchSizeForDataImportsAndExports"].DefaultValue);
            _userOptions.BatchSizeForRandomDataGeneration = Convert.ToInt32(pfDataExtractorCP.Properties.Settings.Default.Properties["BatchSizeForRandomDataGeneration"].DefaultValue);
            _userOptions.DefaultExtractorDefinitionsSaveFolder = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultExtractorDefinitionsSaveFolder"].DefaultValue.ToString();
            _userOptions.DefaultExtractorDefinitionName = pfDataExtractorCP.Properties.Settings.Default.Properties["DefaultExtractorDefinitionName"].DefaultValue.ToString();

            if (_userOptions.DefaultExtractorDefinitionsSaveFolder.Trim().Length == 0)
                _userOptions.DefaultExtractorDefinitionsSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Definitions\";

            this.chkSaveFormLocationsOnExit.Checked = _userOptions.SaveFormLocationsOnExit;
            this.chkShowOutputLog.Checked = _userOptions.ShowOutputLog;
            this.chkShowInstalledDatabaseProvidersOnly.Checked = _userOptions.ShowInstalledDatabaseProvidersOnly;
            this.optPreviewAllRows.Checked = _userOptions.PreviewAllRows;
            this.optLimitPreviewRows.Checked = _userOptions.LimitPreviewRows;
            this.txtMaxNumberOfRows.Text = _userOptions.MaxNumberOfRows.ToString();
            this.cboDefaultDataSourceType.Text = _userOptions.DefaultDataSourceType;
            this.cboDefaultDataDestinationType.Text = _userOptions.DefaultDataDestinationType;
            if (_userOptions.DefaultApplicationtFont == null)
                pfDataExtractorCP.Properties.Settings.Default.DefaultApplicationFont = null;
            LoadDefaultApplicationFont();
            this.txtBatchSizeForDataImportsAndExports.Text = _userOptions.BatchSizeForDataImportsAndExports.ToString();
            this.txtBatchSizeForRandomDataGeneration.Text = _userOptions.BatchSizeForRandomDataGeneration.ToString();
            this.txtDefaultExtractorDefinitionsSaveFolder.Text = _userOptions.DefaultExtractorDefinitionsSaveFolder;
            this.txtDefaultExtractorDefinitionName.Text = _userOptions.DefaultExtractorDefinitionName;

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
            dialog.Font = pfDataExtractorCP.Properties.Settings.Default.DefaultApplicationFont;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pfDataExtractorCP.Properties.Settings.Default.DefaultApplicationFont = dialog.Font;
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
            }


            return res;
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

            _msg.Length = 0;

            if (this.chkSaveFormLocationsOnExit.Checked != _userOptions.SaveFormLocationsOnExit
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["SaveFormLocationsOnExit"] = this.chkSaveFormLocationsOnExit.Checked;
            }

            if (this.chkShowOutputLog.Checked != _userOptions.ShowOutputLog
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["ShowOutputLog"] = this.chkShowOutputLog.Checked;
            }

            if (this.chkShowInstalledDatabaseProvidersOnly.Checked != _userOptions.ShowInstalledDatabaseProvidersOnly
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["ShowInstalledDatabaseProvidersOnly"] = this.chkShowInstalledDatabaseProvidersOnly.Checked;
            }

            if (this.optPreviewAllRows.Checked != _userOptions.PreviewAllRows
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["PreviewAllRows"] = this.optPreviewAllRows.Checked;
            }

            if (this.optLimitPreviewRows.Checked != _userOptions.LimitPreviewRows
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["LimitPreviewRows"] = this.optLimitPreviewRows.Checked;
            }

            if (PFTextProcessor.ConvertStringToInt(this.txtMaxNumberOfRows.Text, 1000) != _userOptions.MaxNumberOfRows
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["MaxNumberOfRows"] = PFTextProcessor.ConvertStringToInt(this.txtMaxNumberOfRows.Text, 1000);
            }

            if (PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000) != _userOptions.BatchSizeForDataImportsAndExports
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["BatchSizeForDataImportsAndExports"] = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForDataImportsAndExports.Text, 1000);
            }

            if (PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 1000) != _userOptions.BatchSizeForRandomDataGeneration
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["BatchSizeForRandomDataGeneration"] = PFTextProcessor.ConvertStringToInt(this.txtBatchSizeForRandomDataGeneration.Text, 5000);
            }


            if (this.cboDefaultDataSourceType.Text != _userOptions.DefaultDataSourceType
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultDataSourceType"] = this.cboDefaultDataSourceType.Text;
            }

            if (this.cboDefaultDataDestinationType.Text != _userOptions.DefaultDataDestinationType
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultDataDestinationType"] = this.cboDefaultDataDestinationType.Text;
            }


            if (this.txtDefaultExtractorDefinitionsSaveFolder.Text != _userOptions.DefaultExtractorDefinitionsSaveFolder
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultExtractorDefinitionsSaveFolder"] = this.txtDefaultExtractorDefinitionsSaveFolder.Text;
            }

            if (this.txtDefaultExtractorDefinitionName.Text != _userOptions.DefaultExtractorDefinitionName
                || forceUpdate == true)
            {
                numUpdates++;
                pfDataExtractorCP.Properties.Settings.Default["DefaultExtractorDefinitionName"] = this.txtDefaultExtractorDefinitionName.Text;
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
                    SaveUserConfigItems();
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
            _printer.PageSubTitle = "User Options Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);
        }

        private void PreviewRowsCheckChanged()
        {
            if (this.optPreviewAllRows.Checked)
            {
                this.lblMaxNumberOfRows.Visible = false;
                this.txtMaxNumberOfRows.Visible = false;
            }
            else
            {
                this.lblMaxNumberOfRows.Visible = true;
                this.txtMaxNumberOfRows.Visible = true;
            }
        }


    }//end class
#pragma warning restore 1591

}//end namespace
