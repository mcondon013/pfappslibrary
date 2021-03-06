﻿//****************************************************************************************************
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
using AppGlobals;
using PFCollectionsObjects;
using PFRandomDataProcessor;
using PFDataAccessObjects;
using PFAppDataObjects;

namespace pfDataExtractorCPObjects
{
    /// <summary>
    /// Initial class prototype for ProFast application or library code that includes ToString override, XML Serialization and output to XML document or string.
    /// </summary>
    public class PFDelimitedTextFileDestination
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private varialbles for properties

        private string _textFilePath = string.Empty;
        private bool _overwriteFileIfExists = true;
        private bool _columnsCommaDelimited = true;
        private bool _columnsTabDelimited = false;
        private bool _columnsHaveOtherDelimiter = false;
        private string _otherSeparator = string.Empty;
        private bool _useCrLfLineTerminator = true;
        private bool _useOtherLineTerminator = false;
        private string _otherLineTerminator = string.Empty;
        private bool _columnNamesOnFirstLine = false;
        private bool _stringValuesSurroundedWithQuotationMarks = false;

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PFDelimitedTextFileDestination()
        {
            ;
        }

        //properties

        /// <summary>
        /// TextFilePath Property.
        /// </summary>
        public string TextFilePath
        {
            get
            {
                return _textFilePath;
            }
            set
            {
                _textFilePath = value;
            }
        }

        /// <summary>
        /// OverwriteFileIfExists Property.
        /// </summary>
        public bool OverwriteFileIfExists
        {
            get
            {
                return _overwriteFileIfExists;
            }
            set
            {
                _overwriteFileIfExists = value;
            }
        }

        /// <summary>
        /// ColumnsCommaDelimited Property.
        /// </summary>
        public bool ColumnsCommaDelimited
        {
            get
            {
                return _columnsCommaDelimited;
            }
            set
            {
                _columnsCommaDelimited = value;
            }
        }

        /// <summary>
        /// ColumnsTabDelimited Property.
        /// </summary>
        public bool ColumnsTabDelimited
        {
            get
            {
                return _columnsTabDelimited;
            }
            set
            {
                _columnsTabDelimited = value;
            }
        }

        /// <summary>
        /// ColumnsHaveOtherDelimiter Property.
        /// </summary>
        public bool ColumnsHaveOtherDelimiter
        {
            get
            {
                return _columnsHaveOtherDelimiter;
            }
            set
            {
                _columnsHaveOtherDelimiter = value;
            }
        }

        /// <summary>
        /// OtherSeparator Property.
        /// </summary>
        public string OtherSeparator
        {
            get
            {
                return _otherSeparator;
            }
            set
            {
                _otherSeparator = value;
            }
        }

        /// <summary>
        /// UseCrLfLineTerminator Property.
        /// </summary>
        public bool UseCrLfLineTerminator
        {
            get
            {
                return _useCrLfLineTerminator;
            }
            set
            {
                _useCrLfLineTerminator = value;
            }
        }

        /// <summary>
        /// UseOtherLineTerminator Property.
        /// </summary>
        public bool UseOtherLineTerminator
        {
            get
            {
                return _useOtherLineTerminator;
            }
            set
            {
                _useOtherLineTerminator = value;
            }
        }

        /// <summary>
        /// OtherLineTerminator Property.
        /// </summary>
        public string OtherLineTerminator
        {
            get
            {
                return _otherLineTerminator;
            }
            set
            {
                _otherLineTerminator = value;
            }
        }

        /// <summary>
        /// ColumnNamesOnFirstLine Property.
        /// </summary>
        public bool ColumnNamesOnFirstLine
        {
            get
            {
                return _columnNamesOnFirstLine;
            }
            set
            {
                _columnNamesOnFirstLine = value;
            }
        }

        /// <summary>
        /// StringValuesSurroundedWithQuotationMarks Property.
        /// </summary>
        public bool StringValuesSurroundedWithQuotationMarks
        {
            get
            {
                return _stringValuesSurroundedWithQuotationMarks;
            }
            set
            {
                _stringValuesSurroundedWithQuotationMarks = value;
            }
        }





        //methods

        /// <summary>
        /// Retrieves the character(s) being used to delimit a column in a text file data destination.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetColumnDelimiter()
        {
            string colDelimiter = ",";

            if (this.ColumnsCommaDelimited)
                colDelimiter = ",";
            else if (this.ColumnsTabDelimited)
                colDelimiter = "\t";
            else if (this.ColumnsHaveOtherDelimiter)
                colDelimiter = this.OtherSeparator;
            else
                colDelimiter = ",";

            return colDelimiter;
        }

        /// <summary>
        /// Retrieves character(s) being used as line terminator.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetLineTerminator()
        {
            string lineTerminator = Environment.NewLine;

            if (this.UseCrLfLineTerminator)
                lineTerminator = Environment.NewLine;
            else if (this.UseOtherLineTerminator)
                lineTerminator = this.OtherLineTerminator;
            else
                lineTerminator = Environment.NewLine;

            return lineTerminator;
        }


        //class helpers

        /// <summary>
        /// Saves the column definitions contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(PFDelimitedTextFileDestination));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of PFDelimitedTextFileDestination.</returns>
        public static PFDelimitedTextFileDestination LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(PFDelimitedTextFileDestination));
            TextReader textReader = new StreamReader(filePath);
            PFDelimitedTextFileDestination columnDefinitions;
            columnDefinitions = (PFDelimitedTextFileDestination)deserializer.Deserialize(textReader);
            textReader.Close();
            return columnDefinitions;
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
            XmlSerializer ser = new XmlSerializer(typeof(PFDelimitedTextFileDestination));
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
