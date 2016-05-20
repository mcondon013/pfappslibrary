namespace TestprogAppDataForms
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
            this.cmdRunTests = new System.Windows.Forms.Button();
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
            this.grpTestsToRun = new System.Windows.Forms.GroupBox();
            this.chkShowFxlVisualParseForm = new System.Windows.Forms.CheckBox();
            this.chkShowFixedLenOutputColDefsForm = new System.Windows.Forms.CheckBox();
            this.cboFilterFile = new System.Windows.Forms.ComboBox();
            this.chkShowFilterForm = new System.Windows.Forms.CheckBox();
            this.chkTestRowFilter = new System.Windows.Forms.CheckBox();
            this.chkShowFilterBuilderForm = new System.Windows.Forms.CheckBox();
            this.cboTableName = new System.Windows.Forms.ComboBox();
            this.chkShowFixedLenInputColDefsForm = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(510, 399);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 4;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdRunTests
            // 
            this.cmdRunTests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(510, 60);
            this.cmdRunTests.Name = "cmdRunTests";
            this.appHelpProvider.SetShowHelp(this.cmdRunTests, true);
            this.cmdRunTests.Size = new System.Drawing.Size(93, 37);
            this.cmdRunTests.TabIndex = 3;
            this.cmdRunTests.Text = "&Run Tests";
            this.cmdRunTests.UseVisualStyleBackColor = true;
            this.cmdRunTests.Click += new System.EventHandler(this.cmdRunTest_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(638, 24);
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
            this.mnuToolsForm});
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
            this.mnuToolsOptions.Size = new System.Drawing.Size(116, 22);
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
            this.mnuToolsForm.Size = new System.Drawing.Size(116, 22);
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
            // grpTestsToRun
            // 
            this.grpTestsToRun.Controls.Add(this.chkShowFxlVisualParseForm);
            this.grpTestsToRun.Controls.Add(this.chkShowFixedLenOutputColDefsForm);
            this.grpTestsToRun.Controls.Add(this.cboFilterFile);
            this.grpTestsToRun.Controls.Add(this.chkShowFilterForm);
            this.grpTestsToRun.Controls.Add(this.chkTestRowFilter);
            this.grpTestsToRun.Controls.Add(this.chkShowFilterBuilderForm);
            this.grpTestsToRun.Controls.Add(this.cboTableName);
            this.grpTestsToRun.Controls.Add(this.chkShowFixedLenInputColDefsForm);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 376);
            this.grpTestsToRun.TabIndex = 1;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkShowFxlVisualParseForm
            // 
            this.chkShowFxlVisualParseForm.AutoSize = true;
            this.chkShowFxlVisualParseForm.Location = new System.Drawing.Point(17, 211);
            this.chkShowFxlVisualParseForm.Name = "chkShowFxlVisualParseForm";
            this.chkShowFxlVisualParseForm.Size = new System.Drawing.Size(171, 17);
            this.chkShowFxlVisualParseForm.TabIndex = 7;
            this.chkShowFxlVisualParseForm.Text = "&6 Show FXL Visual Parse Form";
            this.chkShowFxlVisualParseForm.UseVisualStyleBackColor = true;
            // 
            // chkShowFixedLenOutputColDefsForm
            // 
            this.chkShowFixedLenOutputColDefsForm.AutoSize = true;
            this.chkShowFixedLenOutputColDefsForm.Location = new System.Drawing.Point(17, 175);
            this.chkShowFixedLenOutputColDefsForm.Name = "chkShowFixedLenOutputColDefsForm";
            this.chkShowFixedLenOutputColDefsForm.Size = new System.Drawing.Size(209, 17);
            this.chkShowFixedLenOutputColDefsForm.TabIndex = 6;
            this.chkShowFixedLenOutputColDefsForm.Text = "&5 Show FixedLen Output ColDefs Form";
            this.chkShowFixedLenOutputColDefsForm.UseVisualStyleBackColor = true;
            // 
            // cboFilterFile
            // 
            this.cboFilterFile.FormattingEnabled = true;
            this.cboFilterFile.Items.AddRange(new object[] {
            "",
            "c:\\temp\\filter01.xml",
            "c:\\temp\\filter02_nots.xml"});
            this.cboFilterFile.Location = new System.Drawing.Point(238, 136);
            this.cboFilterFile.Name = "cboFilterFile";
            this.cboFilterFile.Size = new System.Drawing.Size(175, 21);
            this.cboFilterFile.TabIndex = 5;
            // 
            // chkShowFilterForm
            // 
            this.chkShowFilterForm.AutoSize = true;
            this.chkShowFilterForm.Location = new System.Drawing.Point(17, 140);
            this.chkShowFilterForm.Name = "chkShowFilterForm";
            this.chkShowFilterForm.Size = new System.Drawing.Size(113, 17);
            this.chkShowFilterForm.TabIndex = 4;
            this.chkShowFilterForm.Text = "&4 Show Filter Form";
            this.chkShowFilterForm.UseVisualStyleBackColor = true;
            // 
            // chkTestRowFilter
            // 
            this.chkTestRowFilter.AutoSize = true;
            this.chkTestRowFilter.Location = new System.Drawing.Point(17, 105);
            this.chkTestRowFilter.Name = "chkTestRowFilter";
            this.chkTestRowFilter.Size = new System.Drawing.Size(326, 17);
            this.chkTestRowFilter.TabIndex = 3;
            this.chkTestRowFilter.Text = "&3 LoadTextLines RowFilter and AsEnumerable().Skip(0).Take(n)";
            this.chkTestRowFilter.UseVisualStyleBackColor = true;
            // 
            // chkShowFilterBuilderForm
            // 
            this.chkShowFilterBuilderForm.AutoSize = true;
            this.chkShowFilterBuilderForm.Location = new System.Drawing.Point(17, 71);
            this.chkShowFilterBuilderForm.Name = "chkShowFilterBuilderForm";
            this.chkShowFilterBuilderForm.Size = new System.Drawing.Size(148, 17);
            this.chkShowFilterBuilderForm.TabIndex = 2;
            this.chkShowFilterBuilderForm.Text = "&2 Show Filter Builder Form";
            this.chkShowFilterBuilderForm.UseVisualStyleBackColor = true;
            // 
            // cboTableName
            // 
            this.cboTableName.FormattingEnabled = true;
            this.cboTableName.Items.AddRange(new object[] {
            "DimAccount",
            "DimCurrency",
            "DimCustomer",
            "DimDate",
            "DimDepartmentGroup",
            "DimEmployee",
            "DimGeography",
            "DimOrganization",
            "DimProduct",
            "DimProductCategory",
            "DimProductSubcategory",
            "DimPromotion",
            "DimReseller",
            "DimSalesReason",
            "DimSalesTerritory",
            "DimScenario",
            "FactAdditionalInternationalProductDescription",
            "FactCallCenter",
            "FactCurrencyRate",
            "FactFinance",
            "FactInternetSales",
            "FactResellerSales",
            "FactInternetSalesReason",
            "FactSalesQuota",
            "FactSurveyResponse",
            "ProspectiveBuyer",
            "FactProductDescriptionExt",
            "FactCurrencyRateExt",
            "FactResellerSalesExt",
            "FactInternetSalesExt",
            "FactInternetSalesReasonExt",
            "AdventureWorksDWBuildVersion"});
            this.cboTableName.Location = new System.Drawing.Point(238, 33);
            this.cboTableName.Name = "cboTableName";
            this.cboTableName.Size = new System.Drawing.Size(175, 21);
            this.cboTableName.TabIndex = 1;
            // 
            // chkShowFixedLenInputColDefsForm
            // 
            this.chkShowFixedLenInputColDefsForm.AutoSize = true;
            this.chkShowFixedLenInputColDefsForm.Location = new System.Drawing.Point(17, 33);
            this.chkShowFixedLenInputColDefsForm.Name = "chkShowFixedLenInputColDefsForm";
            this.chkShowFixedLenInputColDefsForm.Size = new System.Drawing.Size(201, 17);
            this.chkShowFixedLenInputColDefsForm.TabIndex = 0;
            this.chkShowFixedLenInputColDefsForm.Text = "&1 Show FixedLen Input ColDefs Form";
            this.chkShowFixedLenInputColDefsForm.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 454);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(256, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 2;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each LoadTextLines is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogAppDataForms\\InitWinFormsAppWithUs" +
    "erAndAppSettings\\InitWinFormsHelpFile.chm";
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 235);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 79;
            this.cmdShowHideOutputLog.Text = "Show/Hide\r\nOutput Log";
            this.cmdShowHideOutputLog.UseVisualStyleBackColor = true;
            this.cmdShowHideOutputLog.Click += new System.EventHandler(this.cmdShowHideOutputLog_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.cmdRunTests;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(638, 500);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.chkEraseOutputBeforeEachTest);
            this.Controls.Add(this.grpTestsToRun);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdRunTests);
            this.Controls.Add(this.cmdExit);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.grpTestsToRun.ResumeLayout(false);
            this.grpTestsToRun.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdRunTests;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.GroupBox grpTestsToRun;
        private System.Windows.Forms.CheckBox chkShowFixedLenInputColDefsForm;
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
        private System.Windows.Forms.ComboBox cboTableName;
        private System.Windows.Forms.CheckBox chkShowFilterBuilderForm;
        private System.Windows.Forms.CheckBox chkTestRowFilter;
        private System.Windows.Forms.CheckBox chkShowFilterForm;
        private System.Windows.Forms.ComboBox cboFilterFile;
        private System.Windows.Forms.CheckBox chkShowFixedLenOutputColDefsForm;
        private System.Windows.Forms.CheckBox chkShowFxlVisualParseForm;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

