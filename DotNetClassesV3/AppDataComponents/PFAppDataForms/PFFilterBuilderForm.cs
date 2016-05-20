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
using PFCollectionsObjects;
using PFSystemObjects;

namespace PFAppDataForms
{
    /// <summary>
    /// Form class for filter builder popup.
    /// </summary>
    public partial class PFFilterBuilderForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string _filterNumFormat = "0";
        //subitem indexes for various fields shown in the listview
        private int _filterNumInx = 0;
        private int _booleanInx = 1;
        private int _aggregatorInx = 2;
        private int _fieldNameInx = 3;
        private int _fieldTypeInx = 4;
        private int _conditionInx = 5;
        private int _valueInx = 6;

        
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

        private DataTable _queryDataTable = null;
        private PFList<PFFilterDef> _filterDefs = null;
        private string _filterText = string.Empty;

        //fields for checking for form changes on exit processing
        private PFList<PFFilterDef> _saveFilterDefs = null;
        private PFList<PFFilterDef> _exitFilterDef = null;


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public PFFilterBuilderForm()
        {
            InitializeComponent();
        }


        //properties

        /// <summary>
        /// MessageLog object.
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
        /// QueryDataTable Property. Contains an array of column definitions.
        /// </summary>
        public DataTable QueryDataTable
        {
            get
            {
                return _queryDataTable;
            }
            set
            {
                _queryDataTable = value;
            }
        }

        /// <summary>
        /// QueryDataTable Property. Contains a list of filter definitions.
        /// </summary>
        public PFList<PFFilterDef> FilterDefs
        {
            get
            {
                return _filterDefs;
            }
            set
            {
                _filterDefs = value;
                SetFilterNumFormat();
            }
        }

