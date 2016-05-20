using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;
using PFMessageLogs;
using PFFileSystemObjects;
using PFTextFiles;
using PFTextObjects;
using PFPrinterObjects;
using PFAppDataObjects;
using PFRandomDataProcessor;
using PFRandomValueDataTables;
using PFRandomDataExt;
using KellermanSoftware.CompareNetObjects;
using PFDataAccessObjects;
using PFCollectionsObjects;
using PFSQLServerCE35Objects;
using PFEncryptionObjects;
using PFRandomDataListProcessor;

namespace PFRandomDataForms
{
#pragma warning disable 1591
    /// <summary>
    /// Form for definition and preview of random names and addresses.
    /// </summary>
    public partial class RandomNamesAndLocationsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        private string xlatkey = @"Ov$S=9hT?ONeU],`";
        private string xlativ = @",x033lcI]*O*Y/O0";

        private bool _processItemCheckEvents = false;
        //PFList<PFTableDef> _tableList = null;
        PFSQLServerCE35 _db = new PFSQLServerCE35();


        private string _defaultRandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
        private string _randomNamesAndLocationsDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Definitions\NamesAndLocations\";
        private string _randomNamesAndLocationsOriginalDataRequestsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\OriginalDefinitions\NamesAndLocations\";

        private string _defaultRandomDataDatabase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\RandomData.sdf";
        private string _defaultRandomDataDatabasePassword = @"+d@t@p*^t$1956=";

        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\NamesAndLocations\";
        private string _defaultDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\NamesAndLocations\";
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private bool _showInstalledDatabaseProvidersOnly = true;
        private int _numberOfRandomValueSamples = 100;

        private string _nameListsFolder = string.Empty;
        private string _nameListName = string.Empty;
        private string _customListsFolder = string.Empty;
        private string _customListName = string.Empty;

        private string _sqlQueryForRegionNames = "select distinct RegionName from RegionCodesUS <where> order by RegionName";
        private string _sqlQueryForSubRegionNames = "select distinct SubRegionName from RegionCodesUS <where> order by SubRegionName";
        private string _sqlQueryForStateNames = "select s.StateName, s.StateCode, r.RegionName, r.SubRegionName  from StateCodesUS s join RegionCodesUS r on r.StateCode = s.StateCode <where>  order by s.StateName";
        private string _sqlQueryForMsaNames = "select msa.Code, msa.StateCode, st.StateName, msa.CityName, msa.TotalPopulation from MSA_Codes msa join StateCodesUS st on st.StateCode = msa.StateCode <where> order by TotalPopulation desc, StateName, CityName";
        private string _sqlQueryForCMsaNames = "select cmsa.Code, cmsa.StateCode, st.StateName, cmsa.CityName, cmsa.TotalPopulation from CMSA_Codes cmsa join StateCodesUS st on st.StateCode = cmsa.StateCode <where> order by TotalPopulation desc, StateName, CityName";
        private string _sqlQueryForAgeGroupNames = "select MinAge, MaxAge from AgeGroupRanges <where> order by MinAge";

        private string _sqlQueryForCanRegionNames = "select distinct RegionName from RegionCodesCanada <where> order by RegionName";
        private string _sqlQueryForCanSubRegionNames = "select distinct SubRegionName from RegionCodesCanada <where> order by SubRegionName";
        private string _sqlQueryForCanProvinceNames = "select p.ProvinceName, p.ProvinceCode, r.RegionName, r.SubRegionName from ProvinceCodesCanada p join RegionCodesCanada r on r.ProvinceCode = p.ProvinceCode <where> order by ProvinceName";
        private string _sqlQueryForCanAreaCodes = "select a.ProvinceCode, p.ProvinceName, a.AreaName, a.Code as AreaCode, a.Frequency from Area_Codes_Canada a join ProvinceCodesCanada p on p.ProvinceCode = a.ProvinceCode <where> order by Frequency desc";
        private string _sqlQueryForCanLargeCities = "select lg.ProvinceCode, p.ProvinceName, lg.CityName, lg.Frequency, lg.AdjustedFrequency, lg.AdjustmentNumber from LargeCities_Canada lg join ProvinceCodesCanada p on lg.ProvinceCode = p.ProvinceCode <where> order by lg.Frequency desc";

        private string _sqlQueryForMexRegionNames = "select distinct RegionName from RegionCodesMexico <where> order by RegionName";
        private string _sqlQueryForMexSubRegionNames = "select distinct SubRegionName from RegionCodesMexico <where> order by SubRegionName";
        private string _sqlQueryForMexStateNames = "select s.StateName, s.StateCode, r.RegionName, r.SubRegionName from StateCodesMexico s join RegionCodesMexico r on r.StateCode = s.StateCode <where> order by s.StateName";
        private string _sqlQueryForMexAreaCodes = "select a.StateCode, st.StateName, a.AreaName,a.Code as AreaCode, a.Frequency from Area_Codes_Mexico a join StateCodesMexico st on st.StateCode = a.StateCode <where> order by Frequency desc";
        private string _sqlQueryForMexLargeCities = "select lg.StateCode, st.StateName, lg.CityName, lg.Frequency, lg.AdjustedFrequency, lg.AdjustmentNumber from LargeCities_Mexico lg join StateCodesMexico st on lg.StateCode = st.StateCode <where> order by lg.Frequency desc";



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
        private DateTime _savedBaseDateTime = DateTime.MinValue;
        private RandomNamesAndLocationsDataRequest _saveRequestDef = null;
        private RandomNamesAndLocationsDataRequest _exitRequestDef = null;


        //constructors

        public RandomNamesAndLocationsForm()
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

        private void cmdRefreshCountrySelectionPercentages_Click(object sender, EventArgs e)
        {
            RefreshCountrySelectionPercentages();
        }

