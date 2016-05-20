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
using AppGlobals;
using PFRandomDataExt;
using PFRandomDataProcessor;

namespace PFRandomValueDataTables
{
    /// <summary>
    /// Contains routines to generate random data element values to an ADO.NET DataTable object.
    /// </summary>
    public class RandomDataElementDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private RandomDataProcessor _rdp = new RandomDataProcessor();
        RandomNumber _rn = new RandomNumber();
        RandomValue _rv = new RandomValue();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomDataElementDataTable()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Generate set of random elements as requested in the specified RandomDataElementDataRequest object.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <param name="dataRequest">RandomDataElementDataRequest object containing the definition for the type ofrandom data elements to generate.</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        public DataTable CreateRandomDataTable(int numRows, RandomDataElementDataRequest dataRequest)
        {
            DataTable dt = null;
            enCountry country = enCountry.NotSpecified;

            if (dataRequest.OutputPersonName)
            {
                if (dataRequest.OutputFullName)
                {
                    country = GetCountry(dataRequest);
                    dt = CreateFullNameDataTable(numRows, country, dataRequest.OutputMiddleInitial);
                }
                else if (dataRequest.OutputLastName)
                {
                    country = GetCountry(dataRequest);
                    dt = CreateLastNameDataTable(numRows, country);
                }
                else if (dataRequest.OutputFirstName)
                {
                    country = GetCountry(dataRequest);
                    dt = CreateFirstNameDataTable(numRows, country);
                }
                else
                {
                    dt = new DataTable();  //do nothing
                }
            }
            else if (dataRequest.OutputBusinessName)
            {
                country = GetCountry(dataRequest);
                dt = CreateBusinessNameDataTable(numRows, country);
            }
            else if (dataRequest.OutputTelephoneNumber)
            {
                country = GetCountry(dataRequest);
                dt = CreateTelephoneNumberDataTable(numRows, country);
            }
            else if (dataRequest.OutputEmailAddress)
            {
                dt = CreateEmailAddressDataTable(numRows);
            }
            else if (dataRequest.OutputGUID)
            {
                dt = CreateGUIDDataTable(numRows);
            }
            else
            {
                dt = new DataTable();
            }

            return dt;
        }

        private enCountry GetCountry(RandomDataElementDataRequest dataRequest)
        {
            enCountry country = enCountry.NotSpecified;

            if (dataRequest.UseUnitedStatesTemplate)
                country = enCountry.UnitedStates;
            else if (dataRequest.UseCanadaTemplate)
                country = enCountry.Canada;
            else if (dataRequest.UseMexicoTemplate)
                country = enCountry.Mexico;
            else
                country = enCountry.NotSpecified;

            return country;
        }

