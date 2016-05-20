namespace pfDataExtractorCP
{
#pragma warning disable 1591
    partial class FolderOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderOptionsForm));
            this.cmdReset = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblFormMessage = new System.Windows.Forms.Label();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.optionsFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdSetDefaultSourceAccessDatabaseFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultSourceExcelDataFileFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultSourceDelimitedTextFileFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultSourceFixedLengthTextFileFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultDestinationDelimitedTextFileFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaulDestinationExcelDataFileFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultDestinationAccessDatabaseFolder = new System.Windows.Forms.Button();
            this.cmdSeExtractorDefinitionsSaveFolder = new System.Windows.Forms.Button();
            this.cmdGetDataGridExportFolder = new System.Windows.Forms.Button();
            this.cmdGetQueryDefinitionsFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultSourceXmlFileFolder = new System.Windows.Forms.Button();
            this.cmdSetDefaultDestinationXmlFileFolder = new System.Windows.Forms.Button();
            this.lblDefaultSourceAccessDatabaseFolder = new System.Windows.Forms.Label();
            this.txtDefaultSourceAccessDatabaseFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultSourceExcelDataFileFolder = new System.Windows.Forms.Label();
            this.txtDefaultSourceExcelDataFileFolder = new System.Windows.Forms.TextBox();
            this.lblUpdateResults = new System.Windows.Forms.Label();
            this.FolderOptionsMenu = new System.Windows.Forms.MenuStrip();
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
            this.lblDefaultSourceDelimitedTextFileFolder = new System.Windows.Forms.Label();
            this.txtDefaultSourceDelimitedTextFileFolder = new System.Windows.Forms.TextBox();
            this.txtDefaultSourceFixedLengthTextFileFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultSourceFixedLengthTextFileFolder = new System.Windows.Forms.Label();
            this.txtDefaultDestinationFixedLengthTextFileFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultDestinationFixedLengthTextFileFolder = new System.Windows.Forms.Label();
            this.txtDefaultDestinationDelimitedTextFileFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultDestinationDelimitedTextFileFolder = new System.Windows.Forms.Label();
            this.txtDefaultDestinationExcelDataFileFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultDestinationExcelDataFileFolder = new System.Windows.Forms.Label();
            this.txtDefaultDestinationAccessDatabaseFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultDestinationAccessDatabaseFolder = new System.Windows.Forms.Label();
            this.txtDefaultExtractorDefinitionsSaveFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultExtractorDefinitionsSaveFolder = new System.Windows.Forms.Label();
            this.txtDefaultDataGridExportFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultDataGridExportFolder = new System.Windows.Forms.Label();
            this.txtDefaultQueryDefinitionsFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultQueryDefinitionsFolder = new System.Windows.Forms.Label();
            this.txtDefaultSourceXmlFileFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultSourceXmlFileFolder = new System.Windows.Forms.Label();
            this.txtDefaultDestinationXmlFileFolder = new System.Windows.Forms.TextBox();
            this.lblDefaultDestinationXmlFileFolder = new System.Windows.Forms.Label();
            this.FolderOptionsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(504, 247);
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
            this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHelp.Location = new System.Drawing.Point(504, 366);
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
            this.cmdApply.Location = new System.Drawing.Point(504, 111);
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
            this.cmdOK.Location = new System.Drawing.Point(504, 36);
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
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(504, 588);
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
            this.lblFormMessage.Location = new System.Drawing.Point(37, 33);
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
            // cmdSetDefaultSourceAccessDatabaseFolder
            // 
            this.cmdSetDefaultSourceAccessDatabaseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultSourceAccessDatabaseFolder.Location = new System.Drawing.Point(415, 208);
            this.cmdSetDefaultSourceAccessDatabaseFolder.Name = "cmdSetDefaultSourceAccessDatabaseFolder";
            this.cmdSetDefaultSourceAccessDatabaseFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultSourceAccessDatabaseFolder.TabIndex = 5;
            this.cmdSetDefaultSourceAccessDatabaseFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultSourceAccessDatabaseFolder, "Prompt to select initial folder path");
            this.cmdSetDefaultSourceAccessDatabaseFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultSourceAccessDatabaseFolder.Click += new System.EventHandler(this.cmdSetDefaultSourceAccessDatabaseFolder_Click);
            // 
            // cmdSetDefaultSourceExcelDataFileFolder
            // 
            this.cmdSetDefaultSourceExcelDataFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultSourceExcelDataFileFolder.Location = new System.Drawing.Point(414, 247);
            this.cmdSetDefaultSourceExcelDataFileFolder.Name = "cmdSetDefaultSourceExcelDataFileFolder";
            this.cmdSetDefaultSourceExcelDataFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultSourceExcelDataFileFolder.TabIndex = 8;
            this.cmdSetDefaultSourceExcelDataFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultSourceExcelDataFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaultSourceExcelDataFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultSourceExcelDataFileFolder.Click += new System.EventHandler(this.cmdSetDefaultSourceExcelDataFileFolder_Click);
            // 
            // cmdSetDefaultSourceDelimitedTextFileFolder
            // 
            this.cmdSetDefaultSourceDelimitedTextFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultSourceDelimitedTextFileFolder.Location = new System.Drawing.Point(414, 286);
            this.cmdSetDefaultSourceDelimitedTextFileFolder.Name = "cmdSetDefaultSourceDelimitedTextFileFolder";
            this.cmdSetDefaultSourceDelimitedTextFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultSourceDelimitedTextFileFolder.TabIndex = 18;
            this.cmdSetDefaultSourceDelimitedTextFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultSourceDelimitedTextFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaultSourceDelimitedTextFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultSourceDelimitedTextFileFolder.Click += new System.EventHandler(this.cmdSetDefaultSourceDelimitedTextFileFolder_Click);
            // 
            // cmdSetDefaultSourceFixedLengthTextFileFolder
            // 
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.Location = new System.Drawing.Point(414, 326);
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.Name = "cmdSetDefaultSourceFixedLengthTextFileFolder";
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.TabIndex = 21;
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultSourceFixedLengthTextFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultSourceFixedLengthTextFileFolder.Click += new System.EventHandler(this.cmdSetDefaultSourceFixedLengthTextFileFolder_Click);
            // 
            // cmdSetDefaultDestinationFixedLengthTextFileFolder
            // 
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.Location = new System.Drawing.Point(410, 549);
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.Name = "cmdSetDefaultDestinationFixedLengthTextFileFolder";
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.TabIndex = 36;
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultDestinationFixedLengthTextFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultDestinationFixedLengthTextFileFolder.Click += new System.EventHandler(this.cmdSetDefaultDestinationFixedLengthTextFileFolder_Click);
            // 
            // cmdSetDefaultDestinationDelimitedTextFileFolder
            // 
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.Location = new System.Drawing.Point(411, 509);
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.Name = "cmdSetDefaultDestinationDelimitedTextFileFolder";
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.TabIndex = 33;
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultDestinationDelimitedTextFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultDestinationDelimitedTextFileFolder.Click += new System.EventHandler(this.cmdSetDefaultDestinationDelimitedTextFileFolder_Click);
            // 
            // cmdSetDefaulDestinationExcelDataFileFolder
            // 
            this.cmdSetDefaulDestinationExcelDataFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaulDestinationExcelDataFileFolder.Location = new System.Drawing.Point(411, 469);
            this.cmdSetDefaulDestinationExcelDataFileFolder.Name = "cmdSetDefaulDestinationExcelDataFileFolder";
            this.cmdSetDefaulDestinationExcelDataFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaulDestinationExcelDataFileFolder.TabIndex = 30;
            this.cmdSetDefaulDestinationExcelDataFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaulDestinationExcelDataFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaulDestinationExcelDataFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaulDestinationExcelDataFileFolder.Click += new System.EventHandler(this.cmdSetDefaulDestinationExcelDataFileFolder_Click);
            // 
            // cmdSetDefaultDestinationAccessDatabaseFolder
            // 
            this.cmdSetDefaultDestinationAccessDatabaseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultDestinationAccessDatabaseFolder.Location = new System.Drawing.Point(413, 430);
            this.cmdSetDefaultDestinationAccessDatabaseFolder.Name = "cmdSetDefaultDestinationAccessDatabaseFolder";
            this.cmdSetDefaultDestinationAccessDatabaseFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultDestinationAccessDatabaseFolder.TabIndex = 27;
            this.cmdSetDefaultDestinationAccessDatabaseFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultDestinationAccessDatabaseFolder, "Prompt to select initial folder path");
            this.cmdSetDefaultDestinationAccessDatabaseFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultDestinationAccessDatabaseFolder.Click += new System.EventHandler(this.cmdSetDefaultDestinationAccessDatabaseFolder_Click);
            // 
            // cmdSeExtractorDefinitionsSaveFolder
            // 
            this.cmdSeExtractorDefinitionsSaveFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSeExtractorDefinitionsSaveFolder.Location = new System.Drawing.Point(414, 71);
            this.cmdSeExtractorDefinitionsSaveFolder.Name = "cmdSeExtractorDefinitionsSaveFolder";
            this.cmdSeExtractorDefinitionsSaveFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSeExtractorDefinitionsSaveFolder.TabIndex = 39;
            this.cmdSeExtractorDefinitionsSaveFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSeExtractorDefinitionsSaveFolder, "Prompt to select default extractor definitions save folder.");
            this.cmdSeExtractorDefinitionsSaveFolder.UseVisualStyleBackColor = true;
            this.cmdSeExtractorDefinitionsSaveFolder.Click += new System.EventHandler(this.cmdSeExtractorDefinitionsSaveFolder_Click);
            // 
            // cmdGetDataGridExportFolder
            // 
            this.cmdGetDataGridExportFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetDataGridExportFolder.Location = new System.Drawing.Point(416, 150);
            this.cmdGetDataGridExportFolder.Name = "cmdGetDataGridExportFolder";
            this.cmdGetDataGridExportFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetDataGridExportFolder.TabIndex = 45;
            this.cmdGetDataGridExportFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdGetDataGridExportFolder, "Prompt to select folder for saving statistics files");
            this.cmdGetDataGridExportFolder.UseVisualStyleBackColor = true;
            this.cmdGetDataGridExportFolder.Click += new System.EventHandler(this.cmdGetDataGridExportFolder_Click);
            // 
            // cmdGetQueryDefinitionsFolder
            // 
            this.cmdGetQueryDefinitionsFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGetQueryDefinitionsFolder.Location = new System.Drawing.Point(416, 110);
            this.cmdGetQueryDefinitionsFolder.Name = "cmdGetQueryDefinitionsFolder";
            this.cmdGetQueryDefinitionsFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdGetQueryDefinitionsFolder.TabIndex = 42;
            this.cmdGetQueryDefinitionsFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdGetQueryDefinitionsFolder, "Prompt to select initial folder path");
            this.cmdGetQueryDefinitionsFolder.UseVisualStyleBackColor = true;
            this.cmdGetQueryDefinitionsFolder.Click += new System.EventHandler(this.cmdGetQueryDefinitionsFolder_Click);
            // 
            // cmdSetDefaultSourceXmlFileFolder
            // 
            this.cmdSetDefaultSourceXmlFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultSourceXmlFileFolder.Location = new System.Drawing.Point(415, 368);
            this.cmdSetDefaultSourceXmlFileFolder.Name = "cmdSetDefaultSourceXmlFileFolder";
            this.cmdSetDefaultSourceXmlFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultSourceXmlFileFolder.TabIndex = 48;
            this.cmdSetDefaultSourceXmlFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultSourceXmlFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaultSourceXmlFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultSourceXmlFileFolder.Click += new System.EventHandler(this.cmdSetDefaultSourceXmlFileFolder_Click);
            // 
            // cmdSetDefaultDestinationXmlFileFolder
            // 
            this.cmdSetDefaultDestinationXmlFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetDefaultDestinationXmlFileFolder.Location = new System.Drawing.Point(410, 585);
            this.cmdSetDefaultDestinationXmlFileFolder.Name = "cmdSetDefaultDestinationXmlFileFolder";
            this.cmdSetDefaultDestinationXmlFileFolder.Size = new System.Drawing.Size(38, 20);
            this.cmdSetDefaultDestinationXmlFileFolder.TabIndex = 51;
            this.cmdSetDefaultDestinationXmlFileFolder.Text = "•••";
            this.optionsFormToolTips.SetToolTip(this.cmdSetDefaultDestinationXmlFileFolder, "Prompt to select folder for saving statistics files");
            this.cmdSetDefaultDestinationXmlFileFolder.UseVisualStyleBackColor = true;
            this.cmdSetDefaultDestinationXmlFileFolder.Click += new System.EventHandler(this.cmdSetDefaultDestinationXmlFileFolder_Click);
            // 
            // lblDefaultSourceAccessDatabaseFolder
            // 
            this.lblDefaultSourceAccessDatabaseFolder.AutoSize = true;
            this.lblDefaultSourceAccessDatabaseFolder.Location = new System.Drawing.Point(34, 208);
            this.lblDefaultSourceAccessDatabaseFolder.Name = "lblDefaultSourceAccessDatabaseFolder";
            this.lblDefaultSourceAccessDatabaseFolder.Size = new System.Drawing.Size(200, 13);
            this.lblDefaultSourceAccessDatabaseFolder.TabIndex = 3;
            this.lblDefaultSourceAccessDatabaseFolder.Text = "Default Source: Access Database Folder";
            // 
            // txtDefaultSourceAccessDatabaseFolder
            // 
            this.txtDefaultSourceAccessDatabaseFolder.Location = new System.Drawing.Point(37, 225);
            this.txtDefaultSourceAccessDatabaseFolder.Name = "txtDefaultSourceAccessDatabaseFolder";
            this.txtDefaultSourceAccessDatabaseFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultSourceAccessDatabaseFolder.TabIndex = 4;
            // 
            // lblDefaultSourceExcelDataFileFolder
            // 
            this.lblDefaultSourceExcelDataFileFolder.AutoSize = true;
            this.lblDefaultSourceExcelDataFileFolder.Location = new System.Drawing.Point(34, 248);
            this.lblDefaultSourceExcelDataFileFolder.Name = "lblDefaultSourceExcelDataFileFolder";
            this.lblDefaultSourceExcelDataFileFolder.Size = new System.Drawing.Size(187, 13);
            this.lblDefaultSourceExcelDataFileFolder.TabIndex = 6;
            this.lblDefaultSourceExcelDataFileFolder.Text = "Default Source: Excel Data File Folder";
            // 
            // txtDefaultSourceExcelDataFileFolder
            // 
            this.txtDefaultSourceExcelDataFileFolder.Location = new System.Drawing.Point(37, 264);
            this.txtDefaultSourceExcelDataFileFolder.Name = "txtDefaultSourceExcelDataFileFolder";
            this.txtDefaultSourceExcelDataFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultSourceExcelDataFileFolder.TabIndex = 7;
            // 
            // lblUpdateResults
            // 
            this.lblUpdateResults.AutoSize = true;
            this.lblUpdateResults.Location = new System.Drawing.Point(37, 49);
            this.lblUpdateResults.Name = "lblUpdateResults";
            this.lblUpdateResults.Size = new System.Drawing.Size(13, 13);
            this.lblUpdateResults.TabIndex = 2;
            this.lblUpdateResults.Text = "  ";
            // 
            // FolderOptionsMenu
            // 
            this.FolderOptionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings});
            this.FolderOptionsMenu.Location = new System.Drawing.Point(0, 0);
            this.FolderOptionsMenu.Name = "FolderOptionsMenu";
            this.FolderOptionsMenu.Size = new System.Drawing.Size(639, 24);
            this.FolderOptionsMenu.TabIndex = 0;
            this.FolderOptionsMenu.Text = "menuStrip1";
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
            // lblDefaultSourceDelimitedTextFileFolder
            // 
            this.lblDefaultSourceDelimitedTextFileFolder.AutoSize = true;
            this.lblDefaultSourceDelimitedTextFileFolder.Location = new System.Drawing.Point(34, 288);
            this.lblDefaultSourceDelimitedTextFileFolder.Name = "lblDefaultSourceDelimitedTextFileFolder";
            this.lblDefaultSourceDelimitedTextFileFolder.Size = new System.Drawing.Size(202, 13);
            this.lblDefaultSourceDelimitedTextFileFolder.TabIndex = 16;
            this.lblDefaultSourceDelimitedTextFileFolder.Text = "Default Source: Delimited Text File Folder";
            // 
            // txtDefaultSourceDelimitedTextFileFolder
            // 
            this.txtDefaultSourceDelimitedTextFileFolder.Location = new System.Drawing.Point(37, 303);
            this.txtDefaultSourceDelimitedTextFileFolder.Name = "txtDefaultSourceDelimitedTextFileFolder";
            this.txtDefaultSourceDelimitedTextFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultSourceDelimitedTextFileFolder.TabIndex = 17;
            // 
            // txtDefaultSourceFixedLengthTextFileFolder
            // 
            this.txtDefaultSourceFixedLengthTextFileFolder.Location = new System.Drawing.Point(37, 345);
            this.txtDefaultSourceFixedLengthTextFileFolder.Name = "txtDefaultSourceFixedLengthTextFileFolder";
            this.txtDefaultSourceFixedLengthTextFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultSourceFixedLengthTextFileFolder.TabIndex = 20;
            // 
            // lblDefaultSourceFixedLengthTextFileFolder
            // 
            this.lblDefaultSourceFixedLengthTextFileFolder.AutoSize = true;
            this.lblDefaultSourceFixedLengthTextFileFolder.Location = new System.Drawing.Point(34, 330);
            this.lblDefaultSourceFixedLengthTextFileFolder.Name = "lblDefaultSourceFixedLengthTextFileFolder";
            this.lblDefaultSourceFixedLengthTextFileFolder.Size = new System.Drawing.Size(220, 13);
            this.lblDefaultSourceFixedLengthTextFileFolder.TabIndex = 19;
            this.lblDefaultSourceFixedLengthTextFileFolder.Text = "Default Source: Fixed Length Text File Folder";
            // 
            // txtDefaultDestinationFixedLengthTextFileFolder
            // 
            this.txtDefaultDestinationFixedLengthTextFileFolder.Location = new System.Drawing.Point(35, 566);
            this.txtDefaultDestinationFixedLengthTextFileFolder.Name = "txtDefaultDestinationFixedLengthTextFileFolder";
            this.txtDefaultDestinationFixedLengthTextFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultDestinationFixedLengthTextFileFolder.TabIndex = 35;
            // 
            // lblDefaultDestinationFixedLengthTextFileFolder
            // 
            this.lblDefaultDestinationFixedLengthTextFileFolder.AutoSize = true;
            this.lblDefaultDestinationFixedLengthTextFileFolder.Location = new System.Drawing.Point(33, 550);
            this.lblDefaultDestinationFixedLengthTextFileFolder.Name = "lblDefaultDestinationFixedLengthTextFileFolder";
            this.lblDefaultDestinationFixedLengthTextFileFolder.Size = new System.Drawing.Size(239, 13);
            this.lblDefaultDestinationFixedLengthTextFileFolder.TabIndex = 34;
            this.lblDefaultDestinationFixedLengthTextFileFolder.Text = "Default Destination: Fixed Length Text File Folder";
            // 
            // txtDefaultDestinationDelimitedTextFileFolder
            // 
            this.txtDefaultDestinationDelimitedTextFileFolder.Location = new System.Drawing.Point(36, 526);
            this.txtDefaultDestinationDelimitedTextFileFolder.Name = "txtDefaultDestinationDelimitedTextFileFolder";
            this.txtDefaultDestinationDelimitedTextFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultDestinationDelimitedTextFileFolder.TabIndex = 32;
            // 
            // lblDefaultDestinationDelimitedTextFileFolder
            // 
            this.lblDefaultDestinationDelimitedTextFileFolder.AutoSize = true;
            this.lblDefaultDestinationDelimitedTextFileFolder.Location = new System.Drawing.Point(33, 510);
            this.lblDefaultDestinationDelimitedTextFileFolder.Name = "lblDefaultDestinationDelimitedTextFileFolder";
            this.lblDefaultDestinationDelimitedTextFileFolder.Size = new System.Drawing.Size(221, 13);
            this.lblDefaultDestinationDelimitedTextFileFolder.TabIndex = 31;
            this.lblDefaultDestinationDelimitedTextFileFolder.Text = "Default Destination: Delimited Text File Folder";
            // 
            // txtDefaultDestinationExcelDataFileFolder
            // 
            this.txtDefaultDestinationExcelDataFileFolder.Location = new System.Drawing.Point(36, 486);
            this.txtDefaultDestinationExcelDataFileFolder.Name = "txtDefaultDestinationExcelDataFileFolder";
            this.txtDefaultDestinationExcelDataFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultDestinationExcelDataFileFolder.TabIndex = 29;
            // 
            // lblDefaultDestinationExcelDataFileFolder
            // 
            this.lblDefaultDestinationExcelDataFileFolder.AutoSize = true;
            this.lblDefaultDestinationExcelDataFileFolder.Location = new System.Drawing.Point(33, 469);
            this.lblDefaultDestinationExcelDataFileFolder.Name = "lblDefaultDestinationExcelDataFileFolder";
            this.lblDefaultDestinationExcelDataFileFolder.Size = new System.Drawing.Size(206, 13);
            this.lblDefaultDestinationExcelDataFileFolder.TabIndex = 28;
            this.lblDefaultDestinationExcelDataFileFolder.Text = "Default Destination: Excel Data File Folder";
            // 
            // txtDefaultDestinationAccessDatabaseFolder
            // 
            this.txtDefaultDestinationAccessDatabaseFolder.Location = new System.Drawing.Point(36, 447);
            this.txtDefaultDestinationAccessDatabaseFolder.Name = "txtDefaultDestinationAccessDatabaseFolder";
            this.txtDefaultDestinationAccessDatabaseFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultDestinationAccessDatabaseFolder.TabIndex = 26;
            // 
            // lblDefaultDestinationAccessDatabaseFolder
            // 
            this.lblDefaultDestinationAccessDatabaseFolder.AutoSize = true;
            this.lblDefaultDestinationAccessDatabaseFolder.Location = new System.Drawing.Point(33, 430);
            this.lblDefaultDestinationAccessDatabaseFolder.Name = "lblDefaultDestinationAccessDatabaseFolder";
            this.lblDefaultDestinationAccessDatabaseFolder.Size = new System.Drawing.Size(216, 13);
            this.lblDefaultDestinationAccessDatabaseFolder.TabIndex = 25;
            this.lblDefaultDestinationAccessDatabaseFolder.Text = "Default Destination Access Database Folder";
            // 
            // txtDefaultExtractorDefinitionsSaveFolder
            // 
            this.txtDefaultExtractorDefinitionsSaveFolder.Location = new System.Drawing.Point(38, 88);
            this.txtDefaultExtractorDefinitionsSaveFolder.Name = "txtDefaultExtractorDefinitionsSaveFolder";
            this.txtDefaultExtractorDefinitionsSaveFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultExtractorDefinitionsSaveFolder.TabIndex = 38;
            // 
            // lblDefaultExtractorDefinitionsSaveFolder
            // 
            this.lblDefaultExtractorDefinitionsSaveFolder.AutoSize = true;
            this.lblDefaultExtractorDefinitionsSaveFolder.Location = new System.Drawing.Point(35, 71);
            this.lblDefaultExtractorDefinitionsSaveFolder.Name = "lblDefaultExtractorDefinitionsSaveFolder";
            this.lblDefaultExtractorDefinitionsSaveFolder.Size = new System.Drawing.Size(198, 13);
            this.lblDefaultExtractorDefinitionsSaveFolder.TabIndex = 37;
            this.lblDefaultExtractorDefinitionsSaveFolder.Text = "Default Extractor Definitions Save Folder";
            // 
            // txtDefaultDataGridExportFolder
            // 
            this.txtDefaultDataGridExportFolder.Location = new System.Drawing.Point(37, 167);
            this.txtDefaultDataGridExportFolder.Name = "txtDefaultDataGridExportFolder";
            this.txtDefaultDataGridExportFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultDataGridExportFolder.TabIndex = 44;
            // 
            // lblDefaultDataGridExportFolder
            // 
            this.lblDefaultDataGridExportFolder.AutoSize = true;
            this.lblDefaultDataGridExportFolder.Location = new System.Drawing.Point(34, 150);
            this.lblDefaultDataGridExportFolder.Name = "lblDefaultDataGridExportFolder";
            this.lblDefaultDataGridExportFolder.Size = new System.Drawing.Size(182, 13);
            this.lblDefaultDataGridExportFolder.TabIndex = 43;
            this.lblDefaultDataGridExportFolder.Text = "Default folder for data grid export files";
            // 
            // txtDefaultQueryDefinitionsFolder
            // 
            this.txtDefaultQueryDefinitionsFolder.Location = new System.Drawing.Point(37, 127);
            this.txtDefaultQueryDefinitionsFolder.Name = "txtDefaultQueryDefinitionsFolder";
            this.txtDefaultQueryDefinitionsFolder.Size = new System.Drawing.Size(415, 20);
            this.txtDefaultQueryDefinitionsFolder.TabIndex = 41;
            // 
            // lblDefaultQueryDefinitionsFolder
            // 
            this.lblDefaultQueryDefinitionsFolder.AutoSize = true;
            this.lblDefaultQueryDefinitionsFolder.Location = new System.Drawing.Point(34, 110);
            this.lblDefaultQueryDefinitionsFolder.Name = "lblDefaultQueryDefinitionsFolder";
            this.lblDefaultQueryDefinitionsFolder.Size = new System.Drawing.Size(198, 13);
            this.lblDefaultQueryDefinitionsFolder.TabIndex = 40;
            this.lblDefaultQueryDefinitionsFolder.Text = "Default folder for storing query definitions";
            // 
            // txtDefaultSourceXmlFileFolder
            // 
            this.txtDefaultSourceXmlFileFolder.Location = new System.Drawing.Point(37, 387);
            this.txtDefaultSourceXmlFileFolder.Name = "txtDefaultSourceXmlFileFolder";
            this.txtDefaultSourceXmlFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultSourceXmlFileFolder.TabIndex = 47;
            // 
            // lblDefaultSourceXmlFileFolder
            // 
            this.lblDefaultSourceXmlFileFolder.AutoSize = true;
            this.lblDefaultSourceXmlFileFolder.Location = new System.Drawing.Point(33, 372);
            this.lblDefaultSourceXmlFileFolder.Name = "lblDefaultSourceXmlFileFolder";
            this.lblDefaultSourceXmlFileFolder.Size = new System.Drawing.Size(203, 13);
            this.lblDefaultSourceXmlFileFolder.TabIndex = 46;
            this.lblDefaultSourceXmlFileFolder.Text = "Default Source: XML and XSD File Folder";
            // 
            // txtDefaultDestinationXmlFileFolder
            // 
            this.txtDefaultDestinationXmlFileFolder.Location = new System.Drawing.Point(34, 605);
            this.txtDefaultDestinationXmlFileFolder.Name = "txtDefaultDestinationXmlFileFolder";
            this.txtDefaultDestinationXmlFileFolder.Size = new System.Drawing.Size(414, 20);
            this.txtDefaultDestinationXmlFileFolder.TabIndex = 50;
            // 
            // lblDefaultDestinationXmlFileFolder
            // 
            this.lblDefaultDestinationXmlFileFolder.AutoSize = true;
            this.lblDefaultDestinationXmlFileFolder.Location = new System.Drawing.Point(33, 589);
            this.lblDefaultDestinationXmlFileFolder.Name = "lblDefaultDestinationXmlFileFolder";
            this.lblDefaultDestinationXmlFileFolder.Size = new System.Drawing.Size(222, 13);
            this.lblDefaultDestinationXmlFileFolder.TabIndex = 49;
            this.lblDefaultDestinationXmlFileFolder.Text = "Default Destination: XML and XSD File Folder";
            // 
            // FolderOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 648);
            this.Controls.Add(this.cmdSetDefaultDestinationXmlFileFolder);
            this.Controls.Add(this.txtDefaultDestinationXmlFileFolder);
            this.Controls.Add(this.lblDefaultDestinationXmlFileFolder);
            this.Controls.Add(this.cmdSetDefaultSourceXmlFileFolder);
            this.Controls.Add(this.txtDefaultSourceXmlFileFolder);
            this.Controls.Add(this.lblDefaultSourceXmlFileFolder);
            this.Controls.Add(this.cmdGetDataGridExportFolder);
            this.Controls.Add(this.txtDefaultDataGridExportFolder);
            this.Controls.Add(this.lblDefaultDataGridExportFolder);
            this.Controls.Add(this.cmdGetQueryDefinitionsFolder);
            this.Controls.Add(this.txtDefaultQueryDefinitionsFolder);
            this.Controls.Add(this.lblDefaultQueryDefinitionsFolder);
            this.Controls.Add(this.cmdSeExtractorDefinitionsSaveFolder);
            this.Controls.Add(this.txtDefaultExtractorDefinitionsSaveFolder);
            this.Controls.Add(this.lblDefaultExtractorDefinitionsSaveFolder);
            this.Controls.Add(this.cmdSetDefaultDestinationFixedLengthTextFileFolder);
            this.Controls.Add(this.txtDefaultDestinationFixedLengthTextFileFolder);
            this.Controls.Add(this.lblDefaultDestinationFixedLengthTextFileFolder);
            this.Controls.Add(this.cmdSetDefaultDestinationDelimitedTextFileFolder);
            this.Controls.Add(this.txtDefaultDestinationDelimitedTextFileFolder);
            this.Controls.Add(this.lblDefaultDestinationDelimitedTextFileFolder);
            this.Controls.Add(this.cmdSetDefaulDestinationExcelDataFileFolder);
            this.Controls.Add(this.txtDefaultDestinationExcelDataFileFolder);
            this.Controls.Add(this.lblDefaultDestinationExcelDataFileFolder);
            this.Controls.Add(this.cmdSetDefaultDestinationAccessDatabaseFolder);
            this.Controls.Add(this.txtDefaultDestinationAccessDatabaseFolder);
            this.Controls.Add(this.lblDefaultDestinationAccessDatabaseFolder);
            this.Controls.Add(this.cmdSetDefaultSourceFixedLengthTextFileFolder);
            this.Controls.Add(this.txtDefaultSourceFixedLengthTextFileFolder);
            this.Controls.Add(this.lblDefaultSourceFixedLengthTextFileFolder);
            this.Controls.Add(this.cmdSetDefaultSourceDelimitedTextFileFolder);
            this.Controls.Add(this.txtDefaultSourceDelimitedTextFileFolder);
            this.Controls.Add(this.lblDefaultSourceDelimitedTextFileFolder);
            this.Controls.Add(this.lblUpdateResults);
            this.Controls.Add(this.cmdSetDefaultSourceExcelDataFileFolder);
            this.Controls.Add(this.txtDefaultSourceExcelDataFileFolder);
            this.Controls.Add(this.lblDefaultSourceExcelDataFileFolder);
            this.Controls.Add(this.cmdSetDefaultSourceAccessDatabaseFolder);
            this.Controls.Add(this.txtDefaultSourceAccessDatabaseFolder);
            this.Controls.Add(this.lblDefaultSourceAccessDatabaseFolder);
            this.Controls.Add(this.lblFormMessage);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.FolderOptionsMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.FolderOptionsMenu;
            this.Name = "FolderOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folder Options Options for InitWinFormsAppExt";
            this.Load += new System.EventHandler(this.FolderOptionsForm_Load);
            this.FolderOptionsMenu.ResumeLayout(false);
            this.FolderOptionsMenu.PerformLayout();
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
        private System.Windows.Forms.Label lblDefaultSourceAccessDatabaseFolder;
        private System.Windows.Forms.TextBox txtDefaultSourceAccessDatabaseFolder;
        private System.Windows.Forms.Button cmdSetDefaultSourceAccessDatabaseFolder;
        private System.Windows.Forms.Label lblDefaultSourceExcelDataFileFolder;
        private System.Windows.Forms.TextBox txtDefaultSourceExcelDataFileFolder;
        private System.Windows.Forms.Label lblUpdateResults;
        private System.Windows.Forms.MenuStrip FolderOptionsMenu;
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
        private System.Windows.Forms.Button cmdSetDefaultSourceExcelDataFileFolder;
        private System.Windows.Forms.Label lblDefaultSourceDelimitedTextFileFolder;
        private System.Windows.Forms.Button cmdSetDefaultSourceDelimitedTextFileFolder;
        private System.Windows.Forms.TextBox txtDefaultSourceDelimitedTextFileFolder;
        private System.Windows.Forms.Button cmdSetDefaultSourceFixedLengthTextFileFolder;
        private System.Windows.Forms.TextBox txtDefaultSourceFixedLengthTextFileFolder;
        private System.Windows.Forms.Label lblDefaultSourceFixedLengthTextFileFolder;
        private System.Windows.Forms.Button cmdSetDefaultDestinationFixedLengthTextFileFolder;
        private System.Windows.Forms.TextBox txtDefaultDestinationFixedLengthTextFileFolder;
        private System.Windows.Forms.Label lblDefaultDestinationFixedLengthTextFileFolder;
        private System.Windows.Forms.Button cmdSetDefaultDestinationDelimitedTextFileFolder;
        private System.Windows.Forms.TextBox txtDefaultDestinationDelimitedTextFileFolder;
        private System.Windows.Forms.Label lblDefaultDestinationDelimitedTextFileFolder;
        private System.Windows.Forms.Button cmdSetDefaulDestinationExcelDataFileFolder;
        private System.Windows.Forms.TextBox txtDefaultDestinationExcelDataFileFolder;
        private System.Windows.Forms.Label lblDefaultDestinationExcelDataFileFolder;
        private System.Windows.Forms.Button cmdSetDefaultDestinationAccessDatabaseFolder;
        private System.Windows.Forms.TextBox txtDefaultDestinationAccessDatabaseFolder;
        private System.Windows.Forms.Label lblDefaultDestinationAccessDatabaseFolder;
        private System.Windows.Forms.Button cmdSeExtractorDefinitionsSaveFolder;
        private System.Windows.Forms.TextBox txtDefaultExtractorDefinitionsSaveFolder;
        private System.Windows.Forms.Label lblDefaultExtractorDefinitionsSaveFolder;
        private System.Windows.Forms.Button cmdGetDataGridExportFolder;
        private System.Windows.Forms.TextBox txtDefaultDataGridExportFolder;
        private System.Windows.Forms.Label lblDefaultDataGridExportFolder;
        private System.Windows.Forms.Button cmdGetQueryDefinitionsFolder;
        private System.Windows.Forms.TextBox txtDefaultQueryDefinitionsFolder;
        private System.Windows.Forms.Label lblDefaultQueryDefinitionsFolder;
        private System.Windows.Forms.TextBox txtDefaultSourceXmlFileFolder;
        private System.Windows.Forms.Label lblDefaultSourceXmlFileFolder;
        private System.Windows.Forms.Button cmdSetDefaultSourceXmlFileFolder;
        private System.Windows.Forms.Button cmdSetDefaultDestinationXmlFileFolder;
        private System.Windows.Forms.TextBox txtDefaultDestinationXmlFileFolder;
        private System.Windows.Forms.Label lblDefaultDestinationXmlFileFolder;

    }
#pragma warning restore 1591
}
