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
using PFDataOutputGrid;

namespace PFAppDataObjects
{
    /// <summary>
    /// Prints contents of a windows form.
    /// </summary>
    public class FormPrinter
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private Form _formToPrint = null;
        private DataTable _dt = null;
        private DataOutputGridFormExp _frm = null;


        //private variables for properties
        private string _pageTitle = AppGlobals.AppInfo.AssemblyDescription;
        private string _pageSubTitle = string.Empty;
        private string _pageFooter = AppGlobals.AppInfo.AssemblyProduct;
        private bool _showPageNumbers = true;
        private bool _showTotalPageNumber = true;
        System.Drawing.Printing.PageSettings _savePageSettings = new System.Drawing.Printing.PageSettings();

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public FormPrinter(Form formToPrint)
        {
            InitInstance(formToPrint);
        }

        private void InitInstance(Form formToPrint)
        {
            _formToPrint = formToPrint;
            _pageSubTitle = "Contents of " + _formToPrint.Text + " form";
            _frm = new DataOutputGridFormExp();
        }

        //properties

        /// <summary>
        /// PageTitle Property.
        /// </summary>
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }
            set
            {
                _pageTitle = value;
            }
        }

        /// <summary>
        /// PageSubTitle Property.
        /// </summary>
        public string PageSubTitle
        {
            get
            {
                return _pageSubTitle;
            }
            set
            {
                _pageSubTitle = value;
            }
        }

        /// <summary>
        /// PageFooter Property.
        /// </summary>
        public string PageFooter
        {
            get
            {
                return _pageFooter;
            }
            set
            {
                _pageFooter = value;
            }
        }

        /// <summary>
        /// ShowPageNumbers Property.
        /// </summary>
        public bool ShowPageNumbers
        {
            get
            {
                return _showPageNumbers;
            }
            set
            {
                _showPageNumbers = value;
            }
        }

        /// <summary>
        /// ShowTotalPageNumber Property.
        /// </summary>
        public bool ShowTotalPageNumber
        {
            get
            {
                return _showTotalPageNumber;
            }
            set
            {
                _showTotalPageNumber = value;
            }
        }

        /// <summary>
        /// Page settings to use when printing.
        /// </summary>
        public System.Drawing.Printing.PageSettings SavePageSettings
        {
            get
            {
                return _savePageSettings;
            }
            set
            {
                _savePageSettings = value;
            }
        }



        //methods

        /// <summary>
        /// Displays the Windows page settings form.
        /// </summary>
        public void ShowPageSettings()
        {
            _frm.ShowPageSettings();
        }

        /// <summary>
        /// Prints the form.
        /// </summary>
        /// <param name="showPreview">Set to true to show Print Preview.</param>
        /// <param name="showPrintDialog">Set to true to show Print Dialog.</param>
        public void Print(bool showPreview, bool showPrintDialog)
        {
            DataSet ds = new DataSet("outputDataSet");
            DataTable dt = null;
            //convert form labels and values into a data table
            TextFormatter txtFormatter = new TextFormatter();
            _dt = txtFormatter.FormatFormTextToDataTable(_formToPrint);

            try
            {
                if (String.IsNullOrEmpty(_dt.TableName))
                    _dt.TableName = "formOutputTable";
                dt = _dt.Copy();
                ds.Tables.Add(dt);

                _frm.DataGridBindingSource.DataSource = ds;
                _frm.DataGridBindingSource.DataMember = dt.TableName;
                _frm.outputDataGridBindingNavigator.BindingSource = _frm.DataGridBindingSource;
                _frm.outputDataGrid.DataSource = _frm.DataGridBindingSource;
                _frm.EnableExportMenu = true;
                _frm.PageTitle = this.PageTitle;
                _frm.PageSubTitle = this.PageSubTitle;
                _frm.PageFooter = this.PageFooter;
                _frm.ShowPageNumbers = this.ShowPageNumbers;
                _frm.ShowTotalPageNumber = this.ShowTotalPageNumber;

                if (showPreview)
                {
                    _frm.ShowPrintPreview();
                }
                else
                {
                    if (showPrintDialog)
                    {
                        _frm.ShowPrintDialog();
                    }
                    else
                    {
                        _frm.GridViewPrint(false, false);
                    }
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

        }

    }//end class
}//end namespace
