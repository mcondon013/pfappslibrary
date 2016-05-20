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
    /// Contains routines to generate random number values to an ADO.NET DataTable object.
    /// </summary>
    public class RandomNumberDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private delegate DataTable NumberRangeDataTable(int numItems, string from, string to);

        private System.Type[] numberType = new System.Type[11];

        private struct stTypeMinMax
        {
            public double minVal;
            public double maxVal;

            public stTypeMinMax(double min, double max)
            {
                minVal = min;
                maxVal = max;
            }
        }

        stTypeMinMax[] numberTypeMinMaxVals = new stTypeMinMax[11];


        //public work variables
        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomNumberDataTable()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            InitNumericSystemTypes();
            InitMinMaxValues();
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

        private void InitMinMaxValues()
        {
            numberTypeMinMaxVals[(int)enRandomNumberType.enInt] = new stTypeMinMax(int.MinValue, int.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enUInt] = new stTypeMinMax(uint.MinValue, uint.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enLong] = new stTypeMinMax(long.MinValue, long.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enULong] = new stTypeMinMax(ulong.MinValue, ulong.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enShort] = new stTypeMinMax(short.MinValue, short.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enUShort] = new stTypeMinMax(ushort.MinValue, ushort.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enSByte] = new stTypeMinMax(sbyte.MinValue, sbyte.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enByte] = new stTypeMinMax(byte.MinValue, byte.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enDouble] = new stTypeMinMax(double.MinValue, double.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enFloat] = new stTypeMinMax(float.MinValue, float.MaxValue);
            numberTypeMinMaxVals[(int)enRandomNumberType.enDecimal] = new stTypeMinMax(double.MinValue, double.MaxValue);
        }

        //properties

        //methods

        /// <summary>
        /// Creates a DataTable containing a set of random number values.
        /// </summary>
        /// <param name="numRows">Num of rows with number values to generate.</param>
        /// <param name="dataRequest">RandomNumberDataRequest object contains the definition for how to generate the random numbers.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        public DataTable CreateRandomNumberDataTable(int numRows, RandomNumberDataRequest dataRequest)
        {
            enRandomNumberType numType = enRandomNumberType.enUnknown;
            DataTable dt = null;

            numType = GetRandomNumberTypeFromDataRequest(dataRequest);

            if (dataRequest.OutputRangeOfNumbers)
            {
                dt = CreateRangeDataTable(numType, numRows, dataRequest.MinimumValueForRange, dataRequest.MaximumValueForRange);
            }
            else if (dataRequest.OutputOffsetFromCurrentNumber)
            {
                dt = CreateOffsetDataTable(numType, numRows, dataRequest.MinimumOffsetPercent, dataRequest.MaximumOffsetPercent);
            }
            else if (dataRequest.OutputSequentialNumbers)
            {
                dt = CreateSequentialNumbersDataTable(numType, numRows, dataRequest.StartSequentialValue, dataRequest.IncrementForSequentialValue, dataRequest.MaxSequentialValue, dataRequest.InitStartSequentialValue);
            }
            else
            {
                dt = new DataTable();  //return an empty data table
            }

            return dt;
        }

        /// <summary>
        /// Routine to retrieve the number type from a RandomNumberDataRequest object.
        /// </summary>
        /// <param name="dataRequest">RandomNumberDataRequest object containing the definition for the random number generation.</param>
        /// <returns>enRandomNumberType enum value.</returns>
        public enRandomNumberType GetRandomNumberTypeFromDataRequest(RandomNumberDataRequest dataRequest)
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
        /// Creates set of random numbers that fall within a pre-defined range of numbers.
        /// </summary>
        /// <param name="numType">Type of number to generate (e.g. Int32, Int64, etc.)</param>
        /// <param name="numRows">Number of random numbers to generate.</param>
        /// <param name="fromNumber">Minimum value of defined number range.</param>
        /// <param name="toNumber">Maximum value of defined number range.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        public DataTable CreateRangeDataTable(enRandomNumberType numType, int numRows, string fromNumber, string toNumber)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();

            try
            {
                Double fromNum = Convert.ToDouble(fromNumber);
                Double toNum = Convert.ToDouble(toNumber);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = numberType[(int)numType];
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    Double randNum = rn.GenerateRandomNumber(fromNum, toNum);
                    dr[0] = Convert.ChangeType(randNum, numberType[(int)numType]);
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateRangeDataTable routine.\r\n");
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
        /// Creates a set of offset values that can be used to decrease or increase existing values by the calling program.
        /// </summary>
        /// <param name="numType">Type of offset number to generate (e.g. Int32, Int64, etc.)</param>
        /// <param name="numRows">Number of random offset numbers to generate.</param>
        /// <param name="minPercent">Minimum offset percent for offset.</param>
        /// <param name="maxPercent">Maximum offset percent for offset.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        /// <remarks>Any negative percent values will decrease a value. Positive percent values 100 will increase a value.
        /// For example, min -50 percent max +50 percent means offsets will range from 50 percent less to 50 percent more.</remarks>
        /// <remarks>Offset will be expressed in terms of the following base values: 10000 for short integers, 10 for byte sized integers, 1000000 for all other numeric types.
        /// For example, min to max definition of -50 percent to +50 percent for int32 numbers, will return values ranging from -500000 to +500000. You can use these offsets to calculate a percentage to use in modifying an existing value.
        /// Example, if current value for an Int32 in a table is 100 and offset is -500000, modified value = currentvalue * (-500000/ 1000000) or currentvalue = currentvalue * (offset / basevalue).</remarks>
        public DataTable CreateOffsetDataTable(enRandomNumberType numType, int numRows, string minPercent, string maxPercent)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();
            

            try
            {
                Double minPct = Convert.ToDouble(minPercent) / 100.0;
                Double maxPct = Convert.ToDouble(maxPercent) / 100.0;
                Double baseValue = 1000.0;

                if (numType == enRandomNumberType.enShort || numType == enRandomNumberType.enUShort)
                {
                    baseValue = 10000;
                }
                else if (numType == enRandomNumberType.enByte || numType == enRandomNumberType.enSByte)
                {
                    baseValue = 10.0;
                }
                else if (numType == enRandomNumberType.enInt || numType == enRandomNumberType.enUInt
                         || numType == enRandomNumberType.enLong || numType == enRandomNumberType.enULong
                         || numType == enRandomNumberType.enDouble || numType == enRandomNumberType.enFloat)
                {
                    baseValue = 1000000;
                }
                else
                {
                    //is a float number (.NET Single)
                    baseValue = 100000;
                }

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.Double");
                dt.Columns.Add(dc);
                DataColumn dc2 = new DataColumn("BaseValue");
                dc2.DataType = Type.GetType("System.Double");
                dt.Columns.Add(dc2);
                DataColumn dc3 = new DataColumn("NewValueAfterOffset");
                dc3.DataType = numberType[(int)numType];
                dt.Columns.Add(dc3);

                for (int i = 0; i < numRows; i++)
                {
                    Double minNum = baseValue + (baseValue * minPct) + 1.0;
                    Double maxNum = baseValue + (baseValue * maxPct) + 1.0;
                    DataRow dr = dt.NewRow();
                    Double randNum = rn.GenerateRandomNumber(minNum, maxNum);
                    Double randOffset = randNum - baseValue;
                    if (randNum < numberTypeMinMaxVals[(int)numType].minVal)
                    {
                        dr[0] = numberTypeMinMaxVals[(int)numType].minVal + 1;  //Convert.ChangeType(numberTypeMinMaxVals[(int)numType].minVal + 1, numberType[(int)numType]);
                    }
                    else if (randNum > numberTypeMinMaxVals[(int)numType].maxVal)
                    {
                        dr[0] = numberTypeMinMaxVals[(int)numType].maxVal - 1;  //Convert.ChangeType(numberTypeMinMaxVals[(int)numType].maxVal - 1, numberType[(int)numType]);
                    }
                    else
                    {
                        dr[0] = randOffset; // Convert.ChangeType(randNum, numberType[(int)numType]);
                    }
                    dr[1] = baseValue;
                    Double newValueAfterOffset = baseValue + (double)dr[0];
                    dr[2] = Convert.ChangeType(newValueAfterOffset, numberType[(int)numType]);
                    dt.Rows.Add(dr);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateIntNumberRangeDataTable routine.\r\n");
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
        /// Creates a set of offset values that can be used to decrease or increase existing values by the calling program. This routine also allows display of results on a form in a grid. Random offset, base value and adjuted base value can be displayed in the grid.
        /// </summary>
        /// <param name="numType">Type of offset number to generate (e.g. Int32, Int64, etc.)</param>
        /// <param name="numRows">Number of random offset numbers to generate.</param>
        /// <param name="minPercent">Minimum offset percent for offset.</param>
        /// <param name="maxPercent">Maximum offset percent for offset.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        /// <remarks>Any negative percent values will decrease a value. Positive percent values 100 will increase a value.
        /// For example, min -50 percent max +50 percent means offsets will range from 50 percent less to 50 percent more.</remarks>
        /// <remarks>Offset will be expressed in terms of the following base values: 10000 for short integers, 10 for byte sized integers, 1000000 for all other numeric types.
        /// For example, min to max definition of -50 percent to +50 percent for int32 numbers, will return values ranging from -500000 to +500000. You can use these offsets to calculate a percentage to use in modifying an existing value.
        /// For example, if current value for an Int32 in a table is 100 and offset is -500000, modified value = currentvalue * (-500000/ 1000000) or currentvalue = currentvalue * (offset / basevalue) </remarks>

        public DataTable CreateOffsetDataTablePreview(enRandomNumberType numType, int numRows, string minPercent, string maxPercent)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();


            try
            {
                Double minPct = Convert.ToDouble(minPercent) / 100.0;
                Double maxPct = Convert.ToDouble(maxPercent) / 100.0;
                Double currValue = 1000.0;
                Double currValueMin = 1000.0;
                Double currValueIncrement = 500.0;
                Double currValueMax = 5000.0;

                if (numType == enRandomNumberType.enByte || numType == enRandomNumberType.enSByte)
                {
                    currValue = 10.0;
                    currValueMin = 10.0;
                    currValueIncrement = 5.0;
                    currValueMax = 50.0;
                }

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = numberType[(int)numType];
                dt.Columns.Add(dc);
                DataColumn dc2 = new DataColumn("CurrentValue");
                dc2.DataType = numberType[(int)numType];
                dt.Columns.Add(dc2);
                DataColumn dc3 = new DataColumn("OffsetValue");
                dc3.DataType = numberType[(int)numType];
                dt.Columns.Add(dc3);

                for (int i = 0; i < numRows; i++)
                {
                    if (currValue > currValueMax)
                        currValue = currValueMin;
                    Double minNum = currValue + (currValue * minPct) + 1.0;
                    Double maxNum = currValue + (currValue * maxPct) + 1.0;
                    DataRow dr = dt.NewRow();
                    Double randNum = rn.GenerateRandomNumber(minNum, maxNum);
                    Double randOffset = randNum - currValue;
                    if (randNum < numberTypeMinMaxVals[(int)numType].minVal)
                    {
                        dr[0] = Convert.ChangeType(numberTypeMinMaxVals[(int)numType].minVal+1, numberType[(int)numType]);
                    }
                    else if (randNum > numberTypeMinMaxVals[(int)numType].maxVal)
                    {
                        dr[0] = Convert.ChangeType(numberTypeMinMaxVals[(int)numType].maxVal-1, numberType[(int)numType]);
                    }
                    else
                    {
                        dr[0] = Convert.ChangeType(randNum, numberType[(int)numType]);
                    }
                    dr[1] = Convert.ChangeType(currValue, numberType[(int)numType]);
                    dr[2] = Convert.ChangeType(randOffset, numberType[(int)numType]);
                    dt.Rows.Add(dr);
                    currValue += currValueIncrement;
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateIntNumberRangeDataTable routine.\r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;

        }

        ////Old version of preview method
        //public DataTable CreateOffsetDataTablePreview(enRandomNumberType numType, int numRows, string minPercent, string maxPercent)
        //{
        //    DataTable dt = new DataTable();
        //    RandomNumber rn = new RandomNumber();


        //    try
        //    {
        //        Double minPct = Convert.ToDouble(minPercent) / 100.0;
        //        Double maxPct = Convert.ToDouble(maxPercent) / 100.0;
        //        Double baseValue = 1000.0;
        //        Double currValueMin = 1000.0;
        //        Double currValueIncrement = 500.0;
        //        Double currValueMax = 5000.0;

        //        if (numType == enRandomNumberType.enByte || numType == enRandomNumberType.enSByte)
        //        {
        //            baseValue = 10.0;
        //            currValueMin = 10.0;
        //            currValueIncrement = 5.0;
        //            currValueMax = 50.0;
        //        }

        //        DataColumn dc = new DataColumn("RandomValue");
        //        dc.DataType = numberType[(int)numType];
        //        dt.Columns.Add(dc);
        //        DataColumn dc2 = new DataColumn("CurrentValue");
        //        dc2.DataType = numberType[(int)numType];
        //        dt.Columns.Add(dc2);

        //        for (int i = 0; i < numRows; i++)
        //        {
        //            if (baseValue > currValueMax)
        //                baseValue = currValueMin;
        //            Double minNum = baseValue + (baseValue * minPct) + 1.0;
        //            Double maxNum = baseValue + (baseValue * maxPct) + 1.0;
        //            DataRow dr = dt.NewRow();
        //            Double randNum = rn.GenerateRandomNumber(minNum, maxNum);
        //            if (randNum < numberTypeMinMaxVals[(int)numType].minVal
        //                || randNum > numberTypeMinMaxVals[(int)numType].maxVal)
        //            {
        //                dr[0] = Convert.ChangeType((double)0.0, numberType[(int)numType]);
        //            }
        //            else
        //            {
        //                dr[0] = Convert.ChangeType(randNum, numberType[(int)numType]);
        //            }
        //            dr[1] = Convert.ChangeType(baseValue, numberType[(int)numType]);
        //            dt.Rows.Add(dr);
        //            baseValue += currValueIncrement;
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        _msg.Length = 0;
        //        _msg.Append("Error in CreateIntNumberRangeDataTable routine.\r\n");
        //        _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
        //        throw new System.Exception(_msg.ToString());
        //    }
        //    finally
        //    {
        //        ;
        //    }

        //    return dt;

        //}

        /// <summary>
        /// Creates set of sequential numbers starting from a given value and incremented by a specified value for each new value.
        /// </summary>
        /// <param name="numType">Type of sequential number to generate (e.g. Int32, Int64, etc.)</param>
        /// <param name="numRows">Number of sequential numbers to generate.</param>
        /// <param name="startSequentialValue">Begin value for the sequence.</param>
        /// <param name="incrementForSequentialValue">Increment value for each step in the sequence.</param>
        /// <param name="maxSequentialValue">Maximum sequential value.</param>
        /// <param name="initStartSequentialValue">Number to use when restarting a sequence after the maximum value has been generated.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        /// <remarks>When maxSequentialValue has been exceeded, the sequence will restart with the initStartSequentialValue.</remarks>
        public DataTable CreateSequentialNumbersDataTable(enRandomNumberType numType, int numRows, string startSequentialValue, string incrementForSequentialValue, string maxSequentialValue, string initStartSequentialValue)
        {
            DataTable dt = new DataTable();
            RandomNumber rn = new RandomNumber();


            try
            {
                double startNum = Convert.ToDouble(startSequentialValue);
                double increment = Convert.ToDouble(incrementForSequentialValue);
                double maxNum = uint.MaxValue;
                double initNum = Convert.ToDouble(initStartSequentialValue);
                if(maxSequentialValue.Trim().Length > 0)
                    maxNum = Convert.ToDouble(maxSequentialValue);

                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = numberType[(int)numType];
                dt.Columns.Add(dc);

                double currNum = startNum - increment;
                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    currNum = currNum + increment;
                    if (currNum > maxNum)
                        currNum = initNum;
                    dr[0] = Convert.ChangeType(currNum, numberType[(int)numType]);
                    dt.Rows.Add(dr);
                }


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateSequentialNumbersDataTable routine.\r\n");
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
