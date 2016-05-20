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
    /// Form for defining and previewing random booleans.
    /// </summary>
    public partial class RandomBooleansForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _randomBooleansDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\Booleans\";
        private string _randomBooleansOriginalDataRequestsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\OriginalDefinitions\Booleans\";

        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Booleans\";
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Booleans\";
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
        private RandomBooleanDataRequest _saveRequestDef = null;
        private RandomBooleanDataRequest _exitRequestDef = null;


        //constructors

        public RandomBooleansForm()
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

        private void cmdRecalcTrueValuePercentages_Click(object sender, EventArgs e)
        {
            RecalcTrueValuePercentages();
        }

        private void optBooleanOutput_CheckedChanged(object sender, EventArgs e)
        {
            BooleanOutputCheckedChanged();
        }

        private void optInteger_CheckedChanged(object sender, EventArgs e)
        {
            IntegerCheckedChanged();
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
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Random Booleans");
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
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry_RandomBooleansForm", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder_RandomBooleansForm", @"PFApps\RandomBooleansForm\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey_RandomBooleansForm", @"SOFTWARE\PFApps\RandomBooleansForm");
            _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries_RandomBooleansForm", (int)6);
            _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList_RandomBooleansForm", "true");

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
            string randomBooleansDataRequestFolder = string.Empty;
            string randomBooleansOriginalDataRequestFolder = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomDataRequestFolder = configValue;
            else
                randomDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            configValue = AppConfig.GetStringValueFromConfigFile("RandomBooleansDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomBooleansDataRequestFolder = configValue;
            else
                randomBooleansDataRequestFolder = @"\PFApps\Randomizer\Definitions\Booleans\";

            configValue = AppConfig.GetStringValueFromConfigFile("RandomBooleansOriginalDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomBooleansOriginalDataRequestFolder = configValue;
            else
                randomBooleansOriginalDataRequestFolder = @"\PFApps\Randomizer\OriginalDefinitions\Booleans\";

            _randomBooleansDataRequestFolder = randomDataRequestFolder + randomBooleansDataRequestFolder;
            _randomBooleansOriginalDataRequestsFolder = randomDataRequestFolder + randomBooleansOriginalDataRequestFolder;

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
            this.Text = "Define Boolean Data Mask";
            this.optBooleanOutput.Checked = true;
            this.txtPercentOutputValuesAsTrue.Text = "50";
            this.txtPercentOutputValuesAsFalse.Text = "50";
            this.txtNumericTrueValue.Text = "1";
            this.txtNumericFalseValue.Text = "0";
            this.optInteger.Checked = true;
            this.opt32bit.Checked = true;
            this.optSignedInteger.Checked = true;
            IntegerCheckedChanged();
            this.txtStringTrueValue.Text  = "True";
            this.txtStringFalseValue.Text = "False";
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

        private void IntegerCheckedChanged()
        {
            if (optInteger.Checked)
            {
                this.pnlSignedUnsigned.Enabled = true;
                this.pnlNumberOfBits.Enabled = true;
            }
            else
            {
                this.pnlSignedUnsigned.Enabled = false;
                this.pnlNumberOfBits.Enabled = false;
            }
        }

        private void BooleanOutputCheckedChanged()
        {
            if (this.optBooleanOutput.Checked)
            {
                this.pnlNumericType.Enabled = false;
                this.pnlStringType.Enabled = false;
            }
            else if (this.optNumericOuput.Checked)
            {
                this.pnlNumericType.Enabled = true;
                this.pnlStringType.Enabled = false;
            }
            else
            {
                //this.optStringOutput.checked
                this.pnlNumericType.Enabled = false;
                this.pnlStringType.Enabled = true;
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
                frm.SourceFolder = _randomBooleansDataRequestFolder;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    requestName = frm.lstNames.SelectedItem.ToString();
                    if (requestName.Length > 0)
                    {
                        filePath = Path.Combine(_randomBooleansDataRequestFolder, requestName + ".xml");
                        RandomBooleanDataRequest reqDef = RandomBooleanDataRequest.LoadFromXmlFile(filePath);
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
                RandomBooleanDataRequest req = CreateRequestDef(true);
                if (req.Name.Length == 0)
                {
                    //error occurred in CreateRequestDef: probably a validation error
                    saveSucceeded = false;
                    return false;
                }
                string filename = Path.Combine(_randomBooleansDataRequestFolder, req.Name + ".xml");
                if (File.Exists(filename))
                {
                    _msg.Length = 0;
                    _msg.Append("Random booleans data request ");
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

            string filePath = Path.Combine(_randomBooleansDataRequestFolder, filename + ".xml");

            if (File.Exists(filePath))
            {
                //process it
                RandomBooleanDataRequest reqdef = RandomBooleanDataRequest.LoadFromXmlFile(filePath);
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
                frm.SourceFolder = _randomBooleansDataRequestFolder;
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
            this.txtPercentOutputValuesAsTrue.Text = "50";
            this.txtPercentOutputValuesAsFalse.Text = "50";
            this.optBooleanOutput.Checked = true;
            this.optInteger.Checked = false;
            this.optSignedInteger.Checked = true;
            this.opt32bit.Checked = true;
            this.txtStringTrueValue.Text = "True";
            this.txtStringFalseValue.Text = "False";
            this.txtNumRandomDataItems.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");
        }

        private void InitRequestDefObject(ref RandomBooleanDataRequest reqDef)
        {
            reqDef.Name = string.Empty;
            reqDef.PercentOutputValuesAsTrue = 50.0;
            reqDef.PercentOutputValuesAsFalse = 50.0;
            reqDef.BooleanOutput = false;
            reqDef.NumericOutput = false;
            reqDef.NumericTrueValue = "1";
            reqDef.NumericFalseValue = "0";
            reqDef.OutputIntegerValue = true;
            reqDef.OutputDoubleValue = false;
            reqDef.OutputFloatValue = false;
            reqDef.OutputDecimalValue = false;
            reqDef.OutputSignedInteger = true;
            reqDef.OutputUnsignedInteger = false;
            reqDef.Output64bitInteger = false;
            reqDef.Output32bitInteger = true;
            reqDef.Output16bitInteger = false;
            reqDef.Output8bitInteger = false;
            reqDef.StringOutput = false;
            reqDef.StringTrueValue = "True";
            reqDef.StringFalseValue = "False";
            reqDef.NumRandomDataItems = 1000;
        }

        private RandomBooleanDataRequest CreateRequestDef(bool verifyNumbers)
        {
            RandomBooleanDataRequest reqDef = new RandomBooleanDataRequest();
            double num = 0.0;

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
            reqDef.PercentOutputValuesAsTrue = 50.0;
            reqDef.PercentOutputValuesAsFalse = 50.0;
            if(double.TryParse(this.txtPercentOutputValuesAsTrue.Text, out num))
                reqDef.PercentOutputValuesAsTrue = Convert.ToDouble(this.txtPercentOutputValuesAsTrue.Text);
            if (double.TryParse(this.txtPercentOutputValuesAsFalse.Text, out num))
                reqDef.PercentOutputValuesAsFalse = Convert.ToDouble(this.txtPercentOutputValuesAsFalse.Text);
            reqDef.BooleanOutput = this.optBooleanOutput.Checked;
            reqDef.NumericOutput = this.optNumericOuput.Checked;
            reqDef.NumericTrueValue = this.txtNumericTrueValue.Text;
            reqDef.NumericFalseValue = this.txtNumericFalseValue.Text;
            reqDef.OutputIntegerValue = this.optInteger.Checked;
            reqDef.OutputDoubleValue = this.optDouble.Checked;
            reqDef.OutputFloatValue = this.optFloat.Checked;
            reqDef.OutputDecimalValue = this.optDecimal.Checked;
            reqDef.OutputSignedInteger = this.optSignedInteger.Checked;
            reqDef.OutputUnsignedInteger = this.optUnsignedInteger.Checked;
            reqDef.Output64bitInteger = this.opt64bit.Checked;
            reqDef.Output32bitInteger = this.opt32bit.Checked;
            reqDef.Output16bitInteger = this.opt16bit.Checked;
            reqDef.Output8bitInteger = this.opt8bit.Checked;
            reqDef.StringOutput = this.optStringOutput.Checked;
            reqDef.StringTrueValue = this.txtStringTrueValue.Text;
            reqDef.StringFalseValue = this.txtStringFalseValue.Text;
            reqDef.NumRandomDataItems = PFTextProcessor.ConvertStringToInt(this.txtNumRandomDataItems.Text, 1000);

            return reqDef;
        }

        public string VerifyNumericInput()
        {
            bool numericTrueValueIsNumber = true;
            bool numericFalseValueIsNumber = true;
            bool trueOutputPercentIsNumber = true;
            bool falseOutputPercentIsNumber = true;
            bool numRandomDataItemsIsNumber = true;
            int intNum = 0;

            _msg.Length = 0;


            double pctTrue = 0;
            double pctFalse = 0;
            trueOutputPercentIsNumber = Double.TryParse(this.txtPercentOutputValuesAsTrue.Text, out pctTrue);
            falseOutputPercentIsNumber = Double.TryParse(this.txtPercentOutputValuesAsFalse.Text, out pctFalse);

            if (this.optNumericOuput.Checked)
            {
                if (optInteger.Checked)
                {
                    if (opt64bit.Checked)
                    {
                        if (optSignedInteger.Checked)
                        {
                            long num = 0;
                            numericTrueValueIsNumber = long.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = long.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                        else //optUnsignedInteger.Checked
                        {
                            ulong num = 0;
                            numericTrueValueIsNumber = ulong.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = ulong.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                    }
                    else if (opt32bit.Checked)
                    {
                        if (optSignedInteger.Checked)
                        {
                            int num = 0;
                            numericTrueValueIsNumber = int.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = int.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                        else //optUnsignedInteger.Checked
                        {
                            uint num = 0;
                            numericTrueValueIsNumber = uint.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = uint.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                    }
                    else if (opt16bit.Checked)
                    {
                        if (optSignedInteger.Checked)
                        {
                            short num = 0;
                            numericTrueValueIsNumber = short.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = short.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                        else //optUnsignedInteger.Checked
                        {
                            ushort num = 0;
                            numericTrueValueIsNumber = ushort.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = ushort.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                    }
                    else //opt8bit.Checked
                    {
                        if (optSignedInteger.Checked)
                        {
                            sbyte num = 0;
                            numericTrueValueIsNumber = sbyte.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = sbyte.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                        else //optUnsignedInteger.Checked
                        {
                            byte num = 0;
                            numericTrueValueIsNumber = byte.TryParse(this.txtNumericTrueValue.Text, out num);
                            numericFalseValueIsNumber = byte.TryParse(this.txtNumericFalseValue.Text, out num);
                        }
                    }
                }
                else if (optDouble.Checked)
                {
                    double num = 0;
                    numericTrueValueIsNumber = double.TryParse(this.txtNumericTrueValue.Text, out num);
                    numericFalseValueIsNumber = double.TryParse(this.txtNumericFalseValue.Text, out num);
                }
                else if (optFloat.Checked)
                {
                    float num = 0;
                    numericTrueValueIsNumber = float.TryParse(this.txtNumericTrueValue.Text, out num);
                    numericFalseValueIsNumber = float.TryParse(this.txtNumericFalseValue.Text, out num);
                }
                else //optDecimal.Checked
                {
                    decimal num = 0;
                    numericTrueValueIsNumber = decimal.TryParse(this.txtNumericTrueValue.Text, out num);
                    numericFalseValueIsNumber = decimal.TryParse(this.txtNumericFalseValue.Text, out num);
                }
            }//end if (this.optNumericOuput.Checked)

            numRandomDataItemsIsNumber = int.TryParse(this.txtNumRandomDataItems.Text, out intNum);

            if (numericTrueValueIsNumber == false)
            {
                _msg.Append("Invalid numeric true value: ");
                _msg.Append(this.txtNumericTrueValue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (numericFalseValueIsNumber == false)
            {
                _msg.Append("Invalid numeric false value: ");
                _msg.Append(this.txtNumericFalseValue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (trueOutputPercentIsNumber == false)
            {
                _msg.Append("Invalid true output percent: ");
                _msg.Append(this.txtPercentOutputValuesAsTrue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (falseOutputPercentIsNumber == false)
            {
                _msg.Append("Invalid false output percent: ");
                _msg.Append(this.txtPercentOutputValuesAsTrue.Text);
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


        private void FillFormFromRequestDefinition(RandomBooleanDataRequest reqDef)
        {
            this.txtDataMaskName.Text = reqDef.Name;
            this.txtPercentOutputValuesAsTrue.Text = reqDef.PercentOutputValuesAsTrue.ToString();
            this.txtPercentOutputValuesAsFalse.Text = reqDef.PercentOutputValuesAsFalse.ToString();
            this.optBooleanOutput.Checked = reqDef.BooleanOutput;
            this.optNumericOuput.Checked = reqDef.NumericOutput;
            this.txtNumericTrueValue.Text = reqDef.NumericTrueValue;
            this.txtNumericFalseValue.Text = reqDef.NumericFalseValue;
            this.optInteger.Checked = reqDef.OutputIntegerValue;
            this.optDouble.Checked = reqDef.OutputDoubleValue;
            this.optFloat.Checked = reqDef.OutputFloatValue;
            this.optDecimal.Checked = reqDef.OutputDecimalValue;
            this.optSignedInteger.Checked = reqDef.OutputSignedInteger;
            this.optUnsignedInteger.Checked = reqDef.OutputUnsignedInteger;
            this.opt64bit.Checked = reqDef.Output64bitInteger;
            this.opt32bit.Checked = reqDef.Output32bitInteger;
            this.opt16bit.Checked = reqDef.Output16bitInteger;
            this.opt8bit.Checked = reqDef.Output8bitInteger;
            this.optStringOutput.Checked = reqDef.StringOutput;
            this.txtStringTrueValue.Text = reqDef.StringTrueValue;
            this.txtStringFalseValue.Text = reqDef.StringFalseValue;
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

                if (this.optBooleanOutput.Checked)
                {
                    PreviewBooleanOutput();
                }
                else if (this.optNumericOuput.Checked)
                {
                    PreviewNumericOutput();
                }
                else if (this.optStringOutput.Checked)
                {
                    PreviewStringOutput();
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

        private void PreviewBooleanOutput()
        {
            DataTable dt = null;
            RandomBooleanDataTable rndt = new RandomBooleanDataTable();

            try
            {
                dt = rndt.CreateBooleanDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtPercentOutputValuesAsTrue.Text, this.txtPercentOutputValuesAsFalse.Text);
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

        private void PreviewNumericOutput()
        {
            DataTable dt = null;
            RandomBooleanDataTable rndt = new RandomBooleanDataTable();
            enRandomNumberType randNumberType = enRandomNumberType.enUnknown;

            try
            {
                randNumberType = GetRandomNumberTypeFromForm();
                if (randNumberType != enRandomNumberType.enUnknown)
                {
                    dt = rndt.CreateNumericDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtPercentOutputValuesAsTrue.Text, this.txtPercentOutputValuesAsFalse.Text, randNumberType, this.txtNumericTrueValue.Text, this.txtNumericFalseValue.Text);
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

        private enRandomNumberType GetRandomNumberTypeFromForm()
        {
            enRandomNumberType randNumType = enRandomNumberType.enUnknown;
            if (this.optInteger.Checked)
            {
                if (this.opt64bit.Checked)
                {
                    if (this.optSignedInteger.Checked)
                    {
                        randNumType = enRandomNumberType.enLong;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enULong;
                    }
                }
                else if (this.opt32bit.Checked)
                {
                    if (this.optSignedInteger.Checked)
                    {
                        randNumType = enRandomNumberType.enInt;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enUInt;
                    }
                }
                else if (this.opt16bit.Checked)
                {
                    if (this.optSignedInteger.Checked)
                    {
                        randNumType = enRandomNumberType.enShort;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enUShort;
                    }
                }
                else if (this.opt8bit.Checked)
                {
                    if (this.optSignedInteger.Checked)
                    {
                        randNumType = enRandomNumberType.enSByte;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enByte;
                    }
                }
                else
                {
                    randNumType = enRandomNumberType.enUnknown;
                }

            }
            else if (this.optDouble.Checked)
            {
                randNumType = enRandomNumberType.enDouble;
            }
            else if (this.optFloat.Checked)
            {
                randNumType = enRandomNumberType.enFloat;
            }
            else if (this.optDecimal.Checked)
            {
                randNumType = enRandomNumberType.enDecimal;
            }
            else
            {
                randNumType = enRandomNumberType.enUnknown;
            }

            return randNumType;
        }

        private void PreviewStringOutput()
        {
            DataTable dt = null;
            RandomBooleanDataTable rndt = new RandomBooleanDataTable();

            try
            {
                dt = rndt.CreateStringDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtPercentOutputValuesAsTrue.Text, this.txtPercentOutputValuesAsFalse.Text, this.txtStringTrueValue.Text, this.txtStringFalseValue.Text);
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
            if (Directory.Exists(_randomBooleansOriginalDataRequestsFolder))
            {
                foreach (string sourceFilePath in Directory.GetFiles(_randomBooleansOriginalDataRequestsFolder))
                {
                    string destFilePath = Path.Combine(_randomBooleansDataRequestFolder, Path.GetFileName(sourceFilePath));
                    File.Copy(sourceFilePath, destFilePath, true);
                }
            }
            //force a save prompt of existing data on the form if user exits without saving after this routine is finished
            RandomBooleanDataRequest reqDef = new RandomBooleanDataRequest();
            InitRequestDefObject(ref reqDef);
            _saveRequestDef = reqDef;

        }

        private void RecalcTrueValuePercentages()
        {
            double num = 0.0;
            _msg.Length = 0;
            if (double.TryParse(this.txtPercentOutputValuesAsTrue.Text, out num) == false)
            {
                _msg.Append("Invalid Percent Output Values as True: ");
                _msg.Append(this.txtPercentOutputValuesAsTrue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (double.TryParse(this.txtPercentOutputValuesAsFalse.Text, out num) == false)
            {
                _msg.Append("Invalid Percent Output Values as False: ");
                _msg.Append(this.txtPercentOutputValuesAsFalse.Text);
                _msg.Append(Environment.NewLine);
            }
            if (_msg.Length > 0)
            {
                AppMessages.DisplayErrorMessage(_msg.ToString());
                return;
            }

            double truePercent = Convert.ToDouble(this.txtPercentOutputValuesAsTrue.Text);
            double falsePercent = Convert.ToDouble(this.txtPercentOutputValuesAsFalse.Text);
            double totalPercent = truePercent + falsePercent;
            truePercent = (truePercent / totalPercent) * 100.0;
            falsePercent = (falsePercent / totalPercent) * 100.0;

            this.txtPercentOutputValuesAsTrue.Text = truePercent.ToString("##0.0");
            this.txtPercentOutputValuesAsFalse.Text = falsePercent.ToString("##0.0");


        }

#pragma warning restore 1591

    }//end class
}//end namespace
