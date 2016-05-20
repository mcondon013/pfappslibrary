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

namespace PFAppDataObjects
{
    /// <summary>
    /// Provides various operations for textbox and richtextbox controls.
    /// </summary>
    public class TextEditor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        
        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TextEditor()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Defines operations on the specified textbox.
        /// </summary>
        /// <param name="frm">WinForms object containing the textbox.</param>
        /// <param name="edOp">Operation to perform on the text.</param>
        public void TextBoxEditor(Form frm, EditOperation edOp)
        {
            TextBox txt = null;
            Control ctl = frm.ActiveControl;
            if (ctl == null)
                return;
            if (ctl is TextBox == false)
                return;
            txt = (TextBox)ctl;

            switch (edOp)
            {
                case EditOperation.Cut:
                    txt.Cut();
                    break;
                case EditOperation.Copy:
                    txt.Copy();
                    break;
                case EditOperation.Paste:
                    txt.Paste();
                    break;
                case EditOperation.SelectAll:
                    txt.SelectAll();
                    break;
                case EditOperation.Delete:
                    txt.SelectedText = string.Empty;
                    break;
                default:
                    break;
            }
            txt.Focus();

        }

        /// <summary>
        /// Defines operations on the specified richtextbox.
        /// </summary>
        /// <param name="frm">WinForms object containing the richtextbox.</param>
        /// <param name="edOp">Operation to perform on the text.</param>
        public void RichTextBoxEditor(Form frm, EditOperation edOp)
        {
            RichTextBox txt = null;
            Control ctl = frm.ActiveControl;
            if (ctl == null)
                return;
            if (ctl is RichTextBox == false)
                return;
            txt = (RichTextBox)ctl;

            switch (edOp)
            {
                case EditOperation.Cut:
                    txt.Cut();
                    break;
                case EditOperation.Copy:
                    txt.Copy();
                    break;
                case EditOperation.Paste:
                    txt.Paste();
                    break;
                case EditOperation.SelectAll:
                    txt.SelectAll();
                    break;
                case EditOperation.Delete:
                    txt.SelectedText = string.Empty;
                    break;
                default:
                    break;
            }
            txt.Focus();

        }
    }//end class
}//end namespace
