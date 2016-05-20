namespace PFRandomDataForms
{
    /// <summary>
    /// Form for defining random strings.
    /// </summary>
    partial class RandomStringsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomStringsForm));
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
            this.optAN = new System.Windows.Forms.RadioButton();
            this.optANX = new System.Windows.Forms.RadioButton();
            this.optAL = new System.Windows.Forms.RadioButton();
            this.optLC = new System.Windows.Forms.RadioButton();
            this.optUC = new System.Windows.Forms.RadioButton();
            this.optDEC = new System.Windows.Forms.RadioButton();
            this.optHEX = new System.Windows.Forms.RadioButton();
            this.lblStringMinimumLength = new System.Windows.Forms.Label();
            this.txtStringMinimumLength = new System.Windows.Forms.TextBox();
            this.lblStringMaximumLength = new System.Windows.Forms.Label();
            this.txtStringMaximumLength = new System.Windows.Forms.TextBox();
            this.lblSyllableStringMinimumLength = new System.Windows.Forms.Label();
            this.txtSyllableStringMinimumLength = new System.Windows.Forms.TextBox();
            this.txtMinNumSyllableStrings = new System.Windows.Forms.TextBox();
            this.txtSyllableStringMaximumLength = new System.Windows.Forms.TextBox();
            this.lblSyllableStringMaximumLength = new System.Windows.Forms.Label();
            this.txtMinNumStrings = new System.Windows.Forms.TextBox();
            this.lblMinNumStrings = new System.Windows.Forms.Label();
            this.optRandomSyllableStrings = new System.Windows.Forms.RadioButton();
            this.optRandomStrings = new System.Windows.Forms.RadioButton();
            this.optRepeatingStrings = new System.Windows.Forms.RadioButton();
            this.txtMaxNumSyllableStrings = new System.Windows.Forms.TextBox();
            this.optSyllableUCLC = new System.Windows.Forms.RadioButton();
            this.optSyllableUC = new System.Windows.Forms.RadioButton();
            this.optSyllableLC = new System.Windows.Forms.RadioButton();
            this.lblMaxNumStrings = new System.Windows.Forms.Label();
            this.txtMaxNumStrings = new System.Windows.Forms.TextBox();
            this.optRepeatHEX = new System.Windows.Forms.RadioButton();
            this.optRepeatDEC = new System.Windows.Forms.RadioButton();
            this.optRepeatUC = new System.Windows.Forms.RadioButton();
            this.optRepeatLC = new System.Windows.Forms.RadioButton();
            this.optRepeatAL = new System.Windows.Forms.RadioButton();
            this.optRepeatANX = new System.Windows.Forms.RadioButton();
            this.optRepeatAN = new System.Windows.Forms.RadioButton();
            this.txtMaxRepeatOutputLength = new System.Windows.Forms.TextBox();
            this.lblMaxRepeatOutputLength = new System.Windows.Forms.Label();
            this.lblMinRepeatOutputLength = new System.Windows.Forms.Label();
            this.txtMinRepeatOutputLength = new System.Windows.Forms.TextBox();
            this.optANUC = new System.Windows.Forms.RadioButton();
            this.optANLC = new System.Windows.Forms.RadioButton();
            this.optRepeatANLC = new System.Windows.Forms.RadioButton();
            this.optRepeatANUC = new System.Windows.Forms.RadioButton();
            this.cboRegexReplacement = new System.Windows.Forms.ComboBox();
            this.cboRegexPattern = new System.Windows.Forms.ComboBox();
            this.lblRegexReplacement = new System.Windows.Forms.Label();
            this.lblRegexPattern = new System.Windows.Forms.Label();
            this.mainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.lblDataMaskName = new System.Windows.Forms.Label();
            this.txtDataMaskName = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdPreview = new System.Windows.Forms.Button();
            this.pnlStringDomain = new System.Windows.Forms.Panel();
            this.pnlRepeatingStrings = new System.Windows.Forms.Panel();
            this.lblTextToRepeat = new System.Windows.Forms.Label();
            this.pnlRepeatRandomCharacterType = new System.Windows.Forms.Panel();
            this.txtTextToRepeat = new System.Windows.Forms.TextBox();
            this.optRepeatText = new System.Windows.Forms.RadioButton();
            this.optRepeatRandomCharacter = new System.Windows.Forms.RadioButton();
            this.txtMaxNumRepeats = new System.Windows.Forms.TextBox();
            this.lblMaxNumRepeats = new System.Windows.Forms.Label();
            this.lblMinNumRepeats = new System.Windows.Forms.Label();
            this.txtMinNumRepeats = new System.Windows.Forms.TextBox();
            this.pnlRandomSyllables = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMaxNumSyllableStrings = new System.Windows.Forms.Label();
            this.lblMinNumSyllableStrings = new System.Windows.Forms.Label();
            this.pnlRandomStrings = new System.Windows.Forms.Panel();
            this.pnlRandomStringsType = new System.Windows.Forms.Panel();
            this.lblNumRandomDataItems = new System.Windows.Forms.Label();
            this.txtNumRandomDataItems = new System.Windows.Forms.TextBox();
            this.MainMenu.SuspendLayout();
            this.mainMenuContextMenuStrip.SuspendLayout();
            this.MainFormToolbar.SuspendLayout();
            this.pnlStringDomain.SuspendLayout();
            this.pnlRepeatingStrings.SuspendLayout();
            this.pnlRepeatRandomCharacterType.SuspendLayout();
            this.pnlRandomSyllables.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlRandomStrings.SuspendLayout();
            this.pnlRandomStringsType.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExit
            // 
            this.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.appHelpProvider.SetHelpKeyword(this.cmdExit, "Exit Button");
            this.appHelpProvider.SetHelpNavigator(this.cmdExit, System.Windows.Forms.HelpNavigator.KeywordIndex);
            this.cmdExit.Location = new System.Drawing.Point(516, 515);
            this.cmdExit.Name = "cmdExit";
            this.appHelpProvider.SetShowHelp(this.cmdExit, true);
            this.cmdExit.Size = new System.Drawing.Size(93, 37);
            this.cmdExit.TabIndex = 7;
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
            this.cmdOK.Location = new System.Drawing.Point(514, 81);
            this.cmdOK.Name = "cmdOK";
            this.appHelpProvider.SetShowHelp(this.cmdOK, true);
            this.cmdOK.Size = new System.Drawing.Size(93, 37);
            this.cmdOK.TabIndex = 3;
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
            this.MainMenu.Size = new System.Drawing.Size(630, 24);
            this.MainMenu.TabIndex = 8;
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
            this.mnuFileNew.Size = new System.Drawing.Size(152, 22);
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileClose
            // 
            this.mnuFileClose.Name = "mnuFileClose";
            this.mnuFileClose.Size = new System.Drawing.Size(152, 22);
            this.mnuFileClose.Text = "&Close";
            this.mnuFileClose.Click += new System.EventHandler(this.mnuFileClose_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFilePageSetup
            // 
            this.mnuFilePageSetup.Name = "mnuFilePageSetup";
            this.mnuFilePageSetup.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePageSetup.Text = "Page Set&up";
            this.mnuFilePageSetup.Click += new System.EventHandler(this.mnuFilePageSetup_Click);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePrint.Text = "&Print";
            this.mnuFilePrint.Click += new System.EventHandler(this.mnuFilePrint_Click);
            // 
            // mnuFilePrintPreview
            // 
            this.mnuFilePrintPreview.Name = "mnuFilePrintPreview";
            this.mnuFilePrintPreview.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePrintPreview.Text = "Print Pre&view";
            this.mnuFilePrintPreview.Click += new System.EventHandler(this.mnuFilePrintPreview_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileDelete
            // 
            this.mnuFileDelete.Name = "mnuFileDelete";
            this.mnuFileDelete.Size = new System.Drawing.Size(152, 22);
            this.mnuFileDelete.Text = "&Delete";
            this.mnuFileDelete.Click += new System.EventHandler(this.mnuFileDelete_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileRecent
            // 
            this.mnuFileRecent.Name = "mnuFileRecent";
            this.mnuFileRecent.Size = new System.Drawing.Size(152, 22);
            this.mnuFileRecent.Text = "R&ecent File";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
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
            this.MainFormToolbar.Size = new System.Drawing.Size(630, 25);
            this.MainFormToolbar.TabIndex = 10;
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
            // optAN
            // 
            this.optAN.AutoSize = true;
            this.optAN.Location = new System.Drawing.Point(3, 3);
            this.optAN.Name = "optAN";
            this.optAN.Size = new System.Drawing.Size(40, 17);
            this.optAN.TabIndex = 0;
            this.optAN.TabStop = true;
            this.optAN.Text = "AN";
            this.mainMenuToolTips.SetToolTip(this.optAN, "Alpha/Numeric String");
            this.optAN.UseVisualStyleBackColor = true;
            // 
            // optANX
            // 
            this.optANX.AutoSize = true;
            this.optANX.Location = new System.Drawing.Point(163, 3);
            this.optANX.Name = "optANX";
            this.optANX.Size = new System.Drawing.Size(47, 17);
            this.optANX.TabIndex = 3;
            this.optANX.TabStop = true;
            this.optANX.Text = "ANX";
            this.mainMenuToolTips.SetToolTip(this.optANX, "Alpha/Numeric Plus Punctuation Characters");
            this.optANX.UseVisualStyleBackColor = true;
            // 
            // optAL
            // 
            this.optAL.AutoSize = true;
            this.optAL.Location = new System.Drawing.Point(216, 3);
            this.optAL.Name = "optAL";
            this.optAL.Size = new System.Drawing.Size(38, 17);
            this.optAL.TabIndex = 4;
            this.optAL.TabStop = true;
            this.optAL.Text = "AL";
            this.mainMenuToolTips.SetToolTip(this.optAL, "Letters only (upper and lower case)");
            this.optAL.UseVisualStyleBackColor = true;
            // 
            // optLC
            // 
            this.optLC.AutoSize = true;
            this.optLC.Location = new System.Drawing.Point(261, 3);
            this.optLC.Name = "optLC";
            this.optLC.Size = new System.Drawing.Size(38, 17);
            this.optLC.TabIndex = 5;
            this.optLC.TabStop = true;
            this.optLC.Text = "LC";
            this.mainMenuToolTips.SetToolTip(this.optLC, "Lower case letters only");
            this.optLC.UseVisualStyleBackColor = true;
            // 
            // optUC
            // 
            this.optUC.AutoSize = true;
            this.optUC.Location = new System.Drawing.Point(305, 3);
            this.optUC.Name = "optUC";
            this.optUC.Size = new System.Drawing.Size(40, 17);
            this.optUC.TabIndex = 6;
            this.optUC.TabStop = true;
            this.optUC.Text = "UC";
            this.mainMenuToolTips.SetToolTip(this.optUC, "Upper case letters only");
            this.optUC.UseVisualStyleBackColor = true;
            // 
            // optDEC
            // 
            this.optDEC.AutoSize = true;
            this.optDEC.Location = new System.Drawing.Point(351, 3);
            this.optDEC.Name = "optDEC";
            this.optDEC.Size = new System.Drawing.Size(47, 17);
            this.optDEC.TabIndex = 7;
            this.optDEC.TabStop = true;
            this.optDEC.Text = "DEC";
            this.mainMenuToolTips.SetToolTip(this.optDEC, "Number 0 through 9 only");
            this.optDEC.UseVisualStyleBackColor = true;
            // 
            // optHEX
            // 
            this.optHEX.AutoSize = true;
            this.optHEX.Location = new System.Drawing.Point(402, 3);
            this.optHEX.Name = "optHEX";
            this.optHEX.Size = new System.Drawing.Size(47, 17);
            this.optHEX.TabIndex = 8;
            this.optHEX.TabStop = true;
            this.optHEX.Text = "HEX";
            this.mainMenuToolTips.SetToolTip(this.optHEX, "Hexadecimal numbers only (0-9, A-F)");
            this.optHEX.UseVisualStyleBackColor = true;
            // 
            // lblStringMinimumLength
            // 
            this.lblStringMinimumLength.AutoSize = true;
            this.lblStringMinimumLength.Location = new System.Drawing.Point(3, 64);
            this.lblStringMinimumLength.Name = "lblStringMinimumLength";
            this.lblStringMinimumLength.Size = new System.Drawing.Size(87, 13);
            this.lblStringMinimumLength.TabIndex = 5;
            this.lblStringMinimumLength.Text = "Minimum Length:";
            this.mainMenuToolTips.SetToolTip(this.lblStringMinimumLength, "Minimum number of characters in each string");
            // 
            // txtStringMinimumLength
            // 
            this.txtStringMinimumLength.Location = new System.Drawing.Point(164, 60);
            this.txtStringMinimumLength.Name = "txtStringMinimumLength";
            this.txtStringMinimumLength.Size = new System.Drawing.Size(51, 20);
            this.txtStringMinimumLength.TabIndex = 6;
            this.mainMenuToolTips.SetToolTip(this.txtStringMinimumLength, "Minimum number of characters in each string");
            // 
            // lblStringMaximumLength
            // 
            this.lblStringMaximumLength.AutoSize = true;
            this.lblStringMaximumLength.Location = new System.Drawing.Point(223, 64);
            this.lblStringMaximumLength.Name = "lblStringMaximumLength";
            this.lblStringMaximumLength.Size = new System.Drawing.Size(90, 13);
            this.lblStringMaximumLength.TabIndex = 7;
            this.lblStringMaximumLength.Text = "Maximum Length:";
            this.mainMenuToolTips.SetToolTip(this.lblStringMaximumLength, "Maximum number of characters in each string");
            // 
            // txtStringMaximumLength
            // 
            this.txtStringMaximumLength.Location = new System.Drawing.Point(392, 60);
            this.txtStringMaximumLength.Name = "txtStringMaximumLength";
            this.txtStringMaximumLength.Size = new System.Drawing.Size(51, 20);
            this.txtStringMaximumLength.TabIndex = 8;
            this.mainMenuToolTips.SetToolTip(this.txtStringMaximumLength, "Maximum number of characters in each string");
            // 
            // lblSyllableStringMinimumLength
            // 
            this.lblSyllableStringMinimumLength.AutoSize = true;
            this.lblSyllableStringMinimumLength.Location = new System.Drawing.Point(3, 61);
            this.lblSyllableStringMinimumLength.Name = "lblSyllableStringMinimumLength";
            this.lblSyllableStringMinimumLength.Size = new System.Drawing.Size(145, 13);
            this.lblSyllableStringMinimumLength.TabIndex = 5;
            this.lblSyllableStringMinimumLength.Text = "Minimum Length Each String:";
            this.mainMenuToolTips.SetToolTip(this.lblSyllableStringMinimumLength, "Minimum number of syllables per string");
            // 
            // txtSyllableStringMinimumLength
            // 
            this.txtSyllableStringMinimumLength.Location = new System.Drawing.Point(165, 57);
            this.txtSyllableStringMinimumLength.Name = "txtSyllableStringMinimumLength";
            this.txtSyllableStringMinimumLength.Size = new System.Drawing.Size(51, 20);
            this.txtSyllableStringMinimumLength.TabIndex = 6;
            this.mainMenuToolTips.SetToolTip(this.txtSyllableStringMinimumLength, "Minimum number of syllables per string");
            // 
            // txtMinNumSyllableStrings
            // 
            this.txtMinNumSyllableStrings.Location = new System.Drawing.Point(166, 30);
            this.txtMinNumSyllableStrings.Name = "txtMinNumSyllableStrings";
            this.txtMinNumSyllableStrings.Size = new System.Drawing.Size(51, 20);
            this.txtMinNumSyllableStrings.TabIndex = 2;
            this.mainMenuToolTips.SetToolTip(this.txtMinNumSyllableStrings, "Minimum Number of Strings:");
            // 
            // txtSyllableStringMaximumLength
            // 
            this.txtSyllableStringMaximumLength.Location = new System.Drawing.Point(392, 57);
            this.txtSyllableStringMaximumLength.Name = "txtSyllableStringMaximumLength";
            this.txtSyllableStringMaximumLength.Size = new System.Drawing.Size(51, 20);
            this.txtSyllableStringMaximumLength.TabIndex = 8;
            this.mainMenuToolTips.SetToolTip(this.txtSyllableStringMaximumLength, "Maximum number of syllables per string");
            // 
            // lblSyllableStringMaximumLength
            // 
            this.lblSyllableStringMaximumLength.AutoSize = true;
            this.lblSyllableStringMaximumLength.Location = new System.Drawing.Point(223, 61);
            this.lblSyllableStringMaximumLength.Name = "lblSyllableStringMaximumLength";
            this.lblSyllableStringMaximumLength.Size = new System.Drawing.Size(148, 13);
            this.lblSyllableStringMaximumLength.TabIndex = 7;
            this.lblSyllableStringMaximumLength.Text = "Maximum Length Each String:";
            this.mainMenuToolTips.SetToolTip(this.lblSyllableStringMaximumLength, "Maximum number of syllables per string");
            // 
            // txtMinNumStrings
            // 
            this.txtMinNumStrings.Location = new System.Drawing.Point(165, 34);
            this.txtMinNumStrings.Name = "txtMinNumStrings";
            this.txtMinNumStrings.Size = new System.Drawing.Size(51, 20);
            this.txtMinNumStrings.TabIndex = 2;
            this.mainMenuToolTips.SetToolTip(this.txtMinNumStrings, "Minimum number of strings to generate");
            // 
            // lblMinNumStrings
            // 
            this.lblMinNumStrings.AutoSize = true;
            this.lblMinNumStrings.Location = new System.Drawing.Point(3, 38);
            this.lblMinNumStrings.Name = "lblMinNumStrings";
            this.lblMinNumStrings.Size = new System.Drawing.Size(135, 13);
            this.lblMinNumStrings.TabIndex = 1;
            this.lblMinNumStrings.Text = "Minimum Number of Strings";
            this.mainMenuToolTips.SetToolTip(this.lblMinNumStrings, "Minimum number of strings to generate");
            // 
            // optRandomSyllableStrings
            // 
            this.optRandomSyllableStrings.AutoSize = true;
            this.optRandomSyllableStrings.Location = new System.Drawing.Point(18, 179);
            this.optRandomSyllableStrings.Name = "optRandomSyllableStrings";
            this.optRandomSyllableStrings.Size = new System.Drawing.Size(139, 17);
            this.optRandomSyllableStrings.TabIndex = 2;
            this.optRandomSyllableStrings.TabStop = true;
            this.optRandomSyllableStrings.Text = "Random Syllable Strings";
            this.mainMenuToolTips.SetToolTip(this.optRandomSyllableStrings, "Create string consisting of one or more syllables");
            this.optRandomSyllableStrings.UseVisualStyleBackColor = true;
            this.optRandomSyllableStrings.CheckedChanged += new System.EventHandler(this.optRandomStrings_CheckedChanged);
            // 
            // optRandomStrings
            // 
            this.optRandomStrings.AutoSize = true;
            this.optRandomStrings.Location = new System.Drawing.Point(18, 10);
            this.optRandomStrings.Name = "optRandomStrings";
            this.optRandomStrings.Size = new System.Drawing.Size(100, 17);
            this.optRandomStrings.TabIndex = 0;
            this.optRandomStrings.TabStop = true;
            this.optRandomStrings.Text = "Random Strings";
            this.mainMenuToolTips.SetToolTip(this.optRandomStrings, "Create strings consisting of letters, numbers or punctuation marks or a mix of al" +
        "l types");
            this.optRandomStrings.UseVisualStyleBackColor = true;
            this.optRandomStrings.CheckedChanged += new System.EventHandler(this.optRandomStrings_CheckedChanged);
            // 
            // optRepeatingStrings
            // 
            this.optRepeatingStrings.AutoSize = true;
            this.optRepeatingStrings.Location = new System.Drawing.Point(18, 290);
            this.optRepeatingStrings.Name = "optRepeatingStrings";
            this.optRepeatingStrings.Size = new System.Drawing.Size(109, 17);
            this.optRepeatingStrings.TabIndex = 4;
            this.optRepeatingStrings.TabStop = true;
            this.optRepeatingStrings.Text = "Repeating Strings";
            this.mainMenuToolTips.SetToolTip(this.optRepeatingStrings, "Create string consisting of one letter or a phrase repeated a given number of tim" +
        "es");
            this.optRepeatingStrings.UseVisualStyleBackColor = true;
            this.optRepeatingStrings.CheckedChanged += new System.EventHandler(this.optRandomStrings_CheckedChanged);
            // 
            // txtMaxNumSyllableStrings
            // 
            this.txtMaxNumSyllableStrings.Location = new System.Drawing.Point(392, 30);
            this.txtMaxNumSyllableStrings.Name = "txtMaxNumSyllableStrings";
            this.txtMaxNumSyllableStrings.Size = new System.Drawing.Size(51, 20);
            this.txtMaxNumSyllableStrings.TabIndex = 4;
            this.mainMenuToolTips.SetToolTip(this.txtMaxNumSyllableStrings, "Maximum Number of Strings:");
            // 
            // optSyllableUCLC
            // 
            this.optSyllableUCLC.AutoSize = true;
            this.optSyllableUCLC.Location = new System.Drawing.Point(3, 3);
            this.optSyllableUCLC.Name = "optSyllableUCLC";
            this.optSyllableUCLC.Size = new System.Drawing.Size(53, 17);
            this.optSyllableUCLC.TabIndex = 0;
            this.optSyllableUCLC.TabStop = true;
            this.optSyllableUCLC.Text = "UCLC";
            this.mainMenuToolTips.SetToolTip(this.optSyllableUCLC, "Upper case first letter followed by lower case");
            this.optSyllableUCLC.UseVisualStyleBackColor = true;
            // 
            // optSyllableUC
            // 
            this.optSyllableUC.AutoSize = true;
            this.optSyllableUC.Location = new System.Drawing.Point(114, 3);
            this.optSyllableUC.Name = "optSyllableUC";
            this.optSyllableUC.Size = new System.Drawing.Size(40, 17);
            this.optSyllableUC.TabIndex = 2;
            this.optSyllableUC.TabStop = true;
            this.optSyllableUC.Text = "UC";
            this.mainMenuToolTips.SetToolTip(this.optSyllableUC, "Upper case letters only");
            this.optSyllableUC.UseVisualStyleBackColor = true;
            // 
            // optSyllableLC
            // 
            this.optSyllableLC.AutoSize = true;
            this.optSyllableLC.Location = new System.Drawing.Point(68, 3);
            this.optSyllableLC.Name = "optSyllableLC";
            this.optSyllableLC.Size = new System.Drawing.Size(38, 17);
            this.optSyllableLC.TabIndex = 1;
            this.optSyllableLC.TabStop = true;
            this.optSyllableLC.Text = "LC";
            this.mainMenuToolTips.SetToolTip(this.optSyllableLC, "Lower case letters only");
            this.optSyllableLC.UseVisualStyleBackColor = true;
            // 
            // lblMaxNumStrings
            // 
            this.lblMaxNumStrings.AutoSize = true;
            this.lblMaxNumStrings.Location = new System.Drawing.Point(223, 38);
            this.lblMaxNumStrings.Name = "lblMaxNumStrings";
            this.lblMaxNumStrings.Size = new System.Drawing.Size(141, 13);
            this.lblMaxNumStrings.TabIndex = 3;
            this.lblMaxNumStrings.Text = "Maximum Number of Strings:";
            this.mainMenuToolTips.SetToolTip(this.lblMaxNumStrings, "Maximum number of strings to generate");
            // 
            // txtMaxNumStrings
            // 
            this.txtMaxNumStrings.Location = new System.Drawing.Point(392, 34);
            this.txtMaxNumStrings.Name = "txtMaxNumStrings";
            this.txtMaxNumStrings.Size = new System.Drawing.Size(51, 20);
            this.txtMaxNumStrings.TabIndex = 4;
            this.mainMenuToolTips.SetToolTip(this.txtMaxNumStrings, "Maximum number of strings to generate");
            // 
            // optRepeatHEX
            // 
            this.optRepeatHEX.AutoSize = true;
            this.optRepeatHEX.Location = new System.Drawing.Point(184, 25);
            this.optRepeatHEX.Name = "optRepeatHEX";
            this.optRepeatHEX.Size = new System.Drawing.Size(47, 17);
            this.optRepeatHEX.TabIndex = 8;
            this.optRepeatHEX.TabStop = true;
            this.optRepeatHEX.Text = "HEX";
            this.mainMenuToolTips.SetToolTip(this.optRepeatHEX, "Hexadecimal numbers only (0-9, A-F)");
            this.optRepeatHEX.UseVisualStyleBackColor = true;
            // 
            // optRepeatDEC
            // 
            this.optRepeatDEC.AutoSize = true;
            this.optRepeatDEC.Location = new System.Drawing.Point(137, 25);
            this.optRepeatDEC.Name = "optRepeatDEC";
            this.optRepeatDEC.Size = new System.Drawing.Size(47, 17);
            this.optRepeatDEC.TabIndex = 7;
            this.optRepeatDEC.TabStop = true;
            this.optRepeatDEC.Text = "DEC";
            this.mainMenuToolTips.SetToolTip(this.optRepeatDEC, "Number 0 through 9 only");
            this.optRepeatDEC.UseVisualStyleBackColor = true;
            // 
            // optRepeatUC
            // 
            this.optRepeatUC.AutoSize = true;
            this.optRepeatUC.Location = new System.Drawing.Point(91, 25);
            this.optRepeatUC.Name = "optRepeatUC";
            this.optRepeatUC.Size = new System.Drawing.Size(40, 17);
            this.optRepeatUC.TabIndex = 6;
            this.optRepeatUC.TabStop = true;
            this.optRepeatUC.Text = "UC";
            this.mainMenuToolTips.SetToolTip(this.optRepeatUC, "Upper case letters only");
            this.optRepeatUC.UseVisualStyleBackColor = true;
            // 
            // optRepeatLC
            // 
            this.optRepeatLC.AutoSize = true;
            this.optRepeatLC.Location = new System.Drawing.Point(47, 25);
            this.optRepeatLC.Name = "optRepeatLC";
            this.optRepeatLC.Size = new System.Drawing.Size(38, 17);
            this.optRepeatLC.TabIndex = 5;
            this.optRepeatLC.TabStop = true;
            this.optRepeatLC.Text = "LC";
            this.mainMenuToolTips.SetToolTip(this.optRepeatLC, "Lower case letters only");
            this.optRepeatLC.UseVisualStyleBackColor = true;
            // 
            // optRepeatAL
            // 
            this.optRepeatAL.AutoSize = true;
            this.optRepeatAL.Location = new System.Drawing.Point(3, 25);
            this.optRepeatAL.Name = "optRepeatAL";
            this.optRepeatAL.Size = new System.Drawing.Size(38, 17);
            this.optRepeatAL.TabIndex = 4;
            this.optRepeatAL.TabStop = true;
            this.optRepeatAL.Text = "AL";
            this.mainMenuToolTips.SetToolTip(this.optRepeatAL, "Letters only (upper and lower case)");
            this.optRepeatAL.UseVisualStyleBackColor = true;
            // 
            // optRepeatANX
            // 
            this.optRepeatANX.AutoSize = true;
            this.optRepeatANX.Location = new System.Drawing.Point(164, 3);
            this.optRepeatANX.Name = "optRepeatANX";
            this.optRepeatANX.Size = new System.Drawing.Size(47, 17);
            this.optRepeatANX.TabIndex = 3;
            this.optRepeatANX.TabStop = true;
            this.optRepeatANX.Text = "ANX";
            this.mainMenuToolTips.SetToolTip(this.optRepeatANX, "Alpha/Numeric Plus Punctuation Characters");
            this.optRepeatANX.UseVisualStyleBackColor = true;
            // 
            // optRepeatAN
            // 
            this.optRepeatAN.AutoSize = true;
            this.optRepeatAN.Location = new System.Drawing.Point(3, 3);
            this.optRepeatAN.Name = "optRepeatAN";
            this.optRepeatAN.Size = new System.Drawing.Size(40, 17);
            this.optRepeatAN.TabIndex = 0;
            this.optRepeatAN.TabStop = true;
            this.optRepeatAN.Text = "AN";
            this.mainMenuToolTips.SetToolTip(this.optRepeatAN, "Alpha/Numeric String");
            this.optRepeatAN.UseVisualStyleBackColor = true;
            // 
            // txtMaxRepeatOutputLength
            // 
            this.txtMaxRepeatOutputLength.Location = new System.Drawing.Point(389, 59);
            this.txtMaxRepeatOutputLength.Name = "txtMaxRepeatOutputLength";
            this.txtMaxRepeatOutputLength.Size = new System.Drawing.Size(51, 20);
            this.txtMaxRepeatOutputLength.TabIndex = 5;
            this.mainMenuToolTips.SetToolTip(this.txtMaxRepeatOutputLength, "Maximum number of characters in each string");
            // 
            // lblMaxRepeatOutputLength
            // 
            this.lblMaxRepeatOutputLength.AutoSize = true;
            this.lblMaxRepeatOutputLength.Location = new System.Drawing.Point(220, 63);
            this.lblMaxRepeatOutputLength.Name = "lblMaxRepeatOutputLength";
            this.lblMaxRepeatOutputLength.Size = new System.Drawing.Size(125, 13);
            this.lblMaxRepeatOutputLength.TabIndex = 4;
            this.lblMaxRepeatOutputLength.Text = "Maximum Output Length:";
            this.mainMenuToolTips.SetToolTip(this.lblMaxRepeatOutputLength, "Maximum number of characters in each string");
            // 
            // lblMinRepeatOutputLength
            // 
            this.lblMinRepeatOutputLength.AutoSize = true;
            this.lblMinRepeatOutputLength.Location = new System.Drawing.Point(0, 63);
            this.lblMinRepeatOutputLength.Name = "lblMinRepeatOutputLength";
            this.lblMinRepeatOutputLength.Size = new System.Drawing.Size(119, 13);
            this.lblMinRepeatOutputLength.TabIndex = 2;
            this.lblMinRepeatOutputLength.Text = "Minimum Output Length";
            this.mainMenuToolTips.SetToolTip(this.lblMinRepeatOutputLength, "Minimum number of characters in each string");
            // 
            // txtMinRepeatOutputLength
            // 
            this.txtMinRepeatOutputLength.Location = new System.Drawing.Point(161, 59);
            this.txtMinRepeatOutputLength.Name = "txtMinRepeatOutputLength";
            this.txtMinRepeatOutputLength.Size = new System.Drawing.Size(51, 20);
            this.txtMinRepeatOutputLength.TabIndex = 3;
            this.mainMenuToolTips.SetToolTip(this.txtMinRepeatOutputLength, "Minimum number of characters in each string");
            // 
            // optANUC
            // 
            this.optANUC.AutoSize = true;
            this.optANUC.Location = new System.Drawing.Point(47, 3);
            this.optANUC.Name = "optANUC";
            this.optANUC.Size = new System.Drawing.Size(55, 17);
            this.optANUC.TabIndex = 1;
            this.optANUC.TabStop = true;
            this.optANUC.Text = "ANUC";
            this.mainMenuToolTips.SetToolTip(this.optANUC, "UpperCase Alpha and Numeric String");
            this.optANUC.UseVisualStyleBackColor = true;
            // 
            // optANLC
            // 
            this.optANLC.AutoSize = true;
            this.optANLC.Location = new System.Drawing.Point(105, 3);
            this.optANLC.Name = "optANLC";
            this.optANLC.Size = new System.Drawing.Size(53, 17);
            this.optANLC.TabIndex = 2;
            this.optANLC.TabStop = true;
            this.optANLC.Text = "ANLC";
            this.mainMenuToolTips.SetToolTip(this.optANLC, "LowerCase Alpha and Numeric String");
            this.optANLC.UseVisualStyleBackColor = true;
            // 
            // optRepeatANLC
            // 
            this.optRepeatANLC.AutoSize = true;
            this.optRepeatANLC.Location = new System.Drawing.Point(105, 3);
            this.optRepeatANLC.Name = "optRepeatANLC";
            this.optRepeatANLC.Size = new System.Drawing.Size(53, 17);
            this.optRepeatANLC.TabIndex = 2;
            this.optRepeatANLC.TabStop = true;
            this.optRepeatANLC.Text = "ANLC";
            this.mainMenuToolTips.SetToolTip(this.optRepeatANLC, "LowerCase Alpha and Numeric String");
            this.optRepeatANLC.UseVisualStyleBackColor = true;
            // 
            // optRepeatANUC
            // 
            this.optRepeatANUC.AutoSize = true;
            this.optRepeatANUC.Location = new System.Drawing.Point(47, 3);
            this.optRepeatANUC.Name = "optRepeatANUC";
            this.optRepeatANUC.Size = new System.Drawing.Size(55, 17);
            this.optRepeatANUC.TabIndex = 1;
            this.optRepeatANUC.TabStop = true;
            this.optRepeatANUC.Text = "ANUC";
            this.mainMenuToolTips.SetToolTip(this.optRepeatANUC, "UpperCase Alpha and Numeric String");
            this.optRepeatANUC.UseVisualStyleBackColor = true;
            // 
            // cboRegexReplacement
            // 
            this.cboRegexReplacement.FormattingEnabled = true;
            this.cboRegexReplacement.Items.AddRange(new object[] {
            "",
            "$1-$2",
            "$1-$2-$3",
            "$1-$2-$3-$4",
            "$1-$2-$3-$4-$5"});
            this.cboRegexReplacement.Location = new System.Drawing.Point(117, 111);
            this.cboRegexReplacement.Name = "cboRegexReplacement";
            this.cboRegexReplacement.Size = new System.Drawing.Size(323, 21);
            this.cboRegexReplacement.TabIndex = 13;
            this.mainMenuToolTips.SetToolTip(this.cboRegexReplacement, "Use Regex Pattern and Replacement if you wish to format the random output.");
            // 
            // cboRegexPattern
            // 
            this.cboRegexPattern.FormattingEnabled = true;
            this.cboRegexPattern.Items.AddRange(new object[] {
            "",
            "(\\w{4})(\\w{4})",
            "(\\w{4})(\\w{4})(\\w{4})",
            "(\\w{4})(\\w{4})(\\w{4})(\\w{4})",
            "(\\w{4})(\\w{4})(\\w{4})(\\w{4})(\\w{4})",
            "(\\w{2})(\\w{10})"});
            this.cboRegexPattern.Location = new System.Drawing.Point(117, 86);
            this.cboRegexPattern.Name = "cboRegexPattern";
            this.cboRegexPattern.Size = new System.Drawing.Size(323, 21);
            this.cboRegexPattern.TabIndex = 11;
            this.mainMenuToolTips.SetToolTip(this.cboRegexPattern, "Use Regex Pattern and Replacement if you wish to format the random output.");
            // 
            // lblRegexReplacement
            // 
            this.lblRegexReplacement.AutoSize = true;
            this.lblRegexReplacement.Location = new System.Drawing.Point(3, 115);
            this.lblRegexReplacement.Name = "lblRegexReplacement";
            this.lblRegexReplacement.Size = new System.Drawing.Size(107, 13);
            this.lblRegexReplacement.TabIndex = 12;
            this.lblRegexReplacement.Text = "Regex Replacement;";
            this.mainMenuToolTips.SetToolTip(this.lblRegexReplacement, "Use Regex Pattern and Replacement if you wish to format the random output.");
            // 
            // lblRegexPattern
            // 
            this.lblRegexPattern.AutoSize = true;
            this.lblRegexPattern.Location = new System.Drawing.Point(3, 89);
            this.lblRegexPattern.Name = "lblRegexPattern";
            this.lblRegexPattern.Size = new System.Drawing.Size(78, 13);
            this.lblRegexPattern.TabIndex = 10;
            this.lblRegexPattern.Text = "Regex Pattern:";
            this.mainMenuToolTips.SetToolTip(this.lblRegexPattern, "Use Regex Pattern and Replacement if you wish to format the random output.");
            // 
            // lblDataMaskName
            // 
            this.lblDataMaskName.AutoSize = true;
            this.lblDataMaskName.Location = new System.Drawing.Point(9, 65);
            this.lblDataMaskName.Name = "lblDataMaskName";
            this.lblDataMaskName.Size = new System.Drawing.Size(90, 13);
            this.lblDataMaskName.TabIndex = 0;
            this.lblDataMaskName.Text = "Data Mask Name";
            // 
            // txtDataMaskName
            // 
            this.txtDataMaskName.Location = new System.Drawing.Point(12, 81);
            this.txtDataMaskName.Name = "txtDataMaskName";
            this.txtDataMaskName.Size = new System.Drawing.Size(371, 20);
            this.txtDataMaskName.TabIndex = 1;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(514, 165);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(93, 37);
            this.cmdSave.TabIndex = 4;
            this.cmdSave.Text = "&Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdPreview
            // 
            this.cmdPreview.Location = new System.Drawing.Point(514, 303);
            this.cmdPreview.Name = "cmdPreview";
            this.cmdPreview.Size = new System.Drawing.Size(93, 34);
            this.cmdPreview.TabIndex = 5;
            this.cmdPreview.Text = "&Preview";
            this.cmdPreview.UseVisualStyleBackColor = true;
            this.cmdPreview.Click += new System.EventHandler(this.cmdPreview_Click);
            // 
            // pnlStringDomain
            // 
            this.pnlStringDomain.Controls.Add(this.pnlRepeatingStrings);
            this.pnlStringDomain.Controls.Add(this.optRepeatingStrings);
            this.pnlStringDomain.Controls.Add(this.pnlRandomSyllables);
            this.pnlStringDomain.Controls.Add(this.pnlRandomStrings);
            this.pnlStringDomain.Controls.Add(this.optRandomSyllableStrings);
            this.pnlStringDomain.Controls.Add(this.optRandomStrings);
            this.pnlStringDomain.Location = new System.Drawing.Point(12, 107);
            this.pnlStringDomain.Name = "pnlStringDomain";
            this.pnlStringDomain.Size = new System.Drawing.Size(496, 489);
            this.pnlStringDomain.TabIndex = 2;
            // 
            // pnlRepeatingStrings
            // 
            this.pnlRepeatingStrings.Controls.Add(this.txtMaxRepeatOutputLength);
            this.pnlRepeatingStrings.Controls.Add(this.lblMaxRepeatOutputLength);
            this.pnlRepeatingStrings.Controls.Add(this.lblMinRepeatOutputLength);
            this.pnlRepeatingStrings.Controls.Add(this.txtMinRepeatOutputLength);
            this.pnlRepeatingStrings.Controls.Add(this.lblTextToRepeat);
            this.pnlRepeatingStrings.Controls.Add(this.pnlRepeatRandomCharacterType);
            this.pnlRepeatingStrings.Controls.Add(this.txtTextToRepeat);
            this.pnlRepeatingStrings.Controls.Add(this.optRepeatText);
            this.pnlRepeatingStrings.Controls.Add(this.optRepeatRandomCharacter);
            this.pnlRepeatingStrings.Controls.Add(this.txtMaxNumRepeats);
            this.pnlRepeatingStrings.Controls.Add(this.lblMaxNumRepeats);
            this.pnlRepeatingStrings.Controls.Add(this.lblMinNumRepeats);
            this.pnlRepeatingStrings.Controls.Add(this.txtMinNumRepeats);
            this.pnlRepeatingStrings.Location = new System.Drawing.Point(30, 313);
            this.pnlRepeatingStrings.Name = "pnlRepeatingStrings";
            this.pnlRepeatingStrings.Size = new System.Drawing.Size(455, 164);
            this.pnlRepeatingStrings.TabIndex = 5;
            // 
            // lblTextToRepeat
            // 
            this.lblTextToRepeat.AutoSize = true;
            this.lblTextToRepeat.Location = new System.Drawing.Point(223, 98);
            this.lblTextToRepeat.Name = "lblTextToRepeat";
            this.lblTextToRepeat.Size = new System.Drawing.Size(81, 13);
            this.lblTextToRepeat.TabIndex = 9;
            this.lblTextToRepeat.Text = "Text to Repeat:";
            this.lblTextToRepeat.Visible = false;
            // 
            // pnlRepeatRandomCharacterType
            // 
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatANLC);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatANUC);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatHEX);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatDEC);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatUC);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatLC);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatAL);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatANX);
            this.pnlRepeatRandomCharacterType.Controls.Add(this.optRepeatAN);
            this.pnlRepeatRandomCharacterType.Location = new System.Drawing.Point(114, 3);
            this.pnlRepeatRandomCharacterType.Name = "pnlRepeatRandomCharacterType";
            this.pnlRepeatRandomCharacterType.Size = new System.Drawing.Size(335, 45);
            this.pnlRepeatRandomCharacterType.TabIndex = 1;
            // 
            // txtTextToRepeat
            // 
            this.txtTextToRepeat.Location = new System.Drawing.Point(114, 94);
            this.txtTextToRepeat.Name = "txtTextToRepeat";
            this.txtTextToRepeat.Size = new System.Drawing.Size(102, 20);
            this.txtTextToRepeat.TabIndex = 7;
            // 
            // optRepeatText
            // 
            this.optRepeatText.AutoSize = true;
            this.optRepeatText.Location = new System.Drawing.Point(4, 96);
            this.optRepeatText.Name = "optRepeatText";
            this.optRepeatText.Size = new System.Drawing.Size(87, 17);
            this.optRepeatText.TabIndex = 6;
            this.optRepeatText.TabStop = true;
            this.optRepeatText.Text = "Repeat Text:";
            this.optRepeatText.UseVisualStyleBackColor = true;
            this.optRepeatText.CheckedChanged += new System.EventHandler(this.optRepeatRandomCharacter_CheckedChanged);
            // 
            // optRepeatRandomCharacter
            // 
            this.optRepeatRandomCharacter.AutoSize = true;
            this.optRepeatRandomCharacter.Location = new System.Drawing.Point(7, 3);
            this.optRepeatRandomCharacter.Name = "optRepeatRandomCharacter";
            this.optRepeatRandomCharacter.Size = new System.Drawing.Size(103, 30);
            this.optRepeatRandomCharacter.TabIndex = 0;
            this.optRepeatRandomCharacter.TabStop = true;
            this.optRepeatRandomCharacter.Text = "Repeat Random\r\nCharacter";
            this.optRepeatRandomCharacter.UseVisualStyleBackColor = true;
            this.optRepeatRandomCharacter.CheckedChanged += new System.EventHandler(this.optRepeatRandomCharacter_CheckedChanged);
            // 
            // txtMaxNumRepeats
            // 
            this.txtMaxNumRepeats.Location = new System.Drawing.Point(391, 128);
            this.txtMaxNumRepeats.Name = "txtMaxNumRepeats";
            this.txtMaxNumRepeats.Size = new System.Drawing.Size(51, 20);
            this.txtMaxNumRepeats.TabIndex = 13;
            // 
            // lblMaxNumRepeats
            // 
            this.lblMaxNumRepeats.AutoSize = true;
            this.lblMaxNumRepeats.Location = new System.Drawing.Point(222, 132);
            this.lblMaxNumRepeats.Name = "lblMaxNumRepeats";
            this.lblMaxNumRepeats.Size = new System.Drawing.Size(143, 13);
            this.lblMaxNumRepeats.TabIndex = 12;
            this.lblMaxNumRepeats.Text = "Maximum Number of Outputs";
            // 
            // lblMinNumRepeats
            // 
            this.lblMinNumRepeats.AutoSize = true;
            this.lblMinNumRepeats.Location = new System.Drawing.Point(1, 132);
            this.lblMinNumRepeats.Name = "lblMinNumRepeats";
            this.lblMinNumRepeats.Size = new System.Drawing.Size(140, 13);
            this.lblMinNumRepeats.TabIndex = 10;
            this.lblMinNumRepeats.Text = "Minimum Number of Outputs";
            // 
            // txtMinNumRepeats
            // 
            this.txtMinNumRepeats.Location = new System.Drawing.Point(165, 128);
            this.txtMinNumRepeats.Name = "txtMinNumRepeats";
            this.txtMinNumRepeats.Size = new System.Drawing.Size(51, 20);
            this.txtMinNumRepeats.TabIndex = 11;
            // 
            // pnlRandomSyllables
            // 
            this.pnlRandomSyllables.Controls.Add(this.panel2);
            this.pnlRandomSyllables.Controls.Add(this.txtMaxNumSyllableStrings);
            this.pnlRandomSyllables.Controls.Add(this.lblMaxNumSyllableStrings);
            this.pnlRandomSyllables.Controls.Add(this.txtMinNumSyllableStrings);
            this.pnlRandomSyllables.Controls.Add(this.lblMinNumSyllableStrings);
            this.pnlRandomSyllables.Controls.Add(this.txtSyllableStringMaximumLength);
            this.pnlRandomSyllables.Controls.Add(this.lblSyllableStringMaximumLength);
            this.pnlRandomSyllables.Controls.Add(this.txtSyllableStringMinimumLength);
            this.pnlRandomSyllables.Controls.Add(this.lblSyllableStringMinimumLength);
            this.pnlRandomSyllables.Location = new System.Drawing.Point(30, 198);
            this.pnlRandomSyllables.Name = "pnlRandomSyllables";
            this.pnlRandomSyllables.Size = new System.Drawing.Size(455, 85);
            this.pnlRandomSyllables.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.optSyllableUC);
            this.panel2.Controls.Add(this.optSyllableLC);
            this.panel2.Controls.Add(this.optSyllableUCLC);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(446, 27);
            this.panel2.TabIndex = 0;
            // 
            // lblMaxNumSyllableStrings
            // 
            this.lblMaxNumSyllableStrings.AutoSize = true;
            this.lblMaxNumSyllableStrings.Location = new System.Drawing.Point(223, 34);
            this.lblMaxNumSyllableStrings.Name = "lblMaxNumSyllableStrings";
            this.lblMaxNumSyllableStrings.Size = new System.Drawing.Size(141, 13);
            this.lblMaxNumSyllableStrings.TabIndex = 3;
            this.lblMaxNumSyllableStrings.Text = "Maximum Number of Strings:";
            // 
            // lblMinNumSyllableStrings
            // 
            this.lblMinNumSyllableStrings.AutoSize = true;
            this.lblMinNumSyllableStrings.Location = new System.Drawing.Point(3, 34);
            this.lblMinNumSyllableStrings.Name = "lblMinNumSyllableStrings";
            this.lblMinNumSyllableStrings.Size = new System.Drawing.Size(138, 13);
            this.lblMinNumSyllableStrings.TabIndex = 1;
            this.lblMinNumSyllableStrings.Text = "Minimum Number of Strings:";
            // 
            // pnlRandomStrings
            // 
            this.pnlRandomStrings.Controls.Add(this.cboRegexReplacement);
            this.pnlRandomStrings.Controls.Add(this.cboRegexPattern);
            this.pnlRandomStrings.Controls.Add(this.lblRegexReplacement);
            this.pnlRandomStrings.Controls.Add(this.lblRegexPattern);
            this.pnlRandomStrings.Controls.Add(this.pnlRandomStringsType);
            this.pnlRandomStrings.Controls.Add(this.txtMaxNumStrings);
            this.pnlRandomStrings.Controls.Add(this.lblMaxNumStrings);
            this.pnlRandomStrings.Controls.Add(this.txtMinNumStrings);
            this.pnlRandomStrings.Controls.Add(this.lblMinNumStrings);
            this.pnlRandomStrings.Controls.Add(this.txtStringMaximumLength);
            this.pnlRandomStrings.Controls.Add(this.lblStringMaximumLength);
            this.pnlRandomStrings.Controls.Add(this.lblStringMinimumLength);
            this.pnlRandomStrings.Controls.Add(this.txtStringMinimumLength);
            this.pnlRandomStrings.Location = new System.Drawing.Point(30, 29);
            this.pnlRandomStrings.Name = "pnlRandomStrings";
            this.pnlRandomStrings.Size = new System.Drawing.Size(455, 144);
            this.pnlRandomStrings.TabIndex = 1;
            // 
            // pnlRandomStringsType
            // 
            this.pnlRandomStringsType.Controls.Add(this.optANLC);
            this.pnlRandomStringsType.Controls.Add(this.optANUC);
            this.pnlRandomStringsType.Controls.Add(this.optHEX);
            this.pnlRandomStringsType.Controls.Add(this.optDEC);
            this.pnlRandomStringsType.Controls.Add(this.optUC);
            this.pnlRandomStringsType.Controls.Add(this.optLC);
            this.pnlRandomStringsType.Controls.Add(this.optAL);
            this.pnlRandomStringsType.Controls.Add(this.optANX);
            this.pnlRandomStringsType.Controls.Add(this.optAN);
            this.pnlRandomStringsType.Location = new System.Drawing.Point(3, 3);
            this.pnlRandomStringsType.Name = "pnlRandomStringsType";
            this.pnlRandomStringsType.Size = new System.Drawing.Size(446, 26);
            this.pnlRandomStringsType.TabIndex = 0;
            // 
            // lblNumRandomDataItems
            // 
            this.lblNumRandomDataItems.AutoSize = true;
            this.lblNumRandomDataItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumRandomDataItems.Location = new System.Drawing.Point(511, 352);
            this.lblNumRandomDataItems.Name = "lblNumRandomDataItems";
            this.lblNumRandomDataItems.Size = new System.Drawing.Size(98, 13);
            this.lblNumRandomDataItems.TabIndex = 27;
            this.lblNumRandomDataItems.Text = "Num Preview Items";
            // 
            // txtNumRandomDataItems
            // 
            this.txtNumRandomDataItems.Location = new System.Drawing.Point(514, 368);
            this.txtNumRandomDataItems.Name = "txtNumRandomDataItems";
            this.txtNumRandomDataItems.Size = new System.Drawing.Size(93, 20);
            this.txtNumRandomDataItems.TabIndex = 6;
            // 
            // RandomStringsForm
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdExit;
            this.ClientSize = new System.Drawing.Size(630, 592);
            this.Controls.Add(this.lblNumRandomDataItems);
            this.Controls.Add(this.txtNumRandomDataItems);
            this.Controls.Add(this.pnlStringDomain);
            this.Controls.Add(this.MainFormToolbar);
            this.Controls.Add(this.txtDataMaskName);
            this.Controls.Add(this.lblDataMaskName);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdPreview);
            this.Controls.Add(this.cmdExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RandomStringsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Random String Data Request";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.mainMenuContextMenuStrip.ResumeLayout(false);
            this.MainFormToolbar.ResumeLayout(false);
            this.MainFormToolbar.PerformLayout();
            this.pnlStringDomain.ResumeLayout(false);
            this.pnlStringDomain.PerformLayout();
            this.pnlRepeatingStrings.ResumeLayout(false);
            this.pnlRepeatingStrings.PerformLayout();
            this.pnlRepeatRandomCharacterType.ResumeLayout(false);
            this.pnlRepeatRandomCharacterType.PerformLayout();
            this.pnlRandomSyllables.ResumeLayout(false);
            this.pnlRandomSyllables.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlRandomStrings.ResumeLayout(false);
            this.pnlRandomStrings.PerformLayout();
            this.pnlRandomStringsType.ResumeLayout(false);
            this.pnlRandomStringsType.PerformLayout();
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
        private System.Windows.Forms.Label lblStringMinimumLength;
        private System.Windows.Forms.TextBox txtStringMinimumLength;
        private System.Windows.Forms.Label lblStringMaximumLength;
        private System.Windows.Forms.TextBox txtStringMaximumLength;
        private System.Windows.Forms.Label lblSyllableStringMinimumLength;
        private System.Windows.Forms.TextBox txtSyllableStringMinimumLength;
        private System.Windows.Forms.Label lblDataMaskName;
        private System.Windows.Forms.TextBox txtDataMaskName;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdPreview;
        private System.Windows.Forms.Panel pnlStringDomain;
        private System.Windows.Forms.RadioButton optRandomSyllableStrings;
        private System.Windows.Forms.RadioButton optRandomStrings;
        private System.Windows.Forms.Panel pnlRandomSyllables;
        private System.Windows.Forms.Panel pnlRandomStrings;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsReset;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsResetEmptyMruList;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsResetReloadOriginalRequestDefinitions;
        private System.Windows.Forms.TextBox txtSyllableStringMaximumLength;
        private System.Windows.Forms.Label lblSyllableStringMaximumLength;
        private System.Windows.Forms.Panel pnlRandomStringsType;
        private System.Windows.Forms.RadioButton optANX;
        private System.Windows.Forms.RadioButton optAN;
        private System.Windows.Forms.TextBox txtMinNumStrings;
        private System.Windows.Forms.Label lblMinNumStrings;
        private System.Windows.Forms.RadioButton optLC;
        private System.Windows.Forms.RadioButton optAL;
        private System.Windows.Forms.RadioButton optDEC;
        private System.Windows.Forms.RadioButton optUC;
        private System.Windows.Forms.RadioButton optHEX;
        private System.Windows.Forms.TextBox txtMinNumSyllableStrings;
        private System.Windows.Forms.Label lblMinNumSyllableStrings;
        private System.Windows.Forms.RadioButton optRepeatingStrings;
        private System.Windows.Forms.TextBox txtMaxNumSyllableStrings;
        private System.Windows.Forms.Label lblMaxNumSyllableStrings;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton optSyllableUCLC;
        private System.Windows.Forms.RadioButton optSyllableUC;
        private System.Windows.Forms.RadioButton optSyllableLC;
        private System.Windows.Forms.TextBox txtMaxNumStrings;
        private System.Windows.Forms.Label lblMaxNumStrings;
        private System.Windows.Forms.Panel pnlRepeatingStrings;
        private System.Windows.Forms.TextBox txtMaxNumRepeats;
        private System.Windows.Forms.Label lblMaxNumRepeats;
        private System.Windows.Forms.Label lblMinNumRepeats;
        private System.Windows.Forms.TextBox txtMinNumRepeats;
        private System.Windows.Forms.TextBox txtTextToRepeat;
        private System.Windows.Forms.RadioButton optRepeatText;
        private System.Windows.Forms.RadioButton optRepeatRandomCharacter;
        private System.Windows.Forms.Label lblTextToRepeat;
        private System.Windows.Forms.Panel pnlRepeatRandomCharacterType;
        private System.Windows.Forms.RadioButton optRepeatHEX;
        private System.Windows.Forms.RadioButton optRepeatDEC;
        private System.Windows.Forms.RadioButton optRepeatUC;
        private System.Windows.Forms.RadioButton optRepeatLC;
        private System.Windows.Forms.RadioButton optRepeatAL;
        private System.Windows.Forms.RadioButton optRepeatANX;
        private System.Windows.Forms.RadioButton optRepeatAN;
        private System.Windows.Forms.TextBox txtMaxRepeatOutputLength;
        private System.Windows.Forms.Label lblMaxRepeatOutputLength;
        private System.Windows.Forms.Label lblMinRepeatOutputLength;
        private System.Windows.Forms.TextBox txtMinRepeatOutputLength;
        private System.Windows.Forms.Label lblNumRandomDataItems;
        private System.Windows.Forms.TextBox txtNumRandomDataItems;
        private System.Windows.Forms.RadioButton optANLC;
        private System.Windows.Forms.RadioButton optANUC;
        private System.Windows.Forms.RadioButton optRepeatANLC;
        private System.Windows.Forms.RadioButton optRepeatANUC;
        private System.Windows.Forms.ComboBox cboRegexReplacement;
        private System.Windows.Forms.ComboBox cboRegexPattern;
        private System.Windows.Forms.Label lblRegexReplacement;
        private System.Windows.Forms.Label lblRegexPattern;
    }
}

