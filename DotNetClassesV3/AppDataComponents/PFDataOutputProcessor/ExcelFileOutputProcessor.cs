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
using PFCollectionsObjects;

namespace PFDataOutputProcessor
{
    /// <summary>
    /// Class contains parameters and routines for managing an Excel file output connection.
    /// </summary>
    public class ExcelDocumentFileOutputProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataExporter _exporter = new PFDataExporter();
        private PFDataImporter _importer = new PFDataImporter();

        //private variables for properties
        private enOutputType _outputType = enOutputType.ExcelDocumentFile;
        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;
        enExcelVersion _excelVersion = enExcelVersion.NotSpecified;
        private string _outputFileName = string.Empty;
        private string _sheetName = "Sheet1";
        private bool _replaceExistingFile = true;
        private bool _replaceExistingSheet = true;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public ExcelDocumentFileOutputProcessor()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelVersion">Set to enExcelVersion.Excel2003 for an XLS file. Set to enExcelVersion.Excel2007 for an XLSX file.</param>
        public ExcelDocumentFileOutputProcessor(enExcelVersion excelVersion)
        {
            _excelVersion = excelVersion;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelVersion">Set to enExcelVersion.Excel2003 for an XLS file. Set to enExcelVersion.Excel2007 for an XLSX file.</param>
        /// <param name="outputFilename">Full path to the output file.</param>
        public ExcelDocumentFileOutputProcessor(enExcelVersion excelVersion, string outputFilename)
        {
            _excelVersion = excelVersion;
            _outputFileName = outputFilename;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelVersion">Set to enExcelVersion.Excel2003 for an XLS file. Set to enExcelVersion.Excel2007 for an XLSX file.</param>
        /// <param name="outputFilename">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to modify a file with the same name.</param>
        public ExcelDocumentFileOutputProcessor(enExcelVersion excelVersion, string outputFilename, bool replaceExistingFile)
        {
            _excelVersion = excelVersion;
            _outputFileName = outputFilename;
            _replaceExistingFile = replaceExistingFile;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelVersion">Set to enExcelVersion.Excel2003 for an XLS file. Set to enExcelVersion.Excel2007 for an XLSX file.</param>
        /// <param name="outputFilename">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to modify a file with the same name.</param>
        /// <param name="sheetName">Name to give to the worksheet that will be created.</param>
        public ExcelDocumentFileOutputProcessor(enExcelVersion excelVersion, string outputFilename, bool replaceExistingFile, string sheetName)
        {
            _excelVersion = excelVersion;
            _outputFileName = outputFilename;
            _replaceExistingFile = replaceExistingFile;
            _sheetName = sheetName;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="excelVersion">Set to enExcelVersion.Excel2003 for an XLS file. Set to enExcelVersion.Excel2007 for an XLSX file.</param>
        /// <param name="outputFilename">Full path to the output file.</param>
        /// <param name="replaceExistingFile">True to overwrite existing file with same name. Otherwise, set to false to modify a file with the same name.</param>
        /// <param name="sheetName">Name to give to the worksheet that will be created.</param>
        /// <param name="replaceExistingSheet">True to overwrite existing sheet with same name. Otherwise, set to false to modify contents of existing sheet.</param>
        public ExcelDocumentFileOutputProcessor(enExcelVersion excelVersion, string outputFilename, bool replaceExistingFile, string sheetName, bool replaceExistingSheet)
        {
            _excelVersion = excelVersion;
            _outputFileName = outputFilename;
            _replaceExistingFile = replaceExistingFile;
            _sheetName = sheetName;
            _replaceExistingSheet = replaceExistingSheet;
        }

        //properties

        /// <summary>
        /// Always set to enOutputType.ExcelDocumentFile constant.
        /// </summary>
        public enOutputType OutputType
        {
            get
            {
                return _outputType;
            }
            set
            {
                _outputType = enOutputType.ExcelDocumentFile;
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
        /// Set to enExcelVersion.Excel2003 for an XLS file.
        /// Set to enExcelVersion.Excel2007 for an XLSX file.
        /// </summary>
        public enExcelVersion ExcelVersion
        {
            get
            {
                return _excelVersion;
            }
            set
            {
                _excelVersion = value;
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
        /// Name for the worksheet that will be created. Default is Sheet1.
        /// </summary>
        public string SheetName
        {
            get
            {
                return _sheetName;
            }
            set
            {
                _sheetName = value;
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

        /// <summary>
        /// If True and sheet already exists, the old sheet will be deleted and the new sheet created.
        /// If False, an error will be raised if a sheet with the same name already exists.
        /// </summary>
        /// <remarks>Default is True. An existing sheet with same name will be deleted.</remarks>
        public bool ReplaceExistingSheet
        {
            get
            {
                return _replaceExistingSheet;
            }
            set
            {
                _replaceExistingSheet = value;
            }
        }




        //methods

        /// <summary>
        /// Writes data contained in XML string to path stored in OutputFileName property.
        /// </summary>
        /// <param name="xmlString">String containing valid XML formatted data.</param>
        /// <returns>True if write succeeded.</returns>
        /// <remarks>Non Ext version of WriteDataToOutput will report an error if ReplaceExistingFile is false and file exists.</remarks>
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
        /// <remarks>Non Ext version of WriteDataToOutput will report an error if ReplaceExistingFile is false and file exists.</remarks>
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
        /// <remarks>Non Ext version of WriteDataToOutput will report an error if ReplaceExistingFile is false and file exists.</remarks>
        public bool WriteDataToOutput(DataTable dt)
        {
            bool success = true;

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

                enExcelOutputFormat outputFormat = enExcelOutputFormat.NotSpecified;
                switch (this.ExcelVersion)
                {
                    case enExcelVersion.Excel2007:
                        outputFormat = enExcelOutputFormat.Excel2007;
                        break;
                    case enExcelVersion.Excel2003:
                        outputFormat = enExcelOutputFormat.Excel2003;
                        break;
                    case enExcelVersion.CSV:
                        outputFormat = enExcelOutputFormat.CSV;
                        break;
                    default:
                        outputFormat = enExcelOutputFormat.CSV;
                        break;
                }
                PFExcelDocument excelDoc = new PFExcelDocument(outputFormat, this.OutputFileName, this.SheetName, this.ReplaceExistingFile);
                excelDoc.WriteDataToDocument(dt);

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
                ;
            }

            return success;
        }


        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="dtList">List of temp file names pointing to XML files containing data table data to be output.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks>Non Ext version of WriteDataToOutput will report an error if ReplaceExistingFile is false and file exists.</remarks>
        public bool WriteDataToOutput(PFList<string> dtList)
        {
            bool success = true;
            DataTable dt = new DataTable();

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

                enExcelOutputFormat outputFormat = enExcelOutputFormat.NotSpecified;
                switch (this.ExcelVersion)
                {
                    case enExcelVersion.Excel2007:
                        outputFormat = enExcelOutputFormat.Excel2007;
                        break;
                    case enExcelVersion.Excel2003:
                        outputFormat = enExcelOutputFormat.Excel2003;
                        break;
                    case enExcelVersion.CSV:
                        outputFormat = enExcelOutputFormat.CSV;
                        break;
                    default:
                        outputFormat = enExcelOutputFormat.CSV;
                        break;
                }
                PFExcelDocument excelDoc = new PFExcelDocument(outputFormat, this.OutputFileName, this.SheetName, this.ReplaceExistingFile);

                for (int dtInx = 0; dtInx < dtList.Count; dtInx++)
                {
                    dt.Rows.Clear();
                    dt.ReadXml(dtList[dtInx]);
                    if (dtInx == 0)
                        excelDoc.WriteDataToDocument(dt);
                    else
                        excelDoc.AppendDataToExistingSheet(dt);
                }

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
                ;
            }

            return success;
        }


        /// <summary>
        /// Writes data contained in XML string to path stored in OutputFileName property.
        /// </summary>
        /// <param name="xmlString">String containing valid XML formatted data.</param>
        /// <returns>True if write succeeded.</returns>
        /// <remarks>Ext version of WriteDataToOutputExt allows for modifying existing documents.</remarks>
        public bool WriteDataToOutputExt(string xmlString)
        {
            bool success = true;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);
            success = WriteDataToOutputExt(xmlDoc);
            return success;
        }

        /// <summary>
        /// Writes data contained in XML document object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="xmlDoc">XML formatted document object.</param>
        /// <returns>True if write succeeded.</returns>
        /// <remarks>Ext version of WriteDataToOutputExt allows for modifying existing documents.</remarks>
        public bool WriteDataToOutputExt(XmlDocument xmlDoc)
        {
            bool success = true;
            DataTable dt = _importer.ImportXmlDocumentToDataTable(xmlDoc);
            success = WriteDataToOutputExt(dt);
            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be output.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks>Ext version of WriteDataToOutputExt allows for modifying existing documents.</remarks>
        public bool WriteDataToOutputExt(DataTable dt)
        {
            bool success = true;

            try
            {
                if (File.Exists(_outputFileName))
                {
                    if (_replaceExistingFile)
                    {
                        File.SetAttributes(_outputFileName, FileAttributes.Normal);
                        File.Delete(_outputFileName);
                    }
                }

                enExcelOutputFormat outputFormat = enExcelOutputFormat.NotSpecified;
                switch (this.ExcelVersion)
                {
                    case enExcelVersion.Excel2007:
                        outputFormat = enExcelOutputFormat.Excel2007;
                        break;
                    case enExcelVersion.Excel2003:
                        outputFormat = enExcelOutputFormat.Excel2003;
                        break;
                    case enExcelVersion.CSV:
                        outputFormat = enExcelOutputFormat.CSV;
                        break;
                    default:
                        outputFormat = enExcelOutputFormat.CSV;
                        break;
                }
                PFExcelDocument excelDoc = new PFExcelDocument(outputFormat, this.OutputFileName, this.SheetName, this.ReplaceExistingFile, this.ReplaceExistingSheet);
                excelDoc.WriteDataToDocumentExt(dt);

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
                ;
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
            XmlSerializer ser = new XmlSerializer(typeof(ExcelDocumentFileOutputProcessor));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of ExcelDocumentFileOutputDef.</returns>
        public static ExcelDocumentFileOutputProcessor LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ExcelDocumentFileOutputProcessor));
            TextReader textReader = new StreamReader(filePath);
            ExcelDocumentFileOutputProcessor objectInstance;
            objectInstance = (ExcelDocumentFileOutputProcessor)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(ExcelDocumentFileOutputProcessor));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of ExcelDocumentFileOutputDef.</returns>
        public static ExcelDocumentFileOutputProcessor LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ExcelDocumentFileOutputProcessor));
            StringReader strReader = new StringReader(xmlString);
            ExcelDocumentFileOutputProcessor objectInstance;
            objectInstance = (ExcelDocumentFileOutputProcessor)deserializer.Deserialize(strReader);
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
