namespace TestprogLoadAWDW
{
    partial class MainForm
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
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsOptionsUserSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuToolsOptionsApplicationSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsForm = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsFormSaveScreenLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsFormRestoreScreenLocations = new System.Windows.Forms.ToolStripMenuItem();
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
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.grpDataSource = new System.Windows.Forms.GroupBox();
            this.optSQLCE35WS5 = new System.Windows.Forms.RadioButton();
            this.optSQLServerWS3 = new System.Windows.Forms.RadioButton();
            this.optSQLServerSV2 = new System.Windows.Forms.RadioButton();
            this.txtConnectionStringForOutputTables = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cboDatabaseTypeForOutputTables = new System.Windows.Forms.ComboBox();
            this.cmdBuildConnectionString = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.grpDataDestination = new System.Windows.Forms.GroupBox();
            this.txtOutputTablesSchema = new System.Windows.Forms.TextBox();
            this.lblOutputTablesSchema = new System.Windows.Forms.Label();
            this.txtOutputBatchSize = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkReplaceExistingTables = new System.Windows.Forms.CheckBox();
            this.showHideMessageLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.grpDataSource.SuspendLayout();
            this.grpDataDestination.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(480, 259);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(139, 37);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdCopy, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdCopy, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdCopy, "Help for Run Tests: See Help File.");
            this.cmdCopy.Location = new System.Drawing.Point(479, 59);
            this.cmdCopy.Name = "cmdCopy";
            this.appHelpProvider.SetShowHelp(this.cmdCopy, true);
            this.cmdCopy.Size = new System.Drawing.Size(139, 37);
            this.cmdCopy.TabIndex = 3;
            this.cmdCopy.Text = "&Copy";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(648, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsOptions,
            this.mnuToolsForm,
            this.showHideMessageLogToolStripMenuItem});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsOptions
            // 
            this.mnuToolsOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsOptionsUserSettings,
            this.toolStripSeparator1,
            this.mnuToolsOptionsApplicationSettings});
            this.mnuToolsOptions.Name = "mnuToolsOptions";
            this.mnuToolsOptions.Size = new System.Drawing.Size(205, 22);
            this.mnuToolsOptions.Text = "&Options";
            // 
            // mnuToolsOptionsUserSettings
            // 
            this.mnuToolsOptionsUserSettings.Name = "mnuToolsOptionsUserSettings";
            this.mnuToolsOptionsUserSettings.Size = new System.Drawing.Size(180, 22);
            this.mnuToolsOptionsUserSettings.Text = "&User Settings";
            this.mnuToolsOptionsUserSettings.ToolTipText = "View and modify user specific option settings";
            this.mnuToolsOptionsUserSettings.Click += new System.EventHandler(this.mnuToolsOptionsUserSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // mnuToolsOptionsApplicationSettings
            // 
            this.mnuToolsOptionsApplicationSettings.Name = "mnuToolsOptionsApplicationSettings";
            this.mnuToolsOptionsApplicationSettings.Size = new System.Drawing.Size(180, 22);
            this.mnuToolsOptionsApplicationSettings.Text = "&Application Settings";
            this.mnuToolsOptionsApplicationSettings.ToolTipText = "View and modify application option settings that apply to all users.";
            this.mnuToolsOptionsApplicationSettings.Click += new System.EventHandler(this.mnuToolsOptionsApplicationSettings_Click);
            // 
            // mnuToolsForm
            // 
            this.mnuToolsForm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsFormSaveScreenLocations,
            this.mnuToolsFormRestoreScreenLocations});
            this.mnuToolsForm.Name = "mnuToolsForm";
            this.mnuToolsForm.Size = new System.Drawing.Size(205, 22);
            this.mnuToolsForm.Text = "&Form";
            // 
            // mnuToolsFormSaveScreenLocations
            // 
            this.mnuToolsFormSaveScreenLocations.Name = "mnuToolsFormSaveScreenLocations";
            this.mnuToolsFormSaveScreenLocations.Size = new System.Drawing.Size(205, 22);
            this.mnuToolsFormSaveScreenLocations.Text = "&Save Screen Locations";
            this.mnuToolsFormSaveScreenLocations.Click += new System.EventHandler(this.mnuToolsFormSaveScreenLocations_Click);
            // 
            // mnuToolsFormRestoreScreenLocations
            // 
            this.mnuToolsFormRestoreScreenLocations.Name = "mnuToolsFormRestoreScreenLocations";
            this.mnuToolsFormRestoreScreenLocations.Size = new System.Drawing.Size(205, 22);
            this.mnuToolsFormRestoreScreenLocations.Text = "&Restore Screen Locations";
            this.mnuToolsFormRestoreScreenLocations.Click += new System.EventHandler(this.mnuToolsFormRestoreScreenLocations_Click);
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
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
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
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(480, 194);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(108, 30);
            this.chkEraseOutputBeforeEachTest.TabIndex = 2;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Log Before\r\nEach Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogLoadAWDW\\InitWinFormsAppWithUserAn" +
    "dAppSettings\\InitWinFormsHelpFile.chm";
            // 
            // grpDataSource
            // 
            this.grpDataSource.Controls.Add(this.optSQLCE35WS5);
            this.grpDataSource.Controls.Add(this.optSQLServerWS3);
            this.grpDataSource.Controls.Add(this.optSQLServerSV2);
            this.grpDataSource.Location = new System.Drawing.Point(24, 60);
            this.grpDataSource.Name = "grpDataSource";
            this.grpDataSource.Size = new System.Drawing.Size(441, 52);
            this.grpDataSource.TabIndex = 5;
            this.grpDataSource.TabStop = false;
            this.grpDataSource.Text = "Data Source";
            // 
            // optSQLCE35WS5
            // 
            this.optSQLCE35WS5.AutoSize = true;
            this.optSQLCE35WS5.Location = new System.Drawing.Point(330, 20);
            this.optSQLCE35WS5.Name = "optSQLCE35WS5";
            this.optSQLCE35WS5.Size = new System.Drawing.Size(105, 17);
            this.optSQLCE35WS5.TabIndex = 2;
            this.optSQLCE35WS5.TabStop = true;
            this.optSQLCE35WS5.Text = "SQLCE 3.5 WS5";
            this.optSQLCE35WS5.UseVisualStyleBackColor = true;
            // 
            // optSQLServerWS3
            // 
            this.optSQLServerWS3.AutoSize = true;
            this.optSQLServerWS3.Location = new System.Drawing.Point(168, 19);
            this.optSQLServerWS3.Name = "optSQLServerWS3";
            this.optSQLServerWS3.Size = new System.Drawing.Size(107, 17);
            this.optSQLServerWS3.TabIndex = 1;
            this.optSQLServerWS3.TabStop = true;
            this.optSQLServerWS3.Text = "SQL Server WS3";
            this.optSQLServerWS3.UseVisualStyleBackColor = true;
            // 
            // optSQLServerSV2
            // 
            this.optSQLServerSV2.AutoSize = true;
            this.optSQLServerSV2.Location = new System.Drawing.Point(15, 19);
            this.optSQLServerSV2.Name = "optSQLServerSV2";
            this.optSQLServerSV2.Size = new System.Drawing.Size(100, 17);
            this.optSQLServerSV2.TabIndex = 0;
            this.optSQLServerSV2.TabStop = true;
            this.optSQLServerSV2.Text = "SQLServer SV2";
            this.optSQLServerSV2.UseVisualStyleBackColor = true;
            // 
            // txtConnectionStringForOutputTables
            // 
            this.txtConnectionStringForOutputTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionStringForOutputTables.Location = new System.Drawing.Point(129, 74);
            this.txtConnectionStringForOutputTables.Multiline = true;
            this.txtConnectionStringForOutputTables.Name = "txtConnectionStringForOutputTables";
            this.txtConnectionStringForOutputTables.Size = new System.Drawing.Size(306, 70);
            this.txtConnectionStringForOutputTables.TabIndex = 22;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(12, 77);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 13);
            this.label20.TabIndex = 21;
            this.label20.Text = "Connection String:";
            // 
            // cboDatabaseTypeForOutputTables
            // 
            this.cboDatabaseTypeForOutputTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDatabaseTypeForOutputTables.FormattingEnabled = true;
            this.cboDatabaseTypeForOutputTables.Location = new System.Drawing.Point(132, 28);
            this.cboDatabaseTypeForOutputTables.Name = "cboDatabaseTypeForOutputTables";
            this.cboDatabaseTypeForOutputTables.Size = new System.Drawing.Size(303, 21);
            this.cboDatabaseTypeForOutputTables.TabIndex = 20;
            this.cboDatabaseTypeForOutputTables.SelectedIndexChanged += new System.EventHandler(this.cboDatabaseTypeForOutputTables_SelectedIndexChanged);
            // 
            // cmdBuildConnectionString
            // 
            this.cmdBuildConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBuildConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildConnectionString.Location = new System.Drawing.Point(397, 55);
            this.cmdBuildConnectionString.Name = "cmdBuildConnectionString";
            this.cmdBuildConnectionString.Size = new System.Drawing.Size(38, 20);
            this.cmdBuildConnectionString.TabIndex = 23;
            this.cmdBuildConnectionString.Text = "•••";
            this.cmdBuildConnectionString.UseVisualStyleBackColor = true;
            this.cmdBuildConnectionString.Click += new System.EventHandler(this.cmdBuildConnectionString_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(12, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(83, 13);
            this.label19.TabIndex = 19;
            this.label19.Text = "Database Type:";
            // 
            // grpDataDestination
            // 
            this.grpDataDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDataDestination.Controls.Add(this.txtOutputTablesSchema);
            this.grpDataDestination.Controls.Add(this.lblOutputTablesSchema);
            this.grpDataDestination.Controls.Add(this.txtOutputBatchSize);
            this.grpDataDestination.Controls.Add(this.label19);
            this.grpDataDestination.Controls.Add(this.txtConnectionStringForOutputTables);
            this.grpDataDestination.Controls.Add(this.label11);
            this.grpDataDestination.Controls.Add(this.cmdBuildConnectionString);
            this.grpDataDestination.Controls.Add(this.label20);
            this.grpDataDestination.Controls.Add(this.cboDatabaseTypeForOutputTables);
            this.grpDataDestination.Location = new System.Drawing.Point(24, 118);
            this.grpDataDestination.Name = "grpDataDestination";
            this.grpDataDestination.Size = new System.Drawing.Size(441, 190);
            this.grpDataDestination.TabIndex = 24;
            this.grpDataDestination.TabStop = false;
            this.grpDataDestination.Text = "Data Destination";
            // 
            // txtOutputTablesSchema
            // 
            this.txtOutputTablesSchema.Location = new System.Drawing.Point(335, 154);
            this.txtOutputTablesSchema.Name = "txtOutputTablesSchema";
            this.txtOutputTablesSchema.Size = new System.Drawing.Size(100, 20);
            this.txtOutputTablesSchema.TabIndex = 28;
            // 
            // lblOutputTablesSchema
            // 
            this.lblOutputTablesSchema.AutoSize = true;
            this.lblOutputTablesSchema.Location = new System.Drawing.Point(208, 158);
            this.lblOutputTablesSchema.Name = "lblOutputTablesSchema";
            this.lblOutputTablesSchema.Size = new System.Drawing.Size(116, 13);
            this.lblOutputTablesSchema.TabIndex = 27;
            this.lblOutputTablesSchema.Text = "Output Tables Schema";
            // 
            // txtOutputBatchSize
            // 
            this.txtOutputBatchSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtOutputBatchSize.Location = new System.Drawing.Point(146, 153);
            this.txtOutputBatchSize.Name = "txtOutputBatchSize";
            this.txtOutputBatchSize.Size = new System.Drawing.Size(42, 20);
            this.txtOutputBatchSize.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 157);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Batch size for data writes:";
            // 
            // chkReplaceExistingTables
            // 
            this.chkReplaceExistingTables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkReplaceExistingTables.AutoSize = true;
            this.chkReplaceExistingTables.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkReplaceExistingTables.Location = new System.Drawing.Point(479, 134);
            this.chkReplaceExistingTables.Name = "chkReplaceExistingTables";
            this.chkReplaceExistingTables.Size = new System.Drawing.Size(140, 17);
            this.chkReplaceExistingTables.TabIndex = 24;
            this.chkReplaceExistingTables.Text = "Replace Existing Tables";
            this.chkReplaceExistingTables.UseVisualStyleBackColor = true;
            // 
            // showHideMessageLogToolStripMenuItem
            // 
            this.showHideMessageLogToolStripMenuItem.Name = "showHideMessageLogToolStripMenuItem";
            this.showHideMessageLogToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.showHideMessageLogToolStripMenuItem.Text = "Show/Hide Message Log";
            this.showHideMessageLogToolStripMenuItem.Click += new System.EventHandler(this.showHideMessageLogToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdCopy;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(648, 323);
            this.Controls.Add(this.grpDataDestination);
            this.Controls.Add(this.grpDataSource);
            this.Controls.Add(this.chkReplaceExistingTables);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy AdventureWorksDW";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpDataSource.ResumeLayout(false);
            this.grpDataSource.PerformLayout();
            this.grpDataDestination.ResumeLayout(false);
            this.grpDataDestination.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpTutorial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContact;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.CheckBox chkEraseOutputBeforeEachTest;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptionsUserSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptionsApplicationSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsForm;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsFormSaveScreenLocations;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsFormRestoreScreenLocations;
        private System.Windows.Forms.GroupBox grpDataSource;
        private System.Windows.Forms.RadioButton optSQLCE35WS5;
        private System.Windows.Forms.RadioButton optSQLServerWS3;
        private System.Windows.Forms.RadioButton optSQLServerSV2;
        internal System.Windows.Forms.TextBox txtConnectionStringForOutputTables;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.ComboBox cboDatabaseTypeForOutputTables;
        private System.Windows.Forms.Button cmdBuildConnectionString;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox grpDataDestination;
        internal System.Windows.Forms.TextBox txtOutputBatchSize;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.CheckBox chkReplaceExistingTables;
        private System.Windows.Forms.TextBox txtOutputTablesSchema;
        private System.Windows.Forms.Label lblOutputTablesSchema;
        private System.Windows.Forms.ToolStripMenuItem showHideMessageLogToolStripMenuItem;
    }
}

