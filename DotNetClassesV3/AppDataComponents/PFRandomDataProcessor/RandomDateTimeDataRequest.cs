//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.IO;

namespace PFRandomDataProcessor
{
    /// <summary>
    /// Contains definition for creating a random DateTime value.
    /// </summary>
    public class RandomDateTimeDataRequest
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties
        private string _name = string.Empty;
        private bool _rangeOfDates = true;
        private string _earliestDate = string.Empty;
        private string _latestDate = string.Empty;
        private bool _offsetFromCurrentDate = false;
        private bool _offsetFromDataTableDate = false;
        private bool _yearsOffset = true;
        private bool _monthsOffset = false;
        private bool _daysOffset = false;
        private string _minimumOffset = string.Empty;
        private string _maximumOffset = string.Empty;
        private string _dateToOffset = string.Empty;
        private bool _specifyTimeForEachDay = true;
        private string _earliestTime = string.Empty;
        private string _latestTime = string.Empty;
        private bool _outputSequentialDates = false;
        private string _startSequentialDate = "";           //this date can be changed by processing code as sequential date lists are being generated in sets of rows
        private string _endSequentialDate = "";
        private string _incrementSize = "1";
        private bool _yearsIncrement = true;
        private bool _monthsIncrement = false;
        private bool _daysIncrement = false;
        private string _minNumDatesPerIncrement = "";
        private string _maxNumDatesPerIncrement = "";
        private string _initStartSequentialDate = "";       //used to correctly restart a date sequence after the maximum date has been passed
        private int _numRandomDataItems = 1000;
        private bool _convertGeneratedValueToInteger = false;
        private bool _convertDateTo32BitInteger = false;
        private bool _convertTimeTo32BitInteger = false;
        private bool _convertDateTimeTo64BitInteger = false;


        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomDateTimeDataRequest()
        {
            ;
        }

        //properties

        /// <summary>
        /// Name Property.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// RangeOfDates Property.
        /// </summary>
        public bool RangeOfDates
        {
            get
            {
                return _rangeOfDates;
            }
            set
            {
                _rangeOfDates = value;
            }
        }

        /// <summary>
        /// EarliestDate Property.
        /// </summary>
        public string EarliestDate
        {
            get
            {
                return _earliestDate;
            }
            set
            {
                _earliestDate = value;
            }
        }

        /// <summary>
        /// LatestDate Property.
        /// </summary>
        public string LatestDate
        {
            get
            {
                return _latestDate;
            }
            set
            {
                _latestDate = value;
            }
        }

        /// <summary>
        /// OffsetFromCurrentDate Property.
        /// </summary>
        public bool OffsetFromCurrentDate
        {
            get
            {
                return _offsetFromCurrentDate;
            }
            set
            {
                _offsetFromCurrentDate = value;
            }
        }

        /// <summary>
        /// OffsetFromDataTableDate Property.
        /// </summary>
        public bool OffsetFromDataTableDate
        {
            get
            {
                return _offsetFromDataTableDate;
            }
            set
            {
                _offsetFromDataTableDate = value;
            }
        }

        /// <summary>
        /// YearsOffset Property.
        /// </summary>
        public bool YearsOffset
        {
            get
            {
                return _yearsOffset;
            }
            set
            {
                _yearsOffset = value;
            }
        }

        /// <summary>
        /// MonthsOffset Property.
        /// </summary>
        public bool MonthsOffset
        {
            get
            {
                return _monthsOffset;
            }
            set
            {
                _monthsOffset = value;
            }
        }

        /// <summary>
        /// DaysOffset Property.
        /// </summary>
        public bool DaysOffset
        {
            get
            {
                return _daysOffset;
            }
            set
            {
                _daysOffset = value;
            }
        }

        /// <summary>
        /// MinimumOffset Property.
        /// </summary>
        public string MinimumOffset
        {
            get
            {
                return _minimumOffset;
            }
            set
            {
                _minimumOffset = value;
            }
        }

