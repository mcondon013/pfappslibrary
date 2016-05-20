using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFDataAccessObjects;
using PFConnectionObjects;
using PFConnectionStrings;
using PFMessageLogs;
using PFCollectionsObjects;

namespace PFDatabaseSelector
{
#pragma warning disable 1591
    public partial class DatabaseSelectorForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _userCancelButtonPressed = false;

        private bool _saveErrorMessagesToAppLog = false;
        private MessageLog _messageLog = null;
        private string _helpFilePath = string.Empty;

        //private variables for properties
        private bool _showInstalledDatabaseProvidersOnly = true;
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;


        //constructor
        public DatabaseSelectorForm()
        {
            InitializeComponent();
        }

        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
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
        /// Path to application help file.
        /// </summary>
        public string HelpFilePath
        {
            get
            {
                return _helpFilePath;
            }
            set
            {
                _helpFilePath = value;
            }
        }

        public DatabasePlatform DatabaseType
        {
            get
            {
                return (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForOutputTable.Text);
            }
            set
            {
                this.cboDatabaseTypeForOutputTable.Text = value.ToString();
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.txtConnectionStringForOutputTable.Text;
            }
            set
            {
                this.txtConnectionStringForOutputTable.Text = value;
            }
        }

        public string OutputTableName
        {
            get
            {
                return this.txtOutputTableName.Text;
            }
            set
            {
                this.txtOutputTableName.Text = value;
            }
        }

        public int OutputBatchSize
        {
            get
            {
                return Convert.ToInt32(this.txtOutputBatchSize.Text);
            }
            set
            {
                this.txtOutputBatchSize.Text = value.ToString();
            }
        }

        public bool ReplaceExistingTable
        {
            get
            {
                return this.chkReplaceExistingTable.Checked;
            }
            set
            {
                this.chkReplaceExistingTable.Checked = value;
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
        /// DefaultOutputDatabaseType Property.
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
        /// DefaultOutputDatabaseConnectionString Property.
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
        
        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (this.cboDatabaseTypeForOutputTable.Text.Trim().Length == 0
                || this.txtConnectionStringForOutputTable.Text.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a valid database provider and connection string if you click OK button. Click Cancel button to exit immediately without specifying a provider and connection string.");
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                HideForm();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            _userCancelButtonPressed = true;
            //DialogResult res = CheckCancelRequest();
            //if (res == DialogResult.Yes)
            //{
            //    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //    HideForm();
            //}
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            HideForm();
        }

        private void cmdBuildConnectionString_Click(object sender, EventArgs e)
        {
            BuildConnectionString();
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void PFWindowsForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private DialogResult CheckCancelRequest()
        {
            DialogResult res = AppMessages.DisplayMessage("Click yes to cancel and discard any changes you may have made to the column specifications.", "Cancel Data Randomizer Criteria ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }

        private void cboDatabaseTypeForOutputTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOutputConnectionString();
            SetOutputTableName();
            SetOutputBatchSizeForDbPlatform();
        }



        //common form processing routines
        public void InitializeForm()
        {
            _userCancelButtonPressed = false;
            EnableFormControls();
            InitFormValues();
        }

        private void InitFormValues()
        {

            bool showInstalledDatabaseProvidersOnly = this.ShowInstalledDatabaseProvidersOnly;
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

            this.cboDatabaseTypeForOutputTable.Items.Clear();
            foreach (stKeyValuePair<string, PFProviderDefinition> provDef in provDefs)
            {
                if (showInstalledDatabaseProvidersOnly)
                {
                    if (provDef.Value.InstallationStatus == enProviderInstallationStatus.IsInstalled)
                    {
                        //this.cboDatabaseTypeForOutputTable.Items.Add(provDef.Key);
                        if (provDef.Value.AvailableForSelection)
                        {
                            this.cboDatabaseTypeForOutputTable.Items.Add(provDef.Key);
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
                    this.cboDatabaseTypeForOutputTable.Items.Add(provDef.Key);
                }
            }

            SetOutputDatabaseProvider();
            SetOutputConnectionString();
            if (this.cboDatabaseTypeForOutputTable.Text.Trim().Length > 0)
            {
                SetOutputTableName();
                SetOutputBatchSizeForDbPlatform();
            }
            else
            {
                this.txtOutputTableName.Text = string.Empty;
                this.txtOutputBatchSize.Text = AppConfig.GetStringValueFromConfigFile("DefaultOutputDatabaseBatchSize", "120"); ;
            }
            this.chkReplaceExistingTable.Checked = AppConfig.GetBooleanValueFromConfigFile("ReplaceOutputTableIfExists", "False"); 
        }

        private void SetOutputDatabaseProvider()
        {
            this.cboDatabaseTypeForOutputTable.Text = this.DefaultOutputDatabaseType;
        }

        private void SetOutputConnectionString()
        {
            this.txtConnectionStringForOutputTable.Text = AppConfig.GetStringValueFromConfigFile("DefaultConnection_" + this.cboDatabaseTypeForOutputTable.Text, string.Empty);

        }

        private void SetOutputTableName()
        {
            string tabName = string.Empty;
            string tabSchema = string.Empty;
            tabName = AppConfig.GetStringValueFromConfigFile("DefaultOutputTableName", "OutputDataTable");
            tabSchema = AppConfig.GetStringValueFromConfigFile("DefaultSchema_" + this.cboDatabaseTypeForOutputTable.Text.Trim(), string.Empty);

            if (tabSchema.Trim().Length > 0 && tabName.Trim().Length > 0)
                this.txtOutputTableName.Text = tabSchema.Trim() + "." + tabName.Trim();
            else if (tabName.Trim().Length > 0)
                this.txtOutputTableName.Text = tabName.Trim();
            else
                this.txtOutputTableName.Text = string.Empty;
        }

        private void SetOutputBatchSizeForDbPlatform()
        {
            this.txtOutputBatchSize.Text = AppConfig.GetStringValueFromConfigFile("DefaultOutputBatchSize_" + this.cboDatabaseTypeForOutputTable.Text, "120");
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

        private void BuildConnectionString()
        {
            PFConnectionManager connMgr = null;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            ConnectionStringPrompt cp = null;


            try
            {
                dbPlat = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), this.cboDatabaseTypeForOutputTable.Text);
                connMgr = new PFConnectionManager();
                cp = new ConnectionStringPrompt(dbPlat, connMgr);
                cp.ConnectionString = this.txtConnectionStringForOutputTable.Text;
                System.Windows.Forms.DialogResult res = cp.ShowConnectionPrompt();
                if (res == DialogResult.OK)
                {
                    this.txtConnectionStringForOutputTable.Text = cp.ConnectionString;
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteLogMessage(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
        }

        private void WriteLogMessage(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }



    }//end class
#pragma warning restore 1591
}//end namespace
