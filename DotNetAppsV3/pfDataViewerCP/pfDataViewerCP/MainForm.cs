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
using pfDataViewerCPProcessor;
using PFFileSystemObjects;
using PFTextFiles;
using PFPrinterObjects;
using PFDataAccessObjects;
using PFConnectionObjects;
using PFConnectionStrings;
using PFCollectionsObjects;
using PFSQLBuilder;
using PFSQLServerCE35Objects;
using PFProviderForms;
using KellermanSoftware.CompareNetObjects;
using PFAppDataObjects;
using PFRandomDataProcessor;
using PFTimers;

namespace pfDataViewerCP
{
    /// <summary>
    /// Form for defining and running queries against relational databases.
    /// </summary>
    public partial class MainForm : Form
    {
#pragma warning disable 1591
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _dataViewerHelpFilePath = string.Empty;
        private string _qbfHelpFilePath = string.Empty;
        //private StringBuilder _textToPrint = new StringBuilder();
        private DataViewerProcessor _appProcessor = new DataViewerProcessor();
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        string _initDataFilesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\InitDataFiles\", "");
        private string _defaultRandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        PFSQLServerCE35 _db = new PFSQLServerCE35();  //declared here to verify that the SQL CE 3.5 SP2 software is installed
                                                      //not used in this module but SQL CE 3.5 is extensively used throughout the applicatiion
                                                      //plan is to throw an error here if software is missing instead of waiting until later in the application's processing

        private string _defaultQueryDefinitionFileExtension = @".qrydef";
        private string _defaultQueryDefinitionsSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\QueryDefs\";
        private string _defaultQueryName = "MyQuery";
        private string _defaultDataSource = string.Empty;
        private string _defaultConnectionString = string.Empty;
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\DataExports\";
        private string _defaultSampleDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\SampleData\";
        private string _defaultSampleDefinitionFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\pfDataViewerCP\QueryDefs\Samples\";
        private string _defaultSampleRandomizerDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\";
        
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "Query Definition Files|*.qrydef|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = false;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        //fields for Mru processing
        MruStripMenu _msm;
        private bool _saveMruListToRegistry = true;
        private string _mRUListSaveFileSubFolder = @"PFApps\pfDataViewerCP\Mru\";
        private string _mRUListSaveRegistryKey = @"SOFTWARE\PFApps\pfDataViewerCP";
        private int _maxMruListEntries = 4;
        private bool _useSubMenuForMruList = true;

        //fields for checking for form changes on exit processing
        private pfQueryDef _saveQueryDef = null;
        private pfQueryDef _exitQueryDef = null;

        //query processing fields
        PFList<DataTableRandomizerColumnSpec> _colSpecs = null;

        public MainForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdRunQuery_Click(object sender, EventArgs e)
        {
            RunQuery();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void cmdRandomizeOutput_Click(object sender, EventArgs e)
        {
            RunRandomizer();
        }

        private void cmdGetQueryDefsFolder_Click(object sender, EventArgs e)
        {
            BrowseForQueryDefsFolder();
        }

        private void cboDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDefaultConnectionStringForPlatform();
            SetQbfVisibility();
            ResetSqlQuery();
        }

        private void cmdDefineConnectionString_Click(object sender, EventArgs e)
        {
            DefineConnectionString();
        }

        private void cmdDefineSqlQuery_Click(object sender, EventArgs e)
        {
            RunQueryBuilder();
        }

        //Menu item clicks
        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            FileNew();
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            FileOpen();
        }

        private void mnuFileClose_Click(object sender, EventArgs e)
        {
            FileClose();
        }

        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            FileSaveAs();
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

        private void mnuEditCut_Click(object sender, EventArgs e)
        {
            EditCut();
        }

        private void mnuEditCopy_Click(object sender, EventArgs e)
        {
            EditCopy();
        }

        private void mnuEditPaste_Click(object sender, EventArgs e)
        {
            EditPaste();
        }

        private void mnuSelectAll_Click(object sender, EventArgs e)
        {
            EditSelectAll();
        }

        private void mnuEditDelete_Click(object sender, EventArgs e)
        {
            EditDelete();
        }

        private void mnuFormatFont_Click(object sender, EventArgs e)
        {
            SpecifyFont();
        }

        private void mnuRandomizerManageSources_Click(object sender, EventArgs e)
        {
            ShowRandomDataManager();
        }

