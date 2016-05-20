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
using PFCollectionsObjects;
using PFRandomDataProcessor;
using PFDataAccessObjects;
using PFAppDataObjects;

namespace pfDataExtractorCPObjects
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override, XML Serialization and output to XML document or string.
    /// </summary>
    public class PFExtractorDefinition
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private varialbles for properties
        private string _extractorName = string.Empty;
        private enExtractorDataLocation _extractorSource = enExtractorDataLocation.Unknown;
        private enExtractorDataLocation _extractorDestination = enExtractorDataLocation.Unknown;

        
        private PFRelationalDatabaseSource _relationalDatabaseSource = new PFRelationalDatabaseSource();
        private PFMsAccessSource _msAccessSource = new PFMsAccessSource();
        private PFMsExcelSource _msExcelSource = new PFMsExcelSource();
        private PFDelimitedTextFileSource _delimitedTextFileSource = new PFDelimitedTextFileSource();
        private PFFixedLengthTextFileSource _fixedLengthTextFileSource = new PFFixedLengthTextFileSource();
        private PFXmlFileSource _xmlFileSource = new PFXmlFileSource();

        private PFRelationalDatabaseDestination _relationalDatabaseDestination = new PFRelationalDatabaseDestination();
        private PFMsAccessDestination _msAccessDestination = new PFMsAccessDestination();
        private PFMsExcelDestination _msExcelDestination = new PFMsExcelDestination();
        private PFDelimitedTextFileDestination _delimitedTextFileDestination = new PFDelimitedTextFileDestination();
        private PFFixedLengthTextFileDestination _fixedLengthTextFileDestination = new PFFixedLengthTextFileDestination();
        private PFXmlFileDestination _xmlFileDestination = new PFXmlFileDestination();

        private PFRandomOrdersDefinition _randomOrdersDefinition = new PFRandomOrdersDefinition();

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFExtractorDefinition()
        {
            ;
        }

        //properties

        /// <summary>
        /// ExtractorName Property.
        /// </summary>
        public string ExtractorName
        {
            get
            {
                return _extractorName;
            }
            set
            {
                _extractorName = value;
            }
        }

        /// <summary>
        /// ExtractorSource Property.
        /// </summary>
        public enExtractorDataLocation ExtractorSource
        {
            get
            {
                return _extractorSource;
            }
            set
            {
                _extractorSource = value;
            }
        }

        /// <summary>
        /// ExtractorDestination Property.
        /// </summary>
        public enExtractorDataLocation ExtractorDestination
        {
            get
            {
                return _extractorDestination;
            }
            set
            {
                _extractorDestination = value;
            }
        }


        /// <summary>
        /// RelationalDatabaseSource Property.
        /// </summary>
        public PFRelationalDatabaseSource RelationalDatabaseSource
        {
            get
            {
                return _relationalDatabaseSource;
            }
            set
            {
                _relationalDatabaseSource = value;
            }
        }

        /// <summary>
        /// MsAccessSource Property.
        /// </summary>
        public PFMsAccessSource MsAccessSource
        {
            get
            {
                return _msAccessSource;
            }
            set
            {
                _msAccessSource = value;
            }
        }

        /// <summary>
        /// MsExcelSource Property.
        /// </summary>
        public PFMsExcelSource MsExcelSource
        {
            get
            {
                return _msExcelSource;
            }
            set
            {
                _msExcelSource = value;
            }
        }

        /// <summary>
        /// DelimitedTextFileSource Property.
        /// </summary>
        public PFDelimitedTextFileSource DelimitedTextFileSource
        {
            get
            {
                return _delimitedTextFileSource;
            }
            set
            {
                _delimitedTextFileSource = value;
            }
        }

        /// <summary>
        /// FixedLengthTextFileSource Property.
        /// </summary>
        public PFFixedLengthTextFileSource FixedLengthTextFileSource
        {
            get
            {
                return _fixedLengthTextFileSource;
            }
            set
            {
                _fixedLengthTextFileSource = value;
            }
        }

        /// <summary>
        /// XmlFileSource Property.
        /// </summary>
        public PFXmlFileSource XmlFileSource
        {
            get
            {
                return _xmlFileSource;
            }
            set
            {
                _xmlFileSource = value;
            }
        }


        /// <summary>
        /// RelationalDatabaseDestination Property.
        /// </summary>
        public PFRelationalDatabaseDestination RelationalDatabaseDestination
        {
            get
            {
                return _relationalDatabaseDestination;
            }
            set
            {
                _relationalDatabaseDestination = value;
            }
        }

        /// <summary>
        /// MsAccessDestination Property.
        /// </summary>
        public PFMsAccessDestination MsAccessDestination
        {
            get
            {
                return _msAccessDestination;
            }
            set
            {
                _msAccessDestination = value;
            }
        }

        /// <summary>
        /// MsExcelDestination Property.
        /// </summary>
        public PFMsExcelDestination MsExcelDestination
        {
            get
            {
                return _msExcelDestination;
            }
            set
            {
                _msExcelDestination = value;
            }
        }

        /// <summary>
        /// DelimitedTextFileDestination Property.
        /// </summary>
        public PFDelimitedTextFileDestination DelimitedTextFileDestination
        {
            get
            {
                return _delimitedTextFileDestination;
            }
            set
            {
                _delimitedTextFileDestination = value;
            }
        }

        /// <summary>
        /// FixedLengthTextFileDestination Property.
        /// </summary>
        public PFFixedLengthTextFileDestination FixedLengthTextFileDestination
        {
            get
            {
                return _fixedLengthTextFileDestination;
            }
            set
            {
                _fixedLengthTextFileDestination = value;
            }
        }

        /// <summary>
        /// XmlFileDestination Property.
        /// </summary>
        public PFXmlFileDestination XmlFileDestination
        {
            get
            {
                return _xmlFileDestination;
            }
            set
            {
                _xmlFileDestination = value;
            }
        }

        /// <summary>
        /// RandomOrdersDefinition Property.
        /// </summary>
        public PFRandomOrdersDefinition RandomOrdersDefinition
        {
            get
            {
                return _randomOrdersDefinition;
            }
            set
            {
                _randomOrdersDefinition = value;
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
            XmlSerializer ser = new XmlSerializer(typeof(PFExtractorDefinition));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFExtractorDefinition.</returns>
        public static PFExtractorDefinition LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFExtractorDefinition));
            TextReader textReader = new StreamReader(filePath);
            PFExtractorDefinition columnDefinitions;
            columnDefinitions = (PFExtractorDefinition)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from an XML formatted string.
        /// </summary>
        /// <param name="xmlString">String containing formatted XML version of the object.</param>
        /// <returns>An instance of PFExtractorDefinition.</returns>
        public static PFExtractorDefinition LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFExtractorDefinition));
            StringReader strReader = new StringReader(xmlString);
            PFExtractorDefinition fieldDefinitions = (PFExtractorDefinition)deserializer.Deserialize(strReader);
            strReader.Close();
            return fieldDefinitions;
        }



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
            XmlSerializer ser = new XmlSerializer(typeof(PFExtractorDefinition));
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
