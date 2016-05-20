using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using AppGlobals;
using PFPrinterObjects;
using PFSystemObjects;
using PFAppUtils;
using PFCollectionsObjects;
using PFDataGridViewColumnSelector;
using PFDataGridViewDisableButtonCell;

namespace PFRandomDataProcessor
{
    /// <summary>
    /// Class for managing the display of a data grid containing data from an ADO.NET DataTable.
    /// NOTE: ********* This class is used for testing only. ***********
    /// </summary>
    public partial class TEST_DataTableRandomizerColumnSpecForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";

        TEST_DataTableRandomizer _randomizer = new TEST_DataTableRandomizer();

        private int _randomizerSourceInx = -1;
        private int _randomizerFieldInx = -1;
        private int _randomizerFileInx = -1;
        private int _lookupButtonInx = -1;

        DataGridViewTextBoxCell _txtCell = new DataGridViewTextBoxCell();
        DataGridViewButtonCell _btnCell = new DataGridViewButtonCell();

        private bool _userCancelButtonPressed = false;
        private int[] _previousRandomizerSourceSelectedIndex = null;

        private bool _dataHasChanged = false;

        //private fields for saving DataGridViewPrinter settings
        System.Drawing.Printing.PageSettings _savePageSettings = new System.Drawing.Printing.PageSettings();

        //private fields for processing file and folder dialogs
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfapps\Randomizer\";
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = false;
        private string _saveFilter = "XML Files|*.xml|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = false;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        //private fields for properties
        PFList<DataTableRandomizerColumnSpec> _colSpecs = null;


        /// <summary>
        /// Constructor.
        /// </summary>
        public TEST_DataTableRandomizerColumnSpecForm(PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            InitializeComponent();
            InitInstance(colSpecs);
        }

        private void InitInstance(PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            if (colSpecs == null)
                _colSpecs = new PFList<DataTableRandomizerColumnSpec>();
            else
                _colSpecs = colSpecs;

            _randomizer.InitRandomizerFolders();
        }

        //properties

        /// <summary>
        /// Returns a reference to the grid on the form.
        /// </summary>
        public DataGridView OutputDataGridView
        {
            get
            {
                return this.inputDataGrid;
            }
        }

        /// <summary>
        /// List of column specifications. Will contain any changes made on the form if the Accept key was pressed.
        /// </summary>
        public PFList<DataTableRandomizerColumnSpec> ColSpecs
        {
            get
            {
                return _colSpecs;
            }
            set
            {
                _colSpecs = value;
            }
        }
        
