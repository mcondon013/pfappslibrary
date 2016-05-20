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

namespace InitClassLibrary
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override, XML Serialization and output to XML document or string.
    /// </summary>
    public class PFInitClassExtended
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        private TestClass1 tst = new TestClass1();

        private string _teststring1 = "this is a test string";
        private long _testlong1 = -55555;
        internal double _testfloat = -45678.4397;
        private string _teststring2 = "Yet another string by bing.";
        public int _publicInt = 666;

        //private variables for properties
        internal bool _testBool = false;
        private string _testString = "Testing ...";
        private int _testInt = -55;
        private long _testLOng = 12555777999;
        private double _testDouble = -34578.654321;
        private decimal _testDecimal = (decimal)12345.7654321;
        private string[] _testStringArray = { "First value", "Second value", "Third value", "Fourth value", "Fifth value" };

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFInitClassExtended()
        {
            _teststring1 = "What a day for a daydream. What a load of ...";
            _testlong1 = -666666664;
            _teststring2 = "a modified version of the value of this object known as a string";
        }

        //properties

        private long Testlong1
        {
            get
            {
                return _testlong1;
            }
            set
            {
                _testlong1 = value;
            }
        }

        internal string Teststring1
        {
            get
            {
                return _teststring1;
            }
            set
            {
                _teststring1 = value;
            }
        }



        /// <summary>
        /// Protype property.
        /// </summary>
        public bool TestBool
        {
            get
            {
                return _testBool;
            }
        }

        /// <summary>
        /// Protype property.
        /// </summary>
        public string TestString
        {
            get
            {
                return _testString;
            }
            set
            {
                _testString = value;
            }
        }

        /// <summary>
        /// Protype property.
        /// </summary>
        public int TestInt
        {
            get
            {
                return _testInt;
            }
            set
            {
                _testInt = value;
            }
        }

        /// <summary>
        /// Protype property.
        /// </summary>
        public long TestLOng
        {
            get
            {
                return _testLOng;
            }
            set
            {
                _testLOng = value;
            }
        }

        /// <summary>
        /// Protype property.
        /// </summary>
        public double TestDouble
        {
            get
            {
                return _testDouble;
            }
            set
            {
                _testDouble = value;
            }
        }

        /// <summary>
        /// Protype property.
        /// </summary>
        public decimal TestDecimal
        {
            get
            {
                return _testDecimal;
            }
            set
            {
                _testDecimal = value;
            }
        }

        /// <summary>
        /// Protype property.
        /// </summary>
        public string[] TestStringArray
        {
            get
            {
                return _testStringArray;
            }
            set
            {
                _testStringArray = value;
            }
        }


        public TestClass1 Tst
        {
            get
            {
                return tst;
            }
            set
            {
                tst = value;
            }
        }

        //methods

        //class helpers

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFInitClassExtended));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFInitClassExtended.</returns>
        public static PFInitClassExtended LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFInitClassExtended));
            TextReader textReader = new StreamReader(filePath);
            PFInitClassExtended columnDefinitions;
            columnDefinitions = (PFInitClassExtended)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }


        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from an XML formatted string.
        /// </summary>
        /// <param name="xmlString">String containing formatted XML version of the object.</param>
        /// <returns>An instance of PFExtractorOutputOptions.</returns>
        public static PFInitClassExtended LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFInitClassExtended));
            StringReader strReader = new StringReader(xmlString);
            PFInitClassExtended fieldDefinitions = (PFInitClassExtended)deserializer.Deserialize(strReader);
            strReader.Close();
            return fieldDefinitions;
        }

        /// <summary>
        /// Routine overrides default ToString method and outputs name, type, scope and value for all class properties and fields.
        /// </summary>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
            XmlSerializer ser = new XmlSerializer(typeof(PFInitClassExtended));
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

        /// <summary>
        /// Makes a deep copy of the current instance to a new instance.
        /// </summary>
        /// <returns>New instance of PFExtractorOutputOptions object.</returns>
        public PFInitClassExtended Copy()
        {
            string xmlString = this.ToXmlString();
            PFInitClassExtended newInstance = PFInitClassExtended.LoadFromXmlString(xmlString);
            return newInstance;
        }


    }//end class
}//end namespace
