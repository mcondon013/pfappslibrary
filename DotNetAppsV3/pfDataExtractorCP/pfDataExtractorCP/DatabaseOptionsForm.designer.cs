namespace pfDataExtractorCP
{
#pragma warning disable 1591
    partial class DatabaseOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseOptionsForm));
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
            this.lblDefaultDataGridExportFolder = new System.Windows.Forms.Label();
            this.txtDefaultDataGridExportFolder = new System.Windows.Forms.TextBox();
            this.chkShowInstalledDatabaseProvidersOnly = new System.Windows.Forms.CheckBox();
            this.lblUpdateResults = new System.Windows.Forms.Label();
            this.DatabaseOptionsMenu = new System.Windows.Forms.MenuStrip();
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
            this.txtBatchSizeForRandomDataGeneration = new System.Windows.Forms.TextBox();
            this.txtBatchSizeForDataImportsAndExports = new System.Windows.Forms.TextBox();
            this.lblBatchSizeForRandomDataGeneration = new System.Windows.Forms.Label();
            this.lblBatchSizeForDataImportsAndExports = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDefaultInputDatabaseType = new System.Windows.Forms.Label();
            this.cboDefaultInputDatabaseType = new System.Windows.Forms.ComboBox();
            this.cmdDefineInputDatabaseConnectionString = new System.Windows.Forms.Button();
            this.lblDefaultInputDatabaseConnectionString = new System.Windows.Forms.Label();
            this.txtDefaultInputDatabaseConnectionString = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkOverwriteOutputDestinationIfAlreadyExists = new System.Windows.Forms.CheckBox();
            this.lblDefaultOutputDatabaseType = new System.Windows.Forms.Label();
            this.cboDefaultOutputDatabaseType = new System.Windows.Forms.ComboBox();
            this.cmdDefineOutputDatabaseConnectionString = new System.Windows.Forms.Button();
            this.lblDefaultOutputDatabaseConnectionString = new System.Windows.Forms.Label();
            this.txtDefaultOutputDatabaseConnectionString = new System.Windows.Forms.TextBox();
            this.DatabaseOptionsMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdReset.Location = new System.Drawing.Point(501, 283);
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
            this.cmdHelp.Location = new System.Drawing.Point(501, 437);
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
            this.cmdCancel.Location = new System.Drawing.Point(501, 539);
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\InitWinFormsAppExt\\InitWinFormsAppWithUser" +
    "AndAppSettings\\pfDataExtractorCP.chm";
            // 
            // cmdGetQueryDefinitionsFolder
            // 
            this.cmdGetQueryDefinitionsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGetQueryDefinitionsFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetQueryDefinitionsFolder.Location = new System.Drawing.Point(410, 173);
            this.cmdGetQueryDefinitionsFolder.Name = "cmdGetQueryDefinitionsFolder";
            this.cmdGetQueryDefinitionsFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetQueryDefinitionsFolder.TabIndex = 5;
            this.cmdGetQueryDefinitionsFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdGetQueryDefinitionsFolder, "Prompt to select initial folder path");
            this.cmdGetQueryDefinitionsFolder.UseVisualStyleBackColor = true;
            this.cmdGetQueryDefinitionsFolder.Click += new System.EventHandler(this.cmdGetQueryDefinitionsFolder_Click);
            // 
            // cmdGetDataGridExportFolder
            // 
            this.cmdGetDataGridExportFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGetDataGridExportFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDataGridExportFolder.Location = new System.Drawing.Point(410, 223);
            this.cmdGetDataGridExportFolder.Name = "cmdGetDataGridExportFolder";
            this.cmdGetDataGridExportFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetDataGridExportFolder.TabIndex = 8;
            this.cmdGetDataGridExportFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdGetDataGridExportFolder, "Prompt to select folder for saving statistics files");
            this.cmdGetDataGridExportFolder.UseVisualStyleBackColor = true;
            this.cmdGetDataGridExportFolder.Click += new System.EventHandler(this.cmdGetDataGridExportFolder_Click);
            // 
            // lblDefaultQueryDefinitionsFolder
            // 
            this.lblDefaultQueryDefinitionsFolder.AutoSize = true;
            this.lblDefaultQueryDefinitionsFolder.Location = new System.Drawing.Point(35, 173);
            this.lblDefaultQueryDefinitionsFolder.Name = "lblDefaultQueryDefinitionsFolder";
            this.lblDefaultQueryDefinitionsFolder.Size = new System.Drawing.Size(198, 13);
            this.lblDefaultQueryDefinitionsFolder.TabIndex = 3;
            this.lblDefaultQueryDefinitionsFolder.Text = "Default folder for storing query definitions";
            // 
            // txtDefaultQueryDefinitionsFolder
            // 
            this.txtDefaultQueryDefinitionsFolder.Location = new System.Drawing.Point(38, 190);
            this.txtDefaultQueryDefinitionsFolder.Name = "txtDefaultQueryDefinitionsFolder";
            this.txtDefaultQueryDefinitionsFolder.Size = new System.Drawing.Size(410, 20);
            this.txtDefaultQueryDefinitionsFolder.TabIndex = 4;
            // 
            // lblDefaultDataGridExportFolder
            // 
            this.lblDefaultDataGridExportFolder.AutoSize = true;
            this.lblDefaultDataGridExportFolder.Location = new System.Drawing.Point(35, 223);
            this.lblDefaultDataGridExportFolder.Name = "lblDefaultDataGridExportFolder";
            this.lblDefaultDataGridExportFolder.Size = new System.Drawing.Size(182, 13);
            this.lblDefaultDataGridExportFolder.TabIndex = 6;
            this.lblDefaultDataGridExportFolder.Text = "Default folder for data grid export files";
            // 
            // txtDefaultDataGridExportFolder
            // 
            this.txtDefaultDataGridExportFolder.Location = new System.Drawing.Point(38, 240);
            this.txtDefaultDataGridExportFolder.Name = "txtDefaultDataGridExportFolder";
            this.txtDefaultDataGridExportFolder.Size = new System.Drawing.Size(410, 20);
            this.txtDefaultDataGridExportFolder.TabIndex = 7;
            // 
            // chkShowInstalledDatabaseProvidersOnly
            // 
            this.chkShowInstalledDatabaseProvidersOnly.AutoSize = true;
            this.chkShowInstalledDatabaseProvidersOnly.CheckAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkShowInstalledDatabaseProvidersOnly.Location = new System.Drawing.Point(40, 76);
            this.chkShowInstalledDatabaseProvidersOnly.Name = "chkShowInstalledDatabaseProvidersOnly";
            this.chkShowInstalledDatabaseProvidersOnly.Size = new System.Drawing.Size(215, 17);
            this.chkShowInstalledDatabaseProvidersOnly.TabIndex = 9;
            this.chkShowInstalledDatabaseProvidersOnly.Text = "Show Installed Database Providers Only";
            this.chkShowInstalledDatabaseProvidersOnly.UseVisualStyleBackColor = true;
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
            // DatabaseOptionsMenu
            // 
            this.DatabaseOptionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings});
            this.DatabaseOptionsMenu.Location = new System.Drawing.Point(0, 0);
            this.DatabaseOptionsMenu.Name = "DatabaseOptionsMenu";
            this.DatabaseOptionsMenu.Size = new System.Drawing.Size(635, 24);
            this.DatabaseOptionsMenu.TabIndex = 0;
            this.DatabaseOptionsMenu.Text = "menuStrip1";
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
            // txtBatchSizeForRandomDataGeneration
            // 
            this.txtBatchSizeForRandomDataGeneration.Location = new System.Drawing.Point(255, 133);
            this.txtBatchSizeForRandomDataGeneration.Name = "txtBatchSizeForRandomDataGeneration";
            this.txtBatchSizeForRandomDataGeneration.Size = new System.Drawing.Size(100, 20);
            this.txtBatchSizeForRandomDataGeneration.TabIndex = 34;
            // 
            // txtBatchSizeForDataImportsAndExports
            // 
            this.txtBatchSizeForDataImportsAndExports.Location = new System.Drawing.Point(255, 107);
            this.txtBatchSizeForDataImportsAndExports.Name = "txtBatchSizeForDataImportsAndExports";
            this.txtBatchSizeForDataImportsAndExports.Size = new System.Drawing.Size(100, 20);
            this.txtBatchSizeForDataImportsAndExports.TabIndex = 32;
            // 
            // lblBatchSizeForRandomDataGeneration
            // 
            this.lblBatchSizeForRandomDataGeneration.AutoSize = true;
            this.lblBatchSizeForRandomDataGeneration.Location = new System.Drawing.Point(37, 137);
            this.lblBatchSizeForRandomDataGeneration.Name = "lblBatchSizeForRandomDataGeneration";
            this.lblBatchSizeForRandomDataGeneration.Size = new System.Drawing.Size(197, 13);
            this.lblBatchSizeForRandomDataGeneration.TabIndex = 33;
            this.lblBatchSizeForRandomDataGeneration.Text = "Batch Size for Random Data Generation";
            // 
            // lblBatchSizeForDataImportsAndExports
            // 
            this.lblBatchSizeForDataImportsAndExports.AutoSize = true;
            this.lblBatchSizeForDataImportsAndExports.Location = new System.Drawing.Point(37, 111);
            this.lblBatchSizeForDataImportsAndExports.Name = "lblBatchSizeForDataImportsAndExports";
            this.lblBatchSizeForDataImportsAndExports.Size = new System.Drawing.Size(196, 13);
            this.lblBatchSizeForDataImportsAndExports.TabIndex = 31;
            this.lblBatchSizeForDataImportsAndExports.Text = "Batch Size for Data Imports And Exports";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblDefaultInputDatabaseType);
            this.panel1.Controls.Add(this.cboDefaultInputDatabaseType);
            this.panel1.Controls.Add(this.cmdDefineInputDatabaseConnectionString);
            this.panel1.Controls.Add(this.lblDefaultInputDatabaseConnectionString);
            this.panel1.Controls.Add(this.txtDefaultInputDatabaseConnectionString);
            this.panel1.Location = new System.Drawing.Point(26, 269);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 129);
            this.panel1.TabIndex = 35;
            // 
            // lblDefaultInputDatabaseType
            // 
            this.lblDefaultInputDatabaseType.AutoSize = true;
            this.lblDefaultInputDatabaseType.Location = new System.Drawing.Point(18, 11);
            this.lblDefaultInputDatabaseType.Name = "lblDefaultInputDatabaseType";
            this.lblDefaultInputDatabaseType.Size = new System.Drawing.Size(123, 13);
            this.lblDefaultInputDatabaseType.TabIndex = 0;
            this.lblDefaultInputDatabaseType.Text = "Default database source";
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
            this.cboDefaultInputDatabaseType.Text = "          ";
            this.cboDefaultInputDatabaseType.SelectedIndexChanged += new System.EventHandler(this.cboDefaultInputDatabaseType_SelectedIndexChanged);
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
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chkOverwriteOutputDestinationIfAlreadyExists);
            this.panel2.Controls.Add(this.lblDefaultOutputDatabaseType);
            this.panel2.Controls.Add(this.cboDefaultOutputDatabaseType);
            this.panel2.Controls.Add(this.cmdDefineOutputDatabaseConnectionString);
            this.panel2.Controls.Add(this.lblDefaultOutputDatabaseConnectionString);
            this.panel2.Controls.Add(this.txtDefaultOutputDatabaseConnectionString);
            this.panel2.Location = new System.Drawing.Point(26, 404);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(436, 172);
            this.panel2.TabIndex = 36;
            // 
            // chkOverwriteOutputDestinationIfAlreadyExists
            // 
            this.chkOverwriteOutputDestinationIfAlreadyExists.AutoSize = true;
            this.chkOverwriteOutputDestinationIfAlreadyExists.Location = new System.Drawing.Point(21, 138);
            this.chkOverwriteOutputDestinationIfAlreadyExists.Name = "chkOverwriteOutputDestinationIfAlreadyExists";
            this.chkOverwriteOutputDestinationIfAlreadyExists.Size = new System.Drawing.Size(272, 17);
            this.chkOverwriteOutputDestinationIfAlreadyExists.TabIndex = 37;
            this.chkOverwriteOutputDestinationIfAlreadyExists.Text = "Overwrite Destination Table or File if it Already Exists";
            this.chkOverwriteOutputDestinationIfAlreadyExists.UseVisualStyleBackColor = true;
            // 
            // lblDefaultOutputDatabaseType
            // 
            this.lblDefaultOutputDatabaseType.AutoSize = true;
            this.lblDefaultOutputDatabaseType.Location = new System.Drawing.Point(17, 12);
            this.lblDefaultOutputDatabaseType.Name = "lblDefaultOutputDatabaseType";
            this.lblDefaultOutputDatabaseType.Size = new System.Drawing.Size(142, 13);
            this.lblDefaultOutputDatabaseType.TabIndex = 0;
            this.lblDefaultOutputDatabaseType.Text = "Default database destination";
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
            // DatabaseOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 598);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtBatchSizeForRandomDataGeneration);
            this.Controls.Add(this.txtBatchSizeForDataImportsAndExports);
            this.Controls.Add(this.lblBatchSizeForRandomDataGeneration);
            this.Controls.Add(this.lblBatchSizeForDataImportsAndExports);
            this.Controls.Add(this.lblUpdateResults);
            this.Controls.Add(this.chkShowInstalledDatabaseProvidersOnly);
            this.Controls.Add(this.cmdGetDataGridExportFolder);
            this.Controls.Add(this.txtDefaultDataGridExportFolder);
            this.Controls.Add(this.lblDefaultDataGridExportFolder);
            this.Controls.Add(this.cmdGetQueryDefinitionsFolder);
            this.Controls.Add(this.txtDefaultQueryDefinitionsFolder);
            this.Controls.Add(this.lblDefaultQueryDefinitionsFolder);
            this.Controls.Add(this.lblFormMessage);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.DatabaseOptionsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.DatabaseOptionsMenu;
            this.Name = "DatabaseOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Options for InitWinFormsAppExt";
            this.Load += new System.EventHandler(this.DatabaseOptionsForm_Load);
            this.DatabaseOptionsMenu.ResumeLayout(false);
            this.DatabaseOptionsMenu.PerformLayout();
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
        private System.Windows.Forms.Label lblDefaultDataGridExportFolder;
        private System.Windows.Forms.TextBox txtDefaultDataGridExportFolder;
        private System.Windows.Forms.CheckBox chkShowInstalledDatabaseProvidersOnly;
        private System.Windows.Forms.Label lblUpdateResults;
        private System.Windows.Forms.MenuStrip DatabaseOptionsMenu;
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
        private System.Windows.Forms.Button cmdGetDataGridExportFolder;
        private System.Windows.Forms.TextBox txtBatchSizeForRandomDataGeneration;
        private System.Windows.Forms.TextBox txtBatchSizeForDataImportsAndExports;
        private System.Windows.Forms.Label lblBatchSizeForRandomDataGeneration;
        private System.Windows.Forms.Label lblBatchSizeForDataImportsAndExports;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDefaultInputDatabaseType;
        private System.Windows.Forms.ComboBox cboDefaultInputDatabaseType;
        private System.Windows.Forms.Button cmdDefineInputDatabaseConnectionString;
        private System.Windows.Forms.Label lblDefaultInputDatabaseConnectionString;
        private System.Windows.Forms.TextBox txtDefaultInputDatabaseConnectionString;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDefaultOutputDatabaseType;
        private System.Windows.Forms.ComboBox cboDefaultOutputDatabaseType;
        private System.Windows.Forms.Button cmdDefineOutputDatabaseConnectionString;
        private System.Windows.Forms.Label lblDefaultOutputDatabaseConnectionString;
        private System.Windows.Forms.TextBox txtDefaultOutputDatabaseConnectionString;
        private System.Windows.Forms.CheckBox chkOverwriteOutputDestinationIfAlreadyExists;

    }
#pragma warning restore 1591
}
