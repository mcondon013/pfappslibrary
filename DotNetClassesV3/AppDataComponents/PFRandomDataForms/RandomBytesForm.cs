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
using PFPrinterObjects;
using PFAppDataObjects;
using PFRandomDataProcessor;
using PFRandomValueDataTables;
using PFRandomDataExt;
using KellermanSoftware.CompareNetObjects;
using PFTextObjects;

namespace PFRandomDataForms
{
#pragma warning disable 1591
    /// <summary>
    /// Form for definition and preview of random bytes.
    /// </summary>
    public partial class RandomBytesForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _randomBytesDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\Bytes\";
        private string _randomBytesOriginalDataRequestsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\OriginalDefinitions\Bytes\";

        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Bytes";
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Bytes";
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private bool _showInstalledDatabaseProvidersOnly = true;
        private int _numberOfRandomValueSamples = 100;

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
        private RandomBytesDataRequest _saveRequestDef = null;
        private RandomBytesDataRequest _exitRequestDef = null;


        //constructors

        public RandomBytesForm()
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

        private void optArrayOfValues_CheckedChanged(object sender, EventArgs e)
        {
            ArrayOfValuesCheckedChanged();
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
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Random Bytes");
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

                GetDefaultDatabaseSettings();

                SetFormValues();

                _printer = new FormPrinter(this);

                this.txtNumRandomDataItems.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");

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
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry_RandomBytesForm", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder_RandomBytesForm", @"PFApps\RandomBytesForm\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey_RandomBytesForm", @"SOFTWARE\PFApps\RandomBytesForm");
            _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries_RandomBytesForm", (int)6);
            _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList_RandomBytesForm", "true");

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

        internal void SetHelpFileValues()
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
            string randomBytesDataRequestFolder = string.Empty;
            string randomBytesOriginalDataRequestFolder = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomDataRequestFolder = configValue;
            else
                randomDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            configValue = AppConfig.GetStringValueFromConfigFile("RandomBytesDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomBytesDataRequestFolder = configValue;
            else
                randomBytesDataRequestFolder = @"\PFApps\Randomizer\Definitions\Bytes\";

            configValue = AppConfig.GetStringValueFromConfigFile("RandomBytesOriginalDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomBytesOriginalDataRequestFolder = configValue;
            else
                randomBytesOriginalDataRequestFolder = @"\PFApps\Randomizer\OriginalDefinitions\Bytes\";

            _randomBytesDataRequestFolder = randomDataRequestFolder + randomBytesDataRequestFolder;
            _randomBytesOriginalDataRequestsFolder = randomDataRequestFolder + randomBytesOriginalDataRequestFolder;

        }

        private void GetDefaultDatabaseSettings()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultDatabaseType", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultOutputDatabaseType = configValue;
            }

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnectionString", string.Empty);
            if (configValue != string.Empty)
            {
                _defaultOutputDatabaseConnectionString = configValue;
            }

