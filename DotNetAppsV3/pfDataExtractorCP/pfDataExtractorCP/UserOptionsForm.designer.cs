namespace pfDataExtractorCP
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
            this.cmdSeExtractorDefinitionsSaveFolder = new System.Windows.Forms.Button();
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
            this.cboDefaultDataSourceType = new System.Windows.Forms.ComboBox();
            this.lblDefaultDataSourceType = new System.Windows.Forms.Label();
            this.chkShowOutputLog = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelPreviewOptions = new System.Windows.Forms.Panel();
            this.txtMaxNumberOfRows = new System.Windows.Forms.TextBox();
            this.lblMaxNumberOfRows = new System.Windows.Forms.Label();
            this.optLimitPreviewRows = new System.Windows.Forms.RadioButton();
            this.optPreviewAllRows = new System.Windows.Forms.RadioButton();
            this.lblDefaultDataDestinationType = new System.Windows.Forms.Label();
            this.cboDefaultDataDestinationType = new System.Windows.Forms.ComboBox();
            this.chkShowInstalledDatabaseProvidersOnly = new System.Windows.Forms.CheckBox();
            this.txtBatchSizeForRandomDataGeneration = new System.Windows.Forms.TextBox();
            this.txtBatchSizeForDataImportsAndExports = new System.Windows.Forms.TextBox();
            this.lblBatchSizeForRandomDataGeneration = new System.Windows.Forms.Label();
            this.lblBatchSizeForDataImportsAndExports = new System.Windows.Forms.Label();
            this.lblDefaultApplicationFont = new System.Windows.Forms.Label();
            this.txtDefaultApplicationFont = new System.Windows.Forms.TextBox();
            this.cmdChangeDefaultApplicationFont = new System.Windows.Forms.Button();
            this.FontDialog1 = new System.Windows.Forms.FontDialog();
            this.txtDefaultExtractorDefinitionsSaveFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultExtractorDefinitionsSaveFolder = new System.Windows.Forms.Label();
            this.lblDefaultExtractorDefinitionName = new System.Windows.Forms.Label();
            this.txtDefaultExtractorDefinitionName = new System.Windows.Forms.TextBox();
            this.UserOptionsMenu.SuspendLayout();
            this.panelPreviewOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(501, 208);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(93, 37);
            this.cmdReset.TabIndex = 18;
            this.cmdReset.Text = "&Reset";
            this.optionsFormToolTips.SetToolTip(this.cmdReset, "Reset all app settings to last saved values.");
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHelp.Location = new System.Drawing.Point(501, 304);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(93, 37);
            this.cmdHelp.TabIndex = 19;
            this.cmdHelp.Text = "&Help";
            this.optionsFormToolTips.SetToolTip(this.cmdHelp, "Show Help for this form.");
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.Location = new System.Drawing.Point(501, 111);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(93, 37);
            this.cmdApply.TabIndex = 17;
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
            this.cmdOK.TabIndex = 16;
            this.cmdOK.Text = "&OK";
            this.optionsFormToolTips.SetToolTip(this.cmdOK, "Save changes and close form.");
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(501, 401);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(93, 37);
            this.cmdCancel.TabIndex = 20;
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
            // cmdSeExtractorDefinitionsSaveFolder
            // 
            this.cmdSeExtractorDefinitionsSaveFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSeExtractorDefinitionsSaveFolder.Location = new System.Drawing.Point(414, 152);
            this.cmdSeExtractorDefinitionsSaveFolder.Name = "cmdSeExtractorDefinitionsSaveFolder";
            this.cmdSeExtractorDefinitionsSaveFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSeExtractorDefinitionsSaveFolder.TabIndex = 42;
            this.cmdSeExtractorDefinitionsSaveFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSeExtractorDefinitionsSaveFolder, "Prompt to select default extractor definitions save folder.");
            this.cmdSeExtractorDefinitionsSaveFolder.UseVisualStyleBackColor = true;
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
            // cboDefaultDataSourceType
            // 
            this.cboDefaultDataSourceType.FormattingEnabled = true;
            this.cboDefaultDataSourceType.Location = new System.Drawing.Point(38, 265);
            this.cboDefaultDataSourceType.Name = "cboDefaultDataSourceType";
            this.cboDefaultDataSourceType.Size = new System.Drawing.Size(195, 21);
            this.cboDefaultDataSourceType.TabIndex = 15;
            // 
            // lblDefaultDataSourceType
            // 
            this.lblDefaultDataSourceType.AutoSize = true;
            this.lblDefaultDataSourceType.Location = new System.Drawing.Point(35, 246);
            this.lblDefaultDataSourceType.Name = "lblDefaultDataSourceType";
            this.lblDefaultDataSourceType.Size = new System.Drawing.Size(131, 13);
            this.lblDefaultDataSourceType.TabIndex = 14;
            this.lblDefaultDataSourceType.Text = "Default Data Source Type";
            // 
            // chkShowOutputLog
            // 
            this.chkShowOutputLog.AutoSize = true;
            this.chkShowOutputLog.Location = new System.Drawing.Point(40, 99);
            this.chkShowOutputLog.Name = "chkShowOutputLog";
            this.chkShowOutputLog.Size = new System.Drawing.Size(109, 17);
            this.chkShowOutputLog.TabIndex = 4;
            this.chkShowOutputLog.Text = "Show Output Log";
            this.chkShowOutputLog.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Preview Options";
            // 
            // panelPreviewOptions
            // 
            this.panelPreviewOptions.Controls.Add(this.txtMaxNumberOfRows);
            this.panelPreviewOptions.Controls.Add(this.lblMaxNumberOfRows);
            this.panelPreviewOptions.Controls.Add(this.optLimitPreviewRows);
            this.panelPreviewOptions.Controls.Add(this.optPreviewAllRows);
            this.panelPreviewOptions.Location = new System.Drawing.Point(127, 356);
            this.panelPreviewOptions.Name = "panelPreviewOptions";
            this.panelPreviewOptions.Size = new System.Drawing.Size(322, 52);
            this.panelPreviewOptions.TabIndex = 7;
            // 
            // txtMaxNumberOfRows
            // 
            this.txtMaxNumberOfRows.Location = new System.Drawing.Point(138, 23);
            this.txtMaxNumberOfRows.Name = "txtMaxNumberOfRows";
            this.txtMaxNumberOfRows.Size = new System.Drawing.Size(101, 20);
            this.txtMaxNumberOfRows.TabIndex = 3;
            // 
            // lblMaxNumberOfRows
            // 
            this.lblMaxNumberOfRows.AutoSize = true;
            this.lblMaxNumberOfRows.Location = new System.Drawing.Point(135, 3);
            this.lblMaxNumberOfRows.Name = "lblMaxNumberOfRows";
            this.lblMaxNumberOfRows.Size = new System.Drawing.Size(109, 13);
            this.lblMaxNumberOfRows.TabIndex = 2;
            this.lblMaxNumberOfRows.Text = "Max Number of Rows";
            // 
            // optLimitPreviewRows
            // 
            this.optLimitPreviewRows.AutoSize = true;
            this.optLimitPreviewRows.Location = new System.Drawing.Point(3, 23);
            this.optLimitPreviewRows.Name = "optLimitPreviewRows";
            this.optLimitPreviewRows.Size = new System.Drawing.Size(129, 17);
            this.optLimitPreviewRows.TabIndex = 1;
            this.optLimitPreviewRows.TabStop = true;
            this.optLimitPreviewRows.Text = "Limit Preview Rows to";
            this.optLimitPreviewRows.UseVisualStyleBackColor = true;
            // 
            // optPreviewAllRows
            // 
            this.optPreviewAllRows.AutoSize = true;
            this.optPreviewAllRows.Location = new System.Drawing.Point(3, 0);
            this.optPreviewAllRows.Name = "optPreviewAllRows";
            this.optPreviewAllRows.Size = new System.Drawing.Size(107, 17);
            this.optPreviewAllRows.TabIndex = 0;
            this.optPreviewAllRows.TabStop = true;
            this.optPreviewAllRows.Text = "Preview All Rows";
            this.optPreviewAllRows.UseVisualStyleBackColor = true;
            this.optPreviewAllRows.CheckedChanged += new System.EventHandler(this.optPreviewAllRows_CheckedChanged);
            // 
            // lblDefaultDataDestinationType
            // 
            this.lblDefaultDataDestinationType.AutoSize = true;
            this.lblDefaultDataDestinationType.Location = new System.Drawing.Point(250, 246);
            this.lblDefaultDataDestinationType.Name = "lblDefaultDataDestinationType";
            this.lblDefaultDataDestinationType.Size = new System.Drawing.Size(150, 13);
            this.lblDefaultDataDestinationType.TabIndex = 21;
            this.lblDefaultDataDestinationType.Text = "Default Data Destination Type";
            // 
            // cboDefaultDataDestinationType
            // 
            this.cboDefaultDataDestinationType.FormattingEnabled = true;
            this.cboDefaultDataDestinationType.Location = new System.Drawing.Point(253, 265);
            this.cboDefaultDataDestinationType.Name = "cboDefaultDataDestinationType";
            this.cboDefaultDataDestinationType.Size = new System.Drawing.Size(195, 21);
            this.cboDefaultDataDestinationType.TabIndex = 22;
            // 
            // chkShowInstalledDatabaseProvidersOnly
            // 
            this.chkShowInstalledDatabaseProvidersOnly.AutoSize = true;
            this.chkShowInstalledDatabaseProvidersOnly.Location = new System.Drawing.Point(40, 122);
            this.chkShowInstalledDatabaseProvidersOnly.Name = "chkShowInstalledDatabaseProvidersOnly";
            this.chkShowInstalledDatabaseProvidersOnly.Size = new System.Drawing.Size(215, 17);
            this.chkShowInstalledDatabaseProvidersOnly.TabIndex = 23;
            this.chkShowInstalledDatabaseProvidersOnly.Text = "Show Installed Database Providers Only";
            this.chkShowInstalledDatabaseProvidersOnly.UseVisualStyleBackColor = true;
            // 
            // txtBatchSizeForRandomDataGeneration
            // 
            this.txtBatchSizeForRandomDataGeneration.Location = new System.Drawing.Point(255, 447);
            this.txtBatchSizeForRandomDataGeneration.Name = "txtBatchSizeForRandomDataGeneration";
            this.txtBatchSizeForRandomDataGeneration.Size = new System.Drawing.Size(100, 20);
            this.txtBatchSizeForRandomDataGeneration.TabIndex = 30;
            // 
            // txtBatchSizeForDataImportsAndExports
            // 
            this.txtBatchSizeForDataImportsAndExports.Location = new System.Drawing.Point(255, 421);
            this.txtBatchSizeForDataImportsAndExports.Name = "txtBatchSizeForDataImportsAndExports";
            this.txtBatchSizeForDataImportsAndExports.Size = new System.Drawing.Size(100, 20);
            this.txtBatchSizeForDataImportsAndExports.TabIndex = 28;
            // 
            // lblBatchSizeForRandomDataGeneration
            // 
            this.lblBatchSizeForRandomDataGeneration.AutoSize = true;
            this.lblBatchSizeForRandomDataGeneration.Location = new System.Drawing.Point(37, 451);
            this.lblBatchSizeForRandomDataGeneration.Name = "lblBatchSizeForRandomDataGeneration";
            this.lblBatchSizeForRandomDataGeneration.Size = new System.Drawing.Size(197, 13);
            this.lblBatchSizeForRandomDataGeneration.TabIndex = 29;
            this.lblBatchSizeForRandomDataGeneration.Text = "Batch Size for Random Data Generation";
            // 
            // lblBatchSizeForDataImportsAndExports
            // 
            this.lblBatchSizeForDataImportsAndExports.AutoSize = true;
            this.lblBatchSizeForDataImportsAndExports.Location = new System.Drawing.Point(37, 425);
            this.lblBatchSizeForDataImportsAndExports.Name = "lblBatchSizeForDataImportsAndExports";
            this.lblBatchSizeForDataImportsAndExports.Size = new System.Drawing.Size(196, 13);
            this.lblBatchSizeForDataImportsAndExports.TabIndex = 27;
            this.lblBatchSizeForDataImportsAndExports.Text = "Batch Size for Data Imports And Exports";
            // 
            // lblDefaultApplicationFont
            // 
            this.lblDefaultApplicationFont.AutoSize = true;
            this.lblDefaultApplicationFont.Location = new System.Drawing.Point(35, 309);
            this.lblDefaultApplicationFont.Name = "lblDefaultApplicationFont";
            this.lblDefaultApplicationFont.Size = new System.Drawing.Size(120, 13);
            this.lblDefaultApplicationFont.TabIndex = 24;
            this.lblDefaultApplicationFont.Text = "Application Default Font";
            // 
            // txtDefaultApplicationFont
            // 
            this.txtDefaultApplicationFont.Location = new System.Drawing.Point(161, 305);
            this.txtDefaultApplicationFont.Name = "txtDefaultApplicationFont";
            this.txtDefaultApplicationFont.ReadOnly = true;
            this.txtDefaultApplicationFont.Size = new System.Drawing.Size(225, 20);
            this.txtDefaultApplicationFont.TabIndex = 25;
            // 
            // cmdChangeDefaultApplicationFont
            // 
            this.cmdChangeDefaultApplicationFont.Location = new System.Drawing.Point(387, 304);
            this.cmdChangeDefaultApplicationFont.Name = "cmdChangeDefaultApplicationFont";
            this.cmdChangeDefaultApplicationFont.Size = new System.Drawing.Size(61, 23);
            this.cmdChangeDefaultApplicationFont.TabIndex = 26;
            this.cmdChangeDefaultApplicationFont.Text = "Change";
            this.cmdChangeDefaultApplicationFont.UseVisualStyleBackColor = true;
            this.cmdChangeDefaultApplicationFont.Click += new System.EventHandler(this.cmdChangeDefaultApplicationFont_Click);
            // 
            // txtDefaultExtractorDefinitionsSaveFolder
            // 
            this.txtDefaultExtractorDefinitionsSaveFolder.Location = new System.Drawing.Point(38, 169);
            this.txtDefaultExtractorDefinitionsSaveFolder.Name = "txtDefaultExtractorDefinitionsSaveFolder";
            this.txtDefaultExtractorDefinitionsSaveFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultExtractorDefinitionsSaveFolder.TabIndex = 41;
            // 
            // lblDefaultExtractorDefinitionsSaveFolder
            // 
            this.lblDefaultExtractorDefinitionsSaveFolder.AutoSize = true;
            this.lblDefaultExtractorDefinitionsSaveFolder.Location = new System.Drawing.Point(35, 152);
            this.lblDefaultExtractorDefinitionsSaveFolder.Name = "lblDefaultExtractorDefinitionsSaveFolder";
            this.lblDefaultExtractorDefinitionsSaveFolder.Size = new System.Drawing.Size(198, 13);
            this.lblDefaultExtractorDefinitionsSaveFolder.TabIndex = 40;
            this.lblDefaultExtractorDefinitionsSaveFolder.Text = "Default Extractor Definitions Save Folder";
            // 
            // lblDefaultExtractorDefinitionName
            // 
            this.lblDefaultExtractorDefinitionName.AutoSize = true;
            this.lblDefaultExtractorDefinitionName.Location = new System.Drawing.Point(35, 197);
            this.lblDefaultExtractorDefinitionName.Name = "lblDefaultExtractorDefinitionName";
            this.lblDefaultExtractorDefinitionName.Size = new System.Drawing.Size(164, 13);
            this.lblDefaultExtractorDefinitionName.TabIndex = 43;
            this.lblDefaultExtractorDefinitionName.Text = "Default Extractor Definition Name";
            // 
            // txtDefaultExtractorDefinitionName
            // 
            this.txtDefaultExtractorDefinitionName.Location = new System.Drawing.Point(38, 213);
            this.txtDefaultExtractorDefinitionName.Name = "txtDefaultExtractorDefinitionName";
            this.txtDefaultExtractorDefinitionName.Size = new System.Drawing.Size(412, 20);
            this.txtDefaultExtractorDefinitionName.TabIndex = 44;
            // 
            // UserOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 493);
            this.Controls.Add(this.txtDefaultExtractorDefinitionName);
            this.Controls.Add(this.lblDefaultExtractorDefinitionName);
            this.Controls.Add(this.cmdSeExtractorDefinitionsSaveFolder);
            this.Controls.Add(this.txtDefaultExtractorDefinitionsSaveFolder);
            this.Controls.Add(this.lblDefaultExtractorDefinitionsSaveFolder);
            this.Controls.Add(this.txtBatchSizeForRandomDataGeneration);
            this.Controls.Add(this.txtBatchSizeForDataImportsAndExports);
            this.Controls.Add(this.lblBatchSizeForRandomDataGeneration);
            this.Controls.Add(this.lblBatchSizeForDataImportsAndExports);
            this.Controls.Add(this.lblDefaultApplicationFont);
            this.Controls.Add(this.txtDefaultApplicationFont);
            this.Controls.Add(this.cmdChangeDefaultApplicationFont);
            this.Controls.Add(this.chkShowInstalledDatabaseProvidersOnly);
            this.Controls.Add(this.cboDefaultDataDestinationType);
            this.Controls.Add(this.lblDefaultDataDestinationType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelPreviewOptions);
            this.Controls.Add(this.chkShowOutputLog);
            this.Controls.Add(this.lblDefaultDataSourceType);
            this.Controls.Add(this.cboDefaultDataSourceType);
            this.Controls.Add(this.lblUpdateResults);
            this.Controls.Add(this.chkSaveFormLocationsOnExit);
            this.Controls.Add(this.lblFormMessage);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.UserOptionsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.UserOptionsMenu;
            this.Name = "UserOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Options for InitWinFormsAppExt";
            this.Load += new System.EventHandler(this.UserOptionsForm_Load);
            this.UserOptionsMenu.ResumeLayout(false);
            this.UserOptionsMenu.PerformLayout();
            this.panelPreviewOptions.ResumeLayout(false);
            this.panelPreviewOptions.PerformLayout();
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
        private System.Windows.Forms.ComboBox cboDefaultDataSourceType;
        private System.Windows.Forms.Label lblDefaultDataSourceType;
        private System.Windows.Forms.CheckBox chkShowOutputLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelPreviewOptions;
        private System.Windows.Forms.TextBox txtMaxNumberOfRows;
        private System.Windows.Forms.Label lblMaxNumberOfRows;
        private System.Windows.Forms.RadioButton optLimitPreviewRows;
        private System.Windows.Forms.RadioButton optPreviewAllRows;
        private System.Windows.Forms.Label lblDefaultDataDestinationType;
        private System.Windows.Forms.ComboBox cboDefaultDataDestinationType;
        private System.Windows.Forms.CheckBox chkShowInstalledDatabaseProvidersOnly;
        private System.Windows.Forms.TextBox txtBatchSizeForRandomDataGeneration;
        private System.Windows.Forms.TextBox txtBatchSizeForDataImportsAndExports;
        private System.Windows.Forms.Label lblBatchSizeForRandomDataGeneration;
        private System.Windows.Forms.Label lblBatchSizeForDataImportsAndExports;
        private System.Windows.Forms.Label lblDefaultApplicationFont;
        private System.Windows.Forms.TextBox txtDefaultApplicationFont;
        private System.Windows.Forms.Button cmdChangeDefaultApplicationFont;
        private System.Windows.Forms.FontDialog FontDialog1;
        private System.Windows.Forms.Button cmdSeExtractorDefinitionsSaveFolder;
        private System.Windows.Forms.TextBox txtDefaultExtractorDefinitionsSaveFolder;
        private System.Windows.Forms.Label lblDefaultExtractorDefinitionsSaveFolder;
        private System.Windows.Forms.Label lblDefaultExtractorDefinitionName;
        private System.Windows.Forms.TextBox txtDefaultExtractorDefinitionName;

    }
#pragma warning restore 1591
}
