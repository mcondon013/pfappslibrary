using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using AppGlobals;
using PFMessageLogs;
using PFFileSystemObjects;
//using PFTextFiles;
using PFPrinterObjects;
using PFAppDataObjects;
using PFTextObjects;
using PFTextFiles;
using KellermanSoftware.CompareNetObjects;

namespace PFAppDataForms
{
#pragma warning disable 1591
    /// <summary>
    /// Form class for defining fixed len texs line column widths.
    /// </summary>
    public partial class PFFixedLenColDefsInputForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _colNumFormat = "0";
        //subitem indexes for various fields shown in the listview
        private int _fieldNameInx = 1;
        private int _columnNameInx = 2;
        private int _columnLengthInx = 3;
        private int _colummAlignmentInx = 4;

        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "Text Files|*.txt|All Files|*.*";
        private int _saveFilterIndex = 1;
        //private bool _showCreatePrompt = true;
        //private bool _showOverwritePrompt = true;
        //private bool _showNewFolderButton = true;

        //fields for properties
        MessageLog _messageLog = null;

        private string _defaultSourceFolder = string.Empty;
        private string _fixedLengthTextFilePath = string.Empty;
        private bool _lineTerminatorUsed = true;
        private string _lineTerminatorChars = Environment.NewLine;
        private bool _columnNamesOnFirstLine = true;
        private int _expectedLineLength = -1;

        private PFColumnDefinitionsExt _colDefs = null;

        //fields for checking for form changes on exit processing
        private PFColumnDefinitionsExt _saveColDef = null;
        private PFColumnDefinitionsExt _exitColDef = null;


        //constructors

        public PFFixedLenColDefsInputForm()
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

        /// <summary>
        /// DefaultSourceFolder Property.
        /// </summary>
        public string DefaultSourceFolder
        {
            get
            {
                return _defaultSourceFolder;
            }
            set
            {
                _defaultSourceFolder = value;
            }
        }

        /// <summary>
        /// FixedLengthTextFilePath Property.
        /// </summary>
        public string FixedLengthTextFilePath
        {
            get
            {
                return _fixedLengthTextFilePath;
            }
            set
            {
                _fixedLengthTextFilePath = value;
            }
        }

        /// <summary>
        /// LineTerminatorUsed Property.
        /// </summary>
        public bool LineTerminatorUsed
        {
            get
            {
                return _lineTerminatorUsed;
            }
            set
            {
                _lineTerminatorUsed = value;
            }
        }

        /// <summary>
        /// LineTerminatorChars Property.
        /// </summary>
        public string LineTerminatorChars
        {
            get
            {
                return _lineTerminatorChars;
            }
            //set
            //{

            //    _lineTerminatorChars = value;
            //}
        }

        /// <summary>
        /// ColumnNamesOnFirstLine Property.
        /// </summary>
        public bool ColumnNamesOnFirstLine
        {
            get
            {
                return _columnNamesOnFirstLine;
            }
            set
            {
                _columnNamesOnFirstLine = value;
            }
        }

        /// <summary>
        /// ExpectedLineLength Property.
        /// </summary>
        public int ExpectedLineLength
        {
            get
            {
                return _expectedLineLength;
            }
            set
            {
                _expectedLineLength = value;
            }
        }

        /// <summary>
        /// ColDefs Property. Contains an array of column definitions.
        /// </summary>
        public PFColumnDefinitionsExt ColDefs
        {
            get
            {
                return _colDefs;
            }
            set
            {
                _colDefs = value;
                SetColNumFormat();
            }
        }

        private void SetColNumFormat()
        {
            _colNumFormat = "0";
            if (_colDefs != null)
            {
                if (_colDefs.ColumnDefinition != null)
                {
                    if (_colDefs.ColumnDefinition.Length > 0)
                    {
                        if (_colDefs.ColumnDefinition.Length < 9)
                        {
                            _colNumFormat = "0";
                        }
                        else if (_colDefs.ColumnDefinition.Length < 99)
                        {
                            _colNumFormat = "00";
                        }
                        else
                        {
                            _colNumFormat = "000";
                        }
                    }
                }
            }
        }


