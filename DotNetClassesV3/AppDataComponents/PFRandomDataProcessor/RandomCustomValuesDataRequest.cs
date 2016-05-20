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
using System.Data;
using PFDataAccessObjects;

namespace PFRandomDataProcessor
{
    /// <summary>
    /// Class containing parameters to use when generating a custom random data list.
    /// </summary>
    public class RandomCustomValuesDataRequest
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties
        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;
        private string _dbConnectionString = string.Empty;
        private string _tableIncludeSearchPattern = string.Empty;
        private string _tableExcludeSearchPattern = string.Empty;
        private string _dbTableName = string.Empty;
        private string _dbFieldName = string.Empty;
        private string _dbFieldType = "System.String";
        private string _selectionField = string.Empty;
        private string _selectionFieldType = "System.String";
        private string _selectionCondition = string.Empty;
        private string _selectionCriteria = string.Empty;
        private int _minimumValueFrequency = 1;
        private string _listName = string.Empty;
        private string _listFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\PFApps\Randomizer\Data\Custom";
        private bool _outputToXmlFile = true;
        private bool _outputToGrid = true;
        private int _maxOutputRows = 3000;


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public RandomCustomValuesDataRequest()
        {
            ;
        }



        //properties

        /// <summary>
        /// Database platform (e.g. MSSQLServer, OracleNative etc.
        /// </summary>
        public DatabasePlatform DbPlatform
        {
            get
            {
                return _dbPlatform;
            }
            set
            {
                _dbPlatform = value;
            }
        }

        /// <summary>
        /// Connection string to be used to open the database.
        /// </summary>
        public string DbConnectionString
        {
            get
            {
                return _dbConnectionString;
            }
            set
            {
                _dbConnectionString = value;
            }
        }

        /// <summary>
        /// TableIncludeSearchPattern Property.
        /// </summary>
        public string TableIncludeSearchPattern
        {
            get
            {
                return _tableIncludeSearchPattern;
            }
            set
            {
                _tableIncludeSearchPattern = value;
            }
        }

        /// <summary>
        /// TableExcludeSearchPattern Property.
        /// </summary>
        public string TableExcludeSearchPattern
        {
            get
            {
                return _tableExcludeSearchPattern;
            }
            set
            {
                _tableExcludeSearchPattern = value;
            }
        }


        /// <summary>
        /// Name of table containing data to be used for the random value list.
        /// </summary>
        public string DbTableName
        {
            get
            {
                return _dbTableName;
            }
            set
            {
                _dbTableName = value;
            }
        }

        /// <summary>
        /// Name of field for which the frequency of values will be calculated.
        /// </summary>
        public string DbFieldName
        {
            get
            {
                return _dbFieldName;
            }
            set
            {
                _dbFieldName = value;
            }
        }

        /// <summary>
        /// Data type for the field represented by DBFieldName.
        /// </summary>
        public string DbFieldType
        {
            get
            {
                return _dbFieldType;
            }
            set
            {
                _dbFieldType = value;
            }
        }

        /// <summary>
        /// Name of field that will be used for selection criteria.
        /// </summary>
        public string SelectionField
        {
            get
            {
                return _selectionField;
            }
            set
            {
                _selectionField = value;
            }
        }

        /// <summary>
        /// Data type for the field represented by SelectionField.
        /// </summary>
        public string SelectionFieldType
        {
            get
            {
                return _selectionFieldType;
            }
            set
            {
                _selectionFieldType = value;
            }
        }

        /// <summary>
        /// Selection condtion that will be applied to the criteria. (e.g. Equal To, Greater Than, etc.)
        /// </summary>
        public string SelectionCondition
        {
            get
            {
                return _selectionCondition;
            }
            set
            {
                _selectionCondition = value;
            }
        }

        /// <summary>
        /// Criteria that will be used for determining which values to include in the list.
        /// </summary>
        public string SelectionCriteria
        {
            get
            {
                return _selectionCriteria;
            }
            set
            {
                _selectionCriteria = value;
            }
        }

        /// <summary>
        /// Minimum number of times a value must occur for it to appear in the final random data list.
        /// </summary>
        /// <remarks>Default is 1.</remarks>
        public int MinimumValueFrequency
        {
            get
            {
                return _minimumValueFrequency;
            }
            set
            {
                _minimumValueFrequency = value;
            }
        }

        /// <summary>
        /// Name of the custom random data list.
        /// </summary>
        public string ListName
        {
            get
            {
                return _listName;
            }
            set
            {
                _listName = value;
            }
        }

        /// <summary>
        /// Folder that will contain the generated custom random data list.
        /// </summary>
        public string ListFolder
        {
            get
            {
                return _listFolder;
            }
            set
            {
                _listFolder = value;
            }
        }

        /// <summary>
        /// OutputToXmlFile Property.
        /// </summary>
        public bool OutputToXmlFile
        {
            get
            {
                return _outputToXmlFile;
            }
            set
            {
                _outputToXmlFile = value;
            }
        }

        /// <summary>
        /// OutputToGrid Property.
        /// </summary>
        public bool OutputToGrid
        {
            get
            {
                return _outputToGrid;
            }
            set
            {
                _outputToGrid = value;
            }
        }

        /// <summary>
        /// MaxOutputRows Property.
        /// </summary>
        public int MaxOutputRows
        {
            get
            {
                return _maxOutputRows;
            }
            set
            {
                _maxOutputRows = value;
            }
        }





        //methods

        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param personName="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(RandomCustomValuesDataRequest));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param personName="filePath">Full path for the input file.</param>
        /// <returns>An instance of CustomDataRequest.</returns>
        public static RandomCustomValuesDataRequest LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomCustomValuesDataRequest));
            TextReader textReader = new StreamReader(filePath);
            RandomCustomValuesDataRequest objectInstance;
            objectInstance = (RandomCustomValuesDataRequest)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
        }


        /// <summary>
        /// Routine overrides default ToString method and outputs personName, type, scope and value for all class properties and fields.
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
        /// Routine outputs personName and value for all properties.
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
                object val = prop.GetValue(this, null);

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
        /// Routine outputs personName and value for all fields.
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
            XmlSerializer ser = new XmlSerializer(typeof(RandomCustomValuesDataRequest));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param personName="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of CustomDataRequest.</returns>
        public static RandomCustomValuesDataRequest LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomCustomValuesDataRequest));
            StringReader strReader = new StringReader(xmlString);
            RandomCustomValuesDataRequest objectInstance;
            objectInstance = (RandomCustomValuesDataRequest)deserializer.Deserialize(strReader);
            strReader.Close();
            return objectInstance;
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