        /// <summary>
        /// MaximumOffset Property.
        /// </summary>
        public string MaximumOffset
        {
            get
            {
                return _maximumOffset;
            }
            set
            {
                _maximumOffset = value;
            }
        }

        /// <summary>
        /// DateToOffset Property.
        /// </summary>
        /// <remarks>Set to empty string or null to generate a preview list. If set to a value, a generate request will return only one offset using this property as the base to offset.</remarks>
        public string DateToOffset
        {
            get
            {
                return _dateToOffset;
            }
            set
            {
                _dateToOffset = value;
            }
        }

        /// <summary>
        /// SpecifyTimeForEachDay Property.
        /// </summary>
        public bool SpecifyTimeForEachDay
        {
            get
            {
                return _specifyTimeForEachDay;
            }
            set
            {
                _specifyTimeForEachDay = value;
            }
        }

        /// <summary>
        /// EarliestTime Property.
        /// </summary>
        public string EarliestTime
        {
            get
            {
                return _earliestTime;
            }
            set
            {
                _earliestTime = value;
            }
        }

        /// <summary>
        /// LatestTime Property.
        /// </summary>
        public string LatestTime
        {
            get
            {
                return _latestTime;
            }
            set
            {
                _latestTime = value;
            }
        }

        /// <summary>
        /// OutputSequentialDates Property.
        /// </summary>
        public bool OutputSequentialDates
        {
            get
            {
                return _outputSequentialDates;
            }
            set
            {
                _outputSequentialDates = value;
            }
        }

        /// <summary>
        /// StartSequentialDate Property.
        /// </summary>
        public string StartSequentialDate
        {
            get
            {
                return _startSequentialDate;
            }
            set
            {
                _startSequentialDate = value;
            }
        }

        /// <summary>
        /// EndSequentialDate Property.
        /// </summary>
        public string EndSequentialDate
        {
            get
            {
                return _endSequentialDate;
            }
            set
            {
                _endSequentialDate = value;
            }
        }

        /// <summary>
        /// IncrementSize Property.
        /// </summary>
        public string IncrementSize
        {
            get
            {
                return _incrementSize;
            }
            set
            {
                _incrementSize = value;
            }
        }

        /// <summary>
        /// YearsIncrement Property.
        /// </summary>
        public bool YearsIncrement
        {
            get
            {
                return _yearsIncrement;
            }
            set
            {
                _yearsIncrement = value;
            }
        }

        /// <summary>
        /// MonthsIncrement Property.
        /// </summary>
        public bool MonthsIncrement
        {
            get
            {
                return _monthsIncrement;
            }
            set
            {
                _monthsIncrement = value;
            }
        }

        /// <summary>
        /// DaysIncrement Property.
        /// </summary>
        public bool DaysIncrement
        {
            get
            {
                return _daysIncrement;
            }
            set
            {
                _daysIncrement = value;
            }
        }

        /// <summary>
        /// MinNumDatesPerIncrement Property.
        /// </summary>
        public string MinNumDatesPerIncrement
        {
            get
            {
                return _minNumDatesPerIncrement;
            }
            set
            {
                _minNumDatesPerIncrement = value;
            }
        }

        /// <summary>
        /// MaxNumDatesPerIncrement Property.
        /// </summary>
        public string MaxNumDatesPerIncrement
        {
            get
            {
                return _maxNumDatesPerIncrement;
            }
            set
            {
                _maxNumDatesPerIncrement = value;
            }
        }

        /// <summary>
        /// InitStartSequentialDate Property.
        /// </summary>
        public string InitStartSequentialDate
        {
            get
            {
                return _initStartSequentialDate;
            }
            set
            {
                _initStartSequentialDate = value;
            }
        }


        /// <summary>
        /// NumRandomPreviewItems Property.
        /// </summary>
        public int NumRandomDataItems
        {
            get
            {
                return _numRandomDataItems;
            }
            set
            {
                _numRandomDataItems = value;
            }
        }