        /// <summary>
        /// Creates set of random names that consist of First, Middle (optional) and Last names.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <param name="country">Use enCountry enum to specify the country for which the names are to be generated. (U.S., Canada or Mexico)</param>
        /// <param name="outputMiddleInitial">If true, a middle initial will be included in the generated names.</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        public DataTable CreateFullNameDataTable(int numRows, enCountry country, bool outputMiddleInitial)
        {
            DataTable dt = new DataTable();
            enNameLanguage language = enNameLanguage.NotSpecified;

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();

                    language = GetLanguage(country);

                    string randString = _rdp.GetFullName(language, outputMiddleInitial);

                    dr[0] = randString;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateFullNameDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        /// <summary>
        /// Creates set of random last names.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <param name="country">Use enCountry enum to specify the country for which the names are to be generated. (U.S., Canada or Mexico)</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        public DataTable CreateLastNameDataTable(int numRows, enCountry country)
        {
            DataTable dt = new DataTable();
            enNameLanguage language = enNameLanguage.NotSpecified;

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();

                    language = GetLanguage(country);

                    string randString = _rdp.GetLastName(language);

                    dr[0] = randString;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateLastNameDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        /// <summary>
        /// Creates set of random first names.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <param name="country">Use enCountry enum to specify the country for which the names are to be generated. (U.S., Canada or Mexico)</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        public DataTable CreateFirstNameDataTable(int numRows, enCountry country)
        {
            DataTable dt = new DataTable();
            enNameLanguage language = enNameLanguage.NotSpecified;

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();

                    language = GetLanguage(country);

                    string randString = _rdp.GetFirstName(language);

                    dr[0] = randString;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateFirstNameDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        /// <summary>
        /// Creates set of random business names.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <param name="country">Use enCountry enum to specify the country for which the names are to be generated. (U.S., Canada or Mexico)</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        public DataTable CreateBusinessNameDataTable(int numRows, enCountry country)
        {
            DataTable dt = new DataTable();
            enNameLanguage language = enNameLanguage.NotSpecified;

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();

                    language = GetLanguage(country);

                    string randString = _rdp.GetBusinessName(language);

                    dr[0] = randString;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateBusinessNameDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        private enNameLanguage GetLanguage(enCountry country)
        {
            enNameLanguage language = enNameLanguage.NotSpecified;
            double percentCanadaEnglish = 80.0;
            double minValue = 1.0;
            double maxValue = 100.0;

            if (country == enCountry.UnitedStates)
                language = enNameLanguage.US;
            else if (country == enCountry.Mexico)
                language = enNameLanguage.Spanish;
            else
            {
                Double randNum = _rn.GenerateRandomNumber(minValue, maxValue);
                language = randNum <= percentCanadaEnglish ? enNameLanguage.English : enNameLanguage.French;
            }

            return language;
        }


        /// <summary>
        /// Creates set of random telephone numbers.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <param name="country">Use enCountry enum to specify the country for which the telephone numbers are to be generated. (U.S., Canada or Mexico)</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        /// <remarks>The numbers will be generated in such as way as they do not duplicate any valid telephone numbers.</remarks>
        public DataTable CreateTelephoneNumberDataTable(int numRows, enCountry country)
        {
            DataTable dt = new DataTable();

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                string countryCode = country == enCountry.Mexico ? "52" : "1";

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();

                    string areaCode = GetAreaCode(country);
                    string randString = _rv.GetTelephoneNumber(countryCode, areaCode);

                    dr[0] = randString;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateEmailAddressDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        private string GetAreaCode(enCountry country)
        {
            string areaCode = string.Empty;
            double minValue = 1.0;
            double maxValue = 100.0;

            if (country == enCountry.UnitedStates)
                areaCode = "000";
            else if (country == enCountry.Canada)
                areaCode = "000";
            else if (country == enCountry.Mexico)
            {
                Double randNum = _rn.GenerateRandomNumber(minValue, maxValue);
                if (randNum < 25.0)
                    areaCode = "55";
                else if (randNum < 50.0)
                    areaCode = "81";
                else if (randNum < 75.0)
                    areaCode = "33";
                else
                    areaCode = _rn.GenerateRandomInt(100, 899).ToString("000");
            }
            else
                areaCode = "000";

            return areaCode;
        }

        /// <summary>
        /// Creates set of random email addresses.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        /// <remarks>The addresses will be generated in such as way as they do not duplicate any valid email addresses.</remarks>
        public DataTable CreateEmailAddressDataTable(int numRows)
        {
            DataTable dt = new DataTable();

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();


                    string randString = _rv.GetEmailAddress();

                    dr[0] = randString;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateEmailAddressDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        /// <summary>
        /// Creates set of random GUIDs.
        /// </summary>
        /// <param name="numRows">Number of random elements to generate.</param>
        /// <returns>ADO.NET Data Table containing the set of random values.</returns>
        public DataTable CreateGUIDDataTable(int numRows)
        {
            DataTable dt = new DataTable();

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.Guid");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();


                    Guid randGUID = Guid.NewGuid();

                    dr[0] = randGUID;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateGUIDDataTable routine.\r\n");
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
