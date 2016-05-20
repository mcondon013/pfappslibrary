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

namespace pfDataExtractorCPObjects
{
    /// <summary>
    /// Class for generation of test sales orders.
    /// </summary>
    /// <remarks>Not used. Code needs to be built. See pfDataExtractorCPProcesxor/AppProcessor for an implementation of test order generation.</remarks>
    public class PFTestOrdersGenerator
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFTestOrdersGenerator()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Routine to generate a set of test sales orders.
        /// </summary>
        /// <param name="randOrdersDef">Object containing definition for the random orders.</param>
        /// <param name="numRowsToGenerate">Number of orders to generate.</param>
        /// <returns>Structure containing order header and detail tables.</returns>
        /// <remarks>Not used.</remarks>
        public stOrdersTables GenerateSalesOrdersTables(PFRandomOrdersDefinition randOrdersDef, int numRowsToGenerate)
        {
            stOrdersTables retTabs = default(stOrdersTables);


            return retTabs;
        }

        /// <summary>
        /// Routine to generate a set of test purchase orders.
        /// </summary>
        /// <param name="randOrdersDef">Object containing definition for the random orders.</param>
        /// <param name="numRowsToGenerate">Number of orders to generate.</param>
        /// <returns>Structure containing order header and detail tables.</returns>
        /// <remarks>Not used.</remarks>
        public stOrdersTables GeneratePurchaseOrdersTables(PFRandomOrdersDefinition randOrdersDef, int numRowsToGenerate)
        {
            stOrdersTables retTabs = default(stOrdersTables);


            return retTabs;
        }

        /// <summary>
        /// Routine to generate a set of test Internet sales data warehouse style data table.
        /// </summary>
        /// <param name="randOrdersDef">Object containing definition for the random sales.</param>
        /// <param name="numRowsToGenerate">Number of sales to generate.</param>
        /// <returns>DataTable containing sales table.</returns>
        /// <remarks>Not used.</remarks>
        public DataTable GenerateDwInternetSalesTable(PFRandomOrdersDefinition randOrdersDef, int numRowsToGenerate)
        {
            DataTable dt = null;



            return dt;
        }

        /// <summary>
        /// Routine to generate a set of test Reseller sales data warehouse style data table.
        /// </summary>
        /// <param name="randOrdersDef">Object containing definition for the random sales.</param>
        /// <param name="numRowsToGenerate">Number of sales to generate.</param>
        /// <returns>DataTable containing sales table.</returns>
        /// <remarks>Not used.</remarks>
        public DataTable GenerateDwResellerSalesTable(PFRandomOrdersDefinition randOrdersDef, int numRowsToGenerate)
        {
            DataTable dt = null;



            return dt;
        }


    }//end class
}//end namespace
