namespace pfDataExtractorCP
{
#pragma warning disable 1591
    partial class TestDataGeneratorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestDataGeneratorForm));
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdGenerateTestData = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainMenuContextMenuRunTest = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mainMenuOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenuToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdRunRandomizer = new System.Windows.Forms.Button();
            this.mainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblSourceDataSchemaFrom = new System.Windows.Forms.Label();
            this.cmdPreviewTestData = new System.Windows.Forms.Button();
            this.txtSourceDataSchemaFrom = new System.Windows.Forms.TextBox();
            this.lblDestinationDataOutputTo = new System.Windows.Forms.Label();
            this.txtDestinationDataOutputTo = new System.Windows.Forms.TextBox();
            this.lblNumRowsToGenerate = new System.Windows.Forms.Label();
            this.txtNumRowsToGenerate = new System.Windows.Forms.TextBox();
            this.lblNumRowsToPreview = new System.Windows.Forms.Label();
            this.txtNumRowsToPreview = new System.Windows.Forms.TextBox();
            this.lblSuggestRandomizerUse = new System.Windows.Forms.Label();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.MainMenu.SuspendLayout();
            this.mainMenuContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(469, 217);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(134, 37);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "E&xit";
            this.mainMenuToolTips.SetToolTip(this.cmdExit, "Close form and exit application");
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdGenerateTestData
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdGenerateTestData, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdGenerateTestData, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdGenerateTestData, "Help for Run Tests: See Help File.");
            this.cmdGenerateTestData.Location = new System.Drawing.Point(469, 48);
            this.cmdGenerateTestData.Name = "cmdGenerateTestData";
            this.appHelpProvider.SetShowHelp(this.cmdGenerateTestData, true);
            this.cmdGenerateTestData.Size = new System.Drawing.Size(134, 37);
            this.cmdGenerateTestData.TabIndex = 1;
            this.cmdGenerateTestData.Text = "&Generate Test Data";
            this.mainMenuToolTips.SetToolTip(this.cmdGenerateTestData, "Run selected tests");
            this.cmdGenerateTestData.UseVisualStyleBackColor = true;
            this.cmdGenerateTestData.Click += new System.EventHandler(this.cmdGenerateTestData_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp,
            this.toolbarHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(629, 27);
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
            // mainMenuContextMenuStrip
            // 
            this.mainMenuContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuContextMenuRunTest,
            this.helpToolStripMenuItem});
            this.mainMenuContextMenuStrip.Name = "mainMenuContextMenuStrip";
            this.mainMenuContextMenuStrip.Size = new System.Drawing.Size(121, 48);
            // 
            // mainMenuContextMenuRunTest
            // 
            this.mainMenuContextMenuRunTest.Name = "mainMenuContextMenuRunTest";
            this.mainMenuContextMenuRunTest.Size = new System.Drawing.Size(120, 22);
            this.mainMenuContextMenuRunTest.Text = "Run Test";
            this.mainMenuContextMenuRunTest.Click += new System.EventHandler(this.mainMenuContextMenuRunTest_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\pfDataExtractorCP\\InitWinFormsAppWithToolbar" +
    "\\pfDataExtractorCP.chm";
            // 
            // cmdRunRandomizer
            // 
            this.cmdRunRandomizer.Location = new System.Drawing.Point(469, 100);
            this.cmdRunRandomizer.Name = "cmdRunRandomizer";
            this.cmdRunRandomizer.Size = new System.Drawing.Size(134, 37);
            this.cmdRunRandomizer.TabIndex = 9;
            this.cmdRunRandomizer.Text = "Randomizer";
            this.mainMenuToolTips.SetToolTip(this.cmdRunRandomizer, "Use randomizer to specify how to generate random data for each field in source da" +
        "ta.");
            this.cmdRunRandomizer.UseVisualStyleBackColor = true;
            this.cmdRunRandomizer.Click += new System.EventHandler(this.cmdRunRandomizer_Click);
            // 
            // lblSourceDataSchemaFrom
            // 
            this.lblSourceDataSchemaFrom.AutoSize = true;
            this.lblSourceDataSchemaFrom.Location = new System.Drawing.Point(23, 48);
            this.lblSourceDataSchemaFrom.Name = "lblSourceDataSchemaFrom";
            this.lblSourceDataSchemaFrom.Size = new System.Drawing.Size(138, 13);
            this.lblSourceDataSchemaFrom.TabIndex = 5;
            this.lblSourceDataSchemaFrom.Text = "Source Data Schema From ";
            // 
            // cmdPreviewTestData
            // 
            this.cmdPreviewTestData.Location = new System.Drawing.Point(469, 158);
            this.cmdPreviewTestData.Name = "cmdPreviewTestData";
            this.cmdPreviewTestData.Size = new System.Drawing.Size(134, 37);
            this.cmdPreviewTestData.TabIndex = 6;
            this.cmdPreviewTestData.Text = "Preview Test Data";
            this.cmdPreviewTestData.UseVisualStyleBackColor = true;
            this.cmdPreviewTestData.Click += new System.EventHandler(this.cmdPreviewTestData_Click);
            // 
            // txtSourceDataSchemaFrom
            // 
            this.txtSourceDataSchemaFrom.Location = new System.Drawing.Point(186, 48);
            this.txtSourceDataSchemaFrom.Name = "txtSourceDataSchemaFrom";
            this.txtSourceDataSchemaFrom.ReadOnly = true;
            this.txtSourceDataSchemaFrom.Size = new System.Drawing.Size(197, 20);
            this.txtSourceDataSchemaFrom.TabIndex = 7;
            // 
            // lblDestinationDataOutputTo
            // 
            this.lblDestinationDataOutputTo.AutoSize = true;
            this.lblDestinationDataOutputTo.Location = new System.Drawing.Point(23, 89);
            this.lblDestinationDataOutputTo.Name = "lblDestinationDataOutputTo";
            this.lblDestinationDataOutputTo.Size = new System.Drawing.Size(137, 13);
            this.lblDestinationDataOutputTo.TabIndex = 10;
            this.lblDestinationDataOutputTo.Text = "Destination Data Output To";
            // 
            // txtDestinationDataOutputTo
            // 
            this.txtDestinationDataOutputTo.Location = new System.Drawing.Point(186, 89);
            this.txtDestinationDataOutputTo.Name = "txtDestinationDataOutputTo";
            this.txtDestinationDataOutputTo.ReadOnly = true;
            this.txtDestinationDataOutputTo.Size = new System.Drawing.Size(197, 20);
            this.txtDestinationDataOutputTo.TabIndex = 11;
            // 
            // lblNumRowsToGenerate
            // 
            this.lblNumRowsToGenerate.AutoSize = true;
            this.lblNumRowsToGenerate.Location = new System.Drawing.Point(23, 136);
            this.lblNumRowsToGenerate.Name = "lblNumRowsToGenerate";
            this.lblNumRowsToGenerate.Size = new System.Drawing.Size(149, 13);
            this.lblNumRowsToGenerate.TabIndex = 12;
            this.lblNumRowsToGenerate.Text = "Number of Rows To Generate";
            // 
            // txtNumRowsToGenerate
            // 
            this.txtNumRowsToGenerate.Location = new System.Drawing.Point(186, 133);
            this.txtNumRowsToGenerate.Name = "txtNumRowsToGenerate";
            this.txtNumRowsToGenerate.Size = new System.Drawing.Size(95, 20);
            this.txtNumRowsToGenerate.TabIndex = 13;
            // 
            // lblNumRowsToPreview
            // 
            this.lblNumRowsToPreview.AutoSize = true;
            this.lblNumRowsToPreview.Location = new System.Drawing.Point(23, 178);
            this.lblNumRowsToPreview.Name = "lblNumRowsToPreview";
            this.lblNumRowsToPreview.Size = new System.Drawing.Size(143, 13);
            this.lblNumRowsToPreview.TabIndex = 14;
            this.lblNumRowsToPreview.Text = "Number of Rows To Preview";
            // 
            // txtNumRowsToPreview
            // 
            this.txtNumRowsToPreview.Location = new System.Drawing.Point(186, 175);
            this.txtNumRowsToPreview.Name = "txtNumRowsToPreview";
            this.txtNumRowsToPreview.Size = new System.Drawing.Size(95, 20);
            this.txtNumRowsToPreview.TabIndex = 15;
            // 
            // lblSuggestRandomizerUse
            // 
            this.lblSuggestRandomizerUse.AutoSize = true;
            this.lblSuggestRandomizerUse.Location = new System.Drawing.Point(23, 217);
            this.lblSuggestRandomizerUse.Name = "lblSuggestRandomizerUse";
            this.lblSuggestRandomizerUse.Size = new System.Drawing.Size(254, 26);
            this.lblSuggestRandomizerUse.TabIndex = 16;
            this.lblSuggestRandomizerUse.Text = "Use the Randomizer button on this form to specifiy \r\nnon-default random values to" +
    " generate in the output.";
            this.lblSuggestRandomizerUse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // TestDataGeneratorForm
            // 
            this.AcceptButton = this.cmdGenerateTestData;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(629, 283);
            this.Controls.Add(this.lblSuggestRandomizerUse);
            this.Controls.Add(this.txtNumRowsToPreview);
            this.Controls.Add(this.lblNumRowsToPreview);
            this.Controls.Add(this.txtNumRowsToGenerate);
            this.Controls.Add(this.lblNumRowsToGenerate);
            this.Controls.Add(this.txtDestinationDataOutputTo);
            this.Controls.Add(this.lblDestinationDataOutputTo);
            this.Controls.Add(this.txtSourceDataSchemaFrom);
            this.Controls.Add(this.cmdRunRandomizer);
            this.Controls.Add(this.cmdPreviewTestData);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.lblSourceDataSchemaFrom);
            this.Controls.Add(this.cmdExit);
            this.Controls.Add(this.cmdGenerateTestData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestDataGeneratorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test data generator ...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.mainMenuContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdGenerateTestData;
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
        private System.Windows.Forms.OpenFileDialog mainMenuOpenFileDialog;
        private System.Windows.Forms.ContextMenuStrip mainMenuContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mainMenuContextMenuRunTest;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolTip mainMenuToolTips;
        private System.Windows.Forms.SaveFileDialog mainMenuSaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog mainMenuFolderBrowserDialog;
        private System.Windows.Forms.Label lblSourceDataSchemaFrom;
        private System.Windows.Forms.Button cmdPreviewTestData;
        private System.Windows.Forms.TextBox txtSourceDataSchemaFrom;
        private System.Windows.Forms.Button cmdRunRandomizer;
        private System.Windows.Forms.Label lblDestinationDataOutputTo;
        private System.Windows.Forms.TextBox txtDestinationDataOutputTo;
        private System.Windows.Forms.Label lblNumRowsToGenerate;
        private System.Windows.Forms.TextBox txtNumRowsToGenerate;
        private System.Windows.Forms.Label lblNumRowsToPreview;
        private System.Windows.Forms.TextBox txtNumRowsToPreview;
        private System.Windows.Forms.Label lblSuggestRandomizerUse;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
    }
#pragma warning restore 1591
}

