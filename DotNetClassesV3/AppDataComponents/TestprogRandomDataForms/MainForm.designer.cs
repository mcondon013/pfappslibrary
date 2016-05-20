namespace TestprogRandomDataForms
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
            this.chkRandomDataFormsManager = new System.Windows.Forms.CheckBox();
            this.chkRandomDataElementsForm = new System.Windows.Forms.CheckBox();
            this.chkRandomCustomValuesForm = new System.Windows.Forms.CheckBox();
            this.chkRandomNamesAndLocationsForm = new System.Windows.Forms.CheckBox();
            this.chkRandomBytesForm = new System.Windows.Forms.CheckBox();
            this.chkRandomWordsForm = new System.Windows.Forms.CheckBox();
            this.chkRandomStringsForm = new System.Windows.Forms.CheckBox();
            this.chkRandomBooleansForm = new System.Windows.Forms.CheckBox();
            this.chkRandomDateTimesForm = new System.Windows.Forms.CheckBox();
            this.chkRandomNumbersForm = new System.Windows.Forms.CheckBox();
            this.chkColSpecForm = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.cmdShowHideOutputLog = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.grpTestsToRun.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(384, 399);
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
            this.appHelpProvider.SetHelpKeyword(this.cmdRunTests, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdRunTests, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdRunTests, "Help for Run Tests: See Help File.");
            this.cmdRunTests.Location = new System.Drawing.Point(384, 60);
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
            this.MainMenu.Size = new System.Drawing.Size(541, 24);
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
            this.grpTestsToRun.Controls.Add(this.chkRandomDataFormsManager);
            this.grpTestsToRun.Controls.Add(this.chkRandomDataElementsForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomCustomValuesForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomNamesAndLocationsForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomBytesForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomWordsForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomStringsForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomBooleansForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomDateTimesForm);
            this.grpTestsToRun.Controls.Add(this.chkRandomNumbersForm);
            this.grpTestsToRun.Controls.Add(this.chkColSpecForm);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 60);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(315, 337);
            this.grpTestsToRun.TabIndex = 1;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkRandomDataFormsManager
            // 
            this.chkRandomDataFormsManager.AutoSize = true;
            this.chkRandomDataFormsManager.Location = new System.Drawing.Point(18, 297);
            this.chkRandomDataFormsManager.Name = "chkRandomDataFormsManager";
            this.chkRandomDataFormsManager.Size = new System.Drawing.Size(159, 17);
            this.chkRandomDataFormsManager.TabIndex = 12;
            this.chkRandomDataFormsManager.Text = "RandomDataFormsManager";
            this.chkRandomDataFormsManager.UseVisualStyleBackColor = true;
            // 
            // chkRandomDataElementsForm
            // 
            this.chkRandomDataElementsForm.AutoSize = true;
            this.chkRandomDataElementsForm.Location = new System.Drawing.Point(17, 203);
            this.chkRandomDataElementsForm.Name = "chkRandomDataElementsForm";
            this.chkRandomDataElementsForm.Size = new System.Drawing.Size(164, 17);
            this.chkRandomDataElementsForm.TabIndex = 11;
            this.chkRandomDataElementsForm.Text = "Random Data Elements Form";
            this.chkRandomDataElementsForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomCustomValuesForm
            // 
            this.chkRandomCustomValuesForm.AutoSize = true;
            this.chkRandomCustomValuesForm.Location = new System.Drawing.Point(17, 228);
            this.chkRandomCustomValuesForm.Name = "chkRandomCustomValuesForm";
            this.chkRandomCustomValuesForm.Size = new System.Drawing.Size(165, 17);
            this.chkRandomCustomValuesForm.TabIndex = 10;
            this.chkRandomCustomValuesForm.Text = "Random Custom Values Form";
            this.chkRandomCustomValuesForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomNamesAndLocationsForm
            // 
            this.chkRandomNamesAndLocationsForm.AutoSize = true;
            this.chkRandomNamesAndLocationsForm.Location = new System.Drawing.Point(18, 37);
            this.chkRandomNamesAndLocationsForm.Name = "chkRandomNamesAndLocationsForm";
            this.chkRandomNamesAndLocationsForm.Size = new System.Drawing.Size(199, 17);
            this.chkRandomNamesAndLocationsForm.TabIndex = 9;
            this.chkRandomNamesAndLocationsForm.Text = "Random Names And Locations Form";
            this.chkRandomNamesAndLocationsForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomBytesForm
            // 
            this.chkRandomBytesForm.AutoSize = true;
            this.chkRandomBytesForm.Location = new System.Drawing.Point(17, 180);
            this.chkRandomBytesForm.Name = "chkRandomBytesForm";
            this.chkRandomBytesForm.Size = new System.Drawing.Size(121, 17);
            this.chkRandomBytesForm.TabIndex = 8;
            this.chkRandomBytesForm.Text = "Random Bytes Form";
            this.chkRandomBytesForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomWordsForm
            // 
            this.chkRandomWordsForm.AutoSize = true;
            this.chkRandomWordsForm.Location = new System.Drawing.Point(17, 156);
            this.chkRandomWordsForm.Name = "chkRandomWordsForm";
            this.chkRandomWordsForm.Size = new System.Drawing.Size(126, 17);
            this.chkRandomWordsForm.TabIndex = 7;
            this.chkRandomWordsForm.Text = "Random Words Form";
            this.chkRandomWordsForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomStringsForm
            // 
            this.chkRandomStringsForm.AutoSize = true;
            this.chkRandomStringsForm.Location = new System.Drawing.Point(17, 132);
            this.chkRandomStringsForm.Name = "chkRandomStringsForm";
            this.chkRandomStringsForm.Size = new System.Drawing.Size(127, 17);
            this.chkRandomStringsForm.TabIndex = 6;
            this.chkRandomStringsForm.Text = "Random Strings Form";
            this.chkRandomStringsForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomBooleansForm
            // 
            this.chkRandomBooleansForm.AutoSize = true;
            this.chkRandomBooleansForm.Location = new System.Drawing.Point(17, 108);
            this.chkRandomBooleansForm.Name = "chkRandomBooleansForm";
            this.chkRandomBooleansForm.Size = new System.Drawing.Size(139, 17);
            this.chkRandomBooleansForm.TabIndex = 5;
            this.chkRandomBooleansForm.Text = "Random Booleans Form";
            this.chkRandomBooleansForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomDateTimesForm
            // 
            this.chkRandomDateTimesForm.AutoSize = true;
            this.chkRandomDateTimesForm.Location = new System.Drawing.Point(17, 84);
            this.chkRandomDateTimesForm.Name = "chkRandomDateTimesForm";
            this.chkRandomDateTimesForm.Size = new System.Drawing.Size(151, 17);
            this.chkRandomDateTimesForm.TabIndex = 4;
            this.chkRandomDateTimesForm.Text = "Random Date/Times Form";
            this.chkRandomDateTimesForm.UseVisualStyleBackColor = true;
            // 
            // chkRandomNumbersForm
            // 
            this.chkRandomNumbersForm.AutoSize = true;
            this.chkRandomNumbersForm.Location = new System.Drawing.Point(17, 60);
            this.chkRandomNumbersForm.Name = "chkRandomNumbersForm";
            this.chkRandomNumbersForm.Size = new System.Drawing.Size(137, 17);
            this.chkRandomNumbersForm.TabIndex = 3;
            this.chkRandomNumbersForm.Text = "Random Numbers Form";
            this.chkRandomNumbersForm.UseVisualStyleBackColor = true;
            // 
            // chkColSpecForm
            // 
            this.chkColSpecForm.AutoSize = true;
            this.chkColSpecForm.Location = new System.Drawing.Point(18, 263);
            this.chkColSpecForm.Name = "chkColSpecForm";
            this.chkColSpecForm.Size = new System.Drawing.Size(89, 17);
            this.chkColSpecForm.TabIndex = 2;
            this.chkColSpecForm.Text = "ColSpecForm";
            this.chkColSpecForm.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 419);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 2;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogRandomDataForms\\InitWinFormsAppWit" +
    "hUserAndAppSettings\\InitWinFormsHelpFile.chm";
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(384, 225);
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
            this.ClientSize = new System.Drawing.Size(541, 500);
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
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.GroupBox grpTestsToRun;
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
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptionsUserSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptionsApplicationSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsForm;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsFormSaveScreenLocations;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsFormRestoreScreenLocations;
        private System.Windows.Forms.CheckBox chkColSpecForm;
        private System.Windows.Forms.CheckBox chkRandomNumbersForm;
        private System.Windows.Forms.CheckBox chkRandomDateTimesForm;
        private System.Windows.Forms.CheckBox chkRandomBooleansForm;
        private System.Windows.Forms.CheckBox chkRandomStringsForm;
        private System.Windows.Forms.CheckBox chkRandomWordsForm;
        private System.Windows.Forms.CheckBox chkRandomBytesForm;
        private System.Windows.Forms.CheckBox chkRandomNamesAndLocationsForm;
        private System.Windows.Forms.CheckBox chkRandomCustomValuesForm;
        private System.Windows.Forms.CheckBox chkRandomDataElementsForm;
        private System.Windows.Forms.CheckBox chkRandomDataFormsManager;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

