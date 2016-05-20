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
using PFTextFiles;
using PFCollectionsObjects;

namespace PFDataOutputProcessor
{
    /// <summary>
    /// Class for managing output to a text file containing fixed length data lines.
    /// </summary>
    public class FixedLengthTextFileOutputProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataExporter _exporter = new PFDataExporter();
        private PFDataImporter _importer = new PFDataImporter();
        PFTextFile _textOutFile = new PFTextFile();

        //private variables for properties
        private enOutputType _outputType = enOutputType.FixedLengthTextFile;
        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;
        private string _outputFileName = string.Empty;
        private bool _replaceExistingFile = true;
        private bool _useLineTerminator = false;
        private bool _columnNamesOnFirstLine = false;
        private bool _allowDataTruncation = false;
        private string _lineTerminatorChars = Environment.NewLine;
        private int _columnWidthForStringData = 255;
        private int _maximumAllowedColumnWidth = 1024;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public FixedLengthTextFileOutputProcessor()
        {
            ;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outputFileName">Full path to the output file.</param>
        public FixedLengthTextFileOutputProcessor(string outputFileName)
        {
            _outputFileName = outputFileName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outputFileName">Full path to the output file.</param>
        /// <param name="replaceExistingFile">Set to true to overwrite file if it already exists. Set to false to throw an error if file already exists.</param>
        public FixedLengthTextFileOutputProcessor(string outputFileName, bool replaceExistingFile)
        {
            _outputFileName = outputFileName;
            _replaceExistingFile = replaceExistingFile;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outputFileName">Full path to the output file.</param>
        /// <param name="replaceExistingFile">Set to true to overwrite file if it already exists. Set to false to throw an error if file already exists.</param>
        /// <param name="columnNamesOnFirstLine">Set to true if you want the column names to appear in the first line of the output.</param>
        public FixedLengthTextFileOutputProcessor(string outputFileName, bool replaceExistingFile, bool columnNamesOnFirstLine)
        {
            _outputFileName = outputFileName;
            _replaceExistingFile = replaceExistingFile;
            _columnNamesOnFirstLine = columnNamesOnFirstLine;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outputFileName">Full path to the output file.</param>
        /// <param name="replaceExistingFile">Set to true to overwrite file if it already exists. Set to false to throw an error if file already exists.</param>
        /// <param name="columnNamesOnFirstLine">Set to true if you want the column names to appear in the first line of the output.</param>
        /// <param name="allowDataTruncation">If true and the data is longer than the space allowed for a column, then the data will be truncated to the column length. 
        /// If False and the data is longer than the space allowed for a colum, then an error will be thrown to the calling routine.</param>
        public FixedLengthTextFileOutputProcessor(string outputFileName, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation)
        {
            _outputFileName = outputFileName;
            _replaceExistingFile = replaceExistingFile;
            _columnNamesOnFirstLine = columnNamesOnFirstLine;
            _allowDataTruncation = allowDataTruncation;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outputFileName">Full path to the output file.</param>
        /// <param name="replaceExistingFile">Set to true to overwrite file if it already exists. Set to false to throw an error if file already exists.</param>
        /// <param name="columnNamesOnFirstLine">Set to true if you want the column names to appear in the first line of the output.</param>
        /// <param name="allowDataTruncation">If true and the data is longer than the space allowed for a column, then the data will be truncated to the column length. 
        /// If False and the data is longer than the space allowed for a colum, then an error will be thrown to the calling routine.</param>
        /// <param name="useLineTerminator">Set to True to have a new line terminator placed at the end of each data line.</param>
        public FixedLengthTextFileOutputProcessor(string outputFileName, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation, bool useLineTerminator)
        {
            _outputFileName = outputFileName;
            _replaceExistingFile = replaceExistingFile;
            _columnNamesOnFirstLine = columnNamesOnFirstLine;
            _allowDataTruncation = allowDataTruncation;
            _useLineTerminator = useLineTerminator;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outputFileName">Full path to the output file.</param>
        /// <param name="replaceExistingFile">Set to true to overwrite file if it already exists. Set to false to throw an error if file already exists.</param>
        /// <param name="columnNamesOnFirstLine">Set to true if you want the column names to appear in the first line of the output.</param>
        /// <param name="allowDataTruncation">If true and the data is longer than the space allowed for a column, then the data will be truncated to the column length. 
        /// If False and the data is longer than the space allowed for a colum, then an error will be thrown to the calling routine.</param>
        /// <param name="useLineTerminator">Set to True to have a new line terminator placed at the end of each data line.</param>
        /// <param name="lineTerminatorChars">Default is CR/LF (Environment.NewLine). Set this property if userLineTermintor is True and you need to specify a non-default set of one or more characters for the line terminator.</param>
        public FixedLengthTextFileOutputProcessor(string outputFileName, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation, bool useLineTerminator, string lineTerminatorChars)
        {
            _outputFileName = outputFileName;
            _replaceExistingFile = replaceExistingFile;
            _columnNamesOnFirstLine = columnNamesOnFirstLine;
            _allowDataTruncation = allowDataTruncation;
            _useLineTerminator = useLineTerminator;
            _lineTerminatorChars = lineTerminatorChars;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="outputFileName">Full path to the output file.</param>
        /// <param name="replaceExistingFile">Set to true to overwrite file if it already exists. Set to false to throw an error if file already exists.</param>
        /// <param name="columnNamesOnFirstLine">Set to true if you want the column names to appear in the first line of the output.</param>
        /// <param name="allowDataTruncation">If true and the data is longer than the space allowed for a column, then the data will be truncated to the column length. 
        /// If False and the data is longer than the space allowed for a colum, then an error will be thrown to the calling routine.</param>
        /// <param name="useLineTerminator">Set to True to have a new line terminator placed at the end of each data line.</param>
        /// <param name="lineTerminatorChars">Default is CR/LF (Environment.NewLine). Set this property if userLineTermintor is True and you need to specify a non-default set of one or more characters for the line terminator.</param>
        /// <param name="columnWidthForStringData">Sets the width in characters that will be allowed for each string column being output.</param>
        /// <param name="_maximumAllowedColumnWidth">Sets the maximum width in characters that will be allowed for any column regardless of data type that is being output.</param>
        public FixedLengthTextFileOutputProcessor(string outputFileName, bool replaceExistingFile, bool columnNamesOnFirstLine, bool allowDataTruncation, bool useLineTerminator, string lineTerminatorChars, int columnWidthForStringData, int _maximumAllowedColumnWidth)
        {
            _outputFileName = outputFileName;
            _replaceExistingFile = replaceExistingFile;
            _columnNamesOnFirstLine = columnNamesOnFirstLine;
            _allowDataTruncation = allowDataTruncation;
            _useLineTerminator = useLineTerminator;
            _lineTerminatorChars = lineTerminatorChars;
            _columnWidthForStringData = columnWidthForStringData;
            _maximumAllowedColumnWidth = MaximumAllowedColumnWidth;
        }

        //properties

        /// <summary>
        /// Always set to enOutputType.FixedLengthTextFile constant.
        /// </summary>
        public enOutputType OutputType
        {
            get
            {
                return _outputType;
            }
            set
            {
                _outputType = enOutputType.FixedLengthTextFile;
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

        /// <summary>
        /// Set to True to have a new line terminator placed at the end of each data line.
        /// </summary>
        public bool UseLineTerminator
        {
            get
            {
                return _useLineTerminator;
            }
            set
            {
                _useLineTerminator = value;
            }
        }

        /// <summary>
        /// Set to true if you want the column names to appear in the first line of the output.
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
        /// If true and the data is longer than the space allowed for a column, then the data will be truncated to the column length. 
        /// If False and the data is longer than the space allowed for a colum, then an error will be thrown to the calling routine.
        /// </summary>
        /// <remarks>Default value for this property is False. Truncations will be treated as an error.</remarks>
        public bool AllowDataTruncation
        {
            get
            {
                return _allowDataTruncation;
            }
            set
            {
                _allowDataTruncation = value;
            }
        }

        /// <summary>
        /// Default is CR/LF (Environment.NewLine).
        /// Set this property if you wish to have fixed length line terminators that are different from CR/LF.
        /// </summary>
        public string LineTerminatorChars
        {
            get
            {
                return _lineTerminatorChars;
            }
            set
            {
                _lineTerminatorChars = value;
            }
        }


        /// <summary>
        /// ColumnWidthForStringData Property.
        /// </summary>
        public int ColumnWidthForStringData
        {
            get
            {
                return _columnWidthForStringData;
            }
            set
            {
                _columnWidthForStringData = value;
            }
        }

        /// <summary>
        /// MaximumAllowedColumnWidth Property.
        /// </summary>
        public int MaximumAllowedColumnWidth
        {
            get
            {
                return _maximumAllowedColumnWidth;
            }
            set
            {
                _maximumAllowedColumnWidth = value;
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
                _textOutFile.OpenFile(_outputFileName, PFFileOpenOperation.OpenFileForWrite);
                _exporter.returnResultAsString += WriteFixedLengthFileOutputLine;
                _exporter.DefaultStringColumnLength = this.ColumnWidthForStringData;
                _exporter.MaxColumnLengthOverride = this.MaximumAllowedColumnWidth;
                _exporter.ExtractFixedLengthDataFromTable(dt, _useLineTerminator, _columnNamesOnFirstLine, _allowDataTruncation);

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
                if (_textOutFile.FileIsOpen)
                {
                    _textOutFile.CloseFile();
                }
            }

            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
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
                _textOutFile.OpenFile(_outputFileName, PFFileOpenOperation.OpenFileForWrite);
                _exporter.returnResultAsString += WriteFixedLengthFileOutputLine;

                for (int dtInx = 0; dtInx < dtList.Count; dtInx++)
                {
                    dt.Rows.Clear();
                    dt.ReadXml(dtList[dtInx]);
                    _exporter.DefaultStringColumnLength = this.ColumnWidthForStringData;
                    _exporter.MaxColumnLengthOverride = this.MaximumAllowedColumnWidth;
                    if (dtInx == 0)
                        _exporter.ExtractFixedLengthDataFromTable(dt, _useLineTerminator, _columnNamesOnFirstLine, _allowDataTruncation);
                    else
                        _exporter.ExtractFixedLengthDataFromTable(dt, _useLineTerminator, false, _allowDataTruncation);
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
                if (_textOutFile.FileIsOpen)
                {
                    _textOutFile.CloseFile();
                }
            }

            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be output.</param>
        /// <param name="fxlDataLine">Object containing column definitions for the line.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks> fxlDataLine object can be used to override column lengths in data table.</remarks>
        public bool WriteDataToOutput(DataTable dt, PFFixedLengthDataLine fxlDataLine)
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
                _textOutFile.OpenFile(_outputFileName, PFFileOpenOperation.OpenFileForWrite);
                _exporter.returnResultAsString += WriteFixedLengthFileOutputLine;
                _exporter.DefaultStringColumnLength = this.ColumnWidthForStringData;
                _exporter.MaxColumnLengthOverride = this.MaximumAllowedColumnWidth;
                _exporter.ExtractFixedLengthDataFromTable(fxlDataLine, dt, _useLineTerminator, _columnNamesOnFirstLine, _allowDataTruncation, (int)0, _lineTerminatorChars);

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
                if (_textOutFile.FileIsOpen)
                {
                    _textOutFile.CloseFile();
                }
            }

            return success;
        }

        /// <summary>
        /// Writes data contained in ADO.NET DataTable object to path stored in OutputFileName property.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <param name="fxlDataLine">Object containing column definitions for the line.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        /// <remarks> fxlDataLine object can be used to override column lengths in data table.</remarks>
        public bool WriteDataToOutput(PFList<string> dtList, PFFixedLengthDataLine fxlDataLine)
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
                _textOutFile.OpenFile(_outputFileName, PFFileOpenOperation.OpenFileForWrite);
                _exporter.returnResultAsString += WriteFixedLengthFileOutputLine;

                for (int dtInx = 0; dtInx < dtList.Count; dtInx++)
                {
                    dt.Rows.Clear();
                    dt.ReadXml(dtList[dtInx]);
                    _exporter.DefaultStringColumnLength = this.ColumnWidthForStringData;
                    _exporter.MaxColumnLengthOverride = this.MaximumAllowedColumnWidth;
                    if (dtInx == 0)
                        _exporter.ExtractFixedLengthDataFromTable(fxlDataLine, dt, _useLineTerminator, _columnNamesOnFirstLine, _allowDataTruncation, (int)0, _lineTerminatorChars);
                    else
                        _exporter.ExtractFixedLengthDataFromTable(fxlDataLine, dt, _useLineTerminator, false, _allowDataTruncation, (int)0, _lineTerminatorChars);
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
                if (_textOutFile.FileIsOpen)
                {
                    _textOutFile.CloseFile();
                }
            }

            return success;
        }

        private void WriteFixedLengthFileOutputLine(string outputLine, int tableNumber)
        {
            _textOutFile.WriteData(outputLine);
        }



        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(FixedLengthTextFileOutputProcessor));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of FixedLengthTextFileOutputDef.</returns>
        public static FixedLengthTextFileOutputProcessor LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(FixedLengthTextFileOutputProcessor));
            TextReader textReader = new StreamReader(filePath);
            FixedLengthTextFileOutputProcessor objectInstance;
            objectInstance = (FixedLengthTextFileOutputProcessor)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(FixedLengthTextFileOutputProcessor));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of FixedLengthTextFileOutputDef.</returns>
        public static FixedLengthTextFileOutputProcessor LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(FixedLengthTextFileOutputProcessor));
            StringReader strReader = new StringReader(xmlString);
            FixedLengthTextFileOutputProcessor objectInstance;
            objectInstance = (FixedLengthTextFileOutputProcessor)deserializer.Deserialize(strReader);
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
