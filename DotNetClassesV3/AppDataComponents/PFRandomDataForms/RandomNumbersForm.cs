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
    /// Form for definition and preview of random numbers.
    /// </summary>
    public partial class RandomNumbersForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _randomNumericDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\Numbers\";
        private string _randomNumericOriginalDataRequestsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\OriginalDefinitions\Numbers\";

        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Numbers";
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Numbers";
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
        private RandomNumberDataRequest _saveRequestDef = null;
        private RandomNumberDataRequest _exitRequestDef = null;


        //constructors

        public RandomNumbersForm()
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

        private void optInteger_CheckedChanged(object sender, EventArgs e)
        {
            IntegerCheckedChanged();
        }

        private void optRangeOfNumbers_CheckedChanged(object sender, EventArgs e)
        {
            RangeOfNumbersCheckedChanged();
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
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Random Numbers");
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
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry_RandomNumbersForm", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder_RandomNumbersForm", @"PFApps\RandomNumbersForm\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey_RandomNumbersForm", @"SOFTWARE\PFApps\RandomNumbersForm");
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
            string randomNumericDataRequestFolder = string.Empty;
            string randomNumericOriginalDataRequestFolder = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomDataRequestFolder = configValue;
            else
                randomDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            configValue = AppConfig.GetStringValueFromConfigFile("RandomNumericDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomNumericDataRequestFolder = configValue;
            else
                randomNumericDataRequestFolder = @"\PFApps\Randomizer\Definitions\Numbers\";

            configValue = AppConfig.GetStringValueFromConfigFile("RandomNumericOriginalDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomNumericOriginalDataRequestFolder = configValue;
            else
                randomNumericOriginalDataRequestFolder = @"\PFApps\Randomizer\OriginalDefinitions\Numbers\";

            _randomNumericDataRequestFolder = randomDataRequestFolder + randomNumericDataRequestFolder;
            _randomNumericOriginalDataRequestsFolder = randomDataRequestFolder + randomNumericOriginalDataRequestFolder;

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
            this.Text = "Define Numeric Data Mask";

            this.optInteger.Checked = true;
            this.opt32bit.Checked = true;
            this.optSignedInteger.Checked = true;
            IntegerCheckedChanged();

            this.optRangeOfNumbers.Checked = true;
            this.txtMinimumValue.Text = "1";
            this.txtMaximumValue.Text = "1000";
            this.txtMinimumOffsetPercent.Text = "-50";
            this.txtMaximumOffsetPercent.Text = "+50";
            this.txtStartSequentialValue.Text = "1";
            this.txtIncrementForSequentialValue.Text = "1";
            this.txtMaxSequentialValue.Text = "";
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

        private void RangeOfNumbersCheckedChanged()
        {
            if (this.optRangeOfNumbers.Checked)
            {
                this.pnlRangeOfNumbers.Enabled = true;
                this.pnlPlusMinusNumericPercentOffset.Enabled = false;
                this.pnlSequentialNumbers.Enabled = false;
            }
            else if (this.optNumericOffsetFromCurrentNumber.Checked)
            {
                this.pnlRangeOfNumbers.Enabled = false;
                this.pnlPlusMinusNumericPercentOffset.Enabled = true;
                this.pnlSequentialNumbers.Enabled = false;
            }
            else
            {
                this.pnlRangeOfNumbers.Enabled = false;
                this.pnlPlusMinusNumericPercentOffset.Enabled = false;
                this.pnlSequentialNumbers.Enabled = true;
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
                frm.SourceFolder = _randomNumericDataRequestFolder;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    requestName = frm.lstNames.SelectedItem.ToString();
                    if (requestName.Length > 0)
                    {
                        filePath = Path.Combine(_randomNumericDataRequestFolder, requestName + ".xml");
                        RandomNumberDataRequest reqDef = RandomNumberDataRequest.LoadFromXmlFile(filePath);
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
                RandomNumberDataRequest req = CreateRequestDef(true);
                if (req.Name.Length == 0)
                {
                    //error occurred in CreateRequestDef: probably a validation error
                    saveSucceeded = false;
                    return false;
                }
                string filename = Path.Combine(_randomNumericDataRequestFolder, req.Name + ".xml");
                if (File.Exists(filename))
                {
                    _msg.Length = 0;
                    _msg.Append("Random numeric data request ");
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

            string filePath = Path.Combine(_randomNumericDataRequestFolder, filename + ".xml");

            if (File.Exists(filePath))
            {
                //process it
                RandomNumberDataRequest reqdef = RandomNumberDataRequest.LoadFromXmlFile(filePath);
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
                frm.SourceFolder = _randomNumericDataRequestFolder;
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
            this.optInteger.Checked = true;
            this.optSignedInteger.Checked = true;
            this.opt32bit.Checked = true;
            this.optRangeOfNumbers.Checked = true;
            this.txtMinimumValue.Text = string.Empty;
            this.txtMaximumValue.Text = string.Empty;
            this.txtMinimumOffsetPercent.Text = string.Empty;
            this.txtMaximumOffsetPercent.Text = string.Empty;
            this.txtStartSequentialValue.Text = string.Empty;
            this.txtIncrementForSequentialValue.Text = string.Empty;
            this.txtMaxSequentialValue.Text = string.Empty;
            this.txtNumRandomDataItems.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");
        }

        private void InitRequestDefObject(ref RandomNumberDataRequest reqDef)
        {
            reqDef.Name = string.Empty;
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
            reqDef.OutputRangeOfNumbers = true;
            reqDef.MinimumValueForRange = string.Empty;
            reqDef.MaximumValueForRange = string.Empty;
            reqDef.OutputOffsetFromCurrentNumber = false;
            reqDef.MinimumOffsetPercent = string.Empty;
            reqDef.MaximumOffsetPercent = string.Empty;
            reqDef.OutputSequentialNumbers = false;
            reqDef.StartSequentialValue = string.Empty;
            reqDef.IncrementForSequentialValue = string.Empty;
            reqDef.MaxSequentialValue = string.Empty;
            reqDef.InitStartSequentialValue = string.Empty;
            reqDef.NumRandomDataItems = 1000;
        }

        private RandomNumberDataRequest CreateRequestDef(bool verifyNumbers)
        {
            RandomNumberDataRequest reqDef = new RandomNumberDataRequest();

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
            reqDef.OutputRangeOfNumbers = this.optRangeOfNumbers.Checked;
            reqDef.MinimumValueForRange = this.txtMinimumValue.Text;
            reqDef.MaximumValueForRange = this.txtMaximumValue.Text;
            reqDef.OutputOffsetFromCurrentNumber = this.optNumericOffsetFromCurrentNumber.Checked;
            reqDef.MinimumOffsetPercent = this.txtMinimumOffsetPercent.Text;
            reqDef.MaximumOffsetPercent = this.txtMaximumOffsetPercent.Text;
            reqDef.OutputSequentialNumbers = this.optSequentialNumbers.Checked;
            reqDef.StartSequentialValue = this.txtStartSequentialValue.Text;
            reqDef.IncrementForSequentialValue = this.txtIncrementForSequentialValue.Text;
            reqDef.MaxSequentialValue = this.txtMaxSequentialValue.Text;
            reqDef.InitStartSequentialValue = this.txtStartSequentialValue.Text;
            reqDef.NumRandomDataItems = PFTextProcessor.ConvertStringToInt(this.txtNumRandomDataItems.Text, 1000);

            return reqDef;
        }

        public string VerifyNumericInput()
        {
            bool MinimumValueIsNumber = true;
            bool MaximumValueIsNumber = true;
            bool MaximumValueIsValid = true;

            bool MinimumPercentIsNumber = true;
            bool MaximumPercentIsNumber = true;
            bool MaximumPercentIsValid = true;

            bool StartSequentialValueIsNumber = true;
            bool IncrementForSequentialValueIsNumber = true;
            bool MaxSequentialValueIsNumber = true;
            
            bool numRandomDataItemsIsNumber = true;
            int intNum = 0;

            _msg.Length = 0;

            if (optInteger.Checked)
            {
                if (opt64bit.Checked)
                {
                    if (optSignedInteger.Checked)
                    {
                        long num = 0;
                        long min = 0;
                        long max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = long.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = long.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = long.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = long.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = long.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = long.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = long.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                    else //optUnsignedInteger.Checked
                    {
                        ulong num = 0;
                        ulong min = 0;
                        ulong max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = ulong.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = ulong.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = ulong.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = ulong.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = ulong.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = ulong.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = ulong.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                }
                else if (opt32bit.Checked)
                {
                    if (optSignedInteger.Checked)
                    {
                        int num = 0;
                        int min = 0;
                        int max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = int.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = int.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = int.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = int.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = int.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = int.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = int.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                    else //optUnsignedInteger.Checked
                    {
                        uint num = 0;
                        uint min = 0;
                        uint max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = uint.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = uint.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = uint.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = uint.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = uint.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = uint.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = uint.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                }
                else if (opt16bit.Checked)
                {
                    if (optSignedInteger.Checked)
                    {
                        short num = 0;
                        short min = 0;
                        short max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = short.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = short.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = short.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = short.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = short.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = short.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = short.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                    else //optUnsignedInteger.Checked
                    {
                        ushort num = 0;
                        ushort min = 0;
                        ushort max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = ushort.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = ushort.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = ushort.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = ushort.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = ushort.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = ushort.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = ushort.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                }
                else //opt8bit.Checked
                {
                    if (optSignedInteger.Checked)
                    {
                        sbyte num = 0;
                        sbyte min = 0;
                        sbyte max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = sbyte.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = sbyte.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = sbyte.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = sbyte.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = sbyte.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = sbyte.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = sbyte.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                    else //optUnsignedInteger.Checked
                    {
                        byte num = 0;
                        byte min = 0;
                        byte max = 0;
                        if (this.optRangeOfNumbers.Checked)
                        {
                            MinimumValueIsNumber = byte.TryParse(this.txtMinimumValue.Text, out min);
                            MaximumValueIsNumber = byte.TryParse(this.txtMaximumValue.Text, out max);
                            if (MinimumValueIsNumber && MaximumValueIsNumber)
                            {
                                if (min > max)
                                    MaximumValueIsValid = false;
                            }
                        }
                        else if (this.optNumericOffsetFromCurrentNumber.Checked)
                        {
                            MinimumPercentIsNumber = byte.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                            MaximumPercentIsNumber = byte.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                            if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                            {
                                if (min > max)
                                    MaximumPercentIsValid = false;
                            }
                        }
                        else
                        {
                            StartSequentialValueIsNumber = byte.TryParse(this.txtStartSequentialValue.Text, out num);
                            IncrementForSequentialValueIsNumber = byte.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                            MaxSequentialValueIsNumber = byte.TryParse(this.txtMaxSequentialValue.Text, out num);
                        }
                    }
                }
            }
            else if (optDouble.Checked)
            {
                double num = 0;
                double min = 0;
                double max = 0;
                if (this.optRangeOfNumbers.Checked)
                {
                    MinimumValueIsNumber = Double.TryParse(this.txtMinimumValue.Text, out min);
                    MaximumValueIsNumber = Double.TryParse(this.txtMaximumValue.Text, out max);
                    if (MinimumValueIsNumber && MaximumValueIsNumber)
                    {
                        if (min > max)
                            MaximumValueIsValid = false;
                    }
                }
                else if (this.optNumericOffsetFromCurrentNumber.Checked)
                {
                    MinimumPercentIsNumber = double.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                    MaximumPercentIsNumber = double.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                    if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                    {
                        if (min > max)
                            MaximumPercentIsValid = false;
                    }
                }
                else
                {
                    StartSequentialValueIsNumber = double.TryParse(this.txtStartSequentialValue.Text, out num);
                    IncrementForSequentialValueIsNumber = double.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                    MaxSequentialValueIsNumber = double.TryParse(this.txtMaxSequentialValue.Text, out num);
                }
            }
            else if (optFloat.Checked)
            {
                float num = 0;
                float min = 0;
                float max = 0;
                if (this.optRangeOfNumbers.Checked)
                {
                    MinimumValueIsNumber = float.TryParse(this.txtMinimumValue.Text, out min);
                    MaximumValueIsNumber = float.TryParse(this.txtMaximumValue.Text, out max);
                    if (MinimumValueIsNumber && MaximumValueIsNumber)
                    {
                        if (min > max)
                            MaximumValueIsValid = false;
                    }
                }
                else if (this.optNumericOffsetFromCurrentNumber.Checked)
                {
                    MinimumPercentIsNumber = float.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                    MaximumPercentIsNumber = float.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                    if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                    {
                        if (min > max)
                            MaximumPercentIsValid = false;
                    }
                }
                else
                {
                    StartSequentialValueIsNumber = float.TryParse(this.txtStartSequentialValue.Text, out num);
                    IncrementForSequentialValueIsNumber = float.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                    MaxSequentialValueIsNumber = float.TryParse(this.txtMaxSequentialValue.Text, out num);
                }
            }
            else //optDecimal.Checked
            {
                decimal num = 0;
                decimal min = 0;
                decimal max = 0;
                if (this.optRangeOfNumbers.Checked)
                {
                    MinimumValueIsNumber = Decimal.TryParse(this.txtMinimumValue.Text, out min);
                    MaximumValueIsNumber = Decimal.TryParse(this.txtMaximumValue.Text, out max);
                    if (MinimumValueIsNumber && MaximumValueIsNumber)
                    {
                        if (min > max)
                            MaximumValueIsValid = false;
                    }
                }
                else if (this.optNumericOffsetFromCurrentNumber.Checked)
                {
                    MinimumPercentIsNumber = decimal.TryParse(this.txtMinimumOffsetPercent.Text, out min);
                    MaximumPercentIsNumber = decimal.TryParse(this.txtMaximumOffsetPercent.Text, out max);
                    if (MinimumPercentIsNumber && MaximumPercentIsNumber)
                    {
                        if (min > max)
                            MaximumPercentIsValid = false;
                    }
                }
                else
                {
                    StartSequentialValueIsNumber = decimal.TryParse(this.txtStartSequentialValue.Text, out num);
                    IncrementForSequentialValueIsNumber = decimal.TryParse(this.txtIncrementForSequentialValue.Text, out num);
                    MaxSequentialValueIsNumber = decimal.TryParse(this.txtMaxSequentialValue.Text, out num);
                }

            }

            numRandomDataItemsIsNumber = int.TryParse(this.txtNumRandomDataItems.Text, out intNum);

            if (MinimumValueIsNumber == false)
            {
                _msg.Append("Invalid minimum value: ");
                _msg.Append(this.txtMinimumValue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (MaximumValueIsNumber == false)
            {
                _msg.Append("Invalid maximum value: ");
                _msg.Append(this.txtMaximumValue.Text);
                _msg.Append(Environment.NewLine);
            }

            if (MinimumPercentIsNumber == false)
            {
                _msg.Append("Invalid minimum percent for offset value: ");
                _msg.Append(this.txtMinimumOffsetPercent.Text);
                _msg.Append(Environment.NewLine);
            }
            if (MaximumPercentIsNumber == false)
            {
                _msg.Append("Invalid maximum percent for offset value: ");
                _msg.Append(this.txtMaximumOffsetPercent.Text);
                _msg.Append(Environment.NewLine);
            }
            
            if (MaximumValueIsValid == false)
            {
                _msg.Append("Maximum value must be greater than or equal to the minimum value:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtMinimumValue.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtMaximumValue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (MaximumPercentIsValid == false)
            {
                _msg.Append("Maximum percent value must be greater than or equal to the minimum percent value:\r\n");
                _msg.Append("Minimum ");
                _msg.Append(this.txtMinimumOffsetPercent.Text);
                _msg.Append(" is greater than Maximum ");
                _msg.Append(this.txtMaximumOffsetPercent.Text);
                _msg.Append(Environment.NewLine);
            }

            if (StartSequentialValueIsNumber == false)
            {
                _msg.Append("Invalid start sequential value: ");
                _msg.Append(this.txtStartSequentialValue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (IncrementForSequentialValueIsNumber == false)
            {
                _msg.Append("Invalid increment for sequential value: ");
                _msg.Append(this.txtIncrementForSequentialValue.Text);
                _msg.Append(Environment.NewLine);
            }
            if (MaxSequentialValueIsNumber == false)
            {
                //blank txtMaxSequentialValue signifies no end to the sequence
                if (this.txtMaxSequentialValue.Text.Trim().Length > 0)
                {
                    _msg.Append("Invalid max sequential value: ");
                    _msg.Append(this.txtMaxSequentialValue.Text);
                    _msg.Append(Environment.NewLine);
                }
            }
            
            if (numRandomDataItemsIsNumber == false)
            {
                _msg.Append("Invalid number of random data items value: ");
                _msg.Append(this.txtNumRandomDataItems.Text);
                _msg.Append(Environment.NewLine);
            }

            return _msg.ToString();
        }


        private void FillFormFromRequestDefinition(RandomNumberDataRequest reqDef)
        {
            this.txtDataMaskName.Text = reqDef.Name;
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
            this.optRangeOfNumbers.Checked = reqDef.OutputRangeOfNumbers;
            this.txtMinimumValue.Text = reqDef.MinimumValueForRange;
            this.txtMaximumValue.Text = reqDef.MaximumValueForRange;
            this.optNumericOffsetFromCurrentNumber.Checked = reqDef.OutputOffsetFromCurrentNumber;
            this.txtMinimumOffsetPercent.Text = reqDef.MinimumOffsetPercent;
            this.txtMaximumOffsetPercent.Text = reqDef.MaximumOffsetPercent;
            this.optSequentialNumbers.Checked = reqDef.OutputSequentialNumbers;
            this.txtStartSequentialValue.Text = reqDef.StartSequentialValue;
            this.txtIncrementForSequentialValue.Text = reqDef.IncrementForSequentialValue;
            this.txtMaxSequentialValue.Text = reqDef.MaxSequentialValue;
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

                if (this.optRangeOfNumbers.Checked)
                {
                    PreviewNumberRange();
                }
                else if (this.optNumericOffsetFromCurrentNumber.Checked)
                {
                    PreviewNumberOffset();
                }
                else if (this.optSequentialNumbers.Checked)
                {
                    PreviewSequentialNumbers();
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

        private void PreviewNumberRange()
        {
            DataTable dt = null;
            RandomNumberDataTable rndt = new RandomNumberDataTable();
            enRandomNumberType randNumberType = enRandomNumberType.enUnknown;

            try
            {
                randNumberType = GetRandomNumberTypeFromForm();
                if (randNumberType != enRandomNumberType.enUnknown)
                {
                    dt = rndt.CreateRangeDataTable(randNumberType, Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinimumValue.Text, this.txtMaximumValue.Text);
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

        private void PreviewNumberOffset()
        {
            DataTable dt = null;
            RandomNumberDataTable rndt = new RandomNumberDataTable();
            enRandomNumberType randNumberType = enRandomNumberType.enUnknown;

            try
            {
                randNumberType = GetRandomNumberTypeFromForm();
                if (randNumberType != enRandomNumberType.enUnknown)
                {
                    //dt = rndt.CreateOffsetDataTablePreview(randNumberType, Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinimumOffsetPercent.Text, this.txtMaximumOffsetPercent.Text);
                    dt = rndt.CreateOffsetDataTable(randNumberType, Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtMinimumOffsetPercent.Text, this.txtMaximumOffsetPercent.Text);
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

        private void PreviewSequentialNumbers()
        {
            DataTable dt = null;
            RandomNumberDataTable rndt = new RandomNumberDataTable();
            enRandomNumberType randNumberType = enRandomNumberType.enUnknown;

            try
            {
                randNumberType = GetRandomNumberTypeFromForm();
                if (randNumberType != enRandomNumberType.enUnknown)
                {
                    dt = rndt.CreateSequentialNumbersDataTable(randNumberType, Convert.ToInt32(this.txtNumRandomDataItems.Text), this.txtStartSequentialValue.Text, this.txtIncrementForSequentialValue.Text, this.txtMaxSequentialValue.Text, this.txtStartSequentialValue.Text);
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
            if(this.optInteger.Checked)
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
            if (Directory.Exists(_randomNumericOriginalDataRequestsFolder))
            {
                foreach (string sourceFilePath in Directory.GetFiles(_randomNumericOriginalDataRequestsFolder))
                {
                    string destFilePath = Path.Combine(_randomNumericDataRequestFolder, Path.GetFileName(sourceFilePath));
                    File.Copy(sourceFilePath, destFilePath, true);
                }
            }
            //force a save prompt of existing data on the form if user exits without saving after this routine is finished
            RandomNumberDataRequest reqDef = new RandomNumberDataRequest();
            InitRequestDefObject(ref reqDef);
            _saveRequestDef = reqDef;

        }


#pragma warning restore 1591
    
    }//end class
}//end namespace
