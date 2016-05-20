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
using PFDocumentObjects;
using PFDocumentGlobals;

namespace PFDataOutputProcessor
{
    /// <summary>
    /// Class for managing output to Word docuemnts.
    /// </summary>
    public class WordFileOutputProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataExporter _exporter = new PFDataExporter();
        private PFDataImporter _importer = new PFDataImporter();

        //private variables for properties
        private enOutputType _outputType = enOutputType.WordDocumentFile;
        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;
        enWordVersion _wordVersion = enWordVersion.NotSpecified;
        private string _outputFileName = string.Empty;
        private bool _replaceExistingFile = true;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public WordFileOutputProcessor()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordVersion">Set to enWordVersion.Word2003 for an XLS file. Set to enWordVersion.Word2007 for an XLSX file.</param>
        public WordFileOutputProcessor(enWordVersion wordVersion)
        {
            _wordVersion = wordVersion;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordVersion">Set to enWordVersion.Word2003 for an XLS file. Set to enWordVersion.Word2007 for an XLSX file.</param>
        /// <param name="outputFilename">Full path to the output file.</param>
        public WordFileOutputProcessor(enWordVersion wordVersion, string outputFilename)
        {
            _wordVersion = wordVersion;
            _outputFileName = outputFilename;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="wordVersion">Set to enWordVersion.Word2003 for an XLS file. Set to enWordVersion.Word2007 for an XLSX file.</param>
        /// <param name="outputFilename">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to throw an error if file with same name already exists.</param>
        public WordFileOutputProcessor(enWordVersion wordVersion, string outputFilename, bool replaceExistingFile)
        {
            _wordVersion = wordVersion;
            _outputFileName = outputFilename;
            _replaceExistingFile = replaceExistingFile;
        }


        //properties
       
        /// <summary>
        /// Always set to enOutputType.WordDocumentFile constant.
        /// </summary>
        public enOutputType OutputType
        {
            get
            {
                return _outputType;
            }
            set
            {
                _outputType = enOutputType.WordDocumentFile;
            }
        }

        /// <summary>
        /// Always set to DatabasePlatform.Unknown constant.
        /// </summary>
        public DatabasePlatform DbPlatform
        {
            get
            {
                return _dbPlatform;
            }
            set
            {
                _dbPlatform = DatabasePlatform.Unknown;
            }
        }

        /// <summary>
        /// Set to enWordVersion.Word2003 for a DOC file.
        /// Set to enWordVersion.Word2007 for a DOCX file.
        /// </summary>
        public enWordVersion WordVersion
        {
            get
            {
                return _wordVersion;
            }
            set
            {
                _wordVersion = value;
            }
        }

        /// <summary>
        /// Full path to the output file.
        /// </summary>
        public string OutputFileName
        {
            get
            {
                return _outputFileName;
            }
            set
            {
                _outputFileName = value;
            }
        }

        /// <summary>
        /// If True and file already exists, the old file will be deleted and the new file created.
        /// If False, an error will be raised if a file by the same name already exists.
        /// </summary>
        /// <remarks>Default is True. An existing file with same name will be deleted.</remarks>
        public bool ReplaceExistingFile
        {
            get
            {
                return _replaceExistingFile;
            }
            set
            {
                _replaceExistingFile = value;
            }
        }



        //methods

        /// <summary>
        /// Writes data contained in XML string to path stored in OutputFileName property.
        /// </summary>
        /// <param name="xmlString">String containing valid XML formatted data.</param>
        /// <returns>True if write succeeded.</returns>
        public bool WriteDataToOutput(string xmlString)
        {
            bool success = true;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            success = WriteDataToOutput(xmlDoc);
            return success;
        }

        /// <summary>
        /// Writes data contained in XML document object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="xmlDoc">XML formatted document object.</param>
        /// <returns>True if write succeeded.</returns>
        public bool WriteDataToOutput(XmlDocument xmlDoc)
        {
            bool success = true;
            DataTable dt = _importer.ImportXmlDocumentToDataTable(xmlDoc);
            success = WriteDataToOutput(dt);
            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be output.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        public bool WriteDataToOutput(DataTable dt)
        {
            bool success = true;
            PFWordDocument docOut = null;

            try
            {
                if (File.Exists(_outputFileName))
                {
                    if (_replaceExistingFile)
                    {
                        File.SetAttributes(_outputFileName, FileAttributes.Normal);
                        File.Delete(_outputFileName);
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("File exists and ReplaceExistingFile set to False. Write to Output has failed.");
                        throw new System.Exception(_msg.ToString());
                    }
                }

                enWordOutputFormat outputFormat = this.WordVersion == enWordVersion.Word2007 ? enWordOutputFormat.Word2007 : enWordOutputFormat.Word2003;
                docOut = new PFWordDocument(outputFormat, this.OutputFileName, this.ReplaceExistingFile);
                docOut.WriteDataToDocument(dt);


            }
            catch (System.Exception ex)
            {
                success = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                throw new System.Exception(_msg.ToString());
            }
            finally
            {
                if(docOut != null)
                    docOut=null;
            }

            return success;
        }


        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(WordFileOutputProcessor));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of WordDocumentOutputProcessor.</returns>
        public static WordFileOutputProcessor LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(WordFileOutputProcessor));
            TextReader textReader = new StreamReader(filePath);
            WordFileOutputProcessor objectInstance;
            objectInstance = (WordFileOutputProcessor)deserializer.Deserialize(textReader);
            textReader.Close();
            return objectInstance;
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
            XmlSerializer ser = new XmlSerializer(typeof(WordFileOutputProcessor));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of WordDocumentOutputProcessor.</returns>
        public static WordFileOutputProcessor LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(WordFileOutputProcessor));
            StringReader strReader = new StringReader(xmlString);
            WordFileOutputProcessor objectInstance;
            objectInstance = (WordFileOutputProcessor)deserializer.Deserialize(strReader);
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
