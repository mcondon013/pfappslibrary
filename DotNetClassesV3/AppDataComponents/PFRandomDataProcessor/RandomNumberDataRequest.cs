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
    /// Contains definition for creating a random number value.
    /// </summary>
    public class RandomNumberDataRequest
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties
        private string _name = string.Empty;
        private bool _outputIntegerValue = false;
        private bool _outputDoubleValue = false;
        private bool _outputFloatValue = false;
        private bool _outputDecimalValue = false;
        private bool _outputSignedInteger = false;
        private bool _outputUnsignedInteger = false;
        private bool _output64bitInteger = false;
        private bool _output32bitInteger = false;
        private bool _output16bitInteger = false;
        private bool _output8bitInteger = false;
        private bool _outputRangeOfNumbers = false;
        private string _minimumValueForRange = string.Empty;
        private string _maximumValueForRange = string.Empty;
        private bool _outputOffsetFromCurrentNumber = false;
        private string _minimumOffsetPercent = string.Empty;
        private string _maximumOffsetPercent = string.Empty;
        private string _currentNumberToOffset = string.Empty;
        private bool _outputSequentialNumbers = false;
        private string _startSequentialValue = "1";             //this number can be changed by processing code as sequential number lists are being generated in sets of rows
        private string _incrementForSequentialValue = "1";
        private string _maxSequentialValue = "";
        private string _initStartSequentialValue = "1";         //used to correctly restart a sequence after the maximum number has been passed
        private int _numRandomDataItems = 1000;

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomNumberDataRequest()
        {

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
        /// OutputIntegerValue Property.
        /// </summary>
        public bool OutputIntegerValue
        {
            get
            {
                return _outputIntegerValue;
            }
            set
            {
                _outputIntegerValue = value;
            }
        }

        /// <summary>
        /// OutputDoubleValue Property.
        /// </summary>
        public bool OutputDoubleValue
        {
            get
            {
                return _outputDoubleValue;
            }
            set
            {
                _outputDoubleValue = value;
            }
        }

        /// <summary>
        /// OutputFloatValue Property.
        /// </summary>
        public bool OutputFloatValue
        {
            get
            {
                return _outputFloatValue;
            }
            set
            {
                _outputFloatValue = value;
            }
        }

        /// <summary>
        /// OutputDecimalValue Property.
        /// </summary>
        public bool OutputDecimalValue
        {
            get
            {
                return _outputDecimalValue;
            }
            set
            {
                _outputDecimalValue = value;
            }
        }

        /// <summary>
        /// OutputSignedInteger Property.
        /// </summary>
        public bool OutputSignedInteger
        {
            get
            {
                return _outputSignedInteger;
            }
            set
            {
                _outputSignedInteger = value;
            }
        }

        /// <summary>
        /// OutputUnsignedInteger Property.
        /// </summary>
        public bool OutputUnsignedInteger
        {
            get
            {
                return _outputUnsignedInteger;
            }
            set
            {
                _outputUnsignedInteger = value;
            }
        }

        /// <summary>
        /// Output64bitInteger Property.
        /// </summary>
        public bool Output64bitInteger
        {
            get
            {
                return _output64bitInteger;
            }
            set
            {
                _output64bitInteger = value;
            }
        }

        /// <summary>
        /// Output32bitInteger Property.
        /// </summary>
        public bool Output32bitInteger
        {
            get
            {
                return _output32bitInteger;
            }
            set
            {
                _output32bitInteger = value;
            }
        }

        /// <summary>
        /// Output16bitInteger Property.
        /// </summary>
        public bool Output16bitInteger
        {
            get
            {
                return _output16bitInteger;
            }
            set
            {
                _output16bitInteger = value;
            }
        }

        /// <summary>
        /// Output8bitInteger Property.
        /// </summary>
        public bool Output8bitInteger
        {
            get
            {
                return _output8bitInteger;
            }
            set
            {
                _output8bitInteger = value;
            }
        }

        /// <summary>
        /// OutputRangeOfNumbers Property.
        /// </summary>
        public bool OutputRangeOfNumbers
        {
            get
            {
                return _outputRangeOfNumbers;
            }
            set
            {
                _outputRangeOfNumbers = value;
            }
        }

        /// <summary>
        /// MinimumValueForRange Property.
        /// </summary>
        public string MinimumValueForRange
        {
            get
            {
                return _minimumValueForRange;
            }
            set
            {
                _minimumValueForRange = value;
            }
        }

        /// <summary>
        /// MaximumValueForRange Property.
        /// </summary>
        public string MaximumValueForRange
        {
            get
            {
                return _maximumValueForRange;
            }
            set
            {
                _maximumValueForRange = value;
            }
        }

        /// <summary>
        /// OutputOffsetFromCurrentNumber Property.
        /// </summary>
        public bool OutputOffsetFromCurrentNumber
        {
            get
            {
                return _outputOffsetFromCurrentNumber;
            }
            set
            {
                _outputOffsetFromCurrentNumber = value;
            }
        }

        /// <summary>
        /// MinimumOffsetPercent Property.
        /// </summary>
        public string MinimumOffsetPercent
        {
            get
            {
                return _minimumOffsetPercent;
            }
            set
            {
                _minimumOffsetPercent = value;
            }
        }

        /// <summary>
        /// MaximumOffsetPercent Property.
        /// </summary>
        public string MaximumOffsetPercent
        {
            get
            {
                return _maximumOffsetPercent;
            }
            set
            {
                _maximumOffsetPercent = value;
            }
        }

        /// <summary>
        /// CurrentNumberToOffset property.
        /// </summary>
        /// <remarks>Set to empty string or null to generate a preview list. If set to a value, a generate request will return only one offset using this property as the base to offset.</remarks>
        public string CurrentNumberToOffset
        {
            get
            {
                return _currentNumberToOffset;
            }
            set
            {
                _currentNumberToOffset = value;
            }
        }

        /// <summary>
        /// OutputSequentialNumbers Property.
        /// </summary>
        public bool OutputSequentialNumbers
        {
            get
            {
                return _outputSequentialNumbers;
            }
            set
            {
                _outputSequentialNumbers = value;
            }
        }

        /// <summary>
        /// StartSequentialValue Property.
        /// </summary>
        public string StartSequentialValue
        {
            get
            {
                return _startSequentialValue;
            }
            set
            {
                _startSequentialValue = value;
            }
        }

        /// <summary>
        /// IncrementForSequentialValue Property.
        /// </summary>
        public string IncrementForSequentialValue
        {
            get
            {
                return _incrementForSequentialValue;
            }
            set
            {
                _incrementForSequentialValue = value;
            }
        }

        /// <summary>
        /// MaxSequentialValue Property.
        /// </summary>
        public string MaxSequentialValue
        {
            get
            {
                return _maxSequentialValue;
            }
            set
            {
                _maxSequentialValue = value;
            }
        }

        /// <summary>
        /// InitStartSequentialValue Property.
        /// </summary>
        public string InitStartSequentialValue
        {
            get
            {
                return _initStartSequentialValue;
            }
            set
            {
                _initStartSequentialValue = value;
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



        //methods

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomNumberDataRequest));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of NumericRandomDataRequest.</returns>
        public static RandomNumberDataRequest LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomNumberDataRequest));
            TextReader textReader = new StreamReader(filePath);
            RandomNumberDataRequest columnDefinitions;
            columnDefinitions = (RandomNumberDataRequest)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param personName="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of RandomNumberDataRequest.</returns>
        public static RandomNumberDataRequest LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomNumberDataRequest));
            StringReader strReader = new StringReader(xmlString);
            RandomNumberDataRequest objectInstance;
            objectInstance = (RandomNumberDataRequest)deserializer.Deserialize(strReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(RandomNumberDataRequest));
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