            _numberOfRandomValueSamples = AppConfig.GetIntValueFromConfigFile("NumberOfRandomValueSamples", (int)100);

        }

        private void SetFormValues()
        {
            this.Text = "Define Random Bytes Data Mask";

            this.optByte.Checked = true;
            this.optArrayOfValues.Checked = true;
            this.txtMinArrayLength.Text = "10";
            this.txtMaxArrayLength.Text = "25";
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

        private void ArrayOfValuesCheckedChanged()
        {
            if (optArrayOfValues.Checked)
            {
                this.pnlArrayLength.Enabled = true;
            }
            else
            {
                this.pnlArrayLength.Enabled = false;
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
                frm.SourceFolder = _randomBytesDataRequestFolder;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    requestName = frm.lstNames.SelectedItem.ToString();
                    if (requestName.Length > 0)
                    {
                        filePath = Path.Combine(_randomBytesDataRequestFolder, requestName + ".xml");
                        RandomBytesDataRequest reqDef = RandomBytesDataRequest.LoadFromXmlFile(filePath);
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
                RandomBytesDataRequest req = CreateRequestDef(true);
                if (req.Name.Length == 0)
                {
                    //error occurred in CreateRequestDef: probably a validation error
                    saveSucceeded = false;
                    return false;
                }
                string filename = Path.Combine(_randomBytesDataRequestFolder, req.Name + ".xml");
                if (File.Exists(filename))
                {
                    _msg.Length = 0;
                    _msg.Append("Random bytes data request ");
                    _msg.Append(req.Name);
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
                UpdateMruList(req.Name);
                _msg.Length = 0;
                _msg.Append("Save successful for random data request definition: ");
                _msg.Append(req.Name);
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
            _printer.PageSubTitle = "Application Form";
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

            string filePath = Path.Combine(_randomBytesDataRequestFolder, filename + ".xml");

            if (File.Exists(filePath))
            {
                //process it
                RandomBytesDataRequest reqdef = RandomBytesDataRequest.LoadFromXmlFile(filePath);
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
                frm.SourceFolder = _randomBytesDataRequestFolder;
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

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                if (RequestDefinitionHasChanges() && this.txtDataMaskName.Text.Length > 0)
                {
                    DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileNew);
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

        private void InitRequestDefForm()
        {
            this.txtDataMaskName.Text = string.Empty;
            this.optByte.Checked = true;
            this.optArrayOfValues.Checked = true;
            this.txtMinArrayLength.Text = string.Empty;
            this.txtMaxArrayLength.Text = string.Empty;
            this.txtNumRandomDataItems.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");
        }

        private void InitRequestDefObject(ref RandomBytesDataRequest reqDef)
        {
            reqDef.Name = string.Empty;
            reqDef.OutputByte = false;
            reqDef.OutputChar = false;
            reqDef.OutputArrayOfValues = false;
            reqDef.MinArrayLength = string.Empty;
            reqDef.MaxArrayLength = string.Empty;
            reqDef.OutputSingleValue = false;
            reqDef.NumRandomDataItems = 1000;
        }

        private RandomBytesDataRequest CreateRequestDef(bool verifyNumbers)
        {
            RandomBytesDataRequest reqDef = new RandomBytesDataRequest();

            if (verifyNumbers)
            {
                string errMessages = VerifyNumericInput();
                if (errMessages.Length > 0)
                {
                    AppMessages.DisplayErrorMessage(errMessages);
                    return reqDef;
                }
            }

            reqDef.Name = this.txtDataMaskName.Text;
            reqDef.OutputByte = this.optByte.Checked;
            reqDef.OutputChar = this.optChar.Checked;
            reqDef.OutputArrayOfValues = this.optArrayOfValues.Checked;
            reqDef.MinArrayLength = this.txtMinArrayLength.Text ;
            reqDef.MaxArrayLength = this.txtMaxArrayLength.Text;
            reqDef.OutputSingleValue = this.optSingleValue.Checked;
            reqDef.NumRandomDataItems = PFTextProcessor.ConvertStringToInt(this.txtNumRandomDataItems.Text, 1000);

            return reqDef;
        }

        public string VerifyNumericInput()
        {
            bool minArrayLengthIsNumber = true;
            bool maxArrayLengthIsNumber = true;
            bool minMaxArrayLengthIsValid = true;
            bool numRandomDataItemsIsNumber = true;
            int intNum = 0;

            _msg.Length = 0;


            if (this.optArrayOfValues.Checked)
            {
                long min = 0;
                long max = 0;
                if (this.optArrayOfValues.Checked)
                {
                    minArrayLengthIsNumber = long.TryParse(this.txtMinArrayLength.Text, out min);
                    maxArrayLengthIsNumber = long.TryParse(this.txtMaxArrayLength.Text, out max);
                    if (minArrayLengthIsNumber && maxArrayLengthIsNumber)
                    {
                        if (min > max)
                            minMaxArrayLengthIsValid = false;
                    }
                }
            }

            numRandomDataItemsIsNumber = int.TryParse(this.txtNumRandomDataItems.Text, out intNum);

            if (minArrayLengthIsNumber == false)
            {
                _msg.Append("Invalid minimum value: ");
                _msg.Append(this.txtMinArrayLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (maxArrayLengthIsNumber == false)
            {
                _msg.Append("Invalid maximum value: ");
                _msg.Append(this.txtMaxArrayLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (minMaxArrayLengthIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtMinArrayLength.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtMaxArrayLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (numRandomDataItemsIsNumber == false)
            {
                _msg.Append("Invalid number of random data items value: ");
                _msg.Append(this.txtNumRandomDataItems.Text);
                _msg.Append(Environment.NewLine);
            }

            return _msg.ToString();
        }


        private void FillFormFromRequestDefinition(RandomBytesDataRequest reqDef)
        {
            this.txtDataMaskName.Text = reqDef.Name;
            this.optByte.Checked = reqDef.OutputByte;
            this.optChar.Checked = reqDef.OutputChar;
            this.optArrayOfValues.Checked = reqDef.OutputArrayOfValues;
            this.txtMinArrayLength.Text = reqDef.MinArrayLength;
            this.txtMaxArrayLength.Text = reqDef.MaxArrayLength;
            this.optSingleValue.Checked = reqDef.OutputSingleValue;
            this.txtNumRandomDataItems.Text = reqDef.NumRandomDataItems.ToString();
        }

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

                if (optArrayOfValues.Checked)
                {
                    if (this.optByte.Checked)
                    {
                        PreviewByteArray();
                    }
                    else
                    {
                        PreviewCharArray();
                    }
                }
                else // (optSingleValue.Checked)
                {
                    if (this.optByte.Checked)
                    {
                        PreviewByteValue();
                    }
                    else
                    {
                        PreviewCharValue();
                    }
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
                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }
                 
        }

        private void PreviewByteArray()
        {
            DataTable dt = null;
            RandomBytesDataTable rndt = new RandomBytesDataTable();

            try
            {
                dt = rndt.CreateByteArrayPreviewDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinArrayLength.Text, this.txtMaxArrayLength.Text);
                OutputDataTableToGrid(dt);
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

        private void PreviewCharArray()
        {
            DataTable dt = null;
            RandomBytesDataTable rndt = new RandomBytesDataTable();

            try
            {
                dt = rndt.CreateCharArrayPreviewDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinArrayLength.Text, this.txtMaxArrayLength.Text);
                OutputDataTableToGrid(dt);

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

        private void PreviewByteValue()
        {
            DataTable dt = null;
            RandomBytesDataTable rndt = new RandomBytesDataTable();

            try
            {
                dt = rndt.CreateByteValuePreviewDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text));
                OutputDataTableToGrid(dt);
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

        private void PreviewCharValue()
        {
            DataTable dt = null;
            RandomBytesDataTable rndt = new RandomBytesDataTable();

            try
            {
                dt = rndt.CreateCharValuePreviewDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text));
                OutputDataTableToGrid(dt);
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
            if (Directory.Exists(_randomBytesOriginalDataRequestsFolder))
            {
                foreach (string sourceFilePath in Directory.GetFiles(_randomBytesOriginalDataRequestsFolder))
                {
                    string destFilePath = Path.Combine(_randomBytesDataRequestFolder, Path.GetFileName(sourceFilePath));
                    File.Copy(sourceFilePath, destFilePath, true);
                }
            }
            //force a save prompt of existing data on the form if user exits without saving after this routine is finished
            RandomBytesDataRequest reqDef = new RandomBytesDataRequest();
            InitRequestDefObject(ref reqDef);
            _saveRequestDef = reqDef;

        }

    }//end class
#pragma warning restore 1591


}//end namespace
