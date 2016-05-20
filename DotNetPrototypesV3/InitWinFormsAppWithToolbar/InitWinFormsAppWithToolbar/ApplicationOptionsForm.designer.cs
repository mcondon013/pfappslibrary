namespace InitWinFormsAppWithToolbar
{
    partial class ApplicationOptionsForm
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.cmdHelp = new System.Windows.Forms.Button();
            this.keyValsDataSet = new System.Data.DataSet();
            this.KeyValTable = new System.Data.DataTable();
            this.AppSetting = new System.Data.DataColumn();
            this.SettingValue = new System.Data.DataColumn();
            this.lblFormMessage1 = new System.Windows.Forms.Label();
            this.lblUpdateResults = new System.Windows.Forms.Label();
            this.lblDataValidationMessage = new System.Windows.Forms.Label();
            this.cmdReset = new System.Windows.Forms.Button();
            this.dataGridAppSettings = new System.Windows.Forms.DataGridView();
            this.appOptionsMenu = new System.Windows.Forms.MenuStrip();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsExportToXls = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsExportToXlsx = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsExportToCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingExportToDelimitedTextFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsExportToAccess = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsExportToXml = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettingsPageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettingsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.AppSettingKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppSettingValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettingsRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettingsImport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.keyValsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppSettings)).BeginInit();
            this.appOptionsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(503, 365);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(93, 37);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "&Cancel";
            this.optionsFormToolTips.SetToolTip(this.cmdCancel, "Close this form and cancel any pending updates.");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(503, 47);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(93, 37);
            this.cmdOK.TabIndex = 20;
            this.cmdOK.Text = "&OK";
            this.optionsFormToolTips.SetToolTip(this.cmdOK, "Save changes and close form.");
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdApply.Location = new System.Drawing.Point(503, 122);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(93, 37);
            this.cmdApply.TabIndex = 21;
            this.cmdApply.Text = "&Apply";
            this.optionsFormToolTips.SetToolTip(this.cmdApply, "Save changes and leave form open.");
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // cmdHelp
            // 
            this.cmdHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdHelp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdHelp.Location = new System.Drawing.Point(503, 298);
            this.cmdHelp.Name = "cmdHelp";
            this.cmdHelp.Size = new System.Drawing.Size(93, 37);
            this.cmdHelp.TabIndex = 22;
            this.cmdHelp.Text = "&Help";
            this.optionsFormToolTips.SetToolTip(this.cmdHelp, "Show Help for this form.");
            this.cmdHelp.UseVisualStyleBackColor = true;
            this.cmdHelp.Click += new System.EventHandler(this.cmdHelp_Click);
            // 
            // keyValsDataSet
            // 
            this.keyValsDataSet.DataSetName = "appKeysDataSet";
            this.keyValsDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.KeyValTable});
            // 
            // KeyValTable
            // 
            this.KeyValTable.Columns.AddRange(new System.Data.DataColumn[] {
            this.AppSetting,
            this.SettingValue});
            this.KeyValTable.TableName = "KeyValTable";
            // 
            // AppSetting
            // 
            this.AppSetting.Caption = "App Setting";
            this.AppSetting.ColumnName = "AppSetting";
            this.AppSetting.MaxLength = 100;
            // 
            // SettingValue
            // 
            this.SettingValue.Caption = "Setting Value";
            this.SettingValue.ColumnName = "SettingValue";
            this.SettingValue.MaxLength = 255;
            // 
            // lblFormMessage1
            // 
            this.lblFormMessage1.AutoSize = true;
            this.lblFormMessage1.Location = new System.Drawing.Point(29, 47);
            this.lblFormMessage1.Name = "lblFormMessage1";
            this.lblFormMessage1.Size = new System.Drawing.Size(301, 13);
            this.lblFormMessage1.TabIndex = 24;
            this.lblFormMessage1.Text = "Make changes to values below and press OK or Apply to save";
            // 
            // lblUpdateResults
            // 
            this.lblUpdateResults.AutoSize = true;
            this.lblUpdateResults.Location = new System.Drawing.Point(29, 76);
            this.lblUpdateResults.Name = "lblUpdateResults";
            this.lblUpdateResults.Size = new System.Drawing.Size(13, 13);
            this.lblUpdateResults.TabIndex = 25;
            this.lblUpdateResults.Text = "  ";
            // 
            // lblDataValidationMessage
            // 
            this.lblDataValidationMessage.AutoSize = true;
            this.lblDataValidationMessage.Location = new System.Drawing.Point(29, 94);
            this.lblDataValidationMessage.Name = "lblDataValidationMessage";
            this.lblDataValidationMessage.Size = new System.Drawing.Size(19, 13);
            this.lblDataValidationMessage.TabIndex = 26;
            this.lblDataValidationMessage.Text = "    ";
            // 
            // cmdReset
            // 
            this.cmdReset.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cmdReset.Location = new System.Drawing.Point(503, 219);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(93, 37);
            this.cmdReset.TabIndex = 27;
            this.cmdReset.Text = "&Reset";
            this.optionsFormToolTips.SetToolTip(this.cmdReset, "Reset all app settings to last saved values.");
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // dataGridAppSettings
            // 
            this.dataGridAppSettings.AllowUserToAddRows = false;
            this.dataGridAppSettings.AllowUserToDeleteRows = false;
            this.dataGridAppSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridAppSettings.AutoGenerateColumns = false;
            this.dataGridAppSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAppSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AppSettingKey,
            this.AppSettingValue});
            this.dataGridAppSettings.DataMember = "KeyValTable";
            this.dataGridAppSettings.DataSource = this.keyValsDataSet;
            this.dataGridAppSettings.Location = new System.Drawing.Point(32, 122);
            this.dataGridAppSettings.Name = "dataGridAppSettings";
            this.dataGridAppSettings.Size = new System.Drawing.Size(423, 280);
            this.dataGridAppSettings.TabIndex = 28;
            // 
            // appOptionsMenu
            // 
            this.appOptionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings});
            this.appOptionsMenu.Location = new System.Drawing.Point(0, 0);
            this.appOptionsMenu.Name = "appOptionsMenu";
            this.appOptionsMenu.Size = new System.Drawing.Size(635, 24);
            this.appOptionsMenu.TabIndex = 29;
            this.appOptionsMenu.Text = "menuStrip1";
            // 
            // mnuSettings
            // 
            this.mnuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettingsExport,
            this.toolStripSeparator1,
            this.mnuSettingsImport,
            this.toolStripSeparator4,
            this.mnuSettingsPageSetup,
            this.mnuSettingsPrintPreview,
            this.mnuSettingsPrint,
            this.toolStripSeparator2,
            this.mnuSettingsRestore,
            this.toolStripSeparator3,
            this.mnuSettingsExit});
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(61, 20);
            this.mnuSettings.Text = "&Settings";
            this.mnuSettings.ToolTipText = "Copy app settings to various file formats.";
            // 
            // mnuSettingsExport
            // 
            this.mnuSettingsExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettingsExportToXls,
            this.mnuSettingsExportToXlsx,
            this.mnuSettingsExportToCsv,
            this.mnuSettingExportToDelimitedTextFile,
            this.mnuSettingsExportToAccess,
            this.mnuSettingsExportToXml});
            this.mnuSettingsExport.Name = "mnuSettingsExport";
            this.mnuSettingsExport.Size = new System.Drawing.Size(143, 22);
            this.mnuSettingsExport.Text = "&Export";
            this.mnuSettingsExport.ToolTipText = "Export settings from grid to a file";
            // 
            // mnuSettingsExportToXls
            // 
            this.mnuSettingsExportToXls.Name = "mnuSettingsExportToXls";
            this.mnuSettingsExportToXls.Size = new System.Drawing.Size(188, 22);
            this.mnuSettingsExportToXls.Text = "To &Excel XLS";
            this.mnuSettingsExportToXls.ToolTipText = "Export to Excel .XLS format";
            this.mnuSettingsExportToXls.Click += new System.EventHandler(this.mnuSettingsExportToXls_Click);
            // 
            // mnuSettingsExportToXlsx
            // 
            this.mnuSettingsExportToXlsx.Name = "mnuSettingsExportToXlsx";
            this.mnuSettingsExportToXlsx.Size = new System.Drawing.Size(188, 22);
            this.mnuSettingsExportToXlsx.Text = "To Excel &XLSX";
            this.mnuSettingsExportToXlsx.ToolTipText = "Export to Excel XLSX Format";
            this.mnuSettingsExportToXlsx.Click += new System.EventHandler(this.mnuSettingsExportToXlsx_Click);
            // 
            // mnuSettingsExportToCsv
            // 
            this.mnuSettingsExportToCsv.Name = "mnuSettingsExportToCsv";
            this.mnuSettingsExportToCsv.Size = new System.Drawing.Size(188, 22);
            this.mnuSettingsExportToCsv.Text = "To &CSV File";
            this.mnuSettingsExportToCsv.ToolTipText = "Export to comma separated values format";
            this.mnuSettingsExportToCsv.Click += new System.EventHandler(this.mnuSettingsExportToCsv_Click);
            // 
            // mnuSettingExportToDelimitedTextFile
            // 
            this.mnuSettingExportToDelimitedTextFile.Name = "mnuSettingExportToDelimitedTextFile";
            this.mnuSettingExportToDelimitedTextFile.Size = new System.Drawing.Size(188, 22);
            this.mnuSettingExportToDelimitedTextFile.Text = "To Delimited Text File";
            this.mnuSettingExportToDelimitedTextFile.ToolTipText = "Used this to specify a non-standard delimiter.";
            this.mnuSettingExportToDelimitedTextFile.Click += new System.EventHandler(this.mnuSettingExportToDelimitedTextFile_Click);
            // 
            // mnuSettingsExportToAccess
            // 
            this.mnuSettingsExportToAccess.Name = "mnuSettingsExportToAccess";
            this.mnuSettingsExportToAccess.Size = new System.Drawing.Size(188, 22);
            this.mnuSettingsExportToAccess.Text = "To Access";
            this.mnuSettingsExportToAccess.ToolTipText = "Export settings to an Access table.";
            this.mnuSettingsExportToAccess.Click += new System.EventHandler(this.mnuSettingsExportToAccess_Click);
            // 
            // mnuSettingsExportToXml
            // 
            this.mnuSettingsExportToXml.Name = "mnuSettingsExportToXml";
            this.mnuSettingsExportToXml.Size = new System.Drawing.Size(188, 22);
            this.mnuSettingsExportToXml.Text = "To Xml";
            this.mnuSettingsExportToXml.ToolTipText = "Export settings to an Xml formatted file.";
            this.mnuSettingsExportToXml.Click += new System.EventHandler(this.mnuSettingsExportToXml_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuSettingsPageSetup
            // 
            this.mnuSettingsPageSetup.Name = "mnuSettingsPageSetup";
            this.mnuSettingsPageSetup.Size = new System.Drawing.Size(143, 22);
            this.mnuSettingsPageSetup.Text = "Page Setup";
            this.mnuSettingsPageSetup.ToolTipText = "Modify page settings for printer output";
            this.mnuSettingsPageSetup.Click += new System.EventHandler(this.mnuSettingsPageSetup_Click);
            // 
            // mnuSettingsPrintPreview
            // 
            this.mnuSettingsPrintPreview.Name = "mnuSettingsPrintPreview";
            this.mnuSettingsPrintPreview.Size = new System.Drawing.Size(143, 22);
            this.mnuSettingsPrintPreview.Text = "Print Pre&view";
            this.mnuSettingsPrintPreview.ToolTipText = "Show the grid in print preview mode";
            this.mnuSettingsPrintPreview.Click += new System.EventHandler(this.mnuSettingsPrintPreview_Click);
            // 
            // mnuSettingsPrint
            // 
            this.mnuSettingsPrint.Name = "mnuSettingsPrint";
            this.mnuSettingsPrint.Size = new System.Drawing.Size(143, 22);
            this.mnuSettingsPrint.Text = "Print";
            this.mnuSettingsPrint.ToolTipText = "Output contents of grid to printer";
            this.mnuSettingsPrint.Click += new System.EventHandler(this.mnuSettingsPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuSettingsExit
            // 
            this.mnuSettingsExit.Name = "mnuSettingsExit";
            this.mnuSettingsExit.Size = new System.Drawing.Size(143, 22);
            this.mnuSettingsExit.Text = "E&xit";
            this.mnuSettingsExit.ToolTipText = "Close this form and cancel and pending updates.";
            this.mnuSettingsExit.Click += new System.EventHandler(this.mnuSettingsExit_Click);
            // 
            // AppSettingKey
            // 
            this.AppSettingKey.DataPropertyName = "AppSetting";
            this.AppSettingKey.HeaderText = "AppSetting";
            this.AppSettingKey.Name = "AppSettingKey";
            this.AppSettingKey.ReadOnly = true;
            // 
            // AppSettingValue
            // 
            this.AppSettingValue.DataPropertyName = "SettingValue";
            this.AppSettingValue.HeaderText = "SettingValue";
            this.AppSettingValue.Name = "AppSettingValue";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuSettingsRestore
            // 
            this.mnuSettingsRestore.Name = "mnuSettingsRestore";
            this.mnuSettingsRestore.Size = new System.Drawing.Size(152, 22);
            this.mnuSettingsRestore.Text = "Restore";
            this.mnuSettingsRestore.ToolTipText = "Restore original application settings";
            this.mnuSettingsRestore.Click += new System.EventHandler(this.mnuSettingsRestore_Click);
            // 
            // mnuSettingsImport
            // 
            this.mnuSettingsImport.Name = "mnuSettingsImport";
            this.mnuSettingsImport.Size = new System.Drawing.Size(152, 22);
            this.mnuSettingsImport.Text = "Import";
            this.mnuSettingsImport.ToolTipText = "Import application settings from previously save XML export file.";
            this.mnuSettingsImport.Click += new System.EventHandler(this.mnuSettingsImport_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(140, 6);
            // 
            // ApplicationOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 448);
            this.Controls.Add(this.dataGridAppSettings);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.lblDataValidationMessage);
            this.Controls.Add(this.lblUpdateResults);
            this.Controls.Add(this.lblFormMessage1);
            this.Controls.Add(this.cmdHelp);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.appOptionsMenu);
            this.MainMenuStrip = this.appOptionsMenu;
            this.Name = "ApplicationOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Options";
            this.Load += new System.EventHandler(this.AppOptionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.keyValsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KeyValTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAppSettings)).EndInit();
            this.appOptionsMenu.ResumeLayout(false);
            this.appOptionsMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.Button cmdHelp;
        private System.Data.DataSet keyValsDataSet;
        private System.Data.DataTable KeyValTable;
        private System.Data.DataColumn AppSetting;
        private System.Data.DataColumn SettingValue;
        private System.Windows.Forms.Label lblFormMessage1;
        private System.Windows.Forms.Label lblUpdateResults;
        private System.Windows.Forms.Label lblDataValidationMessage;
        private System.Windows.Forms.Button cmdReset;
        private System.Windows.Forms.DataGridView dataGridAppSettings;
        private System.Windows.Forms.MenuStrip appOptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsExport;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsExportToXls;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsExportToCsv;
        private System.Windows.Forms.ToolTip optionsFormToolTips;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsExportToXlsx;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsPageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsExit;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsExportToAccess;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsExportToXml;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingExportToDelimitedTextFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppSettingKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn AppSettingValue;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuSettingsImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    }
}