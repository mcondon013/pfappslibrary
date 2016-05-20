namespace PFAppDataForms
{
    partial class PFFixedLenColDefsOutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PFFixedLenColDefsOutputForm));
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
            this.mainMenuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainMenuContextMenuRunTest = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mainMenuOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenuToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdUpdateList = new System.Windows.Forms.Button();
            this.txtColumnName = new System.Windows.Forms.TextBox();
            this.txtColumnWidth = new System.Windows.Forms.TextBox();
            this.txtColumnNumber = new System.Windows.Forms.TextBox();
            this.cboValueAlignment = new System.Windows.Forms.ComboBox();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.mainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.listviewColDefs = new System.Windows.Forms.ListView();
            this.lblColumnNumber = new System.Windows.Forms.Label();
            this.lblColumnName = new System.Windows.Forms.Label();
            this.lblColumnWidth = new System.Windows.Forms.Label();
            this.lblValueAlignment = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.MainMenu.SuspendLayout();
            this.mainMenuContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdCancel, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdCancel, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdCancel.Location = new System.Drawing.Point(519, 370);
            this.cmdCancel.Name = "cmdCancel";
            this.appHelpProvider.SetShowHelp(this.cmdCancel, true);
            this.cmdCancel.Size = new System.Drawing.Size(93, 37);
            this.cmdCancel.TabIndex = 18;
            this.cmdCancel.Text = "&Cancel";
            this.mainMenuToolTips.SetToolTip(this.cmdCancel, "Close form and exit application");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdAccept, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdAccept, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdAccept, "Help for Run Tests: See Help File.");
            this.cmdAccept.Location = new System.Drawing.Point(519, 40);
            this.cmdAccept.Name = "cmdAccept";
            this.appHelpProvider.SetShowHelp(this.cmdAccept, true);
            this.cmdAccept.Size = new System.Drawing.Size(93, 37);
            this.cmdAccept.TabIndex = 17;
            this.cmdAccept.Text = "&Accept";
            this.mainMenuToolTips.SetToolTip(this.cmdAccept, "Run selected tests");
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdProcessForm_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp,
            this.toolbarHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(636, 27);
            this.MainMenu.TabIndex = 0;
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
            this.mainMenuContextMenuStrip.Size = new System.Drawing.Size(174, 48);
            // 
            // mainMenuContextMenuRunTest
            // 
            this.mainMenuContextMenuRunTest.Name = "mainMenuContextMenuRunTest";
            this.mainMenuContextMenuRunTest.Size = new System.Drawing.Size(173, 22);
            this.mainMenuContextMenuRunTest.Text = "Run LoadTextLines";
            this.mainMenuContextMenuRunTest.Click += new System.EventHandler(this.mainMenuContextMenuRunTest_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFAppDataForms\\InitWinFormsAppWithToolbar\\" +
    "InitWinFormsHelpFile.chm";
            // 
            // cmdUpdateList
            // 
            this.cmdUpdateList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateList.Location = new System.Drawing.Point(423, 279);
            this.cmdUpdateList.Name = "cmdUpdateList";
            this.cmdUpdateList.Size = new System.Drawing.Size(75, 38);
            this.cmdUpdateList.TabIndex = 13;
            this.cmdUpdateList.Text = "Update";
            this.mainMenuToolTips.SetToolTip(this.cmdUpdateList, "Update item information on list.");
            this.cmdUpdateList.UseVisualStyleBackColor = true;
            this.cmdUpdateList.Click += new System.EventHandler(this.cmdUpdateList_Click);
            // 
            // txtColumnName
            // 
            this.txtColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtColumnName.Location = new System.Drawing.Point(113, 327);
            this.txtColumnName.Name = "txtColumnName";
            this.txtColumnName.Size = new System.Drawing.Size(288, 20);
            this.txtColumnName.TabIndex = 8;
            this.mainMenuToolTips.SetToolTip(this.txtColumnName, "Column Name for output.");
            // 
            // txtColumnWidth
            // 
            this.txtColumnWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtColumnWidth.Location = new System.Drawing.Point(113, 353);
            this.txtColumnWidth.Name = "txtColumnWidth";
            this.txtColumnWidth.Size = new System.Drawing.Size(76, 20);
            this.txtColumnWidth.TabIndex = 10;
            this.mainMenuToolTips.SetToolTip(this.txtColumnWidth, "Column width (length) of output text value.");
            // 
            // txtColumnNumber
            // 
            this.txtColumnNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtColumnNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtColumnNumber.Location = new System.Drawing.Point(113, 275);
            this.txtColumnNumber.Name = "txtColumnNumber";
            this.txtColumnNumber.ReadOnly = true;
            this.txtColumnNumber.Size = new System.Drawing.Size(76, 20);
            this.txtColumnNumber.TabIndex = 4;
            this.mainMenuToolTips.SetToolTip(this.txtColumnNumber, "Column number.");
            // 
            // cboValueAlignment
            // 
            this.cboValueAlignment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboValueAlignment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboValueAlignment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboValueAlignment.DisplayMember = "Set output text alignment for the column\'s value.";
            this.cboValueAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboValueAlignment.FormattingEnabled = true;
            this.cboValueAlignment.Items.AddRange(new object[] {
            "LeftJustify",
            "RightJustify"});
            this.cboValueAlignment.Location = new System.Drawing.Point(113, 379);
            this.cboValueAlignment.Name = "cboValueAlignment";
            this.cboValueAlignment.Size = new System.Drawing.Size(159, 21);
            this.cboValueAlignment.TabIndex = 12;
            this.cboValueAlignment.Tag = "";
            this.mainMenuToolTips.SetToolTip(this.cboValueAlignment, "Set output text alignment for the column\'s value.");
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Location = new System.Drawing.Point(519, 174);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(93, 37);
            this.cmdPrint.TabIndex = 19;
            this.cmdPrint.Text = "&Print";
            this.mainMenuToolTips.SetToolTip(this.cmdPrint, "Print with Preview contents of this form.");
            this.cmdPrint.UseVisualStyleBackColor = true;
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // listviewColDefs
            // 
            this.listviewColDefs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewColDefs.Location = new System.Drawing.Point(24, 40);
            this.listviewColDefs.Name = "listviewColDefs";
            this.listviewColDefs.Size = new System.Drawing.Size(474, 223);
            this.listviewColDefs.TabIndex = 2;
            this.listviewColDefs.UseCompatibleStateImageBehavior = false;
            this.listviewColDefs.SelectedIndexChanged += new System.EventHandler(this.listviewColDefs_SelectedIndexChanged);
            // 
            // lblColumnNumber
            // 
            this.lblColumnNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblColumnNumber.AutoSize = true;
            this.lblColumnNumber.Location = new System.Drawing.Point(24, 279);
            this.lblColumnNumber.Name = "lblColumnNumber";
            this.lblColumnNumber.Size = new System.Drawing.Size(65, 13);
            this.lblColumnNumber.TabIndex = 3;
            this.lblColumnNumber.Text = "Column. No.";
            // 
            // lblColumnName
            // 
            this.lblColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblColumnName.AutoSize = true;
            this.lblColumnName.Location = new System.Drawing.Point(24, 327);
            this.lblColumnName.Name = "lblColumnName";
            this.lblColumnName.Size = new System.Drawing.Size(73, 13);
            this.lblColumnName.TabIndex = 7;
            this.lblColumnName.Text = "Column Name";
            // 
            // lblColumnWidth
            // 
            this.lblColumnWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblColumnWidth.AutoSize = true;
            this.lblColumnWidth.Location = new System.Drawing.Point(24, 352);
            this.lblColumnWidth.Name = "lblColumnWidth";
            this.lblColumnWidth.Size = new System.Drawing.Size(73, 13);
            this.lblColumnWidth.TabIndex = 9;
            this.lblColumnWidth.Text = "Column Width";
            // 
            // lblValueAlignment
            // 
            this.lblValueAlignment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblValueAlignment.AutoSize = true;
            this.lblValueAlignment.Location = new System.Drawing.Point(23, 376);
            this.lblValueAlignment.Name = "lblValueAlignment";
            this.lblValueAlignment.Size = new System.Drawing.Size(83, 13);
            this.lblValueAlignment.TabIndex = 11;
            this.lblValueAlignment.Text = "Value Alignment";
            // 
            // lblFieldName
            // 
            this.lblFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(24, 302);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(60, 13);
            this.lblFieldName.TabIndex = 5;
            this.lblFieldName.Text = "Field Name";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFieldName.Location = new System.Drawing.Point(113, 301);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.ReadOnly = true;
            this.txtFieldName.Size = new System.Drawing.Size(288, 20);
            this.txtFieldName.TabIndex = 6;
            // 
            // toolbarHelp
            // 
            this.toolbarHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolbarHelp.Image")));
            this.toolbarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarHelp.Name = "toolbarHelp";
            this.toolbarHelp.Padding = new System.Windows.Forms.Padding(0, 0, 85, 0);
            this.toolbarHelp.Size = new System.Drawing.Size(105, 20);
            this.toolbarHelp.Text = "Help";
            this.toolbarHelp.Click += new System.EventHandler(this.toolbarHelp_Click);
            // 
            // PFFixedLenColDefsOutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 466);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.txtColumnNumber);
            this.Controls.Add(this.cboValueAlignment);
            this.Controls.Add(this.txtColumnWidth);
            this.Controls.Add(this.txtColumnName);
            this.Controls.Add(this.lblValueAlignment);
            this.Controls.Add(this.lblColumnWidth);
            this.Controls.Add(this.lblColumnNumber);
            this.Controls.Add(this.cmdUpdateList);
            this.Controls.Add(this.lblColumnName);
            this.Controls.Add(this.listviewColDefs);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.cmdCancel);
            this.Name = "PFFixedLenColDefsOutputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fixed Length Column Definitions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.mainMenuContextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.OpenFileDialog mainMenuOpenFileDialog;
        private System.Windows.Forms.ContextMenuStrip mainMenuContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mainMenuContextMenuRunTest;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolTip mainMenuToolTips;
        private System.Windows.Forms.SaveFileDialog mainMenuSaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog mainMenuFolderBrowserDialog;
        private System.Windows.Forms.ListView listviewColDefs;
        private System.Windows.Forms.Label lblColumnNumber;
        private System.Windows.Forms.Label lblColumnName;
        private System.Windows.Forms.Label lblColumnWidth;
        private System.Windows.Forms.Label lblValueAlignment;
        private System.Windows.Forms.TextBox txtColumnName;
        private System.Windows.Forms.TextBox txtColumnWidth;
        private System.Windows.Forms.TextBox txtColumnNumber;
        private System.Windows.Forms.ComboBox cboValueAlignment;
        private System.Windows.Forms.Button cmdUpdateList;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
    }
}

