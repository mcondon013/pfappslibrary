using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppGlobals;

namespace PFRandomDataForms
{
    /// <summary>
    /// Class to display an form where date request name can be changed.
    /// </summary>
    public partial class RandomDataRequestRenameForm : Form
    {
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        /// <summary>
        /// Constructor
        /// </summary>

        public RandomDataRequestRenameForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>

        public RandomDataRequestRenameForm(Control parent)
        {
            this.Parent = parent;
            InitializeComponent();
        }

        //properties
        /// <summary>
        /// Caption Property.
        /// </summary>
        public string Caption
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        /// <summary>
        /// Original text before rename.
        /// </summary>
        public string OriginalText
        {
            get
            {
                return this.txtOriginalData.Text;
            }
            set
            {
                this.txtOriginalData.Text = value;
            }
        }

        /// <summary>
        /// OriginalTextLabel property.
        /// </summary>
        public string OriginalTextLabel
        {
            get
            {
                return this.lblOriginalData.Text;
            }
            set
            {
                this.lblOriginalData.Text = value;
            }
        }

        /// <summary>
        /// InputText Property.
        /// </summary>
        public string InputText
        {
            get
            {
                return this.txtInputData.Text;
            }
            set
            {
                this.txtInputData.Text = value;
            }
        }

        /// <summary>
        /// InputTextLabel Property.
        /// </summary>
        public string InputTextLabel
        {
            get
            {
                return this.lblInputData.Text;
            }
            set
            {
                this.lblInputData.Text = value;
            }
        }

        /// <summary>
        /// OkButtonText Property.
        /// </summary>
        public string OkButtonText
        {
            get
            {
                return this.cmdOK.Text;
            }
            set
            {
                this.cmdOK.Text = value;
            }
        }

        /// <summary>
        /// CancelButtonText Property.
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return this.cmdCancel.Text;
            }
            set
            {
                this.cmdCancel.Text = value;
            }
        }





        //button click events

        
        
        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }




        //common form processing routines
        private void InitializeForm()
        {
            this.SetStyle(ControlStyles.DoubleBuffer |
                           ControlStyles.UserPaint |
                           ControlStyles.AllPaintingInWmPaint,
                           true);
            this.UpdateStyles();

            EnableFormControls();
        }

        /// <summary>
        /// Method to hide form.
        /// </summary>
        public void HideForm()
        {
            this.Hide();
        }

        /// <summary>
        /// Method to close form.
        /// </summary>
        public void CloseForm()
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.InputText = this.txtInputData.Text;
            this.DialogResult = DialogResult.OK;
            this.CloseForm();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.CloseForm();
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

    }//end class
}//end namespace
