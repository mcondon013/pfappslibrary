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

namespace PFRandomDataForms
{
#pragma warning disable 1591
    /// <summary>
    /// Form for definition and preview of random string values.
    /// </summary>
    public partial class RandomStringsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _randomStringDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\Strings\";
        private string _randomStringOriginalDataRequestsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\OriginalDefinitions\Strings\";

        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Strings";
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Strings";
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
        private RandomStringDataRequest _saveRequestDef = null;
        private RandomStringDataRequest _exitRequestDef = null;


        //constructors

        public RandomStringsForm()
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

        private void optRandomStrings_CheckedChanged(object sender, EventArgs e)
        {
            RandomStringsCheckedChanged();
        }

        private void optRepeatRandomCharacter_CheckedChanged(object sender, EventArgs e)
        {
            RepeatRandomCharacterCheckedChanged();
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
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Random Strings");
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
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry_RandomStringsForm", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder_RandomStringsForm", @"PFApps\RandomStringsForm\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey_RandomStringsForm", @"SOFTWARE\PFApps\RandomStringsForm");
            _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries_RandomStringsForm", (int)6);
            _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList_RandomStringsForm", "true");

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
            string randomStringDataRequestFolder = string.Empty;
            string randomStringOriginalDataRequestFolder = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomDataRequestFolder = configValue;
            else
                randomDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            configValue = AppConfig.GetStringValueFromConfigFile("RandomStringsDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomStringDataRequestFolder = configValue;
            else
                randomStringDataRequestFolder = @"\PFApps\Randomizer\Definitions\Strings\";

            configValue = AppConfig.GetStringValueFromConfigFile("RandomStringsOriginalDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomStringOriginalDataRequestFolder = configValue;
            else
                randomStringOriginalDataRequestFolder = @"\PFApps\Randomizer\OriginalDefinitions\Strings\";

            _randomStringDataRequestFolder = randomDataRequestFolder + randomStringDataRequestFolder;
            _randomStringOriginalDataRequestsFolder = randomDataRequestFolder + randomStringOriginalDataRequestFolder;

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
            this.Text = "Define String Data Mask";

            this.optRandomStrings.Checked = true;
            this.optAN.Checked = true;
            this.txtMinNumStrings.Text = "1";
            this.txtMaxNumStrings.Text = "3";
            this.txtStringMinimumLength.Text = "3";
            this.txtStringMaximumLength.Text = "7";
            this.cboRegexPattern.Text = string.Empty;
            this.cboRegexReplacement.Text = string.Empty;
            this.optSyllableLC.Checked = true;
            this.txtMinNumSyllableStrings.Text = "1";
            this.txtMaxNumSyllableStrings.Text = "3";
            this.txtSyllableStringMinimumLength.Text = "2";
            this.txtSyllableStringMaximumLength.Text = "4";
            this.optRepeatRandomCharacter.Checked = true;
            this.optRepeatAN.Checked = true;
            this.txtMinRepeatOutputLength.Text = "5";
            this.txtMaxRepeatOutputLength.Text = "10";
            this.txtMinNumRepeats.Text = "2";
            this.txtMaxNumRepeats.Text = "5";
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

        private void RandomStringsCheckedChanged()
        {
            if (this.optRandomStrings.Checked)
            {
                this.pnlRandomStrings.Enabled = true;
                this.pnlRandomSyllables.Enabled = false;
                this.pnlRepeatingStrings.Enabled = false;
            }
            else if (this.optRandomSyllableStrings.Checked)
            {
                this.pnlRandomStrings.Enabled = false;
                this.pnlRandomSyllables.Enabled = true;
                this.pnlRepeatingStrings.Enabled = false;
            }
            else if (this.optRepeatingStrings.Checked)
            {
                this.pnlRandomStrings.Enabled = false;
                this.pnlRandomSyllables.Enabled = false;
                this.pnlRepeatingStrings.Enabled = true;
            }
            else
            {
                ; //do nothing
            }
        }

        private void RepeatRandomCharacterCheckedChanged()
        {
            if (optRepeatRandomCharacter.Checked)
            {
                this.pnlRepeatRandomCharacterType.Enabled = true;
                this.txtMinRepeatOutputLength.Enabled = true;
                this.txtMaxRepeatOutputLength.Enabled = true;
                this.txtTextToRepeat.Enabled = false;
            }
            else if (optRepeatText.Checked)
            {
                this.pnlRepeatRandomCharacterType.Enabled = false;
                this.txtMinRepeatOutputLength.Enabled = false;
                this.txtMaxRepeatOutputLength.Enabled = false;
                this.txtTextToRepeat.Enabled = true;
            }
            else
            {
                ; //do nothing
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
                frm.SourceFolder = _randomStringDataRequestFolder;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    requestName = frm.lstNames.SelectedItem.ToString();
                    if (requestName.Length > 0)
                    {
                        filePath = Path.Combine(_randomStringDataRequestFolder, requestName + ".xml");
                        RandomStringDataRequest reqDef = RandomStringDataRequest.LoadFromXmlFile(filePath);
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

        private bool                                  FileSave()
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
                RandomStringDataRequest req = CreateRequestDef(true);
                if (req.Name.Length == 0)
                {
                    //error occurred in CreateRequestDef: probably a validation error
                    saveSucceeded = false;
                    return false;
                }
                string filename = Path.Combine(_randomStringDataRequestFolder, req.Name + ".xml");
                if (File.Exists(filename))
                {
                    _msg.Length = 0;
                    _msg.Append("Random string data request ");
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

            string filePath = Path.Combine(_randomStringDataRequestFolder, filename + ".xml");

            if (File.Exists(filePath))
            {
                //process it
                RandomStringDataRequest reqdef = RandomStringDataRequest.LoadFromXmlFile(filePath);
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
                frm.SourceFolder = _randomStringDataRequestFolder;
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
            this.optRandomStrings.Checked = true;
            this.optAN.Checked = true;
            this.optANUC.Checked = false;
            this.optANLC.Checked = false;
            this.optANX.Checked = false;
            this.optAL.Checked = false;
            this.optLC.Checked = false;
            this.optUC.Checked = false;
            this.optDEC.Checked = false;
            this.optHEX.Checked = false;
            this.txtMinNumStrings.Text = string.Empty;
            this.txtMaxNumStrings.Text = string.Empty;
            this.txtStringMinimumLength.Text = string.Empty;
            this.txtStringMaximumLength.Text = string.Empty;
            this.cboRegexPattern.Text = string.Empty;
            this.cboRegexReplacement.Text = string.Empty;
            this.optRandomSyllableStrings.Checked = false;
            this.optSyllableUCLC.Checked = false;
            this.optSyllableLC.Checked = true;
            this.optSyllableUC.Checked = false;
            this.txtMinNumSyllableStrings.Text = string.Empty;
            this.txtMaxNumSyllableStrings.Text = string.Empty;
            this.txtSyllableStringMinimumLength.Text = string.Empty;
            this.txtSyllableStringMaximumLength.Text = string.Empty;
            this.optRepeatingStrings.Checked = false;
            this.optRepeatRandomCharacter.Checked = true;
            this.optRepeatAN.Checked = true;
            this.optRepeatANUC.Checked = false;
            this.optRepeatANLC.Checked = false;
            this.optRepeatANX.Checked = false;
            this.optRepeatAL.Checked = false;
            this.optRepeatLC.Checked = false;
            this.optRepeatUC.Checked = false;
            this.optRepeatDEC.Checked = false;
            this.optRepeatHEX.Checked = false;
            this.optRepeatText.Checked = false;
            this.txtMinRepeatOutputLength.Text = string.Empty;
            this.txtMaxRepeatOutputLength.Text = string.Empty;
            this.txtTextToRepeat.Text = string.Empty;
            this.txtMinNumRepeats.Text = string.Empty;
            this.txtMaxNumRepeats.Text = string.Empty;
            this.txtNumRandomDataItems.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");
        }

        private void InitRequestDefObject(ref RandomStringDataRequest reqDef)
        {
            reqDef.Name = string.Empty;
            reqDef.OutputRandomStrings = true;
            reqDef.OutputAN = true;
            reqDef.OutputANUC = false;
            reqDef.OutputANLC = false;
            reqDef.OutputANX = false;
            reqDef.OutputAL = false;
            reqDef.OutputLC = false;
            reqDef.OutputUC = false;
            reqDef.OutputDEC = false;
            reqDef.OutputHEX = false;
            reqDef.MinNumStrings = string.Empty;
            reqDef.MaxNumStrings = string.Empty;
            reqDef.StringMinimumLength = string.Empty;
            reqDef.StringMaximumLength = string.Empty;
            reqDef.RegexPattern = string.Empty;
            reqDef.RegexReplacement = string.Empty;
            reqDef.OutputRandomSyllableStrings = false;
            reqDef.OutputSyllableUCLC = false;
            reqDef.OutputSyllableLC = true;
            reqDef.OutputSyllableUC = false;
            reqDef.MinNumSyllableStrings = string.Empty;
            reqDef.MaxNumSyllableStrings = string.Empty;
            reqDef.SyllableStringMinimumLength = string.Empty;
            reqDef.SyllableStringMaximumLength = string.Empty;
            reqDef.OutputRepeatingStrings = false;
            reqDef.OutputRepeatRandomCharacter = true;
            reqDef.OutputRepeatAN = true;
            reqDef.OutputRepeatANUC = false;
            reqDef.OutputRepeatANLC = false;
            reqDef.OutputRepeatANX = false;
            reqDef.OutputRepeatAL = false;
            reqDef.OutputRepeatLC = false;
            reqDef.OutputRepeatUC = false;
            reqDef.OutputRepeatDEC = false;
            reqDef.OutputRepeatHEX = false;
            reqDef.MinRepeatOutputLength = string.Empty;
            reqDef.MaxRepeatOutputLength = string.Empty;
            reqDef.OutputRepeatText = false;
            reqDef.TextToRepeat = string.Empty;
            reqDef.MinNumRepeats = string.Empty;
            reqDef.MaxNumRepeats = string.Empty;
            reqDef.NumRandomDataItems = 1000;
        }

        private RandomStringDataRequest CreateRequestDef(bool verifyNumbers)
        {
            RandomStringDataRequest reqDef = new RandomStringDataRequest();

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
            reqDef.OutputRandomStrings = this.optRandomStrings.Checked;
            reqDef.OutputAN = this.optAN.Checked;
            reqDef.OutputANUC = this.optANUC.Checked;
            reqDef.OutputANLC = this.optANLC.Checked;
            reqDef.OutputANX = this.optANX.Checked;
            reqDef.OutputAL = this.optAL.Checked;
            reqDef.OutputLC = this.optLC.Checked;
            reqDef.OutputUC = this.optUC.Checked;
            reqDef.OutputDEC = this.optDEC.Checked;
            reqDef.OutputHEX = this.optHEX.Checked;
            reqDef.MinNumStrings = this.txtMinNumStrings.Text;
            reqDef.MaxNumStrings = this.txtMaxNumStrings.Text;
            reqDef.StringMinimumLength = this.txtStringMinimumLength.Text;
            reqDef.StringMaximumLength = this.txtStringMaximumLength.Text;
            reqDef.RegexPattern = this.cboRegexPattern.Text;
            reqDef.RegexReplacement = this.cboRegexReplacement.Text;
            reqDef.OutputRandomSyllableStrings = this.optRandomSyllableStrings.Checked;
            reqDef.OutputSyllableUCLC = this.optSyllableUCLC.Checked;
            reqDef.OutputSyllableLC = this.optSyllableLC.Checked;
            reqDef.OutputSyllableUC = this.optSyllableUC.Checked;
            reqDef.MinNumSyllableStrings = this.txtMinNumSyllableStrings.Text;
            reqDef.MaxNumSyllableStrings = this.txtMaxNumSyllableStrings.Text;
            reqDef.SyllableStringMinimumLength = this.txtSyllableStringMinimumLength.Text;
            reqDef.SyllableStringMaximumLength = this.txtSyllableStringMaximumLength.Text;
            reqDef.OutputRepeatingStrings = this.optRepeatingStrings.Checked;
            reqDef.OutputRepeatRandomCharacter = this.optRepeatRandomCharacter.Checked;
            reqDef.OutputRepeatAN = this.optRepeatAN.Checked;
            reqDef.OutputRepeatANUC = this.optRepeatANUC.Checked;
            reqDef.OutputRepeatANLC = this.optRepeatANLC.Checked;
            reqDef.OutputRepeatANX = this.optRepeatANX.Checked;
            reqDef.OutputRepeatAL = this.optRepeatAL.Checked;
            reqDef.OutputRepeatLC = this.optRepeatLC.Checked;
            reqDef.OutputRepeatUC = this.optRepeatUC.Checked;
            reqDef.OutputRepeatDEC = this.optRepeatDEC.Checked;
            reqDef.OutputRepeatHEX = this.optRepeatHEX.Checked;
            reqDef.MinRepeatOutputLength = this.txtMinRepeatOutputLength.Text;
            reqDef.MaxRepeatOutputLength =this.txtMaxRepeatOutputLength.Text;
            reqDef.OutputRepeatText = this.optRepeatText.Checked;
            reqDef.TextToRepeat = this.txtTextToRepeat.Text;
            reqDef.MinNumRepeats = this.txtMinNumRepeats.Text;
            reqDef.MaxNumRepeats = this.txtMaxNumRepeats.Text;
            reqDef.NumRandomDataItems = PFTextProcessor.ConvertStringToInt(this.txtNumRandomDataItems.Text, 1000);

            return reqDef;
        }

        public string VerifyNumericInput()
        {
            bool minNumStringsIsNumber = true;
            bool maxNumStringsIsNumber = true;
            bool stringMinimumLengthIsNumber = true;
            bool stringMaximumLengthIsNumber = true;
            bool minNumSyllableStringsIsNumber = true;
            bool maxNumSyllableStringsIsNumber = true;
            bool syllableStringMinimumLengthIsNumber = true;
            bool syllableStringMaximumLengthIsNumber = true;
            bool minRepeatOutputLengthIsNumber = true;
            bool maxRepeatOutputLengthIsNumber = true;
            bool minNumRepeatsIsNumber = true;
            bool maxNumRepeatsIsNumber = true;

            bool numStringsMinMaxIsValid = true;
            bool stringLengthMinMaxIsValid = true;
            bool numSyllableStringsMinMaxIsValid = true;
            bool syllableStringLengthMinMaxIsValid = true;
            bool repeatOutputLengthMinMaxIsValid = true;
            bool numRepeatsMinMaxIsValid = true;

            uint min = uint.MinValue;
            uint max = uint.MaxValue;

            bool numRandomDataItemsIsNumber = true;
            int intNum = 0;
            
            _msg.Length = 0;

            if (this.optRandomStrings.Checked)
            {
                minNumStringsIsNumber = uint.TryParse(this.txtMinNumStrings.Text, out min);
                maxNumStringsIsNumber = uint.TryParse(this.txtMaxNumStrings.Text, out max);
                if(minNumStringsIsNumber && maxNumStringsIsNumber)
                {
                    if (min > max)
                    {
                        numStringsMinMaxIsValid = false;
                    }
                }

                stringMinimumLengthIsNumber = uint.TryParse(this.txtStringMinimumLength.Text, out min);
                stringMaximumLengthIsNumber = uint.TryParse(this.txtStringMaximumLength.Text, out max);
                if (stringMinimumLengthIsNumber && stringMaximumLengthIsNumber)
                {
                    if (min > max)
                    {
                        stringLengthMinMaxIsValid = false;
                    }
                }
            }
            else if (this.optRandomSyllableStrings.Checked)
            {
                minNumSyllableStringsIsNumber = uint.TryParse(this.txtMinNumSyllableStrings.Text, out min);
                maxNumSyllableStringsIsNumber = uint.TryParse(this.txtMaxNumSyllableStrings.Text, out max);
                if (minNumSyllableStringsIsNumber && maxNumSyllableStringsIsNumber)
                {
                    if (min > max)
                    {
                        numSyllableStringsMinMaxIsValid = false;
                    }
                }

                syllableStringMinimumLengthIsNumber = uint.TryParse(this.txtSyllableStringMinimumLength.Text, out min);
                syllableStringMaximumLengthIsNumber = uint.TryParse(this.txtSyllableStringMaximumLength.Text, out max);
                if (syllableStringMinimumLengthIsNumber && syllableStringMaximumLengthIsNumber)
                {
                    if (min > max)
                    {
                        syllableStringLengthMinMaxIsValid = false;
                    }
                }
            }
            else if (this.optRepeatingStrings.Checked)
            {
                if (this.optRepeatRandomCharacter.Checked)
                {
                    minRepeatOutputLengthIsNumber = uint.TryParse(this.txtMinRepeatOutputLength.Text, out min);
                    maxRepeatOutputLengthIsNumber = uint.TryParse(this.txtMaxRepeatOutputLength.Text, out max);
                    if (minRepeatOutputLengthIsNumber && maxRepeatOutputLengthIsNumber)
                    {
                        if (min > max)
                        {
                            repeatOutputLengthMinMaxIsValid = false;
                        }
                    }
                }
                minNumRepeatsIsNumber = uint.TryParse(this.txtMinNumRepeats.Text, out min);
                maxNumRepeatsIsNumber = uint.TryParse(this.txtMaxNumRepeats.Text, out max);
                if (minNumRepeatsIsNumber && maxNumRepeatsIsNumber)
                {
                    if (min > max)
                    {
                        numRepeatsMinMaxIsValid = false;
                    }
                }
            }
            else
            {
                ; //do nothing
            }

            numRandomDataItemsIsNumber = int.TryParse(this.txtNumRandomDataItems.Text, out intNum);

            if (minNumStringsIsNumber == false)
            {
                _msg.Append("Invalid minimum number of strings value: ");
                _msg.Append(this.txtMinNumStrings.Text);
                _msg.Append(Environment.NewLine);
            }
            if (maxNumStringsIsNumber == false)
            {
                _msg.Append("Invalid maximum number of strings value: ");
                _msg.Append(this.txtMaxNumStrings.Text);
                _msg.Append(Environment.NewLine);
            }
            if (stringMinimumLengthIsNumber == false)
            {
                _msg.Append("Invalid minimum string length value: ");
                _msg.Append(this.txtStringMinimumLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (stringMaximumLengthIsNumber == false)
            {
                _msg.Append("Invalid maximum string length value: ");
                _msg.Append(this.txtStringMaximumLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (minNumSyllableStringsIsNumber == false)
            {
                _msg.Append("Invalid minimum number of syllable strings value: ");
                _msg.Append(this.txtMinNumSyllableStrings.Text);
                _msg.Append(Environment.NewLine);
            }
            if (maxNumSyllableStringsIsNumber == false)
            {
                _msg.Append("Invalid maximum number of syllable strings value: ");
                _msg.Append(this.txtMaxNumSyllableStrings.Text);
                _msg.Append(Environment.NewLine);
            }
            if (syllableStringMinimumLengthIsNumber == false)
            {
                _msg.Append("Invalid minimum syllable string length value: ");
                _msg.Append(this.txtSyllableStringMinimumLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (syllableStringMaximumLengthIsNumber == false)
            {
                _msg.Append("Invalid maximum syllable string length value: ");
                _msg.Append(this.txtSyllableStringMaximumLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (minRepeatOutputLengthIsNumber == false)
            {
                _msg.Append("Invalid minimum repeat output length value: ");
                _msg.Append(this.txtMinRepeatOutputLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (maxRepeatOutputLengthIsNumber == false)
            {
                _msg.Append("Invalid maximum repeat output length value: ");
                _msg.Append(this.txtMaxRepeatOutputLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (minNumRepeatsIsNumber == false)
            {
                _msg.Append("Invalid minimum number of repeats value: ");
                _msg.Append(this.txtMinNumRepeats.Text);
                _msg.Append(Environment.NewLine);
            }
            if (maxNumRepeatsIsNumber == false)
            {
                _msg.Append("Invalid maximum number of repeats value: ");
                _msg.Append(this.txtMaxNumRepeats.Text);
                _msg.Append(Environment.NewLine);
            }

            if (numStringsMinMaxIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value for number of strings:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtMinNumStrings.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtMaxNumStrings.Text);
                _msg.Append(Environment.NewLine);
            }
            if (stringLengthMinMaxIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value for length of strings:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtStringMinimumLength.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtStringMaximumLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (numSyllableStringsMinMaxIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value for number of syllable strings:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtMinNumSyllableStrings.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtMaxNumSyllableStrings.Text);
                _msg.Append(Environment.NewLine);
            }
            if (syllableStringLengthMinMaxIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value for length of syllable strings:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtSyllableStringMinimumLength.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtSyllableStringMaximumLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (repeatOutputLengthMinMaxIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value for repeat output length:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtMinRepeatOutputLength.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtMaxRepeatOutputLength.Text);
                _msg.Append(Environment.NewLine);
            }
            if (numRepeatsMinMaxIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value for repeat output length:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtMinNumRepeats.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtMaxNumRepeats.Text);
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


        private void FillFormFromRequestDefinition(RandomStringDataRequest reqDef)
        {
            this.txtDataMaskName.Text = reqDef.Name;
            this.optRandomStrings.Checked = reqDef.OutputRandomStrings;
            this.optAN.Checked = reqDef.OutputAN;
            this.optANUC.Checked = reqDef.OutputANUC;
            this.optANLC.Checked = reqDef.OutputANLC;
            this.optANX.Checked = reqDef.OutputANX;
            this.optAL.Checked = reqDef.OutputAL;
            this.optLC.Checked = reqDef.OutputLC;
            this.optUC.Checked = reqDef.OutputUC;
            this.optDEC.Checked = reqDef.OutputDEC;
            this.optHEX.Checked = reqDef.OutputHEX;
            this.txtMinNumStrings.Text = reqDef.MinNumStrings;
            this.txtMaxNumStrings.Text = reqDef.MaxNumStrings;
            this.txtStringMinimumLength.Text = reqDef.StringMinimumLength;
            this.txtStringMaximumLength.Text = reqDef.StringMaximumLength;
            this.cboRegexPattern.Text = reqDef.RegexPattern;
            this.cboRegexReplacement.Text = reqDef.RegexReplacement;
            this.optRandomSyllableStrings.Checked = reqDef.OutputRandomSyllableStrings;
            this.optSyllableUCLC.Checked = reqDef.OutputSyllableUCLC;
            this.optSyllableLC.Checked = reqDef.OutputSyllableLC;
            this.optSyllableUC.Checked = reqDef.OutputSyllableUC;
            this.txtMinNumSyllableStrings.Text = reqDef.MinNumSyllableStrings;
            this.txtMaxNumSyllableStrings.Text = reqDef.MaxNumSyllableStrings;
            this.txtSyllableStringMinimumLength.Text = reqDef.SyllableStringMinimumLength;
            this.txtSyllableStringMaximumLength.Text = reqDef.SyllableStringMaximumLength;
            this.optRepeatingStrings.Checked = reqDef.OutputRepeatingStrings;
            this.optRepeatRandomCharacter.Checked = reqDef.OutputRepeatRandomCharacter;
            this.optRepeatAN.Checked = reqDef.OutputRepeatAN;
            this.optRepeatANUC.Checked = reqDef.OutputRepeatANUC;
            this.optRepeatANLC.Checked = reqDef.OutputRepeatANLC;
            this.optRepeatANX.Checked = reqDef.OutputRepeatANX;
            this.optRepeatAL.Checked = reqDef.OutputRepeatAL;
            this.optRepeatLC.Checked = reqDef.OutputRepeatLC;
            this.optRepeatUC.Checked = reqDef.OutputRepeatUC;
            this.optRepeatDEC.Checked = reqDef.OutputRepeatDEC;
            this.optRepeatHEX.Checked = reqDef.OutputRepeatHEX;
            this.txtMinRepeatOutputLength.Text = reqDef.MinRepeatOutputLength;
            this.txtMaxRepeatOutputLength.Text = reqDef.MaxRepeatOutputLength;
            this.optRepeatText.Checked = reqDef.OutputRepeatText;
            this.txtTextToRepeat.Text = reqDef.TextToRepeat;
            this.txtMinNumRepeats.Text = reqDef.MinNumRepeats;
            this.txtMaxNumRepeats.Text = reqDef.MaxNumRepeats;
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

                if (this.optRandomStrings.Checked)
                {
                    PreviewRandomStrings();
                }
                else if (this.optRandomSyllableStrings.Checked)
                {
                    PreviewRandomSyllableStrings();
                }
                else if (this.optRepeatingStrings.Checked)
                {
                    if (this.optRepeatRandomCharacter.Checked)
                    {
                        PreviewRandomRepeatingCharacter();
                    }
                    else
                    {
                        PreviewRandomRepeatingText();
                    }
                }
                else
                {
                    ;  //do nothing
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

        private void PreviewRandomStrings()
        {
            DataTable dt = null;
            RandomStringDataTable rndt = new RandomStringDataTable();
            enRandomStringType randStringType = enRandomStringType.enUnknown;

            try
            {
                randStringType = GetRandomStringTypeFromForm();
                if (randStringType != enRandomStringType.enUnknown)
                {
                    dt = rndt.CreateRandomStringsDataTable(randStringType, Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinNumStrings.Text, this.txtMaxNumStrings.Text, this.txtStringMinimumLength.Text, this.txtStringMaximumLength.Text, this.cboRegexPattern.Text, this.cboRegexReplacement.Text);
                    OutputDataTableToGrid(dt);
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

        private enRandomStringType GetRandomStringTypeFromForm()
        {
            enRandomStringType randStringType = enRandomStringType.enUnknown;

            if (this.optAN.Checked)
                randStringType = enRandomStringType.enAN;
            else if (this.optANUC.Checked)
                randStringType = enRandomStringType.enANUC;
            else if (this.optANLC.Checked)
                randStringType = enRandomStringType.enANLC;
            else if (this.optANX.Checked)
                randStringType = enRandomStringType.enANX;
            else if (this.optAL.Checked)
                randStringType = enRandomStringType.enAL;
            else if (this.optLC.Checked)
                randStringType = enRandomStringType.enLC;
            else if (this.optUC.Checked)
                randStringType = enRandomStringType.enUC;
            else if (this.optDEC.Checked)
                randStringType = enRandomStringType.enDEC;
            else if (this.optHEX.Checked)
                randStringType = enRandomStringType.enHEX;
            else
                randStringType = enRandomStringType.enUnknown;

            return randStringType;
        }

        private void PreviewRandomSyllableStrings()
        {
            DataTable dt = null;
            RandomStringDataTable rndt = new RandomStringDataTable();
            enRandomSyllableStringType randSyllableStringType = enRandomSyllableStringType.enUnknown;

            try
            {
                randSyllableStringType = GetRandomSyllableStringTypeFromForm();
                if (randSyllableStringType != enRandomSyllableStringType.enUnknown)
                {
                    dt = rndt.CreateRandomSyllableStringsDataTable(randSyllableStringType, Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinNumSyllableStrings.Text, this.txtMaxNumSyllableStrings.Text, this.txtSyllableStringMinimumLength.Text, this.txtSyllableStringMaximumLength.Text);
                    OutputDataTableToGrid(dt);
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

        private enRandomSyllableStringType GetRandomSyllableStringTypeFromForm()
        {
            enRandomSyllableStringType randSyllableStringType = enRandomSyllableStringType.enUnknown;

            if (this.optSyllableUCLC.Checked)
                randSyllableStringType = enRandomSyllableStringType.enUCLC;
            else if (this.optSyllableLC.Checked)
                randSyllableStringType = enRandomSyllableStringType.enLC;
            else if (this.optSyllableUC.Checked)
                randSyllableStringType = enRandomSyllableStringType.enUC;
            else
                randSyllableStringType = enRandomSyllableStringType.enUnknown;


            return randSyllableStringType;
        }

        private void PreviewRandomRepeatingCharacter()
        {
            DataTable dt = null;
            RandomStringDataTable rndt = new RandomStringDataTable();
            enRandomStringType randStringType = enRandomStringType.enUnknown;

            try
            {
                randStringType = GetRandomRepeatingStringTypeFromForm();
                if (randStringType != enRandomStringType.enUnknown)
                {
                    dt = rndt.CreateRandomRepeatingCharacterDataTable(randStringType, Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinRepeatOutputLength.Text, this.txtMaxRepeatOutputLength.Text, this.txtMinNumRepeats.Text, this.txtMaxNumRepeats.Text);
                    OutputDataTableToGrid(dt);
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

        private enRandomStringType GetRandomRepeatingStringTypeFromForm()
        {
            enRandomStringType randStringType = enRandomStringType.enUnknown;

            if (this.optRepeatAN.Checked)
                randStringType = enRandomStringType.enAN;
            else if (this.optRepeatANUC.Checked)
                randStringType = enRandomStringType.enANUC;
            else if (this.optRepeatANLC.Checked)
                randStringType = enRandomStringType.enANLC;
            else if (this.optRepeatANX.Checked)
                randStringType = enRandomStringType.enANX;
            else if (this.optRepeatAL.Checked)
                randStringType = enRandomStringType.enAL;
            else if (this.optRepeatLC.Checked)
                randStringType = enRandomStringType.enLC;
            else if (this.optRepeatUC.Checked)
                randStringType = enRandomStringType.enUC;
            else if (this.optRepeatDEC.Checked)
                randStringType = enRandomStringType.enDEC;
            else if (this.optRepeatHEX.Checked)
                randStringType = enRandomStringType.enHEX;
            else
                randStringType = enRandomStringType.enUnknown;

            return randStringType;
        }

        private void PreviewRandomRepeatingText()
        {
            DataTable dt = null;
            RandomStringDataTable rndt = new RandomStringDataTable();

            if (this.txtTextToRepeat.Text.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must specify some text to repeat.");
                AppMessages.DisplayErrorMessage(_msg.ToString());
                return;
            }

            try
            {
                dt = rndt.CreateRepeatingTextDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtTextToRepeat.Text, this.txtMinNumRepeats.Text, this.txtMaxNumRepeats.Text);
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
            if (Directory.Exists(_randomStringOriginalDataRequestsFolder))
            {
                foreach (string sourceFilePath in Directory.GetFiles(_randomStringOriginalDataRequestsFolder))
                {
                    string destFilePath = Path.Combine(_randomStringDataRequestFolder, Path.GetFileName(sourceFilePath));
                    File.Copy(sourceFilePath, destFilePath, true);
                }
            }
            //force a save prompt of existing data on the form if user exits without saving after this routine is finished
            RandomStringDataRequest reqDef = new RandomStringDataRequest();
            InitRequestDefObject(ref reqDef);
            _saveRequestDef = reqDef;

        }

#pragma warning restore 1591

    }//end class
}//end namespace
