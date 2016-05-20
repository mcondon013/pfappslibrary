using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using System.Configuration;
using System.IO;
using PFFileSystemObjects;
using PFPrinterObjects;
using PFAppUtils;
using PFTextFiles;
using PFDataAccessObjects;
using PFAppDataObjects;

namespace InitWinFormsAppWithSepDLL
{
#pragma warning disable 1591

    public partial class UserOptionsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private bool _saveErrorMessagesToErrorLog = true;
        private string _applicationLogFileName = "app.log";

        PFTextPrinter _printer = new PFTextPrinter();
        private TextFormatter _textFormatter = new TextFormatter();

        private string _helpFilePath = string.Empty;

        //see InitWinFormsAppWithDialogs (profast templates) and TestprogAppUtils (winapplib solution)
        //for examples of using PFFolderBrowserDialog, PFOpenFileDialog, PFSaveFileDialog usage
        private PFFolderBrowserDialog _folderBrowserDialog = new PFFolderBrowserDialog();

        private class PFUserOptions
        {
            private string _userOption1 = string.Empty;
            private string _userOption2 = string.Empty;
            private bool _saveFormLocationsOnExit = false;
            private string _userOption4 = string.Empty;

            /// <summary>
            /// UserOption1 Property.
            /// </summary>
            public string UserOption1
            {
                get
                {
                    return _userOption1;
                }
                set
                {
                    _userOption1 = value;
                }
            }

            /// <summary>
            /// UserOption2 Property.
            /// </summary>
            public string UserOption2
            {
                get
                {
                    return _userOption2;
                }
                set
                {
                    _userOption2 = value;
                }
            }

            /// <summary>
            /// SaveFormLocationsOnExit Property.
            /// </summary>
            public bool SaveFormLocationsOnExit
            {
                get
                {
                    return _saveFormLocationsOnExit;
                }
                set
                {
                    _saveFormLocationsOnExit = value;
                }
            }

            /// <summary>
            /// UserOption4 Property.
            /// </summary>
            public string UserOption4
            {
                get
                {
                    return _userOption4;
                }
                set
                {
                    _userOption4 = value;
                }
            }



        }//end private class

        PFUserOptions _userOptions = new PFUserOptions();


        /// <summary>
        /// Constructor
        /// </summary>
        public UserOptionsForm()
        {
            InitializeComponent();
        }

        //button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            UpdateAndCloseForm();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            UpdateUserConfigItems();
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            LoadUserConfigItems();
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            DisplayHelp();
            this.DialogResult = DialogResult.None;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void cmdSetUserOption1_Click(object sender, EventArgs e)
        {
            SetUserOption1();
        }

        private void cmdSetUserOption2_Click(object sender, EventArgs e)
        {
            SetUserOption2();
        }

        //menu item clicks

        private void mnuSettingsAccept_Click(object sender, EventArgs e)
        {
            UpdateAndCloseForm();
        }

        private void mnuSettingsPageSetup_Click(object sender, EventArgs e)
        {
            ShowPageSettings();
        }

        private void mnuSettingsPrint_Click(object sender, EventArgs e)
        {
            SettingsPrint(false, true);
         }

        private void mnuSettingsPrintPreview_Click(object sender, EventArgs e)
        {
            SettingsPrint(true, true);
        }

        private void mnuSettingsRestore_Click(object sender, EventArgs e)
        {
            DialogResult res = AppMessages.DisplayMessage("Do you want to replace all user settings with their original installation values?", "User Settings ...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
            if (res == DialogResult.Yes)
            {
                LoadDefaultUserConfigItems();
                UpdateUserConfigItems(true);
            }
        }

        private void mnuSettingsCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }


        //form events
        private void UserOptionsForm_Load(object sender, EventArgs e)
        {
            string formCaption = string.Empty;

            _msg.Length = 0;
            _msg.Append(AppInfo.AssemblyProduct);
            _msg.Append(" User Options");
            formCaption = _msg.ToString();

            this.Text = formCaption;

            SetHelpFile();

            InitializeForm();
        }

        //common form processing routines

