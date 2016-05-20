namespace TestprogAppDataForms
{
#pragma warning disable 1591
    partial class UserOptionsForm
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
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblFormMessage = new System.Windows.Forms.Label();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.optionsFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdSeUserOption1 = new System.Windows.Forms.Button();
            this.cmdSetUserOption2 = new System.Windows.Forms.Button();
            this.lblUserOption1 = new System.Windows.Forms.Label();
            this.txtUserOption1 = new System.Windows.Forms.TextBox();
            this.lblUserOption2 = new System.Windows.Forms.Label();
            this.txtUserOption2 = new System.Windows.Forms.TextBox();
            this.chkSaveFormLocationsOnExit = new System.Windows.Forms.CheckBox();
            this.lblUpdateResults = new System.Windows.Forms.Label();
            this.UserOptionsMenu = new System.Windows.Forms.MenuStrip();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettingsRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.cboUserOption4 = new System.Windows.Forms.ComboBox();
            this.lblUserOption4 = new System.Windows.Forms.Label();
            this.UserOptionsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdReset.Location = new System.Drawing.Point(501, 208);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(93, 37);
            this.cmdReset.TabIndex = 12;
            this.cmdReset.Text = "&Reset";
            this.optionsFormToolTips.SetToolTip(this.cmdReset, "Reset all app settings to last saved values.");
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHelp.Location = new System.Drawing.Point(501, 287);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(93, 37);
            this.cmdHelp.TabIndex = 13;
            this.cmdHelp.Text = "&Help";
            this.optionsFormToolTips.SetToolTip(this.cmdHelp, "Show Help for this form.");
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdApply.Location = new System.Drawing.Point(501, 111);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(93, 37);
            this.cmdApply.TabIndex = 11;
            this.cmdApply.Text = "&Apply";
            this.optionsFormToolTips.SetToolTip(this.cmdApply, "Save changes and leave form open.");
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(501, 36);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(93, 37);
            this.cmdOK.TabIndex = 10;
            this.cmdOK.Text = "&OK";
            this.optionsFormToolTips.SetToolTip(this.cmdOK, "Save changes and close form.");
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(501, 354);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(93, 37);
            this.cmdCancel.TabIndex = 14;
            this.cmdCancel.Text = "&Cancel";
            this.optionsFormToolTips.SetToolTip(this.cmdCancel, "Close this form and cancel any pending updates.");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblFormMessage
            // 
            this.lblFormMessage.AutoSize = true;
            this.lblFormMessage.Location = new System.Drawing.Point(37, 36);
            this.lblFormMessage.Name = "lblFormMessage";
            this.lblFormMessage.Size = new System.Drawing.Size(301, 13);
            this.lblFormMessage.TabIndex = 1;
            this.lblFormMessage.Text = "Make changes to values below and press OK or Apply to save";
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\TestprogAppDataForms\\InitWinFormsAppWithUser" +
    "AndAppSettings\\InitWinFormsHelpFile.chm";
            // 
            // cmdSeUserOption1
            // 
            this.cmdSeUserOption1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSeUserOption1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSeUserOption1.Location = new System.Drawing.Point(412, 123);
            this.cmdSeUserOption1.Name = "cmdSeUserOption1";
            this.cmdSeUserOption1.Size = new System.Drawing.Size(38, 20);
            this.cmdSeUserOption1.TabIndex = 5;
            this.cmdSeUserOption1.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSeUserOption1, "Prompt to select initial folder path");
            this.cmdSeUserOption1.UseVisualStyleBackColor = true;
            this.cmdSeUserOption1.Click += new System.EventHandler(this.cmdSetUserOption1_Click);
            // 
            // cmdSetUserOption2
            // 
            this.cmdSetUserOption2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetUserOption2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetUserOption2.Location = new System.Drawing.Point(412, 173);
            this.cmdSetUserOption2.Name = "cmdSetUserOption2";
            this.cmdSetUserOption2.Size = new System.Drawing.Size(38, 20);
            this.cmdSetUserOption2.TabIndex = 8;
            this.cmdSetUserOption2.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetUserOption2, "Prompt to select folder for saving statistics files");
            this.cmdSetUserOption2.UseVisualStyleBackColor = true;
            this.cmdSetUserOption2.Click += new System.EventHandler(this.cmdSetUserOption2_Click);
            // 
            // lblUserOption1
            // 
            this.lblUserOption1.AutoSize = true;
            this.lblUserOption1.Location = new System.Drawing.Point(37, 123);
            this.lblUserOption1.Name = "lblUserOption1";
            this.lblUserOption1.Size = new System.Drawing.Size(72, 13);
            this.lblUserOption1.TabIndex = 3;
            this.lblUserOption1.Text = "User Option 1";
            // 
            // txtUserOption1
            // 
            this.txtUserOption1.Location = new System.Drawing.Point(40, 140);
            this.txtUserOption1.Name = "txtUserOption1";
            this.txtUserOption1.Size = new System.Drawing.Size(410, 20);
            this.txtUserOption1.TabIndex = 4;
            // 
            // lblUserOption2
            // 
            this.lblUserOption2.AutoSize = true;
            this.lblUserOption2.Location = new System.Drawing.Point(37, 173);
            this.lblUserOption2.Name = "lblUserOption2";
            this.lblUserOption2.Size = new System.Drawing.Size(72, 13);
            this.lblUserOption2.TabIndex = 6;
            this.lblUserOption2.Text = "User Option 2";
            // 
            // txtUserOption2
            // 
            this.txtUserOption2.Location = new System.Drawing.Point(40, 190);
            this.txtUserOption2.Name = "txtUserOption2";
            this.txtUserOption2.Size = new System.Drawing.Size(410, 20);
            this.txtUserOption2.TabIndex = 7;
            // 
            // chkSaveFormLocationsOnExit
            // 
            this.chkSaveFormLocationsOnExit.AutoSize = true;
            this.chkSaveFormLocationsOnExit.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkSaveFormLocationsOnExit.Location = new System.Drawing.Point(40, 76);
            this.chkSaveFormLocationsOnExit.Name = "chkSaveFormLocationsOnExit";
            this.chkSaveFormLocationsOnExit.Size = new System.Drawing.Size(161, 17);
            this.chkSaveFormLocationsOnExit.TabIndex = 9;
            this.chkSaveFormLocationsOnExit.Text = "Save Form Locations on Exit";
            this.chkSaveFormLocationsOnExit.UseVisualStyleBackColor = true;
            // 
            // lblUpdateResults
            // 
            this.lblUpdateResults.AutoSize = true;
            this.lblUpdateResults.Location = new System.Drawing.Point(37, 60);
            this.lblUpdateResults.Name = "lblUpdateResults";
            this.lblUpdateResults.Size = new System.Drawing.Size(13, 13);
            this.lblUpdateResults.TabIndex = 2;
            this.lblUpdateResults.Text = "  ";
            // 
            // UserOptionsMenu
            // 
            this.UserOptionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings});
            this.UserOptionsMenu.Location = new System.Drawing.Point(0, 0);
            this.UserOptionsMenu.Name = "UserOptionsMenu";
            this.UserOptionsMenu.Size = new System.Drawing.Size(635, 24);
            this.UserOptionsMenu.TabIndex = 0;
            this.UserOptionsMenu.Text = "menuStrip1";
            // 
            // mnuSettings
            // 
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileAccept,
            this.toolStripSeparator2,
            this.mnuFilePageSetup,
            this.mnuFilePrint,
            this.mnuFilePrintPreview,
            this.toolStripSeparator3,
            this.mnuSettingsRestore,
            this.toolStripSeparator1,
            this.mnuFileCancel});
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(61, 20);
            this.mnuSettings.Text = "&Settings";
            // 
            // mnuFileAccept
            // 
            this.mnuFileAccept.Name = "mnuFileAccept";
            this.mnuFileAccept.Size = new System.Drawing.Size(143, 22);
            this.mnuFileAccept.Text = "&Accept";
            this.mnuFileAccept.ToolTipText = "Save changes and return to main menu.";
            this.mnuFileAccept.Click += new System.EventHandler(this.mnuSettingsAccept_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFilePageSetup
            // 
            this.mnuFilePageSetup.Name = "mnuFilePageSetup";
            this.mnuFilePageSetup.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePageSetup.Text = "Page Set&up";
            this.mnuFilePageSetup.ToolTipText = "Page setup for printing.";
            this.mnuFilePageSetup.Click += new System.EventHandler(this.mnuSettingsPageSetup_Click);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePrint.Text = "&Print";
            this.mnuFilePrint.ToolTipText = "Output list of user settings to printer.";
            this.mnuFilePrint.Click += new System.EventHandler(this.mnuSettingsPrint_Click);
            // 
            // mnuFilePrintPreview
            // 
            this.mnuFilePrintPreview.Name = "mnuFilePrintPreview";
            this.mnuFilePrintPreview.Size = new System.Drawing.Size(143, 22);
            this.mnuFilePrintPreview.Text = "Print Pre&view";
            this.mnuFilePrintPreview.ToolTipText = "Show user settings in print preview screen.";
            this.mnuFilePrintPreview.Click += new System.EventHandler(this.mnuSettingsPrintPreview_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuSettingsRestore
            // 
            this.mnuSettingsRestore.Name = "mnuSettingsRestore";
            this.mnuSettingsRestore.Size = new System.Drawing.Size(143, 22);
            this.mnuSettingsRestore.Text = "&Restore";
            this.mnuSettingsRestore.ToolTipText = "WARNING: Overwrites current user settings with original settings from installatio" +
    "n.";
            this.mnuSettingsRestore.Click += new System.EventHandler(this.mnuSettingsRestore_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFileCancel
            // 
            this.mnuFileCancel.Name = "mnuFileCancel";
            this.mnuFileCancel.Size = new System.Drawing.Size(143, 22);
            this.mnuFileCancel.Text = "&Cancel";
            this.mnuFileCancel.ToolTipText = "Abandon any pending changes and return to main form.";
            this.mnuFileCancel.Click += new System.EventHandler(this.mnuSettingsCancel_Click);
            // 
            // cboUserOption4
            // 
            this.cboUserOption4.FormattingEnabled = true;
            this.cboUserOption4.Items.AddRange(new object[] {
            "First",
            "Second",
            "Third"});
            this.cboUserOption4.Location = new System.Drawing.Point(40, 251);
            this.cboUserOption4.Name = "cboUserOption4";
            this.cboUserOption4.Size = new System.Drawing.Size(407, 21);
            this.cboUserOption4.TabIndex = 15;
            this.cboUserOption4.Text = "First";
            // 
            // lblUserOption4
            // 
            this.lblUserOption4.AutoSize = true;
            this.lblUserOption4.Location = new System.Drawing.Point(37, 232);
            this.lblUserOption4.Name = "lblUserOption4";
            this.lblUserOption4.Size = new System.Drawing.Size(72, 13);
            this.lblUserOption4.TabIndex = 16;
            this.lblUserOption4.Text = "User Option 4";
            // 
            // UserOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 448);
            this.Controls.Add(this.lblUserOption4);
            this.Controls.Add(this.cboUserOption4);
            this.Controls.Add(this.lblUpdateResults);
            this.Controls.Add(this.chkSaveFormLocationsOnExit);
            this.Controls.Add(this.cmdSetUserOption2);
            this.Controls.Add(this.txtUserOption2);
            this.Controls.Add(this.lblUserOption2);
            this.Controls.Add(this.cmdSeUserOption1);
            this.Controls.Add(this.txtUserOption1);
            this.Controls.Add(this.lblUserOption1);
            this.Controls.Add(this.lblFormMessage);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.UserOptionsMenu);
            this.MainMenuStrip = this.UserOptionsMenu;
            this.Name = "UserOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Options for TestprogAppDataForms";
            this.Load += new System.EventHandler(this.UserOptionsForm_Load);
            this.UserOptionsMenu.ResumeLayout(false);
            this.UserOptionsMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.Button cmdHelp;
        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblFormMessage;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.ToolTip optionsFormToolTips;
        private System.Windows.Forms.Label lblUserOption1;
        private System.Windows.Forms.TextBox txtUserOption1;
        private System.Windows.Forms.Button cmdSeUserOption1;
        private System.Windows.Forms.Label lblUserOption2;
        private System.Windows.Forms.TextBox txtUserOption2;
        private System.Windows.Forms.CheckBox chkSaveFormLocationsOnExit;
        private System.Windows.Forms.Label lblUpdateResults;
        private System.Windows.Forms.MenuStrip UserOptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuFileAccept;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsRestore;
        private System.Windows.Forms.Button cmdSetUserOption2;
        private System.Windows.Forms.ComboBox cboUserOption4;
        private System.Windows.Forms.Label lblUserOption4;

    }
#pragma warning restore 1591
}
