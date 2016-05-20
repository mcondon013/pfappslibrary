namespace pfDataExtractorCP
{
#pragma warning disable 1591
    partial class OrderTransactionDatesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderTransactionDatesForm));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mainMenuToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.lblNumberOfPurchaseOrdersPerDate = new System.Windows.Forms.Label();
            this.lblNumberOfSalesOrdersPerDate = new System.Windows.Forms.Label();
            this.cmdPreview = new System.Windows.Forms.Button();
            this.pnlBusinessTrasactionsDates = new System.Windows.Forms.Panel();
            this.lblPurchaseOrdersPerDate = new System.Windows.Forms.Label();
            this.cmdEstimateTransactionRowCounts = new System.Windows.Forms.Button();
            this.txtMaxNumPurchaseOrdersPerDate = new System.Windows.Forms.TextBox();
            this.txtMinNumPurchaseOrdersPerDate = new System.Windows.Forms.TextBox();
            this.lblMaxNumPurchaseOrdersPerDate = new System.Windows.Forms.Label();
            this.lblMinNumPurchaseOrdersPerDate = new System.Windows.Forms.Label();
            this.chkIncludeWeekendDays = new System.Windows.Forms.CheckBox();
            this.lblTimesPerDate = new System.Windows.Forms.Label();
            this.txtMaxTimePerDate = new System.Windows.Forms.TextBox();
            this.txtMinTimePerDate = new System.Windows.Forms.TextBox();
            this.lblMaxTimePerDate = new System.Windows.Forms.Label();
            this.lblMinTimePerDate = new System.Windows.Forms.Label();
            this.lblTimeRangeForTransactionsToOccur = new System.Windows.Forms.Label();
            this.lblSalesOrdersPerDate = new System.Windows.Forms.Label();
            this.txtMaxNumSalesOrdersPerDate = new System.Windows.Forms.TextBox();
            this.txtMinNumSalesOrdersPerDate = new System.Windows.Forms.TextBox();
            this.lblMaxNumSalesOrdersPerDate = new System.Windows.Forms.Label();
            this.lblMinNumSalesOrdersPerDate = new System.Windows.Forms.Label();
            this.txtLatestTransactionDate = new System.Windows.Forms.TextBox();
            this.lblLatestTransactionDate = new System.Windows.Forms.Label();
            this.txtEarliestTransactionDate = new System.Windows.Forms.TextBox();
            this.lblEarliestTransactionDate = new System.Windows.Forms.Label();
            this.lblOrderTransactionsDates = new System.Windows.Forms.Label();
            this.lblNumPreviewRows = new System.Windows.Forms.Label();
            this.txtNumPreviewDates = new System.Windows.Forms.TextBox();
            this.pnlPreviewOutput = new System.Windows.Forms.Panel();
            this.MainMenu.SuspendLayout();
            this.pnlBusinessTrasactionsDates.SuspendLayout();
            this.pnlPreviewOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdCancel, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdCancel, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdCancel.Location = new System.Drawing.Point(367, 296);
            this.cmdCancel.Name = "cmdCancel";
            this.appHelpProvider.SetShowHelp(this.cmdCancel, true);
            this.cmdCancel.Size = new System.Drawing.Size(111, 37);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "&Cancel";
            this.mainMenuToolTips.SetToolTip(this.cmdCancel, "Close form and exit application");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAccept
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdAccept, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdAccept, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdAccept, "Help for Run Tests: See Help File.");
            this.cmdAccept.Location = new System.Drawing.Point(367, 41);
            this.cmdAccept.Name = "cmdAccept";
            this.appHelpProvider.SetShowHelp(this.cmdAccept, true);
            this.cmdAccept.Size = new System.Drawing.Size(111, 37);
            this.cmdAccept.TabIndex = 1;
            this.cmdAccept.Text = "&Accept";
            this.mainMenuToolTips.SetToolTip(this.cmdAccept, "Run selected tests");
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp,
            this.toolbarHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(500, 27);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFilePageSetup,
            this.mnuFilePrint,
            this.mnuFilePrintPreview,
            this.toolStripSeparator13,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 23);
            this.mnuFile.Text = "&File";
            // 
            // mnuFilePageSetup
            // 
            this.mnuFilePageSetup.Name = "mnuFilePageSetup";
            this.mnuFilePageSetup.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePageSetup.Text = "Page Set&up";
            this.mnuFilePageSetup.Click += new System.EventHandler(this.mnuFilePageSetup_Click);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePrint.Text = "&Print";
            this.mnuFilePrint.Click += new System.EventHandler(this.mnuFilePrint_Click);
            // 
            // mnuFilePrintPreview
            // 
            this.mnuFilePrintPreview.Name = "mnuFilePrintPreview";
            this.mnuFilePrintPreview.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePrintPreview.Text = "Print Pre&view";
            this.mnuFilePrintPreview.Click += new System.EventHandler(this.mnuFilePrintPreview_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(143, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 23);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // toolbarHelp
            // 
            this.toolbarHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolbarHelp.Image")));
            this.toolbarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarHelp.Name = "toolbarHelp";
            this.toolbarHelp.Padding = new System.Windows.Forms.Padding(0, 0, 70, 0);
            this.toolbarHelp.Size = new System.Drawing.Size(90, 20);
            this.toolbarHelp.Text = "Help";
            this.toolbarHelp.Click += new System.EventHandler(this.toolbarHelp_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\pfDataExtractorCP\\InitWinFormsAppWithToolbar" +
    "\\pfDataExtractorCP.chm";
            // 
            // lblNumberOfPurchaseOrdersPerDate
            // 
            this.lblNumberOfPurchaseOrdersPerDate.AutoSize = true;
            this.lblNumberOfPurchaseOrdersPerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfPurchaseOrdersPerDate.Location = new System.Drawing.Point(13, 156);
            this.lblNumberOfPurchaseOrdersPerDate.Name = "lblNumberOfPurchaseOrdersPerDate";
            this.lblNumberOfPurchaseOrdersPerDate.Size = new System.Drawing.Size(265, 13);
            this.lblNumberOfPurchaseOrdersPerDate.TabIndex = 22;
            this.lblNumberOfPurchaseOrdersPerDate.Text = "Number of Purchase Orders to Generate for each Date";
            this.mainMenuToolTips.SetToolTip(this.lblNumberOfPurchaseOrdersPerDate, "Number of transactions for sales and purchase order tables");
            // 
            // lblNumberOfSalesOrdersPerDate
            // 
            this.lblNumberOfSalesOrdersPerDate.AutoSize = true;
            this.lblNumberOfSalesOrdersPerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfSalesOrdersPerDate.Location = new System.Drawing.Point(13, 106);
            this.lblNumberOfSalesOrdersPerDate.Name = "lblNumberOfSalesOrdersPerDate";
            this.lblNumberOfSalesOrdersPerDate.Size = new System.Drawing.Size(246, 13);
            this.lblNumberOfSalesOrdersPerDate.TabIndex = 9;
            this.lblNumberOfSalesOrdersPerDate.Text = "Number of Sales Orders to Generate for each Date";
            this.mainMenuToolTips.SetToolTip(this.lblNumberOfSalesOrdersPerDate, "Number of transactions for sales and purchase order tables");
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(-1, -1);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(111, 34);
            this.cmdPreview.TabIndex = 6;
            this.cmdPreview.Text = "&Preview";
            this.mainMenuToolTips.SetToolTip(this.cmdPreview, "Preview Transaction Dates to be Generated");
            this.cmdPreview.UseVisualStyleBackColor = true;
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // pnlBusinessTrasactionsDates
            // 
            this.pnlBusinessTrasactionsDates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblPurchaseOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblNumberOfPurchaseOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.cmdEstimateTransactionRowCounts);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtMaxNumPurchaseOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtMinNumPurchaseOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblMaxNumPurchaseOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblMinNumPurchaseOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.chkIncludeWeekendDays);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblTimesPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtMaxTimePerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtMinTimePerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblMaxTimePerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblMinTimePerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblTimeRangeForTransactionsToOccur);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblSalesOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblNumberOfSalesOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtMaxNumSalesOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtMinNumSalesOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblMaxNumSalesOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblMinNumSalesOrdersPerDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtLatestTransactionDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblLatestTransactionDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.txtEarliestTransactionDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblEarliestTransactionDate);
            this.pnlBusinessTrasactionsDates.Controls.Add(this.lblOrderTransactionsDates);
            this.pnlBusinessTrasactionsDates.Location = new System.Drawing.Point(21, 41);
            this.pnlBusinessTrasactionsDates.Name = "pnlBusinessTrasactionsDates";
            this.pnlBusinessTrasactionsDates.Size = new System.Drawing.Size(328, 292);
            this.pnlBusinessTrasactionsDates.TabIndex = 29;
            // 
            // lblPurchaseOrdersPerDate
            // 
            this.lblPurchaseOrdersPerDate.AutoSize = true;
            this.lblPurchaseOrdersPerDate.Location = new System.Drawing.Point(260, 175);
            this.lblPurchaseOrdersPerDate.Name = "lblPurchaseOrdersPerDate";
            this.lblPurchaseOrdersPerDate.Size = new System.Drawing.Size(48, 13);
            this.lblPurchaseOrdersPerDate.TabIndex = 23;
            this.lblPurchaseOrdersPerDate.Text = "per Date";
            // 
            // cmdEstimateTransactionRowCounts
            // 
            this.cmdEstimateTransactionRowCounts.Location = new System.Drawing.Point(62, 254);
            this.cmdEstimateTransactionRowCounts.Name = "cmdEstimateTransactionRowCounts";
            this.cmdEstimateTransactionRowCounts.Size = new System.Drawing.Size(197, 23);
            this.cmdEstimateTransactionRowCounts.TabIndex = 15;
            this.cmdEstimateTransactionRowCounts.Text = "Estimate Transaction Row Counts";
            this.cmdEstimateTransactionRowCounts.UseVisualStyleBackColor = true;
            this.cmdEstimateTransactionRowCounts.Click += new System.EventHandler(this.cmdEstimateTransactionRowCounts_Click);
            // 
            // txtMaxNumPurchaseOrdersPerDate
            // 
            this.txtMaxNumPurchaseOrdersPerDate.Location = new System.Drawing.Point(168, 171);
            this.txtMaxNumPurchaseOrdersPerDate.Name = "txtMaxNumPurchaseOrdersPerDate";
            this.txtMaxNumPurchaseOrdersPerDate.Size = new System.Drawing.Size(86, 20);
            this.txtMaxNumPurchaseOrdersPerDate.TabIndex = 21;
            // 
            // txtMinNumPurchaseOrdersPerDate
            // 
            this.txtMinNumPurchaseOrdersPerDate.Location = new System.Drawing.Point(49, 171);
            this.txtMinNumPurchaseOrdersPerDate.Name = "txtMinNumPurchaseOrdersPerDate";
            this.txtMinNumPurchaseOrdersPerDate.Size = new System.Drawing.Size(91, 20);
            this.txtMinNumPurchaseOrdersPerDate.TabIndex = 20;
            // 
            // lblMaxNumPurchaseOrdersPerDate
            // 
            this.lblMaxNumPurchaseOrdersPerDate.AutoSize = true;
            this.lblMaxNumPurchaseOrdersPerDate.Location = new System.Drawing.Point(146, 175);
            this.lblMaxNumPurchaseOrdersPerDate.Name = "lblMaxNumPurchaseOrdersPerDate";
            this.lblMaxNumPurchaseOrdersPerDate.Size = new System.Drawing.Size(20, 13);
            this.lblMaxNumPurchaseOrdersPerDate.TabIndex = 19;
            this.lblMaxNumPurchaseOrdersPerDate.Text = "To";
            // 
            // lblMinNumPurchaseOrdersPerDate
            // 
            this.lblMinNumPurchaseOrdersPerDate.AutoSize = true;
            this.lblMinNumPurchaseOrdersPerDate.Location = new System.Drawing.Point(16, 175);
            this.lblMinNumPurchaseOrdersPerDate.Name = "lblMinNumPurchaseOrdersPerDate";
            this.lblMinNumPurchaseOrdersPerDate.Size = new System.Drawing.Size(30, 13);
            this.lblMinNumPurchaseOrdersPerDate.TabIndex = 18;
            this.lblMinNumPurchaseOrdersPerDate.Text = "From";
            // 
            // chkIncludeWeekendDays
            // 
            this.chkIncludeWeekendDays.AutoSize = true;
            this.chkIncludeWeekendDays.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeWeekendDays.Location = new System.Drawing.Point(15, 78);
            this.chkIncludeWeekendDays.Name = "chkIncludeWeekendDays";
            this.chkIncludeWeekendDays.Size = new System.Drawing.Size(150, 17);
            this.chkIncludeWeekendDays.TabIndex = 17;
            this.chkIncludeWeekendDays.Text = "Include Weekend Days    ";
            this.chkIncludeWeekendDays.UseVisualStyleBackColor = true;
            // 
            // lblTimesPerDate
            // 
            this.lblTimesPerDate.AutoSize = true;
            this.lblTimesPerDate.Location = new System.Drawing.Point(260, 222);
            this.lblTimesPerDate.Name = "lblTimesPerDate";
            this.lblTimesPerDate.Size = new System.Drawing.Size(48, 13);
            this.lblTimesPerDate.TabIndex = 16;
            this.lblTimesPerDate.Text = "per Date";
            // 
            // txtMaxTimePerDate
            // 
            this.txtMaxTimePerDate.Location = new System.Drawing.Point(168, 218);
            this.txtMaxTimePerDate.Name = "txtMaxTimePerDate";
            this.txtMaxTimePerDate.Size = new System.Drawing.Size(86, 20);
            this.txtMaxTimePerDate.TabIndex = 15;
            // 
            // txtMinTimePerDate
            // 
            this.txtMinTimePerDate.Location = new System.Drawing.Point(50, 216);
            this.txtMinTimePerDate.Name = "txtMinTimePerDate";
            this.txtMinTimePerDate.Size = new System.Drawing.Size(91, 20);
            this.txtMinTimePerDate.TabIndex = 14;
            // 
            // lblMaxTimePerDate
            // 
            this.lblMaxTimePerDate.AutoSize = true;
            this.lblMaxTimePerDate.Location = new System.Drawing.Point(145, 221);
            this.lblMaxTimePerDate.Name = "lblMaxTimePerDate";
            this.lblMaxTimePerDate.Size = new System.Drawing.Size(20, 13);
            this.lblMaxTimePerDate.TabIndex = 13;
            this.lblMaxTimePerDate.Text = "To";
            // 
            // lblMinTimePerDate
            // 
            this.lblMinTimePerDate.AutoSize = true;
            this.lblMinTimePerDate.Location = new System.Drawing.Point(17, 220);
            this.lblMinTimePerDate.Name = "lblMinTimePerDate";
            this.lblMinTimePerDate.Size = new System.Drawing.Size(30, 13);
            this.lblMinTimePerDate.TabIndex = 12;
            this.lblMinTimePerDate.Text = "From";
            // 
            // lblTimeRangeForTransactionsToOccur
            // 
            this.lblTimeRangeForTransactionsToOccur.AutoSize = true;
            this.lblTimeRangeForTransactionsToOccur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRangeForTransactionsToOccur.Location = new System.Drawing.Point(13, 202);
            this.lblTimeRangeForTransactionsToOccur.Name = "lblTimeRangeForTransactionsToOccur";
            this.lblTimeRangeForTransactionsToOccur.Size = new System.Drawing.Size(191, 13);
            this.lblTimeRangeForTransactionsToOccur.TabIndex = 11;
            this.lblTimeRangeForTransactionsToOccur.Text = "Time Range for Transactions to Occurr";
            // 
            // lblSalesOrdersPerDate
            // 
            this.lblSalesOrdersPerDate.AutoSize = true;
            this.lblSalesOrdersPerDate.Location = new System.Drawing.Point(260, 125);
            this.lblSalesOrdersPerDate.Name = "lblSalesOrdersPerDate";
            this.lblSalesOrdersPerDate.Size = new System.Drawing.Size(48, 13);
            this.lblSalesOrdersPerDate.TabIndex = 10;
            this.lblSalesOrdersPerDate.Text = "per Date";
            // 
            // txtMaxNumSalesOrdersPerDate
            // 
            this.txtMaxNumSalesOrdersPerDate.Location = new System.Drawing.Point(168, 121);
            this.txtMaxNumSalesOrdersPerDate.Name = "txtMaxNumSalesOrdersPerDate";
            this.txtMaxNumSalesOrdersPerDate.Size = new System.Drawing.Size(86, 20);
            this.txtMaxNumSalesOrdersPerDate.TabIndex = 8;
            // 
            // txtMinNumSalesOrdersPerDate
            // 
            this.txtMinNumSalesOrdersPerDate.Location = new System.Drawing.Point(49, 121);
            this.txtMinNumSalesOrdersPerDate.Name = "txtMinNumSalesOrdersPerDate";
            this.txtMinNumSalesOrdersPerDate.Size = new System.Drawing.Size(91, 20);
            this.txtMinNumSalesOrdersPerDate.TabIndex = 7;
            // 
            // lblMaxNumSalesOrdersPerDate
            // 
            this.lblMaxNumSalesOrdersPerDate.AutoSize = true;
            this.lblMaxNumSalesOrdersPerDate.Location = new System.Drawing.Point(146, 125);
            this.lblMaxNumSalesOrdersPerDate.Name = "lblMaxNumSalesOrdersPerDate";
            this.lblMaxNumSalesOrdersPerDate.Size = new System.Drawing.Size(20, 13);
            this.lblMaxNumSalesOrdersPerDate.TabIndex = 6;
            this.lblMaxNumSalesOrdersPerDate.Text = "To";
            // 
            // lblMinNumSalesOrdersPerDate
            // 
            this.lblMinNumSalesOrdersPerDate.AutoSize = true;
            this.lblMinNumSalesOrdersPerDate.Location = new System.Drawing.Point(16, 125);
            this.lblMinNumSalesOrdersPerDate.Name = "lblMinNumSalesOrdersPerDate";
            this.lblMinNumSalesOrdersPerDate.Size = new System.Drawing.Size(30, 13);
            this.lblMinNumSalesOrdersPerDate.TabIndex = 5;
            this.lblMinNumSalesOrdersPerDate.Text = "From";
            // 
            // txtLatestTransactionDate
            // 
            this.txtLatestTransactionDate.Location = new System.Drawing.Point(147, 52);
            this.txtLatestTransactionDate.Name = "txtLatestTransactionDate";
            this.txtLatestTransactionDate.Size = new System.Drawing.Size(79, 20);
            this.txtLatestTransactionDate.TabIndex = 4;
            // 
            // lblLatestTransactionDate
            // 
            this.lblLatestTransactionDate.AutoSize = true;
            this.lblLatestTransactionDate.Location = new System.Drawing.Point(15, 52);
            this.lblLatestTransactionDate.Name = "lblLatestTransactionDate";
            this.lblLatestTransactionDate.Size = new System.Drawing.Size(118, 13);
            this.lblLatestTransactionDate.TabIndex = 3;
            this.lblLatestTransactionDate.Text = "Latest TransactionDate";
            // 
            // txtEarliestTransactionDate
            // 
            this.txtEarliestTransactionDate.Location = new System.Drawing.Point(147, 28);
            this.txtEarliestTransactionDate.Name = "txtEarliestTransactionDate";
            this.txtEarliestTransactionDate.Size = new System.Drawing.Size(79, 20);
            this.txtEarliestTransactionDate.TabIndex = 2;
            // 
            // lblEarliestTransactionDate
            // 
            this.lblEarliestTransactionDate.AutoSize = true;
            this.lblEarliestTransactionDate.Location = new System.Drawing.Point(15, 28);
            this.lblEarliestTransactionDate.Name = "lblEarliestTransactionDate";
            this.lblEarliestTransactionDate.Size = new System.Drawing.Size(126, 13);
            this.lblEarliestTransactionDate.TabIndex = 1;
            this.lblEarliestTransactionDate.Text = "Earliest Transaction Date";
            // 
            // lblOrderTransactionsDates
            // 
            this.lblOrderTransactionsDates.AutoSize = true;
            this.lblOrderTransactionsDates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderTransactionsDates.Location = new System.Drawing.Point(5, 5);
            this.lblOrderTransactionsDates.Name = "lblOrderTransactionsDates";
            this.lblOrderTransactionsDates.Size = new System.Drawing.Size(152, 13);
            this.lblOrderTransactionsDates.TabIndex = 0;
            this.lblOrderTransactionsDates.Text = "Order Transactions Dates";
            // 
            // lblNumPreviewRows
            // 
            this.lblNumPreviewRows.AutoSize = true;
            this.lblNumPreviewRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumPreviewRows.Location = new System.Drawing.Point(3, 47);
            this.lblNumPreviewRows.Name = "lblNumPreviewRows";
            this.lblNumPreviewRows.Size = new System.Drawing.Size(101, 13);
            this.lblNumPreviewRows.TabIndex = 25;
            this.lblNumPreviewRows.Text = "Num Preview Dates";
            // 
            // txtNumPreviewDates
            // 
            this.txtNumPreviewDates.Location = new System.Drawing.Point(6, 66);
            this.txtNumPreviewDates.Name = "txtNumPreviewDates";
            this.txtNumPreviewDates.Size = new System.Drawing.Size(93, 20);
            this.txtNumPreviewDates.TabIndex = 7;
            // 
            // pnlPreviewOutput
            // 
            this.pnlPreviewOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewOutput.Controls.Add(this.cmdPreview);
            this.pnlPreviewOutput.Controls.Add(this.lblNumPreviewRows);
            this.pnlPreviewOutput.Controls.Add(this.txtNumPreviewDates);
            this.pnlPreviewOutput.Location = new System.Drawing.Point(367, 120);
            this.pnlPreviewOutput.Name = "pnlPreviewOutput";
            this.pnlPreviewOutput.Size = new System.Drawing.Size(111, 96);
            this.pnlPreviewOutput.TabIndex = 32;
            // 
            // OrderTransactionDatesForm
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(500, 355);
            this.Controls.Add(this.pnlBusinessTrasactionsDates);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.pnlPreviewOutput);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrderTransactionDatesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Specify Order Transaction Dates ...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.pnlBusinessTrasactionsDates.ResumeLayout(false);
            this.pnlBusinessTrasactionsDates.PerformLayout();
            this.pnlPreviewOutput.ResumeLayout(false);
            this.pnlPreviewOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolTip mainMenuToolTips;
        private System.Windows.Forms.Panel pnlBusinessTrasactionsDates;
        private System.Windows.Forms.Label lblPurchaseOrdersPerDate;
        private System.Windows.Forms.Label lblNumberOfPurchaseOrdersPerDate;
        private System.Windows.Forms.Button cmdEstimateTransactionRowCounts;
        private System.Windows.Forms.TextBox txtMaxNumPurchaseOrdersPerDate;
        private System.Windows.Forms.TextBox txtMinNumPurchaseOrdersPerDate;
        private System.Windows.Forms.Label lblMaxNumPurchaseOrdersPerDate;
        private System.Windows.Forms.Label lblMinNumPurchaseOrdersPerDate;
        private System.Windows.Forms.CheckBox chkIncludeWeekendDays;
        private System.Windows.Forms.Label lblTimesPerDate;
        private System.Windows.Forms.TextBox txtMaxTimePerDate;
        private System.Windows.Forms.TextBox txtMinTimePerDate;
        private System.Windows.Forms.Label lblMaxTimePerDate;
        private System.Windows.Forms.Label lblMinTimePerDate;
        private System.Windows.Forms.Label lblTimeRangeForTransactionsToOccur;
        private System.Windows.Forms.Label lblSalesOrdersPerDate;
        private System.Windows.Forms.Label lblNumberOfSalesOrdersPerDate;
        private System.Windows.Forms.TextBox txtMaxNumSalesOrdersPerDate;
        private System.Windows.Forms.TextBox txtMinNumSalesOrdersPerDate;
        private System.Windows.Forms.Label lblMaxNumSalesOrdersPerDate;
        private System.Windows.Forms.Label lblMinNumSalesOrdersPerDate;
        private System.Windows.Forms.TextBox txtLatestTransactionDate;
        private System.Windows.Forms.Label lblLatestTransactionDate;
        private System.Windows.Forms.TextBox txtEarliestTransactionDate;
        private System.Windows.Forms.Label lblEarliestTransactionDate;
        private System.Windows.Forms.Label lblOrderTransactionsDates;
        private System.Windows.Forms.Button cmdPreview;
        private System.Windows.Forms.Label lblNumPreviewRows;
        private System.Windows.Forms.TextBox txtNumPreviewDates;
        private System.Windows.Forms.Panel pnlPreviewOutput;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
    }
#pragma warning restore 1591
}

