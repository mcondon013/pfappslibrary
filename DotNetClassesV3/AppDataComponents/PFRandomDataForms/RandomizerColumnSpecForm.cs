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
using PFRandomDataProcessor;
using PFPrinterObjects;
using PFSystemObjects;
using PFAppUtils;
using PFCollectionsObjects;
using PFDataGridViewColumnSelector;
using PFDataGridViewDisableButtonCell;
using PFRandomValueDataTables;

namespace PFRandomDataForms
{
    /// <summary>
    /// Class for mapping data grid columns to random value definitions.
    /// </summary>
    public partial class DataTableRandomizerColumnSpecForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";
        private string _helpFilePath = string.Empty;

        DataTableRandomizer _randomizer = new DataTableRandomizer();

        private int _randomizerTypeInx = -1;
        private int _randomizerSourceInx = -1;
        private int _randomizerFieldInx = -1;
        private int _minNameLocTypeInx = -1;
        private int _maxNameLocTypeInx = -1;
        private int _customDataValueTypeInx = -1;
        private int _currentGridRowIndex = -1;
        private int _currentGridRowNumber = -1;
        private int _totalNumberGridRows = -1;

        private bool _userCancelButtonPressed = false;
        private int[] _previousRandomizerTypeSelectedIndex = null;

        private bool _dataHasChanged = false;
        private bool _erasingGrid = false;

        //private fields for saving DataGridViewPrinter settings
        System.Drawing.Printing.PageSettings _savePageSettings = new System.Drawing.Printing.PageSettings();

        //private fields for locations of data request definition and data files
        private string _randomizerDataRequestDefinitionFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\";
        private string _randomizerOriginalDataRequestDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\OriginalDefinitions\";
        private string _randomizerDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\";
        private string[] _randomizerDataTypeSubFolders = new string[10];
        private enRandomDataType[] _randomizerDataTypes = (enRandomDataType[])Enum.GetValues(typeof(enRandomDataType));        
        
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
        public DataTableRandomizerColumnSpecForm(PFList<DataTableRandomizerColumnSpec> colSpecs)
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
            bool changesSaved = SaveColSpecChanges();
            if (changesSaved)
            {
                this.DialogResult = DialogResult.OK;
                HideForm();
            }
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

        private void cmdEraseAll_Click(object sender, EventArgs e)
        {
            ReinitGrid();
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

        private void mnuRandomizerSources_Click(object sender, EventArgs e)
        {
            ManageRandomizerSources();
        }

        private void mnuHelpMappingRandomValues_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
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
            if (_previousRandomizerTypeSelectedIndex != null)
            {
                if (_colSpecs != null)
                {
                    if (_colSpecs.Count > 0)
                    {
                        for (int r = 0; r < _colSpecs.Count; r++)
                        {
                            _previousRandomizerTypeSelectedIndex[r] = -1;
                        }
                    }
                }
            }
            SetHelpFileValues();
            GetLoggingSettings();
            SetFileLocations();
            SetGridDataView();
            SetColumnSelectors();
            EnableFormControls();

            _dataHasChanged = false;
        }