        private void SetHelpFile()
        {

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "InitWinFormsHelpFile.chm");
            string helpFilePath = PFFile.FormatFilePath(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        public void InitializeForm()
        {
            _saveErrorMessagesToErrorLog = AppConfig.GetBooleanValueFromConfigFile("SaveErrorMessagesToErrorLog", "false");
            _applicationLogFileName = System.Configuration.ConfigurationManager.AppSettings["ApplicationLogFileName"];

            LoadUserConfigItems();

            EnableFormControls();
        }

        private void LoadUserConfigItems()
        {
            _userOptions.UserOption1 = Properties.Settings.Default.UserOption1;
            _userOptions.UserOption2 = Properties.Settings.Default.UserOption2;
            _userOptions.SaveFormLocationsOnExit = Properties.Settings.Default.SaveFormLocationsOnExit;
            _userOptions.UserOption4 = Properties.Settings.Default.UserOption4;

            this.txtUserOption1.Text = _userOptions.UserOption1;
            this.txtUserOption2.Text = _userOptions.UserOption2;
            this.chkSaveFormLocationsOnExit.Checked = _userOptions.SaveFormLocationsOnExit;
            this.cboUserOption4.Text = _userOptions.UserOption4;

        }

        private void LoadDefaultUserConfigItems()
        {
            _userOptions.UserOption1 = Properties.Settings.Default.Properties["UserOption1"].DefaultValue.ToString();
            _userOptions.UserOption2 = Properties.Settings.Default.Properties["UserOption2"].DefaultValue.ToString();
            _userOptions.SaveFormLocationsOnExit = Convert.ToBoolean(Properties.Settings.Default.Properties["SaveFormLocationsOnExit"].DefaultValue);
            _userOptions.UserOption4 = Properties.Settings.Default.Properties["UserOption4"].DefaultValue.ToString();

            this.txtUserOption1.Text = _userOptions.UserOption1;
            this.txtUserOption2.Text = _userOptions.UserOption2;
            this.chkSaveFormLocationsOnExit.Checked = _userOptions.SaveFormLocationsOnExit;
            this.cboUserOption4.Text = _userOptions.UserOption4;

        }

        public void HideForm()
        {
            this.Hide();
        }

        public void CloseForm()
        {
            this.Hide();
        }

        private void UpdateAndCloseForm()
        {
            bool res = false;
            res = UpdateUserConfigItems();
            if (res == true)
                CloseForm();
        }

        private void EnableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = true;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = true;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = true;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = true;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = true;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = true;
                }

            }//end foreach
        }//end method

        private void DisableFormControls()
        {
            TextBox txt = null;
            CheckBox chk = null;
            Button btn = null;
            MenuStrip mnu = null;
            GroupBox grp = null;
            Panel pnl = null;

            foreach (Control ctl in this.Controls)
            {
                if (ctl is MenuStrip)
                {
                    mnu = (MenuStrip)ctl;
                    foreach (ToolStripItem itm in mnu.Items)
                    {
                        itm.Enabled = false;
                    }
                }
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ctl.Enabled = false;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Enabled = false;
                }
                if (ctl is Button)
                {
                    btn = (Button)ctl;
                    btn.Enabled = false;
                }
                if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    grp.Enabled = false;
                }
                if (ctl is Panel)
                {
                    pnl = (Panel)ctl;
                    pnl.Enabled = false;
                }

            }//end foreach control

        }

        //Application routines

        private void DisplayHelp()
        {
            if (File.Exists(_helpFilePath))
            {
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Change a User Setting");
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_helpFilePath);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }
        }

        private DialogResult ShowFolderBrowserDialog(ref string selectedFolder)
        {
            DialogResult res = DialogResult.None;

            _folderBrowserDialog.InitialFolderPath = selectedFolder;
            _folderBrowserDialog.ShowNewFolderButton = false;

            res = _folderBrowserDialog.ShowFolderBrowserDialog();

            if (res != DialogResult.Cancel)
            {
                selectedFolder = _folderBrowserDialog.InitialFolderPath;
            }


            return res;
        }


        private void SetUserOption1()
        {
            string initFolder = this.txtUserOption1.Text;
            ShowFolderBrowserDialog(ref initFolder);
            this.txtUserOption1.Text = initFolder;
        }

        private void SetUserOption2()
        {
            string initFolder = this.txtUserOption2.Text;
            ShowFolderBrowserDialog(ref initFolder);
            this.txtUserOption2.Text = initFolder;
        }

        private bool UpdateUserConfigItems()
        {
            return UpdateUserConfigItems(false);
        }

        private bool UpdateUserConfigItems(bool forceUpdate)
        {
            bool updateSuccessful = true;
            int numErrors = 0;
            int numUpdates = 0;

            _msg.Length = 0;

            if (this.txtUserOption1.Text.ToUpper().Trim() != _userOptions.UserOption1.ToUpper().Trim()
                || forceUpdate == true)
            {
                if(Directory.Exists(this.txtUserOption1.Text.ToUpper().Trim())
                    || this.txtUserOption1.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    Properties.Settings.Default["UserOption1"] = this.txtUserOption1.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("UserOption1 does not exist: ");
                    _msg.Append(this.txtUserOption1.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.txtUserOption2.Text.ToUpper().Trim() != _userOptions.UserOption2.ToUpper().Trim()
                || forceUpdate == true)
            {
                if(Directory.Exists(this.txtUserOption2.Text.ToUpper().Trim())
                    || this.txtUserOption2.Text.ToUpper().Trim().Length == 0)
                {
                    numUpdates++;
                    Properties.Settings.Default["UserOption2"] = this.txtUserOption2.Text;
                }
                else
                {
                    numErrors++;
                    _msg.Append("UserOption2 does not exist: ");
                    _msg.Append(this.txtUserOption2.Text);
                    _msg.Append(Environment.NewLine);
                }
            }

            if (this.chkSaveFormLocationsOnExit.Checked != _userOptions.SaveFormLocationsOnExit
                || forceUpdate == true)
            {
                numUpdates++;
                Properties.Settings.Default["SaveFormLocationsOnExit"] = this.chkSaveFormLocationsOnExit.Checked;
            }

            if (this.cboUserOption4.Text != _userOptions.UserOption4
                || forceUpdate == true)
            {
                numUpdates++;
                Properties.Settings.Default["UserOption4"] = this.cboUserOption4.Text;
            }


            if (numErrors > 0)
            {
                AppMessages.DisplayErrorMessage(_msg.ToString());
            }

            _msg.Length = 0;
            if (numUpdates > 0)
            {
                //save the changes
                Properties.Settings.Default.Save();
                if (numErrors > 0)
                {
                    _msg.Append("Update results: ");
                    _msg.Append(numUpdates.ToString());
                    _msg.Append(" items updated.");
                    _msg.Append(numErrors.ToString());
                    _msg.Append(" update errors.");
                }
                else
                {
                    _msg.Append(numUpdates.ToString());
                    _msg.Append(" items were successfully updated.");
                }
            }
            else
            {
                if (numErrors > 0)
                {
                    _msg.Append(numErrors.ToString());
                    _msg.Append(" encountered during update.");
                }
                else
                {
                    _msg.Append("No updates were needed. No data changes found.");
                }
            }

            this.lblUpdateResults.Text = _msg.ToString();

            if(numErrors==0)
                updateSuccessful = true;
            else
                updateSuccessful = false;

            return updateSuccessful;
        }


        private void ShowPageSettings()
        {
            _printer.ShowPageSettings();
        }

        private void SettingsPrint(bool preview, bool showPrintDialog)
        {
            string textToPrint = string.Empty;

            _printer.Title = this.Text;
            _printer.ShowPageNumbers = true;
            Font fnt = new System.Drawing.Font("Lucida Console", (float)10.0);
            _printer.Font = fnt;
            textToPrint = _textFormatter.FormatFormTextToString(this);
            if (textToPrint == string.Empty)
                textToPrint = "Nothing to print.";
            _printer.TextToPrint = textToPrint;
            if (preview)
            {
                _printer.ShowPrintPreview();
            }
            else
            {
                if (showPrintDialog)
                {
                    _printer.ShowPrintDialog();
                }
                else
                {
                    _printer.Print();
                }
            }

        }//end method



    }//end class
#pragma warning restore 1591

}//end namespace