        //button click events

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            SaveColSpecChanges();
            this.DialogResult = DialogResult.OK;
            HideForm();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult res = CheckCancelRequest();
            if (res == DialogResult.Yes)
            {
                _userCancelButtonPressed = true;
                this.DialogResult = DialogResult.Cancel;
                HideForm();
            }
        }

        private DialogResult CheckCancelRequest()
        {
            DialogResult res = System.Windows.Forms.DialogResult.Yes;
            if(_dataHasChanged)
                res = AppMessages.DisplayMessage("Click yes to cancel and discard any changes you may have made to the column specifications.", "Cancel Data Randomizer Criteria ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }

        private void DataTableRandomizerColumnSpecForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _userCancelButtonPressed == false)
            {
                DialogResult res = CheckCancelRequest();
                if (res == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    this.DialogResult = DialogResult.Ignore;
                }
            }
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


        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        /// <summary>
        /// Routines to setup the form.
        /// </summary>
        public void InitializeForm()
        {
            _userCancelButtonPressed = false;
            if (_previousRandomizerSourceSelectedIndex != null)
            {
                if (_colSpecs != null)
                {
                    if (_colSpecs.Count > 0)
                    {
                        for (int r = 0; r < _colSpecs.Count; r++)
                        {
                            _previousRandomizerSourceSelectedIndex[r] = -1;
                        }
                    }
                }
            }
            GetLoggingSettings();
            SetGridDataView();
            SetColumnSelectors();
            EnableFormControls();

            _dataHasChanged = false;
        }

        private void GetLoggingSettings()
        {
            _saveErrorMessagesToErrorLog = AppConfig.GetBooleanValueFromConfigFile("SaveErrorMessagesToErrorLog", "false");
            _applicationLogFileName = System.Configuration.ConfigurationManager.AppSettings["ApplicationLogFileName"];

        }

        private void SetGridDataView()
        {
            _txtCell.Value = "...";
            _btnCell.Value = "+++";

            inputDataGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            inputDataGrid.ReadOnly = false;
            inputDataGrid.ColumnCount = 2;
            inputDataGrid.Columns[0].Name = "Column Name";
            inputDataGrid.Columns[0].ReadOnly = true;
            inputDataGrid.Columns[1].Name = "Data Type";
            inputDataGrid.Columns[1].ReadOnly = true;

            DataGridViewComboBoxColumn randomizerCombo = new DataGridViewComboBoxColumn();
            _randomizerSourceInx = 2;
            randomizerCombo.HeaderText = "Randomizer Source";
            randomizerCombo.Items.AddRange(" ",
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomNamesAndLocations.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomNumbers.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomWords.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomDatesAndTimes.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomBooleans.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.CustomRandomValues.ToString(), @"(?<!^)(?=[A-Z])"))
                                         );
            randomizerCombo.DropDownWidth = 250;
            randomizerCombo.Width = 250;
            randomizerCombo.MaxDropDownItems = 15;
            randomizerCombo.ReadOnly = false;
            inputDataGrid.Columns.Add(randomizerCombo);

            DataGridViewComboBoxColumn fieldNameCombo = new DataGridViewComboBoxColumn();
            _randomizerFieldInx = 3;
            fieldNameCombo.HeaderText = "Random Name Field";
            fieldNameCombo.Items.AddRange(" ",
                                          string.Join(" ", Regex.Split(enRandomNameField.NameType.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.Country.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.FirstName.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.MiddleInitial.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.LastName.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.Gender.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.BirthDate.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.AddressLine1.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.AddressLine2.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.City.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.Neighborhood.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.StateProvince.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.ZipPostalCode.ToString(), @"(?<!^)(?=[A-Z])")),
                                          string.Join(" ", Regex.Split(enRandomNameField.StateProvinceName.ToString(), @"(?<!^)(?=[A-Z])"))
                                        );
            fieldNameCombo.DropDownWidth = 250;
            fieldNameCombo.Width = 250;
            fieldNameCombo.MaxDropDownItems = 15;
            fieldNameCombo.ReadOnly = false;
            inputDataGrid.Columns.Add(fieldNameCombo);


            DataGridViewTextBoxColumn txtCol = new DataGridViewTextBoxColumn();
            int txtInx = inputDataGrid.Columns.Add(txtCol);
            _randomizerFileInx = txtInx;
            txtCol.HeaderText = "Randomizer File";
            txtCol.Width = 200;
            txtCol.Name = "txt";
            inputDataGrid.Columns[txtInx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;


            //DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            DataGridViewDisableButtonColumn btn = new DataGridViewDisableButtonColumn();
            int btnInx = inputDataGrid.Columns.Add(btn);
            _lookupButtonInx = btnInx;
            btn.HeaderText = "File Lookup";
            btn.Text = "...";
            btn.DefaultCellStyle.Font = new System.Drawing.Font("Lucida Console", (float)10.0, FontStyle.Bold);
            btn.Width = 65;
            btn.Name = "btn";
            btn.ReadOnly = true;
            inputDataGrid.Columns[btnInx].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            btn.UseColumnTextForButtonValue = true;

            _previousRandomizerSourceSelectedIndex = new int[_colSpecs.Count];
            for (int r = 0; r < _colSpecs.Count; r++)
            {
                _previousRandomizerSourceSelectedIndex[r] = -1;
                string randomDataSource = " ";
                if (_colSpecs[r].RandomDataType != enRandomDataType.NotSpecified)
                {
                    randomDataSource = string.Join(" ", Regex.Split(_colSpecs[r].RandomDataType.ToString(), @"(?<!^)(?=[A-Z])"));
                }
                string[] row;
                row = new string[] {_colSpecs[r].DataTableColumnName, _colSpecs[r].DataTableColumnDataType.ToString(), randomDataSource, " ", " "};
                int rowInx = inputDataGrid.Rows.Add(row);
                DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)inputDataGrid.Rows[rowInx].Cells[_lookupButtonInx];
                buttonCell.Enabled = false;
                DataGridViewComboBoxCell cboCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerFieldInx];
                cboCell.Items.Clear();
                cboCell.Items.Add(" ");
            }


        }

        private void SetColumnSelectors()
        {
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(this.OutputDataGridView);
            cs.MaxHeight = 100;
            cs.Width = 110;
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



        private void DataTableRandomizerColumnSpecForm_Shown(object sender, EventArgs e)
        {
            int rowCount = inputDataGrid.RowCount;
            lblTotalNumberOfRows.Text = "Total Number of Columns in DataTable = " + rowCount.ToString("#,##0");
        }

        //Application routines

        private void ShowPageSettings()
        {
            PFDataGridViewPrinter _dgvPrinter = new PFDataGridViewPrinter();
            InitDataGridViewPrinter(_dgvPrinter);
            _dgvPrinter.ShowPageSettingsDialog();
            _savePageSettings = _dgvPrinter.Printer.printDoc.DefaultPageSettings;
        }

        private void ShowPrintPreview()
        {
            GridViewPrint(true);
        }

        private void ShowPrintDialog()
        {
            GridViewPrint(false);
        }

        private void GridViewPrint(bool showPreview)
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
                    _dgvPrinter.Print();
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
            dgvPrinter.Grid = (DataGridView)this.inputDataGrid;

            dgvPrinter.Printer.printDoc.DefaultPageSettings = _savePageSettings;
            dgvPrinter.Printer.printDoc.DefaultPageSettings.PaperSource.RawKind = (int)System.Drawing.Printing.PaperSourceKind.AutomaticFeed;

            dgvPrinter.Title = AppGlobals.AppInfo.AssemblyDescription;
            dgvPrinter.SubTitle = "Data output from " + this.DataGridBindingSource.DataMember;
            dgvPrinter.Footer = AppGlobals.AppInfo.AssemblyProduct;
            dgvPrinter.PageNumbers = true;
            dgvPrinter.ShowTotalPageNumber = true;

        }


        private void CopySelectedCellsToClipBoard()
        {
            DataGridView tempdgv = (DataGridView)this.inputDataGrid;
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


        private void inputDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (inputDataGrid.CurrentCell.ColumnIndex == _randomizerSourceInx)
            {
                ComboBox cboRs = e.Control as ComboBox;
                cboRs.SelectedIndexChanged -= new EventHandler(randomizerSource_SelectedIndexChanged);
                cboRs.SelectedIndexChanged += new EventHandler(randomizerSource_SelectedIndexChanged);
            }
            else if (inputDataGrid.CurrentCell.ColumnIndex == _randomizerFieldInx)
            {
                ComboBox cboRf = e.Control as ComboBox;
                cboRf.SelectedIndexChanged -= new EventHandler(randomizerSource_SelectedIndexChanged);
            }
        }

        private void randomizerSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dataHasChanged = true;
            if (inputDataGrid.CurrentCell.ColumnIndex == _randomizerSourceInx)
            {
                int selectedIndex = ((ComboBox)sender).SelectedIndex;
                int rowInx = inputDataGrid.CurrentRow.Index;
                int colInx = _lookupButtonInx;
                DataGridViewDisableButtonCell buttonCell = (DataGridViewDisableButtonCell)inputDataGrid.Rows[rowInx].Cells[colInx];
                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerFileInx];

                if (selectedIndex != _previousRandomizerSourceSelectedIndex[rowInx])
                {
                    txtCell.Value = "";
                    inputDataGrid.CurrentRow.Cells[_randomizerFieldInx].Value = " ";
                    _previousRandomizerSourceSelectedIndex[rowInx] = selectedIndex;
                }
                
                if (selectedIndex != 0)
                {
                    buttonCell.Enabled = true;
                }
                else
                {
                    buttonCell.Enabled = false;
                }

                InitRandomNameFieldDropDown(selectedIndex, rowInx);

                inputDataGrid.Invalidate();
            }

        }

        private void InitRandomNameFieldDropDown(int selectedIndex, int rowInx)
        {
            DataGridViewComboBoxCell fieldNameCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerFieldInx];

            if (selectedIndex == 1)       //either generate random names option was selected
            {
                fieldNameCombo.Items.Clear();
                //fieldNameCombo.Items.AddRange(" ",
                //                              string.Join(" ", Regex.Split(enRandomNameField.NameType.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.Country.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.FirstName.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.MiddleInitial.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.LastName.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.Gender.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.BirthDate.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.AddressLine1.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.AddressLine2.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.City.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.Neighborhood.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.StateProvince.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.ZipPostalCode.ToString(), @"(?<!^)(?=[A-Z])")),
                //                              string.Join(" ", Regex.Split(enRandomNameField.StateProvinceName.ToString(), @"(?<!^)(?=[A-Z])"))
                //                            );
                fieldNameCombo.Items.AddRange(" ",
                                  string.Join(" ", Regex.Split(enRandomNameField.NameType.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.Country.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.FirstName.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.MiddleInitial.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.LastName.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.Gender.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.BirthDate.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.AddressLine1.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.AddressLine2.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.City.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.Neighborhood.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.StateProvince.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.ZipPostalCode.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.StateProvinceName.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.RegionName.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.SubRegionName.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.AreaCode.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.TelephoneNumber.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.EmailAddress.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.NationalId.ToString(), @"(?<!^)(?=[A-Z])")),
                                  string.Join(" ", Regex.Split(enRandomNameField.CountryCode.ToString(), @"(?<!^)(?=[A-Z])"))
                                );
            }
            else
            {
                fieldNameCombo.Items.Clear();
                fieldNameCombo.Items.Add(" ");
            }
        }

        private void inputDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DataGridViewDisableButtonCell fileLookupCell = (DataGridViewDisableButtonCell)inputDataGrid.Rows[e.RowIndex].Cells[_lookupButtonInx];
                DataGridViewComboBoxCell randomizerSourceCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[e.RowIndex].Cells[_randomizerSourceInx];
                DataGridViewTextBoxCell randomizerFilePathCell = (DataGridViewTextBoxCell)inputDataGrid.Rows[e.RowIndex].Cells[_randomizerFileInx];

                if (fileLookupCell.Enabled)
                {
                    string cellValue = randomizerSourceCell.Value.ToString();
                    enRandomDataType randomizerSource = (enRandomDataType)Enum.Parse(typeof(enRandomDataType), cellValue.Replace(" ",""));
                    string filepath = GetRandomizerFile(randomizerSource);
                    if (filepath.Length > 0)
                    {
                        randomizerFilePathCell.Value = filepath;
                        _dataHasChanged = true;
                    }
                }

            }
        }

        private string GetRandomizerFile(enRandomDataType randomizerSource)
        {
            string folderPath = string.Empty;
            string selectedFile = string.Empty;

            switch (randomizerSource)
            {
                case enRandomDataType.RandomNamesAndLocations:
                    folderPath = Path.Combine(_randomizer.RandomDefinitionsFolder, _randomizer.RandomNamesAndLocationsSubfolder);
                    break;
                case enRandomDataType.CustomRandomValues:
                    folderPath = Path.Combine(_randomizer.RandomDataFolder, _randomizer.RandomCustomDataSubfolder);
                    break;
                case enRandomDataType.RandomNumbers:
                    folderPath = Path.Combine(_randomizer.RandomDefinitionsFolder, _randomizer.RandomNumbersSubfolder);
                    break;
                case enRandomDataType.RandomWords:
                    folderPath = Path.Combine(_randomizer.RandomDefinitionsFolder, _randomizer.RandomWordsSubfolder);
                    break;
                case enRandomDataType.RandomDatesAndTimes:
                    folderPath = Path.Combine(_randomizer.RandomDefinitionsFolder, _randomizer.RandomDatesSubfolder);
                    break;
                case enRandomDataType.RandomBooleans:
                    folderPath = Path.Combine(_randomizer.RandomDefinitionsFolder, _randomizer.RandomBooleansSubfolder);
                    break;
                default:
                    folderPath = Path.Combine(_randomizer.RandomDefinitionsFolder, _randomizer.RandomNamesAndLocationsSubfolder);
                    break;
            }

            _saveSelectionsFolder = folderPath;
            DialogResult res = ShowOpenFileDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                selectedFile = _saveSelectionsFile;
            }

            return selectedFile;
        }

        private void SaveColSpecChanges()
        {
            for (int r = 0; r < _colSpecs.Count; r++)
            {
                DataGridViewComboBoxCell randomizerSourceCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerSourceInx];
                DataGridViewComboBoxCell randomizerFieldCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerFieldInx];
                DataGridViewTextBoxCell randomizerFilePathCell = (DataGridViewTextBoxCell)inputDataGrid.Rows[r].Cells[_randomizerFileInx];

                string cellValue = randomizerSourceCell.Value.ToString();
                enRandomDataType randomizerSource = enRandomDataType.NotSpecified;
                if (cellValue.Trim() != string.Empty)
                {
                    randomizerSource = (enRandomDataType)Enum.Parse(typeof(enRandomDataType), cellValue.Replace(" ", ""));
                }
                _colSpecs[r].RandomDataType = randomizerSource;
                _colSpecs[r].RandomDataFieldName = randomizerFieldCell.Value.ToString();
                _colSpecs[r].RandomDataFileName = randomizerFilePathCell.Value.ToString();

            }
        }


    }//end class


}//end namespace