        /// <summary>
        /// ConvertGeneratedValueToInteger Property.
        /// </summary>
        public bool ConvertGeneratedValueToInteger
        {
            get
            {
                return _convertGeneratedValueToInteger;
            }
            set
            {
                _convertGeneratedValueToInteger = value;
            }
        }

        /// <summary>
        /// ConvertDateTo32BitInteger Property.
        /// </summary>
        public bool ConvertDateTo32BitInteger
        {
            get
            {
                return _convertDateTo32BitInteger;
            }
            set
            {
                _convertDateTo32BitInteger = value;
            }
        }

        /// <summary>
        /// ConvertTimeTo32BitInteger Property.
        /// </summary>
        public bool ConvertTimeTo32BitInteger
        {
            get
            {
                return _convertTimeTo32BitInteger;
            }
            set
            {
                _convertTimeTo32BitInteger = value;
            }
        }

        /// <summary>
        /// ConvertDateTimeTo64BitInteger Property.
        /// </summary>
        public bool ConvertDateTimeTo64BitInteger
        {
            get
            {
                return _convertDateTimeTo64BitInteger;
            }
            set
            {
                _convertDateTimeTo64BitInteger = value;
            }
        }


        //methods

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomDateTimeDataRequest));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of DateTimeDataRequest.</returns>
        public static RandomDateTimeDataRequest LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomDateTimeDataRequest));
            TextReader textReader = new StreamReader(filePath);
            RandomDateTimeDataRequest columnDefinitions;
            columnDefinitions = (RandomDateTimeDataRequest)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param personName="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of RandomDateTimeDataRequest.</returns>
        public static RandomDateTimeDataRequest LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomDateTimeDataRequest));
            StringReader strReader = new StringReader(xmlString);
            RandomDateTimeDataRequest objectInstance;
            objectInstance = (RandomDateTimeDataRequest)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
        }


        //class helpers

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns>String value.</returns>
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            data.Append(PropertiesToString());
            data.Append("\r\n");
            data.Append(FieldsToString());
            data.Append("\r\n");


            return data.ToString();
        }


        /// <summary>
        /// Routine outputs name and value for all properties.
        /// </summary>
        /// <returns>String value.</returns>
        public string PropertiesToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            PropertyInfo[] props = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            data.Append("Class type:");
            data.Append(t.FullName);
            data.Append("\r\nClass properties for");
            data.Append(t.FullName);
            data.Append("\r\n");


            int inx = 0;
            int maxInx = props.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                PropertyInfo prop = props[inx];
                object val = null;
                try
                {
                    val = prop.GetValue(this, null);
                }
                catch
                {
                    val = "Unable to retrieve value.";
                }

                /*
                //****************************************************************************************
                //use the following code if you class has an indexer or is derived from an indexed class
                //****************************************************************************************
                object val = null;
                StringBuilder temp = new StringBuilder();
                if (prop.GetIndexParameters().Length == 0)
                {
                    val = prop.GetValue(this, null);
                }
                else if (prop.GetIndexParameters().Length == 1)
                {
                    temp.Length = 0;
                    for (int i = 0; i < this.Count; i++)
                    {
                        temp.Append("Index ");
                        temp.Append(i.ToString());
                        temp.Append(" = ");
                        temp.Append(val = prop.GetValue(this, new object[] { i }));
                        temp.Append("  ");
                    }
                    val = temp.ToString();
                }
                else
                {
                    //this is an indexed property
                    temp.Length = 0;
                    temp.Append("Num indexes for property: ");
                    temp.Append(prop.GetIndexParameters().Length.ToString());
                    temp.Append("  ");
                    val = temp.ToString();
                }
                //****************************************************************************************
                // end code for indexed property
                //****************************************************************************************
                */

                if (prop.GetGetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetGetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetGetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetGetMethod(true).IsPublic && !prop.GetGetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetGetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" get ");
                    data.Append("> ");
                }
                if (prop.GetSetMethod(true) != null)
                {
                    data.Append(" <");
                    if (prop.GetSetMethod(true).IsPublic)
                        data.Append(" public ");
                    if (prop.GetSetMethod(true).IsPrivate)
                        data.Append(" private ");
                    if (!prop.GetSetMethod(true).IsPublic && !prop.GetSetMethod(true).IsPrivate)
                        data.Append(" internal ");
                    if (prop.GetSetMethod(true).IsStatic)
                        data.Append(" static ");
                    data.Append(" set ");
                    data.Append("> ");
                }
                data.Append(" ");
                data.Append(prop.PropertyType.FullName);
                data.Append(" ");

                data.Append(prop.Name);
                data.Append(": ");
                if (val != null)
                    data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (prop.PropertyType.IsArray)
                {
                    System.Collections.IList valueList = (System.Collections.IList)prop.GetValue(this, null);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        /// <summary>
        /// Routine outputs name and value for all fields.
        /// </summary>
        /// <returns>String value.</returns>
        public string FieldsToString()
        {
            StringBuilder data = new StringBuilder();
            Type t = this.GetType();
            FieldInfo[] finfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            bool typeHasFieldsToStringMethod = false;

            data.Append("\r\nClass fields for ");
            data.Append(t.FullName);
            data.Append("\r\n");

            int inx = 0;
            int maxInx = finfos.Length - 1;

            for (inx = 0; inx <= maxInx; inx++)
            {
                FieldInfo fld = finfos[inx];
                object val = fld.GetValue(this);
                if (fld.IsPublic)
                    data.Append(" public ");
                if (fld.IsPrivate)
                    data.Append(" private ");
                if (!fld.IsPublic && !fld.IsPrivate)
                    data.Append(" internal ");
                if (fld.IsStatic)
                    data.Append(" static ");
                data.Append(" ");
                data.Append(fld.FieldType.FullName);
                data.Append(" ");
                data.Append(fld.Name);
                data.Append(": ");
                typeHasFieldsToStringMethod = UseFieldsToString(fld.FieldType);
                if (val != null)
                    if (typeHasFieldsToStringMethod)
                        data.Append(GetFieldValues(val));
                    else
                        data.Append(val.ToString());
                else
                    data.Append("<null value>");
                data.Append("  ");

                if (fld.FieldType.IsArray)
                //if (fld.Name == "TestStringArray" || "_testStringArray")
                {
                    System.Collections.IList valueList = (System.Collections.IList)fld.GetValue(this);
                    for (int i = 0; i < valueList.Count; i++)
                    {
                        data.Append("Index ");
                        data.Append(i.ToString("#,##0"));
                        data.Append(": ");
                        data.Append(valueList[i].ToString());
                        data.Append("  ");
                    }
                }

                data.Append("\r\n");

            }

            return data.ToString();
        }

        private bool UseFieldsToString(Type pType)
        {
            bool retval = false;

            //avoid have this type calling its own FieldsToString and going into an infinite loop
            if (pType.FullName != this.GetType().FullName)
            {
                MethodInfo[] methods = pType.GetMethods();
                foreach (MethodInfo method in methods)
                {
                    if (method.Name == "FieldsToString")
                    {
                        retval = true;
                        break;
                    }
                }
            }

            return retval;
        }

        private string GetFieldValues(object typeInstance)
        {
            Type typ = typeInstance.GetType();
            MethodInfo methodInfo = typ.GetMethod("FieldsToString");
            Object retval = methodInfo.Invoke(typeInstance, null);


            return (string)retval;
        }


        /// <summary>
        /// Returns a string containing the contents of the object in XML format.
        /// </summary>
        /// <returns>String value in xml format.</returns>
        /// ///<remarks>XML Serialization is used for the transformation.</remarks>
        public string ToXmlString()
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomDateTimeDataRequest));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Converts instance of this class into an XML document.
        /// </summary>
        /// <returns>XmlDocument</returns>
        /// ///<remarks>XML Serialization and XmlDocument class are used for the transformation.</remarks>
        public XmlDocument ToXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(this.ToXmlString());
            return doc;
        }


    }//end class
}//end namespace
