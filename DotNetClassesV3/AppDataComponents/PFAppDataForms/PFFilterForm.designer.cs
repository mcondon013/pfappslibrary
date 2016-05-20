namespace PFAppDataForms
{
#pragma warning disable 1591
    partial class PFFilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PFFilterForm));
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
            this.mainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblQueryText = new System.Windows.Forms.Label();
            this.txtFilterText = new System.Windows.Forms.TextBox();
            this.txtQueryText = new System.Windows.Forms.TextBox();
            this.lblFilterText = new System.Windows.Forms.Label();
            this.cmdFilterBuilder = new System.Windows.Forms.Button();
            this.cmdVerifyFilter = new System.Windows.Forms.Button();
            this.cmdDeleteFilter = new System.Windows.Forms.Button();
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
            this.cmdCancel.Location = new System.Drawing.Point(430, 317);
            this.cmdCancel.Name = "cmdCancel";
            this.appHelpProvider.SetShowHelp(this.cmdCancel, true);
            this.cmdCancel.Size = new System.Drawing.Size(93, 37);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "&Cancel";
            this.mainMenuToolTips.SetToolTip(this.cmdCancel, "Close form and exit application");
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdAccept, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdAccept, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdAccept, "Help for Run Tests: See Help File.");
            this.cmdAccept.Location = new System.Drawing.Point(430, 169);
            this.cmdAccept.Name = "cmdAccept";
            this.appHelpProvider.SetShowHelp(this.cmdAccept, true);
            this.cmdAccept.Size = new System.Drawing.Size(93, 37);
            this.cmdAccept.TabIndex = 4;
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
            this.MainMenu.Size = new System.Drawing.Size(545, 27);
            this.MainMenu.TabIndex = 6;
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
            this.mainMenuContextMenuRunTest.Text = "Run LoadTextLines";
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFAppDataForms\\InitWinFormsAppWithToolbar\\" +
    "InitWinFormsHelpFile.chm";
            // 
            // lblQueryText
            // 
            this.lblQueryText.AutoSize = true;
            this.lblQueryText.Location = new System.Drawing.Point(24, 40);
            this.lblQueryText.Name = "lblQueryText";
            this.lblQueryText.Size = new System.Drawing.Size(59, 13);
            this.lblQueryText.TabIndex = 7;
            this.lblQueryText.Text = "Query Text";
            // 
            // txtFilterText
            // 
            this.txtFilterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterText.Location = new System.Drawing.Point(27, 188);
            this.txtFilterText.Multiline = true;
            this.txtFilterText.Name = "txtFilterText";
            this.txtFilterText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFilterText.Size = new System.Drawing.Size(386, 166);
            this.txtFilterText.TabIndex = 1;
            // 
            // txtQueryText
            // 
            this.txtQueryText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQueryText.Location = new System.Drawing.Point(27, 57);
            this.txtQueryText.Multiline = true;
            this.txtQueryText.Name = "txtQueryText";
            this.txtQueryText.ReadOnly = true;
            this.txtQueryText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQueryText.Size = new System.Drawing.Size(496, 87);
            this.txtQueryText.TabIndex = 8;
            // 
            // lblFilterText
            // 
            this.lblFilterText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilterText.AutoSize = true;
            this.lblFilterText.Location = new System.Drawing.Point(27, 169);
            this.lblFilterText.Name = "lblFilterText";
            this.lblFilterText.Size = new System.Drawing.Size(53, 13);
            this.lblFilterText.TabIndex = 0;
            this.lblFilterText.Text = "Filter Text";
            // 
            // cmdFilterBuilder
            // 
            this.cmdFilterBuilder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFilterBuilder.Location = new System.Drawing.Point(318, 166);
            this.cmdFilterBuilder.Name = "cmdFilterBuilder";
            this.cmdFilterBuilder.Size = new System.Drawing.Size(73, 23);
            this.cmdFilterBuilder.TabIndex = 3;
            this.cmdFilterBuilder.Text = "Filter Builder";
            this.cmdFilterBuilder.UseVisualStyleBackColor = true;
            this.cmdFilterBuilder.Click += new System.EventHandler(this.cmdFilterBuilder_Click);
            // 
            // cmdVerifyFilter
            // 
            this.cmdVerifyFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdVerifyFilter.Location = new System.Drawing.Point(221, 166);
            this.cmdVerifyFilter.Name = "cmdVerifyFilter";
            this.cmdVerifyFilter.Size = new System.Drawing.Size(75, 23);
            this.cmdVerifyFilter.TabIndex = 2;
            this.cmdVerifyFilter.Text = "Verify Filter";
            this.cmdVerifyFilter.UseVisualStyleBackColor = true;
            this.cmdVerifyFilter.Click += new System.EventHandler(this.cmdVerifyFilter_Click);
            // 
            // cmdDeleteFilter
            // 
            this.cmdDeleteFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDeleteFilter.Location = new System.Drawing.Point(31, 354);
            this.cmdDeleteFilter.Name = "cmdDeleteFilter";
            this.cmdDeleteFilter.Size = new System.Drawing.Size(75, 23);
            this.cmdDeleteFilter.TabIndex = 9;
            this.cmdDeleteFilter.Text = "Delete Filter";
            this.cmdDeleteFilter.UseVisualStyleBackColor = true;
            this.cmdDeleteFilter.Click += new System.EventHandler(this.cmdDeleteFilter_Click);
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
            // PFFilterForm
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(545, 400);
            this.Controls.Add(this.cmdDeleteFilter);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdVerifyFilter);
            this.Controls.Add(this.cmdFilterBuilder);
            this.Controls.Add(this.lblFilterText);
            this.Controls.Add(this.txtQueryText);
            this.Controls.Add(this.txtFilterText);
            this.Controls.Add(this.lblQueryText);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.cmdCancel);
            this.Name = "PFFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter Form";
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
        private System.Windows.Forms.Label lblQueryText;
        private System.Windows.Forms.TextBox txtFilterText;
        private System.Windows.Forms.TextBox txtQueryText;
        private System.Windows.Forms.Label lblFilterText;
        private System.Windows.Forms.Button cmdFilterBuilder;
        private System.Windows.Forms.Button cmdVerifyFilter;
        private System.Windows.Forms.Button cmdDeleteFilter;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
    }
#pragma warning restore 1591
}

