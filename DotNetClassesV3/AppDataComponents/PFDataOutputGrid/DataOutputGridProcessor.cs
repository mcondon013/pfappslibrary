//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using PFCollectionsObjects;
using PFDataAccessObjects;
using PFMessageLogs;

namespace PFDataOutputGrid
{
    /// <summary>
    /// Class to manage operations involving the output data to a dynamically formatted data grid.
    /// </summary>
    public class DataOutputGridProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private DataTable _dt = null;
        private PFList<GridColumnFilter> _gridColumnFilters = new PFList<GridColumnFilter>();
        private bool _showInstalledDatabaseProvidersOnly = true;
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private string _defaultGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\NamesAndLocations";
        private bool _enableExportMenu = true;
        private MessageLog _messageLog = null;

        private string _pageTitle = AppGlobals.AppInfo.AssemblyDescription;
        private string _pageSubTitle = string.Empty;
        private string _pageFooter = AppGlobals.AppInfo.AssemblyProduct;
        private bool _showPageNumbers = true;
        private bool _showTotalPageNumber = true;
        private bool _showRowNumber = false;

        private int _veryLargeDataTableRowCountThreshold = 100000;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public DataOutputGridProcessor()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            AppGlobals.AppConfig.GetIntValueFromConfigFile("VeryLargeDataTableRowCountThreshold", 100000);
        }

        //properties

        /// <summary>
        /// List of columns that should have filter capabilities.
        /// </summary>
        public PFList<GridColumnFilter> GridColumnFilters
        {
            get
            {
                return _gridColumnFilters;
            }
            set
            {
                _gridColumnFilters = value;
            }
        } 


        /// <summary>
        /// DataTable object containing data to be displayed on the output Grid.
        /// </summary>
        public DataTable Data
        {
            get
            {
                return _dt;
            }
            set
            {
                _dt = value;
            }
        }

        /// <summary>
        /// Switch used by database selection controls to determine whether or not to list only installed database providers or to list all supported providers.
        /// </summary>
        public bool ShowInstalledDatabaseProvidersOnly
        {
            get
            {
                return _showInstalledDatabaseProvidersOnly;
            }
            set
            {
                _showInstalledDatabaseProvidersOnly = value;
            }
        }

        /// <summary>
        /// DefaultOutputDatabaseType Property.
        /// </summary>
        public string DefaultOutputDatabaseType
        {
            get
            {
                return _defaultOutputDatabaseType;
            }
            set
            {
                _defaultOutputDatabaseType = value;
            }
        }

        /// <summary>
        /// DefaultOutputDatabaseConnectionString Property.
        /// </summary>
        public string DefaultOutputDatabaseConnectionString
        {
            get
            {
                return _defaultOutputDatabaseConnectionString;
            }
            set
            {
                _defaultOutputDatabaseConnectionString = value;
            }
        }

        /// <summary>
        /// Default folder for storing files created by grid export functions.
        /// </summary>
        public string DefaultGridExportFolder
        {
            get
            {
                return _defaultGridExportFolder;
            }
            set
            {
                _defaultGridExportFolder = value;
            }
        }

        /// <summary>
        /// EnableExportMenu property.
        /// </summary>
        public bool EnableExportMenu
        {
            get
            {
                return _enableExportMenu;
            }
            set
            {
                _enableExportMenu = value;
            }
        }

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }



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
        /// If set to true, column showing row number is added to the dataset being displayed.
        /// </summary>
        /// <remarks>Default is false.</remarks>
        public bool ShowRowNumber
        {
            get
            {
                return _showRowNumber;
            }
            set
            {
                _showRowNumber = value;
            }
        }

        /// <summary>
        /// VeryLargeDataTableRowCountThreshold Property. Determines when temp file processing will be used for datagridview exporting.
        /// </summary>
        public int VeryLargeDataTableRowCountThreshold
        {
            get
            {
                return _veryLargeDataTableRowCountThreshold;
            }
            set
            {
                _veryLargeDataTableRowCountThreshold = value;
            }
        }



        //methods


        /// <summary>
        /// Shows the data contained in the DataTable parameter object.
        /// </summary>
        /// <param name="dt">Object containing data to be displayed.</param>
        public void WriteDataToGrid(DataTable dt)
        {
            this.Data = dt;
            WriteDataToGrid();
        }
        
        /// <summary>
        /// Displays the data grid.
        /// </summary>
        /// <remarks>Displays the data contained in the DataTable object represented by the Data property.</remarks>
        public void WriteDataToGrid()
        {
            DataOutputGridFormExp frm = null;
            DataSet ds = new DataSet("outputDataSet");
            DataTable dt = null;
            bool rowNumberColAlreadyDefined = false;
            DataColumn rowNumberDataColumn = null;

            if (_dt == null)
            {
                _msg.Length = 0;
                _msg.Append("You must specify a DataTable object containing data to display.");
                throw new System.Exception(_msg.ToString());
            }

            try
            {
                if(String.IsNullOrEmpty(_dt.TableName))
                    _dt.TableName = "outputDataTable";
                if (_dt.DataSet != null)
                {
                    dt = _dt.Copy();
                }
                else
                {
                    dt = _dt;
                }
                //if (this.ShowRowNumber)
                //{
                //    DataColumn dc = new DataColumn();
                //    dc.ColumnName = "dgvRowNumber";
                //    dc.DataType = Type.GetType("System.Int32");
                //    //dc.AutoIncrement = true;
                //    //dc.AutoIncrementSeed = 1;
                //    //dc.AutoIncrementStep = 1;
                //    dt.Columns.Add(dc);
                //    dc.SetOrdinal(0);
                //    for (int r = 0; r < dt.Rows.Count; r++)
                //    {
                //        dt.Rows[r][0] = r + 1;
                //    }
                //}

                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.Columns[c].ColumnName == "dgvRowNumber")
                    {
                        rowNumberColAlreadyDefined = true;
                    }
                }

                if ((rowNumberColAlreadyDefined == false && dt.Rows.Count > this.VeryLargeDataTableRowCountThreshold)
                    || (rowNumberColAlreadyDefined == false && this.ShowRowNumber))
                {
                    rowNumberDataColumn = new DataColumn();
                    rowNumberDataColumn.ColumnName = "dgvRowNumber";
                    rowNumberDataColumn.DataType = Type.GetType("System.Int32");
                    //rowNumberDataColumn.AutoIncrement = true;
                    //rowNumberDataColumn.AutoIncrementSeed = 1;
                    //rowNumberDataColumn.AutoIncrementStep = 1;
                    dt.Columns.Add(rowNumberDataColumn);
                    rowNumberDataColumn.SetOrdinal(0);
                    for (int r = 0; r < dt.Rows.Count; r++)
                    {
                        dt.Rows[r][0] = r + 1;
                    }
                }


                ds.Tables.Add(dt);

                frm = new DataOutputGridFormExp();
                frm.DataGridBindingSource.DataSource = ds;
                frm.DataGridBindingSource.DataMember = dt.TableName;
                frm.outputDataGridBindingNavigator.BindingSource = frm.DataGridBindingSource;
                frm.outputDataGrid.DataSource = frm.DataGridBindingSource;
                frm.GridColumnFilters = this.GridColumnFilters;
                frm.EnableExportMenu = this.EnableExportMenu;
                frm.ShowInstalledDatabaseProvidersOnly = this.ShowInstalledDatabaseProvidersOnly;
                frm.DefaultOutputDatabaseType = this.DefaultOutputDatabaseType;
                frm.DefaultOutputDatabaseConnectionString = this.DefaultOutputDatabaseConnectionString;
                frm.DefaultGridExportFolder = this.DefaultGridExportFolder;
                if (rowNumberDataColumn != null)
                {
                    if (this.ShowRowNumber)
                        frm.outputDataGrid.Columns[0].Visible = true;
                    else
                        frm.outputDataGrid.Columns[0].Visible = false;
                }
                frm.MessageLogUI = this.MessageLogUI;
                frm.Focus();
                frm.ShowDialog();
                frm.Close();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if (rowNumberDataColumn != null)
                {
                    if (dt != null)
                    {
                        dt.Columns.Remove(rowNumberDataColumn);
                    }
                }
                frm = null;
            }
                 
        }//end method


        //class helpers


        private void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }



    }//end class
}//end namespace
