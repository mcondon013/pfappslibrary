namespace PFAppDataForms
{
    /// <summary>
    /// Form class for visual line parse form.
    /// </summary>
    partial class PFFixedLenColDefsVisualParseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PFFixedLenColDefsVisualParseForm));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.textRuler = new RulerControl.RulerControl();
            this.chkColumnNamesOnFirstLine = new System.Windows.Forms.CheckBox();
            this.lblExpectedColumnWidth = new System.Windows.Forms.Label();
            this.txtExpectedLineWidth = new System.Windows.Forms.TextBox();
            this.toolTipTextParseForm = new System.Windows.Forms.ToolTip(this.components);
            this.cmdShowColumnOffsets = new System.Windows.Forms.Button();
            this.toolStripForVisualParser = new System.Windows.Forms.ToolStrip();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.toolStripForVisualParser.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(538, 162);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(98, 34);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.Location = new System.Drawing.Point(538, 31);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(98, 34);
            this.cmdAccept.TabIndex = 6;
            this.cmdAccept.Text = "&Accept";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // textRuler
            // 
            this.textRuler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textRuler.Location = new System.Drawing.Point(23, 64);
            this.textRuler.Name = "RulerControl";
            this.textRuler.Size = new System.Drawing.Size(490, 142);
            this.textRuler.TabIndex = 5;
            this.textRuler.TextScrollBars = System.Windows.Forms.ScrollBars.None;
            // 
            // chkColumnNamesOnFirstLine
            // 
            this.chkColumnNamesOnFirstLine.AutoCheck = false;
            this.chkColumnNamesOnFirstLine.AutoSize = true;
            this.chkColumnNamesOnFirstLine.Location = new System.Drawing.Point(23, 31);
            this.chkColumnNamesOnFirstLine.Name = "chkColumnNamesOnFirstLine";
            this.chkColumnNamesOnFirstLine.Size = new System.Drawing.Size(157, 17);
            this.chkColumnNamesOnFirstLine.TabIndex = 0;
            this.chkColumnNamesOnFirstLine.Text = "Column Names on First Line";
            this.toolTipTextParseForm.SetToolTip(this.chkColumnNamesOnFirstLine, "Check if data contains column headers on the first line.");
            this.chkColumnNamesOnFirstLine.UseVisualStyleBackColor = true;
            // 
            // lblExpectedColumnWidth
            // 
            this.lblExpectedColumnWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExpectedColumnWidth.AutoSize = true;
            this.lblExpectedColumnWidth.Location = new System.Drawing.Point(330, 35);
            this.lblExpectedColumnWidth.Name = "lblExpectedColumnWidth";
            this.lblExpectedColumnWidth.Size = new System.Drawing.Size(106, 13);
            this.lblExpectedColumnWidth.TabIndex = 2;
            this.lblExpectedColumnWidth.Text = "Expected Line Width";
            this.toolTipTextParseForm.SetToolTip(this.lblExpectedColumnWidth, "Number of characters and spaces expected on each line.");
            // 
            // txtExpectedLineWidth
            // 
            this.txtExpectedLineWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpectedLineWidth.BackColor = System.Drawing.SystemColors.Window;
            this.txtExpectedLineWidth.Location = new System.Drawing.Point(442, 31);
            this.txtExpectedLineWidth.Name = "txtExpectedLineWidth";
            this.txtExpectedLineWidth.ReadOnly = true;
            this.txtExpectedLineWidth.Size = new System.Drawing.Size(71, 20);
            this.txtExpectedLineWidth.TabIndex = 3;
            this.toolTipTextParseForm.SetToolTip(this.txtExpectedLineWidth, "Number of characters and spaces expected on each line.");
            // 
            // cmdShowColumnOffsets
            // 
            this.cmdShowColumnOffsets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdShowColumnOffsets.Location = new System.Drawing.Point(538, 95);
            this.cmdShowColumnOffsets.Name = "cmdShowColumnOffsets";
            this.cmdShowColumnOffsets.Size = new System.Drawing.Size(93, 35);
            this.cmdShowColumnOffsets.TabIndex = 4;
            this.cmdShowColumnOffsets.Text = "Show Column Offsets";
            this.toolTipTextParseForm.SetToolTip(this.cmdShowColumnOffsets, "Display the column start positions and lengths.");
            this.cmdShowColumnOffsets.UseVisualStyleBackColor = true;
            this.cmdShowColumnOffsets.Click += new System.EventHandler(this.cmdShowColumnOffsets_Click);
            // 
            // toolStripForVisualParser
            // 
            this.toolStripForVisualParser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarHelp});
            this.toolStripForVisualParser.Location = new System.Drawing.Point(0, 0);
            this.toolStripForVisualParser.Name = "toolStripForVisualParser";
            this.toolStripForVisualParser.Size = new System.Drawing.Size(657, 25);
            this.toolStripForVisualParser.TabIndex = 8;
            this.toolStripForVisualParser.Text = "toolStrip1";
            // 
            // toolbarHelp
            // 
            this.toolbarHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolbarHelp.Image")));
            this.toolbarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarHelp.Name = "toolbarHelp";
            this.toolbarHelp.Padding = new System.Windows.Forms.Padding(0, 0, 85, 0);
            this.toolbarHelp.Size = new System.Drawing.Size(105, 22);
            this.toolbarHelp.Text = "Help";
            this.toolbarHelp.Click += new System.EventHandler(this.toolbarHelp_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFAppDataForms\\InitWinFormsAppWithToolbar\\" +
    "InitWinFormsHelpFile.chm";
            // 
            // PFFixedLenColDefsVisualParseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 225);
            this.Controls.Add(this.toolStripForVisualParser);
            this.Controls.Add(this.cmdShowColumnOffsets);
            this.Controls.Add(this.txtExpectedLineWidth);
            this.Controls.Add(this.lblExpectedColumnWidth);
            this.Controls.Add(this.chkColumnNamesOnFirstLine);
            this.Controls.Add(this.textRuler);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.cmdCancel);
            this.Name = "PFFixedLenColDefsVisualParseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fixed Length Text File Visual Column Definition";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PFWindowsForm_FormClosing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.toolStripForVisualParser.ResumeLayout(false);
            this.toolStripForVisualParser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
        private RulerControl.RulerControl textRuler;
        private System.Windows.Forms.CheckBox chkColumnNamesOnFirstLine;
        private System.Windows.Forms.ToolTip toolTipTextParseForm;
        private System.Windows.Forms.Label lblExpectedColumnWidth;
        private System.Windows.Forms.TextBox txtExpectedLineWidth;
        private System.Windows.Forms.Button cmdShowColumnOffsets;
        private System.Windows.Forms.ToolStrip toolStripForVisualParser;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
        private System.Windows.Forms.HelpProvider appHelpProvider;
    }
}