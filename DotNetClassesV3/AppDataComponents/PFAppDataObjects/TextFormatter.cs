//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.Common;

namespace PFAppDataObjects
{
    /// <summary>
    /// Format form text into printable format.
    /// </summary>
    public class TextFormatter
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TextFormatter()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Formatter for form printing.
        /// </summary>
        /// <param name="frm">Form to be printed.</param>
        /// <returns>String value.</returns>
        public string FormatFormTextToString(Form frm)
        {
            StringBuilder textToPrint = new StringBuilder();
            List<ControlValue> ctls = new List<ControlValue>();
            int maxlenDescription = 0;


            try
            {
                GetValuesFromForm(ctls, frm, "00");

                foreach (ControlValue cv in ctls)
                {
                    if (cv.Description.Length > maxlenDescription)
                        maxlenDescription = cv.Description.Length;
                }

                //IntComparer ic = new IntComparer();
                StringComparer ic = new StringComparer();
                ctls.Sort(ic);

                textToPrint.Length = 0;
                foreach (ControlValue cv in ctls)
                {
                    string desc = cv.Description + new String(' ', maxlenDescription);
                    desc = desc.Substring(0, maxlenDescription) + " ";
                    textToPrint.Append(desc);
                    textToPrint.Append(cv.Value);
                    textToPrint.Append(Environment.NewLine);
                    textToPrint.Append(Environment.NewLine);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                textToPrint.Append("Error occurred while formatting form for print:\r\n");
                textToPrint.Append(_msg.ToString());
            }
            finally
            {
                ;
            }
        
            return textToPrint.ToString();

        }//end method

        private void GetValuesFromForm(List<ControlValue> ctls, Control container, string containerTabIndex)
        {
            TextBox txt = null;
            CheckBox chk = null;
            RadioButton rad = null;
            ComboBox cbo = null;
            Panel pnl = null;
            GroupBox grp = null;
            TabControl tab = null;
            TabPage pag = null;
            CheckedListBox chklist = null;
            ListBox lst = null;
            FlowLayoutPanel flow = null;
            ListView lvw = null;
            
            foreach (Control ctl in container.Controls)
            {
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    ControlValue val = new ControlValue();
                    //val.TabIndex = txt.TabIndex;
                    val.TabIndex = containerTabIndex + txt.TabIndex.ToString("00");
                    val.Value = txt.Text;
                    val.Description = "Field" + val.TabIndex.ToString();
                    Label lbl = container.Controls.Find("lbl" + txt.Name.Replace("txt", ""), true).FirstOrDefault() as Label;
                    if (lbl != null)
                        val.Description = lbl.Text.Replace(Environment.NewLine, " ");
                    else
                        val.Description = txt.Tag != null ? txt.Tag.ToString() : txt.Name; // val.Description;
                    ctls.Add(val);
                }
                else if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    ControlValue val = new ControlValue();
                    //val.TabIndex = cbo.TabIndex;
                    val.TabIndex = containerTabIndex + cbo.TabIndex.ToString("00");
                    val.Value = cbo.Text;
                    val.Description = "Field" + val.TabIndex.ToString();
                    Label lbl = container.Controls.Find("lbl" + cbo.Name.Replace("cbo", ""), true).FirstOrDefault() as Label;
                    if (lbl != null)
                        val.Description = lbl.Text.Replace(Environment.NewLine, " ");
                    else
                        val.Description = cbo.Tag != null ? txt.Tag.ToString() : cbo.Name; // val.Description;
                    ctls.Add(val);
                }
                else if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    ControlValue val = new ControlValue();
                    val.Value = chk.Checked.ToString();
                    val.Description = chk.Text.Replace(Environment.NewLine, " ");
                    val.TabIndex = containerTabIndex + chk.TabIndex.ToString("00");
                    ctls.Add(val);
                }
                else if (ctl is RadioButton)
                {
                    rad = (RadioButton)ctl;
                    ControlValue val = new ControlValue();
                    val.Value = rad.Checked.ToString();
                    val.Description = rad.Text.Replace(Environment.NewLine, " ");
                    val.TabIndex = containerTabIndex + rad.TabIndex.ToString("00");
                    ctls.Add(val);
                }
                else if (ctl is Panel && ctl is TabPage == false)
                {
                    pnl = (Panel)ctl;
                    string newTabIndex = containerTabIndex + pnl.TabIndex.ToString("00");
                    GetValuesFromForm(ctls, pnl, newTabIndex);
                }
                else if (ctl is FlowLayoutPanel)
                {
                    flow = (FlowLayoutPanel)ctl;
                    string newTabIndex = containerTabIndex + grp.TabIndex.ToString("00");
                    GetValuesFromForm(ctls, flow, newTabIndex);
                }
                else if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    string newTabIndex = containerTabIndex + grp.TabIndex.ToString("00");
                    GetValuesFromForm(ctls, grp, newTabIndex);
                }
                else if (ctl is TabControl)
                {
                    tab = (TabControl)ctl;
                    string newTabIndex = containerTabIndex + tab.TabIndex.ToString("00");
                    GetValuesFromForm(ctls, tab, newTabIndex);
                }
                else if (ctl is TabPage)
                {
                    pag = (TabPage)ctl;
                    ControlValue val = new ControlValue();
                    string tabPageNumber = GetTabPageNumber(pag);
                    val.Value = pag.Text;
                    val.TabIndex = containerTabIndex + tabPageNumber;
                    val.Description = "Form Tab" + " " + val.TabIndex;
                    ctls.Add(val);
                    string newTabIndex = containerTabIndex + tabPageNumber;
                    GetValuesFromForm(ctls, pag, newTabIndex);
                }
                else if (ctl is CheckedListBox)
                {
                    chklist = (CheckedListBox)ctl;
                    ControlValue val = new ControlValue();
                    val.Value = GetCheckedListBoxSelectedItems(chklist);
                    val.Description = chklist.Name.Replace("chklist", "");
                    val.TabIndex = containerTabIndex + chklist.TabIndex.ToString("00");
                    ctls.Add(val);
                }
                else if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    ControlValue val = new ControlValue();
                    val.Value = GetListBoxSelectedItems(chklist);
                    val.Description = lst.Name.Replace("lst", "");
                    val.TabIndex = containerTabIndex + lst.TabIndex.ToString("00");
                    ctls.Add(val);
                }
                else if (ctl is ListView)
                {
                    lvw = (ListView)ctl;
                    ControlValue val = new ControlValue();
                    val.Value = GetListViewItems(lvw);
                    val.Description = lvw.Name.Replace("listview", "");
                    val.TabIndex = containerTabIndex + lvw.TabIndex.ToString("00");
                    ctls.Add(val);
                }
                else
                {
                    ;
                }
            }//end foreach

        }

        private string GetTabPageNumber(TabPage pag)
        {
            string pageNumber = "00";

            TabControl tab = (TabControl)pag.Parent;

            for (int i = 0; i < tab.TabPages.Count; i++)
            {
                if (tab.TabPages[i].Name == pag.Name)
                {
                    pageNumber = i.ToString("00");
                    break;
                }
            }

            return pageNumber;
        }

        private string GetCheckedListBoxSelectedItems(CheckedListBox chklist)
        {
            string selectedValues = string.Empty;

            _msg.Length = 0;

            if (chklist.CheckedItems.Count > 0)
            {
                _msg.Append("Num selected values: ");
                _msg.Append(chklist.CheckedItems.Count.ToString());

                for (int i = 0; i < chklist.CheckedItems.Count; i++)
                {
                    _msg.Append(Environment.NewLine);
                    _msg.Append(chklist.CheckedItems[i].ToString());
                }
            }
            else
            {
                _msg.Append("No values selected.");
            }

            selectedValues = _msg.ToString();

            return selectedValues;
        }

        private string GetListBoxSelectedItems(ListBox lst)
        {
            string selectedValues = string.Empty;

            _msg.Length = 0;

            if (lst.SelectedItems.Count > 0)
            {
                _msg.Append("Num selected values: ");
                _msg.Append(lst.SelectedItems.Count.ToString());

                for (int i = 0; i < lst.SelectedItems.Count; i++)
                {
                    _msg.Append(Environment.NewLine);
                    _msg.Append(lst.SelectedItems[i].ToString());
                }
            }
            else
            {
                _msg.Append("No values selected.");
            }

            selectedValues = _msg.ToString();

            return selectedValues;
        }

        private string GetListViewItems(ListView lvw)
        {
            string listviewValues = string.Empty;

            _msg.Length = 0;

            if (lvw.Items.Count > 0)
            {
                if (lvw.Columns.Count > 0)
                {
                    for (int c = 0; c < lvw.Columns.Count; c++)
                    {
                        if (c > 0)
                        {
                            _msg.Append(", ");
                        }
                        _msg.Append(lvw.Columns[c].Text);
                        
                    }
                    _msg.Append(Environment.NewLine);
                }

                for (int i = 0; i < lvw.Items.Count; i++)
                {
                    for (int si = 0; si < lvw.Items[i].SubItems.Count; si++)
                    {
                        if (si > 0)
                        {
                            _msg.Append(", ");
                            _msg.Append(lvw.Items[i].SubItems[si].Text);
                        }
                        else
                        {
                            _msg.Append(lvw.Items[i].Text);
                        }
                    }
                    _msg.Append(Environment.NewLine);
                }
            }

            listviewValues = _msg.ToString();

            return listviewValues;
        }

        private class StringComparer : IComparer<ControlValue>
        {
            public int Compare(ControlValue cv1, ControlValue cv2)
            {
                return cv1.TabIndex.CompareTo(cv2.TabIndex);
            }
        }

        /// <summary>
        /// Stores form text to a DataTable object.
        /// </summary>
        /// <param name="frm">Form to process.</param>
        /// <returns>DataTable object.</returns>
        public DataTable FormatFormTextToDataTable(Form frm)
        {
            DataTable dt = new DataTable();
            List<ControlValue> ctls = new List<ControlValue>();


            try
            {
                GetValuesFromForm(ctls, frm, "00");

                //IntComparer ic = new IntComparer();
                StringComparer ic = new StringComparer();
                ctls.Sort(ic);

                //create columns

                DataColumn descColumn = new DataColumn("Description");
                dt.Columns.Add(descColumn);
                DataColumn valueColumn = new DataColumn("Value");
                dt.Columns.Add(valueColumn);

                //create data rows
                foreach (ControlValue cv in ctls)
                {
                    DataRow row = dt.NewRow();
                    row["Description"] = cv.Description;
                    row["Value"] = cv.Value;
                    dt.Rows.Add(row);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error occurred while formatting form text to a DataTable for print:\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw;
            }
            finally
            {
                ;
            }

            return dt;

        }//end method


        /// <summary>
        /// Sets the font to use for form text output.
        /// </summary>
        /// <param name="container">Control on the form.</param>
        /// <param name="fnt">Font to use.</param>
        public void SetFormTextValuesFont(Control container, Font fnt)
        {
            TextBox txt = null;
            ComboBox cbo = null;
            Panel pnl = null;
            GroupBox grp = null;
            TabControl tab = null;
            TabPage pag = null;
            CheckedListBox chklist = null;
            ListBox lst = null;
            FlowLayoutPanel flow = null;
            ListView lvw = null;

            foreach (Control ctl in container.Controls)
            {
                //Console.WriteLine(container.Name + "  /  " + ctl.Name + " <- " + fnt.ToString());
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.Font = fnt;
                }
                else if (ctl is ComboBox)
                {
                    cbo = (ComboBox)ctl;
                    cbo.Font = fnt;
                }
                else if (ctl is Panel && ctl is TabPage == false)
                {
                    pnl = (Panel)ctl;
                    SetFormTextValuesFont(pnl, fnt);
                }
                else if (ctl is FlowLayoutPanel)
                {
                    flow = (FlowLayoutPanel)ctl;
                    SetFormTextValuesFont(flow, fnt);
                }
                else if (ctl is GroupBox)
                {
                    grp = (GroupBox)ctl;
                    SetFormTextValuesFont(grp, fnt);
                }
                else if (ctl is TabControl)
                {
                    tab = (TabControl)ctl;
                    SetFormTextValuesFont(tab, fnt);
                }
                else if (ctl is TabPage)
                {
                    pag = (TabPage)ctl;
                    SetFormTextValuesFont(pag, fnt);
                }
                else if (ctl is CheckedListBox)
                {
                    chklist = (CheckedListBox)ctl;
                    chklist.Font = fnt;
                }
                else if (ctl is ListBox)
                {
                    lst = (ListBox)ctl;
                    lst.Font = fnt;
                }
                else if (ctl is ListView)
                {
                    lvw = (ListView)ctl;
                    lvw.Font = fnt;
                }
                else
                {
                    ;
                }
            }//end foreach

            container.Refresh();
        
        }



    }//end class
}//end namespace
