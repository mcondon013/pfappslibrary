namespace PFRandomDataForms
{
    partial class DataTableRandomizerColumnSpecForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataTableRandomizerColumnSpecForm));
            this.inputDataGrid = new System.Windows.Forms.DataGridView();
            this.inputDataGridMenu = new System.Windows.Forms.MenuStrip();
            this.mnuGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.acceptToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGridPageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGridCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRandomizer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRandomizerSources = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.panelGridNavigator = new System.Windows.Forms.Panel();
            this.txtTotalRowCount = new System.Windows.Forms.TextBox();
            this.txtCurrentRowNumber = new System.Windows.Forms.TextBox();
            this.cmdNextGridRow = new System.Windows.Forms.Button();
            this.cmdPrevGridRow = new System.Windows.Forms.Button();
            this.cmdLastGridRow = new System.Windows.Forms.Button();
            this.cmdFirstGridRow = new System.Windows.Forms.Button();
            this.cmdEraseAll = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.DataGridBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpMappingRandomValues = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).BeginInit();
            this.inputDataGridMenu.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelGridNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // inputDataGrid
            // 
            this.inputDataGrid.AllowUserToAddRows = false;
            this.inputDataGrid.AllowUserToDeleteRows = false;
            this.inputDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.inputDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.inputDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputDataGrid.Location = new System.Drawing.Point(0, 25);
            this.inputDataGrid.Name = "inputDataGrid";
            this.inputDataGrid.ReadOnly = true;
            this.inputDataGrid.Size = new System.Drawing.Size(945, 478);
            this.inputDataGrid.TabIndex = 2;
            this.inputDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.inputDataGrid_CellContentClick);
            this.inputDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.inputDataGrid_DataError);
            this.inputDataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.inputDataGrid_EditingControlShowing);
            this.inputDataGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.inputDataGrid_RowEnter);
            this.inputDataGrid.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.inputDataGrid_RowLeave);
            // 
            // inputDataGridMenu
            // 
            this.inputDataGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGrid,
            this.mnuEdit,
            this.mnuRandomizer,
            this.mnuAccept,
            this.mnuHelp});
            this.inputDataGridMenu.Location = new System.Drawing.Point(0, 0);
            this.inputDataGridMenu.Name = "inputDataGridMenu";
            this.inputDataGridMenu.Size = new System.Drawing.Size(943, 24);
            this.inputDataGridMenu.TabIndex = 3;
            this.inputDataGridMenu.Text = "menuStrip1";
            // 
            // mnuGrid
            // 
            this.mnuGrid.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridAccept,
            this.acceptToolStripSeparator,
            this.mnuGridPageSetup,
            this.mnuGridPrintPreview,
            this.mnuGridPrint,
            this.toolStripSeparator2,
            this.mnuGridCancel});
            this.mnuGrid.Name = "mnuGrid";
            this.mnuGrid.Size = new System.Drawing.Size(41, 20);
            this.mnuGrid.Text = "Grid";
            this.mnuGrid.ToolTipText = "Copy app settings to various file formats.";
            // 
            // mnuGridAccept
            // 
            this.mnuGridAccept.Name = "mnuGridAccept";
            this.mnuGridAccept.Size = new System.Drawing.Size(143, 22);
            this.mnuGridAccept.Text = "&Accept";
            this.mnuGridAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // acceptToolStripSeparator
            // 
            this.acceptToolStripSeparator.Name = "acceptToolStripSeparator";
            this.acceptToolStripSeparator.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuGridPageSetup
            // 
            this.mnuGridPageSetup.Name = "mnuGridPageSetup";
            this.mnuGridPageSetup.Size = new System.Drawing.Size(143, 22);
            this.mnuGridPageSetup.Text = "Page Setup";
            this.mnuGridPageSetup.ToolTipText = "Modify page settings for printer output";
            this.mnuGridPageSetup.Click += new System.EventHandler(this.mnuGridPageSetup_Click);
            // 
            // mnuGridPrintPreview
            // 
            this.mnuGridPrintPreview.Name = "mnuGridPrintPreview";
            this.mnuGridPrintPreview.Size = new System.Drawing.Size(143, 22);
            this.mnuGridPrintPreview.Text = "Print Pre&view";
            this.mnuGridPrintPreview.ToolTipText = "Show the grid in print preview mode";
            this.mnuGridPrintPreview.Click += new System.EventHandler(this.mnuGridPrintPreview_Click);
            // 
            // mnuGridPrint
            // 
            this.mnuGridPrint.Name = "mnuGridPrint";
            this.mnuGridPrint.Size = new System.Drawing.Size(143, 22);
            this.mnuGridPrint.Text = "Print";
            this.mnuGridPrint.ToolTipText = "Output contents of grid to printer";
            this.mnuGridPrint.Click += new System.EventHandler(this.mnuGridPrint_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuGridCancel
            // 
            this.mnuGridCancel.Name = "mnuGridCancel";
            this.mnuGridCancel.Size = new System.Drawing.Size(143, 22);
            this.mnuGridCancel.Text = "&Cancel";
            this.mnuGridCancel.ToolTipText = "Close this form and cancel and pending updates.";
            this.mnuGridCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCopy});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(102, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuGridEditCopy_Click);
            // 
            // mnuRandomizer
            // 
            this.mnuRandomizer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRandomizerSources});
            this.mnuRandomizer.Name = "mnuRandomizer";
            this.mnuRandomizer.Size = new System.Drawing.Size(82, 20);
            this.mnuRandomizer.Text = "&Randomizer";
            // 
            // mnuRandomizerSources
            // 
            this.mnuRandomizerSources.Name = "mnuRandomizerSources";
            this.mnuRandomizerSources.Size = new System.Drawing.Size(161, 22);
            this.mnuRandomizerSources.Text = "Manage &Sources";
            this.mnuRandomizerSources.Click += new System.EventHandler(this.mnuRandomizerSources_Click);
            // 
            // mnuAccept
            // 
            this.mnuAccept.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuAccept.BackColor = System.Drawing.SystemColors.Control;
            this.mnuAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuAccept.Name = "mnuAccept";
            this.mnuAccept.Size = new System.Drawing.Size(68, 20);
            this.mnuAccept.Text = "&Accept";
            this.mnuAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.Controls.Add(this.panelGridNavigator);
            this.panelFooter.Controls.Add(this.cmdEraseAll);
            this.panelFooter.Controls.Add(this.cmdCancel);
            this.panelFooter.Location = new System.Drawing.Point(0, 506);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(943, 27);
            this.panelFooter.TabIndex = 4;
            // 
            // panelGridNavigator
            // 
            this.panelGridNavigator.Controls.Add(this.txtTotalRowCount);
            this.panelGridNavigator.Controls.Add(this.txtCurrentRowNumber);
            this.panelGridNavigator.Controls.Add(this.cmdNextGridRow);
            this.panelGridNavigator.Controls.Add(this.cmdPrevGridRow);
            this.panelGridNavigator.Controls.Add(this.cmdLastGridRow);
            this.panelGridNavigator.Controls.Add(this.cmdFirstGridRow);
            this.panelGridNavigator.Location = new System.Drawing.Point(0, 0);
            this.panelGridNavigator.Name = "panelGridNavigator";
            this.panelGridNavigator.Size = new System.Drawing.Size(252, 27);
            this.panelGridNavigator.TabIndex = 6;
            // 
            // txtTotalRowCount
            // 
            this.txtTotalRowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRowCount.Location = new System.Drawing.Point(122, 0);
            this.txtTotalRowCount.Multiline = true;
            this.txtTotalRowCount.Name = "txtTotalRowCount";
            this.txtTotalRowCount.ReadOnly = true;
            this.txtTotalRowCount.Size = new System.Drawing.Size(76, 25);
            this.txtTotalRowCount.TabIndex = 7;
            this.txtTotalRowCount.Text = "of ? rows";
            // 
            // txtCurrentRowNumber
            // 
            this.txtCurrentRowNumber.BackColor = System.Drawing.SystemColors.Window;
            this.txtCurrentRowNumber.Location = new System.Drawing.Point(54, 0);
            this.txtCurrentRowNumber.Multiline = true;
            this.txtCurrentRowNumber.Name = "txtCurrentRowNumber";
            this.txtCurrentRowNumber.ReadOnly = true;
            this.txtCurrentRowNumber.Size = new System.Drawing.Size(62, 25);
            this.txtCurrentRowNumber.TabIndex = 6;
            // 
            // cmdNextGridRow
            // 
            this.cmdNextGridRow.Font = new System.Drawing.Font("Lucida Console", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNextGridRow.Location = new System.Drawing.Point(199, 0);
            this.cmdNextGridRow.Name = "cmdNextGridRow";
            this.cmdNextGridRow.Size = new System.Drawing.Size(27, 25);
            this.cmdNextGridRow.TabIndex = 6;
            this.cmdNextGridRow.Text = "►";
            this.cmdNextGridRow.UseVisualStyleBackColor = true;
            this.cmdNextGridRow.Click += new System.EventHandler(this.cmdNextGridRow_Click);
            // 
            // cmdPrevGridRow
            // 
            this.cmdPrevGridRow.Font = new System.Drawing.Font("Lucida Console", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdPrevGridRow.Location = new System.Drawing.Point(25, 0);
            this.cmdPrevGridRow.Name = "cmdPrevGridRow";
            this.cmdPrevGridRow.Size = new System.Drawing.Size(27, 25);
            this.cmdPrevGridRow.TabIndex = 6;
            this.cmdPrevGridRow.Text = "◄";
            this.cmdPrevGridRow.UseVisualStyleBackColor = true;
            this.cmdPrevGridRow.Click += new System.EventHandler(this.cmdPrevGridRow_Click);
            // 
            // cmdLastGridRow
            // 
            this.cmdLastGridRow.Font = new System.Drawing.Font("Lucida Console", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdLastGridRow.Location = new System.Drawing.Point(224, 0);
            this.cmdLastGridRow.Name = "cmdLastGridRow";
            this.cmdLastGridRow.Size = new System.Drawing.Size(27, 25);
            this.cmdLastGridRow.TabIndex = 6;
            this.cmdLastGridRow.Text = "►▌";
            this.cmdLastGridRow.UseVisualStyleBackColor = true;
            this.cmdLastGridRow.Click += new System.EventHandler(this.cmdLastGridRow_Click);
            // 
            // cmdFirstGridRow
            // 
            this.cmdFirstGridRow.Font = new System.Drawing.Font("Lucida Console", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFirstGridRow.Location = new System.Drawing.Point(0, 0);
            this.cmdFirstGridRow.Name = "cmdFirstGridRow";
            this.cmdFirstGridRow.Size = new System.Drawing.Size(27, 25);
            this.cmdFirstGridRow.TabIndex = 6;
            this.cmdFirstGridRow.Text = "▌◄";
            this.cmdFirstGridRow.UseVisualStyleBackColor = true;
            this.cmdFirstGridRow.Click += new System.EventHandler(this.cmdFirstGridRow_Click);
            // 
            // cmdEraseAll
            // 
            this.cmdEraseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEraseAll.FlatAppearance.BorderSize = 0;
            this.cmdEraseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdEraseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEraseAll.Location = new System.Drawing.Point(682, 0);
            this.cmdEraseAll.Name = "cmdEraseAll";
            this.cmdEraseAll.Size = new System.Drawing.Size(95, 27);
            this.cmdEraseAll.TabIndex = 5;
            this.cmdEraseAll.Text = "Erase All";
            this.cmdEraseAll.UseVisualStyleBackColor = true;
            this.cmdEraseAll.Click += new System.EventHandler(this.cmdEraseAll_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.FlatAppearance.BorderSize = 0;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(845, 0);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(95, 27);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFRandomDataForms\\InitWinFormsAppWithToolb" +
    "ar\\InitWinFormsHelpFile.chm";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpMappingRandomValues});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpMappingRandomValues
            // 
            this.mnuHelpMappingRandomValues.Name = "mnuHelpMappingRandomValues";
            this.mnuHelpMappingRandomValues.Size = new System.Drawing.Size(207, 22);
            this.mnuHelpMappingRandomValues.Text = "&Mapping Random Values";
            this.mnuHelpMappingRandomValues.Click += new System.EventHandler(this.mnuHelpMappingRandomValues_Click);
            // 
            // DataTableRandomizerColumnSpecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 530);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.inputDataGrid);
            this.Controls.Add(this.inputDataGridMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.inputDataGridMenu;
            this.Name = "DataTableRandomizerColumnSpecForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map Random Values to Data Columns ...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataTableRandomizerColumnSpecForm_FormClosing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Shown += new System.EventHandler(this.DataTableRandomizerColumnSpecForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).EndInit();
            this.inputDataGridMenu.ResumeLayout(false);
            this.inputDataGridMenu.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelGridNavigator.ResumeLayout(false);
            this.panelGridNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView inputDataGrid;
        private System.Windows.Forms.MenuStrip inputDataGridMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuGridPageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuGridPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem mnuGridPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuGridCancel;
        private System.Windows.Forms.Panel panelFooter;
        internal System.Windows.Forms.BindingSource DataGridBindingSource;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuGridAccept;
        private System.Windows.Forms.ToolStripSeparator acceptToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuAccept;
        private System.Windows.Forms.ToolStripMenuItem mnuRandomizer;
        private System.Windows.Forms.ToolStripMenuItem mnuRandomizerSources;
        private System.Windows.Forms.Button cmdEraseAll;
        private System.Windows.Forms.Panel panelGridNavigator;
        private System.Windows.Forms.TextBox txtTotalRowCount;
        private System.Windows.Forms.TextBox txtCurrentRowNumber;
        private System.Windows.Forms.Button cmdNextGridRow;
        private System.Windows.Forms.Button cmdPrevGridRow;
        private System.Windows.Forms.Button cmdLastGridRow;
        private System.Windows.Forms.Button cmdFirstGridRow;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpMappingRandomValues;
        private System.Windows.Forms.HelpProvider appHelpProvider;
    }
}