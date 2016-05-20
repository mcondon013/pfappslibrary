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
using PFRandomDataListProcessor;
using PFConnectionObjects;
using PFConnectionStrings;
using PFCollectionsObjects;
using PFDataAccessObjects;

namespace PFRandomDataForms
{
#pragma warning disable 1591
    /// <summary>
    /// Form for definition and preview of custom generated random values.
    /// </summary>
    public partial class RandomCustomValuesForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;
        DataListProcessor _appProcessor = new DataListProcessor();
        PFList<PFTableDef> _tableList = null;

        private string _randomCustomValuesDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\CustomValues\";
        private string _randomCustomValuesOriginalDataRequestsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\OriginalDefinitions\CustomValues\";

        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\CustomValues\";
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\CustomValues\";
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private string _defaultInputDatabaseType = string.Empty;
        private string _defaultInputDatabaseConnectionString = string.Empty;
        private bool _showInstalledDatabaseProvidersOnly = true;
        private int _numberOfRandomValueSamples = 100;

        private string _desktopDbFolder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"PFApps\DesktopDatabases\");

        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "Text Files|*.txt|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = true;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        //fields for Mru processing
        MruStripMenu _msm;
        private bool _saveMruListToRegistry = true;
        private string _mRUListSaveFileSubFolder = @"PFApps\PFRandomDataForms\Mru\";
        private string _mRUListSaveRegistryKey = @"SOFTWARE\PFApps\PFRandomDataForms";
        private int _maxMruListEntries = 4;
        private bool _useSubMenuForMruList = true;

        //fields for properties
        MessageLog _messageLog = null;

        //fields for checking for form changes on exit processing
        private RandomCustomValuesDataRequest _saveRequestDef = null;
        private RandomCustomValuesDataRequest _exitRequestDef = null;

        private int _numSavesForThisSession = 0;

        //constructors

        public RandomCustomValuesForm()
        {
            InitializeComponent();
        }


        //properties

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


        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdProcessForm_Click(object sender, EventArgs e)
        {
            if (ProcessForm() == false)
            {
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                HideForm();
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void cmdPreview_Click(object sender, EventArgs e)
        {
            PreviewRandomData();
        }

        private void cmdGenerateRandomList_Click(object sender, EventArgs e)
        {
            GenerateCustomRandomDataFile();
        }

        private void cmdDefineConnectionString_Click(object sender, EventArgs e)
        {
            DefineConnectionString();
        }

        private void cmdCustomQueryConnect_Click(object sender, EventArgs e)
        {
            ConnectToDatabaseForCustomQueryTables();
        }

        private void cboDatabaseTypeForCustomQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCustomQueryConnectionString();
        }

        private void cboTableNameForCustomQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCustomQueryFields(this.cboTableNameForCustomQuery.Text);
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

        private void mnuFileDelete_Click(object sender, EventArgs e)
        {
            FileDelete();
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void mnuToolsResetEmptyMruList_Click(object sender, EventArgs e)
        {
            DeleteMruList();
        }

        private void mnuToolsResetReloadOriginalRequestDefinitions_Click(object sender, EventArgs e)
        {
            ReloadOriginalRequestDefinitions();
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
                        //foreach (Control ctl in this.grpTestsToRun.Controls)
                        //{
                        //    CheckBox chk = null;
                        //    if (ctl is CheckBox)
                        //    {
                        //        chk = (CheckBox)ctl;
                        //        chk.Checked = false;
                        //    }
                        //}
                        //if (sourceControl.Name == "chkShowDateTimeTest")
                        //{
                        //    this.chkShowDateTimeTest.Checked = true;
                        //    this.ProcessForm();
                        //}
                        //if (sourceControl.Name == "chkShowHelpAboutTest")
                        //{
                        //    this.chkShowHelpAboutTest.Checked = true;
                        //    this.ProcessForm();
                        //}
                    }
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
                    // Get the control that is displaying this context menu
                    sourceControl = owner.SourceControl;
                    if (sourceControl.Name == "chkShowDateTimeTest")
                    {
                        AppMessages.DisplayInfoMessage("Help contents for " + sourceControl.Text);
                        this.ShowHelpContents();
                    }
                    if (sourceControl.Name == "chkShowHelpAboutTest")
                    {
                        AppMessages.DisplayInfoMessage("Help index for " + sourceControl.Text);
                        this.ShowHelpIndex();
                    }
                }
            }
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

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Custom Random Values");
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

        private void MainFormToolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ProcessMainToolbarClick(sender, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMruList();

            if (this.DialogResult != DialogResult.OK)
            {
                if (this.txtDataMaskName.Text.Length > 0)
                {
                    if (RequestDefinitionHasChanges())
                    {
                        DialogResult res = PromptForFileSave(ReasonForFileSavePrompt.ApplicationExit);
                        if (res == DialogResult.Yes)
                        {
                            if (FileSave() == false)
                            {
                                e.Cancel = true;
                            }
                        }
                        if (res == DialogResult.Cancel)
                            e.Cancel = true;
                    }
                }
            }

        }

