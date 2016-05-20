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
    /// Contains routines to generate random date/time values to an ADO.NET DataTable object.
    /// </summary>
    public class RandomDateTimeDataTable
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private RandomNumber _rn = new RandomNumber();
        private RandomValue _rv = new RandomValue();

        //private variables for properties

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomDateTimeDataTable()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Creates a DataTable containing a set of random DateTime values.
        /// </summary>
        /// <param name="numRows">Num of rows with DateTime values to generate.</param>
        /// <param name="dataRequest">RandomDateTimeDataRequest object contains the definition for how to generate the random DateTime values.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        public DataTable CreateRandomDataTable(int numRows, RandomDateTimeDataRequest dataRequest)
        {
            DataTable dt = null;
            enRandomOffsetType randOffsetType = enRandomOffsetType.enUnknown;
            enRandomIncrementType randDateIncrementType = enRandomIncrementType.enUnknown;
            enDateConversionType dateConversionType = enDateConversionType.DoNotConvert;

            if (dataRequest.ConvertGeneratedValueToInteger)
            {
                if (dataRequest.ConvertDateTo32BitInteger)
                    dateConversionType = enDateConversionType.ConvertDateTo32bitInt;
                else if (dataRequest.ConvertTimeTo32BitInteger)
                    dateConversionType = enDateConversionType.ConvertTimeTo32bitInt;
                else if (dataRequest.ConvertDateTimeTo64BitInteger)
                    dateConversionType = enDateConversionType.ConvertDateTimeTo64bitInt;
                else
                    dateConversionType = enDateConversionType.DoNotConvert;

            }

            if (dataRequest.RangeOfDates)
            {
                dt = CreateRangeDataTable(numRows, dataRequest.EarliestDate, dataRequest.LatestDate, dataRequest.SpecifyTimeForEachDay, dataRequest.EarliestTime, dataRequest.LatestTime, dateConversionType);
            }
            else if (dataRequest.OffsetFromCurrentDate)
            {

                randOffsetType = GetRandomOffsetType(dataRequest);
                dt = CreateOffsetFromCurrentDateDataTable(randOffsetType, numRows, dataRequest.MinimumOffset, dataRequest.MaximumOffset, dataRequest.SpecifyTimeForEachDay, dataRequest.EarliestTime, dataRequest.LatestTime, dateConversionType);
            }
            else if (dataRequest.OffsetFromDataTableDate)
            {

                randOffsetType = GetRandomOffsetType(dataRequest);
                dt = CreateOffsetFromDataTableDate(randOffsetType, numRows, dataRequest.MinimumOffset, dataRequest.MaximumOffset, dataRequest.SpecifyTimeForEachDay, dataRequest.EarliestTime, dataRequest.LatestTime, dateConversionType);
            }
            else if (dataRequest.OutputSequentialDates)
            {
                randDateIncrementType = GetRandomDateIncrementType(dataRequest);
                dt = CreateDateSequenceDataTable(randDateIncrementType, numRows, dataRequest.IncrementSize, dataRequest.StartSequentialDate, dataRequest.EndSequentialDate, dataRequest.SpecifyTimeForEachDay, dataRequest.EarliestTime, dataRequest.LatestTime, dataRequest.MinNumDatesPerIncrement, dataRequest.MaxNumDatesPerIncrement, dataRequest.InitStartSequentialDate, dateConversionType); 
            }
            else
            {
                dt = new DataTable();
            }


            return dt;
        }

        private enRandomOffsetType GetRandomOffsetType(RandomDateTimeDataRequest dataRequest)
        {
            enRandomOffsetType offsetType = enRandomOffsetType.enUnknown;

            if(dataRequest.YearsOffset)
            {
                offsetType = enRandomOffsetType.enYears;
            }
            else if (dataRequest.MonthsOffset)
            {
                offsetType = enRandomOffsetType.enMonths;
            }
            else
            {
                //DaysOffset
                offsetType = enRandomOffsetType.enDays;
            }

            return offsetType;
        }

        private enRandomIncrementType GetRandomDateIncrementType(RandomDateTimeDataRequest dataRequest)
        {
            enRandomIncrementType incrementType = enRandomIncrementType.enUnknown;

            if (dataRequest.YearsIncrement)
            {
                incrementType = enRandomIncrementType.enYears;
            }
            else if (dataRequest.MonthsIncrement)
            {
                incrementType = enRandomIncrementType.enMonths;
            }
            else
            {
                incrementType = enRandomIncrementType.enDays;
            }

            return incrementType;
        }

        /// <summary>
        /// Creates set of random DateTime values that fall within a defined range.
        /// </summary>
        /// <param name="numRows">Number of random DataTime values to generate.                                                             </param>
        /// <param name="fromDate">Minimum date to generate.</param>
        /// <param name="toDate">Maximum date to generate.</param>
        /// <param name="generateRandomTime">Set to true to generate a time value as part of the DateTime value being generated.</param>
        /// <param name="fromTime">Minimum time to generate.</param>
        /// <param name="toTime">Maximum time to generate.</param>
        /// <param name="dateConversionType">Determines whether or not to convert the DateTime value to an integer. Useful for data warehousing scenarios.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        public DataTable CreateRangeDataTable(int numRows, string fromDate, string toDate, bool generateRandomTime, string fromTime, string toTime, enDateConversionType dateConversionType)
        {
            DataTable dt = new DataTable();
            
            try
            {
                TimeSpan fromDateTs = Convert.ToDateTime(fromDate).Subtract(DateTime.MinValue);
                TimeSpan toDateTs = Convert.ToDateTime(toDate).Subtract(DateTime.MinValue);
                double fromDays = fromDateTs.TotalDays;
                double toDays = toDateTs.TotalDays;
                TimeSpan fromTimeTs = Convert.ToDateTime(fromTime).TimeOfDay;
                TimeSpan toTimeTs = Convert.ToDateTime(toTime).TimeOfDay;
                double fromSeconds = fromTimeTs.TotalSeconds;
                double toSeconds = toTimeTs.TotalSeconds;

                

                DataColumn dc = new DataColumn("RandomValue");
                switch (dateConversionType)
                {
                    case enDateConversionType.DoNotConvert:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    case enDateConversionType.ConvertDateTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertTimeTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertDateTimeTo64bitInt:
                        dc.DataType = Type.GetType("System.Int64");
                        break;
                    default:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    Double randNum = _rn.GenerateRandomNumber(fromDays, toDays);
                    TimeSpan ts = new TimeSpan((int)randNum, 0, 0, 0, 0);
                    DateTime dtm = DateTime.MinValue.Add(ts);
                    if (generateRandomTime)
                    {
                        randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
                        TimeSpan ts2 = new TimeSpan(0, 0, 0, (int)randNum, 0);
                        dtm = dtm.AddSeconds(ts2.TotalSeconds);
                    }
                    //dr[0] = dtm;
                    switch (dateConversionType)
                    {
                        case enDateConversionType.DoNotConvert:
                            dr[0] = dtm;
                            break;
                        case enDateConversionType.ConvertDateTo32bitInt:
                            dr[0] = Convert.ToInt32(dtm.ToString("yyyyMMdd"));
                            break;
                        case enDateConversionType.ConvertTimeTo32bitInt:
                            dr[0] = Convert.ToInt32(dtm.ToString("HHmmss"));
                            break;
                        case enDateConversionType.ConvertDateTimeTo64bitInt:
                            dr[0] = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));
                            break;
                        default:
                            dr[0] = dtm;
                            break;
                    }
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
        /// Routines for generating offsets that can be used to add or subtract from dates in a database table or an application.
        /// </summary>
        /// <param name="offsetType">enRandomOffsetType enum that determines type of offset to generate (seconds, minutes, hours, days or years).</param>
        /// <param name="numRows">Number of random offsets to generate.</param>
        /// <param name="minimumOffset">Minimum offset number. </param>
        /// <param name="maximumOffset">Maximum offset number.</param>
        /// <param name="generateRandomTime">Set to true to generate a time for the random offset.</param>
        /// <param name="fromTime">Minium time to generate.</param>
        /// <param name="toTime">Maximum time to generate.</param>
        /// <param name="dateConversionType">Determines whether or not to convert the DateTime value to an integer. Useful for data warehousing scenarios.</param>
        /// <returns>ADO.NET DataTable containing the set of random values. First column contains the number of offset days that can be used to adjust a date in a database table or an application. </returns>
        public DataTable CreateOffsetFromDataTableDate(enRandomOffsetType offsetType, int numRows, string minimumOffset, string maximumOffset, bool generateRandomTime, string fromTime, string toTime, enDateConversionType dateConversionType)
        {
            DataTable dt = new DataTable();
            

            try
            {
                double minOffset = Convert.ToDouble(minimumOffset);
                double maxOffset = Convert.ToDouble(maximumOffset);
                TimeSpan currDateTs = DateTime.Now.Subtract(DateTime.MinValue);
                double currDateDays = currDateTs.TotalDays;
                double minOffsetDays = currDateDays + minOffset;
                double maxOffsetDays = currDateDays + maxOffset;
                if (offsetType == enRandomOffsetType.enYears)
                {
                    DateTime tempDtm = DateTime.Now.AddYears((int)minOffset);
                    minOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                    tempDtm = DateTime.Now.AddYears((int)maxOffset);
                    maxOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                }
                else if (offsetType == enRandomOffsetType.enMonths)
                {
                    DateTime tempDtm = DateTime.Now.AddMonths((int)minOffset);
                    minOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                    tempDtm = DateTime.Now.AddMonths((int)maxOffset);
                    maxOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                }
                else if (offsetType == enRandomOffsetType.enDays)
                {
                    //minOffset and maxOffset already specify days
                    minOffsetDays = currDateDays + minOffset;
                    maxOffsetDays = currDateDays + maxOffset;
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to process offset type passed to CreateOffsetFromDataTableDate routine.");
                    throw new System.Exception(_msg.ToString());
                }

                TimeSpan fromTimeTs = generateRandomTime ? Convert.ToDateTime(fromTime).TimeOfDay : Convert.ToDateTime("01/01/2000 00:00:00").TimeOfDay;
                TimeSpan toTimeTs = generateRandomTime ? Convert.ToDateTime(toTime).TimeOfDay : Convert.ToDateTime("01/01/2000 23:59:59").TimeOfDay;
                double fromSeconds = fromTimeTs.TotalSeconds;
                double toSeconds = toTimeTs.TotalSeconds;


                DataColumn dc = new DataColumn("RandomValue");
                dc.DataType = Type.GetType("System.Int32");
                dt.Columns.Add(dc);
                DataColumn dc2 = new DataColumn("CurrDatePlusOffset");
                dc2.DataType = Type.GetType("System.DateTime");
                dt.Columns.Add(dc2);

                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    Double randNum = _rn.GenerateRandomNumber(minOffsetDays, maxOffsetDays);
                    TimeSpan ts = new TimeSpan((int)randNum, 0, 0, 0, 0);
                    DateTime dtm = DateTime.MinValue.Add(ts);
                    if (generateRandomTime)
                    {
                        randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
                        TimeSpan ts2 = new TimeSpan(0, 0, 0, (int)randNum, 0);
                        dtm = dtm.AddSeconds(ts2.TotalSeconds);
                    }
                    TimeSpan offsetTs = dtm.Subtract(DateTime.Now);
                    int offset = Convert.ToInt32(offsetTs.TotalDays);
                    dr[0] = offset;
                    dr[1] = dtm;
                    dt.Rows.Add(dr);
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateOffsetFromDataTableDate routine.\r\n");
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
        /// Routines for generating dates that are offset from the current date/time value.
        /// </summary>
        /// <param name="offsetType">enRandomOffsetType enum that determines type of offset to generate (seconds, minutes, hours, days or years).</param>
        /// <param name="numRows">Number of random offsets to generate.</param>
        /// <param name="minimumOffset">Minimum offset number. </param>
        /// <param name="maximumOffset">Maximum offset number.</param>
        /// <param name="generateRandomTime">Set to true to generate a time for the random offset.</param>
        /// <param name="fromTime">Minium time to generate.</param>
        /// <param name="toTime">Maximum time to generate.</param>
        /// <param name="dateConversionType">Determines whether or not to convert the DateTime value to an integer. Useful for data warehousing scenarios.</param>
        /// <returns>ADO.NET DataTable containing the set of random values. First column of the result set contains a date that has been offset from current date.</returns>
        public DataTable CreateOffsetFromCurrentDateDataTable(enRandomOffsetType offsetType, int numRows, string minimumOffset, string maximumOffset, bool generateRandomTime, string fromTime, string toTime, enDateConversionType dateConversionType)
        {
            DataTable dt = new DataTable();


            try
            {
                double minOffset = Convert.ToDouble(minimumOffset);
                double maxOffset = Convert.ToDouble(maximumOffset);
                TimeSpan currDateTs = DateTime.Now.Subtract(DateTime.MinValue);
                double currDateDays = currDateTs.TotalDays;
                double minOffsetDays = currDateDays + minOffset;
                double maxOffsetDays = currDateDays + maxOffset;
                if (offsetType == enRandomOffsetType.enYears)
                {
                    DateTime tempDtm = DateTime.Now.AddYears((int)minOffset);
                    minOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                    tempDtm = DateTime.Now.AddYears((int)maxOffset);
                    maxOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                }
                else if (offsetType == enRandomOffsetType.enMonths)
                {
                    DateTime tempDtm = DateTime.Now.AddMonths((int)minOffset);
                    minOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                    tempDtm = DateTime.Now.AddMonths((int)maxOffset);
                    maxOffsetDays = tempDtm.Subtract(DateTime.MinValue).TotalDays;
                }
                else if (offsetType == enRandomOffsetType.enDays)
                {
                    //minOffset and maxOffset already specify days
                    minOffsetDays = currDateDays + minOffset;
                    maxOffsetDays = currDateDays + maxOffset;
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to process offset type passed to CreateOffsetFromDataTableDate routine.");
                    throw new System.Exception(_msg.ToString());
                }

                TimeSpan fromTimeTs = generateRandomTime ? Convert.ToDateTime(fromTime).TimeOfDay : Convert.ToDateTime("01/01/2000 00:00:00").TimeOfDay;
                TimeSpan toTimeTs = generateRandomTime ? Convert.ToDateTime(toTime).TimeOfDay : Convert.ToDateTime("01/01/2000 23:59:59").TimeOfDay;
                double fromSeconds = fromTimeTs.TotalSeconds;
                double toSeconds = toTimeTs.TotalSeconds;


                DataColumn dc = new DataColumn("RandomValue");
                //dc.DataType = Type.GetType("System.DateTime");
                switch (dateConversionType)
                {
                    case enDateConversionType.DoNotConvert:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    case enDateConversionType.ConvertDateTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertTimeTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertDateTimeTo64bitInt:
                        dc.DataType = Type.GetType("System.Int64");
                        break;
                    default:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
                DataColumn dc2 = new DataColumn("OffsetDays");
                dc2.DataType = Type.GetType("System.Int32");
                dt.Columns.Add(dc2);


                for (int i = 0; i < numRows; i++)
                {
                    DataRow dr = dt.NewRow();
                    Double randNum = _rn.GenerateRandomNumber(minOffsetDays, maxOffsetDays);
                    TimeSpan ts = new TimeSpan((int)randNum, 0, 0, 0, 0);
                    DateTime dtm = DateTime.MinValue.Add(ts);
                    if (generateRandomTime)
                    {
                        randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
                        TimeSpan ts2 = new TimeSpan(0, 0, 0, (int)randNum, 0);
                        dtm = dtm.AddSeconds(ts2.TotalSeconds);
                    }
                    TimeSpan offsetTs = dtm.Subtract(DateTime.Now);
                    int offset = Convert.ToInt32(offsetTs.TotalDays);
                    //dr[0] = dtm;
                    switch (dateConversionType)
                    {
                        case enDateConversionType.DoNotConvert:
                            dr[0] = dtm;
                            break;
                        case enDateConversionType.ConvertDateTo32bitInt:
                            dr[0] = Convert.ToInt32(dtm.ToString("yyyyMMdd"));
                            break;
                        case enDateConversionType.ConvertTimeTo32bitInt:
                            dr[0] = Convert.ToInt32(dtm.ToString("HHmmss"));
                            break;
                        case enDateConversionType.ConvertDateTimeTo64bitInt:
                            dr[0] = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));
                            break;
                        default:
                            dr[0] = dtm;
                            break;
                    }
                    dr[1] = offset;
                    dt.Rows.Add(dr);
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateOffsetFromDataTableDate routine.\r\n");
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
        /// Routines for testing date/time offset processing.
        /// </summary>
        /// <param name="offsetType">enRandomOffsetType enum that determines type of offset to generate (seconds, minutes, hours, days or years).</param>
        /// <param name="numRows">Number of random offsets to generate.</param>
        /// <param name="minimumOffset">Minimum offset number. </param>
        /// <param name="maximumOffset">Maximum offset number.</param>
        /// <param name="generateRandomTime">Set to true to generate a time for the random offset.</param>
        /// <param name="fromTime">Minium time to generate.</param>
        /// <param name="toTime">Maximum time to generate.</param>
        /// <param name="dateConversionType">Determines whether or not to convert the DateTime value to an integer. Useful for data warehousing scenarios.</param>
        /// <returns>ADO.NET DataTable containing the set of random values. First column contains a date/time that has been offset from the base value stored in the second column (this is a stand-in for the a value that might be found in a database table or an application. </returns>
        public DataTable CreateOffsetPreviewDataTable(enRandomOffsetType offsetType, int numRows, string minimumOffset, string maximumOffset, bool generateRandomTime, string fromTime, string toTime, enDateConversionType dateConversionType)
        {
            DataTable dt = new DataTable();
            DateTime currDate = DateTime.Now;
            DateTime minDate = DateTime.MinValue;
            DateTime maxDate = DateTime.MaxValue;

            try
            {
                DataColumn dc = new DataColumn("RandomValue");
                //dc.DataType = Type.GetType("System.DateTime");
                switch (dateConversionType)
                {
                    case enDateConversionType.DoNotConvert:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    case enDateConversionType.ConvertDateTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertTimeTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertDateTimeTo64bitInt:
                        dc.DataType = Type.GetType("System.Int64");
                        break;
                    default:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }
                dt.Columns.Add(dc);
                DataColumn dc2 = new DataColumn("CurrentDate");
                dc2.DataType = Type.GetType("System.DateTime");
                dt.Columns.Add(dc2);
                //DataColumn dc3 = new DataColumn("RandomNum");
                //dc3.DataType = Type.GetType("System.Int32");
                //dt.Columns.Add(dc3);
                //DataColumn dc4 = new DataColumn("MinDays");
                //dc4.DataType = Type.GetType("System.Int32");
                //dt.Columns.Add(dc4);
                //DataColumn dc5 = new DataColumn("MaxDays");
                //dc5.DataType = Type.GetType("System.Int32");
                //dt.Columns.Add(dc5);
                //DataColumn dc6 = new DataColumn("nRowNum");
                //dc6.DataType = Type.GetType("System.Int32");
                //dt.Columns.Add(dc6);


                for (int i = 0; i < numRows; i++)
                {
                    double minOffset = Convert.ToDouble(minimumOffset);
                    double maxOffset = Convert.ToDouble(maximumOffset);
                    currDate = _rv.GenerateRandomDate(Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("12/31/2029"));
                    minDate = DateTime.MinValue;
                    maxDate = DateTime.MaxValue;
                    if (offsetType == enRandomOffsetType.enYears)
                    {
                        minDate = currDate.AddYears((int)minOffset);
                        maxDate = currDate.AddYears((int)maxOffset);
                    }
                    else if (offsetType == enRandomOffsetType.enMonths)
                    {
                        minDate = currDate.AddMonths((int)minOffset);
                        maxDate = currDate.AddMonths((int)maxOffset);
                    }
                    else if (offsetType == enRandomOffsetType.enDays)
                    {
                        minDate = currDate.AddDays((int)minOffset);
                        maxDate = currDate.AddDays((int)maxOffset);
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to process offset type passed to CreateOffsetFromDataTableDate routine.");
                        throw new System.Exception(_msg.ToString());
                    }
                    

                    TimeSpan fromTimeTs = generateRandomTime ? Convert.ToDateTime(fromTime).TimeOfDay : Convert.ToDateTime("01/01/2000 00:00:00").TimeOfDay;
                    TimeSpan toTimeTs = generateRandomTime ? Convert.ToDateTime(toTime).TimeOfDay : Convert.ToDateTime("01/01/2000 23:59:59").TimeOfDay;
                    double fromSeconds = fromTimeTs.TotalSeconds;
                    double toSeconds = toTimeTs.TotalSeconds;



                    DataRow dr = dt.NewRow();
                    int minDays = (int)minDate.Subtract(DateTime.MinValue).TotalDays;
                    int maxDays = (int)maxDate.Subtract(DateTime.MinValue).TotalDays;
                    int randNum = _rn.GenerateRandomInt(minDays, maxDays);
                    randNum = _rn.GenerateRandomInt(minDays, maxDays);  //Second random number is a workaround that fixes problem of random numbers
                                                                        //sequencing from low to high based on earliest to latest dates.
                                                                        //(Jan. 2015: Do not know what is causing sequencing to occur.)
                                                                        //(Guess: might be related to GenerateRandomDate above.)
                    //int randNum = _rn.GenerateRandomInt(689948, 697252);  //test
                    int saveRandNum = randNum;
                    TimeSpan ts = new TimeSpan(randNum, 0, 0, 0, 0);
                    DateTime dtm = DateTime.MinValue.Add(ts);
                    if (generateRandomTime)
                    {
                        randNum = _rn.GenerateRandomInt((int)fromSeconds, (int)toSeconds);
                        TimeSpan ts2 = new TimeSpan(0, 0, 0, randNum, 0);
                        dtm = dtm.AddSeconds(ts2.TotalSeconds);
                    }
                    //dr[0] = dtm;
                    switch (dateConversionType)
                    {
                        case enDateConversionType.DoNotConvert:
                            dr[0] = dtm;
                            break;
                        case enDateConversionType.ConvertDateTo32bitInt:
                            dr[0] = Convert.ToInt32(dtm.ToString("yyyyMMdd"));
                            break;
                        case enDateConversionType.ConvertTimeTo32bitInt:
                            dr[0] = Convert.ToInt32(dtm.ToString("HHmmss"));
                            break;
                        case enDateConversionType.ConvertDateTimeTo64bitInt:
                            dr[0] = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));
                            break;
                        default:
                            dr[0] = dtm;
                            break;
                    }
                    dr[1] = currDate;
                    //dr[2] = saveRandNum;
                    //dr[3] = minDays;
                    //dr[4] = maxDays;
                    //dr[5] = i;
                    dt.Rows.Add(dr);
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateOffsetFromDataTableDate routine.\r\n");
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
        /// Creates a set of sequential numbers that start from a user specified start number and are incremented by a user specified number.
        /// </summary>
        /// <param name="incrementType">Time slice to increment with (seconds, minutes, hours, days, years).</param>
        /// <param name="numRows">Number of random values to generate.</param>
        /// <param name="incrementSize">Number to increment by.</param>
        /// <param name="startDateForSequence">First date in the sequence.</param>
        /// <param name="endDateForSequence">Maximum date in the sequence.</param>
        /// <param name="generateRandomTime">If true, a randomized time value will be generated for each sequential date.</param>
        /// <param name="fromTime">Minimum time value to generate.</param>
        /// <param name="toTime">Maximum time value to generate.</param>
        /// <param name="minNumDatesPerIncrement">For each date increment, the minimum number of dates that can be generated with same date into the sequence.</param>
        /// <param name="maxNumDatesPerIncrement">For each date increment, the maximum number of dates that can be generated with same date into the sequence.</param>
        /// <param name="initStartDateForSequence">If maximum date is passed during sequence generation, the sequence will be restarted with this date and new values will be appended to the existing sequence.</param>
        /// <param name="dateConversionType">Determines whether or not to convert the DateTime value to an integer. Useful for data warehousing scenarios.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        public DataTable CreateDateSequenceDataTable(enRandomIncrementType incrementType, int numRows, string incrementSize, string startDateForSequence, string endDateForSequence, bool generateRandomTime, string fromTime, string toTime, string minNumDatesPerIncrement, string maxNumDatesPerIncrement, string initStartDateForSequence, enDateConversionType dateConversionType)
        {
            DataTable dt = new DataTable();
            
            int sizeOfIncrement = 1;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            DateTime currDate = DateTime.Now;
            DateTime initDate = DateTime.MinValue;
            int minIncrementDates = 1;
            int maxIncrementDates = 1;
            TimeSpan fromTimeTs = Convert.ToDateTime(fromTime).TimeOfDay;
            TimeSpan toTimeTs = Convert.ToDateTime(toTime).TimeOfDay;
            double fromSeconds = fromTimeTs.TotalSeconds;
            double toSeconds = toTimeTs.TotalSeconds;
            double randNum = 0.0;

            try
            {
                sizeOfIncrement = AppTextGlobals.ConvertStringToInt(incrementSize, 1);
                startDate = AppTextGlobals.ConvertStringToDateTime(startDateForSequence,new DateTime(1000,1,1));
                endDate = AppTextGlobals.ConvertStringToDateTime(endDateForSequence, new DateTime(5999, 12, 31));
                initDate = AppTextGlobals.ConvertStringToDateTime(initStartDateForSequence, new DateTime(1000,1,1));
                currDate = IncrementDateTime(incrementType, -sizeOfIncrement, startDate, startDate);

                minIncrementDates = AppTextGlobals.ConvertStringToInt(minNumDatesPerIncrement,1);
                maxIncrementDates = AppTextGlobals.ConvertStringToInt(maxNumDatesPerIncrement,1);

                DataColumn dc = new DataColumn("RandomValue");
                //dc.DataType = Type.GetType("System.DateTime");
                switch (dateConversionType)
                {
                    case enDateConversionType.DoNotConvert:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    case enDateConversionType.ConvertDateTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertTimeTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertDateTimeTo64bitInt:
                        dc.DataType = Type.GetType("System.Int64");
                        break;
                    default:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }

                dt.Columns.Add(dc);


                for (int i = 0; i < numRows; i++)
                {
                    currDate = IncrementDateTime(incrementType, sizeOfIncrement, currDate, startDate);

                    if (currDate > endDate)
                        currDate = initDate;

                    int numDatesToGenerate = _rn.GenerateRandomInt(minIncrementDates, maxIncrementDates);
                    DateTime dtm = currDate;

                    for (int n = 0; n < numDatesToGenerate; n++)
                    {
                        DataRow dr = dt.NewRow();
                        if (generateRandomTime)
                        {
                            randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
                            TimeSpan ts2 = new TimeSpan(0, 0, 0, (int)randNum, 0);
                            try
                            {
                                dtm = currDate.AddSeconds(ts2.TotalSeconds);
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                dtm = startDate;
                                currDate = startDate;
                            }
                        }
                        //dr[0] = dtm;
                        switch (dateConversionType)
                        {
                            case enDateConversionType.DoNotConvert:
                                dr[0] = dtm;
                                break;
                            case enDateConversionType.ConvertDateTo32bitInt:
                                dr[0] = Convert.ToInt32(dtm.ToString("yyyyMMdd"));
                                break;
                            case enDateConversionType.ConvertTimeTo32bitInt:
                                dr[0] = Convert.ToInt32(dtm.ToString("HHmmss"));
                                break;
                            case enDateConversionType.ConvertDateTimeTo64bitInt:
                                dr[0] = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));
                                break;
                            default:
                                dr[0] = dtm;
                                break;
                        }
                        dt.Rows.Add(dr);
                    }


                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateSequencePreviewDataTable routine.\r\n");
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
        /// Creates a set of sequential numbers that start from a user specified start number and are incremented by a user specified number. This is a testing routine.
        /// </summary>
        /// <param name="incrementType">Time slice to increment with (seconds, minutes, hours, days, years).</param>
        /// <param name="numRows">Number of random values to generate.</param>
        /// <param name="incrementSize">Number to increment by.</param>
        /// <param name="startDateForSequence">First date in the sequence.</param>
        /// <param name="endDateForSequence">Maximum date in the sequence.</param>
        /// <param name="generateRandomTime">If true, a randomized time value will be generated for each sequential date.</param>
        /// <param name="fromTime">Minimum time value to generate.</param>
        /// <param name="toTime">Maximum time value to generate.</param>
        /// <param name="minNumDatesPerIncrement">For each date increment, the minimum number of dates that can be generated with same date into the sequence.</param>
        /// <param name="maxNumDatesPerIncrement">For each date increment, the maximum number of dates that can be generated with same date into the sequence.</param>
        /// <param name="initStartDateForSequence">If maximum date is passed during sequence generation, the sequence will be restarted with this date and new values will be appended to the existing sequence.</param>
        /// <param name="dateConversionType">Determines whether or not to convert the DateTime value to an integer. Useful for data warehousing scenarios.</param>
        /// <returns>ADO.NET DataTable containing the set of random values.</returns>
        /// <remarks>This routine is used for testing.</remarks>
        public DataTable CreateDateSequencePreviewDataTable(enRandomIncrementType incrementType, int numRows, string incrementSize, string startDateForSequence, string endDateForSequence, bool generateRandomTime, string fromTime, string toTime, string minNumDatesPerIncrement, string maxNumDatesPerIncrement, string initStartDateForSequence, enDateConversionType dateConversionType)
        {
            DataTable dt = new DataTable();

            int sizeOfIncrement = 1;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            DateTime currDate = DateTime.Now;
            DateTime initDate = DateTime.MinValue;
            int minIncrementDates = 1;
            int maxIncrementDates = 1;
            TimeSpan fromTimeTs = Convert.ToDateTime(fromTime).TimeOfDay;
            TimeSpan toTimeTs = Convert.ToDateTime(toTime).TimeOfDay;
            double fromSeconds = fromTimeTs.TotalSeconds;
            double toSeconds = toTimeTs.TotalSeconds;
            double randNum = 0.0;

            try
            {
                sizeOfIncrement = AppTextGlobals.ConvertStringToInt(incrementSize, 1);
                startDate = AppTextGlobals.ConvertStringToDateTime(startDateForSequence, new DateTime(1000, 1, 1));
                endDate = AppTextGlobals.ConvertStringToDateTime(endDateForSequence, new DateTime(5999, 12, 31));
                initDate = AppTextGlobals.ConvertStringToDateTime(initStartDateForSequence, new DateTime(1000, 1, 1));
                currDate = IncrementDateTime(incrementType, -sizeOfIncrement, startDate, startDate);

                minIncrementDates = AppTextGlobals.ConvertStringToInt(minNumDatesPerIncrement, 1);
                maxIncrementDates = AppTextGlobals.ConvertStringToInt(maxNumDatesPerIncrement, 1);

                DataColumn dc = new DataColumn("RandomValue");
                //dc.DataType = Type.GetType("System.DateTime");
                switch (dateConversionType)
                {
                    case enDateConversionType.DoNotConvert:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                    case enDateConversionType.ConvertDateTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertTimeTo32bitInt:
                        dc.DataType = Type.GetType("System.Int32");
                        break;
                    case enDateConversionType.ConvertDateTimeTo64bitInt:
                        dc.DataType = Type.GetType("System.Int64");
                        break;
                    default:
                        dc.DataType = Type.GetType("System.DateTime");
                        break;
                }

                dt.Columns.Add(dc);


                for (int i = 0; i < numRows; i++)
                {
                    currDate = IncrementDateTime(incrementType, sizeOfIncrement, currDate, startDate);

                    if (currDate > endDate)
                        currDate = initDate;

                    int numDatesToGenerate = _rn.GenerateRandomInt(minIncrementDates, maxIncrementDates);
                    DateTime dtm = currDate;

                    for (int n = 0; n < numDatesToGenerate; n++)
                    {
                        DataRow dr = dt.NewRow();
                        if (generateRandomTime)
                        {
                            randNum = _rn.GenerateRandomNumber(fromSeconds, toSeconds);
                            TimeSpan ts2 = new TimeSpan(0, 0, 0, (int)randNum, 0);
                            try
                            {
                                dtm = currDate.AddSeconds(ts2.TotalSeconds);
                            }
                            catch (System.ArgumentOutOfRangeException)
                            {
                                dtm = startDate;
                                currDate = startDate;
                            }
                        }
                        //dr[0] = dtm;
                        switch (dateConversionType)
                        {
                            case enDateConversionType.DoNotConvert:
                                dr[0] = dtm;
                                break;
                            case enDateConversionType.ConvertDateTo32bitInt:
                                dr[0] = Convert.ToInt32(dtm.ToString("yyyyMMdd"));
                                break;
                            case enDateConversionType.ConvertTimeTo32bitInt:
                                dr[0] = Convert.ToInt32(dtm.ToString("HHmmss"));
                                break;
                            case enDateConversionType.ConvertDateTimeTo64bitInt:
                                dr[0] = Convert.ToInt64(dtm.ToString("yyyyMMddHHmmss"));
                                break;
                            default:
                                dr[0] = dtm;
                                break;
                        }
                        dt.Rows.Add(dr);
                    }


                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error in CreateSequencePreviewDataTable routine.\r\n");
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
        /// Routine to increment a DateTime value.
        /// </summary>
        /// <param name="incrementType">Type of increment defined: seconds, minutes, hours, days or years.</param>
        /// <param name="sizeOfIncrement">Value of the increment to be applied.</param>
        /// <param name="origDateTime">Original date/time value to be incremented.</param>
        /// <param name="startDate">Date to return in the event the increment fails on an error.</param>
        /// <returns>DateTime value with increment applied.</returns>
        public DateTime IncrementDateTime(enRandomIncrementType incrementType, int sizeOfIncrement, DateTime origDateTime, DateTime startDate)
        {
            DateTime newDateTime = new DateTime(2000, 1, 1);

            try
            {
                switch (incrementType)
                {
                    case enRandomIncrementType.enYears:
                        newDateTime = origDateTime.AddYears(sizeOfIncrement);
                        break;
                    case enRandomIncrementType.enMonths:
                        newDateTime = origDateTime.AddMonths(sizeOfIncrement);
                        break;
                    case enRandomIncrementType.enDays:
                        newDateTime = origDateTime.AddDays(sizeOfIncrement);
                        break;
                    default:
                        newDateTime = origDateTime.AddDays(sizeOfIncrement);
                        break;
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                newDateTime = startDate;
            }


            return newDateTime;
        }

    }//end class
}//end namespace
