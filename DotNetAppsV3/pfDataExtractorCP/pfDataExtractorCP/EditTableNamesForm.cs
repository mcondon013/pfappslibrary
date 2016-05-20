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
//using PFTextFiles;
using PFPrinterObjects;
using PFAppDataObjects;
using pfDataExtractorCPObjects;

namespace pfDataExtractorCP
{
#pragma warning disable 1591
    public partial class EditTableNamesForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        //fields for properties
        MessageLog _messageLog = null;

        PFRandomOrdersDefinition _randomOrderDefinition = new PFRandomOrdersDefinition();

        //constructors

        public EditTableNamesForm()
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
        /// RandomOrderDefinition Property.
        /// </summary>
        public PFRandomOrdersDefinition RandomOrderDefinition
        {
            get
            {
                return _randomOrderDefinition;
            }
            set
            {
                _randomOrderDefinition = value;
            }
        }


        //button click events
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void cmdAcceptForm_Click(object sender, EventArgs e)
        {
            ProcessForm();
            this.DialogResult = DialogResult.OK;
            HideForm();
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

        private void ShowHelpAbout()
        {
            HelpAboutForm appHelpAboutForm = new HelpAboutForm();
            appHelpAboutForm.ShowDialog();

        }

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Specify Order Table Names");
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

            if (FormHasUnsavedChanges())
            {
                DialogResult res = CheckCancelRequest();
                if (res == DialogResult.Yes)
                {
                    SaveFormValues();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }

        }

        private bool FormHasUnsavedChanges()
        {
            bool ret = false;

            if (this.txtTableSchema.Text != _randomOrderDefinition.TableSchema)
                ret = true;
            if (this.txtSalesOrderHeadersTableName.Text != _randomOrderDefinition.SalesOrderHeadersTableName)
                ret = true;
            if (this.txtSalesOrderDetailsTableName.Text != _randomOrderDefinition.SalesOrderDetailsTableName)
                ret = true;
            if (this.txtPurchaseOrderHeadersTableName.Text != _randomOrderDefinition.PurchaseOrderHeadersTableName)
                ret = true;
            if (this.txtPurchaseOrderDetailsTableName.Text != _randomOrderDefinition.PurchaseOrderDetailsTableName)
                ret = true;
            if (this.txtDwInternetSalesTableName.Text != _randomOrderDefinition.DwInternetSalesTableName)
                ret = true;
            if (this.txtDwResellerSalesTableName.Text != _randomOrderDefinition.DwResellerSalesTableName)
                ret = true;

            return ret;
        }

        private DialogResult CheckCancelRequest()
        {
            DialogResult res = AppMessages.DisplayMessage("Click Yes to save changes to the table names.", "Cancel Specify Table Names ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {

                this.Text = "Edit Table Names";

                SetLoggingValues();

                SetFormValues();

                SetSchemaName();

                SetHelpFileValues();

                _printer = new FormPrinter(this);

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

        private void SetFormValues()
        {
            this.txtTableSchema.Text = _randomOrderDefinition.TableSchema;
            this.txtSalesOrderHeadersTableName.Text = _randomOrderDefinition.SalesOrderHeadersTableName;
            this.txtSalesOrderDetailsTableName.Text = _randomOrderDefinition.SalesOrderDetailsTableName;
            this.txtPurchaseOrderHeadersTableName.Text = _randomOrderDefinition.PurchaseOrderHeadersTableName;
            this.txtPurchaseOrderDetailsTableName.Text = _randomOrderDefinition.PurchaseOrderDetailsTableName;
            this.txtDwInternetSalesTableName.Text = _randomOrderDefinition.DwInternetSalesTableName;
            this.txtDwResellerSalesTableName.Text = _randomOrderDefinition.DwResellerSalesTableName;
        }

        private void SetSchemaName()
        {

            this.txtTableSchema.Text = _randomOrderDefinition.TableSchema;

        }

        internal void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "pfFolderSize.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.tableNamesHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

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

        private void ShowPageSettings()
        {
            _printer.ShowPageSettings();
        }

        private void FilePrint(bool preview, bool showPrintDialog)
        {
            _printer.PageTitle = AppGlobals.AppInfo.AssemblyDescription;
            _printer.PageSubTitle = "Edit Table Names Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }

        private void ProcessForm()
        {
            SaveFormValues();
        }

        private void SaveFormValues()
        {
            _randomOrderDefinition.TableSchema = this.txtTableSchema.Text;
            _randomOrderDefinition.SalesOrderHeadersTableName = this.txtSalesOrderHeadersTableName.Text;
            _randomOrderDefinition.SalesOrderDetailsTableName = this.txtSalesOrderDetailsTableName.Text;
            _randomOrderDefinition.PurchaseOrderHeadersTableName = this.txtPurchaseOrderHeadersTableName.Text;
            _randomOrderDefinition.PurchaseOrderDetailsTableName = this.txtPurchaseOrderDetailsTableName.Text;
            _randomOrderDefinition.DwInternetSalesTableName = this.txtDwInternetSalesTableName.Text;
            _randomOrderDefinition.DwResellerSalesTableName = this.txtDwResellerSalesTableName.Text;
        }


        //application routines

        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }



    }//end class
#pragma warning restore 1591
}//end namespace
