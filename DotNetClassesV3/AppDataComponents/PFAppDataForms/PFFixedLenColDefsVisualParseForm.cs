using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AppGlobals;
using PFTextObjects;

namespace PFAppDataForms
{
    /// <summary>
    /// Form for visual line parser.
    /// </summary>
    public partial class PFFixedLenColDefsVisualParseForm : Form
    {
        private StringBuilder _msg = new StringBuilder();
        private string _helpFilePath = string.Empty;

        private bool _userCancelButtonPressed = false;

        private string _textLine1 = new String('x', 100);
        private string _textLine2 = new String('y', 100);
        //private bool _columnNamesOnFirstLine = true;
        //private int _expectedLineWidth = -1;

#pragma warning disable 1591
        public struct ColNameAndLength
        {
            public string colName;
            public int colLength;
        }
#pragma warning restore 1591

        /// <summary>
        /// Constuctor
        /// </summary>
        public PFFixedLenColDefsVisualParseForm()
        {
            InitializeComponent();
        }

        //Properties
        /// <summary>
        /// TextLine1 Property.
        /// </summary>
        public string TextLine1
        {
            get
            {
                return _textLine1;
            }
            set
            {
                _textLine1 = value;
            }
        }

        /// <summary>
        /// TextLine2 Property.
        /// </summary>
        public string TextLine2
        {
            get
            {
                return _textLine2;
            }
            set
            {
                _textLine2 = value;
            }
        }

        /// <summary>
        /// ColumnNamesOnFirstLine Property.
        /// </summary>
        public bool ColumnNamesOnFirstLine
        {
            get
            {
                return this.chkColumnNamesOnFirstLine.Checked;
            }
            set
            {
                this.chkColumnNamesOnFirstLine.Checked = value;
            }
        }

        /// <summary>
        /// ExpectedLineWidth Property.
        /// </summary>
        public int ExpectedLineWidth
        {
            get
            {
                return AppTextGlobals.ConvertStringToInt(this.txtExpectedLineWidth.Text, -1);
            }
            set
            {
                this.txtExpectedLineWidth.Text = value.ToString();
            }
        }




        //button click events
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            this.DialogResult = DisplayFieldNamesAndLengths();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            _userCancelButtonPressed = true;
            DialogResult res = CheckCancelRequest();
            if (res == DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                HideForm();
            }
        }

        private void cmdShowColumnOffsets_Click(object sender, EventArgs e)
        {
            ShowFieldOffsets();
        }

        private void toolbarHelp_Click(object sender, EventArgs e)
        {
            ShowHelpFile();
        }