        internal void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("RandomSourcesHelpFileName", "RandomDataMasks.chm");
            string helpFilePath = Path.Combine(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        private void GetLoggingSettings()
        {
            _saveErrorMessagesToErrorLog = AppConfig.GetBooleanValueFromConfigFile("SaveErrorMessagesToErrorLog", "false");
            _applicationLogFileName = System.Configuration.ConfigurationManager.AppSettings["ApplicationLogFileName"];

        }

        private void SetFileLocations()
        {
            string[] randomizerTypes = Enum.GetNames(typeof(enRandomDataType));
            for (int i = 0; i < randomizerTypes.Length; i++)
            {
                _randomizerDataTypeSubFolders[i] = randomizerTypes[i].Replace("Random",string.Empty) + @"\";
            }

            string configValue = string.Empty;
            string randomDataRequestFolder = string.Empty;
            string randomNumericOriginalDataRequestFolder = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomDataRequestFolder = configValue;
            else
                randomDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\";

            if (randomDataRequestFolder.EndsWith(@"\") == false)
                randomDataRequestFolder = randomDataRequestFolder + @"\";
            
            _randomizerDataRequestDefinitionFolder = randomDataRequestFolder + @"PFApps\Randomizer\Definitions\";
            _randomizerOriginalDataRequestDefinitionsFolder = randomDataRequestFolder + @"PFApps\Randomizer\OriginalDefinitions\";
            _randomizerDataFolder = randomDataRequestFolder + @"PFApps\Randomizer\Data\";

        }

        private void SetGridDataView()
        {
            int typeInx = -1;

            inputDataGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            inputDataGrid.ReadOnly = false;
            inputDataGrid.ColumnCount = 2;
            inputDataGrid.Columns[0].Name = "Column Name";
            inputDataGrid.Columns[0].ReadOnly = true;
            inputDataGrid.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            inputDataGrid.Columns[1].Name = "Data Type";
            inputDataGrid.Columns[1].ReadOnly = true;
            inputDataGrid.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            DataGridViewComboBoxColumn randomizerCombo = new DataGridViewComboBoxColumn();
            _randomizerTypeInx = 2;
            randomizerCombo.HeaderText = "Randomizer Type";
            randomizerCombo.Items.AddRange(" ",
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomNamesAndLocations.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", "RandomNamesAndLocations_2"),
                                           string.Join(" ", "RandomNamesAndLocations_3"),
                                           string.Join(" ", "RandomNamesAndLocations_4"),
                                           string.Join(" ", "RandomNamesAndLocations_5"),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomNumbers.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomStrings.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomWords.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomDatesAndTimes.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomBooleans.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomBytes.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.RandomDataElements.ToString(), @"(?<!^)(?=[A-Z])")),
                                           string.Join(" ", Regex.Split(enRandomDataType.CustomRandomValues.ToString(), @"(?<!^)(?=[A-Z])"))
                                         );
            randomizerCombo.DropDownWidth = 250;
            randomizerCombo.Width = 250;
            randomizerCombo.MaxDropDownItems = 15;
            randomizerCombo.ReadOnly = false;
            randomizerCombo.SortMode = DataGridViewColumnSortMode.NotSortable;
            inputDataGrid.Columns.Add(randomizerCombo);
            _minNameLocTypeInx = 1;
            _maxNameLocTypeInx = 5;
            _customDataValueTypeInx = randomizerCombo.Items.Count - 1;

            DataGridViewComboBoxColumn sourceCombo = new DataGridViewComboBoxColumn();
            _randomizerSourceInx = 3;
            sourceCombo.HeaderText = "Randomizer Source";
            sourceCombo.Items.AddRange(" ",
                                       "Definitions or Data File"
                                      );
            sourceCombo.DropDownWidth = 250;
            sourceCombo.Width = 250;
            sourceCombo.MaxDropDownItems = 15;
            sourceCombo.ReadOnly = false;
            sourceCombo.SortMode = DataGridViewColumnSortMode.NotSortable;
            inputDataGrid.Columns.Add(sourceCombo);

            DataGridViewComboBoxColumn fieldNameCombo = new DataGridViewComboBoxColumn();
            _randomizerFieldInx = 4;
            fieldNameCombo.HeaderText = "Random Value";
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
            fieldNameCombo.DropDownWidth = 250;        
            fieldNameCombo.Width = 250;
            fieldNameCombo.MaxDropDownItems = 15;
            fieldNameCombo.ReadOnly = false;
            fieldNameCombo.SortMode = DataGridViewColumnSortMode.NotSortable;
            inputDataGrid.Columns.Add(fieldNameCombo);

            inputDataGrid.Rows.Clear();
            _previousRandomizerTypeSelectedIndex = new int[_colSpecs.Count];
            for (int r = 0; r < _colSpecs.Count; r++)
            {
                _previousRandomizerTypeSelectedIndex[r] = -1;
                typeInx = -1;
                string randomDataType = " ";
                if (_colSpecs[r].RandomDataType != enRandomDataType.NotSpecified)
                {
                    randomDataType = string.Join(" ", Regex.Split(_colSpecs[r].RandomDataType.ToString(), @"(?<!^)(?=[A-Z])"));
                    if (_colSpecs[r].RandomNamesAndLocationsNumber != -1)
                    {
                        randomDataType = randomDataType.Replace(" ",string.Empty) + "_" + _colSpecs[r].RandomNamesAndLocationsNumber.ToString();
                    }
                    for (int i = 0; i < randomizerCombo.Items.Count; i++)
                    {
                        if (randomDataType == randomizerCombo.Items[i].ToString())
                        {
                            typeInx = i;
                            break;
                        }
                    }
                }
                string[] row;
                row = new string[] {_colSpecs[r].DataTableColumnName, _colSpecs[r].DataTableColumnDataType.ToString(), randomDataType, " ", " "};
                int rowInx = inputDataGrid.Rows.Add(row);
                if (typeInx != -1)
                {
                    InitRandomNameFieldDropDown(typeInx, r, true);
                    DataGridViewComboBoxCell sourceCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerSourceInx];
                    for (int c = 0; c < sourceCell.Items.Count; c++)
                    {
                        if (sourceCell.Items[c].ToString() == _colSpecs[r].RandomDataSource)
                        {
                            sourceCell.Value = String.IsNullOrEmpty(_colSpecs[r].RandomDataSource) ? " " : _colSpecs[r].RandomDataSource;
                            break;
                        }
                    }
                    DataGridViewComboBoxCell fieldCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerFieldInx];
                    for (int c = 0; c < fieldCell.Items.Count; c++)
                    {
                        string fieldCellItemValue = fieldCell.Items[c].ToString().Trim().Replace(" ", string.Empty); //need to remove embedded blanks from the field names listed in the combo drop down; data column names do not have embedded blanks
                        if (fieldCellItemValue == _colSpecs[r].RandomDataFieldName)   
                        {
                            //fieldCell.Value = String.IsNullOrEmpty(_colSpecs[r].RandomDataFieldName) ? " " : _colSpecs[r].RandomDataFieldName;
                            fieldCell.Value = fieldCell.Items[c].ToString();
                            break;
                        }
                    }
                }
                else
                {
                    DataGridViewComboBoxCell cboCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerFieldInx];
                    cboCell.Items.Clear();
                    cboCell.Items.Add(" ");
                }

            }

            _currentGridRowIndex = 0;
            _totalNumberGridRows = inputDataGrid.Rows.Count;
            if (_totalNumberGridRows > 0)
            {
                inputDataGrid.Rows[0].Selected = true;
                txtTotalRowCount.Text = "of " + _totalNumberGridRows.ToString() + " rows";
                _currentGridRowNumber = _currentGridRowIndex + 1;
                txtCurrentRowNumber.Text = _currentGridRowNumber.ToString();
                panelGridNavigator.Enabled = true;
            }
            else
            {
                txtTotalRowCount.Text = "of 0 rows";
                txtCurrentRowNumber.Text = "0";
                panelGridNavigator.Enabled = false;
            }
        }

        private void SetColumnSelectors()
        {
            DataGridViewColumnSelector cs = new DataGridViewColumnSelector(this.OutputDataGridView);
            cs.MaxHeight = 100;
            cs.Width = 110;
        }

        private void ReinitGrid()
        {
            try
            {
                _erasingGrid = true;
                ReinitColSpecs();
                SetGridDataView();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToErrorLog);
            }
            finally
            {
                _erasingGrid = false; ;
            }


        }

        private void ReinitColSpecs()
        {
            for (int i = 0; i < _colSpecs.Count; i++)
            {
                _colSpecs[i].RandomDataType = enRandomDataType.NotSpecified;
                _colSpecs[i].RandomDataSource = string.Empty;
                _colSpecs[i].RandomNamesAndLocationsNumber = -1;
                _colSpecs[i].RandomDataFieldName = string.Empty;
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



        private void DataTableRandomizerColumnSpecForm_Shown(object sender, EventArgs e)
        {
            int rowCount = inputDataGrid.RowCount;
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
                if(dataObj != null)
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


        private void inputDataGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e.RowIndex);
        }

        private void RowEnter(int rowIndex)
        {
            _currentGridRowIndex = rowIndex;
            _totalNumberGridRows = inputDataGrid.Rows.Count;
            inputDataGrid.Rows[_currentGridRowIndex].Selected = true;
            txtTotalRowCount.Text = "of " + _totalNumberGridRows.ToString() + " rows";
            _currentGridRowNumber = _currentGridRowIndex + 1;
            txtCurrentRowNumber.Text = _currentGridRowNumber.ToString();
            panelGridNavigator.Enabled = true;
        }

        private void inputDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (inputDataGrid.CurrentCell.ColumnIndex == _randomizerTypeInx)
            {
                ComboBox cboRt = e.Control as ComboBox;
                cboRt.SelectedIndexChanged -= new EventHandler(randomizerType_SelectedIndexChanged);
                cboRt.SelectedIndexChanged += new EventHandler(randomizerType_SelectedIndexChanged);
            }
            else  if (inputDataGrid.CurrentCell.ColumnIndex == _randomizerSourceInx)
            {
                ComboBox cboRs = e.Control as ComboBox;
                cboRs.SelectedIndexChanged -= new EventHandler(randomizerType_SelectedIndexChanged);
                cboRs.SelectedIndexChanged += new EventHandler(randomizerType_SelectedIndexChanged);
            }
            else if (inputDataGrid.CurrentCell.ColumnIndex == _randomizerFieldInx)
            {
                ComboBox cboRf = e.Control as ComboBox;
                cboRf.SelectedIndexChanged -= new EventHandler(randomizerType_SelectedIndexChanged);
            }
        }

        private void randomizerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dataHasChanged = true;
            bool randomizerTypeHasChanged = false;
            //bool randomizerSourceHasChanged = false;

            if (inputDataGrid.CurrentCell.ColumnIndex == _randomizerTypeInx)
            {
                int selectedIndex = ((ComboBox)sender).SelectedIndex;
                int rowInx = inputDataGrid.CurrentRow.Index;

                if (selectedIndex != _previousRandomizerTypeSelectedIndex[rowInx])
                {
                    randomizerTypeHasChanged = true;
                    inputDataGrid.CurrentRow.Cells[_randomizerFieldInx].Value = " ";
                    _previousRandomizerTypeSelectedIndex[rowInx] = selectedIndex;
                }
                
                InitRandomNameFieldDropDown(selectedIndex, rowInx, randomizerTypeHasChanged);

                inputDataGrid.Invalidate();
            }

        }

        private void InitRandomNameFieldDropDown(int selectedIndex, int rowInx, bool randomizerTypeHasChanged)
        {
            DataGridViewComboBoxCell fieldNameCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerFieldInx];
            DataGridViewComboBoxCell sourceCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerSourceInx];
            DataGridViewComboBoxCell typeCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerTypeInx];

            if (selectedIndex >= _minNameLocTypeInx && selectedIndex <= _maxNameLocTypeInx)       //one of the generate random names and locations types was selected
            {
                fieldNameCombo.Items.Clear();
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
                if (randomizerTypeHasChanged)
                {
                    string source = GetPreviousRandomNamesAndLocationSource(typeCombo, sourceCombo, selectedIndex, rowInx);
                    if (source.Length > 0)
                    {
                        sourceCombo.Items.Clear();
                        sourceCombo.Items.Add(source);
                        sourceCombo.Value = sourceCombo.Items[0];
                    }
                    else
                    {
                        FillSourceComboBox(typeCombo, sourceCombo, selectedIndex, rowInx);
                    }
                }
                ////make sure selected source is the same for all rows with same name and location type
                //if (sourceCombo.Value.ToString().Trim().Length > 0)
                //{
                //    inputDataGrid.Rows[rowInx].Selected = true;
                //    inputDataGrid.Invalidate();
                //    SyncAllNameAndLocationSourcesForSpecifiedType(rowInx);
                //}
            }
            else if (selectedIndex == _customDataValueTypeInx)
            {
                fieldNameCombo.Items.Clear();
                fieldNameCombo.Items.Add(" ");
                fieldNameCombo.Items.Add("CustomDataValue");
                fieldNameCombo.Value = fieldNameCombo.Items[1];
                if (randomizerTypeHasChanged)
                {
                    FillSourceComboBox(typeCombo, sourceCombo, selectedIndex, rowInx);
                    //sourceCombo.Items.Clear();
                    //sourceCombo.Items.Add(" ");
                    //sourceCombo.Items.AddRange("Custom Data Values File");
                    //sourceCombo.Value = sourceCombo.Items[0];
                }
            }
            else if (selectedIndex != 0)
            {
                fieldNameCombo.Items.Clear();
                fieldNameCombo.Items.Add(" ");
                fieldNameCombo.Items.Add("GeneratedValue");
                fieldNameCombo.Value = fieldNameCombo.Items[1];
                if (randomizerTypeHasChanged)
                {
                    FillSourceComboBox(typeCombo, sourceCombo, selectedIndex, rowInx);
                    //sourceCombo.Items.Clear();
                    //sourceCombo.Items.Add(" ");
                    //sourceCombo.Items.AddRange("Random Type Definition File");
                    //sourceCombo.Value = sourceCombo.Items[0];
                }
            }
            else  //(selectedIndex == 0) (blank)
            {
                fieldNameCombo.Items.Clear();
                fieldNameCombo.Items.Add(" ");
                fieldNameCombo.Value = fieldNameCombo.Items[0];
                sourceCombo.Items.Clear();
                sourceCombo.Items.Add(" ");
                sourceCombo.Value = sourceCombo.Items[0];
            }
        }

        private void FillSourceComboBox(DataGridViewComboBoxCell typeCombo, DataGridViewComboBoxCell sourceCombo, int selectedIndex, int rowInx)
        {
            enRandomDataType randomDataType = enRandomDataType.NotSpecified;
            string folderPath = string.Empty;
            string[] files = null;

            if (selectedIndex >= _minNameLocTypeInx && selectedIndex <= _maxNameLocTypeInx)       //one of the generate random names and locations types was selected
            {
                randomDataType = enRandomDataType.RandomNamesAndLocations;
            }
            else
            {
                randomDataType = (enRandomDataType)Enum.Parse(typeof(enRandomDataType), typeCombo.Items[selectedIndex].ToString().Replace(" ",string.Empty));
            }
            if(randomDataType == enRandomDataType.CustomRandomValues)
                folderPath = _randomizerDataFolder + _randomizerDataTypeSubFolders[(int)randomDataType];
            else
                folderPath = _randomizerDataRequestDefinitionFolder + _randomizerDataTypeSubFolders[(int)randomDataType];
            files = Directory.GetFiles(folderPath, "*.xml", SearchOption.AllDirectories);
            sourceCombo.Items.Clear();
            sourceCombo.Items.Add(" ");
            foreach (string file in files)
            {
                sourceCombo.Items.Add(file.Replace(folderPath,string.Empty));
            }
            sourceCombo.Value = sourceCombo.Items[0];
        }

        //Routine to make sure source drop downs include any changes made in Randomizer colSpec form
        //See menu options Randomizer/Manage Sources
        private void RefillAllSourceComboBoxes()
        {
            string source = string.Empty;
            int numGridRows = inputDataGrid.Rows.Count;
            int selectedIndex = -1;
            enRandomDataType randomDataType = enRandomDataType.NotSpecified;

            Console.WriteLine("RefillAll: 1st loop");
            for (int rowInx = 0; rowInx < numGridRows; rowInx++)
            {
                //Console.WriteLine("RowInx: " + rowInx.ToString());
                DataGridViewComboBoxCell fieldNameCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerFieldInx];
                DataGridViewComboBoxCell sourceCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerSourceInx];
                DataGridViewComboBoxCell typeCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerTypeInx];

                if (typeCombo.Value.ToString().Trim().Length > 0)
                {
                    selectedIndex = GetTypeComboSelectedIndex(typeCombo);
                    if (selectedIndex >= _minNameLocTypeInx && selectedIndex <= _maxNameLocTypeInx)       //one of the generate random names and locations types was selected
                        randomDataType = enRandomDataType.RandomNamesAndLocations;
                    else
                        randomDataType = (enRandomDataType)Enum.Parse(typeof(enRandomDataType), typeCombo.Items[selectedIndex].ToString().Replace(" ", string.Empty));
                    if (selectedIndex != -1)
                    {
                        if(randomDataType != enRandomDataType.RandomNamesAndLocations
                            || (randomDataType == enRandomDataType.RandomNamesAndLocations && sourceCombo.Items.Count > 1))  //do not refill if this is one of the secondary entries for a NamesAndLocation type (secondary lines only have one source item)
                        {
                            string saveSourceValue = sourceCombo.Value.ToString();
                            FillSourceComboBox(typeCombo, sourceCombo, selectedIndex, rowInx);
                            sourceCombo.Value = saveSourceValue;
                        }
                    }
                }
                
            }

            //Console.WriteLine("RefillAll: 2nd loop");
            //--Start here
            for (int rowInx = 0; rowInx < numGridRows; rowInx++)
            {
                DataGridViewComboBoxCell fieldNameCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerFieldInx];
                DataGridViewComboBoxCell sourceCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerSourceInx];
                DataGridViewComboBoxCell typeCombo = (DataGridViewComboBoxCell)inputDataGrid.Rows[rowInx].Cells[_randomizerTypeInx];

                if (typeCombo.Value.ToString().Trim().Length > 0)
                {
                    selectedIndex = GetTypeComboSelectedIndex(typeCombo);
                    if (selectedIndex >= _minNameLocTypeInx && selectedIndex <= _maxNameLocTypeInx)       //one of the generate random names and locations types was selected
                        randomDataType = enRandomDataType.RandomNamesAndLocations;
                    else
                        randomDataType = (enRandomDataType)Enum.Parse(typeof(enRandomDataType), typeCombo.Items[selectedIndex].ToString().Replace(" ", string.Empty));
                    if (selectedIndex != -1)
                    {
                        if (randomDataType == enRandomDataType.RandomNamesAndLocations && sourceCombo.Items.Count > 1)  //do not refill if this is one of the secondary entries for a NamesAndLocation type (secondary lines only have one source item)
                        {
                            SyncAllNameAndLocationSourcesForSpecifiedType(rowInx);
                        }
                    }
                }

                //if (cellValue.Replace(" ", string.Empty).StartsWith("RandomNamesAndLocations"))
                //{
                //    if (randomizerTypeCell.Items.Count > 2)
                //    {
                //        //this is the primary definition for this particular NamesAndLocations type (e.g. NamesAndLocations_2, etc.).
                //        SyncAllNameAndLocationSourcesForSpecifiedType(r);
                //    }
                //}

            }
        
        }

        private int GetTypeComboSelectedIndex(DataGridViewComboBoxCell typeCombo)
        {
            int selectedIndex = -1;
            string selectedValue = typeCombo.Value.ToString();

            for (int i = 0; i < typeCombo.Items.Count; i++)
            {
                if (typeCombo.Items[i].ToString() == selectedValue)
                {
                    selectedIndex = i;
                    break;
                }
            }

            return selectedIndex;
        }

        private string GetPreviousRandomNamesAndLocationSource(DataGridViewComboBoxCell typeCombo, DataGridViewComboBoxCell sourceCombo, int selectedIndex, int rowInx)
        {
            string source = string.Empty;
            int numGridRows = inputDataGrid.Rows.Count;
            string randomizerType = typeCombo.Items[selectedIndex].ToString();

            for (int r = 0; r < numGridRows; r++)
            {
                if (r != rowInx)
                {
                    if (randomizerType == inputDataGrid.Rows[r].Cells[_randomizerTypeInx].Value.ToString())
                    {
                        source = inputDataGrid.Rows[r].Cells[_randomizerSourceInx].Value.ToString();
                        break;
                    }
                }
            }

            return source;
        }

        private void SyncAllNameAndLocationSourcesForSpecifiedType(int rowInx)
        {
            if (_erasingGrid)
            {
                //form is not yet in shape to be sync'd
                return;
            }
            inputDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);  //commit any unsaved rows with changes
            int numGridRows = inputDataGrid.Rows.Count;
            string randomizerType = inputDataGrid.Rows[rowInx].Cells[_randomizerTypeInx].Value.ToString();
            string randomizerSource = inputDataGrid.Rows[rowInx].Cells[_randomizerSourceInx].Value.ToString();

            if (randomizerType.Replace(" ",string.Empty).StartsWith("RandomNamesAndLocations"))
            {
                for (int r = 0; r < numGridRows; r++)
                {
                    if (r != rowInx)
                    {
                        if (randomizerType == inputDataGrid.Rows[r].Cells[_randomizerTypeInx].Value.ToString())
                        {
                            DataGridViewComboBoxCell sourceCombo2 = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerSourceInx];
                            if (sourceCombo2.Items.Count > 2) //this is the first use of the type in the current grid: it has all sources
                            {
                                inputDataGrid.Rows[r].Cells[_randomizerSourceInx].Value = randomizerSource;
                            }
                            else
                            {
                                //sourceCombo.Items.Count < 2: this is one of the secondary rows using this type
                                sourceCombo2.Items.Clear();
                                sourceCombo2.Items.Add(randomizerSource);
                                sourceCombo2.Value = sourceCombo2.Items[0];
                            }
                        }
                    }

                }
            }
        }

        private void inputDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell randomizerTypeCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[e.RowIndex].Cells[_randomizerTypeInx];
            }
        }

        private void inputDataGrid_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rowInx = e.RowIndex;

            SyncAllNameAndLocationSourcesForSpecifiedType(rowInx);
        }



        private bool SaveColSpecChanges()
        {
            bool changesSaved = true;

            //resync, if necessary, any NamesAndLocations types
            for (int r = 0; r < _colSpecs.Count; r++)
            {
                DataGridViewComboBoxCell randomizerTypeCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerTypeInx];

                string cellValue = randomizerTypeCell.Value.ToString();
                if (cellValue.Replace(" ",string.Empty).StartsWith("RandomNamesAndLocations"))
                {
                    if (randomizerTypeCell.Items.Count > 2)
                    {
                        //this is the primary definition for this particular NamesAndLocations type (e.g. NamesAndLocations_2, etc.).
                        SyncAllNameAndLocationSourcesForSpecifiedType(r);
                    }
                }

            }

            inputDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);  //commit any unsaved rows with changes

            for (int r = 0; r < _colSpecs.Count; r++)
            {
                DataGridViewComboBoxCell randomizerTypeCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerTypeInx];
                DataGridViewComboBoxCell randomizerSourceCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerSourceInx];
                DataGridViewComboBoxCell randomizerFieldCell = (DataGridViewComboBoxCell)inputDataGrid.Rows[r].Cells[_randomizerFieldInx];
                int randomNamesAndLocationsNumber = -1;
                int numEmptyCells = -1;

                string cellValue = randomizerTypeCell.Value.ToString();
                if (cellValue.Replace(" ", string.Empty).StartsWith("RandomNamesAndLocations"))
                {
                    string lastChar = cellValue.ToString().Substring(cellValue.ToString().Length-1,1);
                    if (lastChar == "2" || lastChar=="3" || lastChar == "4" || lastChar == "5")
                    {
                        randomNamesAndLocationsNumber = Convert.ToInt32(lastChar);
                    }
                    cellValue = "Random Names And Locations";
                }
                enRandomDataType randomizerType = enRandomDataType.NotSpecified;
                if (cellValue.Trim() != string.Empty)
                {
                    randomizerType = (enRandomDataType)Enum.Parse(typeof(enRandomDataType), cellValue.Replace(" ", ""));
                }

                numEmptyCells = 0;
                if (String.IsNullOrEmpty(randomizerTypeCell.Value.ToString().Trim()))
                    numEmptyCells++;
                if (String.IsNullOrEmpty(randomizerSourceCell.Value.ToString().Trim()))
                    numEmptyCells++;
                if (String.IsNullOrEmpty(randomizerFieldCell.Value.ToString().Trim()))
                    numEmptyCells++;

                if (numEmptyCells > 0 && numEmptyCells < 3)
                {
                    //numEmptyCells = 3 means all celss in the row are blank (OK)
                    //numEmptyCells = 0 means all cells are filled in for the row (also OK)
                    //Any other value means one or two cells were not filled in (NOT OK - This means missing input values.)
                    _msg.Length = 0;
                    _msg.Append("One or more input values missing on row ");
                    _msg.Append((r+1).ToString());
                    _msg.Append(Environment.NewLine);
                    changesSaved = false;
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    break;
                }

                _colSpecs[r].RandomDataType = randomizerType;
                _colSpecs[r].RandomNamesAndLocationsNumber = randomNamesAndLocationsNumber;
                _colSpecs[r].RandomDataSource = randomizerSourceCell.Value.ToString().Trim();
                _colSpecs[r].RandomDataFieldName = randomizerFieldCell.Value.ToString().Trim().Replace(" ",string.Empty);
                _colSpecs[r].DataTableColumnIndex = r;

            }

            return changesSaved;
        }

        private void ManageRandomizerSources()
        {
            inputDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);  //commit any unsaved rows with changes

            int savedRowInx = inputDataGrid.CurrentRow.Index;

            RandomDataFormsManager frm = new RandomDataFormsManager();
            frm.ShowDialog();
            RefillAllSourceComboBoxes();
            inputDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);  //commit any unsaved rows with changes
            inputDataGrid.CurrentCell = inputDataGrid.Rows[savedRowInx].Cells[0];

        }

        private void cmdFirstGridRow_Click(object sender, EventArgs e)
        {
            if (_currentGridRowIndex > 0)
            {
                inputDataGrid.Rows[_currentGridRowIndex].Selected = false;
                inputDataGrid.Rows[0].Selected = true;
                RowEnter(0);
            }
        }

        private void cmdPrevGridRow_Click(object sender, EventArgs e)
        {
            if (_currentGridRowIndex > 0)
            {
                int prevRowIndex = _currentGridRowIndex - 1;
                inputDataGrid.Rows[_currentGridRowIndex].Selected = false;
                inputDataGrid.Rows[prevRowIndex].Selected = true;
                RowEnter(prevRowIndex);
            }
        }

        private void cmdNextGridRow_Click(object sender, EventArgs e)
        {
            if (_currentGridRowIndex < (_totalNumberGridRows - 1))
            {
                int nextRowIndex = _currentGridRowIndex + 1;
                inputDataGrid.Rows[_currentGridRowIndex].Selected = false;
                inputDataGrid.Rows[nextRowIndex].Selected = true;
                RowEnter(nextRowIndex);
            }
        }

        private void cmdLastGridRow_Click(object sender, EventArgs e)
        {
            int lastRowIndex = _totalNumberGridRows - 1;
            if (_currentGridRowIndex < lastRowIndex)
            {
                inputDataGrid.Rows[_currentGridRowIndex].Selected = false;
                inputDataGrid.Rows[lastRowIndex].Selected = true;
                RowEnter(lastRowIndex);
            }
        }

        private void inputDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == _randomizerSourceInx)
            {
                //do nothing
                return;
            }

            ;  //do nothing  

            //DataGridView dgv = (DataGridView)inputDataGrid;

            //_msg.Length = 0;
            //_msg.Append("DataError Event for grid has fired.\r\n");
            //_msg.Append("Row: ");
            //_msg.Append(e.RowIndex.ToString());
            //_msg.Append(" Col: ");
            //_msg.Append(e.ColumnIndex.ToString());
            //_msg.Append(Environment.NewLine);
            //_msg.Append("EditedFormattedValue: ");
            //_msg.Append(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue);
            //_msg.Append(Environment.NewLine);
            ////_msg.Append("FormattedValue: ");
            ////_msg.Append(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue);  //NOTE: sometimes blows up visual studio vshost exe
            ////_msg.Append(Environment.NewLine);
            //_msg.Append(e.Context.ToString());
            //_msg.Append(Environment.NewLine);
            //_msg.Append(AppMessages.FormatErrorMessageWithStackTrace(e.Exception));
            //AppMessages.DisplayErrorMessage(_msg.ToString());

            //dgv.Rows[e.RowIndex].ErrorText = "#Error#";
            //dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "#ERROR#";
            
            //e.Cancel = true;
        }

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Randomizer Column Mapper Form");
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
