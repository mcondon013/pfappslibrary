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

namespace pfDataExtractorCPObjects
{
    /// <summary>
    /// Enumeration of possible data locations.
    /// </summary>
    public enum enExtractorDataLocation
    {
#pragma warning disable 1591
        Unknown = 0,
        RelationalDatabase = 1,
        AccessDatabaseFile = 2,
        ExcelDataFile = 3,
        DelimitedTextFile = 4,
        FixedLengthTextFile = 5,
        XMLFile = 6
#pragma warning restore 1591
    }

    /// <summary>
    /// Structure containing orders headers and detail tables.
    /// </summary>
    public struct stOrdersTables
    {
#pragma warning disable 1591
        public DataTable OrderHeaders;
        public DataTable OrderDetails;

        public stOrdersTables(DataTable hdrs, DataTable dtls)
        {
            OrderHeaders = hdrs;
            OrderDetails = dtls;
        }
#pragma warning restore 1591
    }

    /// <summary>
    /// Class containing array of display names for data types supported by the extractor.
    /// </summary>
    public class ExtractorDataTypeList
    {
        private static string[] _extractorDataLocations = {
                                                           "Relational Database",
                                                           "Access Database File",
                                                           "Excel Data File",
                                                           "Text File (Delimited)",
                                                           "Text File (Fixed Length)",
                                                           "XML File"
                                                          };
        
#pragma warning disable 1591
        public static int RelationalDatabaseListIndex = 0;
        public static int AccessDatabaseFileListIndex = 1;
        public static int ExcelDataFileListIndex = 2;
        public static int DelimitedTextFileListIndex = 3;
        public static int FixedLengthTextFileListIndex = 4;
        public static int XMLFileListIndex = 5;
#pragma warning restore 1591

        /// <summary>
        /// Array of display names for data types supported by the extractor.
        /// This is used to fill application drop-downs.
        /// </summary>
        public static string[] ExtractorDataLocations
        {
            get
            {
                return _extractorDataLocations;
            }
            set
            {
                _extractorDataLocations = value;
            }
        }

    }

}//end namespace