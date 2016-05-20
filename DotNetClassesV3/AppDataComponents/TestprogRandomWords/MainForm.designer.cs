namespace TestprogRandomWords
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
            this.chkGenerateSentenceTemplateArrays = new System.Windows.Forms.CheckBox();
            this.chkGenerateRandomBook = new System.Windows.Forms.CheckBox();
            this.txtNumChaptersToOutput = new System.Windows.Forms.TextBox();
            this.chkGenerateRandomChapter = new System.Windows.Forms.CheckBox();
            this.chkGenerateRandomDocument = new System.Windows.Forms.CheckBox();
            this.txtNumParagraphsToOutput = new System.Windows.Forms.TextBox();
            this.chkGenerateRandomParagraphs = new System.Windows.Forms.CheckBox();
            this.chkGenerateRandomSentences = new System.Windows.Forms.CheckBox();
            this.txtNumSentencesToOutput = new System.Windows.Forms.TextBox();
            this.txtNumWordsToOutput = new System.Windows.Forms.TextBox();
            this.chkGenerateSampleRandomSentences = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkGenerateRandomWords = new System.Windows.Forms.CheckBox();
            this.chkEraseOutputBeforeEachTest = new System.Windows.Forms.CheckBox();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.txtRandomDataDatabasePassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdLookupRandomDataDatabase = new System.Windows.Forms.Button();
            this.txtRandomDataDatabase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRandomDataXmlFile = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRandomDataXmlFilesFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.cmdExit.Location = new System.Drawing.Point(510, 544);
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
            this.grpTestsToRun.Controls.Add(this.chkGenerateSentenceTemplateArrays);
            this.grpTestsToRun.Controls.Add(this.chkGenerateRandomBook);
            this.grpTestsToRun.Controls.Add(this.txtNumChaptersToOutput);
            this.grpTestsToRun.Controls.Add(this.chkGenerateRandomChapter);
            this.grpTestsToRun.Controls.Add(this.chkGenerateRandomDocument);
            this.grpTestsToRun.Controls.Add(this.txtNumParagraphsToOutput);
            this.grpTestsToRun.Controls.Add(this.chkGenerateRandomParagraphs);
            this.grpTestsToRun.Controls.Add(this.chkGenerateRandomSentences);
            this.grpTestsToRun.Controls.Add(this.txtNumSentencesToOutput);
            this.grpTestsToRun.Controls.Add(this.txtNumWordsToOutput);
            this.grpTestsToRun.Controls.Add(this.chkGenerateSampleRandomSentences);
            this.grpTestsToRun.Controls.Add(this.label8);
            this.grpTestsToRun.Controls.Add(this.chkGenerateRandomWords);
            this.grpTestsToRun.Location = new System.Drawing.Point(39, 240);
            this.grpTestsToRun.Name = "grpTestsToRun";
            this.grpTestsToRun.Size = new System.Drawing.Size(437, 303);
            this.grpTestsToRun.TabIndex = 1;
            this.grpTestsToRun.TabStop = false;
            this.grpTestsToRun.Text = "Select Tests to Run";
            // 
            // chkGenerateSentenceTemplateArrays
            // 
            this.chkGenerateSentenceTemplateArrays.AutoSize = true;
            this.chkGenerateSentenceTemplateArrays.Location = new System.Drawing.Point(23, 260);
            this.chkGenerateSentenceTemplateArrays.Name = "chkGenerateSentenceTemplateArrays";
            this.chkGenerateSentenceTemplateArrays.Size = new System.Drawing.Size(207, 17);
            this.chkGenerateSentenceTemplateArrays.TabIndex = 39;
            this.chkGenerateSentenceTemplateArrays.Text = "&8 Generate Sentence Template Arrays";
            this.chkGenerateSentenceTemplateArrays.UseVisualStyleBackColor = true;
            // 
            // chkGenerateRandomBook
            // 
            this.chkGenerateRandomBook.AutoSize = true;
            this.chkGenerateRandomBook.Location = new System.Drawing.Point(23, 228);
            this.chkGenerateRandomBook.Name = "chkGenerateRandomBook";
            this.chkGenerateRandomBook.Size = new System.Drawing.Size(150, 17);
            this.chkGenerateRandomBook.TabIndex = 38;
            this.chkGenerateRandomBook.Text = "&7 Generate Random Book";
            this.chkGenerateRandomBook.UseVisualStyleBackColor = true;
            // 
            // txtNumChaptersToOutput
            // 
            this.txtNumChaptersToOutput.Location = new System.Drawing.Point(304, 225);
            this.txtNumChaptersToOutput.Name = "txtNumChaptersToOutput";
            this.txtNumChaptersToOutput.Size = new System.Drawing.Size(100, 20);
            this.txtNumChaptersToOutput.TabIndex = 37;
            // 
            // chkGenerateRandomChapter
            // 
            this.chkGenerateRandomChapter.AutoSize = true;
            this.chkGenerateRandomChapter.Location = new System.Drawing.Point(23, 191);
            this.chkGenerateRandomChapter.Name = "chkGenerateRandomChapter";
            this.chkGenerateRandomChapter.Size = new System.Drawing.Size(162, 17);
            this.chkGenerateRandomChapter.TabIndex = 36;
            this.chkGenerateRandomChapter.Text = "&6 Generate Random Chapter";
            this.chkGenerateRandomChapter.UseVisualStyleBackColor = true;
            // 
            // chkGenerateRandomDocument
            // 
            this.chkGenerateRandomDocument.AutoSize = true;
            this.chkGenerateRandomDocument.Location = new System.Drawing.Point(23, 156);
            this.chkGenerateRandomDocument.Name = "chkGenerateRandomDocument";
            this.chkGenerateRandomDocument.Size = new System.Drawing.Size(174, 17);
            this.chkGenerateRandomDocument.TabIndex = 35;
            this.chkGenerateRandomDocument.Text = "&5 Generate Random Document";
            this.chkGenerateRandomDocument.UseVisualStyleBackColor = true;
            // 
            // txtNumParagraphsToOutput
            // 
            this.txtNumParagraphsToOutput.Location = new System.Drawing.Point(304, 124);
            this.txtNumParagraphsToOutput.Name = "txtNumParagraphsToOutput";
            this.txtNumParagraphsToOutput.Size = new System.Drawing.Size(100, 20);
            this.txtNumParagraphsToOutput.TabIndex = 34;
            // 
            // chkGenerateRandomParagraphs
            // 
            this.chkGenerateRandomParagraphs.AutoSize = true;
            this.chkGenerateRandomParagraphs.Location = new System.Drawing.Point(23, 124);
            this.chkGenerateRandomParagraphs.Name = "chkGenerateRandomParagraphs";
            this.chkGenerateRandomParagraphs.Size = new System.Drawing.Size(179, 17);
            this.chkGenerateRandomParagraphs.TabIndex = 33;
            this.chkGenerateRandomParagraphs.Text = "&4 Generate Random Paragraphs";
            this.chkGenerateRandomParagraphs.UseVisualStyleBackColor = true;
            // 
            // chkGenerateRandomSentences
            // 
            this.chkGenerateRandomSentences.AutoSize = true;
            this.chkGenerateRandomSentences.Location = new System.Drawing.Point(23, 92);
            this.chkGenerateRandomSentences.Name = "chkGenerateRandomSentences";
            this.chkGenerateRandomSentences.Size = new System.Drawing.Size(176, 17);
            this.chkGenerateRandomSentences.TabIndex = 32;
            this.chkGenerateRandomSentences.Text = "&3 Generate Random Sentences";
            this.chkGenerateRandomSentences.UseVisualStyleBackColor = true;
            // 
            // txtNumSentencesToOutput
            // 
            this.txtNumSentencesToOutput.Location = new System.Drawing.Point(304, 89);
            this.txtNumSentencesToOutput.Name = "txtNumSentencesToOutput";
            this.txtNumSentencesToOutput.Size = new System.Drawing.Size(100, 20);
            this.txtNumSentencesToOutput.TabIndex = 31;
            // 
            // txtNumWordsToOutput
            // 
            this.txtNumWordsToOutput.Location = new System.Drawing.Point(304, 29);
            this.txtNumWordsToOutput.Name = "txtNumWordsToOutput";
            this.txtNumWordsToOutput.Size = new System.Drawing.Size(100, 20);
            this.txtNumWordsToOutput.TabIndex = 30;
            // 
            // chkGenerateSampleRandomSentences
            // 
            this.chkGenerateSampleRandomSentences.AutoSize = true;
            this.chkGenerateSampleRandomSentences.Location = new System.Drawing.Point(23, 62);
            this.chkGenerateSampleRandomSentences.Name = "chkGenerateSampleRandomSentences";
            this.chkGenerateSampleRandomSentences.Size = new System.Drawing.Size(208, 17);
            this.chkGenerateSampleRandomSentences.TabIndex = 3;
            this.chkGenerateSampleRandomSentences.Text = "&2 Gnerate Sample Random Sentences";
            this.chkGenerateSampleRandomSentences.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(301, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Num Items to Output:";
            // 
            // chkGenerateRandomWords
            // 
            this.chkGenerateRandomWords.AutoSize = true;
            this.chkGenerateRandomWords.Location = new System.Drawing.Point(23, 31);
            this.chkGenerateRandomWords.Name = "chkGenerateRandomWords";
            this.chkGenerateRandomWords.Size = new System.Drawing.Size(156, 17);
            this.chkGenerateRandomWords.TabIndex = 2;
            this.chkGenerateRandomWords.Text = "&1 Generate Random Words";
            this.chkGenerateRandomWords.UseVisualStyleBackColor = true;
            // 
            // chkEraseOutputBeforeEachTest
            // 
            this.chkEraseOutputBeforeEachTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkEraseOutputBeforeEachTest.AutoSize = true;
            this.chkEraseOutputBeforeEachTest.Location = new System.Drawing.Point(39, 564);
            this.chkEraseOutputBeforeEachTest.Name = "chkEraseOutputBeforeEachTest";
            this.chkEraseOutputBeforeEachTest.Size = new System.Drawing.Size(207, 17);
            this.chkEraseOutputBeforeEachTest.TabIndex = 2;
            this.chkEraseOutputBeforeEachTest.Text = "Erase Output Before Each Test is Run";
            this.chkEraseOutputBeforeEachTest.UseVisualStyleBackColor = true;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogRandomWords\\InitWinFormsAppWithUse" +
    "rAndAppSettings\\InitWinFormsHelpFile.chm";
            // 
            // txtRandomDataDatabasePassword
            // 
            this.txtRandomDataDatabasePassword.Location = new System.Drawing.Point(106, 100);
            this.txtRandomDataDatabasePassword.Name = "txtRandomDataDatabasePassword";
            this.txtRandomDataDatabasePassword.Size = new System.Drawing.Size(370, 20);
            this.txtRandomDataDatabasePassword.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Password";
            // 
            // cmdLookupRandomDataDatabase
            // 
            this.cmdLookupRandomDataDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLookupRandomDataDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLookupRandomDataDatabase.Location = new System.Drawing.Point(438, 60);
            this.cmdLookupRandomDataDatabase.Name = "cmdLookupRandomDataDatabase";
            this.cmdLookupRandomDataDatabase.Size = new System.Drawing.Size(38, 20);
            this.cmdLookupRandomDataDatabase.TabIndex = 15;
            this.cmdLookupRandomDataDatabase.Text = "•••";
            this.cmdLookupRandomDataDatabase.UseVisualStyleBackColor = true;
            // 
            // txtRandomDataDatabase
            // 
            this.txtRandomDataDatabase.Location = new System.Drawing.Point(39, 77);
            this.txtRandomDataDatabase.Name = "txtRandomDataDatabase";
            this.txtRandomDataDatabase.Size = new System.Drawing.Size(437, 20);
            this.txtRandomDataDatabase.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Random Data Database";
            // 
            // cboRandomDataXmlFile
            // 
            this.cboRandomDataXmlFile.FormattingEnabled = true;
            this.cboRandomDataXmlFile.Location = new System.Drawing.Point(93, 183);
            this.cboRandomDataXmlFile.Name = "cboRandomDataXmlFile";
            this.cboRandomDataXmlFile.Size = new System.Drawing.Size(383, 21);
            this.cboRandomDataXmlFile.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "File:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Folder:";
            // 
            // txtRandomDataXmlFilesFolder
            // 
            this.txtRandomDataXmlFilesFolder.Location = new System.Drawing.Point(93, 156);
            this.txtRandomDataXmlFilesFolder.Name = "txtRandomDataXmlFilesFolder";
            this.txtRandomDataXmlFilesFolder.Size = new System.Drawing.Size(383, 20);
            this.txtRandomDataXmlFilesFolder.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Random Data XML Files";
            // 
            // cmdShowHideOutputLog
            // 
            this.cmdShowHideOutputLog.Location = new System.Drawing.Point(510, 329);
            this.cmdShowHideOutputLog.Name = "cmdShowHideOutputLog";
            this.cmdShowHideOutputLog.Size = new System.Drawing.Size(93, 44);
            this.cmdShowHideOutputLog.TabIndex = 80;
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
            this.ClientSize = new System.Drawing.Size(638, 610);
            this.Controls.Add(this.cmdShowHideOutputLog);
            this.Controls.Add(this.cboRandomDataXmlFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRandomDataXmlFilesFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRandomDataDatabasePassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdLookupRandomDataDatabase);
            this.Controls.Add(this.txtRandomDataDatabase);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.CheckBox chkGenerateRandomWords;
        internal System.Windows.Forms.TextBox txtRandomDataDatabasePassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdLookupRandomDataDatabase;
        internal System.Windows.Forms.TextBox txtRandomDataDatabase;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtNumWordsToOutput;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.ComboBox cboRandomDataXmlFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtRandomDataXmlFilesFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkGenerateSampleRandomSentences;
        internal System.Windows.Forms.TextBox txtNumSentencesToOutput;
        private System.Windows.Forms.CheckBox chkGenerateRandomSentences;
        private System.Windows.Forms.CheckBox chkGenerateRandomParagraphs;
        internal System.Windows.Forms.TextBox txtNumParagraphsToOutput;
        private System.Windows.Forms.CheckBox chkGenerateRandomDocument;
        private System.Windows.Forms.CheckBox chkGenerateRandomBook;
        internal System.Windows.Forms.TextBox txtNumChaptersToOutput;
        private System.Windows.Forms.CheckBox chkGenerateRandomChapter;
        private System.Windows.Forms.CheckBox chkGenerateSentenceTemplateArrays;
        private System.Windows.Forms.Button cmdShowHideOutputLog;
    }
}

