﻿using System;
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

namespace pfDataExtractorCP
{
#pragma warning disable 1591
    /// <summary>
    /// Form for definition of number of test data rows to generate.
    /// </summary>
    public partial class TestDataGeneratorForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "Text Files|*.txt|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = true;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        //fields for properties
        MessageLog _messageLog = null;

        //constructors

        public TestDataGeneratorForm()
        {
            InitializeComponent();
        }

        //delegates
#pragma warning disable 1591
        public delegate void RandomizerFormDelegate();
        public event RandomizerFormDelegate showRandomizerForm;

        public delegate void PreviewTestDataDelegate(int numPreviewRows);
        public event PreviewTestDataDelegate runTestDataPreview;

        public delegate void GenerateTestDataDelegate(int numRowsToGenerate);
        public event GenerateTestDataDelegate runTestDataGenerator;
#pragma warning restore 1591


        //properties

        /// <summary>
        /// MessageLog with user interface object.
        /// </summary>
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

        /// <summary>
        /// SourceDataSchemaFrom Property.
        /// </summary>
        public string SourceDataSchemaFrom
        {
            get
            {
                return this.txtSourceDataSchemaFrom.Text;
            }
            set
            {
                this.txtSourceDataSchemaFrom.Text = value;
            }
        }

        /// <summary>
        /// DestinationDataOutputTo Property.
        /// </summary>
        public string DestinationDataOutputTo
        {
            get
            {
                return this.txtDestinationDataOutputTo.Text;
            }
            set
            {
                this.txtDestinationDataOutputTo.Text = value;
            }
        }

        /// <summary>
        /// NumRowsToGenerate Property.
        /// </summary>
        public string NumRowsToGenerate
        {
            get
            {
                return this.txtNumRowsToGenerate.Text;
            }
            set
            {
                this.txtNumRowsToGenerate.Text = value;
            }
        }

        /// <summary>
        /// NumRowsToPreview Property.
        /// </summary>
        public string NumRowsToPreview
        {
            get
            {
                return this.txtNumRowsToPreview.Text;
            }
            set
            {
                this.txtNumRowsToPreview.Text = value;
            }
        }



        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            HideForm();

            //DialogResult res = CheckCancelRequest();
            //if (res == DialogResult.Yes)
            //{
            //    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //}
            //else
            //{
            //    this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            //}
        }

        private void cmdGenerateTestData_Click(object sender, EventArgs e)
        {
            GenerateTestData();
        }

        private void cmdPreviewTestData_Click(object sender, EventArgs e)
        {
            PreviewTestData();
        }

        private void cmdRunRandomizer_Click(object sender, EventArgs e)
        {
            RunRandomizer();
        }

        //Menu item clicks
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

        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            ShowHelpAbout();
        }

        private void toolbarHelp_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
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
                        //    this.GenerateTestData();
                        //}
                        //if (sourceControl.Name == "chkShowHelpAboutTest")
                        //{
                        //    this.chkShowHelpAboutTest.Checked = true;
                        //    this.GenerateTestData();
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

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Generate Test Data Overview");
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // && _userCancelButtonPressed == false)
            {
                DialogResult res = CheckCancelRequest();
                if (res == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    this.DialogResult = DialogResult.Ignore;
                }
                else
                {
                    ;
                }
            }
            else
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.Ignore)
                {
                    e.Cancel = true;
                }
                else
                {
                    ;
                }
            }

        }

        private DialogResult CheckCancelRequest()
        {
            DialogResult res = AppMessages.DisplayMessage("Click yes to cancel and discard any changes you may have made to the column specifications.", "Cancel Data Randomizer Criteria ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {

                this.Text = "Test Data Generator";

                SetLoggingValues();

                SetHelpFileValues();

                SetFormValues();

                _printer = new FormPrinter(this);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }


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
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "pfFolderSize.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        private void SetFormValues()
        {
            if (this.txtNumRowsToGenerate.Text.Trim().Length == 0)
                this.txtNumRowsToGenerate.Text = "10000";
            if (this.txtNumRowsToPreview.Text.Trim().Length == 0)
                this.txtNumRowsToPreview.Text = "1000";
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
            _printer.PageSubTitle = "Test Data Generator Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }


        //application routines

        private void GenerateTestData()
        {
            int numRowsToGenerate = 10000;

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                numRowsToGenerate = AppTextGlobals.ConvertStringToInt(this.txtNumRowsToGenerate.Text, 10000);

                runTestDataGenerator(numRowsToGenerate);


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

        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }

        private void RunRandomizer()
        {
            if (this.showRandomizerForm != null)
            {
                showRandomizerForm();
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("No callback for showing of randomizer form was specified.");
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
        }

        private void PreviewTestData()
        {
            int numPreviewRows = 1000;

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                numPreviewRows = AppTextGlobals.ConvertStringToInt(this.txtNumRowsToPreview.Text, 1000);

                runTestDataPreview(numPreviewRows);

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




    }//end class
#pragma warning restore 1591
}//end namespace
