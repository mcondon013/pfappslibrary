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
using PFFileSystemObjects;
using PFConnectionObjects;
using PFConnectionStrings;
using PFCollectionsObjects;
using PFDataAccessObjects;
using PFTextObjects;

namespace TestprogLoadAWDW
{
    public partial class MainForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;

        private DatabasePlatform _sourceDbPlatform = DatabasePlatform.Unknown;
        private string _sourceConnectionString = @"";
        private string _sv2ConnectionString = @"Data Source=PROFASTSV2; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogLoadAWDW; Workstation ID=PROFASTWS5;";
        private string _ws3ConnectionString = @"Data Source=PROFASTWS3; Initial Catalog=AdventureWorksDW2008R2; Integrated Security=True; Application Name=TestprogLoadAWDW; Workstation ID=PROFASTWS5;";
        private string _ce35ConnectionString = @"data source='C:\ProFast\Projects\DatabaseSamples\AdventureWorksDW.sdf';";


        PFAppProcessor _appProcessor = new PFAppProcessor();

        //private fields for processing file and folder dialogs
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        private SaveFileDialog _saveFileDialog = new SaveFileDialog();
        private FolderBrowserDialog _folderBrowserDialog = new FolderBrowserDialog();
        private string _saveSelectionsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _saveSelectionsFile = string.Empty;
        private string[] _saveSelectedFiles = null;
        private bool _saveMultiSelect = true;
        private string _saveFilter = "Text Files|*.txt|All Files|*.*";
        private int _saveFilterIndex = 1;
        private bool _showCreatePrompt = true;
        private bool _showOverwritePrompt = true;
        private bool _showNewFolderButton = true;

