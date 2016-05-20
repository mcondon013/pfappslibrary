using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using AppGlobals;
using pfDataExtractorCPProcesxor;
using pfDataExtractorCPObjects;
using PFDataAccessObjects;
using PFFileSystemObjects;
using PFTextFiles;
using PFPrinterObjects;
using PFAppDataObjects;
using PFRandomDataProcessor;
using PFTextObjects;
using PFCollectionsObjects;
using PFConnectionObjects;
using PFConnectionStrings;
using PFProviderForms;
using PFSQLServerCE35Objects;
using PFDocumentObjects;
using PFDocumentGlobals;
using KellermanSoftware.CompareNetObjects;
using PFRandomDataForms;
using PFTimers;

namespace pfDataExtractorCP
{
#pragma warning disable 1591
    /// <summary>
    /// Form for the definition and processing of data export and import definitions.
    /// </summary>
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _dataExtractorHelpFilePath = string.Empty;
        private string _queryBuilderHelpFilePath = string.Empty;
        private FormPrinter _printer = null;
        PFAppProcessor _appProcessor = new PFAppProcessor();
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();

        private delegate DialogResult ShowFileDialogDelegate();
        private ShowFileDialogDelegate[] _showFileDialog = new ShowFileDialogDelegate[2];
        private int _openFileDialogIndex = 0;
        private int _saveFileDialogIndex = 1;

        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "Extractor Def Files|*.exdef|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = false;
        private bool _showOverwritePrompt = false;
        private bool _showNewFolderButton = true;

        //fields for Mru processing
        MruStripMenu _msm;
        private bool _saveMruListToRegistry = true;
        private string _mRUListSaveFileSubFolder = @"PFApps\pfDataExtractorCP\Mru\";
        private string _mRUListSaveRegistryKey = @"SOFTWARE\PFApps\pfDataExtractorCP";
        private int _maxMruListEntries = 4;
        private bool _useSubMenuForMruList = true;

        //default folder and name definitions
        string _initDataFilesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\InitDataFiles\", "");
        private string _defaultRandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        private string _defaultExtractorDefinitionFileExtension = @".exdef";
        private string _defaultDataExtractorDefsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\ExtractorDefs\";
        private string _defaultDataExtractorName = string.Empty;
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\DataExports\";
        private string _initExtractDefinitionToOpen = string.Empty;
        private string _defaultSampleDatabaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\SampleData\";
        //private string _defaultSampleCEDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\SampleData\";
        //private string _defaultSampleAccessDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Sources\Access\SampleData\";
        private string _defaultSampleDefinitionFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Definitions\Samples\";
        private string _defaultCEDestinationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\SQLCE\";
        private string _defaultAccessDestinationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\Access\";
        private string _defaultSQLAnywhereDestinationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\Destinations\SQLAnywhere\";
        private string _defaultSampleRandomizerDefinitionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\";

        //current output options save areas
        private PFList<DataTableRandomizerColumnSpec> _currentRandomizerColSpecs = new PFList<DataTableRandomizerColumnSpec>();
        private PFFilter _currentOutputFilter = new PFFilter();

        private int _prevSourceDataLocationIndex = -1;
        //private int _prevDestDataLocationIndex = -1;

        private string _saveNonCsvExcelSheetName = string.Empty;
        private bool _saveUseExcelNamedRangeFormat = true;
        private bool _saveUseExcelRowColFormat = false;


        private PFExtractorOutputOptions[] _saveExtractorOutputOptions = null;
        //next two fields are used by FixedLengthTextFile source and destination processing
        private PFColumnDefinitionsExt _saveInputColumnDefinitions = new PFColumnDefinitionsExt(1);
        private PFColumnDefinitionsExt _saveOutputColumnDefinitions = new PFColumnDefinitionsExt(1);

        private PFExtractorDefinition _saveExtractorDefinition = null;
        private PFExtractorDefinition _exitExtractorDefinition = null;

        private PFRandomOrdersDefinition _randomOrdersDefinition = new PFRandomOrdersDefinition();


        public MainForm()
        {
            InitializeComponent();
        }

        //Properties

        /// <summary>
        /// InitExtractDefinitionToOpen Property.
        /// </summary>
        public string InitExtractDefinitionToOpen
        {
            get
            {
                return _initExtractDefinitionToOpen;
            }
            set
            {
                _initExtractDefinitionToOpen = value;
            }
        }


        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdRunExtract_Click(object sender, EventArgs e)
        {
            RunExtract();
        }

