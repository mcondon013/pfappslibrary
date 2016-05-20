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
    /// Contains routines to generate random booleans to an ADO.NET DataTable object.
    /// </summary>
    public class RandomBooleanDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private System.Type[] numberType = new System.Type[11];

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomBooleanDataTable()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            InitNumericSystemTypes();
        }

        private void InitNumericSystemTypes()
        {
            numberType[(int)enRandomNumberType.enInt] = Type.GetType("System.Int32");
            numberType[(int)enRandomNumberType.enUInt] = Type.GetType("System.UInt32");
            numberType[(int)enRandomNumberType.enLong] = Type.GetType("System.Int64");
            numberType[(int)enRandomNumberType.enULong] = Type.GetType("System.UInt64");
            numberType[(int)enRandomNumberType.enShort] = Type.GetType("System.Int16");
            numberType[(int)enRandomNumberType.enUShort] = Type.GetType("System.UInt16");
            numberType[(int)enRandomNumberType.enSByte] = Type.GetType("System.SByte");
            numberType[(int)enRandomNumberType.enByte] = Type.GetType("System.Byte");
            numberType[(int)enRandomNumberType.enDouble] = Type.GetType("System.Double");
            numberType[(int)enRandomNumberType.enFloat] = Type.GetType("System.Single");
            numberType[(int)enRandomNumberType.enDecimal] = Type.GetType("System.Decimal");
        }

        
        //properties

        //methods

        /// <summary>
        /// Creates a DataTable containing a set of random boolean values.
        /// </summary>
        /// <param name="numRows">Num of rows with boolean values to generate.</param>
        /// <param name="dataRequest">RandomBooleanDataRequest object contains the definition for how to generate the random booleans.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        public DataTable CreateRandomDataTable(int numRows, RandomBooleanDataRequest dataRequest)
        {
            DataTable dt = null;
            enRandomNumberType randNumberType = enRandomNumberType.enUnknown;

            if (dataRequest.BooleanOutput)
            {
                dt = CreateBooleanDataTable(numRows, dataRequest.PercentOutputValuesAsTrue.ToString(), dataRequest.PercentOutputValuesAsFalse.ToString());
            }
            else if (dataRequest.NumericOutput)
            {
                randNumberType = GetRandomNumberType(dataRequest);
                if(randNumberType != enRandomNumberType.enUnknown)
                    dt = CreateNumericDataTable(numRows, dataRequest.PercentOutputValuesAsTrue.ToString(), dataRequest.PercentOutputValuesAsFalse.ToString(), randNumberType, dataRequest.NumericTrueValue, dataRequest.NumericFalseValue);
            }
            else if (dataRequest.StringOutput )
            {
                dt = CreateStringDataTable(numRows, dataRequest.PercentOutputValuesAsTrue.ToString(), dataRequest.PercentOutputValuesAsFalse.ToString(), dataRequest.StringTrueValue, dataRequest.StringFalseValue);
            }
            else
            {
                dt = new DataTable(); 
            }

            return dt;
        }

        /// <summary>
        /// Retrieve the type of number to generate if output request is for a numeric boolean (e.g. 0 or 1).
        /// </summary>
        /// <param name="dataRequest">RandomBooleanDataRequest objects contains the definition for how to generate the random booleans.</param>
        /// <returns>enRandomNumberType enum value.</returns>
        private enRandomNumberType GetRandomNumberType(RandomBooleanDataRequest dataRequest)
        {
            enRandomNumberType randNumType = enRandomNumberType.enUnknown;
            if (dataRequest.OutputIntegerValue)
            {
                if (dataRequest.Output64bitInteger)
                {
                    if (dataRequest.OutputSignedInteger)
                    {
                        randNumType = enRandomNumberType.enLong;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enULong;
                    }
                }
                else if (dataRequest.Output32bitInteger)
                {
                    if (dataRequest.OutputSignedInteger)
                    {
                        randNumType = enRandomNumberType.enInt;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enUInt;
                    }
                }
                else if (dataRequest.Output16bitInteger)
                {
                    if (dataRequest.OutputSignedInteger)
                    {
                        randNumType = enRandomNumberType.enShort;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enUShort;
                    }
                }
                else if (dataRequest.Output8bitInteger)
                {
                    if (dataRequest.OutputSignedInteger)
                    {
                        randNumType = enRandomNumberType.enSByte;
                    }
                    else
                    {
                        randNumType = enRandomNumberType.enByte;
                    }
                }
                else
                {
                    randNumType = enRandomNumberType.enUnknown;
                }

            }
            else if (dataRequest.OutputDoubleValue)
            {
                randNumType = enRandomNumberType.enDouble;
            }
            else if (dataRequest.OutputFloatValue)
            {
                randNumType = enRandomNumberType.enFloat;
            }
            else if (dataRequest.OutputDecimalValue)
            {
                randNumType = enRandomNumberType.enDecimal;
            }
            else
            {
                randNumType = enRandomNumberType.enUnknown;
            }

            return randNumType;
        }


        /// <summary>
        /// Creates a set of random boolean values.
        /// </summary>
        /// <param name="numRows">Number of random values to generate.</param>
        /// <param name="percentOutputValuesAsTrue">Percentage of output values to be true.</param>
        /// <param name="percentOutputValuesAsFalse">Percentage of output values to be false.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        /// <remarks>If percentOutputValuesAsTrue and percentOutputValuesAsFalse do not add up to 100, you will get unexpected results.</remarks>
        public DataTable CreateBooleanDataTable(int numRows, string percentOutputValuesAsTrue, string percentOutputValuesAsFalse)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();

            try
            {
                Double percentTrueValues = Convert.ToDouble(percentOutputValuesAsTrue);
                Double percentFalseValues = Convert.ToDouble(percentOutputValuesAsFalse);
                double minValue = 1.0;
                double maxValue = 100.0;

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.Boolean");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    Double randNum = rn.GenerateRandomNumber(minValue, maxValue);
                    bool randBool = randNum <= percentTrueValues ? true : false;
                    dr[0] = randBool;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateBooleanDataTable routine.\r\n");
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
        /// Creates a set of true/false values expressed as numbers.
        /// </summary>
        /// <param name="numRows">Number of random values to generate.</param>
        /// <param name="percentOutputValuesAsTrue">Percentage of output values to be true.</param>
        /// <param name="percentOutputValuesAsFalse">Percentage of output values to be false.</param>
        /// <param name="numType">Use enRandomNumberType enum to specify type of numbers to generate.</param>
        /// <param name="trueValue">The numeric value for True booleans (e.g. 1).</param>
        /// <param name="falseValue">The numeric value for False booleans (e.g. 0)</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        /// <remarks>If percentOutputValuesAsTrue and percentOutputValuesAsFalse do not add up to 100, you will get unexpected results.</remarks>
        public DataTable CreateNumericDataTable(int numRows, string percentOutputValuesAsTrue, string percentOutputValuesAsFalse, enRandomNumberType numType, string trueValue, string falseValue)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();

            try
            {
                Double percentTrueValues = Convert.ToDouble(percentOutputValuesAsTrue);
                Double percentFalseValues = Convert.ToDouble(percentOutputValuesAsFalse);
                double minValue = 1.0;
                double maxValue = 100.0;

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = numberType[(int)numType];
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    Double randNum = rn.GenerateRandomNumber(minValue, maxValue);
                    if (randNum <= percentTrueValues)
                    {
                        dr[0] = Convert.ChangeType(trueValue, numberType[(int)numType]);
                    }
                    else
                    {
                        //randNum > percentTrueValues //is false
                        dr[0] = Convert.ChangeType(falseValue, numberType[(int)numType]);
                    }
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateNumericDataTable routine.\r\n");
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
        /// Creates a set of true/false values expressed as strings.
        /// </summary>
        /// <param name="numRows">Number of random values to generate.</param>
        /// <param name="percentOutputValuesAsTrue">Percentage of output values to be true.</param>
        /// <param name="percentOutputValuesAsFalse">Percentage of output values to be false.</param>
        /// <param name="trueValue">The string value for True booleans (e.g. "true").</param>
        /// <param name="falseValue">The string value for False booleans (e.g. "false")</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        /// <remarks>If percentOutputValuesAsTrue and percentOutputValuesAsFalse do not add up to 100, you will get unexpected results.</remarks>
        public DataTable CreateStringDataTable(int numRows, string percentOutputValuesAsTrue, string percentOutputValuesAsFalse, string trueValue, string falseValue)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();

            try
            {
                Double percentTrueValues = Convert.ToDouble(percentOutputValuesAsTrue);
                Double percentFalseValues = Convert.ToDouble(percentOutputValuesAsFalse);
                double minValue = 1.0;
                double maxValue = 100.0;

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.String");
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    Double randNum = rn.GenerateRandomNumber(minValue, maxValue);
                    string randString = randNum <= percentTrueValues ? trueValue : falseValue;
                    dr[0] = randString;
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateStringDataTable routine.\r\n");
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
