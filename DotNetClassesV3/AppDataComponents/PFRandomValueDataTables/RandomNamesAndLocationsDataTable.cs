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
    /// Contains routines to generate random name and location values to an ADO.NET DataTable object.
    /// </summary>
    public class RandomNamesAndLocationsDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomNamesAndLocationsDataTable()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Generates set of random names and associated locations.
        /// </summary>
        /// <param name="numRows">Number of names to generate.</param>
        /// <param name="rdr">RandomNamesAndLocationsDataRequest object containing the definition to use for the name and location generation.</param>
        /// <returns>ADO.NET DataTable containing the set of random names and locations.</returns>
        public DataTable CreateRandomNamesAndLocationsDataTable(int numRows,
                                                                RandomNamesAndLocationsDataRequest rdr)
        {
            return CreateRandomNamesAndLocationsDataTable(numRows, rdr, false, string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Generates set of random names and associated locations. This routine allows the randomizer routines to display a grid with the output before returning with the result.
        /// </summary>
        /// <param name="numRows">Number of names to generate.</param>
        /// <param name="rdr">RandomNamesAndLocationsDataRequest object containing the definition to use for the name and location generation.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Show list of installed data providers only if the user is shown an output grid and wants to manually export the list to external storage.</param>
        /// <param name="defaultOutputDatabaseType">Default database to show if user is prompted for connection information.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to show if user is prompted for connection information.</param>
        /// <param name="defaultDataGridExportFolder">Default export file folder to use if user is exporting data from a grid displaying the random names/locations output.</param>
        /// <returns>ADO.NET DataTable containing the set of random names and locations.</returns>
        public DataTable CreateRandomNamesAndLocationsDataTable(int numRows, 
                                                                RandomNamesAndLocationsDataRequest rdr,
                                                                bool showInstalledDatabaseProvidersOnly,
                                                                string defaultOutputDatabaseType,
                                                                string defaultOutputDatabaseConnectionString,
                                                                string defaultDataGridExportFolder)
        {
            DataTable dt = new DataTable();
            DataListProcessor _appProcessor = new DataListProcessor();

            try
            {

                _appProcessor.ShowInstalledDatabaseProvidersOnly = showInstalledDatabaseProvidersOnly;
                _appProcessor.DefaultOutputDatabaseType = defaultOutputDatabaseType;
                _appProcessor.DefaultOutputDatabaseConnectionString = defaultOutputDatabaseConnectionString;
                _appProcessor.GridExportFolder = defaultDataGridExportFolder;

                dt = _appProcessor.GetRandomNamesList(rdr,
                                                      numRows,
                                                      false,   //no output to xml file
                                                      rdr.RandomDataXmlFilesFolder,
                                                      rdr.ListName,
                                                      false);  //no output to preview grid


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRandomNamesAndLocationsDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }



    }//end class
}//end namespace