        private void cmdRefreshNameSelectionPercentages_Click(object sender, EventArgs e)
        {
            RefreshNameSelectionPercentages();
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

        //form field clicks

        private void CountryToUseCheckChanged(object sender, EventArgs e)
        {
            CountryToUse_CheckedChanged();
        }

        private void chklistRegionsUS_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistRegionsUS.SetItemCheckState(e.Index, e.NewValue);
                    USRegionChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistSubRegionsUS_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistSubRegionsUS.SetItemCheckState(e.Index, e.NewValue);
                    USSubRegionChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistStateNamesUS_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistStateNamesUS.SetItemCheckState(e.Index, e.NewValue);
                    USStateChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistRegionsCAN_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistRegionsCAN.SetItemCheckState(e.Index, e.NewValue);
                    CANRegionChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistSubRegionsCAN_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistSubRegionsCAN.SetItemCheckState(e.Index, e.NewValue);
                    CANSubRegionChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistProvincesCAN_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistProvincesCAN.SetItemCheckState(e.Index, e.NewValue);
                    CANProvinceChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistRegionsMEX_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistRegionsMEX.SetItemCheckState(e.Index, e.NewValue);
                    MEXRegionChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistSubRegionsMEX_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistSubRegionsMEX.SetItemCheckState(e.Index, e.NewValue);
                    MEXSubRegionChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void chklistStatesMEX_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_processItemCheckEvents)
            {
                try
                {
                    _processItemCheckEvents = false;
                    this.chklistStatesMEX.SetItemCheckState(e.Index, e.NewValue);
                    MEXStateChangeResync();
                }
                catch (System.Exception ex)
                {
                    DisplaySystemExceptionMessage(ex);
                }
                finally
                {
                    _processItemCheckEvents = true;
                }
            }
        }

        private void cmdSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                _processItemCheckEvents = false;
                Button btn = (Button)sender;
                switch (btn.Name)
                {
                    case "cmdRegionsUSSelectAll":
                        CheckedListBox_SelectAll(this.chklistRegionsUS);
                        USRegionChangeResync();
                        break;
                    case "cmdSubRegionsUSSelectAll":
                        CheckedListBox_SelectAll(this.chklistSubRegionsUS);
                        USSubRegionChangeResync();
                        break;
                    case "cmdStatesUSSelectAll":
                        CheckedListBox_SelectAll(this.chklistStateNamesUS);
                        USStateChangeResync();
                        break;
                    case "cmdAgeGroupsUSSelectAll":
                        CheckedListBox_SelectAll(this.chklistAgeGroupsUS);
                        break;
                    case "cmdMsaUSSelectAll":
                        CheckedListBox_SelectAll(this.chklistMsaUS);
                        break;
                    case "cmdCMsaUSSelectAll":
                        CheckedListBox_SelectAll(this.chklistCMsaUS);
                        break;

                    case "cmdRegionsCANSelectAll":
                        CheckedListBox_SelectAll(this.chklistRegionsCAN);
                        CANRegionChangeResync();
                        break;
                    case "cmdSubRegionsCANSelectAll":
                        CheckedListBox_SelectAll(this.chklistSubRegionsCAN);
                        CANSubRegionChangeResync();
                        break;
                    case "cmdProvincesCANSelectAll":
                        CheckedListBox_SelectAll(this.chklistProvincesCAN);
                        CANProvinceChangeResync();
                        break;
                    case "cmdLargeCitiesCANSelectAll":
                        CheckedListBox_SelectAll(this.chklistLargeCitiesCAN);
                        break;

                    case "cmdRegionsMEXSelectAll":
                        CheckedListBox_SelectAll(this.chklistRegionsMEX);
                        MEXRegionChangeResync();
                        break;
                    case "cmdSubRegionsMEXSelectAll":
                        CheckedListBox_SelectAll(this.chklistSubRegionsMEX);
                        MEXSubRegionChangeResync();
                        break;
                    case "cmdStatesMEXSelectAll":
                        CheckedListBox_SelectAll(this.chklistStatesMEX);
                        MEXStateChangeResync();
                        break;
                    case "cmdAreaCodesMEXSelectAll":
                        CheckedListBox_SelectAll(this.chklistAreaCodesMEX);
                        break;
                    case "cmdLargeCitiesMEXSelectAll":
                        CheckedListBox_SelectAll(this.chklistLargeCitiesMEX);
                        break;

                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                DisplaySystemExceptionMessage(ex);
            }
            finally
            {
                _processItemCheckEvents = true;
            }
        }

        private void cmdSelectNone_Click(object sender, EventArgs e)
        {
            try
            {
                _processItemCheckEvents = false;
                Button btn = (Button)sender;
                switch (btn.Name)
                {
                    case "cmdRegionsUSSelectNone":
                        CheckedListBox_SelectNone(this.chklistRegionsUS);
                        USRegionChangeResync();
                        break;
                    case "cmdSubRegionsUSSelectNone":
                        CheckedListBox_SelectNone(this.chklistSubRegionsUS);
                        USSubRegionChangeResync();
                        break;
                    case "cmdStatesUSSelectNone":
                        CheckedListBox_SelectNone(this.chklistStateNamesUS);
                        USStateChangeResync();
                        break;
                    case "cmdAgeGroupsUSSelectNone":
                        CheckedListBox_SelectNone(this.chklistAgeGroupsUS);
                        break;
                    case "cmdMsaUSSelectNone":
                        CheckedListBox_SelectNone(this.chklistMsaUS);
                        break;
                    case "cmdCMsaUSSelectNone":
                        CheckedListBox_SelectNone(this.chklistCMsaUS);
                        break;

                    case "cmdRegionsCANSelectNone":
                        CheckedListBox_SelectNone(this.chklistRegionsCAN);
                        CANRegionChangeResync();
                        break;
                    case "cmdSubRegionsCANSelectNone":
                        CheckedListBox_SelectNone(this.chklistSubRegionsCAN);
                        CANSubRegionChangeResync();
                        break;
                    case "cmdProvincesCANSelectNone":
                        CheckedListBox_SelectNone(this.chklistProvincesCAN);
                        CANProvinceChangeResync();
                        break;
                    case "cmdAreaCodesCANSelectNone":
                        CheckedListBox_SelectNone(this.chklistAreaCodesCAN);
                        break;
                    case "cmdLargeCitiesCANSelectNone":
                        CheckedListBox_SelectNone(this.chklistLargeCitiesCAN);
                        break;

                    case "cmdRegionsMEXSelectNone":
                        CheckedListBox_SelectNone(this.chklistRegionsMEX);
                        MEXRegionChangeResync();
                        break;
                    case "cmdSubRegionsMEXSelectNone":
                        CheckedListBox_SelectNone(this.chklistSubRegionsMEX);
                        MEXSubRegionChangeResync();
                        break;
                    case "cmdStatesMEXSelectNone":
                        CheckedListBox_SelectNone(this.chklistStatesMEX);
                        MEXStateChangeResync();
                        break;
                    case "cmdAreaCodesMEXSelectNone":
                        CheckedListBox_SelectNone(this.chklistAreaCodesMEX);
                        break;
                    case "cmdLargeCitiesMEXSelectNone":
                        CheckedListBox_SelectNone(this.chklistLargeCitiesMEX);
                        break;


                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                DisplaySystemExceptionMessage(ex);
            }
            finally
            {
                _processItemCheckEvents = true;
            }
        }


        private void DisplaySystemExceptionMessage(System.Exception ex)
        {
            _msg.Length = 0;
            _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
            WriteToMessageLog(_msg.ToString());
            AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
        }

        private void RefreshNameSelectionPercentages()
        {
            if (this.chkIncludePersonalNames.Checked == false)
            {
                this.txtPercentFrequencyPersonalNames.Text = string.Empty;
                this.txtPercentFrequencyMaleNames.Text = string.Empty;
                this.txtPercentFrequencyFemaleNames.Text = string.Empty;
                if (this.chkIncludeBusinessNames.Checked)
                    this.txtPercentFrequencyBusinessNames.Text = "100";
            }
            if (this.chkIncludeBusinessNames.Checked == false)
            {
                this.txtPercentFrequencyBusinessNames.Text = string.Empty;
                if (this.chkIncludePersonalNames.Checked)
                    this.txtPercentFrequencyPersonalNames.Text = "100";
            }
            if (this.chkIncludePersonalNames.Checked && this.chkIncludeBusinessNames.Checked)
            {
                if (this.txtPercentFrequencyPersonalNames.Text == string.Empty || this.txtPercentFrequencyBusinessNames.Text == string.Empty)
                {
                    this.txtPercentFrequencyPersonalNames.Text = "50";
                    this.txtPercentFrequencyBusinessNames.Text = "50";
                }
                else if (this.txtPercentFrequencyPersonalNames.Text == "0" || this.txtPercentFrequencyBusinessNames.Text == "0")
                {
                    this.txtPercentFrequencyPersonalNames.Text = "50";
                    this.txtPercentFrequencyBusinessNames.Text = "50";
                }
                else
                {
                    double personFreq = Convert.ToDouble(this.txtPercentFrequencyPersonalNames.Text);
                    double businessFreq = Convert.ToDouble(this.txtPercentFrequencyBusinessNames.Text);
                    double totFreq = personFreq + businessFreq;
                    int personPct = Convert.ToInt32((personFreq / totFreq) * 100.0);
                    int businessPct = 100 - personPct;
                    this.txtPercentFrequencyPersonalNames.Text = personPct.ToString("#0");
                    this.txtPercentFrequencyBusinessNames.Text = businessPct.ToString("#0");
                }
            }
            if (this.chkIncludePersonalNames.Checked == true)
            {
                if (this.txtPercentFrequencyMaleNames.Text == string.Empty && this.txtPercentFrequencyFemaleNames.Text == string.Empty)
                {
                    this.txtPercentFrequencyMaleNames.Text = "50";
                    this.txtPercentFrequencyFemaleNames.Text = "50";
                }
                else if (this.txtPercentFrequencyMaleNames.Text == "0" && this.txtPercentFrequencyFemaleNames.Text == "0")
                {
                    this.txtPercentFrequencyMaleNames.Text = "50";
                    this.txtPercentFrequencyFemaleNames.Text = "50";
                }
                else if (this.txtPercentFrequencyMaleNames.Text != string.Empty && this.txtPercentFrequencyFemaleNames.Text == string.Empty)
                {
                    this.txtPercentFrequencyMaleNames.Text = "100";
                    this.txtPercentFrequencyFemaleNames.Text = string.Empty;
                }
                else if (this.txtPercentFrequencyMaleNames.Text == string.Empty && this.txtPercentFrequencyFemaleNames.Text != string.Empty)
                {
                    this.txtPercentFrequencyMaleNames.Text = string.Empty;
                    this.txtPercentFrequencyFemaleNames.Text = "100";
                }
                else  //both male and female personName frequencies are filled in
                {
                    double maleFreq = Convert.ToDouble(this.txtPercentFrequencyMaleNames.Text);
                    double femaleFreq = Convert.ToDouble(this.txtPercentFrequencyFemaleNames.Text);
                    double totFreq = maleFreq + femaleFreq;
                    int malePct = Convert.ToInt32((maleFreq / totFreq) * 100.0);
                    int femalePct = 100 - malePct;
                    this.txtPercentFrequencyMaleNames.Text = malePct.ToString("#0");
                    this.txtPercentFrequencyFemaleNames.Text = femalePct.ToString("#0");
                }
            }//end if(this.chkIncludePersonalNames.Checked == true)
        }//end method

        private void RefreshCountrySelectionPercentages()
        {
            double usFreq = 0.0;
            double canFreq = 0.0;
            double mexFreq = 0.0;
            double totFreq = 0.0;
            double freq = 0.0;

            usFreq = AppTextGlobals.ConvertStringToDouble(this.txtPercentFrequencyUnitedStates.Text, 1.0);
            canFreq = AppTextGlobals.ConvertStringToDouble(this.txtPercentFrequencyCanada.Text, 1.0);
            mexFreq = AppTextGlobals.ConvertStringToDouble(this.txtPercentFrequencyMexico.Text, 1.0);

            if (this.chkUSRandomData.Checked)
                totFreq += usFreq;
            if (this.chkCanadaRandomData.Checked)
                totFreq += canFreq;
            if (this.chkMexicoRandomData.Checked)
                totFreq += mexFreq;

            if(totFreq > 0.0)
            {
                if (this.chkUSRandomData.Checked)
                {
                    freq = (usFreq / totFreq) * 100.0;
                    this.txtPercentFrequencyUnitedStates.Text = freq.ToString("0");
                }
                else
                {
                    this.txtPercentFrequencyUnitedStates.Text = string.Empty;
                }
                if (this.chkCanadaRandomData.Checked)
                {
                    freq = (canFreq / totFreq) * 100.0;
                    this.txtPercentFrequencyCanada.Text = freq.ToString("0");
                }
                else
                {
                    this.txtPercentFrequencyCanada.Text = string.Empty;
                }
                if (this.chkMexicoRandomData.Checked)
                {
                    freq = (mexFreq / totFreq) * 100.0;
                    this.txtPercentFrequencyMexico.Text = freq.ToString("0");
                }
                else
                {
                    this.txtPercentFrequencyMexico.Text = string.Empty;
                }
            }
            else
            {
                this.chkUSRandomData.Checked = true;
                this.txtPercentFrequencyUnitedStates.Text = "75";
                this.chkCanadaRandomData.Checked = true;
                this.txtPercentFrequencyCanada.Text = "10";
                this.chkMexicoRandomData.Checked = true;
                this.txtPercentFrequencyMexico.Text = "15";
            }
            
            this.Refresh();

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
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Random Names And Locations");
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

            _exitRequestDef = CreateRequestDef(false, true);

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

                InitSqcCeDatabaseConnection();

                SetFormValues();

                _printer = new FormPrinter(this);

                _saveRequestDef = CreateRequestDef(false, true);
                _savedBaseDateTime = DateTime.MinValue;

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
            _saveMruListToRegistry = AppConfig.GetBooleanValueFromConfigFile("SaveMruListToRegistry_RandomNamesAndLocationsForm", "True");
            _mRUListSaveFileSubFolder = AppConfig.GetStringValueFromConfigFile("MRUListSaveFileSubFolder_RandomNamesAndLocationsForm", @"PFApps\RandomNamesAndLocationsForm\");
            _mRUListSaveRegistryKey = AppConfig.GetStringValueFromConfigFile("MRUListSaveRegistryKey_RandomNamesAndLocationsForm", @"SOFTWARE\PFApps\RandomNamesAndLocationsForm");
            _maxMruListEntries = AppConfig.GetIntValueFromConfigFile("MaxMruListEntries_RandomNamesAndLocationsForm", (int)6);
            _useSubMenuForMruList = AppConfig.GetBooleanValueFromConfigFile("UseSubMenuForMruList_RandomNamesAndLocationsForm", "true");

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
            string randomNamesAndLocationsDataRequestFolder = string.Empty;
            string randomNamesAndLocationsOriginalDataRequestFolder = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("RandomDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomDataRequestFolder = configValue;
            else
                randomDataRequestFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            configValue = AppConfig.GetStringValueFromConfigFile("RandomNamesAndLocationsDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomNamesAndLocationsDataRequestFolder = configValue;
            else
                randomNamesAndLocationsDataRequestFolder = @"\PFApps\Randomizer\Definitions\NamesAndLocations\";

            configValue = AppConfig.GetStringValueFromConfigFile("RandomNamesAndLocationsOriginalDataRequestFolder", string.Empty);
            if (configValue.Length > 0)
                randomNamesAndLocationsOriginalDataRequestFolder = configValue;
            else
                randomNamesAndLocationsOriginalDataRequestFolder = @"\PFApps\Randomizer\OriginalDefinitions\NamesAndLocations\";

            _randomNamesAndLocationsDataRequestFolder = randomDataRequestFolder + randomNamesAndLocationsDataRequestFolder;
            _randomNamesAndLocationsOriginalDataRequestsFolder = randomDataRequestFolder + randomNamesAndLocationsOriginalDataRequestFolder;

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
        private void InitSqcCeDatabaseConnection()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultRandomDataDatabase", string.Empty);
            if (configValue.Trim().Length != 0)
                _defaultRandomDataDatabase = configValue;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultRandomDataDatabasePassword", string.Empty);
            if (configValue.Trim().Length != 0)
            {
                PFStringEncryptor enc = new PFStringEncryptor(pfEncryptionAlgorithm.AES);
                enc.Key = xlatkey;
                enc.IV = xlativ;
                string decryptedString = enc.Decrypt(configValue);
                _defaultRandomDataDatabasePassword = decryptedString;
            }

            _db.DatabasePath = _defaultRandomDataDatabase;
            _db.DatabasePassword = _defaultRandomDataDatabasePassword;
            _db.OpenConnection();

            _msg.Length = 0;
            if (_db.IsConnected)
            {
                _msg.Append("Connection to SQLCE random data database successful.");
                _msg.Append(Environment.NewLine);
                WriteToMessageLog(_msg.ToString());
            }
            else
            {
                _msg.Append("ERROR: Connection to SQLCE database failed.");
                _msg.Append(Environment.NewLine);
                _msg.Append(_defaultRandomDataDatabase);
                _msg.Append(Environment.NewLine);
                throw new System.Exception(_msg.ToString());
            }
        }



        private void SetFormValues()
        {
            try
            {
                this.Text = "Define Random Name And Locations Data Mask";
                this.txtDataMaskName.Text = string.Empty;
                this.txtCurrentYear.Text = DateTime.Now.Year.ToString();

                _processItemCheckEvents = false;

                this.chkUSRandomData.Checked = true;
                this.chkCanadaRandomData.Checked = true;
                this.chkMexicoRandomData.Checked = true;
                this.txtPercentFrequencyUnitedStates.Text = "75";
                this.txtPercentFrequencyCanada.Text = "10";
                this.txtPercentFrequencyMexico.Text = "15";
                CountryToUse_CheckedChanged();



                this.chkIncludePersonalNames.Checked = true;
                this.txtPercentFrequencyPersonalNames.Text = "50";
                this.txtPercentFrequencyMaleNames.Text = "50";
                this.txtPercentFrequencyFemaleNames.Text = "50";
                this.chkIncludeBusinessNames.Checked = true;
                this.txtPercentFrequencyBusinessNames.Text = "50";

                this.chkAddressLine1US.Checked = true;
                this.chkAddressLine2US.Checked = true;
                this.txtPercentFrequencyAddressLine2.Text = "50";
                this.chkCityStateZipUS.Checked = true;
                this.chkAreaCode.Checked = true;

                this.chkNationalId.Checked = true;
                this.chkTelephoneNumber.Checked = true;
                this.chkEmailAddress.Checked = true;

                this.chkGenderUS.Checked = true;
                this.chkBirthDateUS.Checked = true;

                this.txtNumRandomDataItems.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");

                InitUSTab();
                InitCanadaTab();
                InitMexicoTab();

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
                _processItemCheckEvents = true;
            }

        }

        private void InitUSTab()
        {
            FillRegionsUS(string.Empty);
            FillSubRegionsUS(string.Empty);
            FillStatesUS(string.Empty);
            FillMsaUS(string.Empty);
            FillCMsaUS(string.Empty);
            FillAgeGroupsUS(string.Empty);
        }

        private void FillRegionsUS(string sqlWhereClase)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForRegionNames.Replace("<where>", sqlWhereClase), CommandType.Text);
            this.chklistRegionsUS.Items.Clear();
            while (rdr.Read())
            {
                this.chklistRegionsUS.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistRegionsUS.Items.Count; i++)
            {
                chklistRegionsUS.SetItemChecked(i, true);
            }

        }

        private void FillSubRegionsUS(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForSubRegionNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistSubRegionsUS.Items.Clear();
            while (rdr.Read())
            {
                this.chklistSubRegionsUS.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistSubRegionsUS.Items.Count; i++)
            {
                chklistSubRegionsUS.SetItemChecked(i, true);
            }

        }

        private void FillStatesUS(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForStateNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistStateNamesUS.Items.Clear();
            while (rdr.Read())
            {
                this.chklistStateNamesUS.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistStateNamesUS.Items.Count; i++)
            {
                chklistStateNamesUS.SetItemChecked(i, true);
            }

        }

        private void FillMsaUS(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForMsaNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistMsaUS.Items.Clear();
            while (rdr.Read())
            {
                string item = rdr[0].ToString() + ": " + rdr[2].ToString().Trim() + ": " + rdr[3].ToString().Trim() + " ~" + (Convert.ToDouble(rdr[4].ToString()) / (double)1000000.0).ToString("#0.000");
                this.chklistMsaUS.Items.Add(item);
            }
            rdr.Close();
            for (int i = 0; i < this.chklistMsaUS.Items.Count; i++)
            {
                chklistMsaUS.SetItemChecked(i, false);
            }
        }

        private void FillCMsaUS(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForCMsaNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistCMsaUS.Items.Clear();
            while (rdr.Read())
            {
                string item = rdr[0].ToString() + ": " + rdr[2].ToString().Trim() + ": " + rdr[3].ToString().Trim() + " ~" + (Convert.ToDouble(rdr[4].ToString()) / (double)1000000.0).ToString("#0.000");
                this.chklistCMsaUS.Items.Add(item);
            }
            rdr.Close();
            for (int i = 0; i < this.chklistCMsaUS.Items.Count; i++)
            {
                chklistCMsaUS.SetItemChecked(i, false);
            }

        }

        private void FillAgeGroupsUS(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForAgeGroupNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistAgeGroupsUS.Items.Clear();
            while (rdr.Read())
            {
                string item = rdr[0].ToString().Trim() + " - " + rdr[1].ToString().Trim();
                this.chklistAgeGroupsUS.Items.Add(item);
            }
            rdr.Close();
            for (int i = 0; i < this.chklistAgeGroupsUS.Items.Count; i++)
            {
                if (i < 3)
                    chklistAgeGroupsUS.SetItemChecked(i, false);
                else
                    chklistAgeGroupsUS.SetItemChecked(i, true);
            }

        }

        private void InitCanadaTab()
        {
            FillRegionsCAN(string.Empty);
            FillSubRegionsCAN(string.Empty);
            FillProvincesCAN(string.Empty);
            FillAreaCodesCAN(string.Empty);
            FillLargeCitiesCAN(string.Empty);
        }//end method

        private void FillRegionsCAN(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForCanRegionNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistRegionsCAN.Items.Clear();
            while (rdr.Read())
            {
                this.chklistRegionsCAN.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistRegionsCAN.Items.Count; i++)
            {
                chklistRegionsCAN.SetItemChecked(i, true);
            }
        }

        private void FillSubRegionsCAN(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForCanSubRegionNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistSubRegionsCAN.Items.Clear();
            while (rdr.Read())
            {
                this.chklistSubRegionsCAN.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistSubRegionsCAN.Items.Count; i++)
            {
                chklistSubRegionsCAN.SetItemChecked(i, true);
            }
        }

        private void FillProvincesCAN(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForCanProvinceNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistProvincesCAN.Items.Clear();
            while (rdr.Read())
            {
                this.chklistProvincesCAN.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistProvincesCAN.Items.Count; i++)
            {
                chklistProvincesCAN.SetItemChecked(i, true);
            }
        }

        private void FillAreaCodesCAN(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForCanAreaCodes.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistAreaCodesCAN.Items.Clear();
            while (rdr.Read())
            {
                string item = rdr[3].ToString() + " - " + rdr[1].ToString().Trim() + ": " + rdr[2].ToString().Trim() + " ~" + (Convert.ToDouble(rdr[4].ToString()) / (double)1000000.0).ToString("#0.000");
                this.chklistAreaCodesCAN.Items.Add(item);
            }
            rdr.Close();
            for (int i = 0; i < this.chklistAreaCodesCAN.Items.Count; i++)
            {
                chklistAreaCodesCAN.SetItemChecked(i, false);
            }

        }

        private void FillLargeCitiesCAN(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForCanLargeCities.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistLargeCitiesCAN.Items.Clear();
            while (rdr.Read())
            {
                string item = rdr[1].ToString().Trim() + ": " + rdr[2].ToString().Trim() + " ~" + (Convert.ToDouble(rdr[3].ToString()) / (double)1000000.0).ToString("#0.000");
                this.chklistLargeCitiesCAN.Items.Add(item);
            }
            rdr.Close();
            for (int i = 0; i < this.chklistLargeCitiesCAN.Items.Count; i++)
            {
                chklistLargeCitiesCAN.SetItemChecked(i, false);
            }
        }

        private void InitMexicoTab()
        {

            FillRegionsMEX(string.Empty);
            FillSubRegionsMEX(string.Empty);
            FillStatesMEX(string.Empty);
            FillAreaCodesMEX(string.Empty);
            FillLargeCitiesMEX(string.Empty);
        }

        private void FillRegionsMEX(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForMexRegionNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistRegionsMEX.Items.Clear();
            while (rdr.Read())
            {
                this.chklistRegionsMEX.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistRegionsMEX.Items.Count; i++)
            {
                chklistRegionsMEX.SetItemChecked(i, true);
            }
        }

        private void FillSubRegionsMEX(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForMexSubRegionNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistSubRegionsMEX.Items.Clear();
            while (rdr.Read())
            {
                this.chklistSubRegionsMEX.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistSubRegionsMEX.Items.Count; i++)
            {
                chklistSubRegionsMEX.SetItemChecked(i, true);
            }
        }

        private void FillStatesMEX(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForMexStateNames.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistStatesMEX.Items.Clear();
            while (rdr.Read())
            {
                this.chklistStatesMEX.Items.Add(rdr[0].ToString());
            }
            rdr.Close();
            for (int i = 0; i < this.chklistStatesMEX.Items.Count; i++)
            {
                chklistStatesMEX.SetItemChecked(i, true);
            }
        }

        private void FillAreaCodesMEX(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForMexAreaCodes.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistAreaCodesMEX.Items.Clear();
            while (rdr.Read())
            {
                string item = rdr[3].ToString() + " - " + rdr[1].ToString().Trim() + ": " + rdr[2].ToString().Trim() + " ~" + (Convert.ToDouble(rdr[4].ToString()) / (double)1000000.0).ToString("#0.000");
                this.chklistAreaCodesMEX.Items.Add(item);
            }
            rdr.Close();
            for (int i = 0; i < this.chklistAreaCodesMEX.Items.Count; i++)
            {
                chklistAreaCodesMEX.SetItemChecked(i, false);
            }
        }

        private void FillLargeCitiesMEX(string sqlWhereClause)
        {
            DbDataReader rdr = _db.RunQueryDataReader(_sqlQueryForMexLargeCities.Replace("<where>", sqlWhereClause), CommandType.Text);
            this.chklistLargeCitiesMEX.Items.Clear();
            while (rdr.Read())
            {
                string item = rdr[1].ToString().Trim() + ": " + rdr[2].ToString().Trim() + " ~" + (Convert.ToDouble(rdr[3].ToString()) / (double)1000000.0).ToString("#0.000");
                this.chklistLargeCitiesMEX.Items.Add(item);
            }
            rdr.Close();
            for (int i = 0; i < this.chklistLargeCitiesMEX.Items.Count; i++)
            {
                chklistLargeCitiesMEX.SetItemChecked(i, false);
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
            _saveRequestDef = CreateRequestDef(false, true);
            _savedBaseDateTime = DateTime.MinValue;
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
                frm.SourceFolder = _randomNamesAndLocationsDataRequestFolder;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    requestName = frm.lstNames.SelectedItem.ToString();
                    if (requestName.Length > 0)
                    {
                        filePath = Path.Combine(_randomNamesAndLocationsDataRequestFolder, requestName + ".xml");
                        RandomNamesAndLocationsDataRequest reqDef = RandomNamesAndLocationsDataRequest.LoadFromXmlFile(filePath);
                        FillFormFromRequestDefinition(reqDef);
                        _saveRequestDef = reqDef;
                        _savedBaseDateTime = reqDef.BaseDateTime;
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
            _saveRequestDef = CreateRequestDef(false, true);
            _savedBaseDateTime = DateTime.MinValue;
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
                RandomNamesAndLocationsDataRequest req = CreateRequestDef(true, true);
                if (req.ListName.Length == 0)
                {
                    //error occurred in CreateRequestDef: probably a validation error
                    saveSucceeded = false;
                    return false;
                }
                string filename = Path.Combine(_randomNamesAndLocationsDataRequestFolder, req.ListName + ".xml");
                if (File.Exists(filename))
                {
                    _msg.Length = 0;
                    _msg.Append("Random names and locations data request ");
                    _msg.Append(req.ListName);
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
                _savedBaseDateTime = req.BaseDateTime;
                UpdateMruList(req.ListName);
                _msg.Length = 0;
                _msg.Append("Save successful for random data request definition: ");
                _msg.Append(req.ListName);
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

            string filePath = Path.Combine(_randomNamesAndLocationsDataRequestFolder, filename + ".xml");

            if (File.Exists(filePath))
            {
                //process it
                RandomNamesAndLocationsDataRequest reqdef = RandomNamesAndLocationsDataRequest.LoadFromXmlFile(filePath);
                FillFormFromRequestDefinition(reqdef);
                _saveRequestDef = reqdef;
                _savedBaseDateTime = reqdef.BaseDateTime;
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
                frm.SourceFolder = _randomNamesAndLocationsDataRequestFolder;
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

        private void CountryToUse_CheckedChanged()
        {
            this.tabControlForRandomDataLists.TabPages.Clear();
            this.panelCountryRandomData.Visible = true;
            this.tabControlForRandomDataLists.TabPages.Add(this.tabPersonData);
            if (this.chkUSRandomData.Checked)
                this.tabControlForRandomDataLists.TabPages.Add(this.tabUS);
            if (this.chkCanadaRandomData.Checked)
                this.tabControlForRandomDataLists.TabPages.Add(this.tabCanada);
            if (this.chkMexicoRandomData.Checked)
                this.tabControlForRandomDataLists.TabPages.Add(this.tabMexico);
        }

        /* ***************************************************************************
         * Resync US tab checklistboxes  
         ***************************************************************************** */

        private void USRegionChangeResync()
        {
            ReinitSubRegionsUS();
            ReinitStatesUS();
            ReinitMsaUS();
            ReinitCMsaUS();
        }

        private void USSubRegionChangeResync()
        {
            ReinitStatesUS();
            ReinitMsaUS();
            ReinitCMsaUS();
        }

        private void USStateChangeResync()
        {
            ReinitMsaUS();
            ReinitCMsaUS();
        }

        private void ReinitSubRegionsUS()
        {
            string sqlQuery = _sqlQueryForSubRegionNames;
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistRegionsUS.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where RegionName in (");
                for (int i = 0; i < this.chklistRegionsUS.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistRegionsUS.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillSubRegionsUS(sqlWhere.ToString());
            }
            else
            {
                this.chklistSubRegionsUS.Items.Clear();
            }
        }//end method

        private void ReinitStatesUS()
        {
            string sqlQuery = _sqlQueryForStateNames;
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistSubRegionsUS.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where SubRegionName in (");
                for (int i = 0; i < this.chklistSubRegionsUS.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistSubRegionsUS.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillStatesUS(sqlWhere.ToString());
            }
            else
            {
                this.chklistStateNamesUS.Items.Clear();
            }
        }//end method

        private void ReinitMsaUS()
        {
            string sqlQuery = _sqlQueryForMsaNames;
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistStateNamesUS.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where StateName in (");
                for (int i = 0; i < this.chklistStateNamesUS.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistStateNamesUS.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillMsaUS(sqlWhere.ToString());
            }
            else
            {
                this.chklistMsaUS.Items.Clear();
            }
        }//end method

        private void ReinitCMsaUS()
        {
            string sqlQuery = _sqlQueryForMsaNames;
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistStateNamesUS.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where StateName in (");
                for (int i = 0; i < this.chklistStateNamesUS.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistStateNamesUS.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillCMsaUS(sqlWhere.ToString());
            }
            else
            {
                this.chklistCMsaUS.Items.Clear();
            }
        }


        /* ***************************************************************************
         * Resync Canada tab checklistboxes  
         ***************************************************************************** */

        private void CANRegionChangeResync()
        {
            ReinitSubRegionsCAN();
            ReinitProvincesCAN();
            ReinitAreaCodesCAN();
            ReinitLargeCitiesCAN();
        }

        private void CANSubRegionChangeResync()
        {
            ReinitProvincesCAN();
            ReinitAreaCodesCAN();
            ReinitLargeCitiesCAN();
        }

        private void CANProvinceChangeResync()
        {
            ReinitAreaCodesCAN();
            ReinitLargeCitiesCAN();
        }

        private void ReinitSubRegionsCAN()
        {
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistRegionsCAN.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where RegionName in (");
                for (int i = 0; i < this.chklistRegionsCAN.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistRegionsCAN.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillSubRegionsCAN(sqlWhere.ToString());
            }
            else
            {
                this.chklistSubRegionsCAN.Items.Clear();
            }
        }//end method

        private void ReinitProvincesCAN()
        {
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistSubRegionsCAN.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where SubRegionName in (");
                for (int i = 0; i < this.chklistSubRegionsCAN.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistSubRegionsCAN.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillProvincesCAN(sqlWhere.ToString());
            }
            else
            {
                this.chklistProvincesCAN.Items.Clear();
            }
        }//end method

        private void ReinitAreaCodesCAN()
        {
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistProvincesCAN.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where ProvinceName in (");
                for (int i = 0; i < this.chklistProvincesCAN.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistProvincesCAN.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillAreaCodesCAN(sqlWhere.ToString());
            }
            else
            {
                this.chklistAreaCodesCAN.Items.Clear();
            }
        }//end method

        private void ReinitLargeCitiesCAN()
        {
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistProvincesCAN.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where ProvinceName in (");
                for (int i = 0; i < this.chklistProvincesCAN.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistProvincesCAN.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillLargeCitiesCAN(sqlWhere.ToString());
            }
            else
            {
                this.chklistLargeCitiesCAN.Items.Clear();
            }
        }


        /* ***************************************************************************
         * Resync Mexico tab checklistboxes  
         ***************************************************************************** */

        private void MEXRegionChangeResync()
        {
            ReinitSubRegionsMEX();
            ReinitStatesMEX();
            ReinitAreaCodesMEX();
            ReinitLargeCitiesMEX();
        }

        private void MEXSubRegionChangeResync()
        {
            ReinitStatesMEX();
            ReinitAreaCodesMEX();
            ReinitLargeCitiesMEX();
        }

        private void MEXStateChangeResync()
        {
            ReinitAreaCodesMEX();
            ReinitLargeCitiesMEX();
        }

        private void ReinitSubRegionsMEX()
        {
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistRegionsMEX.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where RegionName in (");
                for (int i = 0; i < this.chklistRegionsMEX.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistRegionsMEX.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillSubRegionsMEX(sqlWhere.ToString());
            }
            else
            {
                this.chklistSubRegionsMEX.Items.Clear();
            }
        }//end method

        private void ReinitStatesMEX()
        {
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistSubRegionsMEX.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where SubRegionName in (");
                for (int i = 0; i < this.chklistSubRegionsMEX.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistSubRegionsMEX.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillStatesMEX(sqlWhere.ToString());
            }
            else
            {
                this.chklistStatesMEX.Items.Clear();
            }
        }//end method

        private void ReinitAreaCodesMEX()
        {
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistStatesMEX.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where StateName in (");
                for (int i = 0; i < this.chklistStatesMEX.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistStatesMEX.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillAreaCodesMEX(sqlWhere.ToString());
            }
            else
            {
                this.chklistAreaCodesMEX.Items.Clear();
            }
        }//end method

        private void ReinitLargeCitiesMEX()
        {
            string sqlQuery = _sqlQueryForMsaNames;
            StringBuilder sqlWhere = new StringBuilder();

            sqlWhere.Length = 0;
            if (this.chklistStatesMEX.CheckedItems.Count > 0)
            {
                sqlWhere.Append("where StateName in (");
                for (int i = 0; i < this.chklistStatesMEX.CheckedItems.Count; i++)
                {
                    if (i > 0)
                        sqlWhere.Append(", ");
                    sqlWhere.Append("'");
                    sqlWhere.Append(this.chklistStatesMEX.CheckedItems[i].ToString());
                    sqlWhere.Append("'");
                }
                sqlWhere.Append(")");

                FillLargeCitiesMEX(sqlWhere.ToString());
            }
            else
            {
                this.chklistLargeCitiesMEX.Items.Clear();
            }
        }

        private void CheckedListBox_SelectAll(CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemCheckState(i, CheckState.Checked);
            }
        }

        private void CheckedListBox_SelectNone(CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemCheckState(i, CheckState.Unchecked);
            }
        }



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
            _processItemCheckEvents = false;

            this.txtDataMaskName.Text = string.Empty;

            this.chkUSRandomData.Checked = true;
            this.chkCanadaRandomData.Checked = true;
            this.chkMexicoRandomData.Checked = true;
            this.txtPercentFrequencyUnitedStates.Text = "75";
            this.txtPercentFrequencyCanada.Text = "10";
            this.txtPercentFrequencyMexico.Text = "15";
            CountryToUse_CheckedChanged();

            this.txtCurrentYear.Text = DateTime.Now.Year.ToString();

            this.chkIncludePersonalNames.Checked = true;
            this.txtPercentFrequencyPersonalNames.Text = "50";
            this.txtPercentFrequencyMaleNames.Text = "50";
            this.txtPercentFrequencyFemaleNames.Text = "50";
            this.chkIncludeBusinessNames.Checked = true;
            this.txtPercentFrequencyBusinessNames.Text = "50";

            this.chkAddressLine1US.Checked = true;
            this.chkAddressLine2US.Checked = true;
            this.txtPercentFrequencyAddressLine2.Text = "50";
            this.chkCityStateZipUS.Checked = true;
            this.chkAreaCode.Checked = true;

            this.chkNationalId.Checked = true;
            this.chkTelephoneNumber.Checked = true;
            this.chkEmailAddress.Checked = true;

            this.chkGenderUS.Checked = true;
            this.chkBirthDateUS.Checked = true;

            this.txtNumRandomDataItems.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");

            InitUSTab();
            InitCanadaTab();
            InitMexicoTab();

        }

        private void InitRequestDefObject(ref RandomNamesAndLocationsDataRequest reqDef)
        {
            reqDef.DatabaseFilePath = string.Empty;
            reqDef.DatabasePassword = string.Empty;
            reqDef.RandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";
            //reqDef.ListFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\NamesAndLocations";
            reqDef.ListName = string.Empty;
            reqDef.OutputToXmlFile = true;
            reqDef.OutputToGrid = true;
            reqDef.NumRandomNameItems = 1000;
            reqDef.IncludeUSData = false;
            reqDef.IncludeCanadaData = false;
            reqDef.IncludeMexicoData = false;
            reqDef.PercentFrequencyUnitedStates = 0;
            reqDef.PercentFrequencyCanada = 0;
            reqDef.PercentFrequencyMexico = 0;
            reqDef.IncludePersonNames = false;
            reqDef.IncludeBusinessNames = false;
            reqDef.IncludeAddressLine1 = false;
            reqDef.IncludeAddressLine2 = false;
            reqDef.IncludeCityLocation = false;
            reqDef.IncludeAreaCode = false;
            reqDef.IncludeGenderForPersons = false;
            reqDef.IncludePersonBirthDate = false;
            reqDef.BaseDateTime = PFTextObjects.PFTextProcessor.ConvertStringToDateTime(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + this.txtCurrentYear.Text, DateTime.Now);
            reqDef.PersonNamesPercentFrequency = 100;
            reqDef.BusinessNamesPercentFrequency = 0;
            reqDef.MalePersonNamePercentFrequency = 50;
            reqDef.FemalePersonNamePercentFrequency = 50;
            reqDef.AddressLine2PercentFrequency = 50;
            reqDef.PersonAgeGroups = new PFList<string>();
            reqDef.LocationSelectionCriteriaLists = new PFList<LocationSelectionCriteria>();
        }

        private RandomNamesAndLocationsDataRequest CreateRequestDef(bool verifyNumbers, bool hideDatabasePassword)
        {
            RandomNamesAndLocationsDataRequest reqDef = new RandomNamesAndLocationsDataRequest();

            if (verifyNumbers)
            {
                string errMessages = VerifyNumericInput();
                if (errMessages.Length > 0)
                {
                    AppMessages.DisplayErrorMessage(errMessages);
                    return reqDef;
                }
            }

            RandomNamesAndLocationsDataRequest rdr = new RandomNamesAndLocationsDataRequest();

            rdr.DatabaseFilePath = _defaultRandomDataDatabase;
            if (hideDatabasePassword)
                rdr.DatabasePassword = AppConfig.GetStringValueFromConfigFile("DefaultRandomDataDatabasePassword", string.Empty);
            else
                rdr.DatabasePassword = _defaultRandomDataDatabasePassword;
            rdr.RandomDataXmlFilesFolder = _defaultRandomDataXmlFilesFolder;
            //rdr.ListFolder = _randomNamesAndLocationsDataRequestFolder;
            rdr.ListName = this.txtDataMaskName.Text;
            rdr.OutputToXmlFile = true;
            rdr.OutputToGrid = false;
            rdr.NumRandomNameItems = PFTextProcessor.ConvertStringToInt(this.txtNumRandomDataItems.Text, 1000);
            rdr.IncludeUSData = this.chkUSRandomData.Checked;
            rdr.IncludeCanadaData = this.chkCanadaRandomData.Checked;
            rdr.IncludeMexicoData = this.chkMexicoRandomData.Checked;
            rdr.IncludePersonNames = this.chkIncludePersonalNames.Checked;
            rdr.IncludeBusinessNames = this.chkIncludeBusinessNames.Checked;
            rdr.IncludeAddressLine1 = this.chkAddressLine1US.Checked;
            rdr.IncludeAddressLine2 = this.chkAddressLine2US.Checked;
            if (this.chkAddressLine1US.Checked || this.chkAddressLine2US.Checked)
                this.chkCityStateZipUS.Checked = true;
            rdr.IncludeCityLocation = this.chkCityStateZipUS.Checked;
            rdr.IncludeAreaCode = this.chkAreaCode.Checked;
            rdr.IncludeNationalId = this.chkNationalId.Checked;
            rdr.IncludeTelephoneNumber = this.chkTelephoneNumber.Checked;
            rdr.IncludeEmailAddress = this.chkEmailAddress.Checked;
            rdr.IncludeGenderForPersons = this.chkGenderUS.Checked;
            rdr.IncludePersonBirthDate = this.chkBirthDateUS.Checked;
            if (_savedBaseDateTime != DateTime.MinValue)
                rdr.BaseDateTime = _savedBaseDateTime;
            else
                rdr.BaseDateTime = PFTextObjects.PFTextProcessor.ConvertStringToDateTime(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + this.txtCurrentYear.Text, DateTime.Now);
            rdr.PersonNamesPercentFrequency = this.txtPercentFrequencyPersonalNames.Text != string.Empty ? PFTextProcessor.ConvertStringToInt(this.txtPercentFrequencyPersonalNames.Text, 0) : (int)0;
            rdr.MalePersonNamePercentFrequency = this.txtPercentFrequencyMaleNames.Text != string.Empty ? PFTextProcessor.ConvertStringToInt(this.txtPercentFrequencyMaleNames.Text, 0) : 0;
            rdr.FemalePersonNamePercentFrequency = this.txtPercentFrequencyFemaleNames.Text != string.Empty ? PFTextProcessor.ConvertStringToInt(this.txtPercentFrequencyFemaleNames.Text, 0) : 0;
            rdr.BusinessNamesPercentFrequency = this.txtPercentFrequencyBusinessNames.Text != string.Empty ? PFTextProcessor.ConvertStringToInt(this.txtPercentFrequencyBusinessNames.Text, 0) : 0;
            rdr.AddressLine2PercentFrequency = this.txtPercentFrequencyAddressLine2.Text != string.Empty ? PFTextProcessor.ConvertStringToInt(this.txtPercentFrequencyAddressLine2.Text, 0) : 0;
            FillSelectionsListFromFormChecklist(this.chklistAgeGroupsUS, rdr.PersonAgeGroups);

            rdr.PercentFrequencyUnitedStates = 0;
            rdr.PercentFrequencyCanada = 0;
            rdr.PercentFrequencyMexico = 0;
            if (this.chkUSRandomData.Checked)
            {
                rdr.PercentFrequencyUnitedStates = AppTextGlobals.ConvertStringToInt(this.txtPercentFrequencyUnitedStates.Text, 100);
                LocationSelectionCriteria criteria = new LocationSelectionCriteria();
                criteria.Country = enCountry.UnitedStates;
                FillSelectionsListFromFormChecklist(this.chklistRegionsUS, criteria.Regions);
                FillSelectionsListFromFormChecklist(this.chklistSubRegionsUS, criteria.SubRegions);
                FillSelectionsListFromFormChecklist(this.chklistStateNamesUS, criteria.StatesProvinces);
                FillSelectionsListFromFormChecklist(this.chklistMsaUS, criteria.MsaAreaCodes);
                FillSelectionsListFromFormChecklist(this.chklistCMsaUS, criteria.CmsaLargeCities);
                rdr.LocationSelectionCriteriaLists.Add(criteria);
            }

            if (this.chkCanadaRandomData.Checked)
            {
                rdr.PercentFrequencyCanada = AppTextGlobals.ConvertStringToInt(this.txtPercentFrequencyCanada.Text, 10);
                LocationSelectionCriteria criteria = new LocationSelectionCriteria();
                criteria.Country = enCountry.Canada;
                FillSelectionsListFromFormChecklist(this.chklistRegionsCAN, criteria.Regions);
                FillSelectionsListFromFormChecklist(this.chklistSubRegionsCAN, criteria.SubRegions);
                FillSelectionsListFromFormChecklist(this.chklistProvincesCAN, criteria.StatesProvinces);
                FillSelectionsListFromFormChecklist(this.chklistAreaCodesCAN, criteria.MsaAreaCodes);
                FillSelectionsListFromFormChecklist(this.chklistLargeCitiesCAN, criteria.CmsaLargeCities);
                rdr.LocationSelectionCriteriaLists.Add(criteria);
            }

            if (this.chkMexicoRandomData.Checked)
            {
                rdr.PercentFrequencyMexico = AppTextGlobals.ConvertStringToInt(this.txtPercentFrequencyMexico.Text, 25);
                LocationSelectionCriteria criteria = new LocationSelectionCriteria();
                criteria.Country = enCountry.Mexico;
                FillSelectionsListFromFormChecklist(this.chklistRegionsMEX, criteria.Regions);
                FillSelectionsListFromFormChecklist(this.chklistSubRegionsMEX, criteria.SubRegions);
                FillSelectionsListFromFormChecklist(this.chklistStatesMEX, criteria.StatesProvinces);
                FillSelectionsListFromFormChecklist(this.chklistAreaCodesMEX, criteria.MsaAreaCodes);
                FillSelectionsListFromFormChecklist(this.chklistLargeCitiesMEX, criteria.CmsaLargeCities);
                rdr.LocationSelectionCriteriaLists.Add(criteria);
            }


            return rdr;


        }

        public string VerifyNumericInput()
        {
            bool percentFrequencyPersonalNamesIsNumber = true;
            bool percentFrequencyMaleNamesIsNumber = true;
            bool percentFrequencyFemaleNamesIsNumber = true;
            bool percentFrequencyBusinessNamesIsNumber = true;
            bool percentFrequencyAddressLine2IsNumber = true;
            bool percentFrequencyUnitedStatesIsNumber = true;
            bool percentFrequencyCanadaIsNumber = true;
            bool percentFrequencyMexicoIsNumber = true;
            bool numRandomDataItemsIsNumber = true;

            int num = 0;


            _msg.Length = 0;

            if (this.chkIncludePersonalNames.Checked)
            {
                percentFrequencyPersonalNamesIsNumber = int.TryParse(this.txtPercentFrequencyPersonalNames.Text, out num);
                percentFrequencyMaleNamesIsNumber = int.TryParse(this.txtPercentFrequencyMaleNames.Text, out num);
                percentFrequencyFemaleNamesIsNumber = int.TryParse(this.txtPercentFrequencyFemaleNames.Text, out num);
            }

            if (this.chkIncludeBusinessNames.Checked)
            {
                percentFrequencyBusinessNamesIsNumber = int.TryParse(this.txtPercentFrequencyBusinessNames.Text, out num);
            }

            if (this.chkAddressLine2US.Checked)
            {
                percentFrequencyAddressLine2IsNumber = int.TryParse(this.txtPercentFrequencyAddressLine2.Text, out num);
            }

            if (this.chkUSRandomData.Checked)
            {
                percentFrequencyUnitedStatesIsNumber = int.TryParse(this.txtPercentFrequencyUnitedStates.Text, out num);
            }

            if (this.chkCanadaRandomData.Checked)
            {
                percentFrequencyCanadaIsNumber = int.TryParse(this.txtPercentFrequencyCanada.Text, out num);
            }

            if (this.chkMexicoRandomData.Checked)
            {
                percentFrequencyMexicoIsNumber = int.TryParse(this.txtPercentFrequencyMexico.Text, out num);
            }

            numRandomDataItemsIsNumber = int.TryParse(this.txtNumRandomDataItems.Text, out num);

            if (percentFrequencyPersonalNamesIsNumber == false)
            {
                _msg.Append("Invalid personal names frequency: ");
                _msg.Append(this.txtPercentFrequencyPersonalNames.Text);
                _msg.Append(Environment.NewLine);
            }
            if (percentFrequencyMaleNamesIsNumber == false)
            {
                _msg.Append("Invalid male names frequency: ");
                _msg.Append(this.txtPercentFrequencyMaleNames.Text);
                _msg.Append(Environment.NewLine);
            }
            if (percentFrequencyFemaleNamesIsNumber == false)
            {
                _msg.Append("Invalid female names frequency: ");
                _msg.Append(this.txtPercentFrequencyFemaleNames.Text);
                _msg.Append(Environment.NewLine);
            }
            if (percentFrequencyBusinessNamesIsNumber == false)
            {
                _msg.Append("Invalid business names frequency: ");
                _msg.Append(this.txtPercentFrequencyBusinessNames.Text);
                _msg.Append(Environment.NewLine);
            }
            if (percentFrequencyAddressLine2IsNumber == false)
            {
                _msg.Append("Invalid address line 2 frequency: ");
                _msg.Append(this.txtPercentFrequencyAddressLine2.Text);
                _msg.Append(Environment.NewLine);
            }
            if (percentFrequencyUnitedStatesIsNumber == false)
            {
                _msg.Append("Invalid United States country percent frequency: ");
                _msg.Append(this.txtPercentFrequencyUnitedStates.Text);
                _msg.Append(Environment.NewLine);
            }
            if (percentFrequencyCanadaIsNumber == false)
            {
                _msg.Append("Invalid Canada country percent frequency: ");
                _msg.Append(this.txtPercentFrequencyCanada.Text);
                _msg.Append(Environment.NewLine);
            }
            if (percentFrequencyMexicoIsNumber == false)
            {
                _msg.Append("Invalid Mexico country percent frequency: ");
                _msg.Append(this.txtPercentFrequencyMexico.Text);
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

        private void FillSelectionsListFromFormChecklist(CheckedListBox lbx, PFList<string> list)
        {
            for (int i = 0; i < lbx.CheckedItems.Count; i++)
            {

                list.Add(lbx.CheckedItems[i].ToString());
            }
        }


        private bool ItemWasChecked(string item, PFList<string> list)
        {
            bool itemInList = false;

            for (int inx = 0; inx < list.Count; inx++)
            {
                if (list[inx] == item)
                {
                    itemInList = true;
                    break;
                }
            }

            return itemInList;
        }


        private void FillFormFromRequestDefinition(RandomNamesAndLocationsDataRequest rdr)
        {
            this.txtDataMaskName.Text = rdr.ListName;
            this.txtNumRandomDataItems.Text = rdr.NumRandomNameItems.ToString();
            this.chkUSRandomData.Checked = rdr.IncludeUSData;
            this.chkCanadaRandomData.Checked = rdr.IncludeCanadaData;
            this.chkMexicoRandomData.Checked = rdr.IncludeMexicoData;
            this.chkIncludePersonalNames.Checked = rdr.IncludePersonNames;
            this.chkIncludeBusinessNames.Checked = rdr.IncludeBusinessNames;
            this.chkAddressLine1US.Checked = rdr.IncludeAddressLine1;
            this.chkAddressLine2US.Checked = rdr.IncludeAddressLine2;
            this.chkCityStateZipUS.Checked = rdr.IncludeCityLocation;
            this.chkAreaCode.Checked = rdr.IncludeAreaCode;
            this.chkNationalId.Checked = rdr.IncludeNationalId;
            this.chkTelephoneNumber.Checked = rdr.IncludeTelephoneNumber;
            this.chkEmailAddress.Checked = rdr.IncludeEmailAddress;
            this.chkGenderUS.Checked = rdr.IncludeGenderForPersons;
            this.chkBirthDateUS.Checked = rdr.IncludePersonBirthDate;

            this.txtCurrentYear.Text = rdr.BaseDateTime.Year.ToString();

            this.txtPercentFrequencyUnitedStates.Text = rdr.PercentFrequencyUnitedStates.ToString("#");
            this.txtPercentFrequencyCanada.Text = rdr.PercentFrequencyCanada.ToString("#");
            this.txtPercentFrequencyMexico.Text = rdr.PercentFrequencyMexico.ToString("#");

            this.txtPercentFrequencyPersonalNames.Text = rdr.PersonNamesPercentFrequency.ToString();
            this.txtPercentFrequencyMaleNames.Text = rdr.MalePersonNamePercentFrequency.ToString();
            this.txtPercentFrequencyFemaleNames.Text = rdr.FemalePersonNamePercentFrequency.ToString();
            this.txtPercentFrequencyBusinessNames.Text = rdr.BusinessNamesPercentFrequency.ToString();
            this.txtPercentFrequencyAddressLine2.Text = rdr.AddressLine2PercentFrequency.ToString();

            FillAgeGroupChecklistFromSelectionsList(this.chklistAgeGroupsUS, rdr.PersonAgeGroups);

            if (rdr.IncludeUSData)
            {
                PFList<LocationSelectionCriteria> criteriaLists = rdr.LocationSelectionCriteriaLists;
                LocationSelectionCriteria criteria = new LocationSelectionCriteria();  //create an empty criteria object to use if nameListRequest does not include one
                criteria.Country = enCountry.UnitedStates;
                for (int listInx = 0; listInx < criteriaLists.Count; listInx++)
                {
                    if (criteriaLists[listInx].Country == enCountry.UnitedStates)
                    {
                        criteria = criteriaLists[listInx];
                        break;
                    }
                }
                FillFormChecklistFromSelectionsList(this.chklistRegionsUS, criteria.Regions);
                USRegionChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistSubRegionsUS, criteria.SubRegions);
                USSubRegionChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistStateNamesUS, criteria.StatesProvinces);
                USStateChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistMsaUS, criteria.MsaAreaCodes);
                FillFormChecklistFromSelectionsList(this.chklistCMsaUS, criteria.CmsaLargeCities);
            }

            if (rdr.IncludeCanadaData)
            {
                PFList<LocationSelectionCriteria> criteriaLists = rdr.LocationSelectionCriteriaLists;
                LocationSelectionCriteria criteria = new LocationSelectionCriteria();  //create an empty criteria object to use if nameListRequest does not include one
                criteria.Country = enCountry.Canada;
                for (int listInx = 0; listInx < criteriaLists.Count; listInx++)
                {
                    if (criteriaLists[listInx].Country == enCountry.Canada)
                    {
                        criteria = criteriaLists[listInx];
                        break;
                    }
                }
                FillFormChecklistFromSelectionsList(this.chklistRegionsCAN, criteria.Regions);
                CANRegionChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistSubRegionsCAN, criteria.SubRegions);
                CANSubRegionChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistProvincesCAN, criteria.StatesProvinces);
                CANProvinceChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistAreaCodesCAN, criteria.MsaAreaCodes);
                FillFormChecklistFromSelectionsList(this.chklistLargeCitiesCAN, criteria.CmsaLargeCities);
            }

            if (rdr.IncludeMexicoData)
            {
                PFList<LocationSelectionCriteria> criteriaLists = rdr.LocationSelectionCriteriaLists;
                LocationSelectionCriteria criteria = new LocationSelectionCriteria();  //create an empty criteria object to use if nameListRequest does not include one
                criteria.Country = enCountry.Mexico;
                for (int listInx = 0; listInx < criteriaLists.Count; listInx++)
                {
                    if (criteriaLists[listInx].Country == enCountry.Mexico)
                    {
                        criteria = criteriaLists[listInx];
                        break;
                    }
                }
                FillFormChecklistFromSelectionsList(this.chklistRegionsMEX, criteria.Regions);
                MEXRegionChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistSubRegionsMEX, criteria.SubRegions);
                MEXSubRegionChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistStatesMEX, criteria.StatesProvinces);
                MEXStateChangeResync();
                FillFormChecklistFromSelectionsList(this.chklistAreaCodesMEX, criteria.MsaAreaCodes);
                FillFormChecklistFromSelectionsList(this.chklistLargeCitiesMEX, criteria.CmsaLargeCities);
            }

        }

        private void FillFormChecklistFromSelectionsList(CheckedListBox lbx, PFList<string> list)
        {
            for (int i = 0; i < lbx.Items.Count; i++)
            {
                if (ItemWasChecked(lbx.Items[i].ToString(), list))
                {
                    lbx.SetItemCheckState(i, CheckState.Checked);
                }
                else
                {
                    lbx.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
        }

        private void FillAgeGroupChecklistFromSelectionsList(CheckedListBox lbx, PFList<string> list)
        {
            for (int i = 0; i < lbx.Items.Count; i++)
            {
                if (ItemWasChecked(lbx.Items[i].ToString(), list))
                {
                    lbx.SetItemCheckState(i, CheckState.Checked);
                }
                else
                {
                    lbx.SetItemCheckState(i, CheckState.Unchecked);
                }
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

                PreviewRandomNamesAndLocations();
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

        private void PreviewRandomNamesAndLocations()
        {
            DataTable dt = null;
            RandomNamesAndLocationsDataTable rndt = new RandomNamesAndLocationsDataTable();
            DataListProcessor appProcessor = new DataListProcessor();


            try
            {
                if (this.txtDataMaskName.Text.Trim().Length == 0)
                {
                    this.txtDataMaskName.Text = "TempDef";
                }

                RefreshCountrySelectionPercentages();

                RefreshNameSelectionPercentages();

                appProcessor.MessageLogUI = _messageLog;

                RandomNamesAndLocationsDataRequest rdr = CreateRequestDef(false, false);

                //******************************************************
                //following code lets the appProcessor show the grid
                //******************************************************
                rdr.OutputToXmlFile = false;
                rdr.OutputToGrid = true;

                appProcessor.ShowInstalledDatabaseProvidersOnly = _showInstalledDatabaseProvidersOnly;
                appProcessor.DefaultOutputDatabaseType = _defaultOutputDatabaseType;
                appProcessor.DefaultOutputDatabaseConnectionString = _defaultOutputDatabaseConnectionString;
                appProcessor.GridExportFolder = _defaultDataGridExportFolder;

                dt = appProcessor.GetRandomNamesList(rdr,
                                                     Convert.ToInt32(this.txtNumRandomDataItems.Text),
                                                     rdr.OutputToXmlFile,
                                                     _defaultDataGridExportFolder,
                                                     rdr.ListName,
                                                     rdr.OutputToGrid);

                ////************************************************************************************************************
                ////following code uses RandomNamesAndLocationsDataTable object to get table that will be shown on  the grid.
                ////************************************************************************************************************
                //rdr.OutputToXmlFile = false;
                //rdr.OutputToGrid = false;
                //listTable = rndt.CreateRandomNamesAndLocationsDataTable(Convert.ToInt32(this.txtNumRandomDataItems.Text),
                //                                                 rdr,
                //                                                 _showInstalledDatabaseProvidersOnly,
                //                                                 _defaultOutputDatabaseType,
                //                                                 _defaultOutputDatabaseConnectionString,
                //                                                 _defaultDataGridExportFolder);
                
                //if(listTable != null)
                //    OutputDataTableToGrid(listTable);

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
                if (this.txtDataMaskName.Text == "TempDef")
                    this.txtDataMaskName.Text = string.Empty;
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
            if (Directory.Exists(_randomNamesAndLocationsOriginalDataRequestsFolder))
            {
                foreach (string sourceFilePath in Directory.GetFiles(_randomNamesAndLocationsOriginalDataRequestsFolder))
                {
                    string destFilePath = Path.Combine(_randomNamesAndLocationsDataRequestFolder, Path.GetFileName(sourceFilePath));
                    File.Copy(sourceFilePath, destFilePath, true);
                }
            }
            //force a save prompt of existing data on the form if user exits without saving after this routine is finished
            RandomNamesAndLocationsDataRequest reqDef = new RandomNamesAndLocationsDataRequest();
            InitRequestDefObject(ref reqDef);
            _saveRequestDef = reqDef;
            _savedBaseDateTime = reqDef.BaseDateTime;
        }

#pragma warning restore 1591


    }//end class
}//end namespace