        /// <summary>
        /// FilterText Property.
        /// </summary>
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
            }
        }


        private void SetFilterNumFormat()
        {
            _filterNumFormat = "0";
            if (_filterDefs != null)
            {
                if (_filterDefs.Count > 0)
                {
                    if (_filterDefs.Count < 10)
                    {
                        _filterNumFormat = "0";
                    }
                    else if (_filterDefs.Count < 99)
                    {
                        _filterNumFormat = "00";
                    }
                    else
                    {
                        _filterNumFormat = "000";
                    }
                }
            }
        }


        //button click events
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (FilterDefinitionsHaveChanges())
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

        private void cmdNewItem_Click(object sender, EventArgs e)
        {
            CreateNewItem();
        }

        private void cmdUpdateList_Click(object sender, EventArgs e)
        {
            UpdateListView();
        }

        private void cmdPreviewFilterText_Click(object sender, EventArgs e)
        {
            PreviewFilterText();
        }

        private void cboFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldNameSelectedIndexChanged();
        }

        private void cboComparison_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComparisonSelectedIndexChanged();
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

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Filter Builder Form");
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
                if (FilterDefinitionsHaveChanges())
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

        private bool FilterDefinitionsHaveChanges()
        {
            bool retval = false;
            CompareLogic oCompare = new CompareLogic();

            _exitFilterDef = CreateFilterDefs();
            if (_exitFilterDef == null)
                return false;

            oCompare.Config.MaxDifferences = 10;

            ComparisonResult compResult = oCompare.Compare(_saveFilterDefs, _exitFilterDef);

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

        private DialogResult PromptForFileSave(ReasonForFileSavePrompt promptReason)
        {
            return PromptForFileSave(promptReason, MessageBoxButtons.YesNoCancel);
        }

        private DialogResult PromptForFileSave(ReasonForFileSavePrompt promptReason, MessageBoxButtons btns)
        {
            DialogResult res = System.Windows.Forms.DialogResult.None;

            _msg.Length = 0;
            _msg.Append("The are unsaved changes to the column definition. ");
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
                this.Text = "Filter Builder";

                SetLoggingValues();

                SetHelpFileValues();

                SetFormValues();

                //SetListViewTest();

                _printer = new FormPrinter(this);

                _saveFilterDefs = CreateFilterDefs();

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
            InitInputDropdowns();

        }

        private void InitListView()
        {
            this.listviewFilterDefs.View = View.Details;
            this.listviewFilterDefs.LabelEdit = false;
            this.listviewFilterDefs.AllowColumnReorder = false;
            this.listviewFilterDefs.CheckBoxes = false;
            this.listviewFilterDefs.FullRowSelect = true;
            this.listviewFilterDefs.GridLines = true;
            this.listviewFilterDefs.Sorting = SortOrder.Ascending;
            this.listviewFilterDefs.MultiSelect = false;
            this.listviewFilterDefs.HideSelection = false;

            // Create columns for the items and subitems. 
            // Width of -2 indicates auto-size.
            this.listviewFilterDefs.Columns.Add("FilterNum", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Boolean", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Aggregate", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Field Name", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Field Type", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Condition", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Value", -2, HorizontalAlignment.Left);


            if (this.FilterDefs != null)
            {
                if (this.FilterDefs.Count > 0)
                {
                    for (int i = 0; i < this.FilterDefs.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem((i + 1).ToString(_filterNumFormat));
                        lvi.SubItems.Add(this.FilterDefs[i].FilterBoolean.ToString().Replace("None",string.Empty));
                        lvi.SubItems.Add(this.FilterDefs[i].FilterFieldAggregator.ToString().Replace("None", string.Empty));
                        lvi.SubItems.Add(this.FilterDefs[i].FilterField.ToString());
                        lvi.SubItems.Add(this.FilterDefs[i].FilterFieldType.ToString());
                        lvi.SubItems.Add(this.FilterDefs[i].FilterCondition.ToString().Replace("None", string.Empty));
                        lvi.SubItems.Add(this.FilterDefs[i].Filtervalue.ToString());
                        this.listviewFilterDefs.Items.Add(lvi);
                    }//end for
                    this.listviewFilterDefs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    this.listviewFilterDefs.Items[0].Selected = true;
                }//end if
            }//end if

            this.listviewFilterDefs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.listviewFilterDefs.ListViewItemSorter = new OutputFilterItemComparer();
        
        }//end method

        private void InitInputDropdowns()
        {
            this.cboBoolean.Items.Clear();
            foreach(enFilterBoolean fb in Enum.GetValues(typeof(enFilterBoolean)))
            {
                if(fb == enFilterBoolean.None)
                    this.cboBoolean.Items.Add(string.Empty);
                else
                    this.cboBoolean.Items.Add(fb.ToString());
            }

            this.cboAggregator.Items.Clear();
            foreach (enAggregateFunction agf in Enum.GetValues(typeof(enAggregateFunction)))
            {
                if(agf == enAggregateFunction.None)
                    this.cboAggregator.Items.Add(string.Empty);
                else
                    this.cboAggregator.Items.Add(agf.ToString());
            }

            this.cboFieldName.Items.Clear();
            this.cboFieldName.Items.Add(string.Empty);
            DataColumn col = null;
            if (this.QueryDataTable != null)
            {
                if (this.QueryDataTable.Columns != null)
                {
                    if (this.QueryDataTable.Columns.Count > 0)
                    {
                        for (int i = 0; i < this.QueryDataTable.Columns.Count; i++)
                        {
                            col = this.QueryDataTable.Columns[i];
                            this.cboFieldName.Items.Add(col.ColumnName);
                        }//end for
                    }//end if
                }//end if
            }//end if

            this.cboComparison.Items.Clear();
            foreach (enFilterCondition fc in Enum.GetValues(typeof(enFilterCondition)))
            {
                if (fc == enFilterCondition.None)
                    this.cboComparison.Items.Add(string.Empty);
                else
                    this.cboComparison.Items.Add(fc.ToString());
            }

        }

        private void SetListViewTest()
        {
            this.listviewFilterDefs.View = View.Details;
            this.listviewFilterDefs.LabelEdit = false;
            this.listviewFilterDefs.AllowColumnReorder = false;
            this.listviewFilterDefs.CheckBoxes = false;
            this.listviewFilterDefs.FullRowSelect = true;
            this.listviewFilterDefs.GridLines = true;
            this.listviewFilterDefs.Sorting = SortOrder.Ascending;
            this.listviewFilterDefs.MultiSelect = false;
            this.listviewFilterDefs.HideSelection = false;

            // Create columns for the items and subitems. 
            // Width of -2 indicates auto-size.
            this.listviewFilterDefs.Columns.Add("FilterNum", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Boolean", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Aggregate", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Field Name", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Field Type", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Condition", -2, HorizontalAlignment.Left);
            this.listviewFilterDefs.Columns.Add("Value", -2, HorizontalAlignment.Left);

            // Create three items and three sets of subitems for each item.
            ListViewItem item1 = new ListViewItem("1", 0);
            // Place a check mark next to the item.
            //item1.Checked = true;
            item1.SubItems.Add("");
            item1.SubItems.Add("Count");
            item1.SubItems.Add("GeographyKey");
            item1.SubItems.Add("System.Int32");
            item1.SubItems.Add("GreaterThan");
            item1.SubItems.Add("5");
            ListViewItem item2 = new ListViewItem("2", 0);
            item2.SubItems.Add("And");
            item2.SubItems.Add("");
            item2.SubItems.Add("LastName");
            item2.SubItems.Add("System.String");
            item2.SubItems.Add("Like");
            item2.SubItems.Add("'*abel*'");
            ListViewItem item3 = new ListViewItem("3", 0);
            item3.SubItems.Add("Or");
            item3.SubItems.Add("");
            item3.SubItems.Add("Title");
            item3.SubItems.Add("System.String");
            item3.SubItems.Add("In");
            item3.SubItems.Add("'President','VicePresident', 'Councillor'");

            //Add the items to the ListView.
            this.listviewFilterDefs.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

            this.listviewFilterDefs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            ListViewItem item4 = new ListViewItem("4", 0);
            item4.SubItems.Add("Or");
            item4.SubItems.Add("");
            item4.SubItems.Add("EnglishEducation");
            item4.SubItems.Add("System.String");
            item4.SubItems.Add("StartsWith");
            item4.SubItems.Add("'P'");
            this.listviewFilterDefs.Items.AddRange(new ListViewItem[] { item4 });

            ListViewItem item5 = new ListViewItem("5", 0);
            item5.SubItems.Add("Or");
            item5.SubItems.Add("");
            item5.SubItems.Add("BirthDate");
            item5.SubItems.Add("System.DateTime");
            item5.SubItems.Add("GreaterThanOrEqualTo");
            item5.SubItems.Add("1/1/1990");
            this.listviewFilterDefs.Items.AddRange(new ListViewItem[] { item5 });

            this.listviewFilterDefs.ListViewItemSorter = new OutputFilterItemComparer();
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
            _printer.PageSubTitle = "Filter Builder Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }



        //application routines

        private bool ProcessForm()
        {
            bool filterDefsCreated = false;
            PFList<PFFilterDef> currFilterDefs = null;

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                currFilterDefs = CreateFilterDefs();

                if (currFilterDefs != null)
                {
                    filterDefsCreated = true;
                    _filterDefs = currFilterDefs;
                    _filterText = BuildFilterText();
                }
            }
            catch (System.Exception ex)
            {
                filterDefsCreated = false;
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

            return filterDefsCreated;
        }

        private void InitFilterDefForm()
        {
            if (this.FilterDefs != null)
            {
                if (this.FilterDefs.Count > 0)
                {
                    for (int i = 0; i < this.FilterDefs.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem((i + 1).ToString(_filterNumFormat));
                        lvi.SubItems.Add(this.FilterDefs[i].FilterBoolean.ToString().Replace("None", string.Empty));
                        lvi.SubItems.Add(this.FilterDefs[i].FilterFieldAggregator.ToString().Replace("None", string.Empty));
                        lvi.SubItems.Add(this.FilterDefs[i].FilterField.ToString());
                        lvi.SubItems.Add(this.FilterDefs[i].FilterFieldType.ToString());
                        lvi.SubItems.Add(this.FilterDefs[i].FilterCondition.ToString().Replace("None", string.Empty));
                        lvi.SubItems.Add(this.FilterDefs[i].Filtervalue.ToString());
                        this.listviewFilterDefs.Items.Add(lvi);
                    }//end for
                    this.listviewFilterDefs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    this.listviewFilterDefs.Items[0].Selected = true;
                }//end if
                this.listviewFilterDefs.ListViewItemSorter = new OutputFilterItemComparer();
            }//end if
        }

        private void InitColDefObject(ref PFColumnDefinitionsExt colDefs)
        {
            for (int i = 0; i < colDefs.ColumnDefinition.Length; i++)
            {
                _filterDefs[i].FilterNum = i;
                _filterDefs[i].FilterBoolean = enFilterBoolean.None;
                _filterDefs[i].FilterFieldAggregator = enAggregateFunction.None;
                _filterDefs[i].FilterField = string.Empty;
                _filterDefs[i].FilterFieldType = string.Empty;
                _filterDefs[i].FilterCondition = enFilterCondition.None;
                _filterDefs[i].Filtervalue = string.Empty;
            }
        }

        private PFList<PFFilterDef> CreateFilterDefs()
        {
            if (this.listviewFilterDefs.Items.Count < 1)
            {
                return null;
            }
            PFList<PFFilterDef> filterDefs = new PFList<PFFilterDef>();

            try
            {
                for (int i = 0; i < this.listviewFilterDefs.Items.Count; i++)
                {
                    PFFilterDef filterDef = new PFFilterDef();
                    filterDef.FilterNum = i;
                    if (this.listviewFilterDefs.Items[i].SubItems[_booleanInx].Text.Trim() == string.Empty)
                        filterDef.FilterBoolean = enFilterBoolean.None;
                    else
                        filterDef.FilterBoolean =  (enFilterBoolean)Enum.Parse(typeof(enFilterBoolean),this.listviewFilterDefs.Items[i].SubItems[_booleanInx].Text);
                    if(this.listviewFilterDefs.Items[i].SubItems[_aggregatorInx].Text.Trim() == string.Empty)
                        filterDef.FilterFieldAggregator = enAggregateFunction.None;
                    else
                        filterDef.FilterFieldAggregator = (enAggregateFunction)Enum.Parse(typeof(enAggregateFunction), this.listviewFilterDefs.Items[i].SubItems[_aggregatorInx].Text);
                    filterDef.FilterField = this.listviewFilterDefs.Items[i].SubItems[_fieldNameInx].Text;
                    filterDef.FilterFieldType = this.listviewFilterDefs.Items[i].SubItems[_fieldTypeInx].Text;
                    if (this.listviewFilterDefs.Items[i].SubItems[_conditionInx].Text == string.Empty)
                        filterDef.FilterCondition = enFilterCondition.None;
                    else
                        filterDef.FilterCondition = (enFilterCondition)Enum.Parse(typeof(enFilterCondition), this.listviewFilterDefs.Items[i].SubItems[_conditionInx].Text);
                    filterDef.Filtervalue = this.listviewFilterDefs.Items[i].SubItems[_valueInx].Text;
                    filterDefs.Add(filterDef);
                }
            }
            catch (System.Exception ex)
            {
                filterDefs = null;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
                 
        

            return filterDefs;
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
            if(this.listviewFilterDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewFilterDefs.SelectedIndices[0];
                int prevItemInx = currItemInx - 1;
                if (currItemInx > 0)
                {
                    ListViewItem currItem = this.listviewFilterDefs.Items[currItemInx];
                    ListViewItem prevItem = this.listviewFilterDefs.Items[prevItemInx];
                    if (currItemInx == 1)
                    {
                        prevItem.SubItems[_booleanInx].Text = currItem.SubItems[_booleanInx].Text;
                        currItem.SubItems[_booleanInx].Text = string.Empty;
                    }
                    currItem.Text = (prevItemInx+1).ToString(_filterNumFormat);
                    prevItem.Text = (currItemInx+1).ToString(_filterNumFormat);
                    this.listviewFilterDefs.Sort();
                    currItem.Selected = true;
                    this.txtFilterNumber.Text = currItem.Text;
                    currItem.EnsureVisible();
                    this.listviewFilterDefs.Refresh();
                }
            }
        }

        private void MoveListViewItemDown()
        {
            if (this.listviewFilterDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewFilterDefs.SelectedIndices[0];
                int nextItemInx = currItemInx + 1;
                if (currItemInx < (this.listviewFilterDefs.Items.Count-1))
                {
                    ListViewItem currItem = this.listviewFilterDefs.Items[currItemInx];
                    ListViewItem nextItem = this.listviewFilterDefs.Items[nextItemInx];
                    if (currItemInx == 0)
                    {
                        currItem.SubItems[_booleanInx].Text = nextItem.SubItems[_booleanInx].Text;
                        nextItem.SubItems[_booleanInx].Text = string.Empty;
                    }
                    currItem.Text = (nextItemInx + 1).ToString(_filterNumFormat);
                    nextItem.Text = (currItemInx + 1).ToString(_filterNumFormat);
                    this.listviewFilterDefs.Sort();
                    currItem.Selected = true;
                    this.txtFilterNumber.Text = currItem.Text;
                    currItem.EnsureVisible();
                    this.listviewFilterDefs.Refresh();
                }
            }
        }

        private void ListViewSelectionChanged()
        {
            if (this.listviewFilterDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewFilterDefs.SelectedIndices[0];
                ListViewItem currItem = this.listviewFilterDefs.Items[currItemInx];
                int filterNum = Convert.ToInt32(currItem.SubItems[_filterNumInx].Text);
                string filterNumStr = filterNum.ToString(_filterNumFormat);
                this.txtFilterNumber.Text = filterNumStr;
                this.cboBoolean.Text = currItem.SubItems[_booleanInx].Text;
                this.cboAggregator.Text = currItem.SubItems[_aggregatorInx].Text;
                this.cboFieldName.Text = currItem.SubItems[_fieldNameInx].Text;
                this.txtDataType.Text = currItem.SubItems[_fieldTypeInx].Text;
                this.cboComparison.Text = currItem.SubItems[_conditionInx].Text;
                this.txtCompareToValue.Text = currItem.SubItems[_valueInx].Text;
                currItem.EnsureVisible();
            }
        }

        private void CreateNewItem()
        {
            this.listviewFilterDefs.SelectedIndices.Clear();
            int newFilterNumber = this.listviewFilterDefs.Items.Count+1;
            this.txtFilterNumber.Text = newFilterNumber.ToString(_filterNumFormat);
            this.cboBoolean.Text = string.Empty;
            this.cboAggregator.Text = string.Empty;
            this.cboFieldName.Text = string.Empty;
            this.txtDataType.Text = string.Empty;
            this.cboComparison.Text = string.Empty;
            this.txtCompareToValue.Text = string.Empty;
            if(this.listviewFilterDefs.Items.Count > 0)
                this.listviewFilterDefs.Items[this.listviewFilterDefs.Items.Count-1].EnsureVisible();
        }

        private void UpdateListView()
        {
            int filterNum = 0;
            int numErrors = 0;
            StringBuilder errMsg = new StringBuilder();
            int numFilters = this.listviewFilterDefs.Items.Count;
            int revisedNumFilters = numFilters;

            if (this.txtFilterNumber.Text.Trim() == string.Empty)
            {
                this.txtFilterNumber.Text = (this.listviewFilterDefs.Items.Count + 1).ToString(_filterNumFormat);
            }
            if (PFTextProcessor.StringIsInt(this.txtFilterNumber.Text.Trim()) == false)
            {
                numErrors++;
                errMsg.Append("You must specify the filter number.");
                errMsg.Append(Environment.NewLine);
            }
            filterNum = Convert.ToInt32(this.txtFilterNumber.Text.Trim());

            if (filterNum == 1 && this.cboBoolean.Text != string.Empty)
            {
                numErrors++;
                errMsg.Append("Do not specify a boolean for the first filter.");
                errMsg.Append(Environment.NewLine);
            }

            if (filterNum > 1 && this.cboBoolean.Text == string.Empty)
            {
                numErrors++;
                errMsg.Append("You must specify a boolean for the filter.");
                errMsg.Append(Environment.NewLine);
            }


            if (this.cboFieldName.Text == string.Empty)
            {
                numErrors++;
                errMsg.Append("You must specify a data field name.");
                errMsg.Append(Environment.NewLine);
            }

            if (this.cboComparison.Text == string.Empty)
            {
                numErrors++;
                errMsg.Append("You must specify a comparison operator.");
                errMsg.Append(Environment.NewLine);
            }

            if ((this.cboComparison.Text == enFilterCondition.IsNull.ToString() || this.cboComparison.Text == enFilterCondition.IsNotNull.ToString())
                && (this.txtCompareToValue.Text.Trim() != string.Empty))
            {
                numErrors++;
                errMsg.Append("No Compare To Value allowed for IsNull or IsNotNull comparison operators.");
                errMsg.Append(Environment.NewLine);
            }
            if ((this.txtCompareToValue.Text.Trim() == string.Empty)
                && (this.cboComparison.Text != enFilterCondition.IsNull.ToString() && this.cboComparison.Text != enFilterCondition.IsNotNull.ToString()))
            {
                numErrors++;
                errMsg.Append("You must specify a compare to value or values");
                errMsg.Append(Environment.NewLine);
            }

            if (numErrors > 0)
            {
                AppMessages.DisplayErrorMessage(errMsg.ToString());
                return;
            }

            filterNum = Convert.ToInt32(this.txtFilterNumber.Text);
            string filterNumStr = filterNum.ToString(_filterNumFormat);
            if (this.listviewFilterDefs.SelectedIndices.Count == 1)
            {
                //this is an update to an old item
                int currItemInx = this.listviewFilterDefs.SelectedIndices[0];
                ListViewItem currItem = this.listviewFilterDefs.Items[currItemInx];
                currItem.SubItems[_filterNumInx].Text = filterNumStr;
                currItem.SubItems[_booleanInx].Text = this.cboBoolean.Text;
                currItem.SubItems[_aggregatorInx].Text = this.cboAggregator.Text;
                currItem.SubItems[_fieldNameInx].Text = this.cboFieldName.Text;
                currItem.SubItems[_fieldTypeInx].Text = this.txtDataType.Text;
                currItem.SubItems[_conditionInx].Text = this.cboComparison.Text;
                currItem.SubItems[_valueInx].Text = this.txtCompareToValue.Text;
                this.listviewFilterDefs.Sort();
                currItem.Selected = true;
                currItem.EnsureVisible();
                this.listviewFilterDefs.Refresh();
            }
            else
            {
                //this is a new item
                AdjustFilterNumberFormat(this.listviewFilterDefs.Items.Count + 1);
                RenumberListViewItems();
                ListViewItem newItem = new ListViewItem(filterNumStr);
                newItem.SubItems.Add(this.cboBoolean.Text);
                newItem.SubItems.Add(this.cboAggregator.Text);
                newItem.SubItems.Add(this.cboFieldName.Text);
                newItem.SubItems.Add(this.txtDataType.Text);
                newItem.SubItems.Add(this.cboComparison.Text);
                newItem.SubItems.Add(this.txtCompareToValue.Text);
                this.listviewFilterDefs.Items.Add(newItem);
                this.listviewFilterDefs.Sort();
                newItem.Selected = true;
                newItem.EnsureVisible();
                this.listviewFilterDefs.Refresh();
                revisedNumFilters = this.listviewFilterDefs.Items.Count;

            }
            if (revisedNumFilters != numFilters)
            {
                AdjustFilterNumberFormat(revisedNumFilters);
            }
            RenumberListViewItems();
        }

        private void DeleteSelectedItem()
        {

            if (this.listviewFilterDefs.SelectedIndices.Count == 1)
            {
                int currItemInx = this.listviewFilterDefs.SelectedIndices[0];
                ListViewItem currItem = this.listviewFilterDefs.Items[currItemInx];
                currItem.Remove();
                this.listviewFilterDefs.Refresh();
                AdjustFilterNumberFormat(this.listviewFilterDefs.Items.Count);
                RenumberListViewItems();
                this.listviewFilterDefs.Sort();
                if (currItemInx < this.listviewFilterDefs.Items.Count)
                    this.listviewFilterDefs.Items[currItemInx].Selected = true;
                else
                    if (this.listviewFilterDefs.Items.Count > 0)
                        this.listviewFilterDefs.Items[this.listviewFilterDefs.Items.Count - 1].Selected = true;
                    else
                        CreateNewItem(); //nothing to do: no more items left in list
                this.listviewFilterDefs.Refresh();
            }
            
        }

        private void AdjustFilterNumberFormat(int numFilters)
        {
            //format of filter numbers may need to change
            _filterNumFormat = "0";
            if (numFilters < 10)
            {
                _filterNumFormat = "0";
            }
            else if (numFilters < 100)
            {
                _filterNumFormat = "00";
            }
            else
            {
                _filterNumFormat = "000";
            }

        }

        private void RenumberListViewItems()
        {
            for (int i = 0; i < this.listviewFilterDefs.Items.Count; i++)
            {
                string colNumString = (i + 1).ToString(_filterNumFormat); 
                this.listviewFilterDefs.Items[i].Text = colNumString;
                if (i == 0)
                {
                    //first filter should not have a boolean
                    this.listviewFilterDefs.Items[0].SubItems[_booleanInx].Text = string.Empty;
                }
            }
        }

        private void FieldNameSelectedIndexChanged()
        {
            DataColumn col = null;
            if (this.QueryDataTable != null)
            {
                if (this.QueryDataTable.Columns != null)
                {
                    if (this.QueryDataTable.Columns.Count > 0)
                    {
                        for (int i = 0; i < this.QueryDataTable.Columns.Count; i++)
                        {
                            col = this.QueryDataTable.Columns[i];
                            if (col.ColumnName == this.cboFieldName.Text)
                            {
                                this.txtDataType.Text = col.DataType.ToString();
                                break;
                            }
                        }//end for  
                    }//end if
                }//end if
            }//end if

        }

        private void ComparisonSelectedIndexChanged()
        {
            if (this.cboComparison.Text == enFilterCondition.IsNull.ToString()
                || this.cboComparison.Text == enFilterCondition.IsNotNull.ToString())
            {
                this.txtCompareToValue.Text = string.Empty;
            }
        }

        private void PreviewFilterText()
        {
            string filterText = string.Empty;

            try
            {
                filterText = BuildFilterText();
                AppMessages.DisplayMessage(filterText, "Filter Text", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteToMessageLog (_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
        
        }

        private string BuildFilterText()
        {
            StringBuilder filterText = new StringBuilder();
            string filterElement = string.Empty;  
            bool aggregatorDefined = false;
            string valueElement = string.Empty;

            for (int i = 0; i < this.listviewFilterDefs.Items.Count; i++)
            {
                ListViewItem lvi = this.listviewFilterDefs.Items[i];
                aggregatorDefined = false;

                filterElement = lvi.SubItems[_booleanInx].Text.Trim();
                if(filterElement.ToLower().Replace("none",string.Empty) != string.Empty)
                {
                    //append boolean expression
                    filterText.Append(filterElement);
                    filterText.Append(" ");
                }
                filterElement = lvi.SubItems[_aggregatorInx].Text.Trim();
                if (filterElement.ToLower().Replace("none", string.Empty) != string.Empty)
                {
                    //append aggregator operator
                    aggregatorDefined = true;
                    filterText.Append(filterElement);
                    filterText.Append("(");
                }
                filterElement = lvi.SubItems[_fieldNameInx].Text.Trim();
                if (filterElement.ToLower().Replace("none", string.Empty) != string.Empty)
                {
                    //append field name
                    filterText.Append(filterElement);
                    if(aggregatorDefined)
                        filterText.Append(") ");
                    else
                        filterText.Append(" ");
                }

                //igonore the data type: it is not included in the filter text

                filterElement = lvi.SubItems[_conditionInx].Text.Trim();
                valueElement = lvi.SubItems[_valueInx].Text;
                if (filterElement != string.Empty)
                {
                    //append condition operator
                    string opPlusValueText = GetOperationText(filterElement, valueElement, lvi.SubItems[_fieldTypeInx].Text);
                    filterText.Append(opPlusValueText);
                    filterText.Append(" ");
                }


            }


            return filterText.ToString();
        }

        //EqualTo,
        //GreaterThan,
        //LessThan,
        //GreaterThanOrEqualTo,
        //LessThanOrEqualTo,
        //In,                              //array search (custom)  
        //Like,                            //c# string.contains
        //StartsWith,
        //EndsWith,
        //IsNull,
        //NotEqualTo,
        //NotGreaterThan,
        //NotLessThan,
        //NotIn,
        //NotLike,
        //DoesNotStartWith,
        //DoesNotEndWith,
        //IsNotNull

        private string GetOperationText(string condition, string value, string sourceDataType)
        {
            string opPlusValueText = string.Empty;
            string opText = string.Empty;
            string valueText = string.Empty;
            string valueEdited = value.Replace("\"", "'");
            enFilterCondition filterCondition = (enFilterCondition)Enum.Parse(typeof(enFilterCondition), condition);

            switch (filterCondition)
            {
                case enFilterCondition.EqualTo:
                    opText = "= ";
                    break;
                case enFilterCondition.GreaterThan:
                    opText = "> ";
                    break;
                case enFilterCondition.LessThan:
                    opText = "< ";
                    break;
                case enFilterCondition.GreaterThanOrEqualTo:
                    opText = ">= ";
                    break;
                case enFilterCondition.LessThanOrEqualTo:
                    opText = "<= ";
                    break;
                case enFilterCondition.In:
                    if (value.StartsWith("(") == false)
                        opText = "in (";
                    else
                        opText = "in ";
                    break;
                case enFilterCondition.Contains:
                    if (value.StartsWith("'") == false)
                        opText = "like '";
                    else
                        opText = "like ";
                    break;
                case enFilterCondition.StartsWith:
                    if (value.StartsWith("'") == false)
                        opText = "like '";
                    else
                        opText = "like ";
                    break;
                case enFilterCondition.EndsWith:
                    opText = "like ";
                    break;
                case enFilterCondition.IsNull:
                    opText = "is null ";
                    break;

                case enFilterCondition.NotEqualTo:
                    opText = "not = ";
                    break;
                case enFilterCondition.NotGreaterThan:
                    opText = "not > ";
                    break;
                case enFilterCondition.NotLessThan:
                    opText = "not < ";
                    break;
                case enFilterCondition.NotGreaterThanOrEqualTo:
                    opText = "< ";
                    break;
                case enFilterCondition.NotLessThanOrEqualTo:
                    opText = "> ";
                    break;
                case enFilterCondition.NotIn:
                    if (value.StartsWith("(") == false)
                        opText = "not in (";
                    else
                        opText = "not in ";
                    break;
                case enFilterCondition.DoesNotContain:
                    if (value.StartsWith("'") == false)
                        opText = "not like '";
                    else
                        opText = "not like ";
                    break;
                case enFilterCondition.DoesNotStartWith:
                    opText = "not like '";
                    break;
                case enFilterCondition.DoesNotEndWith:
                    opText = "not like ";
                    break;
                case enFilterCondition.IsNotNull:
                    opText = "is not null ";
                    break;

                
                
                default:
                    break;
            }

            //append the value text next

            switch (filterCondition)
            {
                case enFilterCondition.EqualTo:
                case enFilterCondition.GreaterThan:
                case enFilterCondition.LessThan:
                case enFilterCondition.GreaterThanOrEqualTo:
                case enFilterCondition.LessThanOrEqualTo:
                case enFilterCondition.NotEqualTo:
                case enFilterCondition.NotGreaterThan:
                case enFilterCondition.NotLessThan:
                case enFilterCondition.NotGreaterThanOrEqualTo:
                case enFilterCondition.NotLessThanOrEqualTo:
                    if (sourceDataType == "System.String")
                    {
                        if (valueEdited.StartsWith("'") == false)
                            valueEdited = "'" + valueEdited;
                        if (valueEdited.EndsWith("'") == false)
                            valueEdited = valueEdited + "'";
                    }
                    if (sourceDataType == "System.DateTime")
                    {
                        if (valueEdited.StartsWith("'") == false && valueEdited.StartsWith("#") == false)
                            valueEdited = "#" + valueEdited;
                        if (valueEdited.EndsWith("'") == false && valueEdited.EndsWith("#") == false)
                            valueEdited = valueEdited + "#";
                    }
                    valueText = valueEdited + " ";
                    break;
                case enFilterCondition.In:
                case enFilterCondition.NotIn:
                    if (value.EndsWith("(") == false)
                        valueText = valueEdited + ") ";
                    else
                        valueText = valueEdited + " ";
                    break;
                case enFilterCondition.Contains:
                case enFilterCondition.DoesNotContain:
                    if (value.EndsWith("'") == false)
                        valueText = valueEdited + "'";
                    else
                        valueText = valueEdited;
                    if (valueText.StartsWith("*") == false && valueText.StartsWith("%") == false)
                    {
                        valueText = "*" + valueText;
                    }
                    if (valueText.EndsWith("*'") == false && valueText.EndsWith("%'")==false)
                        valueText = valueText.TrimEnd(new char[] {'\''}) + "*'";
                    valueText = valueText + " ";
                    break;
                case enFilterCondition.StartsWith:
                case enFilterCondition.DoesNotStartWith:
                    if (value.EndsWith("'") == false)
                        valueText = valueEdited + "'";
                    else
                        valueText = valueEdited;
                    if (valueText.EndsWith("*'") == false && valueText.EndsWith("%'")==false)
                        valueText = valueText.TrimEnd(new char[] {'\''}) + "*' ";
                    else
                        valueText = valueText + " ";
                    break;
                case enFilterCondition.EndsWith:
                case enFilterCondition.DoesNotEndWith:
                    if (value.EndsWith("'") == false)
                        valueText = valueEdited + "'";
                    else
                        valueText = valueEdited;
                    if (valueText.StartsWith("'*") == false && valueText.StartsWith("'%") == false)
                        valueText = "'*" + valueText.TrimStart(new char[] { '\'' }).TrimStart(new char[] { '*' }) + " ";
                    else
                        valueText = valueText + " ";
                    break;
                case enFilterCondition.IsNull:
                case enFilterCondition.IsNotNull:
                    valueText = string.Empty; ;
                    break;

                default:
                    break;
            }

            opPlusValueText = opText + valueText;

            return opPlusValueText;
        }

    
    
    }//end class

    internal class OutputFilterItemComparer : IComparer
    {
        private int col;
        public OutputFilterItemComparer()
        {
            col = 0;
        }
        public OutputFilterItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }


}//end namespace
