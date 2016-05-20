namespace PFRandomDataProcessor
{
    partial class TEST_DataTableRandomizerColumnSpecForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TEST_DataTableRandomizerColumnSpecForm));
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
            this.mnuAccept = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.outputDataGridBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.DataGridBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblTotalNumberOfRows = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).BeginInit();
            this.inputDataGridMenu.SuspendLayout();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputDataGridBindingNavigator)).BeginInit();
            this.outputDataGridBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // inputDataGrid
            // 
            this.inputDataGrid.AllowUserToAddRows = false;
            this.inputDataGrid.AllowUserToDeleteRows = false;
            this.inputDataGrid.AllowUserToOrderColumns = true;
            this.inputDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.inputDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.inputDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputDataGrid.Location = new System.Drawing.Point(0, 25);
            this.inputDataGrid.Name = "inputDataGrid";
            this.inputDataGrid.ReadOnly = true;
            this.inputDataGrid.Size = new System.Drawing.Size(760, 478);
            this.inputDataGrid.TabIndex = 2;
            this.inputDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.inputDataGrid_CellContentClick);
            this.inputDataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.inputDataGrid_EditingControlShowing);
            // 
            // inputDataGridMenu
            // 
            this.inputDataGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGrid,
            this.mnuEdit,
            this.mnuAccept});
            this.inputDataGridMenu.Location = new System.Drawing.Point(0, 0);
            this.inputDataGridMenu.Name = "inputDataGridMenu";
            this.inputDataGridMenu.Size = new System.Drawing.Size(758, 24);
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
            this.panelFooter.Controls.Add(this.cmdCancel);
            this.panelFooter.Controls.Add(this.outputDataGridBindingNavigator);
            this.panelFooter.Controls.Add(this.lblTotalNumberOfRows);
            this.panelFooter.Location = new System.Drawing.Point(0, 506);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(758, 27);
            this.panelFooter.TabIndex = 4;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(665, 0);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(95, 27);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // outputDataGridBindingNavigator
            // 
            this.outputDataGridBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.outputDataGridBindingNavigator.BindingSource = this.DataGridBindingSource;
            this.outputDataGridBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.outputDataGridBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.outputDataGridBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.outputDataGridBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.outputDataGridBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.outputDataGridBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.outputDataGridBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.outputDataGridBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.outputDataGridBindingNavigator.Name = "outputDataGridBindingNavigator";
            this.outputDataGridBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.outputDataGridBindingNavigator.Size = new System.Drawing.Size(758, 25);
            this.outputDataGridBindingNavigator.TabIndex = 1;
            this.outputDataGridBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            this.bindingNavigatorAddNewItem.Visible = false;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            this.bindingNavigatorDeleteItem.Visible = false;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblTotalNumberOfRows
            // 
            this.lblTotalNumberOfRows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalNumberOfRows.AutoSize = true;
            this.lblTotalNumberOfRows.Location = new System.Drawing.Point(600, 2);
            this.lblTotalNumberOfRows.Name = "lblTotalNumberOfRows";
            this.lblTotalNumberOfRows.Size = new System.Drawing.Size(155, 13);
            this.lblTotalNumberOfRows.TabIndex = 0;
            this.lblTotalNumberOfRows.Text = "Total Number of Rows = #,##0";
            this.lblTotalNumberOfRows.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TEST_DataTableRandomizerColumnSpecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 530);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.inputDataGrid);
            this.Controls.Add(this.inputDataGridMenu);
            this.MainMenuStrip = this.inputDataGridMenu;
            this.Name = "TEST_DataTableRandomizerColumnSpecForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Data Randomizer Criteria ...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataTableRandomizerColumnSpecForm_FormClosing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Shown += new System.EventHandler(this.DataTableRandomizerColumnSpecForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.inputDataGrid)).EndInit();
            this.inputDataGridMenu.ResumeLayout(false);
            this.inputDataGridMenu.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputDataGridBindingNavigator)).EndInit();
            this.outputDataGridBindingNavigator.ResumeLayout(false);
            this.outputDataGridBindingNavigator.PerformLayout();
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
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.Label lblTotalNumberOfRows;
        internal System.Windows.Forms.BindingSource DataGridBindingSource;
        internal System.Windows.Forms.BindingNavigator outputDataGridBindingNavigator;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ToolStripMenuItem mnuGridAccept;
        private System.Windows.Forms.ToolStripSeparator acceptToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuAccept;
    }
}