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
using System.Data.Common;
using System.IO;
using System.Globalization;
using AppGlobals;
using PFRandomDataExt;
using PFRandomDataListProcessor;
using PFRandomDataProcessor;

namespace PFRandomValueDataTables
{
    /// <summary>
    /// Contains routines to generate random custom values to an ADO.NET DataTable object.
    /// </summary>
    public class RandomCustomValuesDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomCustomValuesDataTable()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Loads DataTable containing the custom values specified in the list file defined in the RandomCustomValuesDataRequest definition.
        /// </summary>
        /// <param name="numRows">Number of output rows to generate. (IGNORED by this routine. Routine takes all the rows in the custom values file.)</param>
        /// <param name="dataRequest">RandomCustomValuesDataRequest object contains the name of the file that contains the custom values.</param>
        /// <returns>ADO.NET DataTable containing the custom values.</returns>
        /// <remarks>Random values are containing in a presiously generated file.</remarks>
        public DataTable CreateRandomDataTable(int numRows, RandomCustomValuesDataRequest dataRequest)
        {
            DataSet ds = new DataSet();
            DataTable dt = null;
            
            string dataFilePath = Path.Combine(dataRequest.ListFolder, dataRequest.ListName + ".xml");
            ds.ReadXml(dataFilePath);
            dt = ds.Tables[0];
            dt.TableName = dataRequest.ListName;

            return dt;
        }


        /// <summary>
        /// Creates DataTable containing the custom values specified in the RandomCustomValuesDataRequest definition.
        /// </summary>
        /// <param name="numRows">Number of output rows to generate.</param>
        /// <param name="rdr">RandomCustomValuesDataRequest object contains the definition for the custom values.</param>
        /// <param name="summaryDataTable">DataTable that will contain the summary values (i.e. how many times each value appears in the result table).</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Instructs custom values routines to only allow connections from the list installed database providers.</param>
        /// <param name="defaultOutputDatabaseType">Default database type to show if user is prompted for connection information.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use if user is prompted for connection information.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing any output requested by user from the result grids that are displayed.</param>
        /// <returns>ADO.NET DataTable containing the custom values.</returns>
        /// <remarks>New set of values are generated and stored by this routine.</remarks>
        public DataTable CreateRandomCustomValuesDataTable(int numRows,
                                                        RandomCustomValuesDataRequest rdr,
                                                        out DataTable summaryDataTable,
                                                        bool showInstalledDatabaseProvidersOnly,
                                                        string defaultOutputDatabaseType,
                                                        string defaultOutputDatabaseConnectionString,
                                                        string defaultDataGridExportFolder)
        {
            DataTable summaryTable = new DataTable();
            DataListProcessor _appProcessor = new DataListProcessor();
            DataTable listTable = null;
            try
            {

                _appProcessor.ShowInstalledDatabaseProvidersOnly = showInstalledDatabaseProvidersOnly;
                _appProcessor.DefaultOutputDatabaseType = defaultOutputDatabaseType;
                _appProcessor.DefaultOutputDatabaseConnectionString = defaultOutputDatabaseConnectionString;
                _appProcessor.GridExportFolder = defaultDataGridExportFolder;

                summaryTable = _appProcessor.GetCustomRandomDataFile(rdr, numRows, false, false);


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRandomCustomValuesDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            summaryDataTable = summaryTable;
            listTable = CreateCustomValuesListTable(summaryTable);

            return listTable;

        }

        private DataTable CreateCustomValuesListTable(DataTable summaryTable)
        {
            DataTable listTable = new DataTable();
            int frequency = 0;
            int valueInx = 0;
            //int frequencyInx = 1;
            int adjustedFrequencyInx = 2;
            //int adjustmentNumberInx = 3;

            DataColumn dc = new DataColumn("RandomValue", Type.GetType("System.String"));
            listTable.Columns.Add(dc);

            for (int i = 0; i < summaryTable.Rows.Count; i++)
            {
                frequency = Convert.ToInt32(summaryTable.Rows[i][adjustedFrequencyInx].ToString());
                for (int k = 0; k < frequency; k++)
                {
                    DataRow dr = listTable.NewRow();
                    dr[0] = summaryTable.Rows[i][valueInx].ToString();
                    listTable.Rows.Add(dr);
                }
            }



            return listTable;
        }



    }//end class
}//end namespace
