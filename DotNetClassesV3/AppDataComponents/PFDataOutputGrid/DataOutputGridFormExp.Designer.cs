namespace PFDataOutputGrid
{
    partial class DataOutputGridFormExp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataOutputGridFormExp));
            this.outputDataGrid = new System.Windows.Forms.DataGridView();
            this.outputDataGridMenu = new System.Windows.Forms.MenuStrip();
            this.mnuGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportTo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToExcelFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToExcelFileXLSXFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToExcelFileXLSFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToExcelFileCSVFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToTextFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToTextFileDelimitedFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToTextFileFixedLengthFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToAccessFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToAccessFileAccdbFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToAccessFileMdbFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToWordFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToWordFileDocxFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToWordFileDocFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToRtfFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToPdfFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridExportToXmlFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGridExportToDatabaseTable = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGridPageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridPrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGridExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGridEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.txtExportFinishedMessage = new System.Windows.Forms.TextBox();
            this.cmdExit = new System.Windows.Forms.Button();
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
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.outputDataGrid)).BeginInit();
            this.outputDataGridMenu.SuspendLayout();
            this.panelFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputDataGridBindingNavigator)).BeginInit();
            this.outputDataGridBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // outputDataGrid
            // 
            this.outputDataGrid.AllowUserToAddRows = false;
            this.outputDataGrid.AllowUserToDeleteRows = false;
            this.outputDataGrid.AllowUserToOrderColumns = true;
            this.outputDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.outputDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputDataGrid.Location = new System.Drawing.Point(0, 25);
            this.outputDataGrid.Name = "outputDataGrid";
            this.outputDataGrid.ReadOnly = true;
            this.outputDataGrid.Size = new System.Drawing.Size(711, 415);
            this.outputDataGrid.TabIndex = 2;
            this.outputDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.outputDataGrid_DataError);
            // 
            // outputDataGridMenu
            // 
            this.outputDataGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGrid,
            this.mnuGridEdit,
            this.toolbarHelp});
            this.outputDataGridMenu.Location = new System.Drawing.Point(0, 0);
            this.outputDataGridMenu.Name = "outputDataGridMenu";
            this.outputDataGridMenu.Size = new System.Drawing.Size(709, 27);
            this.outputDataGridMenu.TabIndex = 3;
            this.outputDataGridMenu.Text = "menuStrip1";
            // 
            // mnuGrid
            // 
            this.mnuGrid.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridExportTo,
            this.exportMenuToolStripSeparator,
            this.mnuGridPageSetup,
            this.mnuGridPrintPreview,
            this.mnuGridPrint,
            this.toolStripSeparator2,
            this.mnuGridExit});
            this.mnuGrid.Name = "mnuGrid";
            this.mnuGrid.Size = new System.Drawing.Size(41, 23);
            this.mnuGrid.Text = "&Grid";
            this.mnuGrid.ToolTipText = "Copy app settings to various file formats.";
            // 
            // mnuGridExportTo
            // 
            this.mnuGridExportTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridExportToExcelFile,
            this.mnuGridExportToTextFile,
            this.mnuGridExportToAccessFile,
            this.mnuGridExportToWordFile,
            this.mnuGridExportToRtfFile,
            this.mnuGridExportToPdfFile,
            this.mnuGridExportToXmlFile,
            this.toolStripSeparator1,
            this.mnuGridExportToDatabaseTable});
            this.mnuGridExportTo.Name = "mnuGridExportTo";
            this.mnuGridExportTo.Size = new System.Drawing.Size(143, 22);
            this.mnuGridExportTo.Text = "&Export To";
            this.mnuGridExportTo.ToolTipText = "Export settings from grid to a file";
            // 
            // mnuGridExportToExcelFile
            // 
            this.mnuGridExportToExcelFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridExportToExcelFileXLSXFormat,
            this.mnuGridExportToExcelFileXLSFormat,
            this.mnuGridExportToExcelFileCSVFormat});
            this.mnuGridExportToExcelFile.Name = "mnuGridExportToExcelFile";
            this.mnuGridExportToExcelFile.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToExcelFile.Text = "&Excel File";
            // 
            // mnuGridExportToExcelFileXLSXFormat
            // 
            this.mnuGridExportToExcelFileXLSXFormat.Name = "mnuGridExportToExcelFileXLSXFormat";
            this.mnuGridExportToExcelFileXLSXFormat.Size = new System.Drawing.Size(205, 22);
            this.mnuGridExportToExcelFileXLSXFormat.Text = "XLSX Format (Excel 2007)";
            this.mnuGridExportToExcelFileXLSXFormat.Click += new System.EventHandler(this.mnuGridExportToExcelFileXLSXFormat_Click);
            // 
            // mnuGridExportToExcelFileXLSFormat
            // 
            this.mnuGridExportToExcelFileXLSFormat.Name = "mnuGridExportToExcelFileXLSFormat";
            this.mnuGridExportToExcelFileXLSFormat.Size = new System.Drawing.Size(205, 22);
            this.mnuGridExportToExcelFileXLSFormat.Text = "XLS Format (Excel 2003)";
            this.mnuGridExportToExcelFileXLSFormat.Click += new System.EventHandler(this.mnuGridExportToExcelFileXLSFormat_Click);
            // 
            // mnuGridExportToExcelFileCSVFormat
            // 
            this.mnuGridExportToExcelFileCSVFormat.Name = "mnuGridExportToExcelFileCSVFormat";
            this.mnuGridExportToExcelFileCSVFormat.Size = new System.Drawing.Size(205, 22);
            this.mnuGridExportToExcelFileCSVFormat.Text = "CSV Format";
            this.mnuGridExportToExcelFileCSVFormat.Click += new System.EventHandler(this.mnuGridExportToExcelFileCSVFormat_Click);
            // 
            // mnuGridExportToTextFile
            // 
            this.mnuGridExportToTextFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridExportToTextFileDelimitedFormat,
            this.mnuGridExportToTextFileFixedLengthFormat});
            this.mnuGridExportToTextFile.Name = "mnuGridExportToTextFile";
            this.mnuGridExportToTextFile.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToTextFile.Text = "&Text File";
            // 
            // mnuGridExportToTextFileDelimitedFormat
            // 
            this.mnuGridExportToTextFileDelimitedFormat.Name = "mnuGridExportToTextFileDelimitedFormat";
            this.mnuGridExportToTextFileDelimitedFormat.Size = new System.Drawing.Size(182, 22);
            this.mnuGridExportToTextFileDelimitedFormat.Text = "Delimited Format";
            this.mnuGridExportToTextFileDelimitedFormat.Click += new System.EventHandler(this.mnuGridExportToTextFileDelimitedFormat_Click);
            // 
            // mnuGridExportToTextFileFixedLengthFormat
            // 
            this.mnuGridExportToTextFileFixedLengthFormat.Name = "mnuGridExportToTextFileFixedLengthFormat";
            this.mnuGridExportToTextFileFixedLengthFormat.Size = new System.Drawing.Size(182, 22);
            this.mnuGridExportToTextFileFixedLengthFormat.Text = "Fixed Length Format";
            this.mnuGridExportToTextFileFixedLengthFormat.Click += new System.EventHandler(this.mnuGridExportToTextFileFixedLengthFormat_Click);
            // 
            // mnuGridExportToAccessFile
            // 
            this.mnuGridExportToAccessFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridExportToAccessFileAccdbFormat,
            this.mnuGridExportToAccessFileMdbFormat});
            this.mnuGridExportToAccessFile.Name = "mnuGridExportToAccessFile";
            this.mnuGridExportToAccessFile.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToAccessFile.Text = "&Access File";
            // 
            // mnuGridExportToAccessFileAccdbFormat
            // 
            this.mnuGridExportToAccessFileAccdbFormat.Name = "mnuGridExportToAccessFileAccdbFormat";
            this.mnuGridExportToAccessFileAccdbFormat.Size = new System.Drawing.Size(223, 22);
            this.mnuGridExportToAccessFileAccdbFormat.Text = "Accdb Format (Access 2007)";
            this.mnuGridExportToAccessFileAccdbFormat.Click += new System.EventHandler(this.mnuGridExportToAccessFileAccdbFormat_Click);
            // 
            // mnuGridExportToAccessFileMdbFormat
            // 
            this.mnuGridExportToAccessFileMdbFormat.Name = "mnuGridExportToAccessFileMdbFormat";
            this.mnuGridExportToAccessFileMdbFormat.Size = new System.Drawing.Size(223, 22);
            this.mnuGridExportToAccessFileMdbFormat.Text = "MDB Format (Access 2003)";
            this.mnuGridExportToAccessFileMdbFormat.Click += new System.EventHandler(this.mnuGridExportToAccessFileMdbFormat_Click);
            // 
            // mnuGridExportToWordFile
            // 
            this.mnuGridExportToWordFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridExportToWordFileDocxFormat,
            this.mnuGridExportToWordFileDocFormat});
            this.mnuGridExportToWordFile.Name = "mnuGridExportToWordFile";
            this.mnuGridExportToWordFile.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToWordFile.Text = "&Word File";
            // 
            // mnuGridExportToWordFileDocxFormat
            // 
            this.mnuGridExportToWordFileDocxFormat.Name = "mnuGridExportToWordFileDocxFormat";
            this.mnuGridExportToWordFileDocxFormat.Size = new System.Drawing.Size(214, 22);
            this.mnuGridExportToWordFileDocxFormat.Text = "DOCX Format (Word 2007)";
            this.mnuGridExportToWordFileDocxFormat.Click += new System.EventHandler(this.mnuGridExportToWordFileDocxFormat_Click);
            // 
            // mnuGridExportToWordFileDocFormat
            // 
            this.mnuGridExportToWordFileDocFormat.Name = "mnuGridExportToWordFileDocFormat";
            this.mnuGridExportToWordFileDocFormat.Size = new System.Drawing.Size(214, 22);
            this.mnuGridExportToWordFileDocFormat.Text = "DOC Format (Word 2003)";
            this.mnuGridExportToWordFileDocFormat.Click += new System.EventHandler(this.mnuGridExportToWordFileDocFormat_Click);
            // 
            // mnuGridExportToRtfFile
            // 
            this.mnuGridExportToRtfFile.Name = "mnuGridExportToRtfFile";
            this.mnuGridExportToRtfFile.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToRtfFile.Text = "&RTF FIle";
            this.mnuGridExportToRtfFile.Click += new System.EventHandler(this.mnuGridExportToRtfFile_Click);
            // 
            // mnuGridExportToPdfFile
            // 
            this.mnuGridExportToPdfFile.Name = "mnuGridExportToPdfFile";
            this.mnuGridExportToPdfFile.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToPdfFile.Text = "&PDF File";
            this.mnuGridExportToPdfFile.Click += new System.EventHandler(this.mnuGridExportToPdfFile_Click);
            // 
            // mnuGridExportToXmlFile
            // 
            this.mnuGridExportToXmlFile.Name = "mnuGridExportToXmlFile";
            this.mnuGridExportToXmlFile.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToXmlFile.Text = "&Xml File";
            this.mnuGridExportToXmlFile.ToolTipText = "Export settings to an Xml formatted file.";
            this.mnuGridExportToXmlFile.Click += new System.EventHandler(this.mnuGridExportToXmlFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuGridExportToDatabaseTable
            // 
            this.mnuGridExportToDatabaseTable.Name = "mnuGridExportToDatabaseTable";
            this.mnuGridExportToDatabaseTable.Size = new System.Drawing.Size(154, 22);
            this.mnuGridExportToDatabaseTable.Text = "Database Table";
            this.mnuGridExportToDatabaseTable.Click += new System.EventHandler(this.mnuGridExportToDatabaseTable_Click);
            // 
            // exportMenuToolStripSeparator
            // 
            this.exportMenuToolStripSeparator.Name = "exportMenuToolStripSeparator";
            this.exportMenuToolStripSeparator.Size = new System.Drawing.Size(140, 6);
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
            // mnuGridExit
            // 
            this.mnuGridExit.Name = "mnuGridExit";
            this.mnuGridExit.Size = new System.Drawing.Size(143, 22);
            this.mnuGridExit.Text = "E&xit";
            this.mnuGridExit.ToolTipText = "Close this form and cancel and pending updates.";
            this.mnuGridExit.Click += new System.EventHandler(this.mnuGridExit_Click);
            // 
            // mnuGridEdit
            // 
            this.mnuGridEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGridEditCopy});
            this.mnuGridEdit.Name = "mnuGridEdit";
            this.mnuGridEdit.Size = new System.Drawing.Size(39, 23);
            this.mnuGridEdit.Text = "&Edit";
            // 
            // mnuGridEditCopy
            // 
            this.mnuGridEditCopy.Name = "mnuGridEditCopy";
            this.mnuGridEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuGridEditCopy.Size = new System.Drawing.Size(144, 22);
            this.mnuGridEditCopy.Text = "&Copy";
            this.mnuGridEditCopy.Click += new System.EventHandler(this.mnuGridEditCopy_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.Controls.Add(this.txtExportFinishedMessage);
            this.panelFooter.Controls.Add(this.cmdExit);
            this.panelFooter.Controls.Add(this.outputDataGridBindingNavigator);
            this.panelFooter.Controls.Add(this.lblTotalNumberOfRows);
            this.panelFooter.Location = new System.Drawing.Point(0, 443);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(709, 27);
            this.panelFooter.TabIndex = 4;
            // 
            // txtExportFinishedMessage
            // 
            this.txtExportFinishedMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExportFinishedMessage.BackColor = System.Drawing.SystemColors.Control;
            this.txtExportFinishedMessage.Location = new System.Drawing.Point(303, 2);
            this.txtExportFinishedMessage.Name = "txtExportFinishedMessage";
            this.txtExportFinishedMessage.ReadOnly = true;
            this.txtExportFinishedMessage.Size = new System.Drawing.Size(305, 20);
            this.txtExportFinishedMessage.TabIndex = 3;
            // 
            // cmdExit
            // 
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.Location = new System.Drawing.Point(614, -2);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(95, 27);
            this.cmdExit.TabIndex = 2;
            this.cmdExit.Text = "E&xit";
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
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
            this.outputDataGridBindingNavigator.Size = new System.Drawing.Size(709, 25);
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
            this.lblTotalNumberOfRows.Location = new System.Drawing.Point(551, 2);
            this.lblTotalNumberOfRows.Name = "lblTotalNumberOfRows";
            this.lblTotalNumberOfRows.Size = new System.Drawing.Size(155, 13);
            this.lblTotalNumberOfRows.TabIndex = 0;
            this.lblTotalNumberOfRows.Text = "Total Number of Rows = #,##0";
            this.lblTotalNumberOfRows.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFRandomDataForms\\InitWinFormsAppWithToolb" +
    "ar\\InitWinFormsHelpFile.chm";
            // 
            // toolbarHelp
            // 
            this.toolbarHelp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolbarHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolbarHelp.Image")));
            this.toolbarHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarHelp.Name = "toolbarHelp";
            this.toolbarHelp.Padding = new System.Windows.Forms.Padding(0, 0, 85, 0);
            this.toolbarHelp.Size = new System.Drawing.Size(105, 20);
            this.toolbarHelp.Text = "Help";
            this.toolbarHelp.Click += new System.EventHandler(this.toolbarHelp_Click);
            // 
            // DataOutputGridFormExp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 467);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.outputDataGrid);
            this.Controls.Add(this.outputDataGridMenu);
            this.MainMenuStrip = this.outputDataGridMenu;
            this.Name = "DataOutputGridFormExp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataOutputGridForm";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Shown += new System.EventHandler(this.DataOutputGridFormExp_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.outputDataGrid)).EndInit();
            this.outputDataGridMenu.ResumeLayout(false);
            this.outputDataGridMenu.PerformLayout();
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

#pragma warning disable 1591
        public System.Windows.Forms.DataGridView outputDataGrid;
        private System.Windows.Forms.MenuStrip outputDataGridMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportTo;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToRtfFile;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToXmlFile;
        private System.Windows.Forms.ToolStripSeparator exportMenuToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem mnuGridPageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuGridPrintPreview;
        private System.Windows.Forms.ToolStripMenuItem mnuGridPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExit;
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
        public System.Windows.Forms.BindingSource DataGridBindingSource;
        public System.Windows.Forms.BindingNavigator outputDataGridBindingNavigator;
        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToExcelFile;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToExcelFileXLSXFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToExcelFileXLSFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToExcelFileCSVFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToTextFile;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToTextFileDelimitedFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToTextFileFixedLengthFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToAccessFile;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToAccessFileAccdbFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToAccessFileMdbFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToWordFile;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToWordFileDocxFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToWordFileDocFormat;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToPdfFile;
        private System.Windows.Forms.ToolStripMenuItem mnuGridEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuGridEditCopy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuGridExportToDatabaseTable;
        private System.Windows.Forms.TextBox txtExportFinishedMessage;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
#pragma warning restore 1591
    }
}