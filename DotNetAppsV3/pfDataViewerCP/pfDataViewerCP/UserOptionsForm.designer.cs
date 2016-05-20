namespace pfDataViewerCP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserOptionsForm));
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblFormMessage = new System.Windows.Forms.Label();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.optionsFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdGetQueryDefinitionsFolder = new System.Windows.Forms.Button();
            this.cmdGetDataGridExportFolder = new System.Windows.Forms.Button();
            this.lblDefaultQueryDefinitionsFolder = new System.Windows.Forms.Label();
            this.txtDefaultQueryDefinitionsFolder = new System.Windows.Forms.TextBox();
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
            this.cboDefaultInputDatabaseType = new System.Windows.Forms.ComboBox();
            this.lblDefaultInputDatabaseType = new System.Windows.Forms.Label();
            this.chkShowInstalledDatabaseProvidersOnly = new System.Windows.Forms.CheckBox();
            this.chkShowApplicationLogWindow = new System.Windows.Forms.CheckBox();
            this.lblDefaultInputDatabaseConnectionString = new System.Windows.Forms.Label();
            this.txtDefaultInputDatabaseConnectionString = new System.Windows.Forms.TextBox();
            this.cmdDefineInputDatabaseConnectionString = new System.Windows.Forms.Button();
            this.lblDefaultDataGridExportFolder = new System.Windows.Forms.Label();
            this.txtDefaultDataGridExportFolder = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdChangeDefaultApplicationFont = new System.Windows.Forms.Button();
            this.txtDefaultApplicationFont = new System.Windows.Forms.TextBox();
            this.lblDefaultApplicationFont = new System.Windows.Forms.Label();
            this.FontDialog1 = new System.Windows.Forms.FontDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDefaultOutputDatabaseType = new System.Windows.Forms.Label();
            this.cboDefaultOutputDatabaseType = new System.Windows.Forms.ComboBox();
            this.cmdDefineOutputDatabaseConnectionString = new System.Windows.Forms.Button();
            this.lblDefaultOutputDatabaseConnectionString = new System.Windows.Forms.Label();
            this.txtDefaultOutputDatabaseConnectionString = new System.Windows.Forms.TextBox();
            this.lblBatchSizeForDataImportsAndExports = new System.Windows.Forms.Label();
            this.lblBatchSizeForRandomDataGeneration = new System.Windows.Forms.Label();
            this.txtBatchSizeForDataImportsAndExports = new System.Windows.Forms.TextBox();
            this.txtBatchSizeForRandomDataGeneration = new System.Windows.Forms.TextBox();
            this.UserOptionsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(501, 248);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(93, 37);
            this.cmdReset.TabIndex = 23;
            this.cmdReset.Text = "&Reset";
            this.optionsFormToolTips.SetToolTip(this.cmdReset, "Reset all app settings to last saved values.");
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHelp.Location = new System.Drawing.Point(501, 398);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(93, 37);
            this.cmdHelp.TabIndex = 24;
            this.cmdHelp.Text = "&Help";
            this.optionsFormToolTips.SetToolTip(this.cmdHelp, "Show Help for this form.");
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.Location = new System.Drawing.Point(501, 113);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(93, 37);
            this.cmdApply.TabIndex = 22;
            this.cmdApply.Text = "&Apply";
            this.optionsFormToolTips.SetToolTip(this.cmdApply, "Save changes and leave form open.");
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(501, 36);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(93, 37);
            this.cmdOK.TabIndex = 21;
            this.cmdOK.Text = "&OK";
            this.optionsFormToolTips.SetToolTip(this.cmdOK, "Save changes and close form.");
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(501, 551);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(93, 37);
            this.cmdCancel.TabIndex = 25;
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\InitWinFormsAppExt\\InitWinFormsAppWithUser" +
    "AndAppSettings\\InitWinFormsHelpFile.chm";
            // 
            // cmdGetQueryDefinitionsFolder
            // 
            this.cmdGetQueryDefinitionsFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetQueryDefinitionsFolder.Location = new System.Drawing.Point(438, 236);
            this.cmdGetQueryDefinitionsFolder.Name = "cmdGetQueryDefinitionsFolder";
            this.cmdGetQueryDefinitionsFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetQueryDefinitionsFolder.TabIndex = 14;
            this.cmdGetQueryDefinitionsFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdGetQueryDefinitionsFolder, "Prompt to select initial folder path");
            this.cmdGetQueryDefinitionsFolder.UseVisualStyleBackColor = true;
            this.cmdGetQueryDefinitionsFolder.Click += new System.EventHandler(this.cmdGetQueryDefinitionsFolder_Click);
            // 
            // cmdGetDataGridExportFolder
            // 
            this.cmdGetDataGridExportFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDataGridExportFolder.Location = new System.Drawing.Point(438, 275);
            this.cmdGetDataGridExportFolder.Name = "cmdGetDataGridExportFolder";
            this.cmdGetDataGridExportFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetDataGridExportFolder.TabIndex = 17;
            this.cmdGetDataGridExportFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdGetDataGridExportFolder, "Prompt to select folder for saving statistics files");
            this.cmdGetDataGridExportFolder.UseVisualStyleBackColor = true;
            this.cmdGetDataGridExportFolder.Click += new System.EventHandler(this.cmdGetDataGridExportFolder_Click);
            // 
            // lblDefaultQueryDefinitionsFolder
            // 
            this.lblDefaultQueryDefinitionsFolder.AutoSize = true;
            this.lblDefaultQueryDefinitionsFolder.Location = new System.Drawing.Point(37, 236);
            this.lblDefaultQueryDefinitionsFolder.Name = "lblDefaultQueryDefinitionsFolder";
            this.lblDefaultQueryDefinitionsFolder.Size = new System.Drawing.Size(198, 13);
            this.lblDefaultQueryDefinitionsFolder.TabIndex = 13;
            this.lblDefaultQueryDefinitionsFolder.Text = "Default folder for storing query definitions";
            // 
            // txtDefaultQueryDefinitionsFolder
            // 
            this.txtDefaultQueryDefinitionsFolder.Location = new System.Drawing.Point(40, 253);
            this.txtDefaultQueryDefinitionsFolder.Name = "txtDefaultQueryDefinitionsFolder";
            this.txtDefaultQueryDefinitionsFolder.Size = new System.Drawing.Size(436, 20);
            this.txtDefaultQueryDefinitionsFolder.TabIndex = 15;
            // 
            // chkSaveFormLocationsOnExit
            // 
            this.chkSaveFormLocationsOnExit.AutoSize = true;
            this.chkSaveFormLocationsOnExit.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkSaveFormLocationsOnExit.Location = new System.Drawing.Point(40, 76);
            this.chkSaveFormLocationsOnExit.Name = "chkSaveFormLocationsOnExit";
            this.chkSaveFormLocationsOnExit.Size = new System.Drawing.Size(161, 17);
            this.chkSaveFormLocationsOnExit.TabIndex = 3;
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
            this.UserOptionsMenu.Size = new System.Drawing.Size(629, 24);
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
            // cboDefaultInputDatabaseType
            // 
            this.cboDefaultInputDatabaseType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDefaultInputDatabaseType.FormattingEnabled = true;
            this.cboDefaultInputDatabaseType.Location = new System.Drawing.Point(15, 31);
            this.cboDefaultInputDatabaseType.Name = "cboDefaultInputDatabaseType";
            this.cboDefaultInputDatabaseType.Size = new System.Drawing.Size(407, 21);
            this.cboDefaultInputDatabaseType.TabIndex = 1;
            this.cboDefaultInputDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cboDefaultInputDatabaseType_SelectedIndexChanged);
            // 
            // lblDefaultInputDatabaseType
            // 
            this.lblDefaultInputDatabaseType.AutoSize = true;
            this.lblDefaultInputDatabaseType.Location = new System.Drawing.Point(18, 11);
            this.lblDefaultInputDatabaseType.Name = "lblDefaultInputDatabaseType";
            this.lblDefaultInputDatabaseType.Size = new System.Drawing.Size(175, 13);
            this.lblDefaultInputDatabaseType.TabIndex = 0;
            this.lblDefaultInputDatabaseType.Text = "Default database source for queries";
            // 
            // chkShowInstalledDatabaseProvidersOnly
            // 
            this.chkShowInstalledDatabaseProvidersOnly.AutoSize = true;
            this.chkShowInstalledDatabaseProvidersOnly.Location = new System.Drawing.Point(40, 100);
            this.chkShowInstalledDatabaseProvidersOnly.Name = "chkShowInstalledDatabaseProvidersOnly";
            this.chkShowInstalledDatabaseProvidersOnly.Size = new System.Drawing.Size(215, 17);
            this.chkShowInstalledDatabaseProvidersOnly.TabIndex = 4;
            this.chkShowInstalledDatabaseProvidersOnly.Text = "Show Installed Database Providers Only";
            this.chkShowInstalledDatabaseProvidersOnly.UseVisualStyleBackColor = true;
            // 
            // chkShowApplicationLogWindow
            // 
            this.chkShowApplicationLogWindow.AutoSize = true;
            this.chkShowApplicationLogWindow.Location = new System.Drawing.Point(40, 124);
            this.chkShowApplicationLogWindow.Name = "chkShowApplicationLogWindow";
            this.chkShowApplicationLogWindow.Size = new System.Drawing.Size(171, 17);
            this.chkShowApplicationLogWindow.TabIndex = 5;
            this.chkShowApplicationLogWindow.Text = "Show Application Log Window";
            this.chkShowApplicationLogWindow.UseVisualStyleBackColor = true;
            // 
            // lblDefaultInputDatabaseConnectionString
            // 
            this.lblDefaultInputDatabaseConnectionString.AutoSize = true;
            this.lblDefaultInputDatabaseConnectionString.Location = new System.Drawing.Point(15, 59);
            this.lblDefaultInputDatabaseConnectionString.Name = "lblDefaultInputDatabaseConnectionString";
            this.lblDefaultInputDatabaseConnectionString.Size = new System.Drawing.Size(184, 13);
            this.lblDefaultInputDatabaseConnectionString.TabIndex = 2;
            this.lblDefaultInputDatabaseConnectionString.Text = "Default data source connection string";
            // 
            // txtDefaultInputDatabaseConnectionString
            // 
            this.txtDefaultInputDatabaseConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefaultInputDatabaseConnectionString.Location = new System.Drawing.Point(15, 76);
            this.txtDefaultInputDatabaseConnectionString.Multiline = true;
            this.txtDefaultInputDatabaseConnectionString.Name = "txtDefaultInputDatabaseConnectionString";
            this.txtDefaultInputDatabaseConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDefaultInputDatabaseConnectionString.Size = new System.Drawing.Size(410, 40);
            this.txtDefaultInputDatabaseConnectionString.TabIndex = 4;
            // 
            // cmdDefineInputDatabaseConnectionString
            // 
            this.cmdDefineInputDatabaseConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDefineInputDatabaseConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDefineInputDatabaseConnectionString.Location = new System.Drawing.Point(387, 57);
            this.cmdDefineInputDatabaseConnectionString.Name = "cmdDefineInputDatabaseConnectionString";
            this.cmdDefineInputDatabaseConnectionString.Size = new System.Drawing.Size(38, 20);
            this.cmdDefineInputDatabaseConnectionString.TabIndex = 3;
            this.cmdDefineInputDatabaseConnectionString.Text = "•••";
            this.cmdDefineInputDatabaseConnectionString.UseVisualStyleBackColor = true;
            this.cmdDefineInputDatabaseConnectionString.Click += new System.EventHandler(this.cmdDefineInputDatabaseConnectionString_Click);
            // 
            // lblDefaultDataGridExportFolder
            // 
            this.lblDefaultDataGridExportFolder.AutoSize = true;
            this.lblDefaultDataGridExportFolder.Location = new System.Drawing.Point(37, 276);
            this.lblDefaultDataGridExportFolder.Name = "lblDefaultDataGridExportFolder";
            this.lblDefaultDataGridExportFolder.Size = new System.Drawing.Size(182, 13);
            this.lblDefaultDataGridExportFolder.TabIndex = 16;
            this.lblDefaultDataGridExportFolder.Text = "Default folder for data grid export files";
            // 
            // txtDefaultDataGridExportFolder
            // 
            this.txtDefaultDataGridExportFolder.Location = new System.Drawing.Point(40, 292);
            this.txtDefaultDataGridExportFolder.Name = "txtDefaultDataGridExportFolder";
            this.txtDefaultDataGridExportFolder.Size = new System.Drawing.Size(436, 20);
            this.txtDefaultDataGridExportFolder.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDefaultInputDatabaseType);
            this.panel1.Controls.Add(this.cboDefaultInputDatabaseType);
            this.panel1.Controls.Add(this.cmdDefineInputDatabaseConnectionString);
            this.panel1.Controls.Add(this.lblDefaultInputDatabaseConnectionString);
            this.panel1.Controls.Add(this.txtDefaultInputDatabaseConnectionString);
            this.panel1.Location = new System.Drawing.Point(40, 317);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 129);
            this.panel1.TabIndex = 19;
            // 
            // cmdChangeDefaultApplicationFont
            // 
            this.cmdChangeDefaultApplicationFont.Location = new System.Drawing.Point(415, 153);
            this.cmdChangeDefaultApplicationFont.Name = "cmdChangeDefaultApplicationFont";
            this.cmdChangeDefaultApplicationFont.Size = new System.Drawing.Size(61, 23);
            this.cmdChangeDefaultApplicationFont.TabIndex = 8;
            this.cmdChangeDefaultApplicationFont.Text = "Change";
            this.cmdChangeDefaultApplicationFont.UseVisualStyleBackColor = true;
            this.cmdChangeDefaultApplicationFont.Click += new System.EventHandler(this.cmdChangeDefaultApplicationFont_Click);
            // 
            // txtDefaultApplicationFont
            // 
            this.txtDefaultApplicationFont.Location = new System.Drawing.Point(163, 154);
            this.txtDefaultApplicationFont.Name = "txtDefaultApplicationFont";
            this.txtDefaultApplicationFont.ReadOnly = true;
            this.txtDefaultApplicationFont.Size = new System.Drawing.Size(246, 20);
            this.txtDefaultApplicationFont.TabIndex = 7;
            // 
            // lblDefaultApplicationFont
            // 
            this.lblDefaultApplicationFont.AutoSize = true;
            this.lblDefaultApplicationFont.Location = new System.Drawing.Point(37, 158);
            this.lblDefaultApplicationFont.Name = "lblDefaultApplicationFont";
            this.lblDefaultApplicationFont.Size = new System.Drawing.Size(120, 13);
            this.lblDefaultApplicationFont.TabIndex = 6;
            this.lblDefaultApplicationFont.Text = "Application Default Font";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblDefaultOutputDatabaseType);
            this.panel2.Controls.Add(this.cboDefaultOutputDatabaseType);
            this.panel2.Controls.Add(this.cmdDefineOutputDatabaseConnectionString);
            this.panel2.Controls.Add(this.lblDefaultOutputDatabaseConnectionString);
            this.panel2.Controls.Add(this.txtDefaultOutputDatabaseConnectionString);
            this.panel2.Location = new System.Drawing.Point(40, 452);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 136);
            this.panel2.TabIndex = 20;
            // 
            // lblDefaultOutputDatabaseType
            // 
            this.lblDefaultOutputDatabaseType.AutoSize = true;
            this.lblDefaultOutputDatabaseType.Location = new System.Drawing.Point(17, 12);
            this.lblDefaultOutputDatabaseType.Name = "lblDefaultOutputDatabaseType";
            this.lblDefaultOutputDatabaseType.Size = new System.Drawing.Size(197, 13);
            this.lblDefaultOutputDatabaseType.TabIndex = 0;
            this.lblDefaultOutputDatabaseType.Text = "Default database destination for exports ";
            // 
            // cboDefaultOutputDatabaseType
            // 
            this.cboDefaultOutputDatabaseType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDefaultOutputDatabaseType.FormattingEnabled = true;
            this.cboDefaultOutputDatabaseType.Location = new System.Drawing.Point(15, 29);
            this.cboDefaultOutputDatabaseType.Name = "cboDefaultOutputDatabaseType";
            this.cboDefaultOutputDatabaseType.Size = new System.Drawing.Size(409, 21);
            this.cboDefaultOutputDatabaseType.TabIndex = 1;
            this.cboDefaultOutputDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cboDefaultOutputDatabaseType_SelectedIndexChanged);
            // 
            // cmdDefineOutputDatabaseConnectionString
            // 
            this.cmdDefineOutputDatabaseConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDefineOutputDatabaseConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDefineOutputDatabaseConnectionString.Location = new System.Drawing.Point(386, 60);
            this.cmdDefineOutputDatabaseConnectionString.Name = "cmdDefineOutputDatabaseConnectionString";
            this.cmdDefineOutputDatabaseConnectionString.Size = new System.Drawing.Size(38, 20);
            this.cmdDefineOutputDatabaseConnectionString.TabIndex = 3;
            this.cmdDefineOutputDatabaseConnectionString.Text = "•••";
            this.cmdDefineOutputDatabaseConnectionString.UseVisualStyleBackColor = true;
            this.cmdDefineOutputDatabaseConnectionString.Click += new System.EventHandler(this.cmdDefineOutputDatabaseConnectionString_Click);
            // 
            // lblDefaultOutputDatabaseConnectionString
            // 
            this.lblDefaultOutputDatabaseConnectionString.AutoSize = true;
            this.lblDefaultOutputDatabaseConnectionString.Location = new System.Drawing.Point(17, 60);
            this.lblDefaultOutputDatabaseConnectionString.Name = "lblDefaultOutputDatabaseConnectionString";
            this.lblDefaultOutputDatabaseConnectionString.Size = new System.Drawing.Size(203, 13);
            this.lblDefaultOutputDatabaseConnectionString.TabIndex = 2;
            this.lblDefaultOutputDatabaseConnectionString.Text = "Default data destination connection string";
            // 
            // txtDefaultOutputDatabaseConnectionString
            // 
            this.txtDefaultOutputDatabaseConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefaultOutputDatabaseConnectionString.Location = new System.Drawing.Point(15, 79);
            this.txtDefaultOutputDatabaseConnectionString.Multiline = true;
            this.txtDefaultOutputDatabaseConnectionString.Name = "txtDefaultOutputDatabaseConnectionString";
            this.txtDefaultOutputDatabaseConnectionString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDefaultOutputDatabaseConnectionString.Size = new System.Drawing.Size(409, 40);
            this.txtDefaultOutputDatabaseConnectionString.TabIndex = 4;
            // 
            // lblBatchSizeForDataImportsAndExports
            // 
            this.lblBatchSizeForDataImportsAndExports.AutoSize = true;
            this.lblBatchSizeForDataImportsAndExports.Location = new System.Drawing.Point(37, 185);
            this.lblBatchSizeForDataImportsAndExports.Name = "lblBatchSizeForDataImportsAndExports";
            this.lblBatchSizeForDataImportsAndExports.Size = new System.Drawing.Size(201, 13);
            this.lblBatchSizeForDataImportsAndExports.TabIndex = 9;
            this.lblBatchSizeForDataImportsAndExports.Text = "Batch Size for Data Imports And Updates";
            // 
            // lblBatchSizeForRandomDataGeneration
            // 
            this.lblBatchSizeForRandomDataGeneration.AutoSize = true;
            this.lblBatchSizeForRandomDataGeneration.Location = new System.Drawing.Point(37, 209);
            this.lblBatchSizeForRandomDataGeneration.Name = "lblBatchSizeForRandomDataGeneration";
            this.lblBatchSizeForRandomDataGeneration.Size = new System.Drawing.Size(197, 13);
            this.lblBatchSizeForRandomDataGeneration.TabIndex = 11;
            this.lblBatchSizeForRandomDataGeneration.Text = "Batch Size for Random Data Generation";
            // 
            // txtBatchSizeForDataImportsAndExports
            // 
            this.txtBatchSizeForDataImportsAndExports.Location = new System.Drawing.Point(255, 181);
            this.txtBatchSizeForDataImportsAndExports.Name = "txtBatchSizeForDataImportsAndExports";
            this.txtBatchSizeForDataImportsAndExports.Size = new System.Drawing.Size(100, 20);
            this.txtBatchSizeForDataImportsAndExports.TabIndex = 10;
            // 
            // txtBatchSizeForRandomDataGeneration
            // 
            this.txtBatchSizeForRandomDataGeneration.Location = new System.Drawing.Point(255, 205);
            this.txtBatchSizeForRandomDataGeneration.Name = "txtBatchSizeForRandomDataGeneration";
            this.txtBatchSizeForRandomDataGeneration.Size = new System.Drawing.Size(100, 20);
            this.txtBatchSizeForRandomDataGeneration.TabIndex = 12;
            // 
            // UserOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 623);
            this.Controls.Add(this.txtBatchSizeForRandomDataGeneration);
            this.Controls.Add(this.txtBatchSizeForDataImportsAndExports);
            this.Controls.Add(this.lblBatchSizeForRandomDataGeneration);
            this.Controls.Add(this.lblBatchSizeForDataImportsAndExports);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblDefaultApplicationFont);
            this.Controls.Add(this.txtDefaultApplicationFont);
            this.Controls.Add(this.cmdChangeDefaultApplicationFont);
            this.Controls.Add(this.cmdGetDataGridExportFolder);
            this.Controls.Add(this.txtDefaultDataGridExportFolder);
            this.Controls.Add(this.lblDefaultDataGridExportFolder);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkShowApplicationLogWindow);
            this.Controls.Add(this.chkShowInstalledDatabaseProvidersOnly);
            this.Controls.Add(this.lblUpdateResults);
            this.Controls.Add(this.chkSaveFormLocationsOnExit);
            this.Controls.Add(this.cmdGetQueryDefinitionsFolder);
            this.Controls.Add(this.txtDefaultQueryDefinitionsFolder);
            this.Controls.Add(this.lblDefaultQueryDefinitionsFolder);
            this.Controls.Add(this.lblFormMessage);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.UserOptionsMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.UserOptionsMenu;
            this.Name = "UserOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Options for pfDataListGenerator";
            this.Load += new System.EventHandler(this.UserOptionsForm_Load);
            this.UserOptionsMenu.ResumeLayout(false);
            this.UserOptionsMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Label lblDefaultQueryDefinitionsFolder;
        private System.Windows.Forms.TextBox txtDefaultQueryDefinitionsFolder;
        private System.Windows.Forms.Button cmdGetQueryDefinitionsFolder;
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
        private System.Windows.Forms.ComboBox cboDefaultInputDatabaseType;
        private System.Windows.Forms.Label lblDefaultInputDatabaseType;
        private System.Windows.Forms.CheckBox chkShowInstalledDatabaseProvidersOnly;
        private System.Windows.Forms.CheckBox chkShowApplicationLogWindow;
        private System.Windows.Forms.Label lblDefaultInputDatabaseConnectionString;
        private System.Windows.Forms.TextBox txtDefaultInputDatabaseConnectionString;
        private System.Windows.Forms.Button cmdDefineInputDatabaseConnectionString;
        private System.Windows.Forms.Label lblDefaultDataGridExportFolder;
        private System.Windows.Forms.TextBox txtDefaultDataGridExportFolder;
        private System.Windows.Forms.Button cmdGetDataGridExportFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdChangeDefaultApplicationFont;
        private System.Windows.Forms.TextBox txtDefaultApplicationFont;
        private System.Windows.Forms.Label lblDefaultApplicationFont;
        private System.Windows.Forms.FontDialog FontDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDefaultOutputDatabaseType;
        private System.Windows.Forms.ComboBox cboDefaultOutputDatabaseType;
        private System.Windows.Forms.Button cmdDefineOutputDatabaseConnectionString;
        private System.Windows.Forms.Label lblDefaultOutputDatabaseConnectionString;
        private System.Windows.Forms.TextBox txtDefaultOutputDatabaseConnectionString;
        private System.Windows.Forms.Label lblBatchSizeForDataImportsAndExports;
        private System.Windows.Forms.Label lblBatchSizeForRandomDataGeneration;
        private System.Windows.Forms.TextBox txtBatchSizeForDataImportsAndExports;
        private System.Windows.Forms.TextBox txtBatchSizeForRandomDataGeneration;

    }
#pragma warning restore 1591
}
