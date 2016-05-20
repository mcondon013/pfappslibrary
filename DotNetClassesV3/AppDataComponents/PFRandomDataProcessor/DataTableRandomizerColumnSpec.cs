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
    /// Class that contains the specification for randomizing a data column in a data table.
    /// </summary>
    public class DataTableRandomizerColumnSpec
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private string _dataTableColumnName = string.Empty;     //ColumnName in the query data table
        private string _dataTableColumnDataType = string.Empty; //Data type for the column in the query data table
        private int _dataTableColumnIndex = -1;                 //index of data column in dataTable
        private string _randomDataFileName = string.Empty;      //Name of file containing custom data values. (No longer used. Was used in initial testing. File name now stored in _randomDataSource.)
        private enRandomDataType _randomDataType = enRandomDataType.NotSpecified;
        private int _randomNamesAndLocationsNumber = -1;        //used to distinguish between different RandomNamesAndLocations specs
        private string _randomDataSource = string.Empty;        //name of file containing definition or custom data values
        private string _randomDataFieldName = string.Empty;     //only used if _randomDataType equals enRandomDataType.RandomNamesAndLocationsFile
        private int _randomDataFieldColumnIndex = -1;           //index of the column in the generated random data table for the _randomDataFieldName
        private int _randomDataTypeProcessorIndex = -1;         //used to simplify launching the randomizer processing for a column spec
        private int _randomDataListIndex = -1;                  //used at runtime to keep track of reading of random data table rows
        private int _currentValueIndex = -1;                    //current row to read in random data table rows collection (managed at runtime)
        private bool _isOffsetRandomValue = false;              //identifies whether or not a random number or a random date/time is to be an offset from the current data table value at runtime.
        private bool _isByteArray = false;                      //Identifies whether or not a random value is a byte array. Special processing for these needed in randomizer routines.
        private bool _isCharArray = false;                      //Identifies whether or not a random value is a char array. Special processing for these needed in randomizer routines.
        private bool _isSequentialNumber = false;               //Idenfifies whether or not random number option for generating sequential numbers has been chosen; Code to keep track of latest sequential number uses this flag.
        private bool _isSequentialDate = false;                 //Idenfifies whether or not random dates option for generating sequential dates has been chosen; Code to keep track of latest sequential date uses this flag.
        private bool _convertRandomDateTimeToInteger=false;     //Identifies whether or not random dates option for generating integer values for dates has been chosen. Used by code that processes the generated random date/time value.
        private bool _convertRandomDateToInt32 = false;         //Identifies whether or not random dates option for generating 32-bit integer values for dates has been chosen. Used by code that translates dates separately as integers..
        private bool _convertRandomTimeToInt32 = false;         //Identifies whether or not random dates option for generating 32-bit integer values for dates has been chosen. Used by code that translates times separately as integers..
        private bool _convertRandomDateTimeToInt64 = false;     //Identifies whether or not random dates option for generating 64-bit integer values for dates has been chosen. Used by code that translates full datetime values as integers..
        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataTableRandomizerColumnSpec()
        {
            ;
        }

        //properties

        /// <summary>
        /// DataTableColumnName Property.
        /// </summary>
        public string DataTableColumnName
        {
            get
            {
                return _dataTableColumnName;
            }
            set
            {
                _dataTableColumnName = value;
            }
        }

        /// <summary>
        /// DataType for the DataTable column.
        /// </summary>
        public string DataTableColumnDataType
        {
            get
            {
                return _dataTableColumnDataType;
            }
            set
            {
                _dataTableColumnDataType = value;
            }
        }

        /// <summary>
        /// DataTableColumnIndex Property.
        /// </summary>
        public int DataTableColumnIndex
        {
            get
            {
                return _dataTableColumnIndex;
            }
            set
            {
                _dataTableColumnIndex = value;
            }
        }

        /// <summary>
        /// RandomDataFileName Property.
        /// </summary>
        public string RandomDataFileName
        {
            get
            {
                return _randomDataFileName;
            }
            set
            {
                _randomDataFileName = value;
            }
        }

        /// <summary>
        /// RandomDataType Property.
        /// </summary>
        public enRandomDataType RandomDataType
        {
            get
            {
                return _randomDataType;
            }
            set
            {
                _randomDataType = value;
            }
        }

        /// <summary>
        /// RandomNamesAndLocationsNumber property is used to distiguish between the different RandomNamesAndLocations options (RandomNamesAndLocations, RandomNamesAndLocations_2 through RandomNamesAndLocations_5.
        /// </summary>
        public int RandomNamesAndLocationsNumber
        {
            get
            {
                return _randomNamesAndLocationsNumber;
            }
            set
            {
                _randomNamesAndLocationsNumber = value;
            }
        }

        /// <summary>
        /// RandomDataSource Property.
        /// </summary>
        public string RandomDataSource
        {
            get
            {
                return _randomDataSource;
            }
            set
            {
                _randomDataSource = value;
            }
        }

        /// <summary>
        /// RandomDataFieldName Property.
        /// </summary>
        public string RandomDataFieldName
        {
            get
            {
                return _randomDataFieldName;
            }
            set
            {
                _randomDataFieldName = value;
            }
        }

        /// <summary>
        /// RandomDataFieldColumnIndex Property.
        /// </summary>
        public int RandomDataFieldColumnIndex
        {
            get
            {
                return _randomDataFieldColumnIndex;
            }
            set
            {
                _randomDataFieldColumnIndex = value;
            }
        }

        /// <summary>
        /// Index into the list of random type processors servicing the current instance when generating random values.
        /// </summary>
        public int RandomDataTypeProcessorIndex
        {
            get
            {
                return _randomDataTypeProcessorIndex;
            }
            set
            {
                _randomDataTypeProcessorIndex = value;
            }
        }

        /// <summary>
        /// RandomDataListIndex Property.
        /// </summary>
        public int RandomDataListIndex
        {
            get
            {
                return _randomDataListIndex;
            }
            set
            {
                _randomDataListIndex = value;
            }
        }

        /// <summary>
        /// CurrentValueIndex property.
        /// </summary>
        public int CurrentValueIndex
        {
            get
            {
                return _currentValueIndex;
            }
            set
            {
                _currentValueIndex = value;
            }
        }

        /// <summary>
        /// IsOffsetRandomValue Property. Set to true for offset random numbers or offset random date/times.
        /// </summary>
        /// <remarks>Applies only to random numbers or random date/times.</remarks>
        public bool IsOffsetRandomValue
        {
            get
            {
                return _isOffsetRandomValue;
            }
            set
            {
                _isOffsetRandomValue = value;
            }
        }

        /// <summary>
        /// IsByteArray Property.
        /// </summary>
        public bool IsByteArray
        {
            get
            {
                return _isByteArray;
            }
            set
            {
                _isByteArray = value;
            }
        }

        /// <summary>
        /// IsCharArray Property.
        /// </summary>
        public bool IsCharArray
        {
            get
            {
                return _isCharArray;
            }
            set
            {
                _isCharArray = value;
            }
        }

        /// <summary>
        /// IsSequentialNumber Property.
        /// </summary>
        public bool IsSequentialNumber
        {
            get
            {
                return _isSequentialNumber;
            }
            set
            {
                _isSequentialNumber = value;
            }
        }

        /// <summary>
        /// IsSequentialDate Property.
        /// </summary>
        public bool IsSequentialDate
        {
            get
            {
                return _isSequentialDate;
            }
            set
            {
                _isSequentialDate = value;
            }
        }

        /// <summary>
        /// ConvertRandomDateTimeToInteger Property.
        /// </summary>
        public bool ConvertRandomDateTimeToInteger
        {
            get
            {
                return _convertRandomDateTimeToInteger;
            }
            set
            {
                _convertRandomDateTimeToInteger = value;
            }
        }

        /// <summary>
        /// ConvertRandomDateToInt32 Property.
        /// </summary>
        public bool ConvertRandomDateToInt32
        {
            get
            {
                return _convertRandomDateToInt32;
            }
            set
            {
                _convertRandomDateToInt32 = value;
            }
        }

        /// <summary>
        /// ConvertRandomTimeToInt32 Property.
        /// </summary>
        public bool ConvertRandomTimeToInt32
        {
            get
            {
                return _convertRandomTimeToInt32;
            }
            set
            {
                _convertRandomTimeToInt32 = value;
            }
        }

        /// <summary>
        /// ConvertRandomDateTimeToInt64 Property.
        /// </summary>
        public bool ConvertRandomDateTimeToInt64
        {
            get
            {
                return _convertRandomDateTimeToInt64;
            }
            set
            {
                _convertRandomDateTimeToInt64 = value;
            }
        }


        //methods

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataTableRandomizerColumnSpec));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFInitClassExtended.</returns>
        public static DataTableRandomizerColumnSpec LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DataTableRandomizerColumnSpec));
            TextReader textReader = new StreamReader(filePath);
            DataTableRandomizerColumnSpec columnDefinitions;
            columnDefinitions = (DataTableRandomizerColumnSpec)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
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
            XmlSerializer ser = new XmlSerializer(typeof(DataTableRandomizerColumnSpec));
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
