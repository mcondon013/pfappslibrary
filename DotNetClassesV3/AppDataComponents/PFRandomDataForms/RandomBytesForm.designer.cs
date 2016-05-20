namespace PFRandomDataForms
{
    partial class RandomBytesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomBytesForm));
            this.cmdExit = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuToolsReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsResetEmptyMruList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsResetReloadOriginalRequestDefinitions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpContents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpTutorial = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpContact = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainMenuContextMenuRunTest = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appHelpProvider = new System.Windows.Forms.HelpProvider();
            this.MainFormToolbar = new System.Windows.Forms.ToolStrip();
            this.toolbtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolbtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolbtnClose = new System.Windows.Forms.ToolStripButton();
            this.toolbtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolbtnPrintPreview = new System.Windows.Forms.ToolStripButton();
            this.toolbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarHelp = new System.Windows.Forms.ToolStripButton();
            this.mainMenuOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainMenuToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.txtMinArrayLength = new System.Windows.Forms.TextBox();
            this.txtMaxArrayLength = new System.Windows.Forms.TextBox();
            this.optChar = new System.Windows.Forms.RadioButton();
            this.optByte = new System.Windows.Forms.RadioButton();
            this.optSingleValue = new System.Windows.Forms.RadioButton();
            this.optArrayOfValues = new System.Windows.Forms.RadioButton();
            this.mainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblMinArrayLength = new System.Windows.Forms.Label();
            this.lblMaxArrayLength = new System.Windows.Forms.Label();
            this.grpDataType = new System.Windows.Forms.GroupBox();
            this.lblDataMaskName = new System.Windows.Forms.Label();
            this.txtDataMaskName = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdPreview = new System.Windows.Forms.Button();
            this.pnlOutputs = new System.Windows.Forms.Panel();
            this.pnlArrayLength = new System.Windows.Forms.Panel();
            this.lblNumRandomDataItems = new System.Windows.Forms.Label();
            this.txtNumRandomDataItems = new System.Windows.Forms.TextBox();
            this.MainMenu.SuspendLayout();
            this.mainMenuContextMenuStrip.SuspendLayout();
            this.MainFormToolbar.SuspendLayout();
            this.grpDataType.SuspendLayout();
            this.pnlOutputs.SuspendLayout();
            this.pnlArrayLength.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(314, 300);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 10;
            this.cmdExit.Text = "&Exit";
            this.mainMenuToolTips.SetToolTip(this.cmdExit, "Close form and exit application");
            this.cmdExit.UseVisualStyleBackColor = true;
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // cmdOK
            // 
            this.appHelpProvider.SetHelpKeyword(this.cmdOK, "Run Tests");
            this.appHelpProvider.SetHelpNavigator(this.cmdOK, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.appHelpProvider.SetHelpString(this.cmdOK, "Help for Run Tests: See Help File.");
            this.cmdOK.Location = new System.Drawing.Point(314, 72);
            this.cmdOK.Name = "cmdOK";
            this.appHelpProvider.SetShowHelp(this.cmdOK, true);
            this.cmdOK.Size = new System.Drawing.Size(93, 37);
            this.cmdOK.TabIndex = 6;
            this.cmdOK.Text = "&OK";
            this.mainMenuToolTips.SetToolTip(this.cmdOK, "Run selected tests");
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdProcessForm_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(435, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileClose,
            this.mnuFileSave,
            this.toolStripSeparator8,
            this.mnuFilePageSetup,
            this.mnuFilePrint,
            this.mnuFilePrintPreview,
            this.toolStripSeparator11,
            this.mnuFileDelete,
            this.toolStripSeparator12,
            this.mnuFileRecent,
            this.toolStripSeparator13,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(143, 22);
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(143, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileClose
            // 
            this.mnuFileClose.Name = "mnuFileClose";
            this.mnuFileClose.Size = new System.Drawing.Size(143, 22);
            this.mnuFileClose.Text = "&Close";
            this.mnuFileClose.Click += new System.EventHandler(this.mnuFileClose_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(143, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(140, 6);
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
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFileDelete
            // 
            this.mnuFileDelete.Name = "mnuFileDelete";
            this.mnuFileDelete.Size = new System.Drawing.Size(143, 22);
            this.mnuFileDelete.Text = "&Delete";
            this.mnuFileDelete.Click += new System.EventHandler(this.mnuFileDelete_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(140, 6);
            // 
            // mnuFileRecent
            // 
            this.mnuFileRecent.Name = "mnuFileRecent";
            this.mnuFileRecent.Size = new System.Drawing.Size(143, 22);
            this.mnuFileRecent.Text = "R&ecent File";
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
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator7,
            this.mnuToolsReset});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(99, 6);
            // 
            // mnuToolsReset
            // 
            this.mnuToolsReset.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsResetEmptyMruList,
            this.mnuToolsResetReloadOriginalRequestDefinitions});
            this.mnuToolsReset.Name = "mnuToolsReset";
            this.mnuToolsReset.Size = new System.Drawing.Size(102, 22);
            this.mnuToolsReset.Text = "&Reset";
            // 
            // mnuToolsResetEmptyMruList
            // 
            this.mnuToolsResetEmptyMruList.Name = "mnuToolsResetEmptyMruList";
            this.mnuToolsResetEmptyMruList.Size = new System.Drawing.Size(260, 22);
            this.mnuToolsResetEmptyMruList.Text = "Empty Recent Files List";
            this.mnuToolsResetEmptyMruList.ToolTipText = "Remove all entries from the Most Recently Used lList.";
            this.mnuToolsResetEmptyMruList.Click += new System.EventHandler(this.mnuToolsResetEmptyMruList_Click);
            // 
            // mnuToolsResetReloadOriginalRequestDefinitions
            // 
            this.mnuToolsResetReloadOriginalRequestDefinitions.Name = "mnuToolsResetReloadOriginalRequestDefinitions";
            this.mnuToolsResetReloadOriginalRequestDefinitions.Size = new System.Drawing.Size(260, 22);
            this.mnuToolsResetReloadOriginalRequestDefinitions.Text = "Reload Original Request Definitions";
            this.mnuToolsResetReloadOriginalRequestDefinitions.ToolTipText = "Reload the random data request definitions originally installed with the applicat" +
    "ion.";
            this.mnuToolsResetReloadOriginalRequestDefinitions.Click += new System.EventHandler(this.mnuToolsResetReloadOriginalRequestDefinitions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpContents,
            this.mnuHelpIndex,
            this.mnuHelpSearch,
            this.toolStripSeparator2,
            this.mnuHelpTutorial,
            this.toolStripSeparator3,
            this.mnuHelpContact,
            this.toolStripSeparator4,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpContents
            // 
            this.mnuHelpContents.Name = "mnuHelpContents";
            this.mnuHelpContents.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContents.Text = "Contents";
            this.mnuHelpContents.Click += new System.EventHandler(this.mnuHelpContents_Click);
            // 
            // mnuHelpIndex
            // 
            this.mnuHelpIndex.Name = "mnuHelpIndex";
            this.mnuHelpIndex.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpIndex.Text = "Index";
            this.mnuHelpIndex.Click += new System.EventHandler(this.mnuHelpIndex_Click);
            // 
            // mnuHelpSearch
            // 
            this.mnuHelpSearch.Name = "mnuHelpSearch";
            this.mnuHelpSearch.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpSearch.Text = "Search";
            this.mnuHelpSearch.Click += new System.EventHandler(this.mnuHelpSearch_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpTutorial
            // 
            this.mnuHelpTutorial.Name = "mnuHelpTutorial";
            this.mnuHelpTutorial.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpTutorial.Text = "Tutorial";
            this.mnuHelpTutorial.Click += new System.EventHandler(this.mnuHelpTutorial_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpContact
            // 
            this.mnuHelpContact.Name = "mnuHelpContact";
            this.mnuHelpContact.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpContact.Text = "Contact ProFast Computing";
            this.mnuHelpContact.Click += new System.EventHandler(this.mnuHelpContact_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(222, 22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
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
            this.mainMenuContextMenuRunTest.Text = "Run Test";
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
            this.appHelpProvider.HelpNamespace = "C:\\ProFast\\Projects\\DotNetPrototypesV3\\PFRandomDataForms\\InitWinFormsAppWithToolb" +
    "ar\\InitWinFormsHelpFile.chm";
            // 
            // MainFormToolbar
            // 
            this.MainFormToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbtnNew,
            this.toolbtnOpen,
            this.toolbtnClose,
            this.toolbtnSave,
            this.toolbtnPrint,
            this.toolbtnPrintPreview,
            this.toolbtnDelete,
            this.toolStripSeparator1,
            this.toolStripSeparator5,
            this.toolStripSeparator6,
            this.toolStripSeparator9,
            this.toolbarHelp});
            this.MainFormToolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MainFormToolbar.Location = new System.Drawing.Point(0, 24);
            this.MainFormToolbar.Name = "MainFormToolbar";
            this.MainFormToolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainFormToolbar.Size = new System.Drawing.Size(435, 25);
            this.MainFormToolbar.TabIndex = 1;
            this.MainFormToolbar.Text = "MainFormToolbar";
            this.MainFormToolbar.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MainFormToolbar_ItemClicked);
            // 
            // toolbtnNew
            // 
            this.toolbtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnNew.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnNew.Image")));
            this.toolbtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnNew.Name = "toolbtnNew";
            this.toolbtnNew.Size = new System.Drawing.Size(23, 22);
            this.toolbtnNew.Text = "New";
            this.toolbtnNew.ToolTipText = "New";
            // 
            // toolbtnOpen
            // 
            this.toolbtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnOpen.Image")));
            this.toolbtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnOpen.Name = "toolbtnOpen";
            this.toolbtnOpen.Size = new System.Drawing.Size(23, 22);
            this.toolbtnOpen.Text = "Open";
            this.toolbtnOpen.ToolTipText = "Open";
            // 
            // toolbtnClose
            // 
            this.toolbtnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnClose.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnClose.Image")));
            this.toolbtnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnClose.Name = "toolbtnClose";
            this.toolbtnClose.Size = new System.Drawing.Size(23, 22);
            this.toolbtnClose.Text = "Close";
            // 
            // toolbtnSave
            // 
            this.toolbtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnSave.Image")));
            this.toolbtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnSave.Name = "toolbtnSave";
            this.toolbtnSave.Size = new System.Drawing.Size(23, 22);
            this.toolbtnSave.Text = "Save";
            // 
            // toolbtnPrint
            // 
            this.toolbtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnPrint.Image")));
            this.toolbtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPrint.Name = "toolbtnPrint";
            this.toolbtnPrint.Size = new System.Drawing.Size(23, 22);
            this.toolbtnPrint.Text = "Print";
            // 
            // toolbtnPrintPreview
            // 
            this.toolbtnPrintPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnPrintPreview.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnPrintPreview.Image")));
            this.toolbtnPrintPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnPrintPreview.Name = "toolbtnPrintPreview";
            this.toolbtnPrintPreview.Size = new System.Drawing.Size(23, 22);
            this.toolbtnPrintPreview.Text = "PrintPreview";
            this.toolbtnPrintPreview.ToolTipText = "Print Preview";
            // 
            // toolbtnDelete
            // 
            this.toolbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolbtnDelete.Image")));
            this.toolbtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbtnDelete.Name = "toolbtnDelete";
            this.toolbtnDelete.Size = new System.Drawing.Size(23, 22);
            this.toolbtnDelete.Text = "Delete";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(15, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.AutoSize = false;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(15, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.AutoSize = false;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(15, 25);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.AutoSize = false;
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(50, 25);
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
            // 
            // txtMinArrayLength
            // 
            this.txtMinArrayLength.Location = new System.Drawing.Point(171, 6);
            this.txtMinArrayLength.Name = "txtMinArrayLength";
            this.txtMinArrayLength.Size = new System.Drawing.Size(61, 20);
            this.txtMinArrayLength.TabIndex = 1;
            this.mainMenuToolTips.SetToolTip(this.txtMinArrayLength, "Minimum number of array items to generate.");
            // 
            // txtMaxArrayLength
            // 
            this.txtMaxArrayLength.Location = new System.Drawing.Point(171, 32);
            this.txtMaxArrayLength.Name = "txtMaxArrayLength";
            this.txtMaxArrayLength.Size = new System.Drawing.Size(61, 20);
            this.txtMaxArrayLength.TabIndex = 3;
            this.mainMenuToolTips.SetToolTip(this.txtMaxArrayLength, "Maximum number of array items to generate.");
            // 
            // optChar
            // 
            this.optChar.AutoSize = true;
            this.optChar.Location = new System.Drawing.Point(18, 48);
            this.optChar.Name = "optChar";
            this.optChar.Size = new System.Drawing.Size(125, 17);
            this.optChar.TabIndex = 1;
            this.optChar.TabStop = true;
            this.optChar.Text = "Char (16-bit Unicode)";
            this.mainMenuToolTips.SetToolTip(this.optChar, "Generate a Unicode value. A byte value will be converted to unicode for this appl" +
        "ication.");
            this.optChar.UseVisualStyleBackColor = true;
            // 
            // optByte
            // 
            this.optByte.AutoSize = true;
            this.optByte.Location = new System.Drawing.Point(18, 20);
            this.optByte.Name = "optByte";
            this.optByte.Size = new System.Drawing.Size(192, 17);
            this.optByte.TabIndex = 0;
            this.optByte.TabStop = true;
            this.optByte.Text = "Byte (8 bit ASCII + ASCII Extended)";
            this.mainMenuToolTips.SetToolTip(this.optByte, "Generate a byte value between 0 and 255");
            this.optByte.UseVisualStyleBackColor = true;
            // 
            // optSingleValue
            // 
            this.optSingleValue.AutoSize = true;
            this.optSingleValue.Location = new System.Drawing.Point(18, 99);
            this.optSingleValue.Name = "optSingleValue";
            this.optSingleValue.Size = new System.Drawing.Size(119, 17);
            this.optSingleValue.TabIndex = 2;
            this.optSingleValue.TabStop = true;
            this.optSingleValue.Text = "Output Single Value";
            this.mainMenuToolTips.SetToolTip(this.optSingleValue, "Output a single byte or char.");
            this.optSingleValue.UseVisualStyleBackColor = true;
            // 
            // optArrayOfValues
            // 
            this.optArrayOfValues.AutoSize = true;
            this.optArrayOfValues.Location = new System.Drawing.Point(18, 6);
            this.optArrayOfValues.Name = "optArrayOfValues";
            this.optArrayOfValues.Size = new System.Drawing.Size(131, 17);
            this.optArrayOfValues.TabIndex = 0;
            this.optArrayOfValues.TabStop = true;
            this.optArrayOfValues.Text = "Output Array of Values";
            this.mainMenuToolTips.SetToolTip(this.optArrayOfValues, "Output an array of many  bytes or chars.");
            this.optArrayOfValues.UseVisualStyleBackColor = true;
            this.optArrayOfValues.CheckedChanged += new System.EventHandler(this.optArrayOfValues_CheckedChanged);
            // 
            // lblMinArrayLength
            // 
            this.lblMinArrayLength.AutoSize = true;
            this.lblMinArrayLength.Location = new System.Drawing.Point(7, 6);
            this.lblMinArrayLength.Name = "lblMinArrayLength";
            this.lblMinArrayLength.Size = new System.Drawing.Size(158, 13);
            this.lblMinArrayLength.TabIndex = 0;
            this.lblMinArrayLength.Text = "Minimum Number of Array Items:";
            // 
            // lblMaxArrayLength
            // 
            this.lblMaxArrayLength.AutoSize = true;
            this.lblMaxArrayLength.Location = new System.Drawing.Point(7, 32);
            this.lblMaxArrayLength.Name = "lblMaxArrayLength";
            this.lblMaxArrayLength.Size = new System.Drawing.Size(161, 13);
            this.lblMaxArrayLength.TabIndex = 2;
            this.lblMaxArrayLength.Text = "Maximum Number of Array Items:";
            // 
            // grpDataType
            // 
            this.grpDataType.Controls.Add(this.optChar);
            this.grpDataType.Controls.Add(this.optByte);
            this.grpDataType.Location = new System.Drawing.Point(12, 115);
            this.grpDataType.Name = "grpDataType";
            this.grpDataType.Size = new System.Drawing.Size(286, 75);
            this.grpDataType.TabIndex = 4;
            this.grpDataType.TabStop = false;
            this.grpDataType.Text = "Data Type";
            // 
            // lblDataMaskName
            // 
            this.lblDataMaskName.AutoSize = true;
            this.lblDataMaskName.Location = new System.Drawing.Point(21, 64);
            this.lblDataMaskName.Name = "lblDataMaskName";
            this.lblDataMaskName.Size = new System.Drawing.Size(90, 13);
            this.lblDataMaskName.TabIndex = 2;
            this.lblDataMaskName.Text = "Data Mask Name";
            // 
            // txtDataMaskName
            // 
            this.txtDataMaskName.Location = new System.Drawing.Point(24, 81);
            this.txtDataMaskName.Name = "txtDataMaskName";
            this.txtDataMaskName.Size = new System.Drawing.Size(250, 20);
            this.txtDataMaskName.TabIndex = 3;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(314, 135);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(93, 37);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(314, 202);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(93, 34);
            this.cmdPreview.TabIndex = 8;
            this.cmdPreview.Text = "&Preview";
            this.cmdPreview.UseVisualStyleBackColor = true;
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // pnlOutputs
            // 
            this.pnlOutputs.Controls.Add(this.pnlArrayLength);
            this.pnlOutputs.Controls.Add(this.optSingleValue);
            this.pnlOutputs.Controls.Add(this.optArrayOfValues);
            this.pnlOutputs.Location = new System.Drawing.Point(12, 205);
            this.pnlOutputs.Name = "pnlOutputs";
            this.pnlOutputs.Size = new System.Drawing.Size(277, 132);
            this.pnlOutputs.TabIndex = 5;
            // 
            // pnlArrayLength
            // 
            this.pnlArrayLength.Controls.Add(this.txtMaxArrayLength);
            this.pnlArrayLength.Controls.Add(this.lblMaxArrayLength);
            this.pnlArrayLength.Controls.Add(this.lblMinArrayLength);
            this.pnlArrayLength.Controls.Add(this.txtMinArrayLength);
            this.pnlArrayLength.Location = new System.Drawing.Point(30, 29);
            this.pnlArrayLength.Name = "pnlArrayLength";
            this.pnlArrayLength.Size = new System.Drawing.Size(237, 60);
            this.pnlArrayLength.TabIndex = 1;
            // 
            // lblNumRandomDataItems
            // 
            this.lblNumRandomDataItems.AutoSize = true;
            this.lblNumRandomDataItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRandomDataItems.Location = new System.Drawing.Point(311, 243);
            this.lblNumRandomDataItems.Name = "lblNumRandomDataItems";
            this.lblNumRandomDataItems.Size = new System.Drawing.Size(98, 13);
            this.lblNumRandomDataItems.TabIndex = 27;
            this.lblNumRandomDataItems.Text = "Num Preview Items";
            // 
            // txtNumRandomDataItems
            // 
            this.txtNumRandomDataItems.Location = new System.Drawing.Point(314, 259);
            this.txtNumRandomDataItems.Name = "txtNumRandomDataItems";
            this.txtNumRandomDataItems.Size = new System.Drawing.Size(93, 20);
            this.txtNumRandomDataItems.TabIndex = 9;
            // 
            // RandomBytesForm
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(435, 364);
            this.Controls.Add(this.lblNumRandomDataItems);
            this.Controls.Add(this.txtNumRandomDataItems);
            this.Controls.Add(this.grpDataType);
            this.Controls.Add(this.pnlOutputs);
            this.Controls.Add(this.txtDataMaskName);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdPreview);
            this.Controls.Add(this.lblDataMaskName);
            this.Controls.Add(this.MainFormToolbar);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RandomBytesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Random Bytes Data Request";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.mainMenuContextMenuStrip.ResumeLayout(false);
            this.MainFormToolbar.ResumeLayout(false);
            this.MainFormToolbar.PerformLayout();
            this.grpDataType.ResumeLayout(false);
            this.grpDataType.PerformLayout();
            this.pnlOutputs.ResumeLayout(false);
            this.pnlOutputs.PerformLayout();
            this.pnlArrayLength.ResumeLayout(false);
            this.pnlArrayLength.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdExit;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContents;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpIndex;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpTutorial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpContact;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.HelpProvider appHelpProvider;
        private System.Windows.Forms.ToolStrip MainFormToolbar;
        private System.Windows.Forms.ToolStripButton toolbtnNew;
        private System.Windows.Forms.ToolStripButton toolbtnOpen;
        private System.Windows.Forms.ToolStripButton toolbtnClose;
        private System.Windows.Forms.ToolStripButton toolbtnSave;
        private System.Windows.Forms.ToolStripButton toolbtnPrint;
        private System.Windows.Forms.ToolStripButton toolbtnPrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolbtnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolbarHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mnuFileDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem mnuFileRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.OpenFileDialog mainMenuOpenFileDialog;
        private System.Windows.Forms.ContextMenuStrip mainMenuContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mainMenuContextMenuRunTest;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolTip mainMenuToolTips;
        private System.Windows.Forms.SaveFileDialog mainMenuSaveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog mainMenuFolderBrowserDialog;
        private System.Windows.Forms.Label lblMinArrayLength;
        private System.Windows.Forms.TextBox txtMinArrayLength;
        private System.Windows.Forms.Label lblMaxArrayLength;
        private System.Windows.Forms.TextBox txtMaxArrayLength;
        private System.Windows.Forms.GroupBox grpDataType;
        private System.Windows.Forms.RadioButton optChar;
        private System.Windows.Forms.RadioButton optByte;
        private System.Windows.Forms.Label lblDataMaskName;
        private System.Windows.Forms.TextBox txtDataMaskName;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdPreview;
        private System.Windows.Forms.Panel pnlOutputs;
        private System.Windows.Forms.RadioButton optSingleValue;
        private System.Windows.Forms.RadioButton optArrayOfValues;
        private System.Windows.Forms.Panel pnlArrayLength;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsReset;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsResetEmptyMruList;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsResetReloadOriginalRequestDefinitions;
        private System.Windows.Forms.Label lblNumRandomDataItems;
        private System.Windows.Forms.TextBox txtNumRandomDataItems;
    }
}

