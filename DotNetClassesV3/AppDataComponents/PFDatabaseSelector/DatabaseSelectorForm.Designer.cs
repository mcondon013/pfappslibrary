namespace PFDatabaseSelector
{
    /// <summary>
    /// Form for selecting which database platforms to enable.
    /// </summary>
    partial class DatabaseSelectorForm
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
            this.txtOutputBatchSize = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkReplaceExistingTable = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txtConnectionStringForOutputTable = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cboDatabaseTypeForOutputTable = new System.Windows.Forms.ComboBox();
            this.cmdBuildConnectionString = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.txtOutputTableName = new System.Windows.Forms.TextBox();
            this.formToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.lblSchemaWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(581, 230);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(98, 34);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(580, 55);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(98, 34);
            this.cmdOK.TabIndex = 1;
            this.cmdOK.Text = "&OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // txtOutputBatchSize
            // 
            this.txtOutputBatchSize.Location = new System.Drawing.Point(615, 161);
            this.txtOutputBatchSize.Name = "txtOutputBatchSize";
            this.txtOutputBatchSize.Size = new System.Drawing.Size(42, 20);
            this.txtOutputBatchSize.TabIndex = 23;
            this.formToolTips.SetToolTip(this.txtOutputBatchSize, "Set this number higher if output creation time is slow. Some database platforms d" +
        "o not recognize this parameter.");
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(578, 145);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(128, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Batch size for data writes:";
            // 
            // chkReplaceExistingTable
            // 
            this.chkReplaceExistingTable.AutoSize = true;
            this.chkReplaceExistingTable.Location = new System.Drawing.Point(580, 113);
            this.chkReplaceExistingTable.Name = "chkReplaceExistingTable";
            this.chkReplaceExistingTable.Size = new System.Drawing.Size(135, 17);
            this.chkReplaceExistingTable.TabIndex = 21;
            this.chkReplaceExistingTable.Text = "Replace Existing Table";
            this.formToolTips.SetToolTip(this.chkReplaceExistingTable, "Check this option to delete output table before writing to it.");
            this.chkReplaceExistingTable.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(256, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(217, 20);
            this.label16.TabIndex = 20;
            this.label16.Text = "Output to Database Table";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(27, 209);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(103, 13);
            this.label34.TabIndex = 19;
            this.label34.Text = "Output Table Name:";
            // 
            // txtConnectionStringForOutputTable
            // 
            this.txtConnectionStringForOutputTable.Location = new System.Drawing.Point(144, 109);
            this.txtConnectionStringForOutputTable.Multiline = true;
            this.txtConnectionStringForOutputTable.Name = "txtConnectionStringForOutputTable";
            this.txtConnectionStringForOutputTable.Size = new System.Drawing.Size(403, 71);
            this.txtConnectionStringForOutputTable.TabIndex = 17;
            this.formToolTips.SetToolTip(this.txtConnectionStringForOutputTable, "Connection string to use when connecting to the database.");
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(27, 112);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(115, 16);
            this.label20.TabIndex = 16;
            this.label20.Text = "Connection String:";
            // 
            // cboDatabaseTypeForOutputTable
            // 
            this.cboDatabaseTypeForOutputTable.FormattingEnabled = true;
            this.cboDatabaseTypeForOutputTable.Location = new System.Drawing.Point(147, 63);
            this.cboDatabaseTypeForOutputTable.Name = "cboDatabaseTypeForOutputTable";
            this.cboDatabaseTypeForOutputTable.Size = new System.Drawing.Size(400, 21);
            this.cboDatabaseTypeForOutputTable.TabIndex = 15;
            this.formToolTips.SetToolTip(this.cboDatabaseTypeForOutputTable, "Database platform to use for the output");
            this.cboDatabaseTypeForOutputTable.SelectedIndexChanged += new System.EventHandler(this.cboDatabaseTypeForOutputTable_SelectedIndexChanged);
            // 
            // cmdBuildConnectionString
            // 
            this.cmdBuildConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBuildConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBuildConnectionString.Location = new System.Drawing.Point(509, 90);
            this.cmdBuildConnectionString.Name = "cmdBuildConnectionString";
            this.cmdBuildConnectionString.Size = new System.Drawing.Size(38, 20);
            this.cmdBuildConnectionString.TabIndex = 18;
            this.cmdBuildConnectionString.Text = "•••";
            this.formToolTips.SetToolTip(this.cmdBuildConnectionString, "Build Connection String Interactive UI");
            this.cmdBuildConnectionString.UseVisualStyleBackColor = true;
            this.cmdBuildConnectionString.Click += new System.EventHandler(this.cmdBuildConnectionString_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(27, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(106, 16);
            this.label19.TabIndex = 14;
            this.label19.Text = "Database Type:";
            // 
            // txtOutputTableName
            // 
            this.txtOutputTableName.Location = new System.Drawing.Point(141, 206);
            this.txtOutputTableName.Name = "txtOutputTableName";
            this.txtOutputTableName.Size = new System.Drawing.Size(406, 20);
            this.txtOutputTableName.TabIndex = 24;
            this.formToolTips.SetToolTip(this.txtOutputTableName, "Name of the output table.");
            // 
            // lblSchemaWarning
            // 
            this.lblSchemaWarning.AutoSize = true;
            this.lblSchemaWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSchemaWarning.Location = new System.Drawing.Point(144, 238);
            this.lblSchemaWarning.Name = "lblSchemaWarning";
            this.lblSchemaWarning.Size = new System.Drawing.Size(344, 26);
            this.lblSchemaWarning.TabIndex = 25;
            this.lblSchemaWarning.Text = "Enter table name in schema.tablename format, if required by database. \r\nFor examp" +
    "le: dbo.OutputTable, hr.Employee, dba.OutputDataTable etc.";
            // 
            // DatabaseSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 288);
            this.Controls.Add(this.lblSchemaWarning);
            this.Controls.Add(this.txtOutputTableName);
            this.Controls.Add(this.txtOutputBatchSize);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.chkReplaceExistingTable);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtConnectionStringForOutputTable);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.cboDatabaseTypeForOutputTable);
            this.Controls.Add(this.cmdBuildConnectionString);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "DatabaseSelectorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Output to Database Table";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PFWindowsForm_FormClosing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.TextBox txtOutputBatchSize;
        private System.Windows.Forms.Label label11;
        internal System.Windows.Forms.CheckBox chkReplaceExistingTable;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label34;
        internal System.Windows.Forms.TextBox txtConnectionStringForOutputTable;
        private System.Windows.Forms.Label label20;
        internal System.Windows.Forms.ComboBox cboDatabaseTypeForOutputTable;
        private System.Windows.Forms.Button cmdBuildConnectionString;
        private System.Windows.Forms.Label label19;
        internal System.Windows.Forms.TextBox txtOutputTableName;
        private System.Windows.Forms.ToolTip formToolTips;
        private System.Windows.Forms.Label lblSchemaWarning;
    }
}