        private void cboSourceDataLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataLocationSelectionChanged(true);
        }

        private void cboDestDataLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataLocationSelectionChanged(false);
        }

        private void tabSourcesAndDestinations_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTabChanged();
        }

        private void cmdGetExtractorDefsFolder_Click(object sender, EventArgs e)
        {
            BrowseForExtractorDefsFolder();
        }

        private void optSourceExcel2007Format_CheckedChanged(object sender, EventArgs e)
        {
            ExcelSourceFormatChanged();
        }

        private void optSourceExcel2003Format_CheckedChanged(object sender, EventArgs e)
        {
            ExcelSourceFormatChanged();
        }

        private void optSourceExcelCSVFormat_CheckedChanged(object sender, EventArgs e)
        {
            ExcelSourceFormatChanged();
        }

        private void optExcelNamedRange_CheckedChanged(object sender, EventArgs e)
        {
            NamedRangeCheckedChanged();
        }

        private void optSourceCommaDelimited_CheckedChanged(object sender, EventArgs e)
        {
            SourceDelimiterChanged();
        }

        private void optSourceTabDelimited_CheckedChanged(object sender, EventArgs e)
        {
            SourceDelimiterChanged();
        }

        private void optSourceOtherDelimiter_CheckedChanged(object sender, EventArgs e)
        {
            SourceDelimiterChanged();
        }

        private void chkSourceLineTerminatorAppendedToEachLine_CheckedChanged(object sender, EventArgs e)
        {
            SourceLineTerminatorAppendedToEachLineCheckedChanged();
        }

        private void optDestCommaDelimited_CheckedChanged(object sender, EventArgs e)
        {
            DestDelimiterChanged();
        }

        private void optDestTabDelimited_CheckedChanged(object sender, EventArgs e)
        {
            DestDelimiterChanged();
        }

        private void optDestOtherDelimiter_CheckedChanged(object sender, EventArgs e)
        {
            DestDelimiterChanged();
        }

        private void optDestCrLf_CheckedChanged(object sender, EventArgs e)
        {
            DestLineTerminatorChanged();
        }

        private void optDestOtherLineTerminator_CheckedChanged(object sender, EventArgs e)
        {
            DestLineTerminatorChanged();
        }

        private void cboDataDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDefaultConnectionStringForDatabaseType(this.cboDataDestination, this.txtDestinationConnectionString, this.txtOutputBatchSize);
            ResetTableSchema();
            SetRelationalDatabaseDestinationTableName();
        }

        private void cboDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSourceSelectedIndexChanged();
        }

        private void cmdDefineConnectionString_Click(object sender, EventArgs e)
        {
            DefineConnectionString(this.cboDataSource, this.txtSourceConnectionString, true);
        }

        private void cmdBuildConnectionString_Click(object sender, EventArgs e)
        {
            DefineConnectionString(this.cboDataDestination, this.txtDestinationConnectionString, false);
        }

        private void cmdDefineSqlQuery_Click(object sender, EventArgs e)
        {
            RunQueryBuilder(cboDataSource, txtSourceConnectionString, txtRelDbSqlQuery);
        }

        private void cmdDefineAccessQuery_Click(object sender, EventArgs e)
        {
            RunQueryBuilderForAccess();
        }

        private void cmdSourceGetAccessDatabaseFilePath_Click(object sender, EventArgs e)
        {
            SourceGetAccessDatabaseFilePath();
        }

        private void cmdSourceGetExcelFile_Click(object sender, EventArgs e)
        {
            SourceGetExcelFile();
        }
        
        private void cmdSourceGetDelimitedTextFilePath_Click(object sender, EventArgs e)
        {
            SourceGetDelimitedTextFilePath();
            this.Focus();
        }

        private void cmdSourceGetFixedLengthTextFilePath_Click(object sender, EventArgs e)
        {
            SourceGetFixedLengthTextFilePath();
        }

        private void cmdDefineFixedLenSourceColDefs_Click(object sender, EventArgs e)
        {
            _appProcessor.ShowFixedLenInputColSpecForm(ref _saveInputColumnDefinitions,
                                                       pfDataExtractorCP.Properties.Settings.Default["DefaultSourceFixedLengthTextFileFolder"].ToString(),
                                                       this.txtSourceFixedLengthTextFilePath.Text,
                                                       this.chkSourceLineTerminatorAppendedToEachLine.Checked,
                                                       this.chkSourceColumnNamesOnFirstLineOfFile.Checked,
                                                       AppTextGlobals.ConvertStringToInt(this.txtSourceFixedLengthExpectedLineWidth.Text, 1));
            
            this.txtSourceFixedLengthExpectedLineWidth.Text = _appProcessor.ExpectedLineLength.ToString();
            
            this.Focus();
        }

        private void cmdSourceGetXmlFilePath_Click(object sender, EventArgs e)
        {
            SourceGetXmlFilePath();
        }

        private void cmdSourceGetXsdFilePath_Click(object sender, EventArgs e)
        {
            SourceGetXsdFilePath();
        }

        private void cmdDestGetAccessDatabseFilePath_Click(object sender, EventArgs e)
        {
            DestGetAccessDatabaseFilePath();
        }

        private void cmdDestGetExcelFile_Click(object sender, EventArgs e)
        {
            DestGetExcelFile();
        }

        private void cmdDestGetDelimitedTextFilePath_Click(object sender, EventArgs e)
        {
            DestGetDelimitedTextFilePath();
        }

        private void cmdDestGetFixedLengthTextFilePath_Click(object sender, EventArgs e)
        {
            DestGetFixedLengthTextFilePath();
        }

        private void chkDestUseLineTerminator_CheckedChanged(object sender, EventArgs e)
        {
            DestUseLineTerminatorCheckedChanged();
        }

        private void optDestFixedLenCrLf_CheckedChanged(object sender, EventArgs e)
        {
            DestFixedLenCrLfCheckedChanged();
        }

        private void cmdDefineFixedLengthDestDataColumns_Click(object sender, EventArgs e)
        {
            PFExtractorDefinition exDef = CreateExtractorDefinition();
            _appProcessor.ShowFixedLenOutputColSpecForm(exDef, ref _saveOutputColumnDefinitions);
            CalculateExpectedOutputColumnLength(_saveOutputColumnDefinitions);
            this.Focus();
        }

        private void cmdDestGetXmlFilePath_Click(object sender, EventArgs e)
        {
            DestGetXmlFilePath();
        }

        private void cmdDestGetXsdFilePath_Click(object sender, EventArgs e)
        {
            DestGetXsdFilePath();
        }

        private void chkShowRowNumber_CheckedChanged(object sender, EventArgs e)
        {
            _saveExtractorOutputOptions[this.cboSourceDataLocation.SelectedIndex].AddRowNumberToOutput = this.chkShowRowNumber.Checked;
            CalculateExpectedOutputColumnLength(_saveOutputColumnDefinitions);
        }

        private void chkRandomizeOutput_CheckedChanged(object sender, EventArgs e)
        {
            _saveExtractorOutputOptions[this.cboSourceDataLocation.SelectedIndex].RandomizeOutput = this.chkRandomizeOutput.Checked;
        }

        private void chkFilterOutput_CheckedChanged(object sender, EventArgs e)
        {
            _saveExtractorOutputOptions[this.cboSourceDataLocation.SelectedIndex].FilterOutput = this.chkFilterOutput.Checked;
        }

        private void cmdRandomizeOutput_Click(object sender, EventArgs e)
        {
            ShowRandomizerBuilder();
            this.Focus();
        }

        private void cmdFilterOutput_Click(object sender, EventArgs e)
        {
            ShowFilterBuilder();
        }

        private void cmdOpenDefinition_Click(object sender, EventArgs e)
        {
            FileOpen();
        }

        private void cmdSaveDefinition_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void cmdRelationalDatabasePreview_Click(object sender, EventArgs e)
        {
            RunPreviewSource();
        }

        private void cmdAccessPreview_Click(object sender, EventArgs e)
        {
            RunPreviewSource();
        }

        private void cmdExcelFilePreview_Click(object sender, EventArgs e)
        {
            RunPreviewSource();
        }

        private void cmdDelimitedTextFilePreview_Click(object sender, EventArgs e)
        {
            RunPreviewSource();
        }

        private void cmdFixedLengthTextFilePreview_Click(object sender, EventArgs e)
        {
            RunPreviewSource();
        }

        private void cmdXmlFilePreview_Click(object sender, EventArgs e)
        {
            RunPreviewSource();
        }

        private void cmdPreviewOutput_Click(object sender, EventArgs e)
        {
            RunPreviewOutput();
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

        private void mnuTestDataGenerate_Click(object sender, EventArgs e)
        {
            RunGenerateTestDataForm();
        }

        private void mnuTestDataGenerateOrderTransactions_Click(object sender, EventArgs e)
        {
            GenerateTestOrderTransactions();
        }

        private void mnuRandomizerManageSources_Click(object sender, EventArgs e)
        {
            ManageRandomizerSources();
        }

        private void mnuToolsOptionsUserSettings_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsUserSettings();
        }

        private void mnuToolsOptionsFolderSettngs_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsFolderSettings();
        }

        private void mnuToolsOptionsDatabaseSettings_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsDatabaseSettings();
        }

        private void mnuToolsOptionsApplicationSettings_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsApplicationSettings();
        }

        private void mnuToolsShowHideOutputLog_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
        }

        private void mnuToolsSelectDatabaseProvidersToUse_Click(object sender, EventArgs e)
        {
            SelectDatabaseProvidersToUse();
        }

        private void mnuToolsResetEmptyMruList_Click(object sender, EventArgs e)
        {
            DeleteMruList();
        }

        private void mnuToolsConnectionStringManager_Click(object sender, EventArgs e)
        {
            ShowToolsConnectionStringManager();
        }

        private void mnuToolsFormSaveScreenLocations_Click(object sender, EventArgs e)
        {
            SaveScreenLocations();
        }

        private void mnuToolsFormRestoreScreenLocations_Click(object sender, EventArgs e)
        {
            RestoreDefaultScreenLocations();
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
        }

        private void mnuHelpTutorialsDataExtractor_Click(object sender, EventArgs e)
        {
            ShowDataExtractorHelpTutorial();
        }

        private void mnuHelpTutorialsQueryBuilder_Click(object sender, EventArgs e)
        {
            ShowQueryBuilderHelpTutorial();
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
                        if (sourceControl.Name == "chkShowDateTimeTest")
                        {
                            //this.chkShowDateTimeTest.Checked = true;
                            this.RunExtract();
                        }
                        if (sourceControl.Name == "chkShowHelpAboutTest")
                        {
                            //this.chkShowHelpAboutTest.Checked = true;
                            this.RunExtract();
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

        private void ShowToolsOptionsUserSettings()
        {
            UserOptionsForm usrOptionsForm = new UserOptionsForm();
            bool saveShowInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            bool saveShowOutputLog = Properties.Settings.Default.ShowOutputLog;
            int saveBatchSizeForDataImportsAndExports = Properties.Settings.Default.BatchSizeForDataImportsAndExports;
            Font saveDefaultApplicationFont = Properties.Settings.Default.DefaultApplicationFont;
            bool saveLimitPreviewRoles = Properties.Settings.Default.LimitPreviewRows;
            string saveDefaultExtractorDefinitionsSaveFolder = Properties.Settings.Default.DefaultExtractorDefinitionsSaveFolder;
            string saveDefaultExtractorDefinitionName = Properties.Settings.Default.DefaultExtractorDefinitionName;

            try
            {
                usrOptionsForm.ShowDialog();

                bool currentShowInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
                if (currentShowInstalledDatabaseProvidersOnly != saveShowInstalledDatabaseProvidersOnly)
                {
                    LoadProviderListDropdowns();
                }

                Font currentDefaultApplicationFont = Properties.Settings.Default.DefaultApplicationFont;
                if (saveDefaultApplicationFont != currentDefaultApplicationFont)
                {
                    SetTextFont();
                }

                bool currentShowApplicationLogWindow = Properties.Settings.Default.ShowOutputLog;
                if (saveShowOutputLog != currentShowApplicationLogWindow)
                {
                    UpdateAppLogWindowVisibility();
                }

                int currentBatchSizeForDataImportsAndExports = Properties.Settings.Default.BatchSizeForDataImportsAndExports;
                if (saveBatchSizeForDataImportsAndExports != currentBatchSizeForDataImportsAndExports)
                {
                    //this.txtOutputBatchSize.Text = currentBatchSizeForDataImportsAndExports.ToString();
                    SetBatchSize();
                }

                bool currentLimitPreviewRoles = Properties.Settings.Default.LimitPreviewRows;
                if (saveLimitPreviewRoles != currentLimitPreviewRoles)
                {
                    SetMaxPreviewRows();
                }

                string currentDefaultExtractorDefinitionsSaveFolder = Properties.Settings.Default.DefaultExtractorDefinitionsSaveFolder;
                if (saveDefaultExtractorDefinitionsSaveFolder != currentDefaultExtractorDefinitionsSaveFolder)
                {
                    if (this.txtExtractorDefinitionsSaveFolder.Text == saveDefaultExtractorDefinitionsSaveFolder)
                    {
                        this.txtExtractorDefinitionsSaveFolder.Text = currentDefaultExtractorDefinitionsSaveFolder;
                    }
                }

                string currentDefaultExtractorDefinitionName = Properties.Settings.Default.DefaultExtractorDefinitionName;
                if (saveDefaultExtractorDefinitionName != currentDefaultExtractorDefinitionName)
                {
                    if (this.txtExtractorName.Text == saveDefaultExtractorDefinitionName)
                    {
                        this.txtExtractorName.Text = currentDefaultExtractorDefinitionName;
                    }
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                usrOptionsForm.Close();
                usrOptionsForm = null;
                this.Focus();
                this.Refresh();
            }

        }


        public void ShowToolsOptionsFolderSettings()
        {
            FolderOptionsForm frm = new FolderOptionsForm();

            try
            {
                frm.ShowDialog();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                frm.Close();
                frm = null;
                this.Focus();
                this.Refresh();
            }
        }


        public void ShowToolsOptionsDatabaseSettings()
        {
            DatabaseOptionsForm frm = new DatabaseOptionsForm();
            bool saveShowInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            int saveBatchSizeForDataImportsAndExports = Properties.Settings.Default.BatchSizeForDataImportsAndExports;
            string saveDefaultInputDatabaseType = Properties.Settings.Default.DefaultInputDatabaseType;
            string saveDefaultInputDatabaseConnectionString = Properties.Settings.Default.DefaultInputDatabaseConnectionString;
            string saveDefaultOutputDatabaseType = Properties.Settings.Default.DefaultOutputDatabaseType;
            string saveDefaultOutputDatabaseConnectionString = Properties.Settings.Default.DefaultOutputDatabaseConnectionString;
            bool saveOverwriteOutputDestinationIfAlreadyExists = Properties.Settings.Default.OverwriteOutputDestinationIfAlreadyExists;

            try
            {
                frm.ShowDialog();

                bool currentShowInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
                if (currentShowInstalledDatabaseProvidersOnly != saveShowInstalledDatabaseProvidersOnly)
                {
                    LoadProviderListDropdowns();
                }

                int currentBatchSizeForDataImportsAndExports = Properties.Settings.Default.BatchSizeForDataImportsAndExports;
                if (saveBatchSizeForDataImportsAndExports != currentBatchSizeForDataImportsAndExports)
                {
                    //this.txtOutputBatchSize.Text = currentBatchSizeForDataImportsAndExports.ToString();
                    SetBatchSize();
                }

                string currentDefaultInputDatabaseType = Properties.Settings.Default.DefaultInputDatabaseType;
                if (saveDefaultInputDatabaseType != currentDefaultInputDatabaseType)
                {
                    if (this.cboDataSource.Text == saveDefaultInputDatabaseType)
                    {
                        this.cboDataSource.Text = currentDefaultInputDatabaseType;
                    }
                }

                string currentDefaultInputDatabaseConnectionString = Properties.Settings.Default.DefaultInputDatabaseConnectionString;
                if (saveDefaultInputDatabaseConnectionString != currentDefaultInputDatabaseConnectionString)
                {
                    if (this.txtSourceConnectionString.Text == saveDefaultInputDatabaseConnectionString)
                    {
                        this.txtSourceConnectionString.Text = currentDefaultInputDatabaseConnectionString;
                    }
                }

                string currentDefaultOutputDatabaseType = Properties.Settings.Default.DefaultOutputDatabaseType;
                if (saveDefaultOutputDatabaseType != currentDefaultOutputDatabaseType)
                {
                    if (this.cboDataDestination.Text == saveDefaultOutputDatabaseType)
                    {
                        this.cboDataDestination.Text = currentDefaultOutputDatabaseType;
                    }
                }

                string currentDefaultOutputDatabaseConnectionString = Properties.Settings.Default.DefaultOutputDatabaseConnectionString;
                if (saveDefaultOutputDatabaseConnectionString != currentDefaultOutputDatabaseConnectionString)
                {
                    if (this.txtDestinationConnectionString.Text == saveDefaultOutputDatabaseConnectionString)
                    {
                        this.txtDestinationConnectionString.Text = currentDefaultOutputDatabaseConnectionString;
                    }
                }

                bool currentOverwriteOutputDestinationIfAlreadyExists = Properties.Settings.Default.OverwriteOutputDestinationIfAlreadyExists;
                if (currentOverwriteOutputDestinationIfAlreadyExists != saveOverwriteOutputDestinationIfAlreadyExists)
                {
                    SetOutputOverwriteCheckBoxes();
                }



            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                frm.Close();
                frm = null;
                this.Focus();
                this.Refresh();
            }
        }
                 
        
        private void ShowToolsOptionsApplicationSettings()
        {
            _appProcessor.ShowAppConfigManager();
            this.InitMruList();
            this.SetLoggingValues();
            this.SetHelpFileValues();
            this.Focus();
            this.Refresh();

        }


        private void ShowHelpContents()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataExtractorHelpFilePath, HelpNavigator.TableOfContents);
        }

        private void ShowHelpIndex()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataExtractorHelpFilePath, HelpNavigator.Index);
        }

        private void ShowHelpSearch()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataExtractorHelpFilePath, HelpNavigator.Find, "");
        }

        private void ShowDataExtractorHelpTutorial()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataExtractorHelpFilePath, HelpNavigator.KeywordIndex, "Tutorial");
        }

        private void ShowQueryBuilderHelpTutorial()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _queryBuilderHelpFilePath, HelpNavigator.TableOfContents);
        }

        private void ShowHelpContact()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataExtractorHelpFilePath, HelpNavigator.KeywordIndex, "Contact Information");
        }

        private void ShowHelpAbout()
        {
            HelpAboutForm appHelpAboutForm = new HelpAboutForm();
            appHelpAboutForm.ShowDialog();

        }

        private void ShowHelpMainFormOverview()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _dataExtractorHelpFilePath, HelpNavigator.KeywordIndex, "Overview");
        }

        private bool HelpFileExists()
        {
            bool ret = false;

            if (File.Exists(_dataExtractorHelpFilePath))
            {
                ret = true;
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_dataExtractorHelpFilePath);
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
            if (Properties.Settings.Default.SaveFormLocationsOnExit)
                SaveScreenLocations();

            if (ExtractorDefinitionHasChanges())
            {
                DialogResult res = PromptForFileSave(ReasonForFileSavePrompt.ApplicationExit);
                if (res == DialogResult.Yes)
                {
                    if (ExpectedFileSaveInfoFound())
                        FileSave();
                    else
                        e.Cancel = true;
                }
                if (res == DialogResult.Cancel)
                    e.Cancel = true;
            }


        }


        private bool ExtractorDefinitionHasChanges()
        {
            bool retval = false;
            CompareLogic oCompare = new CompareLogic();

            _exitExtractorDefinition = CreateExtractorDefinition();

            oCompare.Config.MaxDifferences = 10;

            ComparisonResult compResult = oCompare.Compare(_saveExtractorDefinition, _exitExtractorDefinition);

            if (compResult.AreEqual == false)
            {
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Extractor definition has changes:\r\n");
                _msg.Append(compResult.DifferencesString);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());
            }

            retval = !compResult.AreEqual;

            return retval;
        }

        private DialogResult PromptForFileSave(ReasonForFileSavePrompt promptReason)
        {
            return PromptForFileSave(promptReason, MessageBoxButtons.YesNoCancel);
        }

        private DialogResult PromptForFileSaveYesNo(ReasonForFileSavePrompt promptReason)
        {
            return PromptForFileSave(promptReason, MessageBoxButtons.YesNo);
        }

        private DialogResult PromptForFileSave(ReasonForFileSavePrompt promptReason, MessageBoxButtons btns)
        {
            DialogResult res = System.Windows.Forms.DialogResult.None;

            _msg.Length = 0;
            _msg.Append("The are unsaved changes to the extractor definition. ");
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
                case ReasonForFileSavePrompt.DefaultOptionsChange:
                    _msg.Append("before reset for options change");
                    break;
                case ReasonForFileSavePrompt.DataSourceChange:
                    _msg.Append("before changing data source");
                    break;
                default:
                    _msg.Append("now");
                    break;
            }
            _msg.Append("?");
            _msg.Append(Environment.NewLine);

            res = AppMessages.DisplayMessage(_msg.ToString(), "File save now?", btns, MessageBoxIcon.Exclamation);

            return res;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                //this.Text = AppInfo.AssemblyProduct;
                this.Text = "Data Extractor with Data Masking";

                this.chkEraseOutputLogBeforeEachTest.Checked = true;

                SetFormLocationAndSize();

                SetLoggingValues();

                SetHelpFileValues();

                InitMruList();

                UpdateAppLogWindowVisibility();

                SetTextFont();

                SetFileDialogDelegates();

                InitDefaultDirectories();

                InitSampleDefinitions();

                InitDefaultUserSettings();
                
                SetFormValues();

                InitAppProcessor();

                _printer = new FormPrinter(this);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }

        }

        internal void SetFormLocationAndSize()
        {
            if (Properties.Settings.Default.MainFormX != 0 || Properties.Settings.Default.MainFormY != 0)
            {
                this.Location = new Point(Properties.Settings.Default.MainFormX, Properties.Settings.Default.MainFormY);
            }

            if (Properties.Settings.Default.MessageLogX != 0
                || Properties.Settings.Default.MessageLogY != 0)
            {
                Program._messageLog.Form.Location = new Point(Properties.Settings.Default.MessageLogX, Properties.Settings.Default.MessageLogY);
            }

            if ((Properties.Settings.Default.MessageLogWidth != Properties.Settings.Default.MessageLogDefaultWidth)
                || (Properties.Settings.Default.MessageLogHeight != Properties.Settings.Default.MessageLogDefaultHeight))
            {
                if (Properties.Settings.Default.MessageLogWidth > 0 && Properties.Settings.Default.MessageLogHeight > 0)
                {
                    Program._messageLog.Form.Width = Properties.Settings.Default.MessageLogWidth;
                    Program._messageLog.Form.Height = Properties.Settings.Default.MessageLogHeight;
                }
            }

        }

        internal void InitMruList()
        {
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder", @"PFApps\pfFolderSize\Mru\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey", @"SOFTWARE\PFApps\pfFolderSize");
            _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries", (int)4);
            _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList", "true");

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

            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
        }

        internal void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "pfDataExtractorCP.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _dataExtractorHelpFilePath = helpFilePath;

            string qbfHelpFileName = AppConfig.GetStringValueFromConfigFile("QBFHelpFileName", "QbfUsersGuide.chm");
            string qbfHelpFilePath = PFFile.FormatFilePath(executableFolder, qbfHelpFileName);
            _queryBuilderHelpFilePath = qbfHelpFilePath;

            _appProcessor.HelpFilePath = _dataExtractorHelpFilePath;

        }

        private void InitDefaultDirectories()
        {
            SettingsInitializer si = new SettingsInitializer();

            si.InitFolderOptionsSettings();   //routine creates default directories if they do not already exist

            InitDataFileLocation();   //initialize folder that contains XML files used for randomizing data


        }

        private void InitDataFileLocation()
        {
            string configValue = string.Empty;
            string propertyValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultRandomDataXmlFilesFolder", string.Empty);
            if (configValue.Trim().Length != 0)
                _defaultRandomDataXmlFilesFolder = configValue;

            if (Directory.Exists(_defaultRandomDataXmlFilesFolder) == false)
                Directory.CreateDirectory(_defaultRandomDataXmlFilesFolder);

            DirectoryInfo dirInfo = new DirectoryInfo(_defaultRandomDataXmlFilesFolder);
            if (dirInfo.GetFiles("RandomData.sdf").Length == 0)
            {
                LoadRandomDataDatabase();
            }

            if (dirInfo.GetFiles("TestOrders.sdf").Length == 0)
            {
                LoadTestOrdersGeneratorDatabase();
            }

            if (dirInfo.GetFiles("*.dat").Length == 0)
            {
                LoadRandomDataXmlFiles();
            }

            //dirInfo = new DirectoryInfo(_defaultSampleDatabaseFolder);
            //if (dirInfo.GetFiles("SampleOrderDataCE35.sdf").Length == 0)
            //{
            //    LoadSampleCEDatabase("SampleOrderDataCE35.zip", "SampleOrderDataCE35.sdf");
            //}
            //if (dirInfo.GetFiles("SampleOrderDataCE40.sdf").Length == 0)
            //{
            //    LoadSampleCEDatabase("SampleOrderDataCE40.zip", "SampleOrderDataCE40.sdf");
            //}
            //dirInfo = new DirectoryInfo(_defaultSampleDatabaseFolder);
            //if (dirInfo.GetFiles("SampleOrderData.accdb").Length == 0)
            //{
            //    LoadSampleAccessDatabase("SampleOrderData.accdb.zip", "SampleOrderData.accdb");
            //}
            //if (dirInfo.GetFiles("SampleOrderData.mdb").Length == 0)
            //{
            //    LoadSampleAccessDatabase("SampleOrderData.mdb.zip", "SampleOrderData.mdb");
            //}

            dirInfo = new DirectoryInfo(_defaultSampleDatabaseFolder);
            if (dirInfo.GetFiles("SampleOrderDataCE35.sdf").Length == 0)
            {
                LoadSampleDatabase("SampleOrderDataCE35.zip", "SampleOrderDataCE35.sdf");
            }
            if (dirInfo.GetFiles("SampleOrderDataCE40.sdf").Length == 0)
            {
                LoadSampleDatabase("SampleOrderDataCE40.zip", "SampleOrderDataCE40.sdf");
            }
            if (dirInfo.GetFiles("SampleOrderData.accdb").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.accdb.zip", "SampleOrderData.accdb");
            }
            if (dirInfo.GetFiles("SampleOrderData.mdb").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.mdb.zip", "SampleOrderData.mdb");
            }
            if (dirInfo.GetFiles("SampleOrderData.db").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.db.zip", "SampleOrderData.db");
            }
            if (dirInfo.GetFiles("SampleOrderData.udb").Length == 0)
            {
                LoadSampleDatabase("SampleOrderData.udb.zip", "SampleOrderData.udb");
            }


        }

        private void LoadRandomDataDatabase()
        {
            _msg.Length = 0;
            _msg.Append("Loading random data database into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            Program._messageLog.WriteLine(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;
            //copy random data database file
            string sdfSourceFilePath = Path.Combine(_initDataFilesFolder, "RandomData.sdf");
            string sdfDestinationFilePath = Path.Combine(destinationFolder, "RandomData.sdf");
            File.Copy(sdfSourceFilePath, sdfDestinationFilePath, false);

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of random data database has finished.");
            Program._messageLog.WriteLine(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load random data database: ");
            _msg.Append(sw.FormattedElapsedTime);
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private void LoadTestOrdersGeneratorDatabase()
        {
            _msg.Length = 0;
            _msg.Append("Loading test orders generator database into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            Program._messageLog.WriteLine(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;

            //unzip and copy random word and names XML files
            string zipArchiveFile = Path.Combine(sourceFolder, "TestOrders.zip");
            ZipArchive za = new ZipArchive(zipArchiveFile);
            za.ExtractAll(destinationFolder);
            za = null;


            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of test orders generator database has finished.");
            Program._messageLog.WriteLine(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load test orders generator database: ");
            _msg.Append(sw.FormattedElapsedTime);
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private void LoadRandomDataXmlFiles()
        {
            _msg.Length = 0;
            _msg.Append("Loading random data files into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            Program._messageLog.WriteLine(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;

            //unzip and copy random word and names XML files
            string zipArchiveFile = Path.Combine(sourceFolder, "RandomDataFiles.zip");
            ZipArchive za = new ZipArchive(zipArchiveFile);
            za.ExtractAll(destinationFolder);
            za = null;

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of random data files has finished.");
            Program._messageLog.WriteLine(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load random data files: ");
            _msg.Append(sw.FormattedElapsedTime);
            Program._messageLog.WriteLine(_msg.ToString());

        }

        //private void LoadSampleCEDatabase(string zipFile, string databaseFile)
        //{
        //    _msg.Length = 0;
        //    if (File.Exists(Path.Combine(_defaultSampleDatabaseFolder, zipFile)))
        //    {
        //        _msg.Append("Loading sample data databases into application sample data folder: ");
        //        _msg.Append(_defaultSampleDatabaseFolder);
        //        _msg.Append(Environment.NewLine);
        //        _msg.Append("Database name: ");
        //        _msg.Append(databaseFile);
        //        Program._messageLog.WriteLine(_msg.ToString());
        //    }
        //    else
        //    {
        //        _msg.Append("Unable to load sample data databases into application sample data folder: ");
        //        _msg.Append(_defaultSampleDatabaseFolder);
        //        _msg.Append(Environment.NewLine);
        //        _msg.Append("Expected zip file is missing: ");
        //        _msg.Append(zipFile);
        //        Program._messageLog.WriteLine(_msg.ToString());
        //        return;
        //    }


        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    string sourceFolder = _defaultSampleDatabaseFolder;
        //    string destinationFolder = _defaultSampleDatabaseFolder;

        //    //unzip and copy random word and names XML files
        //    string zipArchiveFile = Path.Combine(sourceFolder, zipFile);
        //    ZipArchive za = new ZipArchive(zipArchiveFile);
        //    za.ExtractAll(destinationFolder);
        //    za = null;

        //    sw.Stop();

        //    _msg.Length = 0;
        //    _msg.Append("Load of sample data database has finished.");
        //    Program._messageLog.WriteLine(_msg.ToString());
        //    _msg.Length = 0;
        //    _msg.Append("Time to load sample data database: ");
        //    _msg.Append(sw.FormattedElapsedTime);
        //    Program._messageLog.WriteLine(_msg.ToString());

        //}


        //private void LoadSampleAccessDatabase(string zipFile, string databaseFile)
        //{
        //    _msg.Length = 0;
        //    if (File.Exists(Path.Combine(_defaultSampleDatabaseFolder, zipFile)))
        //    {
        //        _msg.Append("Loading sample data databases into application sample data folder: ");
        //        _msg.Append(_defaultSampleDatabaseFolder);
        //        _msg.Append(Environment.NewLine);
        //        _msg.Append("Database name: ");
        //        _msg.Append(databaseFile);
        //        Program._messageLog.WriteLine(_msg.ToString());
        //    }
        //    else
        //    {
        //        _msg.Append("Unable to load sample data databases into application sample data folder: ");
        //        _msg.Append(_defaultSampleDatabaseFolder);
        //        _msg.Append(Environment.NewLine);
        //        _msg.Append("Expected zip file is missing: ");
        //        _msg.Append(zipFile);
        //        Program._messageLog.WriteLine(_msg.ToString());
        //        return;
        //    }


        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();

        //    string sourceFolder = _defaultSampleDatabaseFolder;
        //    string destinationFolder = _defaultSampleDatabaseFolder;

        //    //unzip and copy random word and names XML files
        //    string zipArchiveFile = Path.Combine(sourceFolder, zipFile);
        //    ZipArchive za = new ZipArchive(zipArchiveFile);
        //    za.ExtractAll(destinationFolder);
        //    za = null;

        //    sw.Stop();

        //    _msg.Length = 0;
        //    _msg.Append("Load of sample data database has finished.");
        //    Program._messageLog.WriteLine(_msg.ToString());
        //    _msg.Length = 0;
        //    _msg.Append("Time to load sample data database: ");
        //    _msg.Append(sw.FormattedElapsedTime);
        //    Program._messageLog.WriteLine(_msg.ToString());

        //}

        private void LoadSampleDatabase(string zipFile, string databaseFile)
        {
            _msg.Length = 0;
            if (File.Exists(Path.Combine(_defaultSampleDatabaseFolder, zipFile)))
            {
                _msg.Append("Loading sample data databases into application sample data folder: ");
                _msg.Append(_defaultSampleDatabaseFolder);
                _msg.Append(Environment.NewLine);
                _msg.Append("Database name: ");
                _msg.Append(databaseFile);
                Program._messageLog.WriteLine(_msg.ToString());
            }
            else
            {
                _msg.Append("Unable to load sample data databases into application sample data folder: ");
                _msg.Append(_defaultSampleDatabaseFolder);
                _msg.Append(Environment.NewLine);
                _msg.Append("Expected zip file is missing: ");
                _msg.Append(zipFile);
                Program._messageLog.WriteLine(_msg.ToString());
                return;
            }


            Stopwatch sw = new Stopwatch();
            sw.Start();

            string sourceFolder = _defaultSampleDatabaseFolder;
            string destinationFolder = _defaultSampleDatabaseFolder;

            //unzip and copy random word and names XML files
            string zipArchiveFile = Path.Combine(sourceFolder, zipFile);
            ZipArchive za = new ZipArchive(zipArchiveFile);
            za.ExtractAll(destinationFolder);
            za = null;

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of sample data database has finished.");
            Program._messageLog.WriteLine(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load sample data database: ");
            _msg.Append(sw.FormattedElapsedTime);
            Program._messageLog.WriteLine(_msg.ToString());

        }

        private void InitSampleDefinitions()
        {
            string filename = string.Empty;
            string originalMyDocumentsPath = @"C:\Users\Mike\Documents\";
            string currentMyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string originalAppDataPath = @"C:\Users\Mike\AppData\Roaming\";
            string currentAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (currentMyDocumentsPath.EndsWith(@"\") == false)
            {
                currentMyDocumentsPath = currentMyDocumentsPath + @"\";
            }
            if (currentAppDataPath.EndsWith(@"\") == false)
            {
                currentAppDataPath = currentAppDataPath + @"\";
            }

            filename = Path.Combine(_defaultSampleDefinitionFolder, "Access_to_SQLCE_SampleExtractUSA.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "DelimitedText_to_SQLCE_SampleExtractUSA.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "Excel_to_SQLCE_SampleExtractUSA.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "FixedLengthText_to_SQLCE_SampleExtractUSA.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_Access_SampleExtract.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_Excel_SampleExtract.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_SQLCE_SampleExtractNewEngland.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_SQLCE_SampleExtractQuebec.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_SQLCE_SampleExtractUSA.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_TextFileDelimited_SampleExtract.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_TextFileFixedLength_SampleExtract.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "SQLCE_to_XML_SampleExtract.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);

            filename = Path.Combine(_defaultSampleDefinitionFolder, "XML_to_SQLCE_SampleExtractUSA.exdef");
            UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);


            //***************************************************************************************
            //Fix initial set of randomizer definitions, if necessary, that ship with the product
            //***************************************************************************************
            //*
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"CustomValues\", "LastNamesThatStartWithS.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_Mexico.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_MonterreyArea_MEX.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_Quebec.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "BusinessNames_USA_Midwest.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonAndBusinessNames_USA_CAN_MEX.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_California.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_Canada.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_Mexico.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_NewEngland.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_Quebec.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "PersonNames_USA.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "Persons_NewYorkCity_CMSA.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }
            filename = Path.Combine(_defaultSampleRandomizerDefinitionsFolder + @"NamesAndLocations\", "Persons_NewYorkCity_MetArea.xml");
            if (File.Exists(filename))
            {
                UpdateMyDocumentsPathInDefinitionsFile(filename, originalMyDocumentsPath, currentMyDocumentsPath, originalAppDataPath, currentAppDataPath);
            }

        }

        private void UpdateMyDocumentsPathInDefinitionsFile(string filename, string originalMyDocumentsPath, string currentMyDocumentsPath,
                                                                             string originalAppDataPath, string currentAppDataPath)
        {
            string fileContents = string.Empty;
            string modifiedFileContents = string.Empty;
            if (File.Exists(filename))
            {
                fileContents = File.ReadAllText(filename);
                modifiedFileContents = fileContents.Replace(originalMyDocumentsPath, currentMyDocumentsPath).Replace(originalAppDataPath, currentAppDataPath);
                File.WriteAllText(filename, modifiedFileContents);
            }
        }


        private void InitDefaultUserSettings()
        {
            SettingsInitializer si = new SettingsInitializer();

            si.InitUserOptionsSettings();

        }

        private void SetFormValues()
        {
            string configValue = string.Empty;

            SetExtractorDefinitionsSaveFolder();

            SetExtractorDefinitionName();

            SetMaxPreviewRows();

            SetOutputOptions();
            
            SetSaveOutputOptions();

            SetDataLocationDropdowns();

            SetOutputOverwriteCheckBoxes();

            SetRelationalDatabaseSourceTab();

            SetRelationalDatabaseDestTab();

            LoadProviderListDropdowns();

            SetAccessDatabaseFileSourceTab();

            SetAccessDatabaseFileDestTab();

            SetExcelDataFileSourceTab();

            SetExcelDataFileDestTab();

            SetDelimitedTextFileSourceTab();

            SetDelimitedTextFileDestTab();

            SetFixedLengthTextFileSourceTab();

            SetFixedLengthTextFileDestTab();

            SetXmlFileSourceTab();

            SetXmlFileDestTab();

            _randomOrdersDefinition = new PFRandomOrdersDefinition(); 

            _saveExtractorDefinition = CreateExtractorDefinition();

            if (this.InitExtractDefinitionToOpen.Trim().Length > 0)
            {
                LoadInitExtractDefinition();
            }

        }

        private void SetExtractorDefinitionsSaveFolder()
        {
            string configValue = string.Empty;

            configValue = Properties.Settings.Default.DefaultExtractorDefinitionsSaveFolder.Trim();
            if (configValue != string.Empty)
            {
                this.txtExtractorDefinitionsSaveFolder.Text = configValue;
                _defaultDataExtractorDefsFolder = configValue;
            }
            else
                this.txtExtractorDefinitionsSaveFolder.Text = _defaultDataExtractorDefsFolder;

            if (this.txtExtractorDefinitionsSaveFolder.Text.Trim() != string.Empty)
            {
                if (Directory.Exists(this.txtExtractorDefinitionsSaveFolder.Text) == false)
                    Directory.CreateDirectory(this.txtExtractorDefinitionsSaveFolder.Text);
            }

        }

        private void SetExtractorDefinitionName()
        {
            string configValue = string.Empty;

            configValue = Properties.Settings.Default.DefaultExtractorDefinitionName.Trim();
            if (configValue != string.Empty)
            {
                this.txtExtractorName.Text = configValue;
                _defaultDataExtractorName = configValue;
            }
            else
                this.txtExtractorName.Text = _defaultDataExtractorName;

        }

        private void SetMaxPreviewRows()
        {
            string configValue = string.Empty;

            if (Properties.Settings.Default.PreviewAllRows == true)
            {
                this.txtMaxPreviewRows.Text = string.Empty;
            }
            else
            {
                configValue = Properties.Settings.Default.MaxNumberOfRows.ToString();
                this.txtMaxPreviewRows.Text = configValue;
            }
        }//end method

        private void SetOutputOptions()
        {
            this.chkShowRowNumber.Checked = false;
            this.chkRandomizeOutput.Checked = false;
            this.chkFilterOutput.Checked = false;
        }

        private void SetSaveOutputOptions()
        {
            _saveExtractorOutputOptions = new PFExtractorOutputOptions[ExtractorDataTypeList.ExtractorDataLocations.Length];

            for (int i = 0; i < ExtractorDataTypeList.ExtractorDataLocations.Length; i++)
            {
                _saveExtractorOutputOptions[i] = new PFExtractorOutputOptions();
            }
        }

        private void                                           SetDataLocationDropdowns()
        {
            string configValue = string.Empty;

            this.cboSourceDataLocation.Items.Clear();
            this.cboDestDataLocation.Items.Clear();
            for (int i = 0; i < ExtractorDataTypeList.ExtractorDataLocations.Length; i++)
            {
                cboDestDataLocation.Items.Add(ExtractorDataTypeList.ExtractorDataLocations[i]);
                cboSourceDataLocation.Items.Add(ExtractorDataTypeList.ExtractorDataLocations[i]);
            }

            configValue = Properties.Settings.Default.DefaultDataDestinationType.Trim();
            if (configValue != string.Empty)
            {
                this.cboDestDataLocation.Text = configValue;
            }
            else
            {
                this.cboDestDataLocation.Text = this.cboDestDataLocation.Items[0].ToString();
            }

            configValue = Properties.Settings.Default.DefaultDataSourceType.Trim();
            if (configValue != string.Empty)
            {
                this.cboSourceDataLocation.Text = configValue;
            }
            else
            {
                this.cboSourceDataLocation.Text = this.cboSourceDataLocation.Items[0].ToString();
            }

        }

        private void LoadProviderListDropdowns()
        {
            bool showInstalledDatabaseProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;
            PFConnectionManager connMgr = new PFConnectionManager();
            PFKeyValueList<string, PFProviderDefinition> provDefs = connMgr.GetListOfProviderDefinitions();

            if (provDefs.Count == 0)
            {
                connMgr.CreateProviderDefinitions();
            }
            else
            {
                connMgr.UpdateAllProvidersInstallationStatus();
            }
            provDefs = connMgr.GetListOfProviderDefinitions();

            this.cboDataSource.Items.Clear();
            this.cboDataDestination.Items.Clear();
            foreach (stKeyValuePair<string, PFProviderDefinition> provDef in provDefs)
            {
                if (showInstalledDatabaseProvidersOnly)
                {
                    if (provDef.Value.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                    {
                        if (provDef.Value.AvailableForSelection)
                        {
                            this.cboDataSource.Items.Add(provDef.Key);
                            this.cboDataDestination.Items.Add(provDef.Key);
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
                    if (provDef.Value.AvailableForSelection)
                    {
                        this.cboDataSource.Items.Add(provDef.Key);
                        this.cboDataDestination.Items.Add(provDef.Key);
                    }
                    else
                    {
                        ;
                    }
                }
            }

        }//end method

        private void SetRelationalDatabaseSourceTab()
        {
            this.cboDataSource.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultInputDatabaseType;
            this.txtSourceConnectionString.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultInputDatabaseConnectionString;
            this.txtRelDbSqlQuery.Text = string.Empty;
            SetBatchSize();
        }

        private void SetRelationalDatabaseDestTab()
        {
            this.cboDataDestination.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultOutputDatabaseType;
            this.txtDestinationConnectionString.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultOutputDatabaseConnectionString;
            if (this.cboDataDestination.Text.Trim().Length > 0)
            {
                SetRelationalDatabaseDestinationTableName();
            }
            else
            {
                this.txtDestinationTableName.Text = string.Empty;
            }
            this.chkReplaceExistingDbTable.Checked = AppConfig.GetBooleanValueFromConfigFile("ReplaceOutputTableIfExists", "True");
            SetBatchSize();
        }

        private void SetRelationalDatabaseDestinationTableName()
        {
            string tabName = string.Empty;
            string tabSchema = string.Empty;
            tabName = AppConfig.GetStringValueFromConfigFile("DefaultOutputTableName", "ExtractorOutputTable");
            tabSchema = AppConfig.GetStringValueFromConfigFile("DefaultSchema_" + this.cboDataDestination.Text.Trim(), string.Empty);

            if (tabSchema.Trim().Length > 0 && tabName.Trim().Length > 0)
                this.txtDestinationTableName.Text = tabSchema.Trim() + "." + tabName.Trim();
            else if (tabName.Trim().Length > 0)
                this.txtDestinationTableName.Text = tabName.Trim();
            else
                this.txtDestinationTableName.Text = string.Empty;
        }

        private void SetAccessDatabaseFileSourceTab()
        {
            this.optSourceAccess2007.Checked = true;
        }

        private void SetAccessDatabaseFileDestTab()
        {
            this.optDestAccess2007.Checked = true;
        }

        private void SetExcelDataFileSourceTab()
        {
            this.optSourceExcel2007Format.Checked = true;
            this.optExcelNamedRange.Checked = true;
            this.chkSourceExcelColumnNamesInFirstRow.Checked = true;
        }

        private void SetExcelDataFileDestTab()
        {
            this.optDestExcel2007Format.Checked = true;
            this.chkDestExcelColumnNamesInFirstRow.Checked = true;
        }

        private void SetDelimitedTextFileSourceTab()
        {
            this.optSourceCommaDelimited.Checked = true;
            this.optSourceCrLf.Checked = true;
            this.chkSourceColumnNamesOnFirstLine.Checked = true;
            this.chkSourceStringValueQuotes.Checked = false;
        }

        private void SetDelimitedTextFileDestTab()
        {
            this.optDestCommaDelimited.Checked = true;
            this.optDestCrLf.Checked = true;
            this.chkDestIncludeColumnNamesOnFirstLineOfOutput.Checked = true;
            this.chkDestStringValueQuotes.Checked = false;
        }

        private void SetFixedLengthTextFileSourceTab()
        {
            this.chkSourceColumnNamesOnFirstLineOfFile.Checked = true;
            this.chkSourceLineTerminatorAppendedToEachLine.Checked = true;
            this.optSourceFixedLenCrLf.Checked = true;
            this.txtSourceFixedLengthExpectedLineWidth.Text = string.Empty;
        }

        private void SetFixedLengthTextFileDestTab()
        {
            this.chkDestColumnNamesOnFirstLineOfOutput.Checked = true;
            this.chkDestAllowDataTruncation.Checked = false;
            this.chkDestUseLineTerminator.Checked = true;
            this.optDestFixedLenCrLf.Checked = true;
            this.txtOutputLineLength.Text = string.Empty;
        }

        private void SetXmlFileSourceTab()
        {
            this.optSourceXmlNoSchema.Checked = true;
        }

        private void SetXmlFileDestTab()
        {
            this.chkReplaceExistingXmlFile.Checked = true;
            this.optDestXmlNoSchema.Checked = true;
            this.chkReplaceExistingXsdFile.Checked = true;
        }

        private void LoadInitExtractDefinition()
        {
            if (File.Exists(this.InitExtractDefinitionToOpen))
            {
                _saveSelectionsFile = this.InitExtractDefinitionToOpen;
                OpenSelectionsFile(this.InitExtractDefinitionToOpen);
            }
            else
            {
                string filepath = Path.Combine(_defaultDataExtractorDefsFolder, this.InitExtractDefinitionToOpen);
                if (File.Exists(filepath))
                {
                    _saveSelectionsFile = filepath;
                    OpenSelectionsFile(filepath);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find initial extract definition to load at ");
                    _msg.Append(this.InitExtractDefinitionToOpen);
                    Program._messageLog.WriteLine(_msg.ToString());
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                }
            }
        }

        private void SetBatchSize()
        {
            string configValue = string.Empty;

            configValue = Properties.Settings.Default.BatchSizeForDataImportsAndExports.ToString();
            if (configValue != string.Empty && PFTextProcessor.StringIsInt(configValue))
            {
                if (this.txtOutputBatchSize.Text != "1")
                    this.txtOutputBatchSize.Text = configValue;
            }
            else
            {
                if (this.txtOutputBatchSize.Text != "1")
                    this.txtOutputBatchSize.Text = "200";
            }
        }


        private void SetOutputOverwriteCheckBoxes()
        {
            if(pfDataExtractorCP.Properties.Settings.Default.OverwriteOutputDestinationIfAlreadyExists)
            {
                this.chkReplaceExistingDbTable.Checked = true;
                this.chkReplaceExistingAccessDatabaseFile.Checked = true;
                this.chkReplaceExistingAccessTable.Checked = true;
                this.chkReplaceExistingExcelFile.Checked = true;
                this.chkReplaceExcelSheet.Checked = true;
                this.chkReplaceExistingDelimitedTextFile.Checked = true;
                this.chkReplaceExistingFixedLengthTextFile.Checked = true;
            }
            else
            {
                this.chkReplaceExistingDbTable.Checked = false;
                this.chkReplaceExistingAccessDatabaseFile.Checked = false;
                this.chkReplaceExistingAccessTable.Checked = false;
                this.chkReplaceExistingExcelFile.Checked = false;
                this.chkReplaceExcelSheet.Checked = false;
                this.chkReplaceExistingDelimitedTextFile.Checked = false;
                this.chkReplaceExistingFixedLengthTextFile.Checked = false;
            }
        }

        private void InitAppProcessor()
        {
            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
            _appProcessor.MessageLogUI = Program._messageLog;
            _appProcessor.HelpFilePath = _dataExtractorHelpFilePath;
        }

        private void UpdateAppLogWindowVisibility()
        {
            bool ShowApplicationLogWindow = Properties.Settings.Default.ShowOutputLog;

            if (ShowApplicationLogWindow)
            {
                Program._messageLog.ShowWindow();
                this.chkEraseOutputLogBeforeEachTest.Visible = true;
            }
            else
            {
                Program._messageLog.HideWindow();
                this.chkEraseOutputLogBeforeEachTest.Visible = false;
            }
        }

        private void SetTextFont()
        {
            Font fnt = Properties.Settings.Default.DefaultApplicationFont;
            TabPage tp = null;

            TextFormatter txtFmt = new TextFormatter();
            if (fnt != null)
            {
                txtFmt.SetFormTextValuesFont(this, fnt);
                tp = this.tabSourceDb;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabSourceAccess;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabSourceExcel;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabSourceTextDelimited;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabSourceTextFixedLength;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabSourceXml;
                txtFmt.SetFormTextValuesFont(tp, fnt);

                tp = this.tabDestDb;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabDestAccess;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabDestExcel;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabDestTextDelimited;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabDestTextFixedLength;
                txtFmt.SetFormTextValuesFont(tp, fnt);
                tp = this.tabDestXml;
                txtFmt.SetFormTextValuesFont(tp, fnt);
            }
            
            //this.Font = fnt;    //changes all text, including literals

            this.Refresh();
            this.Focus();

        }

        private void SetFileDialogDelegates()
        {
            _showFileDialog[_openFileDialogIndex] = new ShowFileDialogDelegate(ShowOpenFileDialog);
            _showFileDialog[_saveFileDialogIndex] = new ShowFileDialogDelegate(ShowSaveFileDialog);
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
                case "Cut":
                    EditCut();
                    break;
                case "Copy":
                    EditCopy();
                    break;
                case "Paste":
                    EditPaste();
                    break;
                case "Help":
                    ShowHelpMainFormOverview();
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
            if (ExtractorDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileNew);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            InitExtractorDefinitionForm();
            _saveExtractorDefinition = CreateExtractorDefinition();
        }

        private void FileOpen()
        {
            if (ExtractorDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileOpen);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            _saveFilter = "Extractor Def Files|*.exdef|All Files|*.*";
            _saveSelectionsFolder = this.txtExtractorDefinitionsSaveFolder.Text;
            DialogResult res = ShowOpenFileDialog();
            if (res != System.Windows.Forms.DialogResult.OK)
            {
                //cancel save request
                return;
            }

            OpenSelectionsFile(_saveSelectionsFile);
        }

        private void OpenSelectionsFile(string selectionsFile)
        {
            PFExtractorDefinition extractorDef = PFExtractorDefinition.LoadFromXmlFile(selectionsFile);
            FillExtractorDefinitionForm(extractorDef);
            _saveExtractorDefinition = CreateExtractorDefinition();

            this.txtExtractorDefinitionsSaveFolder.Text = Path.GetDirectoryName(selectionsFile);
            if (this.txtExtractorDefinitionsSaveFolder.Text.EndsWith(@"\") == false)
            {
                this.txtExtractorDefinitionsSaveFolder.Text += @"\";
            }

            LoadExtractorSourceOutputOptions(this.cboSourceDataLocation.SelectedIndex);

            UpdateMruList(selectionsFile);
        }

        private void FileClose()
        {
            if (ExtractorDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileNew);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            InitExtractorDefinitionForm();
            _saveExtractorDefinition = CreateExtractorDefinition();
        }

        private void ReinitExtractorDefinitionForm()
        {
            InitExtractorDefinitionForm();
            _saveExtractorDefinition = CreateExtractorDefinition();
        }

        private void FileSave()
        {
            if (ExpectedFileSaveInfoFound() == false)
                return;

            string extractorDefFilename = Path.Combine(this.txtExtractorDefinitionsSaveFolder.Text, this.txtExtractorName.Text + _defaultExtractorDefinitionFileExtension);
            if (File.Exists(extractorDefFilename))
            {
                PFExtractorDefinition extractorDef = CreateExtractorDefinition();
                extractorDef.SaveToXmlFile(extractorDefFilename);
                _saveExtractorDefinition = CreateExtractorDefinition();
                _msg.Length = 0;
                _msg.Append("Extractor definition saved to ");
                _msg.Append(extractorDefFilename);
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayInfoMessage(_msg.ToString());
                UpdateMruList(extractorDefFilename);
            }
            else
            {
                FileSaveAs();
            }
        }

        private void FileSaveAs()
        {
            if (ExpectedFileSaveInfoFound() == false)
                return;

            _saveSelectionsFolder = this.txtExtractorDefinitionsSaveFolder.Text;
            _saveSelectionsFile = this.txtExtractorName.Text + _defaultExtractorDefinitionFileExtension;
            _saveFilter = "Extractor Def Files|*.exdef|All Files|*.*";
            DialogResult res = ShowSaveFileDialog();
            if (res != DialogResult.OK)
            {
                //cancel save request
                return;
            }

            this.txtExtractorDefinitionsSaveFolder.Text = Path.GetDirectoryName(_saveSelectionsFile);
            if (this.txtExtractorDefinitionsSaveFolder.Text.EndsWith(@"\") == false)
            {
                this.txtExtractorDefinitionsSaveFolder.Text += @"\";
            }
            this.txtExtractorName.Text = Path.GetFileNameWithoutExtension(_saveSelectionsFile);
            PFExtractorDefinition extractorDef = CreateExtractorDefinition();
            extractorDef.SaveToXmlFile(_saveSelectionsFile);
            _saveExtractorDefinition = CreateExtractorDefinition();
            _msg.Length = 0;
            _msg.Append("Extractor definition saved to ");
            _msg.Append(_saveSelectionsFile);
            Program._messageLog.WriteLine(_msg.ToString());

            UpdateMruList(_saveSelectionsFile);

        }

        private bool ExpectedFileSaveInfoFound()
        {
            bool infoFound = true;

            _msg.Length = 0;
            if (Directory.Exists(this.txtExtractorDefinitionsSaveFolder.Text) == false)
            {
                _msg.Append("Directory you specified does not exist: ");
                _msg.Append(this.txtExtractorDefinitionsSaveFolder.Text);
                _msg.Append(Environment.NewLine);
            }
            if (this.txtExtractorName.Text.Trim().Length == 0)
            {
                _msg.Append("You must specify a name for the extractor definition.");
                _msg.Append(Environment.NewLine);
            }

            if (this.cboSourceDataLocation.Text.Trim().Length == 0)
            {
                _msg.Append("You must specify a source location for the input data.");
                _msg.Append(Environment.NewLine);
            }

            if (this.cboDestDataLocation.Text.Trim().Length == 0)
            {
                _msg.Append("You must specify a destination location for the output data.");
                _msg.Append(Environment.NewLine);
            }

            if (_msg.ToString().Length > 0)
            {
                infoFound = false;
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }

            return infoFound;
        }



        private DialogResult ShowOpenFileDialog()
        {
            DialogResult res = DialogResult.None;
            mainMenuOpenFileDialog.InitialDirectory =  _saveSelectionsFolder;
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
            mainMenuSaveFileDialog.FileName = _saveSelectionsFile;
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
                Program._messageLog.WriteLine(_msg.ToString());
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
            _printer.PageSubTitle = "Main Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);
        }


        private void OnMruFile(int number, String filename)
        {
            if (ExtractorDefinitionHasChanges())
            {
                DialogResult result = PromptForFileSave(ReasonForFileSavePrompt.FileOpen);
                if (result == DialogResult.Yes)
                    FileSave();
                if (result == DialogResult.Cancel)
                    return;
            }

            if (File.Exists(filename))
            {
                //process it
                _saveSelectionsFile = filename;

                OpenSelectionsFile(filename);

                //PFExtractorDefinition extractorDef = PFExtractorDefinition.LoadFromXmlFile(filename);
                //FillExtractorDefinitionForm(extractorDef);
                //_saveExtractorDefinition = CreateExtractorDefinition();

                //this.txtExtractorDefinitionsSaveFolder.Text = Path.GetDirectoryName(_saveSelectionsFile);
                //if (this.txtExtractorDefinitionsSaveFolder.Text.EndsWith(@"\") == false)
                //{
                //    this.txtExtractorDefinitionsSaveFolder.Text += @"\";
                //}

                //LoadExtractorSourceOutputOptions(this.cboSourceDataLocation.SelectedIndex);

                //UpdateMruList(_saveSelectionsFile);
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("File does not exist. It will be removed from the Mru List");
                _msm.RemoveFile(number);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }


        }//end method

        private void EditCut()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Cut);
        }

        private void EditCopy()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Copy);
        }

        private void EditPaste()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Paste);
        }

        private void EditSelectAll()
        {
            _textEditor.TextBoxEditor(this, EditOperation.SelectAll);
        }

        private void EditDelete()
        {
            _textEditor.TextBoxEditor(this, EditOperation.Delete);
        }


        //application routines

        private void RunExtract()
        {
            PFExtractorDefinition exdef = null;

            if (this.chkEraseOutputLogBeforeEachTest.Checked)
                Program._messageLog.Clear();

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                exdef = CreateExtractorDefinition();

                if (this.chkEraseOutputLogBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }


                _appProcessor.RunExtract(exdef,
                                         AppTextGlobals.ConvertStringToInt(this.txtMaxPreviewRows.Text, -1),
                                         this.chkShowRowNumber.Checked,
                                         this.chkFilterOutput.Checked,
                                         this.chkRandomizeOutput.Checked,
                                         Properties.Settings.Default.BatchSizeForRandomDataGeneration,
                                         Properties.Settings.Default.BatchSizeForDataImportsAndExports
                                        );


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
                this.Focus();
            }

        }


        private void SaveScreenLocations()
        {
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            if (Program._messageLog.Form.WindowState == FormWindowState.Minimized || Program._messageLog.Form.WindowState == FormWindowState.Maximized)
            {
                Program._messageLog.Form.WindowState = FormWindowState.Normal;
            }
            Properties.Settings.Default["MessageLogX"] = Program._messageLog.Form.Location.X;
            Properties.Settings.Default["MessageLogY"] = Program._messageLog.Form.Location.Y;
            Properties.Settings.Default["MessageLogWidth"] = Program._messageLog.Form.Width;
            Properties.Settings.Default["MessageLogHeight"] = Program._messageLog.Form.Height;

            Properties.Settings.Default["MainFormX"] = this.Location.X;
            Properties.Settings.Default["MainFormY"] = this.Location.Y;
            Properties.Settings.Default.Save();

        }

        private void RestoreDefaultScreenLocations()
        {
            Program._messageLog.Form.Location = new Point(0, 0);
            Program._messageLog.Form.Width = Properties.Settings.Default.MessageLogDefaultWidth;
            Program._messageLog.Form.Height = Properties.Settings.Default.MessageLogDefaultHeight;
            Program._messageLog.Form.Refresh();

            Properties.Settings.Default["MessageLogX"] = Program._messageLog.Form.Location.X;
            Properties.Settings.Default["MessageLogY"] = Program._messageLog.Form.Location.Y;
            Properties.Settings.Default["MessageLogWidth"] = Program._messageLog.Form.Width;
            Properties.Settings.Default["MessageLogHeight"] = Program._messageLog.Form.Height;

            this.CenterToScreen();
            Properties.Settings.Default["MainFormX"] = 0;
            Properties.Settings.Default["MainFormY"] = 0;
            Properties.Settings.Default.Save();
        }

        private void DataLocationSelectionChanged(bool sourceDataLocationChanged)
        {
            if (sourceDataLocationChanged)
            {
                if (_prevSourceDataLocationIndex != -1)
                {
                    SaveExtractorSourceOutputOptions(_prevSourceDataLocationIndex);
                }
            }
            else
            {
                //destination location changed
                ResetTableSchema();
            }

            this.tabSourcesAndDestinations.TabPages.Clear();

            switch (this.cboSourceDataLocation.Text)
            {
                case "Relational Database":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabSourceDb);
                    break;
                case "Access Database File":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabSourceAccess);
                    break;
                case "Excel Data File":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabSourceExcel);
                    break;
                case "Text File (Delimited)":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabSourceTextDelimited);
                    break;
                case "Text File (Fixed Length)":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabSourceTextFixedLength);
                    break;
                case "XML File":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabSourceXml);
                    break;
                default:
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabSourceDb);
                    if (sourceDataLocationChanged)
                        this.tabSourcesAndDestinations.SelectedTab = this.tabSourceDb;
                    break;
            }

            switch (this.cboDestDataLocation.Text)
            {
                case "Relational Database":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabDestDb);
                    break;
                case "Access Database File":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabDestAccess);
                    break;
                case "Excel Data File":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabDestExcel);
                    break;
                case "Text File (Delimited)":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabDestTextDelimited);
                    break;
                case "Text File (Fixed Length)":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabDestTextFixedLength);
                    break;
                case "XML File":
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabDestXml);
                    break;
                default:
                    this.tabSourcesAndDestinations.TabPages.Add(this.tabDestDb);
                    break;
            }

            if (sourceDataLocationChanged)
            {
                _prevSourceDataLocationIndex = this.cboSourceDataLocation.SelectedIndex;
                LoadExtractorSourceOutputOptions(this.cboSourceDataLocation.SelectedIndex);
            }

            ResetSelectedTab(sourceDataLocationChanged);

            //foreach (TabPage tp in this.tabSourcesAndDestinations.TabPages)
            //{
            //    if ((tp.Text == "Form" && sourceDataLocationChanged)
            //        || (tp.Text == "To" && sourceDataLocationChanged == false))
            //    {
            //        this.tabSourcesAndDestinations.SelectedTab = tp;
            //        break;
            //    }
 
            //}
        
        }

        private void ResetTableSchema()
        {
            //erase any schema name defined for the test random orders output: this will force
            //  the test orders routine to requery config.sys for a default schema
            _randomOrdersDefinition.TableSchema = string.Empty;
        }

        private void ResetSelectedTab(bool sourceDataLocationChanged)
        {
            foreach (TabPage tp in this.tabSourcesAndDestinations.TabPages)
            {
                if ((tp.Text == "From" && sourceDataLocationChanged)
                    || (tp.Text == "To" && sourceDataLocationChanged == false))
                {
                    this.tabSourcesAndDestinations.SelectedTab = tp;
                    break;
                }

            }
        }

        private void SelectedTabChanged()
        {
            TabPage tp = this.tabSourcesAndDestinations.SelectedTab;
            //if (tp != null)
            //{
            //    if (tp.Text == "To")
            //        this.panelOutputOptions.Visible = true;
            //    else
            //        this.panelOutputOptions.Visible = false;
            //}
            //else
            //{
            //    this.panelOutputOptions.Visible = false;  //assume execution has started and default display of source relation database form is occurring
            //}

        }

        private void SaveExtractorSourceOutputOptions(int sourceDataLocationIndex)
        {

            _saveExtractorOutputOptions[sourceDataLocationIndex].AddRowNumberToOutput = this.chkShowRowNumber.Checked;
            _saveExtractorOutputOptions[sourceDataLocationIndex].RandomizeOutput = this.chkRandomizeOutput.Checked;
            _saveExtractorOutputOptions[sourceDataLocationIndex].FilterOutput = this.chkFilterOutput.Checked;
            //RandomizerColSpecs and OutputFilter save values set when Randomizer and SetFilter buttons pressed
            //_saveExtractorOutputOptions[sourceDataLocationIndex].RandomizerColSpecs 
            //_saveExtractorOutputOptions[sourceDataLocationIndex].OutputFilter
        }

        private void LoadExtractorSourceOutputOptions(int sourceDataLocationIndex)
        {

            this.chkShowRowNumber.Checked = _saveExtractorOutputOptions[sourceDataLocationIndex].AddRowNumberToOutput;
            this.chkRandomizeOutput.Checked = _saveExtractorOutputOptions[sourceDataLocationIndex].RandomizeOutput;
            this.chkFilterOutput.Checked = _saveExtractorOutputOptions[sourceDataLocationIndex].FilterOutput;
            //RandomizerColSpecs and OutputFilter used when Randomizer and SetFilter buttons pressed
            //_saveExtractorOutputOptions[sourceDataLocationIndex].RandomizerColSpecs 
            //_saveExtractorOutputOptions[sourceDataLocationIndex].OutputFilter
        }

        private void BrowseForExtractorDefsFolder()
        {
            _saveSelectionsFolder = this.txtExtractorDefinitionsSaveFolder.Text;
            DialogResult res = ShowFolderBrowserDialog();
            if (res == DialogResult.OK)
            {
                this.txtExtractorDefinitionsSaveFolder.Text = _saveSelectionsFolder;
            }
        }

        private void NamedRangeCheckedChanged()
        {
            if (this.optExcelNamedRange.Checked)
            {
                this.panelExcelNamedRange.Enabled = true;
                this.panelExcelRowCol.Enabled = false;
            }
            else
            {
                //this.optExcelRowColFormat.Checked
                this.panelExcelNamedRange.Enabled = false;
                this.panelExcelRowCol.Enabled = true;
            }
        }

        private void SourceDelimiterChanged()
        {
            if (this.optSourceCommaDelimited.Checked || this.optSourceTabDelimited.Checked)
            {
                this.txtSourceOtherSeparator.Enabled = false;
            }
            else
            {
                //this.optSourceOtherDelimiter.checked
                this.txtSourceOtherSeparator.Enabled = true;
            }
        }

        private void DestDelimiterChanged()
        {
            if (this.optDestCommaDelimited.Checked || this.optDestTabDelimited.Checked)
            {
                this.txtDestOtherSeparator.Enabled = false;
            }
            else
            {
                //this.optDestOtherDelimiter.checked
                this.txtDestOtherSeparator.Enabled = true;
            }
        }

        private void DestLineTerminatorChanged()
        {
            if (this.optDestCrLf.Checked)
            {
                this.txtDestOtherLineTerminator.Enabled = false;
            }
            else
            {
                //this.optDestOtherLineTerminator.checked
                this.txtDestOtherLineTerminator.Enabled = true;
            }
        }

        private void SourceLineTerminatorAppendedToEachLineCheckedChanged()
        {
            if (this.chkSourceLineTerminatorAppendedToEachLine.Checked)
            {
                this.panelSourceFixedLenLineTerminatorChars.Enabled = true;
            }
            else
            {
                this.panelSourceFixedLenLineTerminatorChars.Enabled = false;
            }
        }

        private void DestUseLineTerminatorCheckedChanged()
        {
            if (this.chkDestUseLineTerminator.Checked)
            {
                this.panelDestFixedLenLineTerminatorChars.Enabled = true;
            }
            else
            {
                this.panelDestFixedLenLineTerminatorChars.Enabled = false;
            }
        }

        private void DestFixedLenCrLfCheckedChanged()
        {
            if (this.optDestFixedLenCrLf.Checked)
            {
                this.txtDestFixedLenOtherLineTerminator.Enabled = false;
            }
            else
            {
                this.txtDestFixedLenOtherLineTerminator.Enabled = true;
            }
        }

        private void DataSourceSelectedIndexChanged()
        {
            if (cboDataSource.Text != _saveExtractorDefinition.RelationalDatabaseSource.DbPlatform.ToString())
            {
                if (_saveExtractorDefinition.RelationalDatabaseSource.DbPlatform != DatabasePlatform.Unknown)
                {
                    DialogResult res = OverwriteCurrentExtractorDefinition();
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {

                        try
                        {
                            this.cboDataSource.SelectedIndexChanged -= cboDataSource_SelectedIndexChanged;
                            string newDataSource = this.cboDataSource.Text;
                            this.cboDataSource.Text = _saveExtractorDefinition.RelationalDatabaseSource.DbPlatform.ToString();

                            if (ExtractorDefinitionHasChanges())
                            {
                                DialogResult result = PromptForFileSaveYesNo(ReasonForFileSavePrompt.DataSourceChange);
                                if (result == DialogResult.Yes)
                                    FileSave();
                            }

                            ReinitExtractorDefinitionForm();

                            this.cboDataSource.Text = newDataSource;
                            GetDefaultConnectionStringForDatabaseType(this.cboDataSource, this.txtSourceConnectionString);
                            this.txtRelDbSqlQuery.Text = string.Empty;

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
                            this.cboDataSource.SelectedIndexChanged += cboDataSource_SelectedIndexChanged;
                        }


                    }
                    else
                    {
                        this.cboDataSource.Text = _saveExtractorDefinition.RelationalDatabaseSource.DbPlatform.ToString();
                    }
                }
                else
                {
                    //_saveExtractorDefinition.RelationalDatabaseSource.DbPlatform == DatabasePlatform.Unknown
                    GetDefaultConnectionStringForDatabaseType(this.cboDataSource, this.txtSourceConnectionString);
                    this.txtRelDbSqlQuery.Text = string.Empty;
                }
            }
        }

        private void GetDefaultConnectionStringForDatabaseType(ComboBox cboDatabaseType, TextBox txtConnectionString)
        {
            if (cboDatabaseType.Text.Trim() != string.Empty)
            {
                txtConnectionString.Text = AppConfig.GetStringValueFromConfigFile("DefaultConnection_" + cboDatabaseType.Text, string.Empty);

            }
        }

        private void GetDefaultConnectionStringForDatabaseType(ComboBox cboDatabaseType, TextBox txtConnectionString, TextBox txtBatchSize)
        {
            int batchSize = 200;

            if (cboDatabaseType.Text.Trim() != string.Empty)
            {
                txtConnectionString.Text = AppConfig.GetStringValueFromConfigFile("DefaultConnection_" + cboDatabaseType.Text, string.Empty);

                string configValue = "DefaultOutputBatchSize_" + cboDatabaseType.Text;
                batchSize = AppConfig.GetIntValueFromConfigFile(configValue, 200);
            }
            else
            {
                batchSize = pfDataExtractorCP.Properties.Settings.Default.BatchSizeForDataImportsAndExports;
            }
            if (batchSize == 1)
            {
                txtBatchSize.Text = "1";
            }
            else
            {
                txtBatchSize.Text = pfDataExtractorCP.Properties.Settings.Default.BatchSizeForDataImportsAndExports.ToString();
            }
        }

        private void DefineConnectionString(ComboBox dataSource, TextBox connectionString, bool isDatabaseSource)
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ConnectionStringPrompt cp = null;


            try
            {
                if (dataSource.Text.Trim().Length > 0)
                {
                    dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), dataSource.Text);
                    connMgr = new PFConnectionManager();
                    cp = new ConnectionStringPrompt(dbPlat, connMgr);
                    //cp.ConnectionString = connectionString.Text;
                    if (connectionString.Text.Trim().Length > 0)
                    {
                        cp.ConnectionString = connectionString.Text;
                    }
                    else
                    {
                        if (isDatabaseSource)
                        {
                            if (dbPlat == DatabasePlatform.SQLServerCE35)
                            {
                                cp.ConnectionString = @"data source='" + _defaultSampleDatabaseFolder + @"SampleOrderDataCE35.sdf';";
                            }
                            else if (dbPlat == DatabasePlatform.SQLServerCE40)
                            {
                                cp.ConnectionString = @"data source='" + _defaultSampleDatabaseFolder + @"SampleOrderDataCE40.sdf';";
                            }
                            else if (dbPlat == DatabasePlatform.MSAccess)
                            {
                                cp.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _defaultSampleDatabaseFolder + @"SampleOrderData.mdb;User Id=admin;Password=;Jet OLEDB:Engine Type=5;";
                            }
                            else if (dbPlat == DatabasePlatform.SQLAnywhere)
                            {
                                cp.ConnectionString = @"UserID=DBA;Password=sql;DatabaseName=SampleOrderData;DatabaseFile=" + _defaultSampleDatabaseFolder + @"SampleOrderData.db;";
                            }
                            else if (dbPlat == DatabasePlatform.SQLAnywhereUltraLite)
                            {
                                cp.ConnectionString = @"nt_file=" + _defaultSampleDatabaseFolder + @"SampleOrderData.udb;uid=DBA;pwd=sql";
                            }
                            else
                                cp.ConnectionString = string.Empty;
                        }
                        else //is data destination
                        {
                            if (dbPlat == DatabasePlatform.SQLServerCE35)
                            {
                                cp.ConnectionString = @"data source='" + _defaultCEDestinationDataFolder + @"ExtractDataCE35.sdf';";
                            }
                            else if (dbPlat == DatabasePlatform.SQLServerCE40)
                            {
                                cp.ConnectionString = @"data source='" + _defaultCEDestinationDataFolder + @"ExtractDataCE40.sdf';";
                            }
                            else if (dbPlat == DatabasePlatform.MSAccess)
                            {
                                cp.ConnectionString = @"data source='" + _defaultAccessDestinationDataFolder + @"ExtractData.mdb';";
                            }
                            else if (dbPlat == DatabasePlatform.SQLAnywhere)
                            {
                                cp.ConnectionString = @"data source='" + _defaultSQLAnywhereDestinationDataFolder + @"ExtractData.db';";
                            }
                            else if (dbPlat == DatabasePlatform.SQLAnywhereUltraLite)
                            {
                                cp.ConnectionString = @"data source='" + _defaultSQLAnywhereDestinationDataFolder + @"ExtractData.udb';";
                            }
                            else
                                cp.ConnectionString = string.Empty;
                        }
                    }


                    System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
                    if (res == DialogResult.OK)
                    {
                        connectionString.Text = cp.ConnectionString;
                    }
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a database type for the data source.");
                    AppMessages.DisplayWarningMessage(_msg.ToString());
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
                this.Focus();
            }

        }

        public void RunQueryBuilder(ComboBox dataSource, TextBox connectionString, TextBox sqlQuery)
        {
            string modifiedQueryText = sqlQuery.Text;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ISQLBuilder qbf = null;



            try
            {
                if (connectionString.Text.Trim().Length == 0
                    || dataSource.Text.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a Data Source and a connection string in order to use the Query Builder.");
                    throw new System.Exception(_msg.ToString());
                }

                // *** TEMPORARY CODE FOR QUERY BUILDER ***
                _msg.Length = 0;
                _msg.Append("*** Query builder functionality has been disabled. ***");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("You must have installed Active Query Builder for Winforms for this functionality to work.\r\nProduct can be obtained from the following link:");
                _msg.Append(Environment.NewLine);
                _msg.Append(@"http://www.activequerybuilder.com/product_net.html");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("If you have already installed Active Query Builder for Winforms, you should remove the temporary code found in the RunQueryBuilder() method.");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayAlertMessage(_msg.ToString());
                return;
                // *** END TEMPORARY CODE FOR QUERY BUILDER ***



                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), dataSource.Text);

                // ******************************************************************************
                // Code to activate Query Builder follows
                // ******************************************************************************
                //uncomment next four lines to activate query builder
                //uncomment using PFSQLBuilderObjects; above
                //add reference to PFSQLBuilderObjects.dll in CPApps\Binaries\ProFast\ClassLibraries\Release (this is default path; substitute path if you changed binaries path
                /*
                qbf = SQLBuilder.CreateQueryBuilderObject(dbPlat, connectionString.Text);

                modifiedQueryText = qbf.ShowQueryBuilder(sqlQuery.Text);
                 * */
                // ******************************************************************************
                // End code to activate Query Builder
                // ******************************************************************************

                sqlQuery.Text = modifiedQueryText;

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
                if (qbf != null)
                    qbf = null;
                this.Focus();
            }

        }

        private void RunQueryBuilderForAccess()
        {
            string modifiedQueryText = this.txtAccessSQLQuery.Text;
            DatabasePlatform dbPlat = DatabasePlatform.MSAccess;
            ISQLBuilder qbf = null;
            string connectionString = string.Empty;
            string sqlQuery = this.txtAccessSQLQuery.Text;
            PFMsAccess db = new PFMsAccess();


            try
            {
                db.DatabasePath = this.txtSourceAccessDatabaseFilePath.Text;
                db.DatabaseUsername = this.txtSourceAccessUsername.Text;
                db.DatabasePassword = this.txtSourceAccessPassword.Text;
                if (this.optSourceAccess2003.Checked)
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                else
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;

                connectionString = db.ConnectionString;


                if (connectionString.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify a Access database file path in order to use the Query Builder.");
                    throw new System.Exception(_msg.ToString());
                }

                // *** TEMPORARY CODE FOR QUERY BUILDER ***
                _msg.Length = 0;
                _msg.Append("*** Query builder functionality has been disabled. ***");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("You must have installed Active Query Builder for Winforms for this functionality to work.\r\nProduct can be obtained from the following link:");
                _msg.Append(Environment.NewLine);
                _msg.Append(@"http://www.activequerybuilder.com/product_net.html");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("If you have already installed Active Query Builder for Winforms, you should remove the temporary code found in the RunQueryBuilder() method.");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayAlertMessage(_msg.ToString());
                return;
                // *** END TEMPORARY CODE FOR QUERY BUILDER ***


                // ******************************************************************************
                // Code to activate Query Builder follows
                // ******************************************************************************
                //uncomment next four lines to activate query builder
                //uncomment using PFSQLBuilderObjects; above
                //add reference to PFSQLBuilderObjects.dll in CPApps\Binaries\ProFast\ClassLibraries\Release (this is default path; substitute path if you changed binaries path
                /*
                qbf = SQLBuilder.CreateQueryBuilderObject(dbPlat, connectionString);

                modifiedQueryText = qbf.ShowQueryBuilder(sqlQuery);
                */
                // ******************************************************************************
                // End code to activate Query Builder
                // ******************************************************************************

                this.txtAccessSQLQuery.Text = modifiedQueryText;

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
                if (qbf != null)
                    qbf = null;
                this.Focus();
            }

        }

        private void SelectDatabaseProvidersToUse()
        {
            ProviderSelectorForm provForm = new ProviderSelectorForm();


            try
            {
                provForm.ShowInstalledProvidersOnly = Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly;

                DialogResult res = provForm.ShowDialog();
                //DialogResult will always be OK
                if (provForm.ShowInstalledProvidersOnly != Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly
                    || provForm.ProviderListHasBeenChanged)
                {
                    Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly = provForm.ShowInstalledProvidersOnly;
                    LoadProviderListDropdowns();
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
                if (provForm.Visible)
                    provForm.Close();
                provForm = null;
            }


        }

        private void ShowToolsConnectionStringManager()
        {
            _appProcessor.ShowConnectionStringManagerForm();
            this.Focus();
            this.Refresh();
        }


        private void GetFilePath(int fileDialogIndex, TextBox txtCurrPath, string defaultPath, string filter)
        {
            string currFolder = string.Empty;
            string currFile = string.Empty;
            if (txtCurrPath.Text.Trim().Length > 0)
            {
                currFolder = Path.GetDirectoryName(txtCurrPath.Text.Trim());
                currFile = Path.GetFileName(txtCurrPath.Text.Trim());
            }
            else
            {
                currFolder = string.Empty;
                currFile = string.Empty;
            }

            if (currFolder.Trim().Length == 0)
            {
                if (defaultPath != string.Empty)
                {
                    _saveSelectionsFolder = defaultPath;
                    _saveSelectionsFile = this.txtExtractorName.Text;
                }
                else
                {
                    _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    _saveSelectionsFile = this.txtExtractorName.Text;
                }
            }
            else
            {
                //there already is a file path in the text box
                _saveSelectionsFolder = currFolder;
                _saveSelectionsFile = currFile;
            }

            _saveFilter = filter;
            _saveFilterIndex = 1;

            //DialogResult res = ShowOpenFileDialog();
            DialogResult res = _showFileDialog[fileDialogIndex]();
            if (res != System.Windows.Forms.DialogResult.OK)
            {
                //cancel open request
                return;
            }

            txtCurrPath.Text = _saveSelectionsFile;
        }

        private void SourceGetAccessDatabaseFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceAccessDatabaseFolder;
            string saveCurrentAccessFilePath = this.txtSourceAccessDatabaseFilePath.Text;
            bool saveSourceAccess2007Checked = this.optSourceAccess2007.Checked;
            string filter = string.Empty;
            if (this.optSourceAccess2007.Checked)
                filter = "Access 2007 Files|*.accdb|All Files|*.*";
            else
                filter = "Access 2003 Files|*.mdb|All Files|*.*";

            GetFilePath(_openFileDialogIndex, this.txtSourceAccessDatabaseFilePath, defaultFolder, filter);

            if (this.txtSourceAccessDatabaseFilePath.Text != saveCurrentAccessFilePath)
            {
                if (saveCurrentAccessFilePath.Trim() != string.Empty)
                {
                    DialogResult res = OverwriteCurrentExtractorDefinition();
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        string newPath = this.txtSourceAccessDatabaseFilePath.Text;
                        bool newAcc2007 = this.optSourceAccess2007.Checked;
                        this.txtSourceAccessDatabaseFilePath.Text = saveCurrentAccessFilePath;
                        if (ExtractorDefinitionHasChanges())
                        {
                            DialogResult result = PromptForFileSaveYesNo(ReasonForFileSavePrompt.DataSourceChange);
                            if (result == DialogResult.Yes)
                                FileSave();
                        }
                        ReinitExtractorDefinitionForm();

                        this.txtExtractorName.Text = _defaultDataExtractorName;
                        this.cboSourceDataLocation.Text = ExtractorDataTypeList.ExtractorDataLocations[ExtractorDataTypeList.AccessDatabaseFileListIndex];
                        this.txtSourceAccessDatabaseFilePath.Text = newPath;
                        this.optSourceAccess2007.Checked = newAcc2007;
                    }
                    else
                    {
                        //user does not want to overwrite current extractor definition
                        this.txtSourceAccessDatabaseFilePath.Text = saveCurrentAccessFilePath;
                        this.optSourceAccess2007.Checked = saveSourceAccess2007Checked;
                    }
                }
                else
                {
                    //previous database path was blank
                    ;
                }
            }
            else
            {
                //no change in the database path
                ;
            }

            //if (this.txtSourceAccessDatabaseFilePath.Text != saveCurrentAccessFilePath)
            //{
            //    string newPath = this.txtSourceAccessDatabaseFilePath.Text;
            //    bool newAcc2007 = this.optSourceAccess2007.Checked;
            //    InitMsAccessSource();
            //    this.txtSourceAccessDatabaseFilePath.Text = newPath;
            //    this.optSourceAccess2007.Checked = newAcc2007;
            //}
        }

        private void SourceGetExcelFile()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceExcelDataFileFolder;
            string saveCurrentExcelFilePath = this.txtSourceExcelFilePath.Text;
            enExcelOutputFormat saveExcelFormat = this.optSourceExcel2007Format.Checked ? enExcelOutputFormat.Excel2007 : this.optSourceExcel2003Format.Checked ? enExcelOutputFormat.Excel2003 : enExcelOutputFormat.CSV;
            string filter = string.Empty;
            if (this.optSourceExcel2007Format.Checked)
                filter = "Excel 2007 Files|*.xlsx|All Files|*.*";
            else if (this.optSourceExcel2003Format.Checked)
                filter = "Excel 2003 Files|*.xls|All Files|*.*";
            else
                filter = "Excel CSV Files|*.csv|All Files|*.*";

            GetFilePath(_openFileDialogIndex, this.txtSourceExcelFilePath, defaultFolder, filter);

            if (this.txtSourceExcelFilePath.Text != saveCurrentExcelFilePath)
            {
                if (saveCurrentExcelFilePath.Trim() != string.Empty)
                {
                    DialogResult res = OverwriteCurrentExtractorDefinition();
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        string newPath = this.txtSourceExcelFilePath.Text;
                        bool newExcel2007Format = this.optSourceExcel2007Format.Checked;
                        this.txtSourceExcelFilePath.Text = saveCurrentExcelFilePath;
                        if (ExtractorDefinitionHasChanges())
                        {
                            DialogResult result = PromptForFileSaveYesNo(ReasonForFileSavePrompt.DataSourceChange);
                            if (result == DialogResult.Yes)
                                FileSave();
                        }
                        ReinitExtractorDefinitionForm();

                        this.txtExtractorName.Text = _defaultDataExtractorName;
                        this.cboSourceDataLocation.Text = ExtractorDataTypeList.ExtractorDataLocations[ExtractorDataTypeList.ExcelDataFileListIndex];
                        this.txtSourceExcelFilePath.Text = newPath;
                        if (saveExcelFormat == enExcelOutputFormat.Excel2007)
                            this.optSourceExcel2007Format.Checked = true;
                        else if (saveExcelFormat == enExcelOutputFormat.Excel2003)
                            this.optSourceExcel2003Format.Checked = true;
                        else
                            this.optSourceExcelCSVFormat.Checked = true;
                    }
                    else
                    {
                        //user does not want to overwrite current extractor definition
                        this.txtSourceExcelFilePath.Text = saveCurrentExcelFilePath;
                        if(saveExcelFormat == enExcelOutputFormat.Excel2007)
                            this.optSourceExcel2007Format.Checked = true;
                        else if (saveExcelFormat == enExcelOutputFormat.Excel2003)
                            this.optSourceExcel2003Format.Checked = true;
                        else
                            this.optSourceExcelCSVFormat.Checked = true;
                    }
                }
                else
                {
                    //previous database path was blank
                    ;
                }
            }
            else
            {
                //no change in the database path
                ;
            }

        }

        private void SourceGetDelimitedTextFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceDelimitedTextFileFolder;
            string saveCurrentDelimitedTextFilePath = this.txtSourceDelimitedTextFilePath.Text;
            string filter = string.Empty;
            if (this.optSourceCommaDelimited.Checked)
                filter = "Comma Delimited Text Files|*.txt;*.csv|All Files|*.*";
            else if (this.optSourceTabDelimited.Checked)
                filter = "Tab Delimited Text Files|*.tab;*.txt|All Files|*.*";
            else
                filter = "Delimited Text Files|*.txt;*.csv;*.tab;*.dat|All Files|*.*";

            GetFilePath(_openFileDialogIndex, this.txtSourceDelimitedTextFilePath, defaultFolder, filter);

            if (this.txtSourceDelimitedTextFilePath.Text != saveCurrentDelimitedTextFilePath)
            {
                if (saveCurrentDelimitedTextFilePath.Trim() != string.Empty)
                {
                    DialogResult res = OverwriteCurrentExtractorDefinition();
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        string newPath = this.txtSourceDelimitedTextFilePath.Text;
                        this.txtSourceDelimitedTextFilePath.Text = saveCurrentDelimitedTextFilePath;
                        if (ExtractorDefinitionHasChanges())
                        {
                            DialogResult result = PromptForFileSaveYesNo(ReasonForFileSavePrompt.DataSourceChange);
                            if (result == DialogResult.Yes)
                                FileSave();
                        }
                        ReinitExtractorDefinitionForm();

                        this.txtExtractorName.Text = _defaultDataExtractorName;
                        this.cboSourceDataLocation.Text = ExtractorDataTypeList.ExtractorDataLocations[ExtractorDataTypeList.DelimitedTextFileListIndex];
                        this.txtSourceDelimitedTextFilePath.Text = newPath;
                    }
                    else
                    {
                        //user does not want to overwrite current extractor definition
                        this.txtSourceDelimitedTextFilePath.Text = saveCurrentDelimitedTextFilePath;
                    }
                }
                else
                {
                    //previous database path was blank
                    ;
                }
            }
            else
            {
                //no change in the database path
                ;
            }

        }

        private void SourceGetFixedLengthTextFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceFixedLengthTextFileFolder;
            string saveCurrentFixedLengthTextFilePath = this.txtSourceFixedLengthTextFilePath.Text;
            string filter = "Fixed Length Text Files|*.txt;*.dat|All Files|*.*";

            GetFilePath(_openFileDialogIndex, this.txtSourceFixedLengthTextFilePath, defaultFolder, filter);

            if (this.txtSourceFixedLengthTextFilePath.Text != saveCurrentFixedLengthTextFilePath)
            {
                if (saveCurrentFixedLengthTextFilePath.Trim() != string.Empty)
                {
                    DialogResult res = OverwriteCurrentExtractorDefinition();
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        string newPath = this.txtSourceFixedLengthTextFilePath.Text;
                        this.txtSourceFixedLengthTextFilePath.Text = saveCurrentFixedLengthTextFilePath;
                        if (ExtractorDefinitionHasChanges())
                        {
                            DialogResult result = PromptForFileSaveYesNo(ReasonForFileSavePrompt.DataSourceChange);
                            if (result == DialogResult.Yes)
                                FileSave();
                        }
                        ReinitExtractorDefinitionForm();

                        this.txtExtractorName.Text = _defaultDataExtractorName;
                        this.cboSourceDataLocation.Text = ExtractorDataTypeList.ExtractorDataLocations[ExtractorDataTypeList.FixedLengthTextFileListIndex];
                        this.txtSourceFixedLengthTextFilePath.Text = newPath;
                    }
                    else
                    {
                        //user does not want to overwrite current extractor definition
                        this.txtSourceFixedLengthTextFilePath.Text = saveCurrentFixedLengthTextFilePath;
                    }
                }
                else
                {
                    //previous database path was blank
                    ;
                }
            }
            else
            {
                //no change in the database path
                ;
            }

        }

        private void SourceGetXmlFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceXmlFileFolder;
            string saveCurrentXmlFilePath = this.txtSourceXmlFilePath.Text;
            string filter = "XML Files|*.xml|All Files|*.*";

            GetFilePath(_openFileDialogIndex, this.txtSourceXmlFilePath, defaultFolder, filter);

            if (this.txtSourceXmlFilePath.Text != saveCurrentXmlFilePath)
            {
                if (saveCurrentXmlFilePath.Trim() != string.Empty)
                {
                    DialogResult res = OverwriteCurrentExtractorDefinition();
                    if (res == System.Windows.Forms.DialogResult.Yes)
                    {
                        string newPath = this.txtSourceXmlFilePath.Text;
                        this.txtSourceXmlFilePath.Text = saveCurrentXmlFilePath;
                        if (ExtractorDefinitionHasChanges())
                        {
                            DialogResult result = PromptForFileSaveYesNo(ReasonForFileSavePrompt.DataSourceChange);
                            if (result == DialogResult.Yes)
                                FileSave();
                        }
                        ReinitExtractorDefinitionForm();

                        this.txtExtractorName.Text = _defaultDataExtractorName;
                        this.cboSourceDataLocation.Text = ExtractorDataTypeList.ExtractorDataLocations[ExtractorDataTypeList.XMLFileListIndex];
                        this.txtSourceXmlFilePath.Text = newPath;
                    }
                    else
                    {
                        //user does not want to overwrite current extractor definition
                        this.txtSourceXmlFilePath.Text = saveCurrentXmlFilePath;
                    }
                }
                else
                {
                    //previous database path was blank
                    ;
                }
            }
            else
            {
                //no change in the database path
                ;
            }

        }

        private void SourceGetXsdFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultSourceXmlFileFolder;
            string filter = "XML Schema Files|*.xsd|All Files|*.*";

            GetFilePath(_openFileDialogIndex, this.txtSourceXsdFilePath, defaultFolder, filter);
        }


        private void DestGetAccessDatabaseFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationAccessDatabaseFolder;
            string filter = string.Empty;
            if (this.optDestAccess2007.Checked)
                filter = "Access 2007 Files|*.accdb|All Files|*.*";
            else
                filter = "Access 2003 Files|*.mdb|All Files|*.*";

            GetFilePath(_saveFileDialogIndex, this.txtDestAccessDatabaseFilePath, defaultFolder, filter);
        }

        private void DestGetExcelFile()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationExcelDataFileFolder;
            string filter = string.Empty;
            if (this.optDestExcel2007Format.Checked)
                filter = "Excel 2007 Files|*.xlsx|All Files|*.*";
            else if (this.optDestExcel2003Format.Checked)
                filter = "Excel 2003 Files|*.xls|All Files|*.*";
            else
                filter = "Excel CSV Files|*.csv|All Files|*.*";

            GetFilePath(_saveFileDialogIndex, this.txtDestExcelFilePath, defaultFolder, filter);
        }

        private void DestGetDelimitedTextFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationDelimitedTextFileFolder;
            string filter = string.Empty;
            if (this.optDestCommaDelimited.Checked)
                filter = "Comma Delimited Text Files|*.txt;*.csv|All Files|*.*";
            else if (this.optDestTabDelimited.Checked)
                filter = "Tab Delimited Text Files|*.tab|All Files|*.*";
            else
                filter = "Delimited Text Files|*.txt;*.csv;*.tab;*.dat|All Files|*.*";

            GetFilePath(_saveFileDialogIndex, this.txtDestDelimitedTextFilePath, defaultFolder, filter);
        }

        private void DestGetFixedLengthTextFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationFixedLengthTextFileFolder;
            string filter = "Fixed Length Text Files|*.txt;*.dat|All Files|*.*";

            GetFilePath(_saveFileDialogIndex, this.txtDestFixedLengthTextFilePath, defaultFolder, filter);
        }

        private void DestGetXmlFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationXmlFileFolder;
            string filter = "XML Files|*.xml|All Files|*.*";

            GetFilePath(_saveFileDialogIndex, this.txtDestXmlFilePath, defaultFolder, filter);
        }

        private void DestGetXsdFilePath()
        {
            string defaultFolder = pfDataExtractorCP.Properties.Settings.Default.DefaultDestinationXmlFileFolder;
            string filter = "XML Schema Definition Files|*.xsd|All Files|*.*";

            GetFilePath(_saveFileDialogIndex, this.txtDestXsdFilePath, defaultFolder, filter);
        }

        private DialogResult OverwriteCurrentExtractorDefinition()
        {
            _msg.Length = 0;
            _msg.Append("Do you wish to replace the current Extractor definition with a new one?");
            //msg.Append(Environment.NewLine);
            //msg.Append(Environment.NewLine);
            //msg.Append("You will lose any unsaved changes in current definition if you click Yes.");

            DialogResult res = AppMessages.DisplayMessage(_msg.ToString(), "Create New Extractor Definition?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }


        //*******************************************************************************************************
        //  Routines to create an extractor definition object and load form data into it
        //*******************************************************************************************************

        private PFExtractorDefinition CreateExtractorDefinition()
        {
            PFExtractorDefinition ed = new PFExtractorDefinition();

            ed.ExtractorName = this.txtExtractorName.Text;
            ed.ExtractorSource = GetExtractorDataLocation(this.cboSourceDataLocation.Text);
            ed.ExtractorDestination = GetExtractorDataLocation(this.cboDestDataLocation.Text);
            
            ed.RelationalDatabaseSource = GetRelationalDatabaseSource();
            ed.RelationalDatabaseDestination = GetRelationalDatabaseDestination();

            ed.MsAccessSource = GetMsAccessSource();
            ed.MsAccessDestination = GetMsAccessDestination();

            ed.MsExcelSource = GetMsExcelSource();
            ed.MsExcelDestination = GetMsExcelDestination();

            ed.DelimitedTextFileSource = GetDelimitedTextFileSource();
            ed.DelimitedTextFileDestination = GetDelimitedTextFileDestination();

            ed.FixedLengthTextFileSource = GetFixedLengthTextFileSource();
            ed.FixedLengthTextFileDestination = GetFixedLengthTextFileDestination();

            ed.XmlFileSource = GetXmlFileSource();
            ed.XmlFileDestination = GetXmlFileDestination();

            ed.RandomOrdersDefinition = _randomOrdersDefinition.Copy();

            return ed;
        }

        private enExtractorDataLocation GetExtractorDataLocation(string location)
        {
            enExtractorDataLocation dataLocation = enExtractorDataLocation.Unknown;

            switch (location)
            {
                case "Relational Database":
                    dataLocation = enExtractorDataLocation.RelationalDatabase;
                    break;
                case "Access Database File":
                    dataLocation = enExtractorDataLocation.AccessDatabaseFile;
                    break;
                case "Excel Data File":
                    dataLocation = enExtractorDataLocation.ExcelDataFile;
                    break;
                case "Text File (Delimited)":
                    dataLocation = enExtractorDataLocation.DelimitedTextFile;
                    break;
                case "Text File (Fixed Length)":
                    dataLocation = enExtractorDataLocation.FixedLengthTextFile;
                    break;
                case "XML File":
                    dataLocation = enExtractorDataLocation.XMLFile;
                    break;
                default:
                    dataLocation = enExtractorDataLocation.Unknown;
                    break;
            }

            return dataLocation;
        }

        public string GetExtractorDataLocationText(enExtractorDataLocation locationEnum)
        {
            string dataLocationText = string.Empty;

            switch (locationEnum)
            {
                case enExtractorDataLocation.RelationalDatabase:
                    dataLocationText = "Relational Database";
                    break;
                case enExtractorDataLocation.AccessDatabaseFile:
                    dataLocationText = "Access Database File";
                    break;
                case enExtractorDataLocation.ExcelDataFile:
                    dataLocationText = "Excel Data File";
                    break;
                case enExtractorDataLocation.DelimitedTextFile:
                    dataLocationText = "Text File (Delimited)";
                    break;
                case enExtractorDataLocation.FixedLengthTextFile:
                    dataLocationText = "Text File (Fixed Length)";
                    break;
                case enExtractorDataLocation.XMLFile:
                    dataLocationText = "XML File";
                    break;
                default:
                    dataLocationText = string.Empty;
                    break;
            }

            return dataLocationText;
        }

        private int GetExtractorDataLocationIndex(string location)
        {
            int dataLocationIndex = -1;

            //assume cboSourceDataLocation and cboDestDataLocation items lists are identical
            //see SetDataLocationDropdowns() which is called by SetFormValues() during form initial load routine
            for (int i = 0; i < this.cboSourceDataLocation.Items.Count; i++)
            {
                if (this.cboSourceDataLocation.Items[i].ToString() == location)
                {
                    dataLocationIndex = i;
                    break;
                }
            }

            return dataLocationIndex;
        }

        private PFRelationalDatabaseSource GetRelationalDatabaseSource()
        {
            PFRelationalDatabaseSource rds = new PFRelationalDatabaseSource();
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.RelationalDatabase);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            if (this.cboDataSource.Text.Trim().Length > 0)
                rds.DbPlatform = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDataSource.Text);
            else
                rds.DbPlatform = DatabasePlatform.Unknown;
            rds.ConnectionString = this.txtSourceConnectionString.Text;
            rds.SqlQuery = this.txtRelDbSqlQuery.Text;
            rds.OutputOptions = _saveExtractorOutputOptions[dataLocationIndex].Copy();

            return rds;
        }

        private PFRelationalDatabaseDestination GetRelationalDatabaseDestination()
        {
            PFRelationalDatabaseDestination rdd = new PFRelationalDatabaseDestination();

            if (this.cboDataDestination.Text.Trim().Length > 0)
                rdd.DbPlatform = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDataDestination.Text);
            else
                rdd.DbPlatform = DatabasePlatform.Unknown;
            rdd.ConnectionString = this.txtDestinationConnectionString.Text;
            rdd.TableName = this.txtDestinationTableName.Text;
            rdd.OverwriteTableIfExists = this.chkReplaceExistingDbTable.Checked;
            rdd.OutputBatchSize = AppTextGlobals.ConvertStringToInt(this.txtOutputBatchSize.Text, 1);

            return rdd;
        }

        private PFMsAccessSource GetMsAccessSource()
        {
            PFMsAccessSource acc = new PFMsAccessSource();
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.AccessDatabaseFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            acc.SourceIsAccess2007 = this.optSourceAccess2007.Checked;
            acc.SourceIsAccess2003 = this.optSourceAccess2003.Checked;
            acc.DatabasePath = this.txtSourceAccessDatabaseFilePath.Text;
            acc.DatabaseUsername = this.txtSourceAccessUsername.Text;
            acc.DatabasePassword = this.txtSourceAccessPassword.Text;
            acc.SqlQuery = this.txtAccessSQLQuery.Text;
            acc.OutputOptions = _saveExtractorOutputOptions[dataLocationIndex].Copy();

            return acc;
        }

        private PFMsAccessDestination GetMsAccessDestination()
        {
            PFMsAccessDestination acc = new PFMsAccessDestination();

            acc.DestinationIsAccess2007 = this.optDestAccess2007.Checked;
            acc.DestinationIsAccess2003 = this.optDestAccess2003.Checked;
            acc.DatabasePath = this.txtDestAccessDatabaseFilePath.Text;
            acc.OverwriteDatabaseFileIfExists = this.chkReplaceExistingAccessDatabaseFile.Checked;
            acc.DatabaseUsername = this.txtDestAccessUsername.Text;
            acc.DatabasePassword = this.txtDestAccessPassword.Text;
            acc.OutputTableName = this.txtDestAccessTableName.Text;
            acc.OverwriteTableIfExists = this.chkReplaceExistingAccessTable.Checked;

            return acc;
        }

        private PFMsExcelSource GetMsExcelSource()
        {
            PFMsExcelSource exc = new PFMsExcelSource();
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.ExcelDataFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            exc.SourceIsExcel2007Format = this.optSourceExcel2007Format.Checked;
            exc.SourceIsExcel2003Format = this.optSourceExcel2003Format.Checked;
            exc.SourceIsExcelCsvFormat = this.optSourceExcelCSVFormat.Checked;
            exc.DocumentFilePath = this.txtSourceExcelFilePath.Text;
            exc.SheetName = this.txtSourceExcelSheetName.Text;
            exc.DataLocationInRowColFormat = this.optExcelRowColFormat.Checked;
            exc.DataLocationInNamedRangeFormat = this.optExcelNamedRange.Checked;
            exc.StartRow = AppTextGlobals.ConvertStringToInt(this.txtStartRow.Text, 1);
            exc.EndRow = AppTextGlobals.ConvertStringToInt(this.txtEndRow.Text, 1);
            exc.StartCol = AppTextGlobals.ConvertStringToInt(this.txtStartCol.Text, 1);
            exc.EndCol = AppTextGlobals.ConvertStringToInt(this.txtEndCol.Text, 1);
            exc.RangeName = this.txtExcelRangeName.Text;
            exc.ColumnNamesInFirstRow = this.chkSourceExcelColumnNamesInFirstRow.Checked;
            exc.OutputOptions = _saveExtractorOutputOptions[dataLocationIndex].Copy();

            return exc;
        }

        private PFMsExcelDestination GetMsExcelDestination()
        {
            PFMsExcelDestination exc = new PFMsExcelDestination();

            exc.DestinationIsExcel2007Format = this.optDestExcel2007Format.Checked;
            exc.DestinationIsExcel2003Format = this.optDestExcel2003Format.Checked;
            exc.DestinationIsExcelCsvFormat = this.optDestExcelCSVFormat.Checked;
            exc.DocumentFilePath = this.txtDestExcelFilePath.Text;
            exc.SheetName = this.txtDestExcelSheetName.Text;
            exc.OverwriteFileIfExists = this.chkReplaceExistingExcelFile.Checked;
            exc.OverwriteSheetIfExists = this.chkReplaceExcelSheet.Checked;
            exc.ColumnNamesInFirstRow = this.chkDestExcelColumnNamesInFirstRow.Checked;

            return exc;
        }

        private PFDelimitedTextFileSource GetDelimitedTextFileSource()
        {
            PFDelimitedTextFileSource dtx = new PFDelimitedTextFileSource();
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.DelimitedTextFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            dtx.TextFilePath = this.txtSourceDelimitedTextFilePath.Text;
            dtx.ColumnsCommaDelimited = this.optSourceCommaDelimited.Checked;
            dtx.ColumnsTabDelimited = this.optSourceTabDelimited.Checked;
            dtx.ColumnsHaveOtherDelimiter = this.optSourceOtherDelimiter.Checked;
            dtx.OtherSeparator = this.txtSourceOtherSeparator.Text;
            dtx.UseCrLfLineTerminator = this.optSourceCrLf.Checked;
            dtx.ColumnNamesOnFirstLine = this.chkSourceColumnNamesOnFirstLine.Checked;
            dtx.StringValuesSurroundedWithQuotationMarks = this.chkSourceStringValueQuotes.Checked;
            dtx.OutputOptions = _saveExtractorOutputOptions[dataLocationIndex].Copy();


            return dtx;
        }

        private PFDelimitedTextFileDestination GetDelimitedTextFileDestination()
        {
            PFDelimitedTextFileDestination dtx = new PFDelimitedTextFileDestination();

            dtx.TextFilePath = this.txtDestDelimitedTextFilePath.Text;
            dtx.OverwriteFileIfExists = this.chkReplaceExistingDelimitedTextFile.Checked;
            dtx.ColumnsCommaDelimited = this.optDestCommaDelimited.Checked;
            dtx.ColumnsTabDelimited = this.optDestTabDelimited.Checked;
            dtx.ColumnsHaveOtherDelimiter = this.optDestOtherDelimiter.Checked;
            dtx.OtherSeparator = this.txtDestOtherSeparator.Text;
            dtx.UseCrLfLineTerminator = this.optDestCrLf.Checked;
            dtx.UseOtherLineTerminator = this.optDestOtherLineTerminator.Checked;
            dtx.OtherLineTerminator = this.txtDestOtherLineTerminator.Text;
            dtx.ColumnNamesOnFirstLine = this.chkDestIncludeColumnNamesOnFirstLineOfOutput.Checked;
            dtx.StringValuesSurroundedWithQuotationMarks = this.chkDestStringValueQuotes.Checked;

            return dtx;
        }

        private PFFixedLengthTextFileSource GetFixedLengthTextFileSource()
        {
            PFFixedLengthTextFileSource fxl = new PFFixedLengthTextFileSource();
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.FixedLengthTextFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            fxl.TextFilePath = this.txtSourceFixedLengthTextFilePath.Text;
            fxl.ColumnNamesOnFirstLineOfFile = this.chkSourceColumnNamesOnFirstLineOfFile.Checked;
            fxl.LineTerminatorAppendedToEachLine = this.chkSourceLineTerminatorAppendedToEachLine.Checked;
            fxl.UseCrLfLineTerminator = this.optSourceFixedLenCrLf.Checked;
            fxl.ExpectedLineLength = AppTextGlobals.ConvertStringToInt(this.txtSourceFixedLengthExpectedLineWidth.Text, 1);
            fxl.ColumnDefinitions = _saveInputColumnDefinitions.Copy();
            fxl.OutputOptions = _saveExtractorOutputOptions[dataLocationIndex].Copy();

            return fxl;
        }

        private PFFixedLengthTextFileDestination GetFixedLengthTextFileDestination()
        {
            PFFixedLengthTextFileDestination fxl = new PFFixedLengthTextFileDestination();

            fxl.TextFilePath = this.txtDestFixedLengthTextFilePath.Text;
            fxl.OverwriteFileIfExists = this.chkReplaceExistingFixedLengthTextFile.Checked;
            fxl.ColumnNamesOnFirstLineOfFile = this.chkDestColumnNamesOnFirstLineOfOutput.Checked;
            fxl.AllowDataTruncation = this.chkDestAllowDataTruncation.Checked;
            fxl.AppendLineTerminatorToEachLine = this.chkDestUseLineTerminator.Checked;
            fxl.UseCrLfLineTerminator = this.optDestFixedLenCrLf.Checked;
            fxl.UseOtherLineTerminator = this.optDestFixedLenOtherLineTerminator.Checked;
            fxl.OtherLineTerminator = this.txtDestFixedLenOtherLineTerminator.Text;
            fxl.ExpectedOutputLineLength = AppTextGlobals.ConvertStringToInt(this.txtOutputLineLength.Text,-1);
            fxl.ColumnDefinitions = _saveOutputColumnDefinitions.Copy();
            //fxl.OutputLineLength = AppTextGlobals.ConvertStringToInt(this.txtOutputLineLength.Text, -1);  //read-only property

            return fxl;
        }

        private PFXmlFileSource GetXmlFileSource()
        {
            PFXmlFileSource xml = new PFXmlFileSource();
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.XMLFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            xml.XmlFilePath = this.txtSourceXmlFilePath.Text;
            xml.SourceXmlNoSchema = this.optSourceXmlNoSchema.Checked;
            xml.SourceXmlSchemaInXmlFile = this.optSourceXmlSchemaInXmlFile.Checked;
            xml.SourceXmlSchemaInSeparateXsdFile = this.optSourceXmlSchemaInSeparateXsdFile.Checked;
            xml.SourceXsdFilePath = this.txtSourceXsdFilePath.Text;
            xml.OutputOptions = _saveExtractorOutputOptions[dataLocationIndex].Copy();

            return xml;
        }

        private PFXmlFileDestination GetXmlFileDestination()
        {
            PFXmlFileDestination xml = new PFXmlFileDestination();

            xml.DestXmlFilePath = this.txtDestXmlFilePath.Text;
            xml.ReplaceExistingXmlFile = this.chkReplaceExistingXmlFile.Checked;
            xml.DestXmlNoSchema = this.optDestXmlNoSchema.Checked;
            xml.DestXmlSchemaInXmlFile = this.optDestXmlSchemaInXmlFile.Checked;
            xml.DestXmlSchemaInSeparateXsdFile = this.optDestXmlSchemaInSeparateXsdFile.Checked;
            xml.DestXsdFilePath = this.txtDestXsdFilePath.Text;
            xml.ReplaceExistingXsdFile = this.chkReplaceExistingXsdFile.Checked;

            return xml;
        }

        private void ShowRandomizerBuilder()
        {
            int sourceDataLocationIndex = this.cboSourceDataLocation.SelectedIndex;
            PFList<DataTableRandomizerColumnSpec> colSpecs = _saveExtractorOutputOptions[sourceDataLocationIndex].RandomizerColSpecs;
            PFExtractorDefinition exdef = CreateExtractorDefinition();
            _appProcessor.RunRandomizer(exdef, ref colSpecs);
            _saveExtractorOutputOptions[sourceDataLocationIndex].RandomizerColSpecs = colSpecs;
        }

        private void ShowFilterBuilder()
        {
            int sourceDataLocationIndex = this.cboSourceDataLocation.SelectedIndex;
            PFFilter filter = _saveExtractorOutputOptions[sourceDataLocationIndex].OutputFilter;
            PFExtractorDefinition exdef = CreateExtractorDefinition();
            _appProcessor.SetFilter(exdef, ref filter);
            _saveExtractorOutputOptions[sourceDataLocationIndex].OutputFilter = filter;
            this.Focus();
        }

        //*******************************************************************************************************
        //  Routines to initialize extractor form with default or blank values
        //*******************************************************************************************************

        private void InitExtractorDefinitionForm()
        {
            SetExtractorDefinitionsSaveFolder();
            SetExtractorDefinitionName();
            SetDataLocationDropdowns();

            InitRelationalDatabaseSource();
            InitRelationalDatabaseDestination();

            InitMsAccessSource();
            InitMsAccessDestination();

            InitMsExcelSource();
            InitMsExcelDestination();

            InitDelimitedTextFileSource();
            InitDelimitedTextFileDestination();

            InitFixedLengthTextFileSource();
            InitFixedLengthTextFileDestination();

            InitXmlFileSource();
            InitXmlFileDestination();

            _randomOrdersDefinition = new PFRandomOrdersDefinition();

            SetOutputOptions();
        }

        private void InitRelationalDatabaseSource()
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.RelationalDatabase);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.cboDataSource.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultInputDatabaseType;
            this.txtSourceConnectionString.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultInputDatabaseConnectionString;
            this.txtRelDbSqlQuery.Text = string.Empty;
            _saveExtractorOutputOptions[dataLocationIndex] = new PFExtractorOutputOptions();
            SetBatchSize();
        }

        private void InitRelationalDatabaseDestination()
        {
            this.cboDataDestination.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultOutputDatabaseType;
            this.txtDestinationConnectionString.Text = pfDataExtractorCP.Properties.Settings.Default.DefaultOutputDatabaseConnectionString;
            this.txtDestinationTableName.Text = string.Empty;
            this.chkReplaceExistingDbTable.Checked = true;
            SetBatchSize();
        }

        private void InitMsAccessSource()
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.AccessDatabaseFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.optSourceAccess2007.Checked = true;
            this.optSourceAccess2003.Checked = false;
            this.txtSourceAccessDatabaseFilePath.Text = string.Empty;
            this.txtSourceAccessUsername.Text = string.Empty;
            this.txtSourceAccessPassword.Text = string.Empty;
            this.txtAccessSQLQuery.Text = string.Empty;
            _saveExtractorOutputOptions[dataLocationIndex] = new PFExtractorOutputOptions();
        }

        private void InitMsAccessDestination()
        {
            this.optDestAccess2007.Checked = true;
            this.optDestAccess2003.Checked = false;
            this.txtDestAccessDatabaseFilePath.Text = string.Empty;
            this.chkReplaceExistingAccessDatabaseFile.Checked = true;
            this.txtDestAccessUsername.Text = string.Empty;
            this.txtDestAccessPassword.Text = string.Empty;
            this.txtDestAccessTableName.Text = string.Empty;
            this.chkReplaceExistingAccessTable.Checked = true;
        }

        private void InitMsExcelSource()
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.ExcelDataFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.optSourceExcel2007Format.Checked = true;
            this.optSourceExcel2003Format.Checked = false;
            this.optSourceExcelCSVFormat.Checked = false;
            this.txtSourceExcelFilePath.Text = string.Empty;
            this.txtSourceExcelSheetName.Text = string.Empty;
            this.optExcelRowColFormat.Checked = false;
            this.optExcelNamedRange.Checked = true;
            this.txtStartRow.Text = string.Empty;
            this.txtEndRow.Text = string.Empty;
            this.txtStartCol.Text = string.Empty;
            this.txtEndCol.Text = string.Empty;
            this.txtExcelRangeName.Text = string.Empty;
            this.chkSourceExcelColumnNamesInFirstRow.Checked = true;
            _saveExtractorOutputOptions[dataLocationIndex] = new PFExtractorOutputOptions();
        }

        private void InitMsExcelDestination()
        {
            this.optDestExcel2007Format.Checked = true;
            this.optDestExcel2003Format.Checked = false;
            this.optDestExcelCSVFormat.Checked = false;
            this.txtDestExcelFilePath.Text = string.Empty;
            this.txtDestExcelSheetName.Text = string.Empty;
            this.chkReplaceExistingExcelFile.Checked = true;
            this.chkReplaceExcelSheet.Checked = true;
            this.chkDestExcelColumnNamesInFirstRow.Checked = true;
        }

        private void InitDelimitedTextFileSource()
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.DelimitedTextFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.txtSourceDelimitedTextFilePath.Text = string.Empty;
            this.optSourceCommaDelimited.Checked = true;
            this.optSourceTabDelimited.Checked = false;
            this.optSourceOtherDelimiter.Checked = false;
            this.txtSourceOtherSeparator.Text = string.Empty;
            this.optSourceCrLf.Checked = true;
            this.chkSourceColumnNamesOnFirstLine.Checked = true;
            this.chkSourceStringValueQuotes.Checked = false;
            _saveExtractorOutputOptions[dataLocationIndex] = new PFExtractorOutputOptions();

        }

        private void InitDelimitedTextFileDestination()
        {
            this.txtDestDelimitedTextFilePath.Text = string.Empty;
            this.chkReplaceExistingDelimitedTextFile.Checked = true;
            this.optDestCommaDelimited.Checked = true;
            this.optDestTabDelimited.Checked = false;
            this.optDestOtherDelimiter.Checked = false;
            this.txtDestOtherSeparator.Text = string.Empty;
            this.optDestCrLf.Checked = true;
            this.chkDestIncludeColumnNamesOnFirstLineOfOutput.Checked = true;
            this.chkDestStringValueQuotes.Checked = false;
        }

        private void InitFixedLengthTextFileSource()
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.FixedLengthTextFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.txtSourceFixedLengthTextFilePath.Text = string.Empty;
            this.chkSourceColumnNamesOnFirstLineOfFile.Checked = true;
            this.chkSourceLineTerminatorAppendedToEachLine.Checked = true;
            this.optSourceFixedLenCrLf.Checked = true;
            this.txtSourceFixedLengthExpectedLineWidth.Text = string.Empty;
            _saveInputColumnDefinitions = new PFColumnDefinitionsExt(1);
            _saveExtractorOutputOptions[dataLocationIndex] = new PFExtractorOutputOptions();

        }

        private void InitFixedLengthTextFileDestination()
        {
            this.txtDestFixedLengthTextFilePath.Text = string.Empty;
            this.chkReplaceExistingFixedLengthTextFile.Checked = true;
            this.chkDestColumnNamesOnFirstLineOfOutput.Checked = true;
            this.chkDestAllowDataTruncation.Checked = false;
            this.chkDestUseLineTerminator.Checked = true;
            this.optDestFixedLenCrLf.Checked = true;
            _saveOutputColumnDefinitions = new PFColumnDefinitionsExt(1);
            this.txtOutputLineLength.Text = string.Empty;
        }

        private void InitXmlFileSource()
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.XMLFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.txtSourceXmlFilePath.Text = string.Empty;
            this.optSourceXmlNoSchema.Checked = true;
            this.optSourceXmlSchemaInXmlFile.Checked = false;
            this.optSourceXmlSchemaInSeparateXsdFile.Checked = false;
            this.txtSourceXsdFilePath.Text = string.Empty;
            _saveExtractorOutputOptions[dataLocationIndex] = new PFExtractorOutputOptions();
        }

        private void InitXmlFileDestination()
        {
            this.txtDestXmlFilePath.Text = string.Empty;
            this.chkReplaceExistingXmlFile.Checked = true;
            this.optDestXmlNoSchema.Checked = true;
            this.optDestXmlSchemaInXmlFile.Checked = false;
            this.optDestXmlSchemaInSeparateXsdFile.Checked = false;
            this.txtDestXsdFilePath.Text = string.Empty;
            this.chkReplaceExistingXsdFile.Checked = true;
        }

        //*******************************************************************************************************
        //  Routines to fill extractor form with data from extractor definition object
        //*******************************************************************************************************
        
        private void FillExtractorDefinitionForm(PFExtractorDefinition ed)
        {
            this.txtExtractorDefinitionsSaveFolder.Text = _defaultDataExtractorDefsFolder;
            this.txtExtractorName.Text = ed.ExtractorName;
            this.cboDestDataLocation.Text = GetExtractorDataLocationText(ed.ExtractorDestination);
            this.cboSourceDataLocation.Text = GetExtractorDataLocationText(ed.ExtractorSource);

            FillRelationalDatabaseSource(ed.RelationalDatabaseSource);
            FillRelationalDatabaseDestination(ed.RelationalDatabaseDestination);

            FillMsAccessSource(ed.MsAccessSource);
            FillMsAccessDestination(ed.MsAccessDestination);

            FillMsExcelSource(ed.MsExcelSource);
            FillMsExcelDestination(ed.MsExcelDestination);

            FillDelimitedTextFileSource(ed.DelimitedTextFileSource);
            FillDelimitedTextFileDestination(ed.DelimitedTextFileDestination);

            FillFixedLengthTextFileSource(ed.FixedLengthTextFileSource);
            FillFixedLengthTextFileDestination(ed.FixedLengthTextFileDestination);

            FillXmlFileSource(ed.XmlFileSource);
            FillXmlFileDestination(ed.XmlFileDestination);

            _randomOrdersDefinition = ed.RandomOrdersDefinition.Copy();

            ResetSelectedTab(true);
        }

        private void FillRelationalDatabaseSource(PFRelationalDatabaseSource rds)
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.RelationalDatabase);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            if (rds.DbPlatform != DatabasePlatform.Unknown)
                this.cboDataSource.Text = rds.DbPlatform.ToString();
            else
                this.cboDataSource.Text = string.Empty;
            this.txtSourceConnectionString.Text = rds.ConnectionString;
            this.txtRelDbSqlQuery.Text = rds.SqlQuery;
            _saveExtractorOutputOptions[dataLocationIndex] = PFExtractorOutputOptions.LoadFromXmlString(rds.OutputOptions.ToXmlString());
        }

        private void FillRelationalDatabaseDestination(PFRelationalDatabaseDestination rdd)
        {
            if (rdd.DbPlatform != DatabasePlatform.Unknown)
                this.cboDataDestination.Text = rdd.DbPlatform.ToString();
            else
                this.cboDataDestination.Text = string.Empty;
            this.txtDestinationConnectionString.Text = rdd.ConnectionString;
            this.txtDestinationTableName.Text = rdd.TableName;
            this.chkReplaceExistingDbTable.Checked = rdd.OverwriteTableIfExists;
            this.txtOutputBatchSize.Text = rdd.OutputBatchSize.ToString();
        }

        private void FillMsAccessSource(PFMsAccessSource acc)
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.AccessDatabaseFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.optSourceAccess2007.Checked = acc.SourceIsAccess2007;
            this.optSourceAccess2003.Checked = acc.SourceIsAccess2003;
            this.txtSourceAccessDatabaseFilePath.Text = acc.DatabasePath;
            this.txtSourceAccessUsername.Text = acc.DatabaseUsername;
            this.txtSourceAccessPassword.Text = acc.DatabasePassword;
            this.txtAccessSQLQuery.Text = acc.SqlQuery;
            //_saveExtractorOutputOptions[dataLocationIndex] = acc.OutputOptions;
            _saveExtractorOutputOptions[dataLocationIndex] = PFExtractorOutputOptions.LoadFromXmlString(acc.OutputOptions.ToXmlString());
        }

        private void FillMsAccessDestination(PFMsAccessDestination acc)
        {
            this.optDestAccess2007.Checked = acc.DestinationIsAccess2007;
            this.optDestAccess2003.Checked = acc.DestinationIsAccess2003;
            this.txtDestAccessDatabaseFilePath.Text = acc.DatabasePath;
            this.chkReplaceExistingAccessDatabaseFile.Checked = acc.OverwriteDatabaseFileIfExists;
            this.txtDestAccessUsername.Text = acc.DatabaseUsername;
            this.txtDestAccessPassword.Text = acc.DatabasePassword;
            this.txtDestAccessTableName.Text = acc.OutputTableName;
            this.chkReplaceExistingAccessTable.Checked = acc.OverwriteTableIfExists;
        }

        private void FillMsExcelSource(PFMsExcelSource exc)
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.ExcelDataFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.optSourceExcel2007Format.Checked = exc.SourceIsExcel2007Format;
            this.optSourceExcel2003Format.Checked = exc.SourceIsExcel2003Format;
            this.optSourceExcelCSVFormat.Checked = exc.SourceIsExcelCsvFormat;
            this.txtSourceExcelFilePath.Text = exc.DocumentFilePath;
            this.txtSourceExcelSheetName.Text = exc.SheetName;
            this.optExcelRowColFormat.Checked = exc.DataLocationInRowColFormat;
            this.optExcelNamedRange.Checked = exc.DataLocationInNamedRangeFormat;
            this.txtStartRow.Text = exc.StartRow.ToString();
            this.txtEndRow.Text = exc.EndRow.ToString();
            this.txtStartCol.Text = exc.StartCol.ToString();
            this.txtEndCol.Text = exc.EndCol.ToString();
            this.txtExcelRangeName.Text = exc.RangeName;
            this.chkSourceExcelColumnNamesInFirstRow.Checked = exc.ColumnNamesInFirstRow;
            //_saveExtractorOutputOptions[dataLocationIndex] = exc.OutputOptions;
            _saveExtractorOutputOptions[dataLocationIndex] = PFExtractorOutputOptions.LoadFromXmlString(exc.OutputOptions.ToXmlString());
        }

        private void FillMsExcelDestination(PFMsExcelDestination exc)
        {
            this.optDestExcel2007Format.Checked = exc.DestinationIsExcel2007Format;
            this.optDestExcel2003Format.Checked = exc.DestinationIsExcel2003Format;
            this.optDestExcelCSVFormat.Checked = exc.DestinationIsExcelCsvFormat;
            this.txtDestExcelFilePath.Text = exc.DocumentFilePath;
            this.txtDestExcelSheetName.Text = exc.SheetName;
            this.chkReplaceExistingExcelFile.Checked = exc.OverwriteFileIfExists;
            this.chkReplaceExcelSheet.Checked = exc.OverwriteSheetIfExists;
            this.chkDestExcelColumnNamesInFirstRow.Checked = exc.ColumnNamesInFirstRow;
        }
        
        private void FillDelimitedTextFileSource(PFDelimitedTextFileSource dtx)
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.DelimitedTextFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.txtSourceDelimitedTextFilePath.Text = dtx.TextFilePath;
            this.optSourceCommaDelimited.Checked = dtx.ColumnsCommaDelimited;
            this.optSourceTabDelimited.Checked = dtx.ColumnsTabDelimited;
            this.optSourceOtherDelimiter.Checked = dtx.ColumnsHaveOtherDelimiter;
            this.txtSourceOtherSeparator.Text = dtx.OtherSeparator;
            this.optSourceCrLf.Checked = dtx.UseCrLfLineTerminator;
            this.chkSourceColumnNamesOnFirstLine.Checked = dtx.ColumnNamesOnFirstLine;
            this.chkSourceStringValueQuotes.Checked = dtx.StringValuesSurroundedWithQuotationMarks;
            //_saveExtractorOutputOptions[dataLocationIndex] = dtx.OutputOptions;
            _saveExtractorOutputOptions[dataLocationIndex] = PFExtractorOutputOptions.LoadFromXmlString(dtx.OutputOptions.ToXmlString());
        }

        private void FillDelimitedTextFileDestination(PFDelimitedTextFileDestination dtx)
        {
            this.txtDestDelimitedTextFilePath.Text = dtx.TextFilePath;
            this.chkReplaceExistingDelimitedTextFile.Checked = dtx.OverwriteFileIfExists;
            this.optDestCommaDelimited.Checked = dtx.ColumnsCommaDelimited;
            this.optDestTabDelimited.Checked = dtx.ColumnsTabDelimited;
            this.optDestOtherDelimiter.Checked = dtx.ColumnsHaveOtherDelimiter;
            this.txtDestOtherSeparator.Text = dtx.OtherSeparator;
            this.optDestCrLf.Checked = dtx.UseCrLfLineTerminator;
            this.optDestOtherLineTerminator.Checked = dtx.UseOtherLineTerminator;
            this.txtDestOtherLineTerminator.Text = dtx.OtherLineTerminator;
            this.chkDestIncludeColumnNamesOnFirstLineOfOutput.Checked = dtx.ColumnNamesOnFirstLine;
            this.chkDestStringValueQuotes.Checked = dtx.StringValuesSurroundedWithQuotationMarks;
        }

        private void FillFixedLengthTextFileSource(PFFixedLengthTextFileSource fxl)
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.FixedLengthTextFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.txtSourceFixedLengthTextFilePath.Text = fxl.TextFilePath;
            this.chkSourceColumnNamesOnFirstLineOfFile.Checked = fxl.ColumnNamesOnFirstLineOfFile;
            this.chkSourceLineTerminatorAppendedToEachLine.Checked = fxl.LineTerminatorAppendedToEachLine;
            this.optSourceFixedLenCrLf.Checked = fxl.UseCrLfLineTerminator;
            this.txtSourceFixedLengthExpectedLineWidth.Text = fxl.ExpectedLineLength.ToString();
            //_saveInputColumnDefinitions = fxl.ColumnDefinitions;
            _saveInputColumnDefinitions = PFColumnDefinitionsExt.LoadFromXmlString(fxl.ColumnDefinitions.ToXmlString());
            //_saveExtractorOutputOptions[dataLocationIndex] = fxl.OutputOptions;
            _saveExtractorOutputOptions[dataLocationIndex] = PFExtractorOutputOptions.LoadFromXmlString(fxl.OutputOptions.ToXmlString());
        }

        private void FillFixedLengthTextFileDestination(PFFixedLengthTextFileDestination fxl)
        {
            this.txtDestFixedLengthTextFilePath.Text = fxl.TextFilePath;
            this.chkReplaceExistingFixedLengthTextFile.Checked = fxl.OverwriteFileIfExists;
            this.chkDestColumnNamesOnFirstLineOfOutput.Checked = fxl.ColumnNamesOnFirstLineOfFile;
            this.chkDestAllowDataTruncation.Checked = fxl.AllowDataTruncation;
            this.chkDestUseLineTerminator.Checked = fxl.AppendLineTerminatorToEachLine;
            this.optDestFixedLenCrLf.Checked = fxl.UseCrLfLineTerminator;
            this.optDestFixedLenOtherLineTerminator.Checked = fxl.UseOtherLineTerminator;
            this.txtDestFixedLenOtherLineTerminator.Text = fxl.OtherLineTerminator;
            //_saveOutputColumnDefinitions = fxl.ColumnDefinitions;
            _saveOutputColumnDefinitions = PFColumnDefinitionsExt.LoadFromXmlString(fxl.ColumnDefinitions.ToXmlString());
            //CalculateExpectedOutputColumnLength(_saveOutputColumnDefinitions); //this.txtOutputLineLength.Text filled in here
            this.txtOutputLineLength.Text = fxl.ExpectedOutputLineLength.ToString();
        }

        private void FillXmlFileSource(PFXmlFileSource xml)
        {
            string dataLocationText = GetExtractorDataLocationText(enExtractorDataLocation.XMLFile);
            int dataLocationIndex = GetExtractorDataLocationIndex(dataLocationText);

            this.txtSourceXmlFilePath.Text = xml.XmlFilePath;
            this.optSourceXmlNoSchema.Checked = xml.SourceXmlNoSchema;
            this.optSourceXmlSchemaInXmlFile.Checked = xml.SourceXmlSchemaInXmlFile;
            this.optSourceXmlSchemaInSeparateXsdFile.Checked = xml.SourceXmlSchemaInSeparateXsdFile;
            this.txtSourceXsdFilePath.Text = xml.SourceXsdFilePath;
            //_saveExtractorOutputOptions[dataLocationIndex] = xml.OutputOptions;
            _saveExtractorOutputOptions[dataLocationIndex] = PFExtractorOutputOptions.LoadFromXmlString(xml.OutputOptions.ToXmlString());
        }

        private void FillXmlFileDestination(PFXmlFileDestination xml)
        {
            this.txtDestXmlFilePath.Text = xml.DestXmlFilePath;
            this.chkReplaceExistingXmlFile.Checked = xml.ReplaceExistingXmlFile;
            this.optDestXmlNoSchema.Checked = xml.DestXmlNoSchema;
            this.optDestXmlSchemaInXmlFile.Checked = xml.DestXmlSchemaInXmlFile;
            this.optDestXmlSchemaInSeparateXsdFile.Checked = xml.DestXmlSchemaInSeparateXsdFile;
            this.txtDestXsdFilePath.Text = xml.DestXsdFilePath;
            this.chkReplaceExistingXsdFile.Checked = xml.ReplaceExistingXsdFile;
        }


        private void RunPreviewSource()
        {
            enExtractorDataLocation dataLocation = GetExtractorDataLocation(this.cboSourceDataLocation.Text);
            PFExtractorDefinition exDef = CreateExtractorDefinition();
            int maxPreviewRows = -1;

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                if (this.txtMaxPreviewRows.Text.Trim().Length > 0)
                {
                    maxPreviewRows = AppTextGlobals.ConvertStringToInt(this.txtMaxPreviewRows.Text, -1);
                }

                if (this.chkEraseOutputLogBeforeEachTest.Checked)
                    Program._messageLog.Clear();

                _appProcessor.PreviewData(dataLocation,
                                          exDef,
                                          maxPreviewRows,
                                          false,
                                          false,
                                          false,
                                          Properties.Settings.Default.BatchSizeForRandomDataGeneration,
                                          Properties.Settings.Default.BatchSizeForDataImportsAndExports,
                                          Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly,
                                          Properties.Settings.Default.DefaultDataGridExportFolder,
                                          Properties.Settings.Default.DefaultOutputDatabaseType,
                                          Properties.Settings.Default.DefaultOutputDatabaseConnectionString
                                         );
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
                this.Focus();
            }
                 
        

        }

        private void RunPreviewOutput()
        {
            enExtractorDataLocation dataLocation = GetExtractorDataLocation(this.cboSourceDataLocation.Text);
            PFExtractorDefinition exDef = CreateExtractorDefinition();
            int maxPreviewRows = -1;

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                if (this.txtMaxPreviewRows.Text.Trim().Length > 0)
                {
                    maxPreviewRows = AppTextGlobals.ConvertStringToInt(this.txtMaxPreviewRows.Text, -1);
                }

                if (this.chkEraseOutputLogBeforeEachTest.Checked)
                    Program._messageLog.Clear();

                _appProcessor.PreviewData(dataLocation,
                                          exDef,
                                          maxPreviewRows,
                                          this.chkShowRowNumber.Checked,
                                          this.chkFilterOutput.Checked,
                                          this.chkRandomizeOutput.Checked,
                                          Properties.Settings.Default.BatchSizeForRandomDataGeneration,
                                          Properties.Settings.Default.BatchSizeForDataImportsAndExports,
                                          Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly,
                                          Properties.Settings.Default.DefaultDataGridExportFolder,
                                          Properties.Settings.Default.DefaultOutputDatabaseType,
                                          Properties.Settings.Default.DefaultOutputDatabaseConnectionString
                                         );
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
                this.Focus();
            }
                 
        
        }

        private void ExcelSourceFormatChanged()
        {
            if (this.optSourceExcelCSVFormat.Checked)
            {
                _saveNonCsvExcelSheetName = this.txtSourceExcelSheetName.Text;
                _saveUseExcelNamedRangeFormat = this.optExcelNamedRange.Checked;
                _saveUseExcelRowColFormat = this.optExcelRowColFormat.Checked;
                this.txtSourceExcelSheetName.Text = "Sheet1";
                this.optExcelRowColFormat.Checked = true;
                this.optExcelNamedRange.Enabled = false;
                this.panelExcelNamedRange.Enabled = false;
            }
            else
            {
                this.txtSourceExcelSheetName.Text = _saveNonCsvExcelSheetName;
                this.optExcelNamedRange.Checked = _saveUseExcelNamedRangeFormat;
                this.optExcelRowColFormat.Checked = _saveUseExcelRowColFormat;
                this.optExcelNamedRange.Enabled = true;
                this.panelExcelNamedRange.Enabled = true;
            }
        }

        public void CalculateExpectedOutputColumnLength(PFColumnDefinitionsExt _saveOutputColumnDefinitions)
        {
            int expectedOutputLineLength = 0;
            
            for (int i = 0; i < _saveOutputColumnDefinitions.ColumnDefinition.Length; i++)
            {
                expectedOutputLineLength += _saveOutputColumnDefinitions.ColumnDefinition[i].OutputColumnLength;
            }

            if (this.chkShowRowNumber.Checked)
                expectedOutputLineLength += 11;

            if (expectedOutputLineLength > 0)
                this.txtOutputLineLength.Text = expectedOutputLineLength.ToString();
            else
                this.txtOutputLineLength.Text = string.Empty;

        }

        private void RunGenerateTestDataForm()
        {
            TestDataGeneratorForm frm = new TestDataGeneratorForm();

            try
            {
                DisableFormControls();
                this.Cursor = Cursors.WaitCursor;

                frm.SourceDataSchemaFrom = this.cboSourceDataLocation.Text;
                frm.DestinationDataOutputTo = this.cboDestDataLocation.Text;
                frm.showRandomizerForm += new TestDataGeneratorForm.RandomizerFormDelegate(this.ShowRandomizerBuilder);
                frm.runTestDataPreview += new TestDataGeneratorForm.PreviewTestDataDelegate(this.RunTestDataPreview);
                frm.runTestDataGenerator += new TestDataGeneratorForm.GenerateTestDataDelegate(this.RunTestDataGenerator);

                frm.ShowDialog();

                frm.Close();
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
                frm = null;

                EnableFormControls();
                this.Cursor = Cursors.Default;
                this.Focus();
            }
                 
        }

        private void RunTestDataPreview(int numPreviewRows)
        {
            enExtractorDataLocation dataLocation = GetExtractorDataLocation(this.cboSourceDataLocation.Text);
            PFExtractorDefinition exDef = CreateExtractorDefinition();

            try
            {
                if (this.chkEraseOutputLogBeforeEachTest.Checked)
                    Program._messageLog.Clear();

                _appProcessor.PreviewTestData(dataLocation,
                                              exDef,
                                              numPreviewRows,
                                              this.chkShowRowNumber.Checked,
                                              this.chkFilterOutput.Checked,
                                              this.chkRandomizeOutput.Checked,
                                              Properties.Settings.Default.BatchSizeForRandomDataGeneration,
                                              Properties.Settings.Default.BatchSizeForDataImportsAndExports,
                                              Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly,
                                              Properties.Settings.Default.DefaultDataGridExportFolder,
                                              Properties.Settings.Default.DefaultOutputDatabaseType,
                                              Properties.Settings.Default.DefaultOutputDatabaseConnectionString
                                             );
                this.Focus();

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

        private void RunTestDataGenerator(int numRowsToGenerate)
        {
            PFExtractorDefinition exdef = null;

            if (this.chkEraseOutputLogBeforeEachTest.Checked)
                Program._messageLog.Clear();

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                exdef = CreateExtractorDefinition();

                if (Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly)
                {
                    Program._messageLog.Clear();
                }


                _appProcessor.GenerateTestData(exdef,
                                               numRowsToGenerate,
                                               this.chkShowRowNumber.Checked,
                                               this.chkFilterOutput.Checked,
                                               this.chkRandomizeOutput.Checked,
                                               Properties.Settings.Default.BatchSizeForRandomDataGeneration,
                                               Properties.Settings.Default.BatchSizeForDataImportsAndExports
                                              );


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
                this.Focus();
            }

                 
        }

        private void ManageRandomizerSources()
        {
            RandomDataFormsManager frm = new RandomDataFormsManager();
            frm.ShowDialog();

        }

        private void GenerateTestOrderTransactions()
        {
            RandomOrdersGeneratorForm frm = new RandomOrdersGeneratorForm();

            if (DestinationRelDbOrAccessDefined() == false)
            {
                _msg.Length = 0;
                _msg.Append("Destination location must be either Relational Database or MS Access Datatabase.");
                _msg.Append(Environment.NewLine);
                _msg.Append(this.cboDestDataLocation.Text);
                _msg.Append(" not allowed for test random orders generator.");
                AppMessages.DisplayErrorMessage(_msg.ToString());
                return;
            }

            if (this.cboDestDataLocation.Text == "Relational Database")
            {
                if (this.cboDataDestination.Text.Trim() == string.Empty
                    || this.txtDestinationConnectionString.Text.Trim() == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Destination source and connection string must be specified for a relational database destination.");
                    if (this.cboDataDestination.Text.Trim() == string.Empty)
                    {
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Destination source has not been specified.");
                    }
                    if (this.txtDestinationConnectionString.Text.Trim() == string.Empty)
                    {
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Destination connection string has not been specified.");
                    }
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
            }

            if (this.cboDestDataLocation.Text == "Access Database File")
            {
                if (this.txtDestAccessDatabaseFilePath.Text.Trim() == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("Destination file path must be specified for a Access database file destination.");
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
            }

            try
            {
                frm.MessageLogUI = Program._messageLog;
                frm.EraseLogBeforeExtracting = this.chkEraseOutputLogBeforeEachTest.Checked;
                _randomOrdersDefinition.OutputDatabaseLocation = this.cboDestDataLocation.Text;
                if (this.cboDestDataLocation.Text == "Relational Database")
                {
                    _randomOrdersDefinition.OutputDatabasePlatform = this.cboDataDestination.Text;
                    _randomOrdersDefinition.OutputDatabaseConnection = this.txtDestinationConnectionString.Text;
                    _randomOrdersDefinition.AccessOutputUsername = string.Empty;
                    _randomOrdersDefinition.AccessOutputPassword = string.Empty;
                    _randomOrdersDefinition.ReplaceExistingAccessFile = false;
                    _randomOrdersDefinition.DatabaseOutputBatchSize = AppTextGlobals.ConvertStringToInt(this.txtOutputBatchSize.Text, 1);
                }
                if (this.cboDestDataLocation.Text == "Access Database File")
                {
                    _randomOrdersDefinition.OutputDatabasePlatform = this.optDestAccess2007.Checked ? AccessVersion.Access2007.ToString() : AccessVersion.Access2003.ToString();
                    _randomOrdersDefinition.OutputDatabaseConnection = this.txtDestAccessDatabaseFilePath.Text;
                    _randomOrdersDefinition.AccessOutputUsername = this.txtDestAccessUsername.Text;
                    _randomOrdersDefinition.AccessOutputPassword = this.txtDestAccessPassword.Text;
                    _randomOrdersDefinition.ReplaceExistingAccessFile = this.chkReplaceExistingAccessDatabaseFile.Checked;
                    _randomOrdersDefinition.DatabaseOutputBatchSize = 1;
                }

                frm.RandomOrderDefinition = _randomOrdersDefinition;
                frm.GenerateSalesOrderTransactions += new RandomOrdersGeneratorForm.GenerateSalesOrderTransactionsDelegate(this.GenerateSalesOrderTransactions);
                frm.GeneratePurchaseOrderTransactions += new RandomOrdersGeneratorForm.GeneratePurchaseOrderTransactionsDelegate(this.GeneratePurchaseOrderTransactions);
                frm.GenerateInternetSalesTransactions += new RandomOrdersGeneratorForm.GenerateInternetSalesTransactionsDelegate(this.GenerateInternetSalesTransactions);
                frm.GenerateResellerSalesTransactions += new RandomOrdersGeneratorForm.GenerateResellerSalesTransactionsDelegate(this.GenerateResellerSalesTransactions);
                frm.SaveExtractorDefinition += new RandomOrdersGeneratorForm.SaveExtractorDefinitionDelegate(this.FileSave);

                frm.ShowDialog();

                frm.Close();

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
                frm = null;
            }
        
        }

        private bool DestinationRelDbOrAccessDefined()
        {
            if (this.cboDestDataLocation.Text == "Relational Database"
                || this.cboDestDataLocation.Text == "Access Database File")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GenerateSalesOrderTransactions(int maxTxsToGenerate, StringBuilder generatorMessages)
        {
            _appProcessor.GenerateSalesOrderTransactions(maxTxsToGenerate, 
                                                         _randomOrdersDefinition,
                                                         generatorMessages,
                                                         Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly,
                                                         Properties.Settings.Default.DefaultDataGridExportFolder,
                                                         Properties.Settings.Default.DefaultOutputDatabaseType,
                                                         Properties.Settings.Default.DefaultOutputDatabaseConnectionString);
        }

        private void GeneratePurchaseOrderTransactions(int maxTxsToGenerate, StringBuilder generatorMessages)
        {
            _appProcessor.GeneratePurchaseOrderTransactions(maxTxsToGenerate,
                                                           _randomOrdersDefinition,
                                                           generatorMessages,
                                                           Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly,
                                                           Properties.Settings.Default.DefaultDataGridExportFolder,
                                                           Properties.Settings.Default.DefaultOutputDatabaseType,
                                                           Properties.Settings.Default.DefaultOutputDatabaseConnectionString);
        }

        private void GenerateInternetSalesTransactions(int maxTxsToGenerate, StringBuilder generatorMessages)
        {
            _appProcessor.GenerateInternetSalesTransactions(maxTxsToGenerate,
                                                            _randomOrdersDefinition,
                                                            generatorMessages,
                                                            Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly,
                                                            Properties.Settings.Default.DefaultDataGridExportFolder,
                                                            Properties.Settings.Default.DefaultOutputDatabaseType,
                                                            Properties.Settings.Default.DefaultOutputDatabaseConnectionString);
        }

        private void GenerateResellerSalesTransactions(int maxTxsToGenerate, StringBuilder generatorMessages)
        {
            _appProcessor.GenerateResellerSalesTransactions(maxTxsToGenerate,
                                                            _randomOrdersDefinition,
                                                            generatorMessages,
                                                            Properties.Settings.Default.ShowInstalledDatabaseProvidersOnly,
                                                            Properties.Settings.Default.DefaultDataGridExportFolder,
                                                            Properties.Settings.Default.DefaultOutputDatabaseType,
                                                            Properties.Settings.Default.DefaultOutputDatabaseConnectionString);
        }


        //temporary code
        private void cmdGenProps_Click(object sender, EventArgs e)
        {
            GenPropsList();
        }

        private void GenPropsList()
        {
            Regex r = new Regex(
                                @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])"
                               );

            Program._messageLog.Clear();

            _msg.Length = 0;
            _msg.Append("User Setting\tDescription/Notes\tValid Values\tDefaul tValue");
            Program._messageLog.WriteLine(_msg.ToString());

            foreach (System.Configuration.SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
                _msg.Length = 0;
                _msg.Append(r.Replace(currentProperty.Name," "));
                _msg.Append("\t\t\t");
                _msg.Append(currentProperty.DefaultValue);
                Program._messageLog.WriteLine(_msg.ToString());
            }
        }

        //end temporary code

    }//end class
#pragma warning restore 1591
}//end namespace
