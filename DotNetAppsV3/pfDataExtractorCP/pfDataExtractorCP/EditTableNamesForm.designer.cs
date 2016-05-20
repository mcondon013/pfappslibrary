namespace pfDataExtractorCP
{
#pragma warning disable 1591
    partial class EditTableNamesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditTableNamesForm));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.TableNamesMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.tableNamesHelpProvider = new System.Windows.Forms.HelpProvider();
            this.tableNamesFormToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.lblSalesOrderHeadersTableName = new System.Windows.Forms.Label();
            this.lblSalesOrderDetailsTableName = new System.Windows.Forms.Label();
            this.lblPurchaseOrderHeadersTableName = new System.Windows.Forms.Label();
            this.blPurchaseOrderDetailsTableName = new System.Windows.Forms.Label();
            this.lblDwInternetSalesTableName = new System.Windows.Forms.Label();
            this.lblDwResellerSalesTableName = new System.Windows.Forms.Label();
            this.txtDwResellerSalesTableName = new System.Windows.Forms.TextBox();
            this.txtDwInternetSalesTableName = new System.Windows.Forms.TextBox();
            this.txtPurchaseOrderDetailsTableName = new System.Windows.Forms.TextBox();
            this.txtPurchaseOrderHeadersTableName = new System.Windows.Forms.TextBox();
            this.txtSalesOrderDetailsTableName = new System.Windows.Forms.TextBox();
            this.txtSalesOrderHeadersTableName = new System.Windows.Forms.TextBox();
            this.lblSchemaWarning = new System.Windows.Forms.Label();
            this.lblTableSchema = new System.Windows.Forms.Label();
            this.txtTableSchema = new System.Windows.Forms.TextBox();
            this.TableNamesMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.tableNamesHelpProvider.SetHelpKeyword(this.cmdCancel, "Exit Button");
            this.tableNamesHelpProvider.SetHelpNavigator(this.cmdCancel, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdCancel.Location = new System.Drawing.Point(463, 232);
            this.cmdCancel.Name = "cmdCancel";
            this.tableNamesHelpProvider.SetShowHelp(this.cmdCancel, true);
            this.cmdCancel.Size = new System.Drawing.Size(93, 30);
            this.cmdCancel.TabIndex = 17;
            this.cmdCancel.Text = "&Cancel";
            this.tableNamesFormToolTips.SetToolTip(this.cmdCancel, "Close form and exit application");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAccept
            // 
            this.tableNamesHelpProvider.SetHelpKeyword(this.cmdAccept, "Run Tests");
            this.tableNamesHelpProvider.SetHelpNavigator(this.cmdAccept, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.tableNamesHelpProvider.SetHelpString(this.cmdAccept, "Help for Run Tests: See Help File.");
            this.cmdAccept.Location = new System.Drawing.Point(463, 98);
            this.cmdAccept.Name = "cmdAccept";
            this.tableNamesHelpProvider.SetShowHelp(this.cmdAccept, true);
            this.cmdAccept.Size = new System.Drawing.Size(93, 29);
            this.cmdAccept.TabIndex = 14;
            this.cmdAccept.Text = "&Accept";
            this.tableNamesFormToolTips.SetToolTip(this.cmdAccept, "Run selected tests");
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAcceptForm_Click);
            // 
            // TableNamesMenu
            // 
            this.TableNamesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp,
            this.toolbarHelp});
            this.TableNamesMenu.Location = new System.Drawing.Point(0, 0);
            this.TableNamesMenu.Name = "TableNamesMenu";
            this.TableNamesMenu.Size = new System.Drawing.Size(575, 27);
            this.TableNamesMenu.TabIndex = 0;
            this.TableNamesMenu.Text = "menuStrip1";
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
            // tableNamesHelpProvider
            // 
            this.tableNamesHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\pfDataExtractorCP\\InitWinFormsAppWithToolbar" +
    "\\pfDataExtractorCP.chm";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormTitle.Location = new System.Drawing.Point(76, 34);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(361, 48);
            this.lblFormTitle.TabIndex = 1;
            this.lblFormTitle.Text = "Table Names for Generated Data are Listed Below.\r\n\r\nReview and modify, as needed," +
    " on this form.";
            this.lblFormTitle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSalesOrderHeadersTableName
            // 
            this.lblSalesOrderHeadersTableName.AutoSize = true;
            this.lblSalesOrderHeadersTableName.Location = new System.Drawing.Point(17, 100);
            this.lblSalesOrderHeadersTableName.Name = "lblSalesOrderHeadersTableName";
            this.lblSalesOrderHeadersTableName.Size = new System.Drawing.Size(105, 13);
            this.lblSalesOrderHeadersTableName.TabIndex = 2;
            this.lblSalesOrderHeadersTableName.Text = "Sales Order Headers";
            // 
            // lblSalesOrderDetailsTableName
            // 
            this.lblSalesOrderDetailsTableName.AutoSize = true;
            this.lblSalesOrderDetailsTableName.Location = new System.Drawing.Point(17, 131);
            this.lblSalesOrderDetailsTableName.Name = "lblSalesOrderDetailsTableName";
            this.lblSalesOrderDetailsTableName.Size = new System.Drawing.Size(97, 13);
            this.lblSalesOrderDetailsTableName.TabIndex = 4;
            this.lblSalesOrderDetailsTableName.Text = "Sales Order Details";
            // 
            // lblPurchaseOrderHeadersTableName
            // 
            this.lblPurchaseOrderHeadersTableName.AutoSize = true;
            this.lblPurchaseOrderHeadersTableName.Location = new System.Drawing.Point(17, 157);
            this.lblPurchaseOrderHeadersTableName.Name = "lblPurchaseOrderHeadersTableName";
            this.lblPurchaseOrderHeadersTableName.Size = new System.Drawing.Size(124, 13);
            this.lblPurchaseOrderHeadersTableName.TabIndex = 6;
            this.lblPurchaseOrderHeadersTableName.Text = "Purchase Order Headers";
            // 
            // blPurchaseOrderDetailsTableName
            // 
            this.blPurchaseOrderDetailsTableName.AutoSize = true;
            this.blPurchaseOrderDetailsTableName.Location = new System.Drawing.Point(17, 184);
            this.blPurchaseOrderDetailsTableName.Name = "blPurchaseOrderDetailsTableName";
            this.blPurchaseOrderDetailsTableName.Size = new System.Drawing.Size(116, 13);
            this.blPurchaseOrderDetailsTableName.TabIndex = 8;
            this.blPurchaseOrderDetailsTableName.Text = "Purchase Order Details";
            // 
            // lblDwInternetSalesTableName
            // 
            this.lblDwInternetSalesTableName.AutoSize = true;
            this.lblDwInternetSalesTableName.Location = new System.Drawing.Point(17, 210);
            this.lblDwInternetSalesTableName.Name = "lblDwInternetSalesTableName";
            this.lblDwInternetSalesTableName.Size = new System.Drawing.Size(159, 13);
            this.lblDwInternetSalesTableName.TabIndex = 10;
            this.lblDwInternetSalesTableName.Text = "Data Warehouse Internet Sales ";
            // 
            // lblDwResellerSalesTableName
            // 
            this.lblDwResellerSalesTableName.AutoSize = true;
            this.lblDwResellerSalesTableName.Location = new System.Drawing.Point(17, 238);
            this.lblDwResellerSalesTableName.Name = "lblDwResellerSalesTableName";
            this.lblDwResellerSalesTableName.Size = new System.Drawing.Size(158, 13);
            this.lblDwResellerSalesTableName.TabIndex = 12;
            this.lblDwResellerSalesTableName.Text = "Data Warehouse Reseller Sales";
            // 
            // txtDwResellerSalesTableName
            // 
            this.txtDwResellerSalesTableName.Location = new System.Drawing.Point(182, 238);
            this.txtDwResellerSalesTableName.Name = "txtDwResellerSalesTableName";
            this.txtDwResellerSalesTableName.Size = new System.Drawing.Size(255, 20);
            this.txtDwResellerSalesTableName.TabIndex = 13;
            // 
            // txtDwInternetSalesTableName
            // 
            this.txtDwInternetSalesTableName.Location = new System.Drawing.Point(182, 210);
            this.txtDwInternetSalesTableName.Name = "txtDwInternetSalesTableName";
            this.txtDwInternetSalesTableName.Size = new System.Drawing.Size(255, 20);
            this.txtDwInternetSalesTableName.TabIndex = 11;
            // 
            // txtPurchaseOrderDetailsTableName
            // 
            this.txtPurchaseOrderDetailsTableName.Location = new System.Drawing.Point(182, 184);
            this.txtPurchaseOrderDetailsTableName.Name = "txtPurchaseOrderDetailsTableName";
            this.txtPurchaseOrderDetailsTableName.Size = new System.Drawing.Size(255, 20);
            this.txtPurchaseOrderDetailsTableName.TabIndex = 9;
            // 
            // txtPurchaseOrderHeadersTableName
            // 
            this.txtPurchaseOrderHeadersTableName.Location = new System.Drawing.Point(182, 157);
            this.txtPurchaseOrderHeadersTableName.Name = "txtPurchaseOrderHeadersTableName";
            this.txtPurchaseOrderHeadersTableName.Size = new System.Drawing.Size(255, 20);
            this.txtPurchaseOrderHeadersTableName.TabIndex = 7;
            // 
            // txtSalesOrderDetailsTableName
            // 
            this.txtSalesOrderDetailsTableName.Location = new System.Drawing.Point(182, 128);
            this.txtSalesOrderDetailsTableName.Name = "txtSalesOrderDetailsTableName";
            this.txtSalesOrderDetailsTableName.Size = new System.Drawing.Size(255, 20);
            this.txtSalesOrderDetailsTableName.TabIndex = 5;
            // 
            // txtSalesOrderHeadersTableName
            // 
            this.txtSalesOrderHeadersTableName.Location = new System.Drawing.Point(182, 100);
            this.txtSalesOrderHeadersTableName.Name = "txtSalesOrderHeadersTableName";
            this.txtSalesOrderHeadersTableName.Size = new System.Drawing.Size(255, 20);
            this.txtSalesOrderHeadersTableName.TabIndex = 3;
            // 
            // lblSchemaWarning
            // 
            this.lblSchemaWarning.AutoSize = true;
            this.lblSchemaWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchemaWarning.Location = new System.Drawing.Point(179, 265);
            this.lblSchemaWarning.Name = "lblSchemaWarning";
            this.lblSchemaWarning.Size = new System.Drawing.Size(225, 39);
            this.lblSchemaWarning.TabIndex = 18;
            this.lblSchemaWarning.Text = "Enter table name in schema.tablename format,\r\nif required by database. For exampl" +
    "e: \r\ndbo.TestSalesOrderHeader";
            this.lblSchemaWarning.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTableSchema
            // 
            this.lblTableSchema.AutoSize = true;
            this.lblTableSchema.Location = new System.Drawing.Point(470, 147);
            this.lblTableSchema.Name = "lblTableSchema";
            this.lblTableSchema.Size = new System.Drawing.Size(76, 13);
            this.lblTableSchema.TabIndex = 19;
            this.lblTableSchema.Text = "Table Schema";
            // 
            // txtTableSchema
            // 
            this.txtTableSchema.Location = new System.Drawing.Point(463, 163);
            this.txtTableSchema.Name = "txtTableSchema";
            this.txtTableSchema.Size = new System.Drawing.Size(93, 20);
            this.txtTableSchema.TabIndex = 20;
            // 
            // EditTableNamesForm
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(575, 324);
            this.Controls.Add(this.txtTableSchema);
            this.Controls.Add(this.lblTableSchema);
            this.Controls.Add(this.lblSchemaWarning);
            this.Controls.Add(this.txtSalesOrderHeadersTableName);
            this.Controls.Add(this.txtSalesOrderDetailsTableName);
            this.Controls.Add(this.txtPurchaseOrderHeadersTableName);
            this.Controls.Add(this.txtPurchaseOrderDetailsTableName);
            this.Controls.Add(this.txtDwInternetSalesTableName);
            this.Controls.Add(this.txtDwResellerSalesTableName);
            this.Controls.Add(this.lblDwResellerSalesTableName);
            this.Controls.Add(this.lblDwInternetSalesTableName);
            this.Controls.Add(this.blPurchaseOrderDetailsTableName);
            this.Controls.Add(this.lblPurchaseOrderHeadersTableName);
            this.Controls.Add(this.lblSalesOrderDetailsTableName);
            this.Controls.Add(this.lblSalesOrderHeadersTableName);
            this.Controls.Add(this.lblFormTitle);
            this.Controls.Add(this.TableNamesMenu);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditTableNamesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Table Names";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TableNamesMenu.ResumeLayout(false);
            this.TableNamesMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.MenuStrip TableNamesMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.HelpProvider tableNamesHelpProvider;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolTip tableNamesFormToolTips;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblSalesOrderHeadersTableName;
        private System.Windows.Forms.Label lblSalesOrderDetailsTableName;
        private System.Windows.Forms.Label lblPurchaseOrderHeadersTableName;
        private System.Windows.Forms.Label blPurchaseOrderDetailsTableName;
        private System.Windows.Forms.Label lblDwInternetSalesTableName;
        private System.Windows.Forms.Label lblDwResellerSalesTableName;
        private System.Windows.Forms.TextBox txtDwResellerSalesTableName;
        private System.Windows.Forms.TextBox txtDwInternetSalesTableName;
        private System.Windows.Forms.TextBox txtPurchaseOrderDetailsTableName;
        private System.Windows.Forms.TextBox txtPurchaseOrderHeadersTableName;
        private System.Windows.Forms.TextBox txtSalesOrderDetailsTableName;
        private System.Windows.Forms.TextBox txtSalesOrderHeadersTableName;
        private System.Windows.Forms.Label lblSchemaWarning;
        private System.Windows.Forms.Label lblTableSchema;
        private System.Windows.Forms.TextBox txtTableSchema;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
    }
#pragma warning restore 1591
}

