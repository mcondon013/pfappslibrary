using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using AppGlobals;

namespace TestprogTextBoxRuler
{
    public partial class ApplicationOptionsForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        private struct SavedBooleanControl
        {
            public string Name;
            public bool Checked;
        }
        private List<SavedBooleanControl> _saveFormCheckBoxes = new List<SavedBooleanControl>();
        private List<SavedBooleanControl> _saveFormRadioButtons = new List<SavedBooleanControl>();


        public ApplicationOptionsForm()
        {
            InitializeComponent();
        }

        //Button click events
        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (ProcessOptions() == true)
                CloseForm();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            ProcessOptions();
        }

        private void cmdHelp_Click(object sender, EventArgs e)
        {
            DisplayHelp();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        //menu click events
        private void ShowAllOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAllOptionsForm();
        }


        //Common form routines

        private void CloseForm()
        {
            this.Hide();
        }

        //Form events
        private void AppOptionsForm_Load(object sender, EventArgs e)
        {
            string formCaption = string.Empty;

            _msg.Length = 0;
            _msg.Append(AppInfo.AssemblyProduct);
            _msg.Append(" Application Options");
            formCaption = _msg.ToString();

            this.Text = formCaption;

            InitForm();
        }

        private void InitForm()
        {
            LoadAppConfigItems();


            foreach (Control ctl in this.Controls)
            {
                if (ctl is CheckBox)
                {
                    CheckBox chk = (CheckBox)ctl;
                    SavedBooleanControl saveChk = new SavedBooleanControl();
                    saveChk.Name = chk.Name;
                    saveChk.Checked = chk.Checked;
                    _saveFormCheckBoxes.Add(saveChk);
                }
                if (ctl is RadioButton)
                {
                    RadioButton rdo = (RadioButton)ctl;
                    SavedBooleanControl saveRdo = new SavedBooleanControl();
                    saveRdo.Name = rdo.Name;
                    saveRdo.Checked = rdo.Checked;
                    _saveFormRadioButtons.Add(saveRdo);
                }
            }//end foreach

        }//end method

        private void LoadAppConfigItems()
        {

            this.chkSaveErrorMessagesToErrorLog.Checked = AppConfig.GetBooleanValueFromConfigFile("SaveErrorMessagesToErrorLog", "false");
            this.txtApplicationLogFileName.Text = System.Configuration.ConfigurationManager.AppSettings["ApplicationLogFileName"];

        }

        private bool ProcessOptions()
        {
            bool err = false;
            bool ret = true;
            bool formHasChanged = false;
            int inx = 0;
            int maxInx = -1;

            System.Configuration.Configuration config =
                System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            try
            {

                foreach (Control ctl in this.Controls)
                {
                    if (ctl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)ctl;
                        SavedBooleanControl saveChk;
                        maxInx = _saveFormCheckBoxes.Count - 1;
                        for (inx = 0; inx <= maxInx; inx++)
                        {
                            saveChk = _saveFormCheckBoxes[inx];
                            if (chk.Name == saveChk.Name)
                                if (chk.Checked != saveChk.Checked)
                                {
                                    formHasChanged = true;
                                    break;
                                }
                        }
                    }
                    if (ctl is RadioButton)
                    {
                        RadioButton rdo = (RadioButton)ctl;
                        SavedBooleanControl saveRdo;
                        maxInx = _saveFormRadioButtons.Count - 1;
                        for (inx = 0; inx <= maxInx; inx++)
                        {
                            saveRdo = _saveFormRadioButtons[inx];
                            if (rdo.Name == saveRdo.Name)
                                if (rdo.Checked != saveRdo.Checked)
                                {
                                    formHasChanged = true;
                                    break;
                                }
                        }
                    }
                    if (ctl is TextBox)
                    {
                        TextBox txt = (TextBox)ctl;
                        if (txt.Modified)
                            formHasChanged = true;
                    }
                    if (formHasChanged)
                        break;
                }//end foreach

                if (formHasChanged)
                {
                    //update all configuration file item values
                    ResetAllConfigurationFileItemValues(config);
                }

            }
            catch (System.Exception ex)
            {
                err = true;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
            }



            if (err)
            {
                if (this.chkSaveErrorMessagesToErrorLog.Checked)
                    AppGlobals.AppMessages.DisplayErrorMessage(_msg.ToString(), true);
                else
                    AppGlobals.AppMessages.DisplayErrorMessage(_msg.ToString(), false);
                ret = false;
            }
            else
            {
                if (formHasChanged)
                {
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                    _msg.Length = 0;
                    _msg.Append("Application options have been updated.");
                    if (this.chkSaveErrorMessagesToErrorLog.Checked)
                        AppGlobals.AppMessages.DisplayInfoMessage(_msg.ToString(), true);
                    else
                        AppGlobals.AppMessages.DisplayInfoMessage(_msg.ToString(), false);
                    this.Refresh();

                    //re-save all input values for future checks
                    _saveFormCheckBoxes.Clear();
                    _saveFormRadioButtons.Clear();
                    foreach (Control ctl in this.Controls)
                    {
                        if (ctl is CheckBox)
                        {
                            CheckBox chk = (CheckBox)ctl;
                            SavedBooleanControl saveChk = new SavedBooleanControl();
                            saveChk.Name = chk.Name;
                            saveChk.Checked = chk.Checked;
                            _saveFormCheckBoxes.Add(saveChk);
                        }
                        if (ctl is RadioButton)
                        {
                            RadioButton rdo = (RadioButton)ctl;
                            SavedBooleanControl saveRdo = new SavedBooleanControl();
                            saveRdo.Name = rdo.Name;
                            saveRdo.Checked = rdo.Checked;
                            _saveFormRadioButtons.Add(saveRdo);
                        }
                        if (ctl is TextBox)
                        {
                            TextBox txt = (TextBox)ctl;
                            txt.Modified = false;
                        }
                    }//end foreach
                }
                ret = true;
            }
            return ret;
        }


        private void ResetAllConfigurationFileItemValues(System.Configuration.Configuration config)
        {

            config.AppSettings.Settings["SaveErrorMessagesToErrorLog"].Value = this.chkSaveErrorMessagesToErrorLog.Checked.ToString();
            config.AppSettings.Settings["ApplicationLogFileName"].Value = this.txtApplicationLogFileName.Text;

        }

        private void DisplayHelp()
        {
            string messageCaption = string.Empty;
            string messageText = string.Empty;

            _msg.Length = 0;
            _msg.Append("Help for ");
            _msg.Append(AppGlobals.AppInfo.AssemblyProduct);
            messageCaption = _msg.ToString();

            _msg.Length = 0;
            _msg.Append("Help not yet implemented for ");
            _msg.Append(AppGlobals.AppInfo.AssemblyProduct);
            _msg.Append(".");
            messageText = _msg.ToString();

            MessageBox.Show(messageText, messageCaption);

        }

        private void ShowAllOptionsForm()
        {
            ShowAllAppOptionsForm frm = new ShowAllAppOptionsForm();

            frm.ShowDialog();

            frm.Close();

            LoadAppConfigItems();
        }

    }//end class
}//end namespace
