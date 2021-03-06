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
using PFFileSystemObjects;
using PFTextFiles;
using PFPrinterObjects;

namespace InitWinFormsAppWithToolbar
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        PFTextPrinter _printer = new PFTextPrinter();

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
        private string _mRUListSaveFileSubFolder = @"PFApps\InitWinFormsAppWithToolbar\Mru\";
        private string _mRUListSaveRegistryKey = @"SOFTWARE\PFApps\InitWinFormsAppWithToolbar";
        private int _maxMruListEntries = 4;
        private bool _useSubMenuForMruList = true;
        

        public MainForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdRunTest_Click(object sender, EventArgs e)
        {
            RunTests();
        }

        private void cmdShowHideOutputLog_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
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

        private void mnuFileRename_Click(object sender, EventArgs e)
        {
            FileRename();
        }

        private void mnuFileDelete_Click(object sender, EventArgs e)
        {
            FileDelete();
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

        private void mnuEditFind_Click(object sender, EventArgs e)
        {
            EditFind();
        }

        private void mnuToolsOptions_Click(object sender, EventArgs e)
        {
            ShowToolsOptions();
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
                        foreach (Control ctl in this.grpTestsToRun.Controls)
                        {
                            CheckBox chk = null;
                            if (ctl is CheckBox)
                            {
                                chk = (CheckBox)ctl;
                                chk.Checked = false;
                            }
                        }
                        if (sourceControl.Name == "chkShowDateTimeTest")
                        {
                            this.chkShowDateTimeTest.Checked = true;
                            this.RunTests();
                        }
                        if (sourceControl.Name == "chkShowHelpAboutTest")
                        {
                            this.chkShowHelpAboutTest.Checked = true;
                            this.RunTests();
                        }
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

        private void ShowToolsOptions()
        {
            ApplicationOptionsForm appOptionsForm = new ApplicationOptionsForm();

            try
            {
                appOptionsForm.ShowDialog();
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
                appOptionsForm.Close();
                appOptionsForm = null;
            }

        }

        private void ShowHelpContents()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.TableOfContents);
        }

        private void ShowHelpIndex()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.Index);
        }

        private void ShowHelpSearch()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.Find, "");
        }

        private void ShowHelpTutorial()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Tutorial");
        }

        private void ShowHelpContact()
        {
            Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Contact Information");
        }

        private void ShowHelpAbout()
        {
            HelpAboutForm appHelpAboutForm = new HelpAboutForm();
            appHelpAboutForm.ShowDialog();

        }


        private void MainFormToolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ProcessMainToolbarClick(sender, e);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                this.Text = AppInfo.AssemblyProduct;

                configValue = AppGlobals.AppConfig.GetConfigValue("SaveErrorMessagesToErrorLog");
                if (configValue.ToUpper() == "TRUE")
                    _saveErrorMessagesToAppLog = true;
                else
                    _saveErrorMessagesToAppLog = false;
                _appLogFileName = AppGlobals.AppConfig.GetConfigValue("AppLogFileName");

                if (_appLogFileName.Trim().Length > 0)
                    AppGlobals.AppMessages.AppLogFilename = _appLogFileName;

                string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
                string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "InitWinFormsHelpFile.chm");
                string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
                this.appHelpProvider.HelpNamespace = helpFilePath;
                _helpFilePath = helpFilePath;

                this.chkEraseOutputBeforeEachTest.Checked = true;

                _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry", "True");
                _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder", @"PFApps\InitWinFormsAppWithToolbar\Mru\");
                _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey", @"SOFTWARE\PFApps\InitWinFormsAppWithToolbar");
                _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries", (int)4);
                _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList", "true");

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

            
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
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
                case "Cut":
                    EditCut();
                    break;
                case "Copy":
                    EditCopy();
                    break;
                case "Paste":
                    EditPaste();
                    break;
                case "Find":
                    EditFind();
                    break;
                case "Help":
                    Help.ShowHelp(this, _helpFilePath, HelpNavigator.TableOfContents);
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
            _msg.Length = 0;
            _msg.Append("FileNew not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void FileOpen()
        {
            ShowOpenFileDialog();
        }

        private void FileClose()
        {
            _msg.Length = 0;
            _msg.Append("FileClose not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void FileSave()
        {
            DialogResult res = ShowSaveFileDialog();
            if(res==DialogResult.OK)
                UpdateMruList(_saveSelectionsFile);
        }

        private void FileSaveAs()
        {
            ShowSaveFileDialog();
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
                _saveSelectionsFile =mainMenuOpenFileDialog.FileName;
                _saveFilterIndex = mainMenuOpenFileDialog.FilterIndex;
                if (mainMenuOpenFileDialog.Multiselect)
                {
                    _saveSelectedFiles = mainMenuOpenFileDialog.FileNames;
                    if(_saveSelectedFiles.Length>0)
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

            _printer.Title = this.Text;
            _printer.ShowPageNumbers = true;
            _printer.Font = this.Font;
            _printer.TextToPrint = "Testing the print function.\nThis is the second line.";
            if (preview)
            {
                _printer.ShowPrintPreview();
            }
            else
            {
                if (showPrintDialog)
                {
                    _printer.ShowPrintDialog();
                }
                else
                {
                    _printer.Print();
                }
            }

        }


        private void OnMruFile(int number, String filename)
        {
            _msg.Length = 0;
            _msg.Append("File # ");
            _msg.Append(number.ToString("#,##0"));
            _msg.Append(" selected from Mru List: ");
            _msg.Append(filename);
            _msg.Append(".\r\n");

            //TODO:
            //if file exists, process it
            //if not, show error message and remove from list
            if (File.Exists(filename))
            {
                //process it
                _msg.Append("File exists. It will be processed.");
                AppMessages.DisplayInfoMessage(_msg.ToString());
            }
            else
            {
                _msg.Append("File does not exist. It will be removed from the Mru List");
                _msm.RemoveFile(number);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

        }//end method

        private void FileRename()
        {
            _msg.Length = 0;
            _msg.Append("FileRename not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void FileDelete()
        {
            _msg.Length = 0;
            _msg.Append("FileDelete not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void EditCut()
        {
            _msg.Length = 0;
            _msg.Append("EditCut not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void EditCopy()
        {
            _msg.Length = 0;
            _msg.Append("EditCopy not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void EditPaste()
        {
            _msg.Length = 0;
            _msg.Append("EditPaste not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void EditSelectAll()
        {
            _msg.Length = 0;
            _msg.Append("EditSelectAll not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void EditDelete()
        {
            _msg.Length = 0;
            _msg.Append("EditDelete not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }

        private void EditFind()
        {
            _msg.Length = 0;
            _msg.Append("EditFind not yet implemented");
            AppMessages.DisplayWarningMessage(_msg.ToString());
        }


        //application routines

        private void RunTests()
        {

            int numTestsSelected = 0;

            try
            {
                DisableFormControls();
                Tests.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (this.chkShowDateTimeTest.Checked)
                {
                    numTestsSelected++;
                    Tests.RunShowDateTimeTest(this.chkShowDateTimeTest.Text);
                }

                if (this.chkShowHelpAboutTest.Checked)
                {
                    numTestsSelected++;
                    Tests.ShowHelpAboutTest(this.chkShowHelpAboutTest.Text);
                }

                if (this.chkShowFolderBrowserDialog.Checked)
                {
                    numTestsSelected++;
                    ShowFolderBrowserDialog();
                    _msg.Length = 0;
                    _msg.Append("Selected folder: ");
                    _msg.Append(_saveSelectionsFolder);
                    Program._messageLog.WriteLine(_msg.ToString());
                }

                if (this.chkGetStaticKeysTest.Checked)
                {
                    numTestsSelected++;
                    Tests.GetStaticKeys();
                }


                if (numTestsSelected == 0)
                {
                    AppMessages.DisplayInfoMessage("No tests selected ...", false);
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
                EnableFormControls();
                this.Cursor = Cursors.Default;
                _msg.Length = 0;
                _msg.Append("\r\n");
                _msg.Append("Number of tests run: ");
                _msg.Append(numTestsSelected.ToString("#,##0"));
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
            }



        }



    }//end class
}//end namespace