        public MainForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            RunCopy();
        }

        private void cboDatabaseTypeForOutputTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOutputConnectionString();
            SetOutputTablesSchema();
            SetOutputBatchSizeForDbPlatform();
        }

        private void cmdBuildConnectionString_Click(object sender, EventArgs e)
        {
            DefineConnectionString();
        }

        //Menu item clicks
        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void mnuToolsOptionsUserSettings_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsUserSettings();
        }

        private void mnuToolsOptionsApplicationSettings_Click(object sender, EventArgs e)
        {
            ShowToolsOptionsApplicationSettings();
        }

        private void mnuToolsFormSaveScreenLocations_Click(object sender, EventArgs e)
        {
            SaveScreenLocations();
        }

        private void mnuToolsFormRestoreScreenLocations_Click(object sender, EventArgs e)
        {
            RestoreDefaultScreenLocations();
        }

        private void showHideMessageLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program._messageLog.FormIsVisible)
                Program._messageLog.HideWindow();
            else
                Program._messageLog.ShowWindow();
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



        //Form Routines
        private void CloseForm()
        {
            this.Close();
        }

        private void ShowToolsOptionsUserSettings()
        {
            UserOptionsForm usrOptionsForm = new UserOptionsForm();

            try
            {
                usrOptionsForm.ShowDialog();
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

        private void ShowToolsOptionsApplicationSettings()
        {
            _appProcessor.ShowAppConfigManager();
            this.SetLoggingValues();
            this.SetHelpFileValues();
            this.Focus();
            this.Refresh();
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.SaveFormLocationsOnExit)
                SaveScreenLocations();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {
                this.Text = AppInfo.AssemblyProduct;

                SetFormLocationAndSize();

                SetLoggingValues();

                SetHelpFileValues();

                this.chkEraseOutputBeforeEachTest.Checked = true;

                SetFormValues();

                LoadSourceConnectionStrings();

                InitAppProcessor();

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
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "pfFolderSize.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

            _appProcessor.HelpFilePath = _helpFilePath;

        }

        private void SetFormValues()
        {
            this.optSQLServerSV2.Checked = true;

            this.txtOutputBatchSize.Text = AppConfig.GetStringValueFromConfigFile("DefaultOutputDatabaseBatchSize", "120"); ;
            this.chkReplaceExistingTables.Checked = AppConfig.GetBooleanValueFromConfigFile("ReplaceOutputTableIfExists", "true");

            InitDatabaseTypesDropdown();

            this.cboDatabaseTypeForOutputTables.Text = string.Empty;

        }

        private void InitDatabaseTypesDropdown()
        {
            bool showInstalledDatabaseProvidersOnly = true;
            PFConnectionManager connMgr = new PFConnectionManager();
            PFKeyValueList<string, PFProviderDefinition> provDefs = connMgr.GetListOfProviderDefinitions();

            this.cboDatabaseTypeForOutputTables.Items.Clear();
            foreach (stKeyValuePair<string, PFProviderDefinition> provDef in provDefs)
            {
                if (showInstalledDatabaseProvidersOnly)
                {
                    if (provDef.Value.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                    {
                        this.cboDatabaseTypeForOutputTables.Items.Add(provDef.Key);
                    }
                    else
                    {
                        ;
                    }
                }
                else
                {
                    this.cboDatabaseTypeForOutputTables.Items.Add(provDef.Key);
                }
            }

        }

        private void LoadSourceConnectionStrings()
        {
            string configValue = string.Empty;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_SV2", string.Empty);
            if (configValue.Length > 0)
                _sv2ConnectionString = configValue;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_WS3", string.Empty);
            if (configValue.Length > 0)
                _ws3ConnectionString = configValue;

            configValue = AppConfig.GetStringValueFromConfigFile("DefaultConnection_CE35", string.Empty);
            if (configValue.Length > 0)
                _ce35ConnectionString = configValue;
       
        }

        private void InitAppProcessor()
        {
            _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
            _appProcessor.MessageLogUI = Program._messageLog;
            _appProcessor.HelpFilePath = _helpFilePath;
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

        //application routines
        private void RunCopy()
        {

            try
            {
                DisableFormControls();
                _appProcessor.SaveErrorMessagesToAppLog = _saveErrorMessagesToAppLog;
                this.Cursor = Cursors.WaitCursor;

                if (this.chkEraseOutputBeforeEachTest.Checked)
                {
                    Program._messageLog.Clear();
                }

                if (optSQLServerSV2.Checked)
                {
                    _sourceDbPlatform = DatabasePlatform.MSSQLServer;
                    _sourceConnectionString = _sv2ConnectionString;
                }
                else if (optSQLServerWS3.Checked)
                {
                    _sourceDbPlatform = DatabasePlatform.MSSQLServer;
                    _sourceConnectionString = _ws3ConnectionString;
                }
                else
                {
                    _sourceDbPlatform = DatabasePlatform.SQLServerCE35;
                    _sourceConnectionString = _ce35ConnectionString;
                }

                DatabasePlatform destDbPlatform = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForOutputTables.Text);

                _appProcessor.CopyAdventureWorksDW(_sourceDbPlatform, _sourceConnectionString,
                                                   destDbPlatform, this.txtConnectionStringForOutputTables.Text,
                                                   this.chkReplaceExistingTables.Checked, PFTextProcessor.ConvertStringToInt(this.txtOutputBatchSize.Text, 120), this.txtOutputTablesSchema.Text);

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


        //private void GetStaticKeys()
        //{

        //    try
        //    {
        //        _msg.Length = 0;
        //        _msg.Append("GetStaticKeys started ...\r\n");
        //        Program._messageLog.WriteLine(_msg.ToString());

        //        _msg.Length = 0;
        //        _msg.Append("StaticKeySection values:\r\n");
        //        _msg.Append("MainFormCaption = ");
        //        _msg.Append(StaticKeysSection.Settings.MainFormCaption);
        //        _msg.Append("\r\n");
        //        _msg.Append("MinAppThreads = ");
        //        _msg.Append(StaticKeysSection.Settings.MinAppThreads.ToString());
        //        _msg.Append("\r\n");
        //        _msg.Append("MaxAppThreads = ");
        //        _msg.Append(StaticKeysSection.Settings.MaxAppThreads.ToString());
        //        _msg.Append("\r\n");
        //        _msg.Append("RequireLogon = ");
        //        _msg.Append(StaticKeysSection.Settings.RequireLogon.ToString());
        //        _msg.Append("\r\n");
        //        _msg.Append("ValidBooleanValues = ");
        //        _msg.Append(StaticKeysSection.Settings.ValidBooleanValues);
        //        _msg.Append("\r\n");
        //        Program._messageLog.WriteLine(_msg.ToString());
        //    }
        //    catch (System.Exception ex)
        //    {
        //        _msg.Length = 0;
        //        _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
        //        Program._messageLog.WriteLine(_msg.ToString());
        //        AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
        //    }
        //    finally
        //    {
        //        _msg.Length = 0;
        //        _msg.Append("\r\n... GetStaticKeys finished.");
        //        Program._messageLog.WriteLine(_msg.ToString());

        //    }
        //}

        private void SaveScreenLocations()
        {
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


        private void SetOutputConnectionString()
        {
            this.txtConnectionStringForOutputTables.Text = AppConfig.GetStringValueFromConfigFile("DefaultConnection_" + this.cboDatabaseTypeForOutputTables.Text, string.Empty);
        }

        private void SetOutputTablesSchema()
        {
            this.txtOutputTablesSchema.Text = AppConfig.GetStringValueFromConfigFile("DefaultSchema_" + this.cboDatabaseTypeForOutputTables.Text, string.Empty);
        }

        private void SetOutputBatchSizeForDbPlatform()
        {
            this.txtOutputBatchSize.Text = AppConfig.GetStringValueFromConfigFile("DefaultOutputBatchSize_" + this.cboDatabaseTypeForOutputTables.Text, "120");
        }

        private void DefineConnectionString()
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ConnectionStringPrompt cp = null;


            try
            {
                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForOutputTables.Text);
                connMgr = new PFConnectionManager();
                cp = new ConnectionStringPrompt(dbPlat, connMgr);
                cp.ConnectionString = this.txtConnectionStringForOutputTables.Text;
                System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
                if (res == DialogResult.OK)
                {
                    this.txtConnectionStringForOutputTables.Text = cp.ConnectionString;
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
                ;
            }

        }


    }//end class
}//end namespace
