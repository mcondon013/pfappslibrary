namespace PFAppDataForms
{
    partial class PFFilterBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PFFilterBuilderForm));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.mainMenuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainMenuContextMenuRunTest = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.mainMenuOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenuToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.cmdMoveItemUp = new System.Windows.Forms.Button();
            this.cmdMoveItemDown = new System.Windows.Forms.Button();
            this.cmdNewItem = new System.Windows.Forms.Button();
            this.cmdUpdateList = new System.Windows.Forms.Button();
            this.cmdDeleteItem = new System.Windows.Forms.Button();
            this.txtFilterNumber = new System.Windows.Forms.TextBox();
            this.cboFieldName = new System.Windows.Forms.ComboBox();
            this.txtDataType = new System.Windows.Forms.TextBox();
            this.cboComparison = new System.Windows.Forms.ComboBox();
            this.txtCompareToValue = new System.Windows.Forms.TextBox();
            this.cmdPreviewFilterText = new System.Windows.Forms.Button();
            this.mainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.listviewFilterDefs = new System.Windows.Forms.ListView();
            this.lblFilterNumber = new System.Windows.Forms.Label();
            this.lblFieldName = new System.Windows.Forms.Label();
            this.lblBoolean = new System.Windows.Forms.Label();
            this.cboBoolean = new System.Windows.Forms.ComboBox();
            this.lblAggregator = new System.Windows.Forms.Label();
            this.cboAggregator = new System.Windows.Forms.ComboBox();
            this.lblDataType = new System.Windows.Forms.Label();
            this.lblComparison = new System.Windows.Forms.Label();
            this.lblCompareToValue = new System.Windows.Forms.Label();
            this.MainMenu.SuspendLayout();
            this.mainMenuContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdCancel, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdCancel, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdCancel.Location = new System.Drawing.Point(586, 421);
            this.cmdCancel.Name = "cmdCancel";
            this.appHelpProvider.SetShowHelp(this.cmdCancel, true);
            this.cmdCancel.Size = new System.Drawing.Size(93, 37);
            this.cmdCancel.TabIndex = 18;
            this.cmdCancel.Text = "&Cancel";
            this.mainMenuToolTips.SetToolTip(this.cmdCancel, "Close form and exit application");
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdAccept
            // 
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.appHelpProvider.SetHelpKeyword(this.cmdAccept, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdAccept, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdAccept, "Help for Run Tests: See Help File.");
            this.cmdAccept.Location = new System.Drawing.Point(586, 45);
            this.cmdAccept.Name = "cmdAccept";
            this.appHelpProvider.SetShowHelp(this.cmdAccept, true);
            this.cmdAccept.Size = new System.Drawing.Size(93, 37);
            this.cmdAccept.TabIndex = 17;
            this.cmdAccept.Text = "&Accept";
            this.mainMenuToolTips.SetToolTip(this.cmdAccept, "Run selected tests");
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdProcessForm_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp,
            this.toolbarHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(700, 27);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
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
            this.toolbarHelp.Padding = new System.Windows.Forms.Padding(0, 0, 85, 0);
            this.toolbarHelp.Size = new System.Drawing.Size(105, 20);
            this.toolbarHelp.Text = "Help";
            this.toolbarHelp.Click += new System.EventHandler(this.toolbarHelp_Click);
            // 
            // mainMenuContextMenuStrip
            // 
            this.mainMenuContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuContextMenuRunTest,
            this.helpToolStripMenuItem});
            this.mainMenuContextMenuStrip.Name = "mainMenuContextMenuStrip";
            this.mainMenuContextMenuStrip.Size = new System.Drawing.Size(121, 48);
            // 
            // mainMenuContextMenuRunTest
            // 
            this.mainMenuContextMenuRunTest.Name = "mainMenuContextMenuRunTest";
            this.mainMenuContextMenuRunTest.Size = new System.Drawing.Size(120, 22);
            this.mainMenuContextMenuRunTest.Text = "Run LoadTextLines";
            this.mainMenuContextMenuRunTest.Click += new System.EventHandler(this.mainMenuContextMenuRunTest_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // appHelpProvider
            // 
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFAppDataForms\\InitWinFormsAppWithToolbar\\" +
    "InitWinFormsHelpFile.chm";
            // 
            // cmdMoveItemUp
            // 
            this.cmdMoveItemUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMoveItemUp.Font = new System.Drawing.Font("Lucida Console", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMoveItemUp.Location = new System.Drawing.Point(535, 66);
            this.cmdMoveItemUp.Name = "cmdMoveItemUp";
            this.cmdMoveItemUp.Size = new System.Drawing.Size(28, 29);
            this.cmdMoveItemUp.TabIndex = 14;
            this.cmdMoveItemUp.Text = "▲";
            this.mainMenuToolTips.SetToolTip(this.cmdMoveItemUp, "Move Selected Item Up");
            this.cmdMoveItemUp.UseVisualStyleBackColor = true;
            this.cmdMoveItemUp.Click += new System.EventHandler(this.cmdMoveItemUp_Click);
            // 
            // cmdMoveItemDown
            // 
            this.cmdMoveItemDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMoveItemDown.Font = new System.Drawing.Font("Lucida Console", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMoveItemDown.Location = new System.Drawing.Point(535, 113);
            this.cmdMoveItemDown.Name = "cmdMoveItemDown";
            this.cmdMoveItemDown.Size = new System.Drawing.Size(28, 29);
            this.cmdMoveItemDown.TabIndex = 15;
            this.cmdMoveItemDown.Text = "▼";
            this.mainMenuToolTips.SetToolTip(this.cmdMoveItemDown, "Move Selected Item Down");
            this.cmdMoveItemDown.UseVisualStyleBackColor = true;
            this.cmdMoveItemDown.Click += new System.EventHandler(this.cmdMoveItemDown_Click);
            // 
            // cmdNewItem
            // 
            this.cmdNewItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNewItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNewItem.Location = new System.Drawing.Point(534, 237);
            this.cmdNewItem.Name = "cmdNewItem";
            this.cmdNewItem.Size = new System.Drawing.Size(28, 31);
            this.cmdNewItem.TabIndex = 16;
            this.cmdNewItem.Tag = "Create new column definition.";
            this.cmdNewItem.Text = "+";
            this.mainMenuToolTips.SetToolTip(this.cmdNewItem, "Define new item.");
            this.cmdNewItem.UseVisualStyleBackColor = true;
            this.cmdNewItem.Click += new System.EventHandler(this.cmdNewItem_Click);
            // 
            // cmdUpdateList
            // 
            this.cmdUpdateList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdateList.Location = new System.Drawing.Point(451, 306);
            this.cmdUpdateList.Name = "cmdUpdateList";
            this.cmdUpdateList.Size = new System.Drawing.Size(75, 38);
            this.cmdUpdateList.TabIndex = 13;
            this.cmdUpdateList.Text = "Update";
            this.mainMenuToolTips.SetToolTip(this.cmdUpdateList, "Update item information on list.");
            this.cmdUpdateList.UseVisualStyleBackColor = true;
            this.cmdUpdateList.Click += new System.EventHandler(this.cmdUpdateList_Click);
            // 
            // cmdDeleteItem
            // 
            this.cmdDeleteItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDeleteItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeleteItem.Location = new System.Drawing.Point(535, 179);
            this.cmdDeleteItem.Name = "cmdDeleteItem";
            this.cmdDeleteItem.Size = new System.Drawing.Size(28, 31);
            this.cmdDeleteItem.TabIndex = 14;
            this.cmdDeleteItem.Text = "Х";
            this.mainMenuToolTips.SetToolTip(this.cmdDeleteItem, "Delete currently selected item.");
            this.cmdDeleteItem.UseVisualStyleBackColor = true;
            this.cmdDeleteItem.Click += new System.EventHandler(this.cmdDeleteItem_Click);
            // 
            // txtFilterNumber
            // 
            this.txtFilterNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilterNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtFilterNumber.Location = new System.Drawing.Point(112, 280);
            this.txtFilterNumber.Name = "txtFilterNumber";
            this.txtFilterNumber.ReadOnly = true;
            this.txtFilterNumber.Size = new System.Drawing.Size(76, 20);
            this.txtFilterNumber.TabIndex = 4;
            this.mainMenuToolTips.SetToolTip(this.txtFilterNumber, "Filter number.");
            // 
            // cboFieldName
            // 
            this.cboFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFieldName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboFieldName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFieldName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFieldName.FormattingEnabled = true;
            this.cboFieldName.Location = new System.Drawing.Point(112, 357);
            this.cboFieldName.Name = "cboFieldName";
            this.cboFieldName.Size = new System.Drawing.Size(288, 21);
            this.cboFieldName.TabIndex = 23;
            this.mainMenuToolTips.SetToolTip(this.cboFieldName, "Name of the field filtering will be done on.");
            this.cboFieldName.SelectedIndexChanged += new System.EventHandler(this.cboFieldName_SelectedIndexChanged);
            // 
            // txtDataType
            // 
            this.txtDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataType.Location = new System.Drawing.Point(112, 384);
            this.txtDataType.Name = "txtDataType";
            this.txtDataType.ReadOnly = true;
            this.txtDataType.Size = new System.Drawing.Size(159, 20);
            this.txtDataType.TabIndex = 25;
            this.mainMenuToolTips.SetToolTip(this.txtDataType, "Data type of the field.");
            // 
            // cboComparison
            // 
            this.cboComparison.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboComparison.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboComparison.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboComparison.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComparison.FormattingEnabled = true;
            this.cboComparison.Location = new System.Drawing.Point(112, 411);
            this.cboComparison.Name = "cboComparison";
            this.cboComparison.Size = new System.Drawing.Size(159, 21);
            this.cboComparison.TabIndex = 27;
            this.mainMenuToolTips.SetToolTip(this.cboComparison, "Comparison operator (EqualTo, GreaterThan, LessThan, In, Like etc.)");
            this.cboComparison.SelectedIndexChanged += new System.EventHandler(this.cboComparison_SelectedIndexChanged);
            // 
            // txtCompareToValue
            // 
            this.txtCompareToValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompareToValue.Location = new System.Drawing.Point(112, 438);
            this.txtCompareToValue.Name = "txtCompareToValue";
            this.txtCompareToValue.Size = new System.Drawing.Size(288, 20);
            this.txtCompareToValue.TabIndex = 28;
            this.mainMenuToolTips.SetToolTip(this.txtCompareToValue, "Value or values to compare against.");
            // 
            // cmdPreviewFilterText
            // 
            this.cmdPreviewFilterText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPreviewFilterText.Location = new System.Drawing.Point(586, 144);
            this.cmdPreviewFilterText.Name = "cmdPreviewFilterText";
            this.cmdPreviewFilterText.Size = new System.Drawing.Size(93, 37);
            this.cmdPreviewFilterText.TabIndex = 30;
            this.cmdPreviewFilterText.Text = "Show SQL";
            this.mainMenuToolTips.SetToolTip(this.cmdPreviewFilterText, "Shows the SQL text that will be generated using the current filter items.");
            this.cmdPreviewFilterText.UseVisualStyleBackColor = true;
            this.cmdPreviewFilterText.Click += new System.EventHandler(this.cmdPreviewFilterText_Click);
            // 
            // listviewFilterDefs
            // 
            this.listviewFilterDefs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listviewFilterDefs.Location = new System.Drawing.Point(23, 45);
            this.listviewFilterDefs.Name = "listviewFilterDefs";
            this.listviewFilterDefs.Size = new System.Drawing.Size(503, 223);
            this.listviewFilterDefs.TabIndex = 2;
            this.listviewFilterDefs.UseCompatibleStateImageBehavior = false;
            this.listviewFilterDefs.SelectedIndexChanged += new System.EventHandler(this.listviewFilterlDefs_SelectedIndexChanged);
            // 
            // lblFilterNumber
            // 
            this.lblFilterNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFilterNumber.AutoSize = true;
            this.lblFilterNumber.Location = new System.Drawing.Point(20, 283);
            this.lblFilterNumber.Name = "lblFilterNumber";
            this.lblFilterNumber.Size = new System.Drawing.Size(52, 13);
            this.lblFilterNumber.TabIndex = 3;
            this.lblFilterNumber.Text = "Filter. No.";
            // 
            // lblFieldName
            // 
            this.lblFieldName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFieldName.AutoSize = true;
            this.lblFieldName.Location = new System.Drawing.Point(20, 360);
            this.lblFieldName.Name = "lblFieldName";
            this.lblFieldName.Size = new System.Drawing.Size(60, 13);
            this.lblFieldName.TabIndex = 5;
            this.lblFieldName.Text = "Field Name";
            // 
            // lblBoolean
            // 
            this.lblBoolean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBoolean.AutoSize = true;
            this.lblBoolean.Location = new System.Drawing.Point(20, 306);
            this.lblBoolean.Name = "lblBoolean";
            this.lblBoolean.Size = new System.Drawing.Size(46, 13);
            this.lblBoolean.TabIndex = 19;
            this.lblBoolean.Text = "Boolean";
            // 
            // cboBoolean
            // 
            this.cboBoolean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBoolean.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboBoolean.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBoolean.DisplayMember = "Boolean operator connecting two filters (and, or).";
            this.cboBoolean.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBoolean.FormattingEnabled = true;
            this.cboBoolean.Location = new System.Drawing.Point(112, 306);
            this.cboBoolean.Name = "cboBoolean";
            this.cboBoolean.Size = new System.Drawing.Size(159, 21);
            this.cboBoolean.TabIndex = 20;
            this.cboBoolean.ValueMember = "Boolean operator connecting two filters (and, or).";
            // 
            // lblAggregator
            // 
            this.lblAggregator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAggregator.AutoSize = true;
            this.lblAggregator.Location = new System.Drawing.Point(20, 331);
            this.lblAggregator.Name = "lblAggregator";
            this.lblAggregator.Size = new System.Drawing.Size(59, 13);
            this.lblAggregator.TabIndex = 21;
            this.lblAggregator.Text = "Aggregator";
            // 
            // cboAggregator
            // 
            this.cboAggregator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAggregator.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.cboAggregator.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAggregator.DisplayMember = "Aggregate function to apply to the field.";
            this.cboAggregator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAggregator.FormattingEnabled = true;
            this.cboAggregator.Location = new System.Drawing.Point(112, 331);
            this.cboAggregator.Name = "cboAggregator";
            this.cboAggregator.Size = new System.Drawing.Size(159, 21);
            this.cboAggregator.TabIndex = 22;
            this.cboAggregator.ValueMember = "Aggregate function to apply to the field.";
            // 
            // lblDataType
            // 
            this.lblDataType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(20, 384);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(57, 13);
            this.lblDataType.TabIndex = 24;
            this.lblDataType.Text = "Data Type";
            // 
            // lblComparison
            // 
            this.lblComparison.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblComparison.AutoSize = true;
            this.lblComparison.Location = new System.Drawing.Point(20, 411);
            this.lblComparison.Name = "lblComparison";
            this.lblComparison.Size = new System.Drawing.Size(62, 13);
            this.lblComparison.TabIndex = 26;
            this.lblComparison.Text = "Comparison";
            // 
            // lblCompareToValue
            // 
            this.lblCompareToValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCompareToValue.AutoSize = true;
            this.lblCompareToValue.Location = new System.Drawing.Point(20, 438);
            this.lblCompareToValue.Name = "lblCompareToValue";
            this.lblCompareToValue.Size = new System.Drawing.Size(65, 13);
            this.lblCompareToValue.TabIndex = 29;
            this.lblCompareToValue.Text = "Compare To";
            // 
            // PFFilterBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 478);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.lblCompareToValue);
            this.Controls.Add(this.txtCompareToValue);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.cboComparison);
            this.Controls.Add(this.lblComparison);
            this.Controls.Add(this.txtDataType);
            this.Controls.Add(this.lblDataType);
            this.Controls.Add(this.cboFieldName);
            this.Controls.Add(this.cboAggregator);
            this.Controls.Add(this.lblAggregator);
            this.Controls.Add(this.cmdPreviewFilterText);
            this.Controls.Add(this.cboBoolean);
            this.Controls.Add(this.lblBoolean);
            this.Controls.Add(this.lblFieldName);
            this.Controls.Add(this.txtFilterNumber);
            this.Controls.Add(this.cmdDeleteItem);
            this.Controls.Add(this.cmdNewItem);
            this.Controls.Add(this.lblFilterNumber);
            this.Controls.Add(this.cmdUpdateList);
            this.Controls.Add(this.listviewFilterDefs);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdMoveItemDown);
            this.Controls.Add(this.cmdMoveItemUp);
            this.Name = "PFFilterBuilderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter Builder";
            this.mainMenuToolTips.SetToolTip(this, "Shows the SQL text that will be generated by the current set of filter items.");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.mainMenuContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.OpenFileDialog mainMenuOpenFileDialog;
        private System.Windows.Forms.ContextMenuStrip mainMenuContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mainMenuContextMenuRunTest;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolTip mainMenuToolTips;
        private System.Windows.Forms.SaveFileDialog mainMenuSaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog mainMenuFolderBrowserDialog;
        private System.Windows.Forms.ListView listviewFilterDefs;
        private System.Windows.Forms.Button cmdMoveItemUp;
        private System.Windows.Forms.Button cmdMoveItemDown;
        private System.Windows.Forms.Label lblFilterNumber;
        private System.Windows.Forms.TextBox txtFilterNumber;
        private System.Windows.Forms.Button cmdNewItem;
        private System.Windows.Forms.Button cmdUpdateList;
        private System.Windows.Forms.Button cmdDeleteItem;
        private System.Windows.Forms.Label lblFieldName;
        private System.Windows.Forms.Label lblBoolean;
        private System.Windows.Forms.ComboBox cboBoolean;
        private System.Windows.Forms.Label lblAggregator;
        private System.Windows.Forms.ComboBox cboAggregator;
        private System.Windows.Forms.ComboBox cboFieldName;
        private System.Windows.Forms.Label lblDataType;
        private System.Windows.Forms.TextBox txtDataType;
        private System.Windows.Forms.Label lblComparison;
        private System.Windows.Forms.ComboBox cboComparison;
        private System.Windows.Forms.TextBox txtCompareToValue;
        private System.Windows.Forms.Label lblCompareToValue;
        private System.Windows.Forms.Button cmdPreviewFilterText;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
    }
}

