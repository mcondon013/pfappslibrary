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
    /// Contains definition for creating random words.
    /// </summary>
    public class RandomWordsDataRequest
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();


        //private variables for properties
        private string _name = string.Empty;
        private bool _outputRandomWords = false;
        private string _minNumWords = string.Empty;
        private string _maxNumWords = string.Empty;
        private bool _outputWordUCLC = false;
        private bool _outputWordLC = true;
        private bool _outputWordUC = false;
        private bool _outputRandomSentences = false;
        private string _minNumSentences = string.Empty;
        private string _maxNumSentences = string.Empty;
        private bool _outputRandomDocument = false;
        private string _minNumParagraphs = string.Empty;
        private string _maxNumParagraphs = string.Empty;
        private string _minNumSentencesPerParagraph = string.Empty;
        private string _maxNumSentencesPerParagraph = string.Empty;
        private bool _includeDocumentTitle = false;
        private int _numRandomDataItems = 1000;

        //constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomWordsDataRequest()
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
        /// OutputRandomWords Property.
        /// </summary>
        public bool OutputRandomWords
        {
            get
            {
                return _outputRandomWords;
            }
            set
            {
                _outputRandomWords = value;
            }
        }

        /// <summary>
        /// MinNumWords Property.
        /// </summary>
        public string MinNumWords
        {
            get
            {
                return _minNumWords;
            }
            set
            {
                _minNumWords = value;
            }
        }

        /// <summary>
        /// MaxNumWords Property.
        /// </summary>
        public string MaxNumWords
        {
            get
            {
                return _maxNumWords;
            }
            set
            {
                _maxNumWords = value;
            }
        }

        /// <summary>
        /// OutputWordUCLC Property.
        /// </summary>
        public bool OutputWordUCLC
        {
            get
            {
                return _outputWordUCLC;
            }
            set
            {
                _outputWordUCLC = value;
            }
        }

        /// <summary>
        /// OutputWordLC Property.
        /// </summary>
        public bool OutputWordLC
        {
            get
            {
                return _outputWordLC;
            }
            set
            {
                _outputWordLC = value;
            }
        }

        /// <summary>
        /// OutputWordUC Property.
        /// </summary>
        public bool OutputWordUC
        {
            get
            {
                return _outputWordUC;
            }
            set
            {
                _outputWordUC = value;
            }
        }

        /// <summary>
        /// OutputRandomSentences Property.
        /// </summary>
        public bool OutputRandomSentences
        {
            get
            {
                return _outputRandomSentences;
            }
            set
            {
                _outputRandomSentences = value;
            }
        }

        /// <summary>
        /// MinNumSentences Property.
        /// </summary>
        public string MinNumSentences
        {
            get
            {
                return _minNumSentences;
            }
            set
            {
                _minNumSentences = value;
            }
        }

        /// <summary>
        /// MaxNumSentences Property.
        /// </summary>
        public string MaxNumSentences
        {
            get
            {
                return _maxNumSentences;
            }
            set
            {
                _maxNumSentences = value;
            }
        }

        /// <summary>
        /// OutputRandomDocument Property.
        /// </summary>
        public bool OutputRandomDocument
        {
            get
            {
                return _outputRandomDocument;
            }
            set
            {
                _outputRandomDocument = value;
            }
        }

        /// <summary>
        /// MinNumParagraphs Property.
        /// </summary>
        public string MinNumParagraphs
        {
            get
            {
                return _minNumParagraphs;
            }
            set
            {
                _minNumParagraphs = value;
            }
        }

        /// <summary>
        /// MaxNumParagraphs Property.
        /// </summary>
        public string MaxNumParagraphs
        {
            get
            {
                return _maxNumParagraphs;
            }
            set
            {
                _maxNumParagraphs = value;
            }
        }

        /// <summary>
        /// MinNumSentencesPerParagraph Property.
        /// </summary>
        public string MinNumSentencesPerParagraph
        {
            get
            {
                return _minNumSentencesPerParagraph;
            }
            set
            {
                _minNumSentencesPerParagraph = value;
            }
        }

        /// <summary>
        /// MaxNumSentencesPerParagraph Property.
        /// </summary>
        public string MaxNumSentencesPerParagraph
        {
            get
            {
                return _maxNumSentencesPerParagraph;
            }
            set
            {
                _maxNumSentencesPerParagraph = value;
            }
        }

        /// <summary>
        /// IncludeDocumentTitle Property.
        /// </summary>
        public bool IncludeDocumentTitle
        {
            get
            {
                return _includeDocumentTitle;
            }
            set
            {
                _includeDocumentTitle = value;
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
            XmlSerializer ser = new XmlSerializer(typeof(RandomWordsDataRequest));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of RandomWordsDataRequest.</returns>
        public static RandomWordsDataRequest LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(RandomWordsDataRequest));
            TextReader textReader = new StreamReader(filePath);
            RandomWordsDataRequest columnDefinitions;
            columnDefinitions = (RandomWordsDataRequest)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(RandomWordsDataRequest));
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
