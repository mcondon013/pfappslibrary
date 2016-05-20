using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using AppGlobals;
using PFPrinterObjects;
using PFSystemObjects;
using PFAppUtils;
using PFCollectionsObjects;
using PFDataGridViewFilterPopup;
using PFDataGridViewColumnSelector;
using PFTextFiles;
using PFDatabaseSelector;
using PFDataAccessObjects;
using PFMessageLogs;

namespace PFDataOutputGrid
{
    /// <summary>
    /// Class for managing the display of a data grid containing data from an ADO.NET DataTable.
    /// </summary>
    public partial class DataOutputGridFormExp : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";
        private DataView _gridDataView = null;
        private DataOutputGridExporter _gridExporter = new DataOutputGridExporter();
        private string _helpFilePath = string.Empty;

        private int _veryLargeDataTableRowCountThreshold = 100000;
        private int _maxDataTableRowsForWordOutput = 65536;
        private int _maxTempDataTableRows = 50000;

        //private fields for saving DataGridViewPrinter settings
        System.Drawing.Printing.PageSettings _savePageSettings = new System.Drawing.Printing.PageSettings();

        //private fields for processing file and folder dialogs
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\RandomData\DataGridExports\";
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = false;
        private string _saveFilter = "Text Files|*.txt|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = false;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        //private fields for properties
        private PFList<GridColumnFilter> _gridColumnFilters = new PFList<GridColumnFilter>();
        private bool _enableFiltersOnAllColumns = false;
        private bool _enableExportMenu = true;
        private bool _showInstalledDatabaseProvidersOnly = true;
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private string _defaultGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\RandomData\DataGridExports\";

        private MessageLog _messageLog = null;

        private string _pageTitle = AppGlobals.AppInfo.AssemblyDescription;
        private string _pageSubTitle = string.Empty;
        private string _pageFooter = AppGlobals.AppInfo.AssemblyProduct;
        private bool _showPageNumbers = true;
        private bool _showTotalPageNumber = true;


        /// <summary>
        /// Constructor.
        /// </summary>
        public DataOutputGridFormExp()
        {
            InitializeComponent();
        }

        //properties

        /// <summary>
        /// Returns a reference to the grid on the form.
        /// </summary>
        public DataGridView OutputDataGridView
        {
            get
            {
                return this.outputDataGrid;
            }
        }

        /// <summary>
        /// VeryLargeDataTableRowCountThreshold Property. Determines when temp file processing will be used for datagridview exporting.
        /// </summary>
        public int VeryLargeDataTableRowCountThreshold
        {
            get
            {
                return _veryLargeDataTableRowCountThreshold;
            }
            set
            {
                _veryLargeDataTableRowCountThreshold = value;
            }
        }

        /// <summary>
        /// MaxDataTableRowsForWordOutput Property. Specifies maximum number of DataTable rows allowed for output to a Word, PDF or RTF file.
        /// </summary>
        public int MaxDataTableRowsForWordOutput
        {
            get
            {
                return _maxDataTableRowsForWordOutput;
            }
            set
            {
                _maxDataTableRowsForWordOutput = value;
            }
        }

        /// <summary>
        /// List of columns that should have filter capabilities.
        /// </summary>
        public PFList<GridColumnFilter> GridColumnFilters
        {
            get
            {
                return _gridColumnFilters;
            }
            set
            {
                _gridColumnFilters = value;
            }
        }

        /// <summary>
        /// If true, then a filter will be enabled for each column on the grid, even if caller has ot asked for one.
        /// </summary>
        public bool EnableFiltersOnAllColumns
        {
            get
            {
                return _enableFiltersOnAllColumns;
            }
            set
            {
                _enableFiltersOnAllColumns = value;
            }
        }

        /// <summary>
        /// if true, menu to export grid data to external files is shown. Otherwise the menu is hidden.
        /// Default is true: export menu will be shown.
        /// </summary>
        public bool EnableExportMenu
        {
            get
            {
                return _enableExportMenu;
            }
            set
            {
                _enableExportMenu = value;
                if (_enableExportMenu)
                {
                    this.exportMenuToolStripSeparator.Visible = true;
                    this.mnuGridExportTo.Visible = true;
                }
                else
                {
                    this.exportMenuToolStripSeparator.Visible = false;
                    this.mnuGridExportTo.Visible = false;
                }
            }
        }

        /// <summary>
        /// Switch used by database selection controls to determine whether or not to list only installed database providers or to list all supported providers.
        /// </summary>
        public bool ShowInstalledDatabaseProvidersOnly
        {
            get
            {
                return _showInstalledDatabaseProvidersOnly;
            }
            set
            {
                _showInstalledDatabaseProvidersOnly = value;
            }
        }

        /// <summary>
        /// Default type of database to use for data grid exports to database tables.
        /// </summary>
        public string DefaultOutputDatabaseType
        {
            get
            {
                return _defaultOutputDatabaseType;
            }
            set
            {
                _defaultOutputDatabaseType = value;
            }
        }

        /// <summary>
        /// Default database connection string property to use for grid exports to database tables.
        /// </summary>
        public string DefaultOutputDatabaseConnectionString
        {
            get
            {
                return _defaultOutputDatabaseConnectionString;
            }
            set
            {
                _defaultOutputDatabaseConnectionString = value;
            }
        }

        /// <summary>
        /// Default folder for storing files created by grid export functions.
        /// </summary>
        public string DefaultGridExportFolder
        {
            get
            {
                return _defaultGridExportFolder;
            }
            set
            {
                _defaultGridExportFolder = value;
                _saveSelectionsFolder = value;
            }
        }

