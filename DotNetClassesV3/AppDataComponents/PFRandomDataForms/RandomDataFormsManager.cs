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
using PFTimers;

namespace PFRandomDataForms
{
#pragma warning disable 1591
    /// <summary>
    /// Main form for organizing access to random value definition and preview forms.
    /// </summary>
    public partial class RandomDataFormsManager : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private string _helpFilePath = string.Empty;
        private bool _saveErrorMessagesToAppLog = true;

        string _initDataFilesFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\InitDataFiles\", "");
        private string _defaultRandomDataXmlFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\";

        //fields for properties
        MessageLog _messageLog = null;


        public RandomDataFormsManager()
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
        private void cmdManage_Click(object sender, EventArgs e)
        {
            ManageRandomTypeSources();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            ShowHelpContents();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            HideForm();
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void PFWindowsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            HideForm();
            this.DialogResult = DialogResult.Cancel;
        }




        //common form processing routines
        public void InitializeForm()
        {
            EnableFormControls();
            SetHelpFileValues();
            LoadRandomDataFiles();
            this.Focus();
        }
        private void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("RandomSourcesHelpFileName", "RandomDataMasks.chm");
            string helpFilePath = Path.Combine(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;
        }

        private void LoadRandomDataFiles()
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

            if (dirInfo.GetFiles("*.dat").Length == 0)
            {
                LoadRandomDataXmlFiles();
            }

            if (dirInfo.GetFiles("TestOrders.sdf").Length == 0)
            {
                LoadTestOrdersGeneratorDatabase();
            }

        }

        private void LoadRandomDataDatabase()
        {
            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;

            _msg.Length = 0;
            _msg.Append("Loading random data database into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            WriteToMessageLog(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                //copy random data database file
                string sdfSourceFilePath = Path.Combine(_initDataFilesFolder, "RandomData.sdf");
                string sdfDestinationFilePath = Path.Combine(destinationFolder, "RandomData.sdf");
                File.Copy(sdfSourceFilePath, sdfDestinationFilePath, false);
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

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of random data database has finished.");
            WriteToMessageLog(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load random data database: ");
            _msg.Append(sw.FormattedElapsedTime);
            WriteToMessageLog(_msg.ToString());
        
        }

        private void LoadRandomDataXmlFiles()
        {
            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;

            _msg.Length = 0;
            _msg.Append("Loading random data files into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            WriteToMessageLog(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                //unzip and copy random word and names XML files
                string zipArchiveFile = Path.Combine(sourceFolder, "RandomDataFiles.zip");
                ZipArchive za = new ZipArchive(zipArchiveFile);
                za.ExtractAll(destinationFolder);
                za = null;
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

            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of random data files has finished.");
            WriteToMessageLog(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load random data files: ");
            _msg.Append(sw.FormattedElapsedTime);
            WriteToMessageLog(_msg.ToString());

        }

        private void LoadTestOrdersGeneratorDatabase()
        {
            string sourceFolder = _initDataFilesFolder;
            string destinationFolder = _defaultRandomDataXmlFilesFolder;

            _msg.Length = 0;
            _msg.Append("Loading test orders generator database into application data folder: ");
            _msg.Append(_defaultRandomDataXmlFilesFolder);
            WriteToMessageLog(_msg.ToString());

            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                //unzip and copy random word and names XML files
                string zipArchiveFile = Path.Combine(sourceFolder, "TestOrders.zip");
                ZipArchive za = new ZipArchive(zipArchiveFile);
                za.ExtractAll(destinationFolder);
                za = null;
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
                 
        
            sw.Stop();

            _msg.Length = 0;
            _msg.Append("Load of test orders generator database has finished.");
            WriteToMessageLog(_msg.ToString());
            _msg.Length = 0;
            _msg.Append("Time to load test orders generator database: ");
            _msg.Append(sw.FormattedElapsedTime);
            WriteToMessageLog(_msg.ToString());

        }



        public void HideForm()
        {
            this.Hide();
        }

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

        //Application routines
        private void ManageRandomTypeSources()
        {
            if (this.optRandomNamesAndLocations.Checked)
            {
                ManageRandomNamesAndLocationsSources();
            }
            else if (this.optRandomNumbers.Checked)
            {
                ManageRandomNumbersSources();
            }
            else if (this.optRandomWords.Checked)
            {
                ManageRandomWordsSources();
            }
            else if (this.optRandomDatesAndTimes.Checked)
            {
                ManageRandomDatesAndTimesSources();
            }
            else if (this.optRandomBooleans.Checked)
            {
                ManageRandomBooleansSources();
            }
            else if (this.optCustomRandomValues.Checked)
            {
                ManageCustomRandomValuesSources();
            }
            else if (this.optRandomStrings.Checked)
            {
                ManageRandomStringsSources();
            }
            else if (this.optRandomBytes.Checked)
            {
                ManageRandomBytesSources();
            }
            else if (this.optRandomDataElements.Checked)
            {
                ManageRandomDataElementsSources();
            }
            else
            {
                AppMessages.DisplayAlertMessage("You must select a randomizer type to manage.");
            }
        }

        private void ShowRandomSourcesForm(Form frm)
        {
            DialogResult res = frm.ShowDialog();

            _msg.Length = 0;
            if (res == DialogResult.OK)
            {
                _msg.Append("OK pressed.");
            }
            else
            {
                _msg.Append("Cancel pressed.");
            }

            WriteToMessageLog(_msg.ToString());
        }

        private void ManageRandomNamesAndLocationsSources()
        {
            RandomNamesAndLocationsForm frm = new RandomNamesAndLocationsForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageRandomNumbersSources()
        {
            RandomNumbersForm frm = new RandomNumbersForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageRandomWordsSources()
        {
            RandomWordsForm frm = new RandomWordsForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageRandomDatesAndTimesSources()
        {
            RandomDateTimesForm frm = new RandomDateTimesForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageRandomBooleansSources()
        {
            RandomBooleansForm frm = new RandomBooleansForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageCustomRandomValuesSources()
        {
            RandomCustomValuesForm frm = new RandomCustomValuesForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageRandomStringsSources()
        {
            RandomStringsForm frm = new RandomStringsForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageRandomBytesSources()
        {
            RandomBytesForm frm = new RandomBytesForm();
            ShowRandomSourcesForm(frm);
        }

        private void ManageRandomDataElementsSources()
        {
            RandomDataElementsForm frm = new RandomDataElementsForm();
            ShowRandomSourcesForm(frm);
        }

        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }

        private void ShowHelpContents()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Randomizer Sources Manager Form");
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
#pragma warning restore 1591


    }//end class
}//end namespace
