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
using PFRandomDataExt;

namespace pfDataExtractorCP
{
#pragma warning disable 1591
    /// <summary>
    /// Form for definition of date ranges to include in test order transactions.
    /// </summary>
    public partial class OrderTransactionDatesForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = true;
        private string _appLogFileName = @"app.log";
        private string _helpFilePath = string.Empty;
        private TextEditor _textEditor = new TextEditor();
        private TextFormatter _textFormatter = new TextFormatter();
        private FormPrinter _printer = null;

        public delegate void OutputToGridDelegate(DataTable tab);
        public event OutputToGridDelegate OutputTableToGrid;


        //fields for properties
        MessageLog _messageLog = null;

        PFRandomOrdersDefinition _randomOrderDefinition = new PFRandomOrdersDefinition();

        //constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderTransactionDatesForm()
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

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            ProcessForm();
        }

        private void cmdEstimateTransactionRowCounts_Click(object sender, EventArgs e)
        {
            EstimateTransactionRowCounts();
        }

        private void cmdPreview_Click(object sender, EventArgs e)
        {
            PreviewData();
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
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Specify Test Order Transaction Dates and Counts");
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
                    string errMsg = VerifyFormValues();
                    if (errMsg.Length == 0)
                    {
                        SaveFormValues();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else
                    {
                        Program._messageLog.WriteLine(errMsg);
                        AppMessages.DisplayErrorMessage(errMsg);
                        e.Cancel = true;
                    }
                }
            }
        }

        private bool FormHasUnsavedChanges()
        {
            bool ret = false;

            if (this.txtEarliestTransactionDate.Text != _randomOrderDefinition.EarliestTransactionDate)
                ret = true;
            if (this.txtLatestTransactionDate.Text != _randomOrderDefinition.LatestTransactionDate)
                ret = true;
            if (this.chkIncludeWeekendDays.Checked != _randomOrderDefinition.IncludeWeekendDays)
                ret = true;
            if (this.txtMinNumSalesOrdersPerDate.Text != _randomOrderDefinition.MinNumSalesOrdersPerDate)
                ret = true;
            if (this.txtMaxNumSalesOrdersPerDate.Text != _randomOrderDefinition.MaxNumSalesOrdersPerDate)
                ret = true;
            if (this.txtMinNumPurchaseOrdersPerDate.Text != _randomOrderDefinition.MinNumPurchaseOrdersPerDate)
                ret = true;
            if (this.txtMaxNumPurchaseOrdersPerDate.Text != _randomOrderDefinition.MaxNumPurchaseOrdersPerDate)
                ret = true;
            if (this.txtMinTimePerDate.Text != _randomOrderDefinition.MinTimePerDate)
                ret = true;
            if (this.txtMaxTimePerDate.Text != _randomOrderDefinition.MaxTimePerDate)
                ret = true;

            return ret;
        }


        private DialogResult CheckCancelRequest()
        {
            DialogResult res = AppMessages.DisplayMessage("Click Yes to save changes to the transaction date specifications.", "Cancel Specify Transaction Dates and Counts ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            string configValue = string.Empty;

            try
            {

                this.Text = "Specify Order Transaction Dates";

                SetLoggingValues();

                SetHelpFileValues();

                SetFormValues();

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

        internal void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "pfFolderSize.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        private void SetFormValues()
        {
            this.txtNumPreviewDates.Text = AppConfig.GetStringValueFromConfigFile("NumberOfRandomValueSamples", "1200");

            this.txtEarliestTransactionDate.Text = _randomOrderDefinition.EarliestTransactionDate;
            this.txtLatestTransactionDate.Text = _randomOrderDefinition.LatestTransactionDate;
            this.chkIncludeWeekendDays.Checked = _randomOrderDefinition.IncludeWeekendDays;
            this.txtMinNumSalesOrdersPerDate.Text = _randomOrderDefinition.MinNumSalesOrdersPerDate;
            this.txtMaxNumSalesOrdersPerDate.Text = _randomOrderDefinition.MaxNumSalesOrdersPerDate;
            this.txtMinNumPurchaseOrdersPerDate.Text = _randomOrderDefinition.MinNumPurchaseOrdersPerDate;
            this.txtMaxNumPurchaseOrdersPerDate.Text = _randomOrderDefinition.MaxNumPurchaseOrdersPerDate;
            this.txtMinTimePerDate.Text = _randomOrderDefinition.MinTimePerDate;
            this.txtMaxTimePerDate.Text = _randomOrderDefinition.MaxTimePerDate;


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
            _printer.PageSubTitle = "Order Transaction Dates Form";
            _printer.PageFooter = AppGlobals.AppInfo.AssemblyProduct;
            _printer.ShowPageNumbers = true;
            _printer.ShowTotalPageNumber = true;

            _printer.Print(preview, showPrintDialog);

        }


        //application routines

        private void ProcessForm()
        {
            string errMsg = VerifyFormValues();
            if (errMsg.Length > 0)
            {
                Program._messageLog.WriteLine(errMsg);
                AppMessages.DisplayErrorMessage(errMsg);
            }
            else
            {
                SaveFormValues();
                this.DialogResult = DialogResult.OK;
                HideForm();
            }

        }


        private void SaveFormValues()
        {
            _randomOrderDefinition.EarliestTransactionDate = this.txtEarliestTransactionDate.Text;
            _randomOrderDefinition.LatestTransactionDate = this.txtLatestTransactionDate.Text;
            _randomOrderDefinition.IncludeWeekendDays = this.chkIncludeWeekendDays.Checked;
            _randomOrderDefinition.MinNumSalesOrdersPerDate = this.txtMinNumSalesOrdersPerDate.Text;
            _randomOrderDefinition.MaxNumSalesOrdersPerDate = this.txtMaxNumSalesOrdersPerDate.Text;
            _randomOrderDefinition.MinNumPurchaseOrdersPerDate = this.txtMinNumPurchaseOrdersPerDate.Text;
            _randomOrderDefinition.MaxNumPurchaseOrdersPerDate = this.txtMaxNumPurchaseOrdersPerDate.Text;
            _randomOrderDefinition.MinTimePerDate = this.txtMinTimePerDate.Text;
            _randomOrderDefinition.MaxTimePerDate = this.txtMaxTimePerDate.Text;
        }

        private string VerifyFormValues()
        {
            StringBuilder errMsg = new StringBuilder();
            bool earliestTransactionDateIsValid = true;
            bool latestTransactionDateIsValid = true;
            bool transactionDateRangeIsValid = true;
            bool minNumSalesOrdersPerDateIsValid = true;
            bool maxNumSalesOrdersPerDateIsValid = true;
            bool salesOrdersRangeIsValid = true;
            bool minNumPurchaseOrdersPerDateIsValid = true;
            bool maxNumPurchaseOrdersPerDateIsValid = true;
            bool purchaseOrdersRangeIsValid = true;
            bool minTimePerDateIsValid = true;
            bool maxTimePerDateIsValid = true;
            bool timeRangeIsValid = true;
            bool numPreviewDatesIsValid = true;

            int intNum = 0;
            int minNum = 0;
            int maxNum = 0;

            errMsg.Length = 0;

            DateTime earliestDate = DateTime.MinValue;
            DateTime latestDate = DateTime.MaxValue;
            earliestTransactionDateIsValid = DateTime.TryParse(this.txtEarliestTransactionDate.Text, out earliestDate);
            latestTransactionDateIsValid = DateTime.TryParse(this.txtLatestTransactionDate.Text, out latestDate);
            if (earliestTransactionDateIsValid && latestTransactionDateIsValid)
            {
                if (earliestDate > latestDate)
                    transactionDateRangeIsValid = false;
            }

            minNumSalesOrdersPerDateIsValid = int.TryParse(this.txtMinNumSalesOrdersPerDate.Text, out minNum);
            maxNumSalesOrdersPerDateIsValid = int.TryParse(this.txtMaxNumSalesOrdersPerDate.Text, out maxNum);
            if (minNumSalesOrdersPerDateIsValid && maxNumSalesOrdersPerDateIsValid)
            {
                if (minNum > maxNum)
                    salesOrdersRangeIsValid = false;
            }

            minNumPurchaseOrdersPerDateIsValid = int.TryParse(this.txtMinNumPurchaseOrdersPerDate.Text, out minNum);
            maxNumPurchaseOrdersPerDateIsValid = int.TryParse(this.txtMaxNumPurchaseOrdersPerDate.Text, out maxNum);
            if (minNumPurchaseOrdersPerDateIsValid && maxNumPurchaseOrdersPerDateIsValid)
            {
                if (minNum > maxNum)
                    purchaseOrdersRangeIsValid = false;
            }

            TimeSpan earliestTime = TimeSpan.MinValue;
            TimeSpan latestTime = TimeSpan.MaxValue;
            minTimePerDateIsValid = TimeSpan.TryParse(this.txtMinTimePerDate.Text, out earliestTime);
            maxTimePerDateIsValid = TimeSpan.TryParse(this.txtMaxTimePerDate.Text, out latestTime);
            if (minTimePerDateIsValid && maxTimePerDateIsValid)
            {
                if (earliestTime > latestTime)
                    timeRangeIsValid = false;
            }

            numPreviewDatesIsValid = int.TryParse(this.txtNumPreviewDates.Text, out intNum);

            if (earliestTransactionDateIsValid == false)
            {
                errMsg.Append("Invalid earliest transaction date: ");
                errMsg.Append(this.txtEarliestTransactionDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (latestTransactionDateIsValid == false)
            {
                errMsg.Append("Invalid latest transaction date: ");
                errMsg.Append(this.txtLatestTransactionDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (transactionDateRangeIsValid == false)
            {
                errMsg.Append("Transaction date range is invalid: ");
                errMsg.Append(this.txtEarliestTransactionDate.Text);
                errMsg.Append(" to ");
                errMsg.Append(this.txtLatestTransactionDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (minNumSalesOrdersPerDateIsValid == false)
            {
                errMsg.Append("Minimum number of sales orders per date is invalid: ");
                errMsg.Append(this.txtMinNumSalesOrdersPerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (maxNumSalesOrdersPerDateIsValid == false)
            {
                errMsg.Append("Maximum number of sales orders per date is invalid: ");
                errMsg.Append(this.txtMaxNumSalesOrdersPerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (salesOrdersRangeIsValid == false)
            {
                errMsg.Append("Sales orders occurance range is invalid: ");
                errMsg.Append(this.txtMinNumSalesOrdersPerDate.Text);
                errMsg.Append(" to ");
                errMsg.Append(this.txtMaxNumSalesOrdersPerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (minNumPurchaseOrdersPerDateIsValid == false)
            {
                errMsg.Append("Minimum number of purchase orders per date is invalid: ");
                errMsg.Append(this.txtMinNumPurchaseOrdersPerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (maxNumPurchaseOrdersPerDateIsValid == false)
            {
                errMsg.Append("Maximum number of purchase orders per date is invalid: ");
                errMsg.Append(this.txtMaxNumPurchaseOrdersPerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (purchaseOrdersRangeIsValid == false)
            {
                errMsg.Append("Purchase orders occurance range is invalid: ");
                errMsg.Append(this.txtMinNumPurchaseOrdersPerDate.Text);
                errMsg.Append(" to ");
                errMsg.Append(this.txtMaxNumPurchaseOrdersPerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (minTimePerDateIsValid == false)
            {
                errMsg.Append("Invalid minimum time for each date: ");
                errMsg.Append(this.txtMinTimePerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (maxTimePerDateIsValid == false)
            {
                errMsg.Append("Invalid maximum time for each date: ");
                errMsg.Append(this.txtMaxTimePerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (timeRangeIsValid == false)
            {
                errMsg.Append("Transaction time range per date is invalid: ");
                errMsg.Append(this.txtMinTimePerDate.Text);
                errMsg.Append(" to ");
                errMsg.Append(this.txtMaxTimePerDate.Text);
                errMsg.Append(Environment.NewLine);
            }
            if (numPreviewDatesIsValid == false)
            {
                errMsg.Append("Number of preview dates is invalid: ");
                errMsg.Append(this.txtNumPreviewDates.Text);
                errMsg.Append(Environment.NewLine);
            }


            return errMsg.ToString();
        }

        private void WriteToMessageLog(string message)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(message);
            }
        }

        private void EstimateTransactionRowCounts()
        {
            string errMsg = VerifyFormValues();
            if (errMsg.Length > 0)
            {
                WriteToMessageLog(errMsg);
                AppMessages.DisplayErrorMessage(errMsg);
                return;
            }


            try
            {
                _msg.Length = 0;

                DateTime earliestTxDate = AppTextGlobals.ConvertStringToDateTime(this.txtEarliestTransactionDate.Text, DateTime.MinValue);
                DateTime latestTxDate = AppTextGlobals.ConvertStringToDateTime(this.txtLatestTransactionDate.Text, DateTime.MinValue);
                TimeSpan dateDiff = latestTxDate.Subtract(earliestTxDate);
                double numDays = dateDiff.TotalDays;
                double numWeeks = numDays / 7.0;
                if (this.chkIncludeWeekendDays.Checked == false)
                {
                    numDays = numDays - (numWeeks * 2.0);
                }
                _msg.Append("Estimated number of days:                ");
                _msg.Append(numDays.ToString("#,##0"));
                _msg.Append(Environment.NewLine);

                double minNum = AppTextGlobals.ConvertStringToDouble(this.txtMinNumSalesOrdersPerDate.Text, 0.0);
                double maxNum = AppTextGlobals.ConvertStringToDouble(this.txtMaxNumSalesOrdersPerDate.Text, 0.0);
                double numSalesTxs = numDays * ((minNum + maxNum) / 2.0);
                double numSalesDetails = numSalesTxs * 3.0;
                double numInternetSalesDetails = numSalesTxs * 2.0;
                double numResellerSalesDetails = numSalesTxs * 16.0;

                _msg.Append("Estimated number of sales order headers:    ");
                _msg.Append(numSalesTxs.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("Estimated number of sales order details:    ");
                _msg.Append(numSalesDetails.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("Estimated number of internet order details: ");
                _msg.Append(numInternetSalesDetails.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("Estimated number of reseller order details: ");
                _msg.Append(numResellerSalesDetails.ToString("#,##0"));
                _msg.Append(Environment.NewLine);

                minNum = AppTextGlobals.ConvertStringToDouble(this.txtMinNumPurchaseOrdersPerDate.Text, 0.0);
                maxNum = AppTextGlobals.ConvertStringToDouble(this.txtMaxNumPurchaseOrdersPerDate.Text, 0.0);
                double numPurchaseTxs = numDays * ((minNum + maxNum) / 2.0);
                double numPurchaseDetails = numPurchaseTxs * 2.0;

                _msg.Append("Estimated number of purchase order headers: ");
                _msg.Append(numPurchaseTxs.ToString("#,##0"));
                _msg.Append(Environment.NewLine);
                _msg.Append("Estimated number of purchase order details: ");
                _msg.Append(numPurchaseDetails.ToString("#,##0"));
                _msg.Append(Environment.NewLine);


                WriteToMessageLog(_msg.ToString());
                AppMessages.DisplayInfoMessage(_msg.ToString());

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

        private void PreviewData()
        {
            if (OutputTableToGrid == null)
                throw new System.Exception("OutputTableToGrid delegate not set.");

            DataTable tab = CreateOrderTransactionDatesPreviewTable();
            if(tab != null)
                OutputTableToGrid(tab);
        }

        private DataTable CreateOrderTransactionDatesPreviewTable()
        {
            DataTable dt = new DataTable();
            RandomNumber rnd = new RandomNumber();
            int numPreviewDates = 10;
            DateTime earliestDate = DateTime.MinValue;
            DateTime latestDate = DateTime.MinValue;
            DateTime currDate = DateTime.MinValue;
            bool includeWeekendDays = this.chkIncludeWeekendDays.Checked;
            int minNumSalesOrdersPerDate = 1;
            int maxNumSalesOrdersPerDate = 1;
            int minNumPurchaseOrdersPerDate = 1;
            int maxNumPurchaseOrdersPerDate = 1;
            TimeSpan minTimePerDate = TimeSpan.MinValue;
            TimeSpan maxTimePerDate = TimeSpan.MinValue;
            int minSecondsPerDate = 1;
            int maxSecondsPerDate = 1;
            int numSalesOrdersForDate = 0;
            int numPurchaseOrdersForDate = 0;
            int numSecondsForTx = 0;
            TimeSpan txTime = TimeSpan.MinValue;
            StringBuilder timeList = new StringBuilder();

            string errMsg = VerifyFormValues();
            if (errMsg.Length > 0)
            {
                WriteToMessageLog(errMsg);
                AppMessages.DisplayErrorMessage(errMsg);
                return null;
            }


            DataColumn dc0 = new DataColumn();
            dc0.ColumnName = "DayNum";
            dc0.DataType = Type.GetType("System.Int32");
            dc0.AutoIncrement = true;
            dc0.AutoIncrementSeed=1;
            dc0.AutoIncrementStep = 1;

            DataColumn dc1 = new DataColumn();
            dc1.ColumnName = "DayOfWeek";
            dc1.DataType = Type.GetType("System.String");
            dc1.MaxLength = 255;

            DataColumn dc2 = new DataColumn();
            dc2.ColumnName = "DateAndTime";
            dc2.DataType = Type.GetType("System.DateTime");

            DataColumn dc3 = new DataColumn();
            dc3.ColumnName = "NumSalesOrders";
            dc3.DataType = Type.GetType("System.Int32");

            DataColumn dc4 = new DataColumn();
            dc4.ColumnName = "NumPurchaseOrders";
            dc4.DataType = Type.GetType("System.Int32");

            DataColumn dc5 = new DataColumn();
            dc5.ColumnName = "Tx Times";
            dc5.DataType = Type.GetType("System.String");
            dc5.MaxLength = Int16.MaxValue;

            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);

            numPreviewDates = AppTextGlobals.ConvertStringToInt(this.txtNumPreviewDates.Text, -1);
            earliestDate = AppTextGlobals.ConvertStringToDateTime(this.txtEarliestTransactionDate.Text, DateTime.MinValue);
            latestDate = AppTextGlobals.ConvertStringToDateTime(this.txtLatestTransactionDate.Text, DateTime.MinValue);
            currDate = earliestDate;
            minNumSalesOrdersPerDate = AppTextGlobals.ConvertStringToInt(this.txtMinNumSalesOrdersPerDate.Text, 1);
            maxNumSalesOrdersPerDate = AppTextGlobals.ConvertStringToInt(this.txtMaxNumSalesOrdersPerDate.Text, 1);
            minNumPurchaseOrdersPerDate = AppTextGlobals.ConvertStringToInt(this.txtMinNumPurchaseOrdersPerDate.Text, 1);
            maxNumPurchaseOrdersPerDate = AppTextGlobals.ConvertStringToInt(this.txtMaxNumPurchaseOrdersPerDate.Text, 1);
            minTimePerDate = AppTextGlobals.ConvertStringToTimeSpan(this.txtMinTimePerDate.Text, "00:00:00");
            maxTimePerDate = AppTextGlobals.ConvertStringToTimeSpan(this.txtMaxTimePerDate.Text, "00:00:00");
            minSecondsPerDate = (int)minTimePerDate.TotalSeconds;
            maxSecondsPerDate = (int)maxTimePerDate.TotalSeconds;

            for (int r = 0; r < numPreviewDates; r++)
            {
                numSalesOrdersForDate = rnd.GenerateRandomInt(minNumSalesOrdersPerDate, maxNumSalesOrdersPerDate);
                numPurchaseOrdersForDate = rnd.GenerateRandomInt(minNumPurchaseOrdersPerDate, maxNumPurchaseOrdersPerDate);

                DataRow dr = dt.NewRow();
                dr[1] = currDate.DayOfWeek.ToString();
                dr[2] = currDate;
                dr[3] = numSalesOrdersForDate;
                dr[4] = numPurchaseOrdersForDate;

                timeList.Length = 0;

                for (int so = 0; so < numSalesOrdersForDate; so++)
                {
                    numSecondsForTx = rnd.GenerateRandomInt(minSecondsPerDate, maxSecondsPerDate);
                    DateTime txDate = currDate.AddSeconds(numSecondsForTx);
                    timeList.Append("SO at ");
                    timeList.Append(txDate.ToString("HH:mm:ss"));
                    timeList.Append(", ");
                }

                for (int po = 0; po < numPurchaseOrdersForDate; po++)
                {
                    numSecondsForTx = rnd.GenerateRandomInt(minSecondsPerDate, maxSecondsPerDate);
                    DateTime txDate = currDate.AddSeconds(numSecondsForTx);
                    timeList.Append("PO at ");
                    timeList.Append(txDate.ToString("HH:mm:ss"));
                    timeList.Append(", ");
                }

                dr[5] = timeList.ToString().TrimEnd(' ').TrimEnd(',');

                dt.Rows.Add(dr);

                //increment counters and dates to continue loop
                currDate = currDate.AddDays(1);
                if (currDate > latestDate)
                {
                    break;
                }
                if (currDate.DayOfWeek == DayOfWeek.Sunday
                    && includeWeekendDays == false)
                {
                    currDate = currDate.AddDays(1);
                }
                else if (currDate.DayOfWeek == DayOfWeek.Saturday
                    && includeWeekendDays == false)
                {
                    currDate = currDate.AddDays(2);
                }
                else
                {
                    //keep the previously calculated date
                    ;
                }
            }


            return dt;
        }


    }//end class
#pragma warning restore 1591
}//end namespace