        private void mnuToolsOptionsUserSettings_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsUserSettings();
        }

        private void mnuToolsOptionsApplicationSettings_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsApplicationSettings();
        }

        private void mnuToolsShowHideOutputLog_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
        }

        private void mnuToolsFormSaveScreenLocations_Click(object sender, EventArgs e)
        {
            SaveScreenLocations();
        }

        private void mnuToolsFormRestoreScreenLocations_Click(object sender, EventArgs e)
        {
            RestoreDefaultScreenLocations();
        }

        private void mnuToolsSelectDatabaseProvidersToUse_Click(object sender, EventArgs e)
        {
            SelectDatabaseProvidersToUse();
        }

        private void mnuToolsConnectionStringManager_Click(object sender, EventArgs e)
        {
            ShowToolsConnectionStringManager();
        }

        private void mnuToolsResetEmptyMruList_Click(object sender, EventArgs e)
        {
            DeleteMruList();
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
            ShowDataViewerHelpTutorial();
        }

        private void mnuHelpContact_Click(object sender, EventArgs e)
        {
            ShowHelpContact();
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            ShowHelpAbout();
        }


        //context menu clicks
        private void mainMenuContextMenuRunTest_Click(object sender, EventArgs e)
        {
            Control sourceControl = null;
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    sourceControl = owner.SourceControl;
                    if (sourceControl != null)
                    {
                        if (sourceControl.Name == "txtSqlQuery"
                            || sourceControl.Name == "txtQueryName"
                            || sourceControl.Name == "txtConnectionString"
                            || sourceControl.Name == "cboDataSource")
                        {
                            this.RunQuery();
                        }
                    //    foreach (Control ctl in this.grpTestsToRun.Controls)
                    //    {
                    //        CheckBox chk = null;
                    //        if (ctl is CheckBox)
                    //        {
                    //            chk = (CheckBox)ctl;
                    //            chk.Checked = false;
                    //        }
                    //    }
                    //    if (sourceControl.Name == "chkShowDateTimeTest")
                    //    {
                    //        this.chkShowDateTimeTest.Checked = true;
                    //        this.RunQuery();
                    //    }
                    //    if (sourceControl.Name == "chkShowHelpAboutTest")
                    //    {
                    //        this.chkShowHelpAboutTest.Checked = true;
                    //        this.RunQuery();
                    //    }
                    }
                }
            }


        }

        private void contextMenuChangeFont_Click(object sender, EventArgs e)
        {
            Control sourceControl = null;
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    sourceControl = owner.SourceControl;
                    if (sourceControl.Name == "txtSqlQuery"
                        || sourceControl.Name == "txtQueryName"
                        || sourceControl.Name == "txtConnectionString"
                        || sourceControl.Name == "cboDataSource")
                    {
                        SpecifyFont();
                    }

                    //// Get the control that is displaying this context menu
                    //sourceControl = owner.SourceControl;
                    //if (sourceControl.Name == "chkShowDateTimeTest")
                    //{
                    //    AppMessages.DisplayInfoMessage("Help contents for " + sourceControl.Text);
                    //    this.ShowHelpContents();
                    //}
                    //if (sourceControl.Name == "chkShowHelpAboutTest")
                    //{
                    //    AppMessages.DisplayInfoMessage("Help index for " + sourceControl.Text);
                    //    this.ShowHelpIndex();
                    //}
                }
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control sourceControl = null;
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    sourceControl = owner.SourceControl;

                    this.ShowHelpContents();

                    //// Get the control that is displaying this context menu
                    //sourceControl = owner.SourceControl;
                    //if (sourceControl.Name == "chkShowDateTimeTest")
                    //{
                    //    AppMessages.DisplayInfoMessage("Help contents for " + sourceControl.Text);
                    //    this.ShowHelpContents();
                    //}
                    //if (sourceControl.Name == "chkShowHelpAboutTest")
                    //{
                    //    AppMessages.DisplayInfoMessage("Help index for " + sourceControl.Text);
                    //    this.ShowHelpIndex();
                    //}
                }
            }
        }



        //Form Routines
        private void CloseForm()
        {
            this.Close();
        }

        private void ShowToolsOptionsUserSettings()
        {
            UserOptionsForm usrOptionsForm = new UserOptionsForm();
            bool saveShowInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            bool saveShowApplicationLogWindow = Properties.Settings.Default.ShowApplicationLogWindow;
            string saveDefaultInputDatabaseType = Properties.Settings.Default.DefaultInputDatabaseType;
            string saveDefaultQueryDefinitionsFolder = Properties.Settings.Default.DefaultQueryDefinitionsFolder;
            string saveDefaultDataGridExportFolder = Properties.Settings.Default.DefaultDataGridExportFolder;
            Font saveDefaultApplicationFont = Properties.Settings.Default.DefaultApplicationFont;

            try
            {
                usrOptionsForm.ShowDialog();

                bool currentShowInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
                string currentDefaultInputDatabaseType = Properties.Settings.Default.DefaultInputDatabaseType;
                string currentDefaulQueryDefinitionsFolder = Properties.Settings.Default.DefaultQueryDefinitionsFolder;
                string currentDefaultDataGridExportFolder = Properties.Settings.Default.DefaultDataGridExportFolder;
                Font currentDefaultApplicationFont = Properties.Settings.Default.DefaultApplicationFont;
                if (saveDefaultApplicationFont != currentDefaultApplicationFont)
                {
                    SetTextFont();
                }
                if (saveShowInstalledDatabaseProvidersOnly != currentShowInstalledDatabaseProvidersOnly)
                {
                    LoadProviderList();
                }

                if (saveDefaultQueryDefinitionsFolder != currentDefaulQueryDefinitionsFolder)
                {
                    if (currentDefaulQueryDefinitionsFolder.Trim().Length == 0)
                    {
                        _defaultQueryDefinitionsSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\QueryDefs\";
                        pfDataViewerCP.Properties.Settings.Default["DefaultQueryDefinitionsFolder"] = _defaultQueryDefinitionsSaveFolder;
                    }
                    else
                    {
                        _defaultQueryDefinitionsSaveFolder = currentDefaulQueryDefinitionsFolder;
                    }
                }

                if (saveDefaultDataGridExportFolder != currentDefaultDataGridExportFolder)
                {
                    if (currentDefaultDataGridExportFolder.Trim().Length == 0)
                    {
                        _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\DataExports\";
                        pfDataViewerCP.Properties.Settings.Default["DefaultDataGridExportFolder"] = _defaultDataGridExportFolder;
                    }
                    else
                    {
                        _defaultDataGridExportFolder = currentDefaultDataGridExportFolder;
                    }
                }

                bool currentShowApplicationLogWindow = Properties.Settings.Default.ShowApplicationLogWindow;
                if (saveShowApplicationLogWindow != currentShowApplicationLogWindow)
                {
                    UpdateAppLogWindowVisibility();
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                usrOptionsForm.Close();
                usrOptionsForm = null;
                this.Focus();
                this.Refresh();
            }

        }

        private void ShowToolsOptionsApplicationSettings()
        {
            _appProcessor.ShowAppConfigManager();
            this.InitMruList();
            this.SetLoggingValues();
            this.SetHelpFileValues();
            this.Focus();
            this.Refresh();

        }


        private void ShowHelpContents()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataViewerHelpFilePath, HelpNavigator.TableOfContents);
        }

        private void ShowHelpIndex()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataViewerHelpFilePath, HelpNavigator.Index);
        }

        private void ShowHelpSearch()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataViewerHelpFilePath, HelpNavigator.Find, "");
        }

        private void ShowDataViewerHelpTutorial()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataViewerHelpFilePath, HelpNavigator.KeywordIndex, "Tutorial");
        }

        private void ShowHelpContact()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataViewerHelpFilePath, HelpNavigator.KeywordIndex, "Contact Information");
        }

        private void ShowHelpAbout()
        {
            HelpAboutForm appHelpAboutForm = new HelpAboutForm();
            appHelpAboutForm.ShowDialog();

        }

        private void ShowHelpMainFormOverview()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataViewerHelpFilePath, HelpNavigator.KeywordIndex, "Overview");
        }

        private bool HelpFileExists()
        {
            bool ret = false;

            if (File.Exists(_dataViewerHelpFilePath))
            {
                ret = true;
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_dataViewerHelpFilePath);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

            return ret;
        }

        private void MainFormToolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ProcessMainToolbarClick(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMruList();
            if (Properties.Settings.Default.SaveFormLocationsOnExit)
                SaveScreenLocations();

            if (QueryDefinitionHasChanges())
            {
                DialogResult res = PromptForFileSave(ReasonForFileSavePrompt.ApplicationExit);
                if (res == DialogResult.Yes)
                {
                    if (ExpectedFileSaveInfoFound())
                        FileSave();
                    else
                        e.Cancel = true;
                }
                if (res == DialogResult.Cancel)
                    e.Cancel = true;
            }


        }

        private bool QueryDefinitionHasChanges()
        {
            bool retval = false;
            CompareLogic oCompare = new CompareLogic();

            _exitQueryDef = CreateQueryDef();

            oCompare.Config.MaxDifferences = 10;

            ComparisonResult compResult = oCompare.Compare(_saveQueryDef, _exitQueryDef);

            if (compResult.AreEqual == false)
            {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Query definition has changes:\r\n");
                _msg.Append(compResult.DifferencesString);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());
            }

            retval = !compResult.AreEqual;

            return retval;
        }

        private DialogResult PromptForFileSave(ReasonForFileSavePrompt promptReason)
        {
            return PromptForFileSave(promptReason, MessageBoxButtons.YesNoCancel);
        }

        private DialogResult PromptForFileSave(ReasonForFileSavePrompt promptReason, MessageBoxButtons btns)
        {
            DialogResult res = System.Windows.Forms.DialogResult.None;

            _msg.Length = 0;
            _msg.Append("The are unsaved changes to the query definition. ");
            _msg.Append(Environment.NewLine);
            _msg.Append(Environment.NewLine);
            _msg.Append("Do you want to save these changes ");
            switch (promptReason)
            {
                case ReasonForFileSavePrompt.ApplicationExit:
                    _msg.Append("before exiting the application");
                    break;
                case ReasonForFileSavePrompt.FileOpen:
                    _msg.Append("before loading another file");
                    break;
                case ReasonForFileSavePrompt.FileClose:
                    _msg.Append("before closing current file");
                    break;
                case ReasonForFileSavePrompt.FileNew:
                    _msg.Append("erasing the data on the current form");
                    break;
                case ReasonForFileSavePrompt.DefaultOptionsChange:
                    _msg.Append("before reset for options change");
                    break;
                default:
                    _msg.Append("now");
                    break;
            }
            _msg.Append("?");
            _msg.Append(Environment.NewLine);

            res = AppMessages.DisplayMessage(_msg.ToString(), "File save before exit?", btns, MessageBoxIcon.Exclamation);

            return res;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                this.Text = AppInfo.AssemblyProduct;

                SetFormLocationAndSize();

                SetLoggingValues();

                SetHelpFileValues();

                InitMruList();

                this.chkShowRowNumber.Checked = false;
                this.chkEraseOutputBeforeEachTest.Checked = true;

                UpdateAppLogWindowVisibility();

                SetTextFont();

                InitDefaultValues();

                InitDataFileLocation();

                InitSampleDefinitions();

                LoadProviderList();

                SetFormFields();

                SetQbfVisibility();

                InitAppProcessor();

                GetInitSaveExitObjects();

                _printer = new FormPrinter(this);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        private void GetInitSaveExitObjects()
        {
            _saveQueryDef = CreateQueryDef();
            _exitQueryDef = CreateQueryDef();
        }

        internal void SetFormLocationAndSize()
        {
            if (Properties.Settings.Default.MainFormX != 0 || Properties.Settings.Default.MainFormY != 0)
            {
                this.Location = new Point(Properties.Settings.Default.MainFormX, Properties.Settings.Default.MainFormY);
            }

            if (Properties.Settings.Default.MessageLogX != 0
                || Properties.Settings.Default.MessageLogY != 0)
            {
                Program._messageLog.Form.Location = new Point(Properties.Settings.Default.MessageLogX, Properties.Settings.Default.MessageLogY);
            }

            if ((Properties.Settings.Default.MessageLogWidth != Properties.Settings.Default.MessageLogDefaultWidth)
                || (Properties.Settings.Default.MessageLogHeight != Properties.Settings.Default.MessageLogDefaultHeight))
            {
                if (Properties.Settings.Default.MessageLogWidth > 0 && Properties.Settings.Default.MessageLogHeight > 0)
                {
                    Program._messageLog.Form.Width = Properties.Settings.Default.MessageLogWidth;
                    Program._messageLog.Form.Height = Properties.Settings.Default.MessageLogHeight;
                }
            }

        }

        internal void InitMruList()
        {
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder", @"PFApps\pfFolderSize\Mru\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey", @"SOFTWARE\PFApps\pfFolderSize");
            _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries", (int)4);
            _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList", "true");

            if (_msm != null)
                _msm.RemoveAll();

            if (_saveMruListToRegistry)
            {
                if (_useSubMenuForMruList)
                {
                    _msm = new MruStripMenu(mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _mRUListSaveRegistryKey + "\\MRU", false, _maxMruListEntries);
                }
                else
                {
                    //use inline
                    _msm = new MruStripMenuInline(mnuFile, mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _mRUListSaveRegistryKey + "\\MRU", _maxMruListEntries);
                }
                _msm.LoadFromRegistry();
            }
            else
            {
                //load from and save to the file system
                if (_useSubMenuForMruList)
                {
                    _msm = new MruStripMenu(mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _maxMruListEntries);
                }
                else
                {
                    //use inline
                    _msm = new MruStripMenuInline(mnuFile, mnuFileRecent, new MruStripMenu.ClickedHandler(OnMruFile), _maxMruListEntries);
                }
                _msm.FileSystemMruPath = _mRUListSaveFileSubFolder;
                _msm.LoadFromFileSystem();
            }


        }//end InitMruList

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

            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
        }

        internal void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "pfDataViewerCP.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _dataViewerHelpFilePath = helpFilePath;

            string qbfHelpFileName = AppConfig.GetStringValueFromConfigFile("QBFHelpFileName", "QbfUsersGuide.chm");
            string qbfHelpFilePath = PFFile.FormatFilePath(executableFolder, qbfHelpFileName);
            _qbfHelpFilePath = qbfHelpFileName;

            _appProcessor.HelpFilePath = _dataViewerHelpFilePath;

        }

        private void InitAppProcessor()
        {
            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
            _appProcessor.MessageLogUI = Program._messageLog;
            _appProcessor.HelpFilePath = _dataViewerHelpFilePath;
            _appProcessor.DefaultDataGridExportFolder = _defaultDataGridExportFolder;
            _appProcessor.DefaultOutputDatabaseType = _defaultDataSource;
            _appProcessor.DefaultOutputDatabaseConnectionString = _defaultConnectionString;
        }

        private void UpdateAppLogWindowVisibility()
        {
            bool ShowApplicationLogWindow = Properties.Settings.Default.ShowApplicationLogWindow;

            if (ShowApplicationLogWindow)
            {
                Program._messageLog.ShowWindow();
                this.chkEraseOutputBeforeEachTest.Visible = true;
            }
            else
            {
                Program._messageLog.HideWindow();
                this.chkEraseOutputBeforeEachTest.Visible = false;
            }
        }

        private void SetTextFont()
        {
            Font fnt = Properties.Settings.Default.DefaultApplicationFont;

            this.txtSqlQuery.Font = fnt;
            this.txtConnectionString.Font = fnt;
            this.txtQueryName.Font = fnt;
            this.txtQueryDefinitionsSaveFolder.Font = fnt;
            this.cboDataSource.Font = fnt;

            this.Refresh();

        }

        private void InitDefaultValues()
        {
            string configValue = string.Empty;
            string propertyValue = string.Empty;

            propertyValue = Properties.Settings.Default.DefaultDataGridExportFolder;
            if (propertyValue.Trim().Length != 0)
            {
                _defaultDataGridExportFolder = propertyValue;
            }
            else
            {
                _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\DataExports\";
                pfDataViewerCP.Properties.Settings.Default["DefaultDataGridExportFolder"] = _defaultDataGridExportFolder;
            }

            if (Directory.Exists(_defaultDataGridExportFolder) == false)
                Directory.CreateDirectory(_defaultDataGridExportFolder);

            propertyValue = Properties.Settings.Default.DefaultQueryDefinitionsFolder;
            if (propertyValue.Trim().Length != 0)
            {
                _defaultQueryDefinitionsSaveFolder = propertyValue;
            }
            else
            {
                _defaultQueryDefinitionsSaveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\QueryDefs\";
                pfDataViewerCP.Properties.Settings.Default["DefaultQueryDefinitionsFolder"] = _defaultQueryDefinitionsSaveFolder;
            }

            if (Directory.Exists(_defaultQueryDefinitionsSaveFolder) == false)
                Directory.CreateDirectory(_defaultQueryDefinitionsSaveFolder);

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultQueryDefinitionName", string.Empty);
            if (configValue.Length > 0)
                _defaultQueryName = configValue;
            else
                _defaultQueryName = "MyQuery";

            propertyValue = Properties.Settings.Default.DefaultInputDatabaseType;
            if (propertyValue.Trim().Length != 0)
                _defaultDataSource = propertyValue;
            else
                _defaultDataSource = string.Empty;

            propertyValue = Properties.Settings.Default.DefaultInputDatabaseConnectionString;
            if (propertyValue.Trim().Length != 0)
                _defaultConnectionString = propertyValue;
            else
                GetDefaultConnectionStringForPlatform();

        }

        private void InitDataFileLocation()
        {
            string configValue = string.Empty;
            string propertyValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultRandomDataXmlFilesFolder", string.Empty);
            if (configValue.Trim().Length != 0)
                _defaultRandomDataXmlFilesFolder = configValue;

            if (Directory.Exists(_defaultRandomDataXmlFilesFolder) == false)
                Directory.CreateDirectory(_defaultRandomDataXmlFilesFolder);

            DirectoryInfo dirInfo = new DirectoryInfo(_defaultRandomDataXmlFilesFolder);
            if (dirInfo.GetFiles("RandomData.sdf").Length == 0)
            {
                LoadRandomDataDatabase();
            }

            if (dirInfo.GetFiles("*.dat").Length == 0)
            {
                LoadRandomDataXmlFiles();
            }

            dirInfo = new DirectoryInfo(_defaultSampleDataFolder);
            if (dirInfo.GetFiles("SampleOrderDataCE35.sdf").Length == 0)
            {
                LoadSampleDatabase("SampleOrderDataCE35.zip", "SampleOrderDataCE35.sdf");
            }
            if (dirInfo.GetFiles("SampleOrderDataCE40.sdf").Length == 0)
            {
                LoadSampleDatabase("SampleOrderDataCE40.zip", "SampleOrderDataCE40.sdf");
            }
            if (dirInfo.GetFiles("SampleOrderData.accdb").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.accdb.zip", "SampleOrderData.accdb");
            }
            if (dirInfo.GetFiles("SampleOrderData.mdb").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.mdb.zip", "SampleOrderData.mdb");
            }
            if (dirInfo.GetFiles("SampleOrderData.db").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.db.zip", "SampleOrderData.db");
            }
            if (dirInfo.GetFiles("SampleOrderData.udb").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.udb.zip", "SampleOrderData.udb");
            }


        }


        //private void LoadRandomDataXmlFilesFolder()
        //{
        //    _msg.Length = 0;
        //    _msg.Append("Loading random data database and files into application data folder: ");
        //    _msg.Append(_defaultRandomDataXmlFilesFolder);
        //    Program._messageLog.WriteLine(_msg.ToString());

        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    string sourceFolder = _initDataFilesFolder;
        //    string destinationFolder = _defaultRandomDataXmlFilesFolder;
        //    //copy random data database file
        //    string sdfSourceFilePath = Path.Combine(_initDataFilesFolder, "RandomData.sdf");
        //    string sdfDestinationFilePath = Path.Combine(destinationFolder, "RandomData.sdf");
        //    File.Copy(sdfSourceFilePath, sdfDestinationFilePath, false);

        //    //unzip and copy random word and names XML files
        //    string zipArchiveFile = Path.Combine(sourceFolder, "RandomDataFiles.zip");
        //    ZipArchive za = new ZipArchive(zipArchiveFile);
        //    za.ExtractAll(destinationFolder);
        //    za = null;

        //    sw.Stop();

        //    _msg.Length = 0;
        //    _msg.Append("Load of random data database and files has finished.");
        //    Program._messageLog.WriteLine(_msg.ToString());
        //    _msg.Length = 0;
        //    _msg.Append("Time to load database and files: ");
        //    _msg.Append(sw.FormattedElapsedTime);
        //    Program._messageLog.WriteLine(_msg.ToString());

        //}

        private void LoadRandomDataDatabase()
        {
            _msg.Length = 0;
            _msg.Append("Loading random data database into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            Program._messageLog.WriteLine(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;
            //copy random data database file
            string sdfSourceFilePath = Path.Combine(_initDataFilesFolder, "RandomData.sdf");
            string sdfDestinationFilePath = Path.Combine(destinationFolder, "RandomData.sdf");
            File.Copy(sdfSourceFilePath, sdfDestinationFilePath, false);

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of random data database has finished.");
            Program._messageLog.WriteLine(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load random data database: ");
            _msg.Append(sw.FormattedElapsedTime);
            Program._messageLog.WriteLine(_msg.ToString());

        }


        private void LoadRandomDataXmlFiles()
        {
            _msg.Length = 0;
            _msg.Append("Loading random data files into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            Program._messageLog.WriteLine(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;

            //unzip and copy random word and names XML files
            string zipArchiveFile = Path.Combine(sourceFolder, "RandomDataFiles.zip");
            ZipArchive za = new ZipArchive(zipArchiveFile);
            za.ExtractAll(destinationFolder);
            za = null;

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of random data files has finished.");
            Program._messageLog.WriteLine(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load random data files: ");
            _msg.Append(sw.FormattedElapsedTime);
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private void LoadSampleDatabase(string zipFile, string databaseFile)
        {
            _msg.Length = 0;
            if(File.Exists(Path.Combine(_defaultSampleDataFolder,zipFile)))
            {
                _msg.Append("Loading sample data databases into application sample data folder: ");
                _msg.Append(_defaultSampleDataFolder);
                _msg.Append(Environment.NewLine);
                _msg.Append("Database name: ");
                _msg.Append(databaseFile);
                Program._messageLog.WriteLine(_msg.ToString());
            }
            else
            {
                _msg.Append("Unable to load sample data databases into application sample data folder: ");
                _msg.Append(_defaultSampleDataFolder);
                _msg.Append(Environment.NewLine);
                _msg.Append("Expected zip file is missing: ");
                _msg.Append(zipFile);
                Program._messageLog.WriteLine(_msg.ToString());
                return;
            }


            Stopwatch sw = new Stopwatch();
            sw.Start();

            string sourceFolder = _defaultSampleDataFolder;
            string destinationFolder = _defaultSampleDataFolder;

            //unzip and copy random word and names XML files
            string zipArchiveFile = Path.Combine(sourceFolder, zipFile);
            ZipArchive za = new ZipArchive(zipArchiveFile);
            za.ExtractAll(destinationFolder);
            za = null;

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of sample data database has finished.");
            Program._messageLog.WriteLine(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load sample data database: ");
            _msg.Append(sw.FormattedElapsedTime);
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private void InitSampleDefinitions()
        {
            string filename = string.Empty;
            string originalMyDocumentsPath = @"C:\Users\Mike\Documents\";
            string currentMyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string originalAppDataPath = @"C:\Users\Mike\AppData\Roaming\";
            string currentAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (currentMyDocumentsPath.EndsWith(@"\") == false)
            {
                currentMyDocumentsPath = currentMyDocumentsPath + @"\";
            }
            if (currentAppDataPath.EndsWith(@"\") == false)
            {
                currentAppDataPath = currentAppDataPath + @"\";
            }

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE35_Example.qrydef");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleDefinitionFolder, "AccessMdb_Example.qrydef");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }

            //***************************************************************************************
            //Fix initial set of randomizer definitions, if necessary, that ship with the product
            //***************************************************************************************
            //*
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"CustomValues\", "LastNamesThatStartWithS.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_Mexico.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_MonterreyArea_MEX.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_Quebec.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_USA_Midwest.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonAndBusinessNames_USA_CAN_MEX.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_California.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_Canada.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_Mexico.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_NewEngland.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_Quebec.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_USA.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "Persons_NewYorkCity_CMSA.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "Persons_NewYorkCity_MetArea.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }

        }

        private void UpdateMyDocumentsPathInDefinitionsFile(string filename, string originalMyDocumentsPath, string currentMyDocumentsPath,
                                                                             string originalAppDataPath, string currentAppDataPath)
        {
            string fileContents = string.Empty;
            string modifiedFileContents = string.Empty;
            if (File.Exists(filename))
            {
                fileContents = File.ReadAllText(filename);
                modifiedFileContents = fileContents.Replace(originalMyDocumentsPath, currentMyDocumentsPath).Replace(originalAppDataPath, currentAppDataPath);
                File.WriteAllText(filename, modifiedFileContents);
            }
        }

        private void GetDefaultConnectionStringForPlatform()
        {
            this.txtConnectionString.Text = AppConfig.GetStringValueFromConfigFile("DefaultConnection_" + this.cboDataSource.Text, string.Empty);
        }

        private void SetQbfVisibility()
        {
            if (this.cboDataSource.Text.Trim().Length == 0)
            {
                this.cmdDefineSqlQuery.Visible = false;
                this.chkRandomizeOutput.Visible = false;
                this.cmdRandomizeOutput.Visible = false;
                return;
            }
            DatabasePlatform dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDataSource.Text);
            //change 7/15: show define sql query button for all providers
            this.cmdDefineSqlQuery.Visible = true;
            this.chkRandomizeOutput.Visible = true;
            this.cmdRandomizeOutput.Visible = true;
            //if (dbPlat == DatabasePlatform.SQLAnywhereUltraLite)
            //{
            //    this.cmdDefineSqlQuery.Visible = false;
            //    this.chkRandomizeOutput.Visible = true;
            //    this.cmdRandomizeOutput.Visible = true;
            //    //this.cmdDefineSqlQuery.Visible = true;  //testing
            //}
            //else
            //{
            //    this.cmdDefineSqlQuery.Visible = true;
            //    this.chkRandomizeOutput.Visible = true;
            //    this.cmdRandomizeOutput.Visible = true;
            //}
        }

        private void ResetSqlQuery()
        {
            this.txtSqlQuery.Text = string.Empty;
        }

        private void LoadProviderList()
        {
            bool showInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            PFConnectionManager connMgr = new PFConnectionManager();
            PFKeyValueList<string, PFProviderDefinition> provDefs = connMgr.GetListOfProviderDefinitions();

            if (provDefs.Count == 0)
            {
                connMgr.CreateProviderDefinitions();
            }
            else
            {
                connMgr.UpdateAllProvidersInstallationStatus();
            }
            provDefs = connMgr.GetListOfProviderDefinitions();

            this.cboDataSource.Items.Clear();
            foreach (stKeyValuePair<string, PFProviderDefinition> provDef in provDefs)
            {
                if (showInstalledDatabaseProvidersOnly)
                {
                    if (provDef.Value.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                    {
                        if (provDef.Value.AvailableForSelection)
                        {
                            this.cboDataSource.Items.Add(provDef.Key);
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
                        this.cboDataSource.Items.Add(provDef.Key);
                    }
                    else
                    {
                        ;
                    }
                }
            }

        }//end method



        private void SetFormFields()
        {
            this.txtQueryDefinitionsSaveFolder.Text = _defaultQueryDefinitionsSaveFolder;
            this.txtQueryName.Text = _defaultQueryName;
            this.cboDataSource.Text = _defaultDataSource;
            this.txtConnectionString.Text = _defaultConnectionString;
            _colSpecs = null;
            _colSpecs = new PFList<DataTableRandomizerColumnSpec>();
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

        private void ProcessMainToolbarClick(object sender, ToolStripItemClickedEventArgs e)
        {
            string itemClicked;
            if (e.ClickedItem.Text != null)
                if (e.ClickedItem.Text.Length > 0)
                    itemClicked = e.ClickedItem.Text;
                else
                    itemClicked = "<Item text is blank>";
            else
                itemClicked = "<Item text not specified>";

            switch (itemClicked)
            {
                case "New":
                    FileNew();
                    break;
                case "Open":
                    FileOpen();
                    break;
                case "Close":
                    FileClose();
                    break;
                case "Save":
                    FileSave();
                    break;
                case "Print":
                    FilePrint(false, false);
                    break;
                case "PrintPreview":
                    FilePrint(true, false);
                    break;
                case "Cut":
                    EditCut();
                    break;
                case "Copy":
                    EditCopy();
                    break;
                case "Paste":
                    EditPaste();
                    break;
                case "Help":
                    ShowHelpMainFormOverview();
                    break;
                default:
                    //unexpected ClickedItem tag
                    _msg.Length = 0;
                    _msg.Append("ClickedItem text ");
                    _msg.Append(itemClicked);
                    _msg.Append(" not recognized by the application.");
                    AppGlobals.AppMessages.DisplayWarningMessage(_msg.ToString(), true);
                    break;

            }
        }

        private void FileNew()
        {
            if (QueryDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileNew);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            InitQueryDefForm();
            _saveQueryDef = CreateQueryDef();

        }

        private void FileOpen()
        {
            if (QueryDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileOpen);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            _saveSelectionsFolder = this.txtQueryDefinitionsSaveFolder.Text;
            DialogResult res = ShowOpenFileDialog();
            if (res != System.Windows.Forms.DialogResult.OK)
            {
                //cancel save request
                return;
            }

            pfQueryDef querydef = pfQueryDef.LoadFromXmlFile(_saveSelectionsFile);
            FillQueryDefForm(querydef);
            _saveQueryDef = CreateQueryDef();

            this.txtQueryDefinitionsSaveFolder.Text = Path.GetDirectoryName(_saveSelectionsFile);
            if (this.txtQueryDefinitionsSaveFolder.Text.EndsWith(@"\") == false)
            {
                this.txtQueryDefinitionsSaveFolder.Text += @"\";
            }

            UpdateMruList(_saveSelectionsFile);

        }

        private void FileClose()
        {
            if (QueryDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileClose);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            InitQueryDefForm();
            _saveQueryDef = CreateQueryDef();

        }

        private void FileSave()
        {
            if (ExpectedFileSaveInfoFound() == false)
                return;

            string queryDefFilename = Path.Combine(this.txtQueryDefinitionsSaveFolder.Text, this.txtQueryName.Text + _defaultQueryDefinitionFileExtension);
            if(File.Exists(queryDefFilename))
            {
                pfQueryDef qrydef = CreateQueryDef();
                PFList<DataTableRandomizerColumnSpec> colSpecs = qrydef.RandomizerColSpecs;
                _appProcessor.SyncColSpecsWithDataSchema(qrydef, ref colSpecs);
                qrydef.RandomizerColSpecs = colSpecs;
                qrydef.SaveToXmlFile(queryDefFilename);
                _saveQueryDef = CreateQueryDef();
                _msg.Length = 0;
                _msg.Append("Query definition saved to ");
                _msg.Append(queryDefFilename);
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayInfoMessage(_msg.ToString());
                UpdateMruList(queryDefFilename);
            }
            else
            {
                FileSaveAs();
            }
        }

        private void FileSaveAs()
        {
            if (ExpectedFileSaveInfoFound() == false)
                return;

            _saveSelectionsFolder = this.txtQueryDefinitionsSaveFolder.Text;
            _saveSelectionsFile = this.txtQueryName.Text + _defaultQueryDefinitionFileExtension;
            DialogResult res = ShowSaveFileDialog();
            if (res != DialogResult.OK)
            {
                //cancel save request
                return;
            }

            this.txtQueryDefinitionsSaveFolder.Text = Path.GetDirectoryName(_saveSelectionsFile);
            if (this.txtQueryDefinitionsSaveFolder.Text.EndsWith(@"\") == false)
            {
                this.txtQueryDefinitionsSaveFolder.Text += @"\";
            }
            this.txtQueryName.Text = Path.GetFileNameWithoutExtension(_saveSelectionsFile);
            pfQueryDef qrydef = CreateQueryDef();
            PFList<DataTableRandomizerColumnSpec> colSpecs = qrydef.RandomizerColSpecs;
            _appProcessor.SyncColSpecsWithDataSchema(qrydef, ref colSpecs);
            qrydef.RandomizerColSpecs = colSpecs;
            qrydef.SaveToXmlFile(_saveSelectionsFile);
            _saveQueryDef = CreateQueryDef();
            _msg.Length = 0;
            _msg.Append("Query definition saved to ");
            _msg.Append(_saveSelectionsFile);
            Program._messageLog.WriteLine(_msg.ToString());

            UpdateMruList(_saveSelectionsFile);
        }

        private bool ExpectedFileSaveInfoFound()
        {
            bool infoFound = true;

            _msg.Length = 0;
            if (Directory.Exists(this.txtQueryDefinitionsSaveFolder.Text) == false)
            {
                _msg.Append("Directory you specified does not exist: ");
                _msg.Append(this.txtQueryDefinitionsSaveFolder.Text);
                _msg.Append(Environment.NewLine);
            }
            if (this.txtQueryName.Text.Trim().Length == 0)
            {
                _msg.Append("You must specify a name for the query.");
                _msg.Append(Environment.NewLine);
            }

            if (this.cboDataSource.Text.Trim().Length == 0)
            {
                _msg.Append("You must specify a data source for the query.");
                _msg.Append(Environment.NewLine);
            }

            if (this.txtConnectionString.Text.Trim().Length == 0)
            {
                _msg.Append("You must specify a connection string for the query.");
                _msg.Append(Environment.NewLine);
            }

            if (this.txtConnectionString.Text.Trim().Length == 0)
            {
                _msg.Append("You must specify the SQL query.");
                _msg.Append(Environment.NewLine);
            }

            if (_msg.ToString().Length > 0)
            {
                infoFound = false;
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }

            return infoFound;
        }

        private DialogResult ShowOpenFileDialog()
        {
            DialogResult res = DialogResult.None;
            mainMenuOpenFileDialog.InitialDirectory = _saveSelectionsFolder;
            mainMenuOpenFileDialog.FileName = string.Empty;
            mainMenuOpenFileDialog.Filter = _saveFilter;
            mainMenuOpenFileDialog.FilterIndex = _saveFilterIndex;
            mainMenuOpenFileDialog.Multiselect = _saveMultiSelect;
            _saveSelectionsFile = string.Empty;
            _saveSelectedFiles = null;
            res = mainMenuOpenFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(mainMenuOpenFileDialog.FileName);
                _saveSelectionsFile = mainMenuOpenFileDialog.FileName;
                _saveFilterIndex = mainMenuOpenFileDialog.FilterIndex;
                if (mainMenuOpenFileDialog.Multiselect)
                {
                    _saveSelectedFiles = mainMenuOpenFileDialog.FileNames;
                    if (_saveSelectedFiles.Length > 0)
                    {
                        for (int i = 0; i < _saveSelectedFiles.Length; i++)
                        {
                            UpdateMruList(_saveSelectedFiles[i]);
                        }
                    }
                }
                else
                {
                    UpdateMruList(mainMenuOpenFileDialog.FileName);
                }
            }
            return res;
        }

        private DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;
            mainMenuSaveFileDialog.InitialDirectory = _saveSelectionsFolder;
            mainMenuSaveFileDialog.FileName = _saveSelectionsFile;
            mainMenuSaveFileDialog.Filter = _saveFilter;
            mainMenuSaveFileDialog.FilterIndex = _saveFilterIndex;
            mainMenuSaveFileDialog.CreatePrompt = _showCreatePrompt;
            mainMenuSaveFileDialog.OverwritePrompt = _showOverwritePrompt;
            res = mainMenuSaveFileDialog.ShowDialog();
            _saveSelectionsFile = string.Empty;
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(mainMenuSaveFileDialog.FileName);
                _saveSelectionsFile = mainMenuSaveFileDialog.FileName;
                _saveFilterIndex = mainMenuSaveFileDialog.FilterIndex;
            }
            return res;
        }

        private void UpdateMruList(string filename)
        {
            try
            {
                _msm.AddFile(filename);
                SaveMruList();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }

        }

        private void SaveMruList()
        {
            if (_msm != null)
            {
                if (_saveMruListToRegistry)
                {
                    _msm.SaveToRegistry();
                }
                else
                {
                    _msm.SaveToFileSystem();
                }
            }
            else
            {
                //do not save
                ;
            }
        }

        private void DeleteMruList()
        {
            _msm.RemoveAll();
        }

        private DialogResult ShowFolderBrowserDialog()
        {
            DialogResult res = DialogResult.None;

            string folderPath = string.Empty;

            if (_saveSelectionsFolder.Length > 0)
                folderPath = _saveSelectionsFolder;
            else
                folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            mainMenuFolderBrowserDialog.ShowNewFolderButton = _showNewFolderButton;
            //mainMenuFolderBrowserDialog.RootFolder = 
            mainMenuFolderBrowserDialog.SelectedPath = folderPath;
            res = mainMenuFolderBrowserDialog.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                folderPath = mainMenuFolderBrowserDialog.SelectedPath;
                _str.Length = 0;
                _str.Append(folderPath);
                if (folderPath.EndsWith(@"\") == false)
                    _str.Append(@"\");
                _saveSelectionsFolder = folderPath;
            }


            return res;
        }

        private void ShowPageSettings()
        {
            _printer.ShowPageSettings();
        }

        private void FilePrint(bool preview, bool showPrintDialog)
        {
            _printer.PageTitle = AppGlobals.AppInfo.AssemblyDescription;
            _printer.PageSubTitle = "Main Form";
            _printer.PageFooter  = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }//end method


        private void OnMruFile(int number, String filename)
        {
            if (QueryDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileOpen);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            if (File.Exists(filename))
            {
                //process it
                _saveSelectionsFile = filename;

                pfQueryDef querydef = pfQueryDef.LoadFromXmlFile(filename);
                FillQueryDefForm(querydef);
                _saveQueryDef = CreateQueryDef();

                this.txtQueryDefinitionsSaveFolder.Text = Path.GetDirectoryName(_saveSelectionsFile);
                if (this.txtQueryDefinitionsSaveFolder.Text.EndsWith(@"\") == false)
                {
                    this.txtQueryDefinitionsSaveFolder.Text += @"\";
                }
                UpdateMruList(_saveSelectionsFile);
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("File does not exist. It will be removed from the Mru List");
                _msm.RemoveFile(number);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }



        }//end method

        private void EditCut()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Cut);
        }

        private void EditCopy()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Copy);
        }

        private void EditPaste()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Paste);
        }

        private void EditSelectAll()
        {
            _textEditor.TextBoxEditor(this, EditOperation.SelectAll);
        }

        private void EditDelete()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Delete);
        }

        //application routines

        private void RunQuery()
        {
            pfQueryDef queryDef = null;

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                queryDef = CreateQueryDef();
                //_saveQueryDef = queryDef;       //workaround to keep _save and _exit query def objects in sync  
                                                  //workaround removed 1/2015: it was breaking exit, new and close processing when unsaved changes had been made

                _appProcessor.BatchSizeForRandomDataGeneration = Properties.Settings.Default.BatchSizeForRandomDataGeneration;

                _appProcessor.RunQuery(queryDef, this.chkRandomizeOutput.Checked, this.chkShowRowNumber.Checked);



            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }

        }

        private pfQueryDef CreateQueryDef()
        {
            pfQueryDef queryDef = new pfQueryDef();

            queryDef.QueryName = this.txtQueryName.Text;
            if (this.cboDataSource.Text.Trim().Length > 0)
                queryDef.DatabaseType = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDataSource.Text);
            else
                queryDef.DatabaseType = DatabasePlatform.Unknown;
            queryDef.ConnectionString = this.txtConnectionString.Text;
            queryDef.Query = this.txtSqlQuery.Text;
            queryDef.ShowRowNumber = this.chkShowRowNumber.Checked;
            queryDef.RandomizeOutput = this.chkRandomizeOutput.Checked;
            queryDef.RandomizerColSpecs = _colSpecs;
            return queryDef;
        }

        private void RunRandomizer()
        {
            pfQueryDef queryDef = null;

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                queryDef = CreateQueryDef();

                _colSpecs = _appProcessor.ShowRandomizerDefinitionForm(queryDef);
                queryDef.RandomizerColSpecs = _colSpecs;

                ReviewColSpecs();


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }

        }

        private void ReviewColSpecs()
        {
            bool randomizerSpecsDefined = false;

            if (_colSpecs == null)
            {
                this.chkRandomizeOutput.Checked = false;
                return;
            }

            foreach (DataTableRandomizerColumnSpec colSpec in _colSpecs)
            {
                if (String.IsNullOrEmpty(colSpec.RandomDataFieldName.Trim())==false
                    && String.IsNullOrEmpty(colSpec.RandomDataSource.Trim()) == false)
                {
                    randomizerSpecsDefined = true;
                    break;
                }
            }

            if (randomizerSpecsDefined)
                this.chkRandomizeOutput.Checked = true;
            else
                this.chkRandomizeOutput.Checked = false;

        }

        private void GetStaticKeys()
        {

            try
            {
                _msg.Length = 0;
                _msg.Append("GetStaticKeys started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append("StaticKeySection values:\r\n");
                _msg.Append("MainFormCaption = ");
                _msg.Append(StaticKeysSection.Settings.MainFormCaption);
                _msg.Append("\r\n");
                _msg.Append("MinAppThreads = ");
                _msg.Append(StaticKeysSection.Settings.MinAppThreads.ToString());
                _msg.Append("\r\n");
                _msg.Append("MaxAppThreads = ");
                _msg.Append(StaticKeysSection.Settings.MaxAppThreads.ToString());
                _msg.Append("\r\n");
                _msg.Append("RequireLogon = ");
                _msg.Append(StaticKeysSection.Settings.RequireLogon.ToString());
                _msg.Append("\r\n");
                _msg.Append("ValidBooleanValues = ");
                _msg.Append(StaticKeysSection.Settings.ValidBooleanValues);
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... GetStaticKeys finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void ShowHelpAboutTest(string testDescription)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append(testDescription);
                _msg.Append(": ");
                Program._messageLog.WriteLine(_msg.ToString());
                HelpAboutForm frm = new HelpAboutForm();
                frm.CHelpAbout_Load(frm, new EventArgs());
                frm.Hide();
                _msg.Length = 0;
                _msg.Append("\r\n----------\r\n");
                _msg.Append(frm.txtApplicationName.Text);
                _msg.Append("\r\n----------\r\n");
                _msg.Append(frm.txtApplicationInfo.Text);
                _msg.Append("\r\n----------\r\n");
                _msg.Append(frm.txtRegistrationInfo.Text);
                _msg.Append("\r\n----------\r\n");
                _msg.Append(frm.txtSystemInfo.Text);
                _msg.Append("\r\n----------\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
                frm.Close();
                frm = null;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
        }

        private void SaveScreenLocations()
        {
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            if (Program._messageLog.Form.WindowState == FormWindowState.Minimized || Program._messageLog.Form.WindowState == FormWindowState.Maximized)
            {
                Program._messageLog.Form.WindowState = FormWindowState.Normal;
            }
            Properties.Settings.Default["MessageLogX"] = Program._messageLog.Form.Location.X;
            Properties.Settings.Default["MessageLogY"] = Program._messageLog.Form.Location.Y;
            Properties.Settings.Default["MessageLogWidth"] = Program._messageLog.Form.Width;
            Properties.Settings.Default["MessageLogHeight"] = Program._messageLog.Form.Height;

            Properties.Settings.Default["MainFormX"] = this.Location.X;
            Properties.Settings.Default["MainFormY"] = this.Location.Y;
            Properties.Settings.Default.Save();
        }

        private void RestoreDefaultScreenLocations()
        {
            Program._messageLog.Form.Location = new Point(0, 0);
            Program._messageLog.Form.Width = Properties.Settings.Default.MessageLogDefaultWidth;
            Program._messageLog.Form.Height = Properties.Settings.Default.MessageLogDefaultHeight;
            Program._messageLog.Form.Refresh();

            Properties.Settings.Default["MessageLogX"] = Program._messageLog.Form.Location.X;
            Properties.Settings.Default["MessageLogY"] = Program._messageLog.Form.Location.Y;
            Properties.Settings.Default["MessageLogWidth"] = Program._messageLog.Form.Width;
            Properties.Settings.Default["MessageLogHeight"] = Program._messageLog.Form.Height;

            this.CenterToScreen();
            Properties.Settings.Default["MainFormX"] = 0;
            Properties.Settings.Default["MainFormY"] = 0;
            Properties.Settings.Default.Save();
        }

        private void SpecifyFont()
        {
            FontDialog dialog = this.FontDialog1;
            dialog.Font = this.txtSqlQuery.Font;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtSqlQuery.Font = dialog.Font;
                this.txtConnectionString.Font = dialog.Font;
                this.txtQueryName.Font = dialog.Font;
                this.txtQueryDefinitionsSaveFolder.Font = dialog.Font;
                this.cboDataSource.Font = dialog.Font;
            }
            dialog = null;
            this.Focus();
        }

        private void BrowseForQueryDefsFolder()
        {
            _saveSelectionsFolder = _defaultQueryDefinitionsSaveFolder;
            DialogResult res = ShowFolderBrowserDialog();
            if (res == DialogResult.OK)
            {
                _defaultQueryDefinitionsSaveFolder = _saveSelectionsFolder;
                this.txtQueryDefinitionsSaveFolder.Text = _saveSelectionsFolder;
            }
        }

        private void    DefineConnectionString()
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ConnectionStringPrompt cp = null;


            try
            {
                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDataSource.Text);
                connMgr = new PFConnectionManager();
                cp = new ConnectionStringPrompt(dbPlat, connMgr);
                if (this.txtConnectionString.Text.Trim().Length > 0)
                {
                    cp.ConnectionString = this.txtConnectionString.Text;
                }
                else
                {
                    if (dbPlat == DatabasePlatform.SQLServerCE35)
                    {
                        cp.ConnectionString = @"data source='" + _defaultSampleDataFolder + @"SampleOrderDataCE35.sdf';";
                    }
                    else if (dbPlat == DatabasePlatform.SQLServerCE40)
                    {
                        cp.ConnectionString = @"data source='" + _defaultSampleDataFolder + @"SampleOrderDataCE40.sdf';";
                    }
                    else if (dbPlat == DatabasePlatform.MSAccess)
                    {
                        cp.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _defaultSampleDataFolder + @"SampleOrderData.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=5;";
                    }
                    else if (dbPlat == DatabasePlatform.SQLAnywhere)
                    {
                        cp.ConnectionString = @"UserID=DBA;Password=sql;DatabaseName=SampleOrderData;DatabaseFile=" + _defaultSampleDataFolder + @"SampleOrderData.db;";
                    }
                    else if (dbPlat == DatabasePlatform.SQLAnywhereUltraLite)
                    {
                        cp.ConnectionString = @"nt_file=" + _defaultSampleDataFolder + @"SampleOrderData.udb;uid=DBA;pwd=sql";
                    }
                    else
                        cp.ConnectionString = string.Empty;
                }

                System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
                if (res == DialogResult.OK)
                {
                    this.txtConnectionString.Text = cp.ConnectionString;
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }

        }

        public void RunQueryBuilder()
        {
            string modifiedQueryText = this.txtSqlQuery.Text;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ISQLBuilder qbf = null;



            try
            {
                if (this.txtConnectionString.Text.Trim().Length == 0
                    || this.cboDataSource.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a Data Source and a connection string in order to use the Query Builder.");
                    throw new System.Exception(_msg.ToString());
                }

                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDataSource.Text);

                // *** TEMPORARY CODE FOR QUERY BUILDER ***
                _msg.Length = 0;
                _msg.Append("*** Query builder functionality has been disabled. ***");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("You must have installed Active Query Builder for Winforms for this functionality to work.\r\nProduct can be obtained from the following link:");
                _msg.Append(Environment.NewLine);
                _msg.Append(@"http://www.activequerybuilder.com/product_net.html");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("If you have already installed Active Query Builder for Winforms, you should remove the temporary code found in the RunQueryBuilder() method.");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayAlertMessage(_msg.ToString());
                return;
                // *** END TEMPORARY CODE FOR QUERY BUILDER ***

                // ******************************************************************************
                // Code to activate Query Builder follows
                // ******************************************************************************
                //uncomment next four lines to activate query builder
                //uncomment using PFSQLBuilderObjects; above
                //add reference to PFSQLBuilderObjects.dll in CPLibs\Binaries\ProFast\ClassLibraries\Release (this is default path; substitute path if you changed binaries path
                /*
                qbf = SQLBuilder.CreateQueryBuilderObject(dbPlat, this.txtConnectionString.Text);

                modifiedQueryText = qbf.ShowQueryBuilder(this.txtSqlQuery.Text);
                 * */
                // ******************************************************************************
                // End code to activate Query Builder
                // ******************************************************************************

                this.txtSqlQuery.Text = modifiedQueryText;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if(qbf != null)
                    qbf = null;
                this.Focus();
            }

        }

        private void SelectDatabaseProvidersToUse()
        {
            ProviderSelectorForm provForm = new ProviderSelectorForm();


            try
            {
                provForm.ShowInstalledProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;

                DialogResult res = provForm.ShowDialog();
                //DialogResult will always be OK
                if (provForm.ShowInstalledProvidersOnly != Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly
                    || provForm.ProviderListHasBeenChanged)
                {
                    Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly = provForm.ShowInstalledProvidersOnly;
                    LoadProviderList();
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (provForm.Visible)
                    provForm.Close();
                provForm = null;
            }


        }


        private void FillQueryDefForm(pfQueryDef querydef)
        {
            this.txtQueryName.Text = querydef.QueryName;
            this.cboDataSource.Text = querydef.DatabaseType.ToString();
            this.txtConnectionString.Text = querydef.ConnectionString;
            this.txtSqlQuery.Text = querydef.Query;
            this.chkShowRowNumber.Checked = querydef.ShowRowNumber;
            this.chkRandomizeOutput.Checked = querydef.RandomizeOutput;
            _colSpecs = querydef.RandomizerColSpecs;
            SetQbfVisibility();
        }

        private void InitQueryDefForm()
        {
            this.txtQueryDefinitionsSaveFolder.Text = _defaultQueryDefinitionsSaveFolder;
            this.txtQueryName.Text = string.Empty;
            this.cboDataSource.Text = string.Empty;
            this.txtConnectionString.Text = string.Empty;
            this.txtSqlQuery.Text = string.Empty;
            this.chkRandomizeOutput.Checked = false;
            this.chkShowRowNumber.Checked = false;
            _colSpecs = null;
            _colSpecs = new PFList<DataTableRandomizerColumnSpec>();
            SetQbfVisibility();
        }

        private void ShowToolsConnectionStringManager()
        {
            _appProcessor.ShowConnectionStringManagerForm();
            this.Focus();
            this.Refresh();

        }

        private void ShowRandomDataManager()
        {
            _appProcessor.ShowRandomDataManagerForm();
            this.Focus();
            this.Refresh();

        }


#pragma warning restore 1591

    }//end class
}//end namespace