        /// <summary>
        /// Message log window manager.
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
        /// PageTitle Property.
        /// </summary>
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value;
            }
        }

        /// <summary>
        /// PageSubTitle Property.
        /// </summary>
        public string PageSubTitle
        {
            get
            {
                return _pageSubTitle;
            }
            set
            {
                _pageSubTitle = value;
            }
        }

        /// <summary>
        /// PageFooter Property.
        /// </summary>
        public string PageFooter
        {
            get
            {
                return _pageFooter;
            }
            set
            {
                _pageFooter = value;
            }
        }

        /// <summary>
        /// ShowPageNumbers Property.
        /// </summary>
        public bool ShowPageNumbers
        {
            get
            {
                return _showPageNumbers;
            }
            set
            {
                _showPageNumbers = value;
            }
        }

        /// <summary>
        /// ShowTotalPageNumber Property.
        /// </summary>
        public bool ShowTotalPageNumber
        {
            get
            {
                return _showTotalPageNumber;
            }
            set
            {
                _showTotalPageNumber = value;
            }
        }

        /// <summary>
        /// Page settings to use when printing.
        /// </summary>
        public System.Drawing.Printing.PageSettings SavePageSettings
        {
            get
            {
                return _savePageSettings;
            }
            set
            {
                _savePageSettings = value;
            }
        }

        //button click events

        private void cmdExit_Click(object sender, EventArgs e)
        {
            HideForm();
        }
        
        //menu clicks
        private void mnuGridEditCopy_Click(object sender, EventArgs e)
        {
            CopySelectedCellsToClipBoard();
        }

        private void mnuGridPageSetup_Click(object sender, EventArgs e)
        {
            ShowPageSettings();
        }

        private void mnuGridPrintPreview_Click(object sender, EventArgs e)
        {
            ShowPrintPreview();
        }

        private void mnuGridPrint_Click(object sender, EventArgs e)
        {
            ShowPrintDialog();
        }

        private void mnuGridExit_Click(object sender, EventArgs e)
        {
            HideForm();
        }

        private void mnuGridExportToExcelFileXLSXFormat_Click(object sender, EventArgs e)
        {
            ExportToExcelFileXLSXFormat();
        }

        private void mnuGridExportToExcelFileXLSFormat_Click(object sender, EventArgs e)
        {
            ExportToExcelFileXLSFormat();
        }

        private void mnuGridExportToExcelFileCSVFormat_Click(object sender, EventArgs e)
        {
            ExportToExcelFileCSVFormat();
        }

        private void mnuGridExportToTextFileDelimitedFormat_Click(object sender, EventArgs e)
        {
            ExportToTextFileDelimitedFormat();
        }

        private void mnuGridExportToTextFileFixedLengthFormat_Click(object sender, EventArgs e)
        {
            ExportToTextFileFixedLengthFormat();
        }

        private void mnuGridExportToAccessFileAccdbFormat_Click(object sender, EventArgs e)
        {
            ExportToAccessFileAccdbFormat();
        }

        private void mnuGridExportToAccessFileMdbFormat_Click(object sender, EventArgs e)
        {
            ExportToAccessFileMdbFormat();
        }

        private void mnuGridExportToWordFileDocxFormat_Click(object sender, EventArgs e)
        {
            ExportToWordDocxFormat();
        }

        private void mnuGridExportToWordFileDocFormat_Click(object sender, EventArgs e)
        {
            ExportToWordDocFormat();
        }

        private void mnuGridExportToRtfFile_Click(object sender, EventArgs e)
        {
            ExportToRtfFile();
        }

        private void mnuGridExportToPdfFile_Click(object sender, EventArgs e)
        {
            ExportToPdfFile();
        }

        private void mnuGridExportToXmlFile_Click(object sender, EventArgs e)
        {
            ExportToXmlFile();
        }

        private void mnuGridExportToDatabaseTable_Click(object sender, EventArgs e)
        {
            ExportToDatabaseTable();
        }

        private void toolbarHelp_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void outputDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView view = (DataGridView)sender;
            if (e.Exception != null)
            {
                view.Rows[e.RowIndex].ErrorText = e.Exception.Message;
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = e.Exception.Message;
            }
            else
            {
                view.Rows[e.RowIndex].ErrorText = "<data error>";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "<data error>";
            }

            e.ThrowException = false;
        }




        //common form processing routines
        /// <summary>
        /// Routines to setup the form.
        /// </summary>
        public void InitializeForm()
        {
            this.txtExportFinishedMessage.Text = string.Empty;
            _veryLargeDataTableRowCountThreshold = AppGlobals.AppConfig.GetIntValueFromConfigFile("VeryLargeDataTableRowCountThreshold", 100000);
            _maxDataTableRowsForWordOutput = AppGlobals.AppConfig.GetIntValueFromConfigFile("MaxDataTableRowsForWordOutput", 100000);
            _maxTempDataTableRows = AppGlobals.AppConfig.GetIntValueFromConfigFile("MaxTempDataTableRows", 50000);
            SetHelpFileValues();
            GetLoggingSettings();
            InitGridOutputFolder();
            SetGridDataView();
            SetColumnFilters();
            SetColumnSelectors();
            SetExportFields();
            EnableFormControls();
            this.Focus();
        }

        private void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("DataOutputGridHelpFileName", "DataOutputGrid.chm");
            string helpFilePath = Path.Combine(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        private void GetLoggingSettings()
        {
            _saveErrorMessagesToErrorLog = AppConfig.GetBooleanValueFromConfigFile("SaveErrorMessagesToErrorLog", "false");
            _applicationLogFileName = System.Configuration.ConfigurationManager.AppSettings["ApplicationLogFileName"];

        }

        private void InitGridOutputFolder()
        {
            if (Directory.Exists(_defaultGridExportFolder) == false)
            {
                Directory.CreateDirectory(_defaultGridExportFolder);
            }
        }

        private void SetGridDataView()
        {
            DataSet ds = (DataSet)this.DataGridBindingSource.DataSource;
            _gridDataView = ds.Tables[this.DataGridBindingSource.DataMember].DefaultView;
        }

        private void SetColumnFilters()
        {
            DgvFilterManager fm = new DgvFilterManager();

            // Using the ColumnFilterAdding event, you may force your preferred filter,
            // BEFORE the FilterManager create the predefined filter. This event is 
            // raised for each column in the grid when you set the DataGridView property 
            // of the FilterManager.
            fm.ColumnFilterAdding += new ColumnFilterEventHandler(fm_ColumnFilterAdding);

            fm.DataGridView = this.outputDataGrid; // this raises ColumnFilterAdding events

        }

        private void SetColumnSelectors()
        {
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(this.OutputDataGridView);
            cs.MaxHeight = 100;
            cs.Width = 110;
        }

        private void SetExportFields()
        {
            string configValue = AppConfig.GetStringValueFromConfigFile("DefaultGridExportFileLocation", string.Empty);
            if (configValue != string.Empty)
                _saveSelectionsFolder = configValue;
        }

        private void fm_ColumnFilterAdding(object sender, ColumnFilterEventArgs e)
        {
            foreach (GridColumnFilter filter in this.GridColumnFilters)
            {
                if (filter.ColName == e.Column.Name)
                {
                    switch (filter.ColFilterType)
                    {
                        case enFilterType.TextBoxColumnFilter:
                            e.ColumnFilter = new DgvTextBoxColumnFilter();
                            break;
                        case enFilterType.ComboBoxColumnFilter:
                            e.ColumnFilter = new DgvComboBoxColumnFilter();
                            break;
                        case enFilterType.CheckBoxColumnFilter:
                            e.ColumnFilter = new DgvCheckBoxColumnFilter();
                            break;
                        case enFilterType.DateColumnFilter:
                            e.ColumnFilter = new DgvDateColumnFilter();
                            break;
                        case enFilterType.DateRangeColumnFilter:
                            e.ColumnFilter = new DgvDateRangeColumnFilter();
                            break;
                        case enFilterType.MonthYearColumnFilter:
                            e.ColumnFilter = new DgvMonthYearColumnFilter();
                            break;
                        case enFilterType.NumRangeColumnFilter:
                            e.ColumnFilter = new DgvNumRangeColumnFilter();
                            break;
                        default:
                            if (_gridDataView != null)
                            {
                                Type dataType = _gridDataView.Table.Columns[e.Column.Name].DataType;
                                if (dataType == typeof(DateTime))
                                {
                                    e.ColumnFilter = new DgvDateColumnFilter();
                                }
                                else
                                    e.ColumnFilter = new DgvTextBoxColumnFilter();
                            }
                            else
                            {
                                e.ColumnFilter = new DgvTextBoxColumnFilter();
                            }
                            break;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Method to hide the form.
        /// </summary>
        public void HideForm()
        {
            this.Hide();
        }

        /// <summary>
        /// Method to close the form.
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;

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

            }//end foreach control

        }

        //routines for processing file open, file save and folder browser dialogs
        private DialogResult ShowOpenFileDialog()
        {
            DialogResult res = DialogResult.None;
            _openFileDialog.InitialDirectory = _saveSelectionsFolder;
            _openFileDialog.FileName = _saveSelectionsFile;
            _openFileDialog.Filter = _saveFilter;
            _openFileDialog.FilterIndex = _saveFilterIndex;
            _openFileDialog.Multiselect = _saveMultiSelect;
            _saveSelectionsFile = string.Empty;
            _saveSelectedFiles = null;
            res = _openFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(_openFileDialog.FileName);
                _saveSelectionsFile = _openFileDialog.FileName;
                _saveFilterIndex = _openFileDialog.FilterIndex;
                if (_openFileDialog.Multiselect)
                {
                    _saveSelectedFiles = _openFileDialog.FileNames;
                }
            }
            return res;
        }

        private DialogResult ShowSaveFileDialog()
        {
            DialogResult res = DialogResult.None;
            _saveFileDialog.InitialDirectory = _saveSelectionsFolder;
            _saveFileDialog.FileName = _saveSelectionsFile;
            _saveFileDialog.Filter = _saveFilter;
            _saveFileDialog.FilterIndex = _saveFilterIndex;
            _saveFileDialog.CreatePrompt = _showCreatePrompt;
            _saveFileDialog.OverwritePrompt = _showOverwritePrompt;
            res = _saveFileDialog.ShowDialog();
            _saveSelectionsFile = string.Empty;
            if (res == DialogResult.OK)
            {
                _saveSelectionsFolder = Path.GetDirectoryName(_saveFileDialog.FileName);
                _saveSelectionsFile = _saveFileDialog.FileName;
                _saveFilterIndex = _saveFileDialog.FilterIndex;
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
            _folderBrowserDialog.ShowNewFolderButton = _showNewFolderButton;
            //_folderBrowserDialog.RootFolder = 
            _folderBrowserDialog.SelectedPath = folderPath;
            res = _folderBrowserDialog.ShowDialog();
            if (res != DialogResult.Cancel)
            {
                folderPath = _folderBrowserDialog.SelectedPath;
                _str.Length = 0;
                _str.Append(folderPath);
                if (folderPath.EndsWith(@"\") == false)
                    _str.Append(@"\");
                _saveSelectionsFolder = folderPath;
            }


            return res;
        }



        private void DataOutputGridFormExp_Shown(object sender, EventArgs e)
        {
            int rowCount = outputDataGrid.RowCount;
            lblTotalNumberOfRows.Text = "Total Number of Rows = " + rowCount.ToString("#,##0");
        }

        //Application routines

        /// <summary>
        /// Displays printer page settings dialog.
        /// </summary>
        public void ShowPageSettings()
        {
            PFDataGridViewPrinter _dgvPrinter = new PFDataGridViewPrinter();
            InitDataGridViewPrinter(_dgvPrinter);
            _dgvPrinter.ShowPageSettingsDialog();
            _savePageSettings = _dgvPrinter.Printer.printDoc.DefaultPageSettings;
        }

        /// <summary>
        /// Displays print preview window.
        /// </summary>
        public void ShowPrintPreview()
        {
            GridViewPrint(true);
        }

        /// <summary>
        /// Shows print dialog.
        /// </summary>
        public void ShowPrintDialog()
        {
            GridViewPrint(false);
        }

        /// <summary>
        /// Prints contents of grid.
        /// </summary>
        /// <param name="showPreview">Set to true to display the output in a preview window.</param>
        public void GridViewPrint(bool showPreview)
        {
            GridViewPrint(showPreview, true);
        }

        /// <summary>
        /// Prints contents of grid.
        /// </summary>
        /// <param name="showPreview">Set to true to display the output in a preview window.</param>
        /// <param name="showPrintDialog">Set to true to display a printer selection dialog before printing.</param>
        public void GridViewPrint(bool showPreview, bool showPrintDialog)
        {

            try
            {
                if (PFSystemObjects.SysInfo.DefaultPrinterIsDefined() == false)
                {
                    _msg.Length = 0;
                    _msg.Append("No default printer specified. You must specify a default printer for print routines to work.");
                    throw new System.Exception(_msg.ToString());
                }

                PFDataGridViewPrinter _dgvPrinter = new PFDataGridViewPrinter();
                InitDataGridViewPrinter(_dgvPrinter);
                if (showPreview)
                {
                    _dgvPrinter.PrintPreview();
                }
                else
                {
                    if (showPrintDialog)
                    {
                        _dgvPrinter.Print(true);
                    }
                    else
                    {
                        _dgvPrinter.Print(false);
                    }
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToErrorLog);
            }
            finally
            {
                ;
            }


        }

        private void InitDataGridViewPrinter(PFDataGridViewPrinter dgvPrinter)
        {
            dgvPrinter.Grid = (DataGridView)this.outputDataGrid;

            dgvPrinter.Printer.printDoc.DefaultPageSettings = _savePageSettings;
            dgvPrinter.Printer.printDoc.DefaultPageSettings.PaperSource.RawKind = (int)System.Drawing.Printing.PaperSourceKind.AutomaticFeed;

            //dgvPrinter.Title = AppGlobals.AppInfo.AssemblyDescription;
            //dgvPrinter.SubTitle = "Data output from " + this.DataGridBindingSource.DataMember;
            //dgvPrinter.Footer = AppGlobals.AppInfo.AssemblyProduct;
            //dgvPrinter.PageNumbers = true;
            //dgvPrinter.ShowTotalPageNumber = true;

            dgvPrinter.Title = this.PageTitle;
            if (this.PageSubTitle == string.Empty)
            {
                dgvPrinter.SubTitle = "Data output from " + this.DataGridBindingSource.DataMember;
            }
            else
            {
                dgvPrinter.SubTitle = this.PageSubTitle;
            }
            dgvPrinter.Footer = this.PageFooter;
            dgvPrinter.PageNumbers = this.ShowPageNumbers;
            dgvPrinter.ShowTotalPageNumber = this.ShowTotalPageNumber;

        }


        private void CopySelectedCellsToClipBoard()
        {
            DataGridView tempdgv = (DataGridView)this.outputDataGrid;
            DataObject dataObj = tempdgv.GetClipboardContent();
            try
            {
                Clipboard.SetDataObject(dataObj, true);
            }
            catch (Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }
        }

        private void ExportToExcelFileXLSXFormat()
        {
            PFList<string> dtList = new PFList<string>();

            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".xlsx";
                    else
                        _saveSelectionsFile = "GridDataExport.xlsx";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.xlsx";
                }
                _saveFilter = "Excel 2007 Files|*.xlsx|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to Excel XLSX file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                    {
                        //run processing that will use temp files for outputting
                        //dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                _gridExporter.ExportToExcelFile(dtList, PFDocumentGlobals.enExcelOutputFormat.Excel2007, _saveSelectionsFile, true);
                                this.txtExportFinishedMessage.Text = "Export to Excel XLSX file succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to Excel XLSX file failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Excel XLSX file failed.";
                        }
                    }
                    else
                    {
                        dgvTab = _gridDataView.Table;
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToExcelFile(dgvTab, PFDocumentGlobals.enExcelOutputFormat.Excel2007, _saveSelectionsFile, true);
                            this.txtExportFinishedMessage.Text = "Export to Excel XLSX file succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Excel XLSX file failed.";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Excel XLSX file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                if (dtList != null)
                    DeleteTempFiles(dtList);
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToExcelFileXLSFormat()
        {
            PFList<string> dtList = new PFList<string>();

            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".xls";
                    else
                        _saveSelectionsFile = "GridDataExport.xls";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.xls";
                }
                _saveFilter = "Excel 2003 Files|*.xls|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to Excel XLS file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                    {
                        //run processing that will use temp files for outputting
                        //dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                _gridExporter.ExportToExcelFile(dtList, PFDocumentGlobals.enExcelOutputFormat.Excel2003, _saveSelectionsFile, true);
                                this.txtExportFinishedMessage.Text = "Export to Excel XLS file succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to Excel XLS file failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Excel XLS file failed.";
                        }
                    }
                    else
                    {
                        dgvTab = _gridDataView.Table;
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToExcelFile(dgvTab, PFDocumentGlobals.enExcelOutputFormat.Excel2003, _saveSelectionsFile, true);
                            this.txtExportFinishedMessage.Text = "Export to Excel XLS file succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Excel XLS file failed.";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Excel XLS file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                if (dtList != null)
                    DeleteTempFiles(dtList);
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        
        }

        private void ExportToExcelFileCSVFormat()
        {
            PFList<string> dtList = new PFList<string>();

            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".csv";
                    else
                        _saveSelectionsFile = "GridDataExport.csv";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.csv";
                }
                _saveFilter = "Excel CSV Files|*.csv|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to Excel CSV file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                    {
                        //run processing that will use temp files for outputting
                        //dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                //_gridExporter.ExportToDelimitedTextFile(dtList, _saveSelectionsFile, true, columnDelimiter, lineTerminator, includeColumnNamesInOutput);
                                _gridExporter.ExportToExcelFile(dtList, PFDocumentGlobals.enExcelOutputFormat.CSV, _saveSelectionsFile, true);
                                this.txtExportFinishedMessage.Text = "Export to Excel CSV file succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to Excel CSV file failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Excel CSV file failed.";
                        }
                    }
                    else
                    {
                        dgvTab = _gridDataView.Table;
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToExcelFile(dgvTab, PFDocumentGlobals.enExcelOutputFormat.CSV, _saveSelectionsFile, true);
                            this.txtExportFinishedMessage.Text = "Export to Excel CSV file succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Excel CSV file failed.";
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Excel CSV file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                if (dtList != null)
                    DeleteTempFiles(dtList);
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToTextFileDelimitedFormat()
        {
            DialogResult res = DialogResult.None;
            string columnDelimiter = string.Empty;
            string lineTerminator = string.Empty;
            bool includeColumnNamesInOutput = true;
            PFDataDelimitersPrompt delimitersPrompt = new PFDataDelimitersPrompt();
            PFList<string> dtList = new PFList<string>();


            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                res = delimitersPrompt.ShowDialog();
                if (res != DialogResult.OK)
                {
                    delimitersPrompt.Close();
                    return;
                }
                columnDelimiter = delimitersPrompt.ColumnDelimiter;
                lineTerminator = delimitersPrompt.LineTerminator;
                includeColumnNamesInOutput = delimitersPrompt.IncludeColumnHeadersInOutput;
                delimitersPrompt.Close();

                //DataTable dt = _gridDataView.ToTable();
                //if (String.IsNullOrEmpty(dt.TableName) == false)
                //    _saveSelectionsFile = dt.TableName + ".txt";
                //else
                //    _saveSelectionsFile = "GridDataExport.txt";
                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".txt";
                    else
                    {
                        _gridDataView.Table.TableName = "GridDataExport";
                        _saveSelectionsFile = "GridDataExport.txt";
                    }
                }
                else
                {
                    _gridDataView.Table.TableName = "GridDataExport";
                    _saveSelectionsFile = "GridDataExport.txt";
                }
                _saveFilter = "Text Files|*.txt|All Files|*.*"; ;
                res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to delimited file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                    {
                        //run processing that will use temp files for outputting
                        //dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                _gridExporter.ExportToDelimitedTextFile(dtList, _saveSelectionsFile, true, columnDelimiter, lineTerminator, includeColumnNamesInOutput);
                                this.txtExportFinishedMessage.Text = "Export to delimited text file succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to delimited text file failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to delimited text file failed.";
                        }
                    }
                    else
                    {
                        dgvTab = _gridDataView.Table;
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToDelimitedTextFile(dgvTab, _saveSelectionsFile, true, columnDelimiter, lineTerminator, includeColumnNamesInOutput);
                            this.txtExportFinishedMessage.Text = "Export to delimited text file succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to delimited text file failed.";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to delimited data file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (dtList != null)
                    DeleteTempFiles(dtList);
                if (delimitersPrompt != null)
                    delimitersPrompt = null;
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToTextFileFixedLengthFormat()
        {
            DialogResult res = DialogResult.None;
            bool includeColumnNamesInOutput = true;
            bool allowDataTruncation = false;
            bool useLineTerminator = true;
            string lineTerminatorChars = Environment.NewLine;
            int columnWidthForStringData = 255;
            int maximumAllowedColumnWidth = 1024;
            PFFixedLengthDataPrompt fixedLengthDataLinePrompt = new PFFixedLengthDataPrompt();
            PFList<string> dtList = new PFList<string>();

            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                res = fixedLengthDataLinePrompt.ShowDialog();
                if (res != DialogResult.OK)
                {
                    fixedLengthDataLinePrompt.Close();
                    return;
                }
                includeColumnNamesInOutput = fixedLengthDataLinePrompt.IncludeColumnHeadersInOutput;
                allowDataTruncation = fixedLengthDataLinePrompt.AllowDataTruncation;
                useLineTerminator = fixedLengthDataLinePrompt.UseLineTerminator;
                lineTerminatorChars = fixedLengthDataLinePrompt.LineTerminatorChars;
                columnWidthForStringData = fixedLengthDataLinePrompt.ColumnWidthForStringData;
                maximumAllowedColumnWidth = fixedLengthDataLinePrompt.MaximumAllowedColumnWidth;
                fixedLengthDataLinePrompt.Close();

                //DataTable dt = _gridDataView.ToTable();
                //if (String.IsNullOrEmpty(dt.TableName) == false)
                //    _saveSelectionsFile = dt.TableName + ".dat";
                //else
                //    _saveSelectionsFile = "GridDataExport.dat";
                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".dat";
                    else
                        _saveSelectionsFile = "GridDataExport.dat";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.dat";
                }
                _saveFilter = "Fixed Length Data Files|*.dat|Text Files|*.txt|All Files|*.*"; ;
                res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to fixed length text file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    //if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                    //    dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    //else
                    //    dgvTab = _gridDataView.Table;
                    //if (dgvTab != null)
                    //{
                    //    _gridExporter.ExportToFixedLengthTextFile(dgvTab, _saveSelectionsFile, true, includeColumnNamesInOutput, allowDataTruncation, useLineTerminator, lineTerminatorChars, columnWidthForStringData, maximumAllowedColumnWidth);
                    //    this.txtExportFinishedMessage.Text = "Export to fixed length text file succeeded.";
                    //}
                    //else
                    //{
                    //    this.txtExportFinishedMessage.Text = "Export to fixed length text file failed.";
                    //}
                    if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                    {
                        //run processing that will use temp files for outputting
                        //dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                _gridExporter.ExportToFixedLengthTextFile(dtList, _saveSelectionsFile, true, includeColumnNamesInOutput, allowDataTruncation, useLineTerminator, lineTerminatorChars, columnWidthForStringData, maximumAllowedColumnWidth);
                                this.txtExportFinishedMessage.Text = "Export to fixed length text file succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to fixed length text file failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to fixed length text file failed.";
                        }
                    }
                    else
                    {
                        dgvTab = _gridDataView.Table;
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToFixedLengthTextFile(dgvTab, _saveSelectionsFile, true, includeColumnNamesInOutput, allowDataTruncation, useLineTerminator, lineTerminatorChars, columnWidthForStringData, maximumAllowedColumnWidth);
                            this.txtExportFinishedMessage.Text = "Export to fixed length text file succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to fixed length text file failed.";
                        }
                    }

                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Fixed Length data file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (dtList != null)
                    DeleteTempFiles(dtList);
                if (fixedLengthDataLinePrompt != null)
                    fixedLengthDataLinePrompt = null;
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }


        
        }

        private void ExportToAccessFileAccdbFormat()
        {
            PFList<string> dtList = new PFList<string>();

            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                //DataTable dt = _gridDataView.ToTable();
                //if (String.IsNullOrEmpty(dt.TableName) == false)
                //    _saveSelectionsFile = dt.TableName + ".accdb";
                //else
                //    _saveSelectionsFile = "GridDataExport.accdb";
                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".accdb";
                    else
                        _saveSelectionsFile = "GridDataExport.accdb";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.accdb";
                }

                _saveFilter = "Access 2007 Files|*.accdb|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to Access ACCDB file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    //if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                    //    dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    //else
                    //    dgvTab = _gridDataView.Table;
                    //if (dgvTab != null)
                    //{
                    //    _gridExporter.ExportToAccessFile(dgvTab, _saveSelectionsFile, true, PFDataOutputProcessor.enAccessVersion.Access2007, "admin", string.Empty);
                    //    this.txtExportFinishedMessage.Text = "Export to Access ACCDB file succeeded.";
                    //}
                    //else
                    //{
                    //    this.txtExportFinishedMessage.Text = "Export to Access ACCDB file failed.";
                    //}

                    if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                    {
                        //run processing that will use temp files for outputting
                        //dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                _gridExporter.ExportToAccessFile(dtList, _saveSelectionsFile, true, PFDataOutputProcessor.enAccessVersion.Access2007, "admin", string.Empty);
                                this.txtExportFinishedMessage.Text = "Export to Access ACCDB file succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to Access ACCDB file failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Access ACCDB file failed.";
                        }
                    }
                    else
                    {
                        dgvTab = _gridDataView.Table;
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToAccessFile(dgvTab, _saveSelectionsFile, true, PFDataOutputProcessor.enAccessVersion.Access2007, "admin", string.Empty);
                            this.txtExportFinishedMessage.Text = "Export to Access ACCDB file succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Access ACCDB file failed.";
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Access ACCDB file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                if (dtList != null)
                    DeleteTempFiles(dtList);
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToAccessFileMdbFormat()
        {
            PFList<string> dtList = new PFList<string>();

            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                //DataTable dt = _gridDataView.ToTable();
                //if (String.IsNullOrEmpty(dt.TableName) == false)
                //    _saveSelectionsFile = dt.TableName + ".mdb";
                //else
                //    _saveSelectionsFile = "GridDataExport.mdb";
                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".mdb";
                    else
                        _saveSelectionsFile = "GridDataExport.mdb";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.mdb";
                }

                _saveFilter = "Access 2003 Files|*.mdb|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to Access MDB file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    //if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                    //    dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    //else
                    //    dgvTab = _gridDataView.Table;
                    //if (dgvTab != null)
                    //{
                    //    _gridExporter.ExportToAccessFile(dgvTab, _saveSelectionsFile, true, PFDataOutputProcessor.enAccessVersion.Access2003, "admin", string.Empty);
                    //    this.txtExportFinishedMessage.Text = "Export to Access MDB file succeeded.";
                    //}
                    //else
                    //{
                    //    this.txtExportFinishedMessage.Text = "Export to Access MDB file failed.";
                    //}

                    if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                    {
                        //run processing that will use temp files for outputting
                        //dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                _gridExporter.ExportToAccessFile(dtList, _saveSelectionsFile, true, PFDataOutputProcessor.enAccessVersion.Access2003, "admin", string.Empty);
                                this.txtExportFinishedMessage.Text = "Export to Access MDB file succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to Access MDB file failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Access MDB file failed.";
                        }
                    }
                    else
                    {
                        dgvTab = _gridDataView.Table;
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToAccessFile(dgvTab, _saveSelectionsFile, true, PFDataOutputProcessor.enAccessVersion.Access2003, "admin", string.Empty);
                            this.txtExportFinishedMessage.Text = "Export to Access MDB file succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to Access MDB file failed.";
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Access MDB file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                if (dtList != null)
                    DeleteTempFiles(dtList);
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToWordDocxFormat()
        {
            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                //DataTable dt = _gridDataView.ToTable();
                //if (String.IsNullOrEmpty(dt.TableName) == false)
                //    _saveSelectionsFile = dt.TableName + ".docx";
                //else
                //    _saveSelectionsFile = "GridDataExport.docx";
                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".docx";
                    else
                        _saveSelectionsFile = "GridDataExport.docx";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.docx";
                }
                _saveFilter = "Word 2007 Files|*.docx|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to Word DOCX file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                        dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    else
                        dgvTab = _gridDataView.Table;
                    if (dgvTab != null)
                    {
                        if (dgvTab.Rows.Count <= this.MaxDataTableRowsForWordOutput)
                        {
                            _gridExporter.ExportToWordFile(dgvTab, _saveSelectionsFile, true, PFDocumentGlobals.enWordOutputFormat.Word2007);
                            this.txtExportFinishedMessage.Text = "Export to Word DOCX file succeeded.";
                        }
                        else
                        {
                            _msg.Length = 0;
                            _msg.Append("Export to Word DOCX file failed. Output row count is greater than maximum allowed rows for Word output (");
                            _msg.Append(this.MaxDataTableRowsForWordOutput.ToString("#,##0"));
                            _msg.Append(")."); ;
                            AppMessages.DisplayErrorMessage(_msg.ToString());
                            this.txtExportFinishedMessage.Text = "Export to Word DOCX file failed.";
                        }
                    }
                    else
                    {
                        this.txtExportFinishedMessage.Text = "Export to Word DOCX file failed.";
                    }
                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Word DOCX file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToWordDocFormat()
        {
            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                //DataTable dt = _gridDataView.ToTable();
                //if (String.IsNullOrEmpty(dt.TableName) == false)
                //    _saveSelectionsFile = dt.TableName + ".doc";
                //else
                //    _saveSelectionsFile = "GridDataExport.doc";
                _saveFilter = "Word 2003 Files|*.doc|All Files|*.*"; ;
                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".doc";
                    else
                        _saveSelectionsFile = "GridDataExport.doc";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.doc";
                }
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to Word DOC file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                        dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    else
                        dgvTab = _gridDataView.Table;
                    if (dgvTab != null)
                    {
                        if (dgvTab.Rows.Count <= this.MaxDataTableRowsForWordOutput)
                        {
                            _gridExporter.ExportToWordFile(dgvTab, _saveSelectionsFile, true, PFDocumentGlobals.enWordOutputFormat.Word2003);
                            this.txtExportFinishedMessage.Text = "Export to Word DOC file succeeded.";
                        }
                        else
                        {
                            _msg.Length = 0;
                            _msg.Append("Export to Word DOC file failed. Output row count is greater than maximum allowed rows for Word output (");
                            _msg.Append(this.MaxDataTableRowsForWordOutput.ToString("#,##0"));
                            _msg.Append(")."); ;
                            AppMessages.DisplayErrorMessage(_msg.ToString());
                            this.txtExportFinishedMessage.Text = "Export to Word DOC file failed.";
                        }
                    }
                    else
                    {
                        this.txtExportFinishedMessage.Text = "Export to Word DOC file failed.";
                    }
                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Word DOC file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }
        
        }

        private void ExportToRtfFile()
        {
            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".rtf";
                    else
                        _saveSelectionsFile = "GridDataExport.rtf";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.rtf";
                }
                _saveFilter = "Rtf Files|*.rtf|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to RTF file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                        dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    else
                        dgvTab = _gridDataView.Table;
                    if (dgvTab != null)
                    {
                        if (dgvTab.Rows.Count <= this.MaxDataTableRowsForWordOutput)
                        {
                            _gridExporter.ExportToRtfFile(dgvTab, _saveSelectionsFile, true);
                            this.txtExportFinishedMessage.Text = "Export to RTF file succeeded.";
                        }
                        else
                        {
                            _msg.Length = 0;
                            _msg.Append("Export to Word RTF file failed. Output row count is greater than maximum allowed rows for RTF output (");
                            _msg.Append(this.MaxDataTableRowsForWordOutput.ToString("#,##0"));
                            _msg.Append(")."); ;
                            AppMessages.DisplayErrorMessage(_msg.ToString());
                            this.txtExportFinishedMessage.Text = "Export to RTF file failed.";
                        }
                    }
                    else
                    {
                        this.txtExportFinishedMessage.Text = "Export to RTF file succeeded.";
                    }
                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to Word RTF file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToPdfFile()
        {
            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                //DataTable dt = _gridDataView.ToTable();
                //if (String.IsNullOrEmpty(dt.TableName) == false)
                //    _saveSelectionsFile = dt.TableName + ".pdf";
                //else
                //    _saveSelectionsFile = "GridDataExport.pdf";
                //_saveFilter = "Pdf Files|*.pdf|All Files|*.*"; ;
                if (_gridDataView.Table != null)
                {
                    if (String.IsNullOrEmpty(_gridDataView.Table.TableName) == false)
                        _saveSelectionsFile = _gridDataView.Table.TableName + ".pdf";
                    else
                        _saveSelectionsFile = "GridDataExport.pdf";
                }
                else
                {
                    _saveSelectionsFile = "GridDataExport.pdf";
                }
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to PDF file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = null;
                    if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                        dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    else
                        dgvTab = _gridDataView.Table;
                    if (dgvTab != null)
                    {
                        if (dgvTab.Rows.Count <= this.MaxDataTableRowsForWordOutput)
                        {
                            _gridExporter.ExportToPdfFile(dgvTab, _saveSelectionsFile, true);
                            this.txtExportFinishedMessage.Text = "Export to PDF file succeeded.";
                        }
                        else
                        {
                            _msg.Length = 0;
                            _msg.Append("Export to Word PDF file failed. Output row count is greater than maximum allowed rows for PDF output (");
                            _msg.Append(this.MaxDataTableRowsForWordOutput.ToString("#,##0"));
                            _msg.Append(")."); ;
                            AppMessages.DisplayErrorMessage(_msg.ToString());
                            this.txtExportFinishedMessage.Text = "Export to PDF file failed.";
                        }
                    }
                    else
                    {
                        this.txtExportFinishedMessage.Text = "Export to PDF file failed.";
                    }
                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to PDF file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToXmlFile()
        {
            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                DataTable dt = _gridDataView.ToTable();
                if (String.IsNullOrEmpty(dt.TableName) == false)
                    _saveSelectionsFile = dt.TableName + ".xml";
                else
                    _saveSelectionsFile = "GridDataExport.xml";
                _saveFilter = "Xml Files|*.xml|All Files|*.*"; ;
                DialogResult res = ShowSaveFileDialog();
                if (res == DialogResult.OK)
                {
                    this.txtExportFinishedMessage.Text = "Exporting to XML file ...";
                    this.Text = this.Text + " Exporting ...";
                    DisableFormControls();
                    this.Cursor = Cursors.WaitCursor;
                    DataTable dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                    _gridExporter.ExportToXmlFile(dgvTab, _saveSelectionsFile, true);
                    this.txtExportFinishedMessage.Text = "Export to XML file succeeded.";
                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to XML formatted file.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                EnableFormControls();
                this.Text = this.Text.Replace(" Exporting ...", string.Empty);
                this.Cursor = Cursors.Default;
            }

        }

        private void ExportToDatabaseTable()
        {
            DialogResult res = DialogResult.None;
            DatabaseSelectorForm dbSelectorForm = new DatabaseSelectorForm();
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            string configValue = string.Empty;
            string connectionString = string.Empty;
            string outputTableName = string.Empty;
            int outputBatchSize = 100;
            bool replaceExistingTable = false;
            PFList<string> dtList = new PFList<string>();

            try
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                this.txtExportFinishedMessage.Refresh();

                ////default database platform will be read from config file in the dbSelectorForm initializing routines
                //configValue = AppConfig.GetStringValueFromConfigFile("DefaultOutputDatabasePlatform",string.Empty);
                //if (configValue.Length > 0)
                //    dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), configValue);
                //dbSelectorForm.DatabaseType = dbPlat;
                dbSelectorForm.ShowInstalledDatabaseProvidersOnly = this.ShowInstalledDatabaseProvidersOnly;
                dbSelectorForm.DefaultOutputDatabaseType = this.DefaultOutputDatabaseType;
                dbSelectorForm.DefaultOutputDatabaseConnectionString = this.DefaultOutputDatabaseConnectionString;

                res = dbSelectorForm.ShowDialog();
                if (res != DialogResult.OK)
                {
                    dbSelectorForm.Close();
                    return;
                }

                DisableFormControls();

                this.txtExportFinishedMessage.Text = "Exporting to database ...";
                this.txtExportFinishedMessage.Refresh();

                dbPlat = dbSelectorForm.DatabaseType;
                connectionString = dbSelectorForm.ConnectionString;
                outputTableName = dbSelectorForm.OutputTableName;
                outputBatchSize = dbSelectorForm.OutputBatchSize;
                replaceExistingTable = dbSelectorForm.ReplaceExistingTable;

                //DataTable dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                //_gridExporter.ExportToDatabaseTable(dgvTab, dbPlat, connectionString, outputTableName, outputBatchSize, replaceExistingTable);
                //this.txtExportFinishedMessage.Text = "Export to database succeeded.";

                DataTable dgvTab = null;
                //if (GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid))
                //    dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                //else
                //    dgvTab = _gridDataView.Table;
                //if (dgvTab != null)
                //{
                //    _gridExporter.ExportToDatabaseTable(dgvTab, dbPlat, connectionString, outputTableName, outputBatchSize, replaceExistingTable);
                //    this.txtExportFinishedMessage.Text = "Export to database succeeded.";
                //}
                //else
                //{
                //    this.txtExportFinishedMessage.Text = "Export to database failed.";
                //}

                if ((GridIsShowingSubsetOfTableData(_gridDataView.Table, this.outputDataGrid)) || (_gridDataView.Table.Rows.Count > this.VeryLargeDataTableRowCountThreshold))
                {
                    //run processing that will use temp files for outputting
                    if (RowNumberColumnDefined(_gridDataView.Table))
                    {
                        dtList = _gridExporter.GetGridContentAsDataTableList(this.outputDataGrid, _gridDataView.Table.TableName, true, _maxTempDataTableRows);
                        if (dtList != null)
                        {
                            if (dtList.Count > 0)
                            {
                                _gridExporter.ExportToDatabaseTable(dtList, dbPlat, connectionString, outputTableName, outputBatchSize, replaceExistingTable);
                                this.txtExportFinishedMessage.Text = "Export to database succeeded.";
                            }
                            else
                            {
                                this.txtExportFinishedMessage.Text = "Export to database failed. No rows found to export.";
                            }
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to database failed.";
                        }
                    }
                    else
                    {
                        //dgvRowNumber field not defined in the table data
                        dgvTab = _gridExporter.GetGridContentAsDataTable(this.outputDataGrid, true);
                        if (dgvTab != null)
                        {
                            _gridExporter.ExportToDatabaseTable(dgvTab, dbPlat, connectionString, outputTableName, outputBatchSize, replaceExistingTable);
                            this.txtExportFinishedMessage.Text = "Export to database succeeded.";
                        }
                        else
                        {
                            this.txtExportFinishedMessage.Text = "Export to database failed.";
                        }
                    }
                }
                else
                {
                    dgvTab = _gridDataView.Table;
                    if (dgvTab != null)
                    {
                        _gridExporter.ExportToDatabaseTable(dgvTab, dbPlat, connectionString, outputTableName, outputBatchSize, replaceExistingTable);
                        this.txtExportFinishedMessage.Text = "Export to database succeeded.";
                    }
                    else
                    {
                        this.txtExportFinishedMessage.Text = "Export to database failed.";
                    }
                }

            }
            catch (System.Exception ex)
            {
                this.txtExportFinishedMessage.Text = string.Empty;
                _msg.Length = 0;
                _msg.Append("Error occurred during Export to database table::\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                if(dtList != null)
                    DeleteTempFiles(dtList);
                EnableFormControls();
            }
        }//end method


        private bool GridIsShowingSubsetOfTableData(DataTable dt, DataGridView dgv)
        {
            bool ret = false;
            if (dt == null || dgv == null)
                return false;

            int numDtRows = dt.Rows.Count;
            int numDtCols = dt.Columns.Count;
            int numDgvRows = dgv.Rows.Count;
            int numDgvCols = 0;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (!col.Visible) continue;
                if (col.Name == string.Empty) continue;
                if (col.ValueType == null) continue;
                numDgvCols++;
            }

            if (numDtRows != numDgvRows
                || numDtCols != numDgvCols)
            {
                ret = true;
            }

            //AppMessages.DisplayInfoMessage("ret for grid is subset of data table is " + ret.ToString());

            return ret;
        }

        private bool RowNumberColumnDefined(DataTable dt)
        {
            bool ret = false;

            if (dt == null)
                return false;

            for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
            {
                if (dt.Columns[colInx].ColumnName == "dgvRowNumber")
                {
                    ret = true;
                    break;
                }
            }

            return ret;
        }

        private void DeleteTempFiles(PFList<string> dtList)
        {
            for (int i = 0; i < dtList.Count; i++)
            {
                string filename = dtList[i];
                try
                {
                    File.Delete(filename);
                }
                catch (System.Exception ex)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to delete temp file: ");
                    _msg.Append(filename);
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Make sure you have sufficient security rights to delete from your user temp folder.");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(AppMessages.FormatErrorMessage(ex));
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
            }
        }

        //class helpers

        private void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Data Output Grid Overview");
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



    }//end class
}//end namespace
    