        //form events
        private void WinForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void PFWindowsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && _userCancelButtonPressed == false)
            {
                DialogResult res = CheckCancelRequest();
                if (res == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    this.DialogResult = DialogResult.Ignore;
                }
            }

        }

        private DialogResult CheckCancelRequest()
        {
            DialogResult res = AppMessages.DisplayMessage("Click yes to cancel and discard any changes you may have made to the column specifications.", "Cancel Data Randomizer Criteria ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            return res;
        }




        //common form processing routines

        /// <summary>
        /// Initialize form values.
        /// </summary>
        public void InitializeForm()
        {
            _userCancelButtonPressed = false;

            textRuler.RulerText = this.TextLine1;
            textRuler.RulerTextLinesExtra = this.TextLine2;

            SetHelpFileValues();

            EnableFormControls();

            this.Focus();
        }

        private void SetHelpFileValues()
        {
            string configValue = string.Empty;

            string executableFolder = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            string helpFileName = AppConfig.GetStringValueFromConfigFile("HelpFileName", "PFAppDataComponents.chm");
            string helpFilePath = Path.Combine(executableFolder, helpFileName);
            this.appHelpProvider.HelpNamespace = helpFilePath;
            _helpFilePath = helpFilePath;

        }

        /// <summary>
        /// Renders form invisible.
        /// </summary>
        public void HideForm()
        {
            this.Hide();
        }

        /// <summary>
        /// Renders form visible.
        /// </summary>
        public void CloseForm()
        {
            this.Close();
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

        /// <summary>
        /// GetFieldNamesAndLengths method.
        /// </summary>
        /// <returns>Array of ColNameAndLength objects.</returns>
        public ColNameAndLength[] GetFieldNamesAndLengths()
        {
            ColNameAndLength[] colNamesAndLengths = null;
            string fieldOffsets = string.Empty;
            string colNamesPlusFieldLens = string.Empty;
            int lineLength = AppTextGlobals.ConvertStringToInt(this.txtExpectedLineWidth.Text, 1);

            if (textRuler.FieldOffSets.Count == 0)
            {
                object newValMin = (int)1;
                textRuler.FieldOffSets.Add(newValMin);
                if (lineLength < 1)
                    lineLength = 0;
                object newValMax = lineLength + 1;
                textRuler.FieldOffSets.Add(newValMax);
            }

            textRuler.FieldOffSets.Sort();

            if ((int)textRuler.FieldOffSets[0] != 1)
            {
                object newVal = (int)1;
                textRuler.FieldOffSets.Add(newVal);
            }

            if ((int)textRuler.FieldOffSets[textRuler.FieldOffSets.Count - 1] != (lineLength + 1))
            {
                object newVal = lineLength + 1;
                textRuler.FieldOffSets.Add(newVal);
            }

            textRuler.FieldOffSets.Sort();

            colNamesAndLengths = new ColNameAndLength[textRuler.FieldOffSets.Count];

            int maxInx = textRuler.FieldOffSets.Count - 1;

            for (int i = 0; i < maxInx; i++)
            {
                int startPos = (int)textRuler.FieldOffSets[i] - 1;
                int colLen = (int)textRuler.FieldOffSets[i + 1] - (int)textRuler.FieldOffSets[i];
                string colName = this.TextLine1.Substring(startPos, colLen).Trim();
                if (colName.Trim() == string.Empty)
                    colName = new string('?', colLen);
                string val = this.TextLine1.Substring(startPos, colLen);
                ColNameAndLength cnl = new ColNameAndLength();
                cnl.colName = colName;
                cnl.colLength = colLen;
                colNamesAndLengths[i] = cnl;
            }

            return colNamesAndLengths;
        }

        private DialogResult DisplayFieldNamesAndLengths()
        {
            ColNameAndLength[] colNamesAndLengths = GetFieldNamesAndLengths();
            int maxInx = colNamesAndLengths.Length - 1;
            string values = string.Empty;
            DialogResult res = DialogResult.None;

            for (int i = 0; i < maxInx; i++)
            {
                if (i > 0)
                {
                    values += Environment.NewLine + colNamesAndLengths[i].colName + ", " + colNamesAndLengths[i].colLength;
                }
                else
                {
                    values += colNamesAndLengths[i].colName + ", " + colNamesAndLengths[i].colLength;
                }
            }

            _msg.Length = 0;
            _msg.Append("Are these field names and lengths correct?");
            _msg.Append(Environment.NewLine);
            _msg.Append(Environment.NewLine);
            _msg.Append(values);
            res = AppMessages.DisplayMessage(_msg.ToString(), "Verify Names and Lengths are Correct ...", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            return res;
        }


        //Application routines
        //private void LoadTextLines()
        //{
        //    textRuler.RulerText =           "LNTYPFIELD1         FIELDB            FIELDC                    FIELD_D                     COLHDR_E            FINALCOL       NUMBERS   ";
        //    textRuler.RulerTextLinesExtra = "LINE2xxxxxxxxxxxxxxxyyyyyyyyyyyyyyyyyyzzzzzzzzzzzzzzzzzzzzzzzzzzddddddddddddddddddddddddddddeeeeeeeeeeeeeeeeeeeefffffffffffffff0123456789";
        //}


        private void ShowFieldOffsets()
        {
            string fieldOffsets = string.Empty;
            string colNamesPlusFieldLens = string.Empty;
            int lineLength = AppTextGlobals.ConvertStringToInt(this.txtExpectedLineWidth.Text,1);

            if (textRuler.FieldOffSets.Count == 0)
            {
                object newValMin = (int)1;
                textRuler.FieldOffSets.Add(newValMin);
                object newValMax = lineLength + 1;
                textRuler.FieldOffSets.Add(newValMax);
            }

            textRuler.FieldOffSets.Sort();

            if ((int)textRuler.FieldOffSets[0] != 1)
            {
                object newVal = (int)1;
                textRuler.FieldOffSets.Add(newVal);
            }

            if ((int)textRuler.FieldOffSets[textRuler.FieldOffSets.Count - 1] != (lineLength+1))
            {
                object newVal = lineLength+1;
                textRuler.FieldOffSets.Add(newVal);
            }

            textRuler.FieldOffSets.Sort();

            int maxInx = textRuler.FieldOffSets.Count - 1;

            for (int i = 0; i < maxInx; i++)
            {
                int startPos = (int)textRuler.FieldOffSets[i] - 1;
                int colLen = (int)textRuler.FieldOffSets[i+1] - (int)textRuler.FieldOffSets[i];
                string colName = this.TextLine1.Substring(startPos, colLen).Trim();
                if (colName.Trim() == string.Empty)
                    colName = new string('?', colLen);
                string val = this.TextLine1.Substring(startPos, colLen);
                if (i > 0)
                {
                    //fieldOffsets += Environment.NewLine + textRuler.FieldOffSets[i].ToString();
                    fieldOffsets += Environment.NewLine + colName + ", " + colLen.ToString();
                }
                else
                {
                    fieldOffsets += colName + ", " + colLen.ToString();
                    //fieldOffsets += textRuler.FieldOffSets[i].ToString();
                }
            }

            _msg.Length = 0;
            _msg.Append("Field offsets are: ");
            _msg.Append(Environment.NewLine);
            _msg.Append(fieldOffsets);
            AppMessages.DisplayMessage(_msg.ToString(), "Column Offsets", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowHelpFile()
        {
            if (HelpFileExists())
                Help.ShowHelp(this, _helpFilePath, HelpNavigator.KeywordIndex, "Fixed Length Text Visual Column Definition");
        }

        private bool HelpFileExists()
        {
            bool ret = false;

            if (File.Exists(_helpFilePath))
            {
                ret = true;
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Unable to find Help File: ");
                _msg.Append(_helpFilePath);
                AppMessages.DisplayWarningMessage(_msg.ToString());
            }

            return ret;
        }

    
    }//end class
}//end namespace
