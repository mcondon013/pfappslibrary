namespace pfDataExtractorCP
{
#pragma warning disable 1591
    partial class RandomOrdersGeneratorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomOrdersGeneratorForm));
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdGeneratoTransactionData = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpContact = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mainMenuOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenuToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdPreviewTransactionData = new System.Windows.Forms.Button();
            this.txtNumPreviewTransactions = new System.Windows.Forms.TextBox();
            this.cmdSaveExtractDeinition = new System.Windows.Forms.Button();
            this.mainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cmdEditOutputTableNames = new System.Windows.Forms.Button();
            this.chkGeneratePurchaseOrders = new System.Windows.Forms.CheckBox();
            this.chkGenerateSalesOrders = new System.Windows.Forms.CheckBox();
            this.chkGenerateResellerSalesTable = new System.Windows.Forms.CheckBox();
            this.chkGenerateInternetSalesTable = new System.Windows.Forms.CheckBox();
            this.pnlOutputDatabaseLocation = new System.Windows.Forms.Panel();
            this.txtOutputDatabasePlatform = new System.Windows.Forms.TextBox();
            this.lblOutputDatabasePlatform = new System.Windows.Forms.Label();
            this.lblOutputDatabaseLocation = new System.Windows.Forms.Label();
            this.cmdSpecifyTransactionDatesAndCounts = new System.Windows.Forms.Button();
            this.txtOutputDatabaseLocation = new System.Windows.Forms.TextBox();
            this.txtOutputDatabaseConnection = new System.Windows.Forms.TextBox();
            this.chkReplaceExistingTables = new System.Windows.Forms.CheckBox();
            this.lblOutputDatabaseConnection = new System.Windows.Forms.Label();
            this.lblOutputDatabaseLocationAndType = new System.Windows.Forms.Label();
            this.lblNumNumPreviewRows = new System.Windows.Forms.Label();
            this.optPreviewPurchaseOrders = new System.Windows.Forms.RadioButton();
            this.optPreviewDwInternet = new System.Windows.Forms.RadioButton();
            this.optPreviewDwReseller = new System.Windows.Forms.RadioButton();
            this.optPreviewSalesOrders = new System.Windows.Forms.RadioButton();
            this.pnlPreviewOutput = new System.Windows.Forms.Panel();
            this.MainMenu.SuspendLayout();
            this.pnlOutputDatabaseLocation.SuspendLayout();
            this.pnlPreviewOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(342, 383);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(111, 36);
            this.cmdExit.TabIndex = 5;
            this.cmdExit.Text = "&Exit";
            this.mainMenuToolTips.SetToolTip(this.cmdExit, "Close form and return to extractor form.");
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdGeneratoTransactionData
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdGeneratoTransactionData, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdGeneratoTransactionData, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdGeneratoTransactionData, "Help for Run Tests: See Help File.");
            this.cmdGeneratoTransactionData.Location = new System.Drawing.Point(342, 36);
            this.cmdGeneratoTransactionData.Name = "cmdGeneratoTransactionData";
            this.appHelpProvider.SetShowHelp(this.cmdGeneratoTransactionData, true);
            this.cmdGeneratoTransactionData.Size = new System.Drawing.Size(111, 43);
            this.cmdGeneratoTransactionData.TabIndex = 2;
            this.cmdGeneratoTransactionData.Text = "&Generate\r\nTransactions";
            this.mainMenuToolTips.SetToolTip(this.cmdGeneratoTransactionData, "Generate Database");
            this.cmdGeneratoTransactionData.UseVisualStyleBackColor = true;
            this.cmdGeneratoTransactionData.Click += new System.EventHandler(this.cmdGenerateTransactionData_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp,
            this.toolbarHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(466, 27);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileSave,
            this.toolStripSeparator5,
            this.mnuFilePageSetup,
            this.mnuFilePrint,
            this.mnuFilePrintPreview,
            this.toolStripSeparator1,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 23);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(143, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(140, 6);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
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
            this.mnuHelpContents,
            this.mnuHelpIndex,
            this.mnuHelpSearch,
            this.toolStripSeparator2,
            this.mnuHelpTutorial,
            this.toolStripSeparator3,
            this.mnuHelpContact,
            this.toolStripSeparator4,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 23);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpContents
            // 
            this.mnuHelpContents.Name = "mnuHelpContents";
            this.mnuHelpContents.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContents.Text = "Contents";
            this.mnuHelpContents.Click += new System.EventHandler(this.mnuHelpContents_Click);
            // 
            // mnuHelpIndex
            // 
            this.mnuHelpIndex.Name = "mnuHelpIndex";
            this.mnuHelpIndex.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpIndex.Text = "Index";
            this.mnuHelpIndex.Click += new System.EventHandler(this.mnuHelpIndex_Click);
            // 
            // mnuHelpSearch
            // 
            this.mnuHelpSearch.Name = "mnuHelpSearch";
            this.mnuHelpSearch.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpSearch.Text = "Search";
            this.mnuHelpSearch.Click += new System.EventHandler(this.mnuHelpSearch_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpTutorial
            // 
            this.mnuHelpTutorial.Name = "mnuHelpTutorial";
            this.mnuHelpTutorial.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpTutorial.Text = "Tutorial";
            this.mnuHelpTutorial.Click += new System.EventHandler(this.mnuHelpTutorial_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpContact
            // 
            this.mnuHelpContact.Name = "mnuHelpContact";
            this.mnuHelpContact.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContact.Text = "Contact ProFast Computing";
            this.mnuHelpContact.Click += new System.EventHandler(this.mnuHelpContact_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(222, 22);
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
            // cmdPreviewTransactionData
            // 
            this.cmdPreviewTransactionData.Location = new System.Drawing.Point(-1, -1);
            this.cmdPreviewTransactionData.Name = "cmdPreviewTransactionData";
            this.cmdPreviewTransactionData.Size = new System.Drawing.Size(111, 34);
            this.cmdPreviewTransactionData.TabIndex = 0;
            this.cmdPreviewTransactionData.Text = "&Preview";
            this.mainMenuToolTips.SetToolTip(this.cmdPreviewTransactionData, "Preview Transaction Dates to be Generated");
            this.cmdPreviewTransactionData.UseVisualStyleBackColor = true;
            this.cmdPreviewTransactionData.Click += new System.EventHandler(this.cmdPreviewTransactionData_Click);
            // 
            // txtNumPreviewTransactions
            // 
            this.txtNumPreviewTransactions.Location = new System.Drawing.Point(9, 177);
            this.txtNumPreviewTransactions.Name = "txtNumPreviewTransactions";
            this.txtNumPreviewTransactions.Size = new System.Drawing.Size(93, 20);
            this.txtNumPreviewTransactions.TabIndex = 6;
            this.txtNumPreviewTransactions.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mainMenuToolTips.SetToolTip(this.txtNumPreviewTransactions, "Number of transactions to generate for preview.");
            // 
            // cmdSaveExtractDeinition
            // 
            this.cmdSaveExtractDeinition.Location = new System.Drawing.Point(342, 95);
            this.cmdSaveExtractDeinition.Name = "cmdSaveExtractDeinition";
            this.cmdSaveExtractDeinition.Size = new System.Drawing.Size(111, 43);
            this.cmdSaveExtractDeinition.TabIndex = 3;
            this.cmdSaveExtractDeinition.Text = "&Save Extract\r\nDefinition";
            this.mainMenuToolTips.SetToolTip(this.cmdSaveExtractDeinition, "Save both the random orders and extract definitions.");
            this.cmdSaveExtractDeinition.UseVisualStyleBackColor = true;
            this.cmdSaveExtractDeinition.Click += new System.EventHandler(this.cmdSaveExtractDeinition_Click);
            // 
            // cmdEditOutputTableNames
            // 
            this.cmdEditOutputTableNames.Location = new System.Drawing.Point(48, 294);
            this.cmdEditOutputTableNames.Name = "cmdEditOutputTableNames";
            this.cmdEditOutputTableNames.Size = new System.Drawing.Size(224, 23);
            this.cmdEditOutputTableNames.TabIndex = 12;
            this.cmdEditOutputTableNames.Text = "Specify Output Table Names";
            this.cmdEditOutputTableNames.UseVisualStyleBackColor = true;
            this.cmdEditOutputTableNames.Click += new System.EventHandler(this.cmdEditOutputTableNames_Click);
            // 
            // chkGeneratePurchaseOrders
            // 
            this.chkGeneratePurchaseOrders.AutoSize = true;
            this.chkGeneratePurchaseOrders.Location = new System.Drawing.Point(18, 213);
            this.chkGeneratePurchaseOrders.Name = "chkGeneratePurchaseOrders";
            this.chkGeneratePurchaseOrders.Size = new System.Drawing.Size(271, 17);
            this.chkGeneratePurchaseOrders.TabIndex = 9;
            this.chkGeneratePurchaseOrders.Text = "Generate Purchase Order Header and Detail Tables";
            this.chkGeneratePurchaseOrders.UseVisualStyleBackColor = true;
            // 
            // chkGenerateSalesOrders
            // 
            this.chkGenerateSalesOrders.AutoSize = true;
            this.chkGenerateSalesOrders.Location = new System.Drawing.Point(18, 192);
            this.chkGenerateSalesOrders.Name = "chkGenerateSalesOrders";
            this.chkGenerateSalesOrders.Size = new System.Drawing.Size(249, 17);
            this.chkGenerateSalesOrders.TabIndex = 8;
            this.chkGenerateSalesOrders.Text = "Generate SalesOrder Header and Detail Tables";
            this.chkGenerateSalesOrders.UseVisualStyleBackColor = true;
            // 
            // chkGenerateResellerSalesTable
            // 
            this.chkGenerateResellerSalesTable.AutoSize = true;
            this.chkGenerateResellerSalesTable.Location = new System.Drawing.Point(19, 259);
            this.chkGenerateResellerSalesTable.Name = "chkGenerateResellerSalesTable";
            this.chkGenerateResellerSalesTable.Size = new System.Drawing.Size(254, 17);
            this.chkGenerateResellerSalesTable.TabIndex = 11;
            this.chkGenerateResellerSalesTable.Text = "Generate Data Warehouse Reseller Sales Table";
            this.chkGenerateResellerSalesTable.UseVisualStyleBackColor = true;
            // 
            // chkGenerateInternetSalesTable
            // 
            this.chkGenerateInternetSalesTable.AutoSize = true;
            this.chkGenerateInternetSalesTable.Location = new System.Drawing.Point(18, 236);
            this.chkGenerateInternetSalesTable.Name = "chkGenerateInternetSalesTable";
            this.chkGenerateInternetSalesTable.Size = new System.Drawing.Size(252, 17);
            this.chkGenerateInternetSalesTable.TabIndex = 10;
            this.chkGenerateInternetSalesTable.Text = "Generate Data Warehouse Internet Sales Table";
            this.chkGenerateInternetSalesTable.UseVisualStyleBackColor = true;
            // 
            // pnlOutputDatabaseLocation
            // 
            this.pnlOutputDatabaseLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOutputDatabaseLocation.Controls.Add(this.txtOutputDatabasePlatform);
            this.pnlOutputDatabaseLocation.Controls.Add(this.lblOutputDatabasePlatform);
            this.pnlOutputDatabaseLocation.Controls.Add(this.lblOutputDatabaseLocation);
            this.pnlOutputDatabaseLocation.Controls.Add(this.cmdSpecifyTransactionDatesAndCounts);
            this.pnlOutputDatabaseLocation.Controls.Add(this.chkGeneratePurchaseOrders);
            this.pnlOutputDatabaseLocation.Controls.Add(this.cmdEditOutputTableNames);
            this.pnlOutputDatabaseLocation.Controls.Add(this.txtOutputDatabaseLocation);
            this.pnlOutputDatabaseLocation.Controls.Add(this.chkGenerateSalesOrders);
            this.pnlOutputDatabaseLocation.Controls.Add(this.txtOutputDatabaseConnection);
            this.pnlOutputDatabaseLocation.Controls.Add(this.chkGenerateResellerSalesTable);
            this.pnlOutputDatabaseLocation.Controls.Add(this.chkReplaceExistingTables);
            this.pnlOutputDatabaseLocation.Controls.Add(this.chkGenerateInternetSalesTable);
            this.pnlOutputDatabaseLocation.Controls.Add(this.lblOutputDatabaseConnection);
            this.pnlOutputDatabaseLocation.Controls.Add(this.lblOutputDatabaseLocationAndType);
            this.pnlOutputDatabaseLocation.Location = new System.Drawing.Point(12, 36);
            this.pnlOutputDatabaseLocation.Name = "pnlOutputDatabaseLocation";
            this.pnlOutputDatabaseLocation.Size = new System.Drawing.Size(315, 383);
            this.pnlOutputDatabaseLocation.TabIndex = 1;
            // 
            // txtOutputDatabasePlatform
            // 
            this.txtOutputDatabasePlatform.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputDatabasePlatform.Location = new System.Drawing.Point(118, 54);
            this.txtOutputDatabasePlatform.Name = "txtOutputDatabasePlatform";
            this.txtOutputDatabasePlatform.ReadOnly = true;
            this.txtOutputDatabasePlatform.Size = new System.Drawing.Size(176, 13);
            this.txtOutputDatabasePlatform.TabIndex = 4;
            // 
            // lblOutputDatabasePlatform
            // 
            this.lblOutputDatabasePlatform.AutoSize = true;
            this.lblOutputDatabasePlatform.Location = new System.Drawing.Point(15, 54);
            this.lblOutputDatabasePlatform.Name = "lblOutputDatabasePlatform";
            this.lblOutputDatabasePlatform.Size = new System.Drawing.Size(94, 13);
            this.lblOutputDatabasePlatform.TabIndex = 3;
            this.lblOutputDatabasePlatform.Text = "Database Platform";
            // 
            // lblOutputDatabaseLocation
            // 
            this.lblOutputDatabaseLocation.AutoSize = true;
            this.lblOutputDatabaseLocation.Location = new System.Drawing.Point(15, 29);
            this.lblOutputDatabaseLocation.Name = "lblOutputDatabaseLocation";
            this.lblOutputDatabaseLocation.Size = new System.Drawing.Size(97, 13);
            this.lblOutputDatabaseLocation.TabIndex = 1;
            this.lblOutputDatabaseLocation.Text = "Database Location";
            // 
            // cmdSpecifyTransactionDatesAndCounts
            // 
            this.cmdSpecifyTransactionDatesAndCounts.Location = new System.Drawing.Point(48, 336);
            this.cmdSpecifyTransactionDatesAndCounts.Name = "cmdSpecifyTransactionDatesAndCounts";
            this.cmdSpecifyTransactionDatesAndCounts.Size = new System.Drawing.Size(224, 23);
            this.cmdSpecifyTransactionDatesAndCounts.TabIndex = 13;
            this.cmdSpecifyTransactionDatesAndCounts.Text = "Specify Transaction Dates And Counts";
            this.cmdSpecifyTransactionDatesAndCounts.UseVisualStyleBackColor = true;
            this.cmdSpecifyTransactionDatesAndCounts.Click += new System.EventHandler(this.cmdSpecifyTransactionDatesAndCounts_Click);
            // 
            // txtOutputDatabaseLocation
            // 
            this.txtOutputDatabaseLocation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputDatabaseLocation.Location = new System.Drawing.Point(118, 29);
            this.txtOutputDatabaseLocation.Name = "txtOutputDatabaseLocation";
            this.txtOutputDatabaseLocation.ReadOnly = true;
            this.txtOutputDatabaseLocation.Size = new System.Drawing.Size(176, 13);
            this.txtOutputDatabaseLocation.TabIndex = 2;
            // 
            // txtOutputDatabaseConnection
            // 
            this.txtOutputDatabaseConnection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOutputDatabaseConnection.Location = new System.Drawing.Point(19, 98);
            this.txtOutputDatabaseConnection.Multiline = true;
            this.txtOutputDatabaseConnection.Name = "txtOutputDatabaseConnection";
            this.txtOutputDatabaseConnection.ReadOnly = true;
            this.txtOutputDatabaseConnection.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutputDatabaseConnection.Size = new System.Drawing.Size(279, 56);
            this.txtOutputDatabaseConnection.TabIndex = 6;
            // 
            // chkReplaceExistingTables
            // 
            this.chkReplaceExistingTables.AutoSize = true;
            this.chkReplaceExistingTables.Location = new System.Drawing.Point(74, 164);
            this.chkReplaceExistingTables.Name = "chkReplaceExistingTables";
            this.chkReplaceExistingTables.Size = new System.Drawing.Size(173, 17);
            this.chkReplaceExistingTables.TabIndex = 7;
            this.chkReplaceExistingTables.Text = "Replace Tables If Already Exist";
            this.chkReplaceExistingTables.UseVisualStyleBackColor = true;
            // 
            // lblOutputDatabaseConnection
            // 
            this.lblOutputDatabaseConnection.AutoSize = true;
            this.lblOutputDatabaseConnection.Location = new System.Drawing.Point(15, 82);
            this.lblOutputDatabaseConnection.Name = "lblOutputDatabaseConnection";
            this.lblOutputDatabaseConnection.Size = new System.Drawing.Size(61, 13);
            this.lblOutputDatabaseConnection.TabIndex = 5;
            this.lblOutputDatabaseConnection.Text = "Connection";
            // 
            // lblOutputDatabaseLocationAndType
            // 
            this.lblOutputDatabaseLocationAndType.AutoSize = true;
            this.lblOutputDatabaseLocationAndType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutputDatabaseLocationAndType.Location = new System.Drawing.Point(5, 5);
            this.lblOutputDatabaseLocationAndType.Name = "lblOutputDatabaseLocationAndType";
            this.lblOutputDatabaseLocationAndType.Size = new System.Drawing.Size(213, 13);
            this.lblOutputDatabaseLocationAndType.TabIndex = 0;
            this.lblOutputDatabaseLocationAndType.Text = "Output Database for Random Orders";
            // 
            // lblNumNumPreviewRows
            // 
            this.lblNumNumPreviewRows.AutoSize = true;
            this.lblNumNumPreviewRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumNumPreviewRows.Location = new System.Drawing.Point(25, 146);
            this.lblNumNumPreviewRows.Name = "lblNumNumPreviewRows";
            this.lblNumNumPreviewRows.Size = new System.Drawing.Size(70, 26);
            this.lblNumNumPreviewRows.TabIndex = 5;
            this.lblNumNumPreviewRows.Text = "Num Preview\r\nTransactions";
            // 
            // optPreviewPurchaseOrders
            // 
            this.optPreviewPurchaseOrders.AutoSize = true;
            this.optPreviewPurchaseOrders.Location = new System.Drawing.Point(12, 65);
            this.optPreviewPurchaseOrders.Name = "optPreviewPurchaseOrders";
            this.optPreviewPurchaseOrders.Size = new System.Drawing.Size(90, 17);
            this.optPreviewPurchaseOrders.TabIndex = 2;
            this.optPreviewPurchaseOrders.TabStop = true;
            this.optPreviewPurchaseOrders.Text = "Purch. Orders";
            this.optPreviewPurchaseOrders.UseVisualStyleBackColor = true;
            // 
            // optPreviewDwInternet
            // 
            this.optPreviewDwInternet.AutoSize = true;
            this.optPreviewDwInternet.Location = new System.Drawing.Point(12, 89);
            this.optPreviewDwInternet.Name = "optPreviewDwInternet";
            this.optPreviewDwInternet.Size = new System.Drawing.Size(83, 17);
            this.optPreviewDwInternet.TabIndex = 3;
            this.optPreviewDwInternet.TabStop = true;
            this.optPreviewDwInternet.Text = "DW Internet";
            this.optPreviewDwInternet.UseVisualStyleBackColor = true;
            // 
            // optPreviewDwReseller
            // 
            this.optPreviewDwReseller.AutoSize = true;
            this.optPreviewDwReseller.Location = new System.Drawing.Point(12, 113);
            this.optPreviewDwReseller.Name = "optPreviewDwReseller";
            this.optPreviewDwReseller.Size = new System.Drawing.Size(85, 17);
            this.optPreviewDwReseller.TabIndex = 4;
            this.optPreviewDwReseller.TabStop = true;
            this.optPreviewDwReseller.Text = "DW Reseller";
            this.optPreviewDwReseller.UseVisualStyleBackColor = true;
            // 
            // optPreviewSalesOrders
            // 
            this.optPreviewSalesOrders.AutoSize = true;
            this.optPreviewSalesOrders.Location = new System.Drawing.Point(12, 42);
            this.optPreviewSalesOrders.Name = "optPreviewSalesOrders";
            this.optPreviewSalesOrders.Size = new System.Drawing.Size(85, 17);
            this.optPreviewSalesOrders.TabIndex = 1;
            this.optPreviewSalesOrders.TabStop = true;
            this.optPreviewSalesOrders.Text = "Sales Orders";
            this.optPreviewSalesOrders.UseVisualStyleBackColor = true;
            // 
            // pnlPreviewOutput
            // 
            this.pnlPreviewOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewOutput.Controls.Add(this.optPreviewSalesOrders);
            this.pnlPreviewOutput.Controls.Add(this.optPreviewDwReseller);
            this.pnlPreviewOutput.Controls.Add(this.optPreviewDwInternet);
            this.pnlPreviewOutput.Controls.Add(this.optPreviewPurchaseOrders);
            this.pnlPreviewOutput.Controls.Add(this.cmdPreviewTransactionData);
            this.pnlPreviewOutput.Controls.Add(this.lblNumNumPreviewRows);
            this.pnlPreviewOutput.Controls.Add(this.txtNumPreviewTransactions);
            this.pnlPreviewOutput.Location = new System.Drawing.Point(342, 158);
            this.pnlPreviewOutput.Name = "pnlPreviewOutput";
            this.pnlPreviewOutput.Size = new System.Drawing.Size(111, 209);
            this.pnlPreviewOutput.TabIndex = 4;
            // 
            // RandomOrdersGeneratorForm
            // 
            this.AcceptButton = this.cmdGeneratoTransactionData;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(466, 431);
            this.Controls.Add(this.cmdSaveExtractDeinition);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.pnlOutputDatabaseLocation);
            this.Controls.Add(this.cmdGeneratoTransactionData);
            this.Controls.Add(this.pnlPreviewOutput);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RandomOrdersGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Random Orders Generator Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.pnlOutputDatabaseLocation.ResumeLayout(false);
            this.pnlOutputDatabaseLocation.PerformLayout();
            this.pnlPreviewOutput.ResumeLayout(false);
            this.pnlPreviewOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdGeneratoTransactionData;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.OpenFileDialog mainMenuOpenFileDialog;
        private System.Windows.Forms.ToolTip mainMenuToolTips;
        private System.Windows.Forms.SaveFileDialog mainMenuSaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog mainMenuFolderBrowserDialog;
        private System.Windows.Forms.CheckBox chkGeneratePurchaseOrders;
        private System.Windows.Forms.CheckBox chkGenerateSalesOrders;
        private System.Windows.Forms.CheckBox chkGenerateResellerSalesTable;
        private System.Windows.Forms.CheckBox chkGenerateInternetSalesTable;
        private System.Windows.Forms.Button cmdPreviewTransactionData;
        private System.Windows.Forms.Button cmdEditOutputTableNames;
        private System.Windows.Forms.Panel pnlOutputDatabaseLocation;
        private System.Windows.Forms.TextBox txtOutputDatabaseLocation;
        private System.Windows.Forms.TextBox txtOutputDatabaseConnection;
        private System.Windows.Forms.Label lblOutputDatabaseConnection;
        private System.Windows.Forms.Label lblOutputDatabaseLocationAndType;
        private System.Windows.Forms.CheckBox chkReplaceExistingTables;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpTutorial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContact;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TextBox txtNumPreviewTransactions;
        private System.Windows.Forms.Label lblNumNumPreviewRows;
        private System.Windows.Forms.RadioButton optPreviewPurchaseOrders;
        private System.Windows.Forms.RadioButton optPreviewDwInternet;
        private System.Windows.Forms.RadioButton optPreviewDwReseller;
        private System.Windows.Forms.RadioButton optPreviewSalesOrders;
        private System.Windows.Forms.Panel pnlPreviewOutput;
        private System.Windows.Forms.Button cmdSpecifyTransactionDatesAndCounts;
        private System.Windows.Forms.TextBox txtOutputDatabasePlatform;
        private System.Windows.Forms.Label lblOutputDatabasePlatform;
        private System.Windows.Forms.Label lblOutputDatabaseLocation;
        private System.Windows.Forms.Button cmdSaveExtractDeinition;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
    }
#pragma warning restore 1591
}