        //button click events
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (ColumnDefinitionHasChanges())
            {
                DialogResult res = CheckCancelRequest();
                if (res == DialogResult.Yes)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    HideForm();
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                HideForm();
            }
        }

        private void cmdProcessForm_Click(object sender, EventArgs e)
        {
            if (ProcessForm() == true)
            {
                this.DialogResult = DialogResult.OK;
                HideForm();
            }
        }

        private void cmdShowVisualLineParser_Click(object sender, EventArgs e)
        {
            ShowVisualLineParser();
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            FilePrint(true, false);
        }

        private void listviewColDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectionChanged();
        }

        private void cmdUpdateList_Click(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void cmdMoveItemUp_Click(object sender, EventArgs e)
        {
            MoveListViewItemUp();
        }

        private void cmdMoveItemDown_Click(object sender, EventArgs e)
        {
            MoveListViewItemDown();
        }

        private void listviewFilterlDefs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSelectionChanged();
        }

        private void cmdDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteSelectedItem();
        }

        private void cmdCopyLine_Click(object sender, EventArgs e)
        {
            CopySelectedItem();
        }

        private void cmdNewItem_Click(object sender, EventArgs e)
        {
            CreateNewItem();
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

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Fixed Length Text Input Definition Form");
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) 
            {
                if (ColumnDefinitionHasChanges())
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

        private bool ColumnDefinitionHasChanges()
        {
            bool retval = false;
            CompareLogic oCompare = new CompareLogic();

            _exitColDef = CreateColDefs(false);
            if (_exitColDef == null)
                return false;

            oCompare.Config.MaxDifferences = 10;

            ComparisonResult compResult = oCompare.Compare(_saveColDef, _exitColDef);

            if (compResult.AreEqual == false)
            {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Column Definitions have changes:\r\n");
                _msg.Append(compResult.DifferencesString);
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());
            }

            retval = !compResult.AreEqual;

            return retval;
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
                this.Text = "Build Fixed Length Column Definitions";

                SetLoggingValues();

                SetHelpFileValues();
                
                SetFormValues();

                //SetListViewTest();

                _printer = new FormPrinter(this);

                _saveColDef = CreateColDefs(false);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }


        private void SetLoggingValues()
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
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "PFAppDataComponents.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        private void SetFormValues()
        {
            InitListView();

        }

        private void InitListView()
        {
            PFColDefExt[] cols = null;

            this.listviewColDefs.View = View.Details;
            this.listviewColDefs.LabelEdit = false;
            this.listviewColDefs.AllowColumnReorder = false;
            this.listviewColDefs.CheckBoxes = false;
            this.listviewColDefs.FullRowSelect = true;
            this.listviewColDefs.GridLines = true;
            this.listviewColDefs.Sorting = SortOrder.Ascending;
            this.listviewColDefs.MultiSelect = false;
            this.listviewColDefs.HideSelection = false;

            // Create columns for the items and subitems. 
            // Width of -2 indicates auto-size.
            this.listviewColDefs.Columns.Add("Col No.", -2, HorizontalAlignment.Left);
            this.listviewColDefs.Columns.Add("Field Name", -2, HorizontalAlignment.Left);
            this.listviewColDefs.Columns.Add("Col Name", -2, HorizontalAlignment.Left);
            this.listviewColDefs.Columns.Add("Col Length", -2, HorizontalAlignment.Left);
            this.listviewColDefs.Columns.Add("Data Alignment", -2, HorizontalAlignment.Left);

            
            if (this.ColDefs != null)
            {
                if (this.ColDefs.ColumnDefinition != null)
                {
                    if (this.ColDefs.ColumnDefinition.Length > 0)
                    {
                        cols = this.ColDefs.ColumnDefinition;
                        for (int i = 0; i < cols.Length; i++)
                        {
                            ListViewItem lvi = new ListViewItem((i+1).ToString(_colNumFormat));
                            lvi.SubItems.Add(cols[i].SourceColumnName);
                            lvi.SubItems.Add(cols[i].OutputColumnName);
                            lvi.SubItems.Add(cols[i].OutputColumnLength.ToString());
                            lvi.SubItems.Add(cols[i].OutputColumnDataAlignment.ToString());
                            this.listviewColDefs.Items.Add(lvi);
                        }//end for
                        this.listviewColDefs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        this.listviewColDefs.Items[0].Selected = true;
                    }//end if
                }//end if
            }//end if

            this.listviewColDefs.ListViewItemSorter = new ColDefsItemComparer();
        
        }//end method

        private void SetListViewTest()
        {
            this.listviewColDefs.View = View.Details;
            this.listviewColDefs.LabelEdit = false;
            this.listviewColDefs.AllowColumnReorder = false;
            this.listviewColDefs.CheckBoxes = false;
            this.listviewColDefs.FullRowSelect = true;
            this.listviewColDefs.GridLines = true;
            this.listviewColDefs.Sorting = SortOrder.Ascending;
            this.listviewColDefs.MultiSelect = false;
            this.listviewColDefs.HideSelection = false;

            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("1", 0);
            // Place a check mark next to the item.
            //item1.Checked = true;
            item1.SubItems.Add("PKforTheTable");
            item1.SubItems.Add("12");
            item1.SubItems.Add("LeftJustify");
            ListViewItem item2 = new ListViewItem("2", 0);
            item2.SubItems.Add("Customer Fullname");
            item2.SubItems.Add("50");
            item2.SubItems.Add("LeftJustify");
            ListViewItem item3 = new ListViewItem("3", 0);
            // Place a check mark next to the item.
            //item3.Checked = true;
            item3.SubItems.Add("CityOrTownOfResidence");
            item3.SubItems.Add("75");
            item3.SubItems.Add("LeftJustfify");

            // Create columns for the items and subitems. 
            // Width of -2 indicates auto-size.
            this.listviewColDefs.Columns.Add("Col No.", -2, HorizontalAlignment.Left);
            this.listviewColDefs.Columns.Add("Field Name", -2, HorizontalAlignment.Left);
            this.listviewColDefs.Columns.Add("Field Length", -2, HorizontalAlignment.Left);
            this.listviewColDefs.Columns.Add("Data Alignment", -2, HorizontalAlignment.Left);

            //Add the items to the ListView.
            this.listviewColDefs.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

            this.listviewColDefs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            ListViewItem item4 = new ListViewItem("4", 0);
            // Place a check mark next to the item.
            //item3.Checked = true;
            item4.SubItems.Add("AnnualSalaryInDollars");
            item4.SubItems.Add("18");
            item4.SubItems.Add("RightJustify");
            this.listviewColDefs.Items.AddRange(new ListViewItem[] { item4});

            this.listviewColDefs.ListViewItemSorter = new ColDefsItemComparer();
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

        //private DialogResult ShowSaveFileDialog()
        //{
        //    DialogResult res = DialogResult.None;
        //    mainMenuSaveFileDialog.InitialDirectory = _saveSelectionsFolder;
        //    mainMenuSaveFileDialog.FileName = string.Empty;
        //    mainMenuSaveFileDialog.Filter = _saveFilter;
        //    mainMenuSaveFileDialog.FilterIndex = _saveFilterIndex;
        //    mainMenuSaveFileDialog.CreatePrompt = _showCreatePrompt;
        //    mainMenuSaveFileDialog.OverwritePrompt = _showOverwritePrompt;
        //    res = mainMenuSaveFileDialog.ShowDialog();
        //    _saveSelectionsFile = string.Empty;
        //    if (res == DialogResult.OK)
        //    {
        //        _saveSelectionsFolder = Path.GetDirectoryName(mainMenuSaveFileDialog.FileName);
        //        _saveSelectionsFile = mainMenuSaveFileDialog.FileName;
        //        _saveFilterIndex = mainMenuSaveFileDialog.FilterIndex;
        //    }
        //    return res;
        //}

        //private DialogResult ShowFolderBrowserDialog()
        //{
        //    DialogResult res = DialogResult.None;

        //    string folderPath = string.Empty;

        //    if (_saveSelectionsFolder.Length > 0)
        //        folderPath = _saveSelectionsFolder;
        //    else
        //        folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    mainMenuFolderBrowserDialog.ShowNewFolderButton = _showNewFolderButton;
        //    //mainMenuFolderBrowserDialog.RootFolder = 
        //    mainMenuFolderBrowserDialog.SelectedPath = folderPath;
        //    res = mainMenuFolderBrowserDialog.ShowDialog();
        //    if (res != DialogResult.Cancel)
        //    {
        //        folderPath = mainMenuFolderBrowserDialog.SelectedPath;
        //        _str.Length = 0;
        //        _str.Append(folderPath);
        //        if (folderPath.EndsWith(@"\") == false)
        //            _str.Append(@"\");
        //        _saveSelectionsFolder = folderPath;
        //    }


        //    return res;
        //}

        private void ShowPageSettings()
        {
            _printer.ShowPageSettings();
        }

        private void FilePrint(bool preview, bool showPrintDialog)
        {
            _printer.PageTitle = AppGlobals.AppInfo.AssemblyDescription;
            _printer.PageSubTitle = this.Text;
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }


        //application routines

        private bool ProcessForm()
        {
            bool colDefsCreated = false;
            PFColumnDefinitionsExt currColDefs = null;

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                currColDefs = CreateColDefs(true);

                if (currColDefs != null)
                {
                    colDefsCreated = true;
                    _colDefs = currColDefs;
                }
            }
            catch (System.Exception ex)
            {
                colDefsCreated = false;
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

            return colDefsCreated;
        }

        private void InitColDefForm()
        {
            if (this.ColDefs != null)
            {
                if (this.ColDefs.ColumnDefinition != null)
                {
                    if (this.ColDefs.ColumnDefinition.Length > 0)
                    {
                        this.listviewColDefs.ListViewItemSorter = new ColDefsItemComparer();

                        this.listviewColDefs.Items.Clear();
                        for (int i = 0; i < this.ColDefs.ColumnDefinition.Length; i++)
                        {
                            ListViewItem newItem = new ListViewItem(i.ToString(_colNumFormat));
                            newItem.SubItems.Add(this.ColDefs.ColumnDefinition[_fieldNameInx].SourceColumnName);
                            newItem.SubItems.Add(this.ColDefs.ColumnDefinition[_columnNameInx].OutputColumnName);
                            newItem.SubItems.Add(this.ColDefs.ColumnDefinition[_columnLengthInx].OutputColumnLength.ToString());
                            newItem.SubItems.Add(this.ColDefs.ColumnDefinition[_colummAlignmentInx].OutputColumnDataAlignment.ToString());

                        }
                        this.listviewColDefs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                        this.listviewColDefs.Items[0].Selected = true;
                    }//end if
                }//end if
            }//end if

        }

        private void InitColDefObject(ref PFColumnDefinitionsExt colDefs)
        {
            for (int i = 0; i < colDefs.ColumnDefinition.Length; i++)
            {
                colDefs.ColumnDefinition[_fieldNameInx].SourceColumnName = string.Empty;
                colDefs.ColumnDefinition[_columnNameInx].OutputColumnName = string.Empty;
                colDefs.ColumnDefinition[_columnLengthInx].OutputColumnLength = -1;
                colDefs.ColumnDefinition[_colummAlignmentInx].OutputColumnDataAlignment = PFDataAlign.LeftJustify;
            }
        }

        private PFColumnDefinitionsExt CreateColDefs(bool verifyNumbers)
        {
            int numErrors = 0;
            int expectedLineLength = -1;
            StringBuilder errMsg = new StringBuilder();

            if (this.listviewColDefs.Items.Count < 1)
            {
                return null;
            }
            PFColumnDefinitionsExt colDefs = new PFColumnDefinitionsExt(this.listviewColDefs.Items.Count);

            //error message only shown if errors occur
            errMsg.Append("Unable to create PFColumnDefinitionsExt object.");
            errMsg.Append(Environment.NewLine);


            try
            {
                expectedLineLength = 0;
                for (int i = 0; i < this.listviewColDefs.Items.Count; i++)
                {
                    colDefs.ColumnDefinition[i].SourceColumnName = this.listviewColDefs.Items[i].SubItems[_fieldNameInx].Text;
                    colDefs.ColumnDefinition[i].OutputColumnName = this.listviewColDefs.Items[i].SubItems[_columnNameInx].Text;
                    if (verifyNumbers)
                    {
                        if (PFTextProcessor.StringIsInt(this.listviewColDefs.Items[i].SubItems[_columnLengthInx].Text))
                        {
                            int colLen = Convert.ToInt32(this.listviewColDefs.Items[i].SubItems[_columnLengthInx].Text);
                            if (colLen > 0)
                            {
                                colDefs.ColumnDefinition[i].OutputColumnLength = Convert.ToInt32(this.listviewColDefs.Items[i].SubItems[_columnLengthInx].Text);
                                expectedLineLength += colDefs.ColumnDefinition[i].OutputColumnLength;
                            }
                            else
                            {
                                numErrors++;
                                errMsg.Append("Invalid negative output column length: ");
                                errMsg.Append(this.listviewColDefs.Items[i].SubItems[_columnLengthInx].Text);
                                errMsg.Append(Environment.NewLine);
                            }
                        }
                        else
                        {
                            numErrors++;
                            errMsg.Append("Invalid output column length: ");
                            errMsg.Append(this.listviewColDefs.Items[i].SubItems[_columnLengthInx].Text);
                            errMsg.Append(Environment.NewLine);
                        }
                    }
                    else
                    {
                        if (PFTextProcessor.StringIsInt(this.listviewColDefs.Items[i].SubItems[_columnLengthInx].Text))
                        {
                            colDefs.ColumnDefinition[i].OutputColumnLength = Convert.ToInt32(this.listviewColDefs.Items[i].SubItems[_columnLengthInx].Text);
                            expectedLineLength += colDefs.ColumnDefinition[i].OutputColumnLength;
                        }
                        else
                        {
                            colDefs.ColumnDefinition[i].OutputColumnLength = -1;
                        }
                    }
                    colDefs.ColumnDefinition[i].OutputColumnDataAlignment = (PFDataAlign)Enum.Parse(typeof(PFDataAlign), this.listviewColDefs.Items[i].SubItems[_colummAlignmentInx].Text);
                }
                if (numErrors > 0)
                {
                    throw new System.Exception(errMsg.ToString());
                }
                
                if(expectedLineLength > 0)
                    this.ExpectedLineLength = expectedLineLength;

            }
            catch (System.Exception ex)
            {
                colDefs = null;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
                 
        

            return colDefs;
        }



        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }

        private void MoveListViewItemUp()
        {
            if(this.listviewColDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewColDefs.SelectedIndices[0];
                int prevItemInx = currItemInx - 1;
                if (currItemInx > 0)
                {
                    ListViewItem currItem = this.listviewColDefs.Items[currItemInx];
                    ListViewItem prevItem = this.listviewColDefs.Items[prevItemInx];
                    currItem.Text = (prevItemInx+1).ToString(_colNumFormat);
                    prevItem.Text = (currItemInx+1).ToString(_colNumFormat);
                    this.listviewColDefs.Sort();
                    currItem.Selected = true;
                    this.txtColumnNumber.Text = currItem.Text;
                    currItem.EnsureVisible();
                    this.listviewColDefs.Refresh();
                }
            }
        }

        private void MoveListViewItemDown()
        {
            if (this.listviewColDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewColDefs.SelectedIndices[0];
                int nextItemInx = currItemInx + 1;
                if (currItemInx < (this.listviewColDefs.Items.Count-1))
                {
                    ListViewItem currItem = this.listviewColDefs.Items[currItemInx];
                    ListViewItem nextItem = this.listviewColDefs.Items[nextItemInx];
                    currItem.Text = (nextItemInx + 1).ToString(_colNumFormat);
                    nextItem.Text = (currItemInx + 1).ToString(_colNumFormat);
                    this.listviewColDefs.Sort();
                    currItem.Selected = true;
                    this.txtColumnNumber.Text = currItem.Text;
                    currItem.EnsureVisible();
                    this.listviewColDefs.Refresh();
                }
            }
        }

        private void ListViewSelectionChanged()
        {
            if (this.listviewColDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewColDefs.SelectedIndices[0];
                ListViewItem currItem = this.listviewColDefs.Items[currItemInx];
                int colNum = Convert.ToInt32(currItem.SubItems[0].Text);
                string colNumStr = colNum.ToString(_colNumFormat);
                this.txtColumnNumber.Text = currItem.SubItems[0].Text;
                this.txtFieldName.Text = currItem.SubItems[1].Text;
                this.txtColumnName.Text = currItem.SubItems[2].Text;
                this.txtColumnWidth.Text = currItem.SubItems[3].Text;
                this.cboValueAlignment.Text = currItem.SubItems[4].Text;
                currItem.EnsureVisible();
            }
        }

        private void CreateNewItem()
        {
            this.listviewColDefs.SelectedIndices.Clear();
            int newColumnNumber = this.listviewColDefs.Items.Count+1;
            this.txtColumnNumber.Text = newColumnNumber.ToString(_colNumFormat);
            this.txtFieldName.Text = string.Empty;
            this.txtColumnName.Text = string.Empty;
            this.txtColumnWidth.Text = string.Empty;
            this.cboValueAlignment.Text = "Left";
            if(this.listviewColDefs.Items.Count > 0)
                this.listviewColDefs.Items[this.listviewColDefs.Items.Count - 1].EnsureVisible();
        }

        private void UpdateListView()
        {
            int numErrors = 0;
            StringBuilder errMsg = new StringBuilder();
            if (PFTextProcessor.StringIsInt(this.txtColumnNumber.Text.Trim())==false)
            {
                numErrors++;
                errMsg.Append("You must specify the column number.");
                errMsg.Append(Environment.NewLine);
            }
            if (this.txtFieldName.Text.Trim().Length == 0)
            {
                numErrors++;
                errMsg.Append("You must specify a source data field name.");
                errMsg.Append(Environment.NewLine);
            }
            if (this.txtColumnName.Text.Trim().Length == 0)
            {
                numErrors++;
                errMsg.Append("You must specify a column name.");
                errMsg.Append(Environment.NewLine);
            }
            if (PFTextProcessor.StringIsInt(this.txtColumnWidth.Text.Trim()) == false)
            {
                numErrors++;
                errMsg.Append("You must specify a valid number for the column width.");
                errMsg.Append(Environment.NewLine);
            }
            else
            {
                int colLen = Convert.ToInt32(this.txtColumnWidth.Text.Trim());
                if (colLen < 1)
                {
                    numErrors++;
                    errMsg.Append("You must specify a valid positive number for the column width.");
                    errMsg.Append(Environment.NewLine);
                }
            }
            if (this.cboValueAlignment.Text.Trim().Length == 0)
            {
                numErrors++;
                errMsg.Append("You must specify a text alignment for the output value.");
                errMsg.Append(Environment.NewLine);
            }

            if (numErrors > 0)
            {
                AppMessages.DisplayErrorMessage(errMsg.ToString());
                return;
            }

            int colNum = Convert.ToInt32(this.txtColumnNumber.Text);
            string colNumStr = colNum.ToString(_colNumFormat);
            if (this.listviewColDefs.SelectedIndices.Count == 1)
            {
                //this is an update to an old item
                int currItemInx = this.listviewColDefs.SelectedIndices[0];
                ListViewItem currItem = this.listviewColDefs.Items[currItemInx];
                currItem.SubItems[0].Text = colNumStr;
                currItem.SubItems[1].Text = this.txtFieldName.Text;
                currItem.SubItems[2].Text = this.txtColumnName.Text;
                currItem.SubItems[3].Text = this.txtColumnWidth.Text;
                currItem.SubItems[4].Text = this.cboValueAlignment.Text;
                this.listviewColDefs.Sort();
                currItem.Selected = true;
                currItem.EnsureVisible();
                this.listviewColDefs.Refresh();
            }
            else
            {
                //this is a new item
                ListViewItem newItem = new ListViewItem(colNumStr);
                newItem.SubItems.Add(this.txtFieldName.Text);
                newItem.SubItems.Add(this.txtColumnName.Text);
                newItem.SubItems.Add(this.txtColumnWidth.Text.Trim());
                newItem.SubItems.Add(this.cboValueAlignment.Text);
                this.listviewColDefs.Items.Add(newItem);
                this.listviewColDefs.Sort();
                newItem.Selected = true;
                newItem.EnsureVisible();
                this.listviewColDefs.Refresh();

            }
        }

        private void DeleteSelectedItem()
        {

            if (this.listviewColDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewColDefs.SelectedIndices[0];
                ListViewItem currItem = this.listviewColDefs.Items[currItemInx];
                currItem.Remove();
                this.listviewColDefs.Refresh();
                AdjustFilterNumberFormat(this.listviewColDefs.Items.Count);
                RenumberListViewItems();
                this.listviewColDefs.Sort();
                if (currItemInx < this.listviewColDefs.Items.Count)
                    this.listviewColDefs.Items[currItemInx].Selected = true;
                else
                    if (this.listviewColDefs.Items.Count > 0)
                        this.listviewColDefs.Items[this.listviewColDefs.Items.Count - 1].Selected = true;
                    else
                        CreateNewItem(); //nothing to do: no more items left in list
                this.listviewColDefs.Refresh();
            }

        }

        private void AdjustFilterNumberFormat(int numFilters)
        {
            //format of filter numbers may need to change
            _colNumFormat = "0";
            if (numFilters < 10)
            {
                _colNumFormat = "0";
            }
            else if (numFilters < 100)
            {
                _colNumFormat = "00";
            }
            else
            {
                _colNumFormat = "000";
            }

        }

        private void RenumberListViewItems()
        {
            for (int i = 0; i < this.listviewColDefs.Items.Count; i++)
            {
                string colNumString = (i + 1).ToString(_colNumFormat);
                this.listviewColDefs.Items[i].Text = colNumString;
            }
        }


        private void CopySelectedItem()
        {
            if (this.listviewColDefs.SelectedIndices.Count != 1)
            {
                return;
            }

            int currItemInx = this.listviewColDefs.SelectedIndices[0];
            ListViewItem currItem = this.listviewColDefs.Items[currItemInx];
            ListViewItem newItem = new ListViewItem((listviewColDefs.Items.Count + 1).ToString(), 0);

            newItem.SubItems.Add(currItem.SubItems[1].Text);
            newItem.SubItems.Add(currItem.SubItems[2].Text);
            newItem.SubItems.Add(currItem.SubItems[3].Text);
            newItem.SubItems.Add(currItem.SubItems[4].Text);
            this.listviewColDefs.Items.Add(newItem);
            this.listviewColDefs.Sort();
            newItem.Selected = true;
            newItem.EnsureVisible();
            this.listviewColDefs.Refresh();
        }

        private void ShowVisualLineParser()
        {
            PFFixedLenColDefsVisualParseForm frm = null;
            DialogResult res = System.Windows.Forms.DialogResult.None;

            if (this.FixedLengthTextFilePath.Trim().Length == 0)
            {
                if (this.DefaultSourceFolder.Trim() != string.Empty)
                    _saveSelectionsFolder = this.DefaultSourceFolder;
                else
                    _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractor\Sources\TextFiles\FixedLength\";
                _saveFilter = "Fixed Length Text Files|*.txt;*.dat|All Files|*.*";

                res = ShowOpenFileDialog();
                if (res == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                this.FixedLengthTextFilePath = _saveSelectionsFile;
            }


            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                string[] lines = LoadFirstTwoLinesFromFile();

                if (lines[0].Length > 512
                    || lines[1].Length > 512)
                {
                    _msg.Length = 0;
                    _msg.Append("ALERT: Unable to use visual parser control for lines longer than 512 bytes.");
                    _msg.Append(" Use the Column Definitions Form to define the fields in the fixed length line.");
                    throw new System.Exception(_msg.ToString());
                }


                frm = new PFFixedLenColDefsVisualParseForm();

                frm.ColumnNamesOnFirstLine = this.ColumnNamesOnFirstLine;
                frm.ExpectedLineWidth = this.ExpectedLineLength;
                frm.TextLine1 = lines[0];
                frm.TextLine2 = lines[1];
                res = frm.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.Cancel
                    || res == System.Windows.Forms.DialogResult.No)
                {
                    return;
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


            if (frm != null)
            {
                PFFixedLenColDefsVisualParseForm.ColNameAndLength[] colNamesAndLengths = frm.GetFieldNamesAndLengths();

                frm.Close();

                ShowParsedNamesAndLengths(colNamesAndLengths);
            }

        }

        private string[] LoadFirstTwoLinesFromFile()
        {
            string[] lines = new string[2] { string.Empty, string.Empty};
            PFTextFile file = new PFTextFile(this.FixedLengthTextFilePath, PFFileOpenOperation.OpenFileToRead);
            string data = string.Empty;

            try
            {
                if (this.LineTerminatorUsed)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (file.Peek() > 0)
                        {
                            lines[i] = file.ReadLine();
                        }
                        else
                        {
                            lines[i] = string.Empty;
                        }
                    }
                    this.ExpectedLineLength = lines[0].Length;
                }
                else
                {
                    //no line terminator
                    int numBytesPerLine = this.ExpectedLineLength;
                    int numBytesToRead = this.ExpectedLineLength * 2;
                    data = file.ReadData(numBytesToRead, false);
                    if (data.Length < numBytesPerLine)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable find data for one line in file ");
                        _msg.Append(this.FixedLengthTextFilePath);
                        WriteToMessageLog(_msg.ToString());
                        AppMessages.DisplayErrorMessage(_msg.ToString());
                        return lines;
                    }
                    lines[0] = data.Substring(0, this.ExpectedLineLength);
                    if (data.Length == numBytesToRead)
                        lines[1] = data.Substring(this.ExpectedLineLength, this.ExpectedLineLength);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                if (ex is OutOfMemoryException || ex is IOException)
                {
                    if (this.LineTerminatorUsed)
                    {
                        _msg.Append("Attempt to load first two lines of file failed on out-of-memory exception.\r\n");
                        _msg.Append("Make sure line terminators exist in the file. You specified the line was terminated.");
                    }
                }
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (file.FileIsOpen)
                    file.CloseFile();
            }
                 

            return lines;
        }

        private void ShowParsedNamesAndLengths(PFFixedLenColDefsVisualParseForm.ColNameAndLength[] colNamesAndLengths)
        {

            listviewColDefs.Items.Clear();

            for (int i = 0; i < colNamesAndLengths.Length-1; i++)   
            {
                ListViewItem lvi = new ListViewItem((i + 1).ToString(_colNumFormat));
                lvi.SubItems.Add(colNamesAndLengths[i].colName);
                lvi.SubItems.Add(colNamesAndLengths[i].colName);
                lvi.SubItems.Add(colNamesAndLengths[i].colLength.ToString());
                lvi.SubItems.Add(PFDataAlign.LeftJustify.ToString());
                this.listviewColDefs.Items.Add(lvi);
            }

        }


    }//end class

    internal class ColDefsItemComparer : IComparer
    {
        private int col;
        public ColDefsItemComparer()
        {
            col = 0;
        }
        public ColDefsItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }

    //internal class ColDefsItemComparer : IComparer
    //{
    //    private string col;
    //    public ColDefsItemComparer()
    //    {
    //        col = "0";
    //    }
    //    public ColDefsItemComparer(string column)
    //    {
    //        col = column;
    //    }
    //    public int Compare(object x, object y)
    //    {
    //        return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
    //    }
    //}


#pragma warning restore 1591
}//end namespace