        private bool RequestDefinitionHasChanges()
        {
            bool retval = false;
            CompareLogic oCompare = new CompareLogic();

            _exitRequestDef = CreateRequestDef(false);

            oCompare.Config.MaxDifferences = 10;

            ComparisonResult compResult = oCompare.Compare(_saveRequestDef, _exitRequestDef);

            if (compResult.AreEqual == false)
            {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Query definition has changes:\r\n");
                _msg.Append(compResult.DifferencesString);
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());
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
            _msg.Append("The are unsaved changes to the request definition. ");
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
                SetLoggingValues();

                SetHelpFileValues();

                InitMruList();

                GetFileLocations();

                InitDesktopDatabasesFolder();

                GetDefaultDatabaseSettings();

                SetFormValues();

                _printer = new FormPrinter(this);

                _saveRequestDef = CreateRequestDef(false);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }


        internal void InitMruList()
        {
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry_RandomNumbersForm", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder_RandomNumbersForm", @"PFApps\RandomCustomValuesForm\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey_RandomNumbersForm", @"SOFTWARE\PFApps\RandomCustomValuesForm");
            _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries_RandomNumbersForm", (int)6);
            _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList_RandomNumbersForm", "true");

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

        }

        private void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("RandomSourcesHelpFileName", "RandomDataMasks.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        private void GetFileLocations()
        {
            string configValue = string.Empty;
            string randomDataRequestFolder = string.Empty;
            string randomCustomValuesDataRequestFolder = string.Empty;
            string randomCustomValuesOriginalDataRequestFolder = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomDataRequestFolder = configValue;
            else
                randomDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            configValue = AppConfig.GetStringValueFromConfigFile("RandomCustomValuesDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomCustomValuesDataRequestFolder = configValue;
            else
                randomCustomValuesDataRequestFolder = @"\PFApps\Randomizer\Definitions\CustomValues\";

            configValue = AppConfig.GetStringValueFromConfigFile("RandomCustomValuesOriginalDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomCustomValuesOriginalDataRequestFolder = configValue;
            else
                randomCustomValuesOriginalDataRequestFolder = @"\PFApps\Randomizer\OriginalDefinitions\CustomValues\";

            _randomCustomValuesDataRequestFolder = randomDataRequestFolder + randomCustomValuesDataRequestFolder;
            _randomCustomValuesOriginalDataRequestsFolder = randomDataRequestFolder + randomCustomValuesOriginalDataRequestFolder;

        }

        private void InitDesktopDatabasesFolder()
        {
            string sampleUdbFolder = _desktopDbFolder;
            string[] sampleZipFiles = {"SampleOrderData.accdb.zip",
                                       "SampleOrderData.db.zip",
                                       "SampleOrderData.mdb.zip",
                                       "SampleOrderData.udb.zip",
                                       "SampleOrderData.db.zip",
                                       "SampleOrderDataCE35.zip",
                                       "SampleOrderDataCE40.zip"};
            string[] sampleDbFiles = {"SampleOrderData.accdb",
                                      "SampleOrderData.db",
                                      "SampleOrderData.mdb",
                                      "SampleOrderData.udb",
                                      "SampleOrderData.db",
                                      "SampleOrderDataCE35.sdf",
                                      "SampleOrderDataCE40.sdf"};


            for(int i = 0; i < sampleDbFiles.Length; i++)
            {
                string sampleZipFile = Path.Combine(_desktopDbFolder, sampleZipFiles[i]);
                string sampleDbFile = Path.Combine(_desktopDbFolder, sampleDbFiles[i]);
                if (File.Exists(sampleDbFile) == false)
                {
                    if (File.Exists(sampleZipFile))
                    {
                        ZipArchive za = new ZipArchive(sampleZipFile);
                        za.ExtractAll(sampleUdbFolder);
                        za = null;
                        _msg.Length = 0;
                        _msg.Append(sampleDbFile);
                        _msg.Append(" extracted from ");
                        _msg.Append(sampleZipFile);
                        WriteToMessageLog(_msg.ToString());
                    }
                }
            }

            string sampleUdbZipFile = Path.Combine(_desktopDbFolder, "SampleOrderData.udb.zip");
            string sampleUdbFile = Path.Combine(_desktopDbFolder, "SampleOrderData.udb");

            if (File.Exists(sampleUdbFile) == false)
            {
                if (File.Exists(sampleUdbZipFile))
                {
                    ZipArchive za = new ZipArchive(sampleUdbZipFile);
                    za.ExtractAll(sampleUdbFolder);
                    za = null;
                }
            }
        }

        private void GetDefaultDatabaseSettings()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultOutputDatabaseType", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultOutputDatabaseType = configValue;
            }

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultOutputConnectionString", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultOutputDatabaseConnectionString = configValue;
            }

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultInputDatabaseType", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultInputDatabaseType = configValue;
            }

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultInputConnectionString", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultInputDatabaseConnectionString = configValue;
            }

            _numberOfRandomValueSamples = AppConfig.GetIntValueFromConfigFile("NumberOfRandomValueSamples", (int)100);

        }

        private void SetFormValues()
        {
            this.Text = "Define Random Custom Values Data Mask";

            InitRequestDefForm();

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
                case "Delete":
                    FileDelete();
                    break;
                case "Help":
                    ShowHelpFile();
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
            if (RequestDefinitionHasChanges() && this.txtDataMaskName.Text.Length > 0)
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileNew);
                if (result == DialogResult.Yes)
                {
                    if (FileSave() == false)
                    {
                        return;
                    }
                }
                if (result == DialogResult.Cancel)
                    return;
            }

            InitRequestDefForm();
            _saveRequestDef = CreateRequestDef(false);
        }

        private void FileOpen()
        {
            RandomDataRequestOpenListForm frm = null;
            string requestName = string.Empty;
            string filePath = string.Empty;

            if (RequestDefinitionHasChanges() && this.txtDataMaskName.Text.Length > 0)
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileOpen);
                if (result == DialogResult.Yes)
                {
                    if (FileSave() == false)
                    {
                        return;
                    }
                }
                if (result == DialogResult.Cancel)
                    return;
            }


            try
            {
                frm = new RandomDataRequestOpenListForm();
                frm.Caption = "Open Random Data Request";
                frm.ListBoxLabel = "Select item from list to open:";
                frm.SourceFolder = _randomCustomValuesDataRequestFolder;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    requestName = frm.lstNames.SelectedItem.ToString();
                    if (requestName.Length > 0)
                    {
                        filePath = Path.Combine(_randomCustomValuesDataRequestFolder, requestName + ".xml");
                        RandomCustomValuesDataRequest reqDef = RandomCustomValuesDataRequest.LoadFromXmlFile(filePath);
                        FillFormFromRequestDefinition(reqDef);
                        _saveRequestDef = reqDef;
                        UpdateMruList(requestName);
                    }
                }
                frm.Close();
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
                frm = null;
            }


        }

        private void FileClose()
        {
            if (RequestDefinitionHasChanges() && this.txtDataMaskName.Text.Length > 0)
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileNew);
                if (result == DialogResult.Yes)
                {
                    if (FileSave() == false)
                    {
                        return;
                    }
                }
                if (result == DialogResult.Cancel)
                    return;
            }

            InitRequestDefForm();
            _saveRequestDef = CreateRequestDef(false);
        }

        private bool FileSave()
        {
            bool saveSucceeded = true;

            if (this.txtDataMaskName.Text.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a name for the data mask definition.");
                AppMessages.DisplayErrorMessage(_msg.ToString());
                saveSucceeded = false;
                return false;
            }

            try
            {
                RandomCustomValuesDataRequest req = CreateRequestDef(true);
                if (req.ListName.Length == 0)
                {
                    //error occurred in CreateRequestDef: probably a validation error
                    saveSucceeded = false;
                    return false;
                }
                string filename = Path.Combine(_randomCustomValuesDataRequestFolder, req.ListName + ".xml");
                if (File.Exists(filename))
                {
                    _msg.Length = 0;
                    _msg.Append("Random numeric data request ");
                    _msg.Append(req.ListName);
                    _msg.Append(" already exists. \r\nDo you want to replace the old request with the new request?");
                    DialogResult res = AppMessages.DisplayMessage(_msg.ToString(), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No)
                    {
                        saveSucceeded = false;
                        return false;
                    }
                }
                req.SaveToXmlFile(filename);
                _saveRequestDef = req;
                UpdateMruList(req.ListName);
                _msg.Length = 0;
                _msg.Append("Save successful for random data request definition: ");
                _msg.Append(req.ListName);
                AppMessages.DisplayInfoMessage(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to save data mask definition failed for ");
                _msg.Append(this.txtDataMaskName.Text);
                _msg.Append(Environment.NewLine);
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
                saveSucceeded = false;
            }
            finally
            {
                ;
            }

            if (saveSucceeded)
                _numSavesForThisSession++;

            return saveSucceeded;
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
            mainMenuSaveFileDialog.FileName = string.Empty;
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
                WriteToMessageLog(_msg.ToString());
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
            _printer.PageSubTitle = "Random Custom Values Data Mask";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }


        private void OnMruFile(int number, String filename)
        {
            if (RequestDefinitionHasChanges() && this.txtDataMaskName.Text.Length > 0)
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileOpen);
                if (result == DialogResult.Yes)
                {
                    if (FileSave() == false)
                    {
                        return;
                    }
                }
                if (result == DialogResult.Cancel)
                    return;
            }

            string filePath = Path.Combine(_randomCustomValuesDataRequestFolder, filename + ".xml");

            if (File.Exists(filePath))
            {
                //process it
                RandomCustomValuesDataRequest reqdef = RandomCustomValuesDataRequest.LoadFromXmlFile(filePath);
                FillFormFromRequestDefinition(reqdef);
                _saveRequestDef = reqdef;

            }
            else
            {
                _msg.Length = 0;
                _msg.Append("File does not exist. It will be removed from the Mru List");
                _msm.RemoveFile(number);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

        }//end method

        private void FileDelete()
        {
            RandomDataRequestDeleteListForm frm = null;

            try
            {
                frm = new RandomDataRequestDeleteListForm();
                frm.Caption = "Delete Random Data Requests";
                frm.ListBoxLabel = "Select one or more requests to delete:";
                frm.SourceFolder = _randomCustomValuesDataRequestFolder;
                frm.ShowDialog();
                frm.Close();
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
                frm = null;
            }



        }


        //application routines

        private bool ProcessForm()
        {
            bool exitFromForm = true;
            DialogResult result = DialogResult.None;

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                if (RequestDefinitionHasChanges() && this.txtDataMaskName.Text.Length > 0)
                {
                    result = PromptForFileSave(ReasonForFileSavePrompt.FileNew);
                    if (result == DialogResult.Yes)
                    {
                        if (FileSave() == false)
                        {
                            exitFromForm = false;
                            return false;
                        }
                    }
                    if (result == DialogResult.Cancel)
                    {
                        exitFromForm = false;
                        return false;
                    }
                }

                if (this.txtDataMaskName.Text.Length > 0)
                {
                    CheckIfGenerateCustomDataFileNeeded();
                }


            }
            catch (System.Exception ex)
            {
                exitFromForm = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }

            return exitFromForm;
        }

        private void CheckIfGenerateCustomDataFileNeeded()
        {
            DialogResult result = DialogResult.None;
            StringBuilder displayMessage = new StringBuilder();
            bool dataFileExists = false;
            string filename = Path.Combine(_defaultDataGridExportFolder, this.txtDataMaskName.Text + ".xml");

            dataFileExists = File.Exists(filename);

            if (dataFileExists == false
                || _numSavesForThisSession > 0)
            {
                displayMessage.Length = 0;
                displayMessage.Append("Do you want to ");
                if (dataFileExists == false)
                    displayMessage.Append("generate");
                else
                    displayMessage.Append("re-generate");
                displayMessage.Append(" the custom value list before closing this form? ");
                result = AppMessages.DisplayMessage(displayMessage.ToString(), "Random Custom Values", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    GenerateCustomRandomDataFile();
                }
            }

        
        }

        private RandomCustomValuesDataRequest CreateRequestDef(bool verifyNumbers)
        {
            RandomCustomValuesDataRequest dataRequest = new RandomCustomValuesDataRequest();

            if (verifyNumbers)
            {
                string errMessages = VerifyNumericInput();
                if (errMessages.Length > 0)
                {
                    AppMessages.DisplayErrorMessage(errMessages);
                    return dataRequest;
                }
            }

            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            if (this.cboDatabaseTypeForCustomQuery.Text.Trim().Length > 0)
                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForCustomQuery.Text, true);
            CustomDataListGenerator gen = new CustomDataListGenerator(dbPlat);
            //string sqlUsedToGenerateDataValues = string.Empty;

            dataRequest.DbPlatform = dbPlat;
            dataRequest.DbConnectionString = this.txtConnectionStringForCustomQuery.Text;
            dataRequest.TableIncludeSearchPattern = this.txtTableIncludeSearchPattern.Text;
            dataRequest.TableExcludeSearchPattern = this.txtTableExcludeSearchPatten.Text;
            dataRequest.ListFolder = _defaultDataGridExportFolder;
            dataRequest.ListName = this.txtDataMaskName.Text;
            dataRequest.OutputToGrid = false; ;
            dataRequest.OutputToXmlFile = true;
            dataRequest.MaxOutputRows = PFTextProcessor.ConvertStringToInt(this.txtMaxTotalFrequency.Text, 3000);
            dataRequest.DbTableName = this.cboTableNameForCustomQuery.Text;
            dataRequest.DbFieldName = this.cboDataFieldForCustomQuery.Text;
            if (dataRequest.DbFieldName.Trim().Length > 0)
            {
                DataFieldNameTypeItem item = (DataFieldNameTypeItem)this.cboDataFieldForCustomQuery.SelectedItem;
                dataRequest.DbFieldType = item.FieldType.FullName;
            }
            else
            {
                dataRequest.DbFieldType = string.Empty;
            }
            dataRequest.SelectionField = this.cboCustomQuerySelectionField.Text;
            if (dataRequest.SelectionField.Trim().Length > 0)
            {
                DataFieldNameTypeItem item = (DataFieldNameTypeItem)this.cboCustomQuerySelectionField.SelectedItem;
                dataRequest.SelectionFieldType = item.FieldType.FullName;
            }
            else
            {
                dataRequest.SelectionFieldType = string.Empty;
            }
            dataRequest.SelectionCondition = this.cboCustomQuerySelectionCondition.Text;
            dataRequest.SelectionCriteria = this.txtCustomQuerySelectionCriteria.Text.Replace("*","%");
            dataRequest.MinimumValueFrequency = PFTextProcessor.ConvertStringToInt(this.txtMinimumFrequency.Text, 1);

            return dataRequest;
        }

        public string VerifyNumericInput()
        {
            bool minimumFrequencyIsNumber = true;
            bool maxTotalFrequencyIsNumber = true;
            int intNum = 0;

            _msg.Length = 0;

            minimumFrequencyIsNumber = int.TryParse(this.txtMinimumFrequency.Text, out intNum);
            maxTotalFrequencyIsNumber = int.TryParse(this.txtMaxTotalFrequency.Text, out intNum);

            if (minimumFrequencyIsNumber == false)
            {
                _msg.Append("Invalid minimum data value frequency value: ");
                _msg.Append(this.txtMinimumFrequency.Text);
                _msg.Append(Environment.NewLine);
            }
            if (maxTotalFrequencyIsNumber == false)
            {
                _msg.Append("Invalid max number of output rows value: ");
                _msg.Append(this.txtMaxTotalFrequency.Text);
                _msg.Append(Environment.NewLine);
            }

            return _msg.ToString();
        }


        private void FillFormFromRequestDefinition(RandomCustomValuesDataRequest reqDef)
        {
            //workaround to fix sample file for custom values definition
            if (reqDef.ListName == "LastNamesThatStartWithS")
            {
                if (reqDef.ListFolder.Contains(@"C:\Users\Mike\Documents\")
                    || reqDef.ListFolder.Contains(@"C:\Users\Test\Documents\"))
                {
                    FixSampleCustomValuesDefinition(reqDef);
                }
            }
            //end workaround
            this.txtDataMaskName.Text = reqDef.ListName;
            this.cboDatabaseTypeForCustomQuery.Text = reqDef.DbPlatform.ToString();
            this.txtConnectionStringForCustomQuery.Text = reqDef.DbConnectionString;
            this.txtTableIncludeSearchPattern.Text = reqDef.TableIncludeSearchPattern;
            this.txtTableExcludeSearchPatten.Text = reqDef.TableExcludeSearchPattern;
            this.txtDataMaskName.Text = reqDef.ListName;
            this.txtMaxTotalFrequency.Text = reqDef.MaxOutputRows.ToString();

            ConnectToDatabaseForCustomQueryTables();
            this.cboTableNameForCustomQuery.Text = reqDef.DbTableName;
            GetCustomQueryFields(this.cboTableNameForCustomQuery.Text);
            this.cboDataFieldForCustomQuery.Text = reqDef.DbFieldName;

            this.cboCustomQuerySelectionField.Text = reqDef.SelectionField;
            this.cboCustomQuerySelectionCondition.Text = reqDef.SelectionCondition;
            this.txtCustomQuerySelectionCriteria.Text = reqDef.SelectionCriteria;
            this.txtMinimumFrequency.Text = reqDef.MinimumValueFrequency.ToString();

            this.panelCustomQueryDataField.Visible = true;


        }

        //workaround to fix sample file for custom values definition
        private void FixSampleCustomValuesDefinition(RandomCustomValuesDataRequest reqDef)
        {
            string localDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (localDocumentsPath.EndsWith(@"\") == false)
                localDocumentsPath = localDocumentsPath + @"\";

            reqDef.ListFolder = reqDef.ListFolder.Replace(@"C:\Users\Mike\Documents\", localDocumentsPath);
            reqDef.ListFolder = reqDef.ListFolder.Replace(@"C:\Users\Test\Documents\", localDocumentsPath);
            reqDef.DbConnectionString = reqDef.DbConnectionString.Replace(@"C:\Users\Mike\Documents\", localDocumentsPath);
            reqDef.DbConnectionString = reqDef.DbConnectionString.Replace(@"C:\Users\Test\Documents\", localDocumentsPath);
        }
        //end workaround

        public void ShowHelpAboutTest(string testDescription)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append(testDescription);
                _msg.Append(": ");
                WriteToMessageLog(_msg.ToString());
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
                WriteToMessageLog(_msg.ToString());
                frm.Close();
                frm = null;
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
                ;
            }
        }

        private void InitCustomForm()
        {
            bool showInstalledDatabaseProvidersOnly = _showInstalledDatabaseProvidersOnly;
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

            this.cboDatabaseTypeForCustomQuery.Items.Clear();
            foreach (stKeyValuePair<string, PFProviderDefinition> provDef in provDefs)
            {
                if (showInstalledDatabaseProvidersOnly)
                {
                    if (provDef.Value.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                    {
                        if (provDef.Value.AvailableForSelection)
                        {
                            this.cboDatabaseTypeForCustomQuery.Items.Add(provDef.Key);
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
                        this.cboDatabaseTypeForCustomQuery.Items.Add(provDef.Key);
                    }
                    else
                    {
                        ;
                    }
                }
            }

            this.cboDatabaseTypeForCustomQuery.Text = _defaultInputDatabaseType; 
            this.txtConnectionStringForCustomQuery.Text = _defaultInputDatabaseConnectionString;
        }

        private void SetCustomQueryConnectionString()
        {
            this.txtConnectionStringForCustomQuery.Text = AppConfig.GetStringValueFromConfigFile("DefaultConnection_" + this.cboDatabaseTypeForCustomQuery.Text, string.Empty);
            this.txtTableIncludeSearchPattern.Text = string.Empty;
            this.txtTableExcludeSearchPatten.Text = string.Empty;
            this.panelCustomQueryDataField.Visible = false;
            this.cboTableNameForCustomQuery.Text = string.Empty;
            this.cboDataFieldForCustomQuery.Text = string.Empty;
            this.cboCustomQuerySelectionField.Text = string.Empty;
            this.cboCustomQuerySelectionCondition.Text = string.Empty;
            this.txtCustomQuerySelectionCriteria.Text = string.Empty;
            this.txtMinimumFrequency.Text = "1";
            this.txtMaxTotalFrequency.Text = "3000";
        }


        private void ConnectToDatabaseForCustomQueryTables()
        {
            CustomDataListGenerator gen = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.cboDatabaseTypeForCustomQuery.Text.Trim().Length == 0
                    || this.txtConnectionStringForCustomQuery.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both a database type and a connection string when connecting to a database for a custom query.");
                    throw new System.Exception(_msg.ToString());
                }
                DatabasePlatform dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForCustomQuery.Text);
                this.panelCustomQueryDataField.Visible = false;
                this.cboTableNameForCustomQuery.Text = string.Empty;
                this.cboTableNameForCustomQuery.Items.Clear();
                this.cboDataFieldForCustomQuery.Text = string.Empty;
                this.cboDataFieldForCustomQuery.Items.Clear();
                this.cboCustomQuerySelectionField.Text = string.Empty;
                this.cboCustomQuerySelectionField.Items.Clear();
                this.cboCustomQuerySelectionCondition.Text = string.Empty;
                this.txtCustomQuerySelectionCriteria.Text = string.Empty;
                this.txtMinimumFrequency.Text = "1";
                gen = new CustomDataListGenerator(dbPlat);
                gen.ConnectionString = this.txtConnectionStringForCustomQuery.Text;
                _tableList = gen.GetTableList(this.txtTableIncludeSearchPattern.Text, this.txtTableExcludeSearchPatten.Text);
                foreach (PFTableDef tabdef in _tableList)
                {
                    this.cboTableNameForCustomQuery.Items.Add(tabdef.TableFullName);
                }
                gen = null;
                this.panelCustomQueryDataField.Visible = true;
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
                this.Cursor = Cursors.Default;
                if (gen != null)
                {
                    gen = null;
                }
            }


        }

        private class DataFieldNameTypeItem
        {
            public string FieldName { get; set; }
            public Type FieldType { get; set; }

            public override string ToString()
            {
                return FieldName;
            }
        }

        public void GetCustomQueryFields(string tableFullName)
        {
            PFTableDef tabDef = null;

            this.cboDataFieldForCustomQuery.Text = string.Empty;
            this.cboDataFieldForCustomQuery.Items.Clear();
            this.cboCustomQuerySelectionField.Text = string.Empty;
            this.cboCustomQuerySelectionField.Items.Clear();
            this.cboCustomQuerySelectionCondition.Text = string.Empty;
            this.txtCustomQuerySelectionCriteria.Text = string.Empty;


            foreach (PFTableDef temp in _tableList)
            {
                if (String.Compare(temp.TableFullName, tableFullName, true) == 0)
                {
                    tabDef = temp;
                    break;
                }
            }

            if (tabDef != null)
            {
                this.cboCustomQuerySelectionField.Items.Add(string.Empty);
                DataTable dt = tabDef.TableObject;
                if (dt != null)
                {
                    if (dt.Columns.Count > 0)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            DataFieldNameTypeItem dataFieldItem = new DataFieldNameTypeItem();
                            dataFieldItem.FieldName = col.ColumnName;
                            dataFieldItem.FieldType = col.DataType;
                            this.cboDataFieldForCustomQuery.Items.Add(dataFieldItem);

                            DataFieldNameTypeItem selectionFieldItem = new DataFieldNameTypeItem();
                            selectionFieldItem.FieldName = col.ColumnName;
                            selectionFieldItem.FieldType = col.DataType;
                            this.cboCustomQuerySelectionField.Items.Add(selectionFieldItem);
                            //this.cboDataFieldForCustomQuery.Items.Add(col.ColumnName);
                            //this.cboCustomQuerySelectionField.Items.Add(col.ColumnName);
                        }
                    }//end if
                }//end if

            }//end if
        }//end method

        private void DefineConnectionString()
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ConnectionStringPrompt cp = null;


            try
            {
                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForCustomQuery.Text);
                connMgr = new PFConnectionManager();
                cp = new ConnectionStringPrompt(dbPlat, connMgr);
                cp.ConnectionString = this.txtConnectionStringForCustomQuery.Text;
                System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
                if (res == DialogResult.OK)
                {
                    this.txtConnectionStringForCustomQuery.Text = cp.ConnectionString;
                    ReinitDatabaseForCustomQueryTables();
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
                ;
            }

        }

        private void GenerateCustomRandomDataFile()
        {
            RandomCustomValuesDataRequest dataRequest = CreateRequestDef(true);

            string errMessages = VerifyNumericInput();
            if (errMessages.Length > 0)
            {
                AppMessages.DisplayErrorMessage(errMessages);
                return;
            }


            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;


                int maxTotalFrequency = AppTextGlobals.ConvertStringToInt(this.txtMaxTotalFrequency.Text, 3000);
                _appProcessor.ShowInstalledDatabaseProvidersOnly = _showInstalledDatabaseProvidersOnly;
                _appProcessor.DefaultOutputDatabaseType = _defaultOutputDatabaseType;
                _appProcessor.DefaultOutputDatabaseConnectionString = _defaultOutputDatabaseConnectionString;
                _appProcessor.GridExportFolder = _defaultDataGridExportFolder;

                _appProcessor.GetCustomRandomDataFile(dataRequest, maxTotalFrequency, true, false);

                _msg.Length = 0;
                _msg.Append("Data file with custom values generated in following folder: ");
                _msg.Append(_appProcessor.GridExportFolder);
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("File ");
                _msg.Append(dataRequest.ListName + ".xml");
                _msg.Append(" has random values.\r\n\r\n Supporting files " + dataRequest.ListName + ".sql, " + dataRequest.ListName + ".clistdef and " + dataRequest.ListName + ".clistsum  were also generated.");
                AppMessages.DisplayInfoMessage(_msg.ToString());

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
                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }
                 
        


        }

        private RandomCustomValuesDataRequest CreateCustomRandomDataRequest()
        {
            RandomCustomValuesDataRequest dataRequest = new RandomCustomValuesDataRequest();

            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            if (this.cboDatabaseTypeForCustomQuery.Text.Trim().Length > 0)
                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForCustomQuery.Text, true);
            CustomDataListGenerator gen = new CustomDataListGenerator(dbPlat);
            //string sqlUsedToGenerateDataValues = string.Empty;

            dataRequest.DbPlatform = dbPlat;
            dataRequest.DbConnectionString = this.txtConnectionStringForCustomQuery.Text;
            dataRequest.TableIncludeSearchPattern = this.txtTableIncludeSearchPattern.Text;
            dataRequest.TableExcludeSearchPattern = this.txtTableExcludeSearchPatten.Text;
            dataRequest.ListFolder = _defaultDataGridExportFolder;
            dataRequest.ListName = this.txtDataMaskName.Text;
            dataRequest.OutputToGrid = false; ;
            dataRequest.OutputToXmlFile = true;
            dataRequest.MaxOutputRows = PFTextProcessor.ConvertStringToInt(this.txtMaxTotalFrequency.Text, 3000);
            dataRequest.DbTableName = this.cboTableNameForCustomQuery.Text;
            dataRequest.DbFieldName = this.cboDataFieldForCustomQuery.Text;
            if (dataRequest.DbFieldName.Trim().Length > 0)
            {
                DataFieldNameTypeItem item = (DataFieldNameTypeItem)this.cboDataFieldForCustomQuery.SelectedItem;
                dataRequest.DbFieldType = item.FieldType.FullName;
            }
            else
            {
                dataRequest.DbFieldType = string.Empty;
            }
            dataRequest.SelectionField = this.cboCustomQuerySelectionField.Text;
            if (dataRequest.SelectionField.Trim().Length > 0)
            {
                DataFieldNameTypeItem item = (DataFieldNameTypeItem)this.cboCustomQuerySelectionField.SelectedItem;
                dataRequest.SelectionFieldType = item.FieldType.FullName;
            }
            else
            {
                dataRequest.SelectionFieldType = string.Empty;
            }
            dataRequest.SelectionCondition = this.cboCustomQuerySelectionCondition.Text;
            dataRequest.SelectionCriteria = this.txtCustomQuerySelectionCriteria.Text;
            dataRequest.MinimumValueFrequency = PFTextProcessor.ConvertStringToInt(this.txtMinimumFrequency.Text, 1);

            return dataRequest;
        }

        private void ReinitDatabaseForCustomQueryTables()
        {
            try
            {
                this.panelCustomQueryDataField.Visible = false;
                this.cboTableNameForCustomQuery.Text = string.Empty;
                this.cboTableNameForCustomQuery.Items.Clear();
                this.cboDataFieldForCustomQuery.Text = string.Empty;
                this.cboDataFieldForCustomQuery.Items.Clear();
                this.cboCustomQuerySelectionField.Text = string.Empty;
                this.cboCustomQuerySelectionField.Items.Clear();
                this.cboCustomQuerySelectionCondition.Text = string.Empty;
                this.txtCustomQuerySelectionCriteria.Text = string.Empty;
                this.txtMinimumFrequency.Text = "1";
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
                ;
            }

        }

        private void InitRequestDefForm()
        {
            this.txtDataMaskName.Text = string.Empty;
            this.txtMaxTotalFrequency.Text = "3000";

            InitCustomForm();

            SetCustomQueryConnectionString();

            this.txtMaxTotalFrequency.Text = AppConfig.GetStringValueFromConfigFile("DefaultOutputCustomListRows", "3300");

        }

        private void InitRequestDefObject(ref RandomCustomValuesDataRequest reqDef)
        {
            reqDef.DbPlatform = DatabasePlatform.Unknown;
            reqDef.DbConnectionString = string.Empty;
            reqDef.TableIncludeSearchPattern = string.Empty;
            reqDef.TableExcludeSearchPattern = string.Empty;
            reqDef.DbTableName = string.Empty;
            reqDef.DbFieldName = string.Empty;
            reqDef.DbFieldType = "System.String";
            reqDef.SelectionField = string.Empty;
            reqDef.SelectionFieldType = "System.String";
            reqDef.SelectionCondition = string.Empty;
            reqDef.SelectionCriteria = string.Empty;
            reqDef.MinimumValueFrequency = 1;
            reqDef.ListName = string.Empty;
            reqDef.ListFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Custom";
            reqDef.OutputToXmlFile = true;
            reqDef.OutputToGrid = true;
            reqDef.MaxOutputRows = 3000;
        }



        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }


        private void PreviewRandomData()
        {
            string errMessages = VerifyNumericInput();
            if (errMessages.Length > 0)
            {
                AppMessages.DisplayErrorMessage(errMessages);
                return;
            }

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                PreviewCustomValues();
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
                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }

        }

        private void PreviewCustomValues()
        {
            RandomCustomValuesDataRequest dataRequest = CreateRequestDef(true);
            DataTable listTable = null;
            DataTable summaryTable = null;
            RandomCustomValuesDataTable rndt = new RandomCustomValuesDataTable();

            if (this.cboTableNameForCustomQuery.Text.Trim().Length == 0
                || this.cboDataFieldForCustomQuery.Text.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must connect to a database and select a table and data field to use before running Preview.");
                AppMessages.DisplayAlertMessage(_msg.ToString());
                return;
            }

            try
            {
                //_appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                //_appProcessor.MessageLogUI = _messageLog;
                //_appProcessor.HelpFilePath = _helpFilePath;

                //DisableFormControls();
                //_appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                //this.Cursor = Cursors.WaitCursor;


                //int maxTotalFrequency = AppTextGlobals.ConvertStringToInt(this.txtMaxTotalFrequency.Text, 3000);
                //_appProcessor.ShowInstalledDatabaseProvidersOnly = _showInstalledDatabaseProvidersOnly;
                //_appProcessor.DefaultOutputDatabaseType = _defaultOutputDatabaseType;
                //_appProcessor.DefaultOutputDatabaseConnectionString = _defaultOutputDatabaseConnectionString;
                //_appProcessor.GridExportFolder = _defaultDataGridExportFolder;

                //_appProcessor.GetCustomRandomDataFile(dataRequest, maxTotalFrequency, false, true);

                listTable = rndt.CreateRandomCustomValuesDataTable(Convert.ToInt32(this.txtMaxTotalFrequency.Text),
                                                                   dataRequest,
                                                                   out summaryTable, 
                                                                   _showInstalledDatabaseProvidersOnly,
                                                                   _defaultOutputDatabaseType,
                                                                   _defaultOutputDatabaseConnectionString,
                                                                   _defaultDataGridExportFolder);
                if(summaryTable != null)
                    OutputDataTableToGrid(summaryTable);
                if(listTable != null)
                    OutputDataTableToGrid(listTable);
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
                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }

        }

        private void OutputDataTableToGrid(DataTable tab)
        {
            PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
            grid.ShowInstalledDatabaseProvidersOnly = _showInstalledDatabaseProvidersOnly;
            grid.DefaultOutputDatabaseType = _defaultOutputDatabaseType;
            grid.DefaultOutputDatabaseConnectionString = _defaultOutputDatabaseConnectionString;
            if (Directory.Exists(_defaultDataGridExportFolder) == false)
            {
                grid.DefaultGridExportFolder = _initDataGridExportFolder;
            }
            else
            {
                grid.DefaultGridExportFolder = _defaultDataGridExportFolder;
            }
            grid.EnableExportMenu = true;
            grid.WriteDataToGrid(tab);
        }

        private void ReloadOriginalRequestDefinitions()
        {
            if (Directory.Exists(_randomCustomValuesOriginalDataRequestsFolder))
            {
                foreach (string sourceFilePath in Directory.GetFiles(_randomCustomValuesOriginalDataRequestsFolder))
                {
                    string destFilePath = Path.Combine(_randomCustomValuesDataRequestFolder, Path.GetFileName(sourceFilePath));
                    File.Copy(sourceFilePath, destFilePath, true);
                }
            }
            //force a save prompt of existing data on the form if user exits without saving after this routine is finished
            RandomCustomValuesDataRequest reqDef = new RandomCustomValuesDataRequest();
            InitRequestDefObject(ref reqDef);
            _saveRequestDef = reqDef;

        }

#pragma warning restore 1591


    }//end class
}//end namespace
