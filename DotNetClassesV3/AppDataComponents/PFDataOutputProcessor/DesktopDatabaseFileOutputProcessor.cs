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
using AppGlobals;
using PFDataAccessObjects;
using PFTextFiles;
using PFSQLServerCE35Objects;

namespace PFDataOutputProcessor
{
    /// <summary>
    /// Class to manage creating and populating a SQLCE desktop database.
    /// </summary>
    /// <remarks>This class has the ability to create a new database. If database already exists, consider using the DatabaseTableOutputProcessor class.</remarks>
    public class DesktopDatabaseFileOutputProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataImporter _importer = new PFDataImporter();
        private string _defaultDbTemplatesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\InitDatabases\";
        private string _defaultSQLCE35TemplateFile = @"InitSQLCE35.sdf";
        private string _defaultSQLCE40TemplateFile = @"InitSQLCE40.sdf";
        private string _defaultSQLAnywhereTemplateFile = @"newSQLA.db";
        private string _defaultSQLAnywhereUltraLiteTemplateFile = @"initSQLA_UL.udb";

        //private string _defaultSQLCEConnectionString = "data source='<filename>'";
        private string _defaultSQLAnywhereUltraLiteConnectionString = "nt_file=<filename>;uid=DBA;pwd=sql";
        private string _defaultSQLAnywhereConnectionString = "DatabaseFile=<filename>;UserID=DBA;Password=sql";

        //private variables for properties
        private enOutputType _outputType = enOutputType.DesktopDatabaseFile;
        private DatabasePlatform _dbPlatform = DatabasePlatform.SQLServerCE35;
        private string _outputFileName = string.Empty;
        private bool _replaceExistingFile = true;
        private enDesktopDbVersion _desktopDbVersion = enDesktopDbVersion.SQLCE_Version35;
        private string _dbPassword = string.Empty;
        private string _tableName = "OutputTable";

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public DesktopDatabaseFileOutputProcessor()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="outputFileName">Full path for the output SQLCE database file.</param>
        /// <remarks>By default, SQLCE version 3.5 database is created and no password is provided for the database. Data is unencrypted.</remarks>
        public DesktopDatabaseFileOutputProcessor(string outputFileName)
        {
            _outputFileName = outputFileName;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="outputFileName">Full path for the output SQLCE database file.</param>
        /// <param name="sqlceVersion">Specify SQLCE35 for a database in SQLCE version 3.5 format. (This is the default.) Specify SQLCE40 for a database in SQLCE version 4.0 format.</param>
        /// <remarks>By default, no password is provided for the database. Data is unencrypted.</remarks>
        public DesktopDatabaseFileOutputProcessor(string outputFileName, enDesktopDbVersion sqlceVersion)
        {
            _outputFileName = outputFileName;
            this.SQLCEVersion = sqlceVersion;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="outputFileName">Full path for the output SQLCE database file.</param>
        /// <param name="sqlceVersion">Specify SQLCE35 for a database in SQLCE version 3.5 format. (This is the default.) Specify SQLCE40 for a database in SQLCE version 4.0 format.</param>
        /// <param name="dbPassword">Password for the username assigned to the database. Default is blank (no password). Set the password if you wish to have the database data encrypted.</param>
        public DesktopDatabaseFileOutputProcessor(string outputFileName, enDesktopDbVersion sqlceVersion, string dbPassword)
        {
            _outputFileName = outputFileName;
            this.SQLCEVersion = sqlceVersion;
            _dbPassword = dbPassword;
        }

        //properties

        /// <summary>
        /// Always set to enOutputType.DesktopDatabaseFile constant.
        /// </summary>
        public enOutputType OutputType
        {
            get
            {
                return _outputType;
            }
            set
            {
                _outputType = enOutputType.DesktopDatabaseFile;
            }
        }

        /// <summary>
        /// Set the DbPlatform value to either SQLServerCE35 or SQLServerCE40..
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
                if (_dbPlatform == DatabasePlatform.SQLServerCE40)
                    _desktopDbVersion = enDesktopDbVersion.SQLCE_Version40;
                else
                    _desktopDbVersion = enDesktopDbVersion.SQLCE_Version35;
            }
        }

        /// <summary>
        /// Full path for the output SQLCE database file.
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
        /// Specify SQLCE35 for a database in SQLCE version 3.5 format. (This is the default.) 
        /// Specify SQLCE40 for a database in SQLCE version 4.0 format.
        /// </summary>
        public enDesktopDbVersion SQLCEVersion
        {
            get
            {
                return _desktopDbVersion;
            }
            set
            {
                _desktopDbVersion = value;
                if (_desktopDbVersion == enDesktopDbVersion.SQLCE_Version40)
                    _dbPlatform = DatabasePlatform.SQLServerCE40;
                else
                    _dbPlatform = DatabasePlatform.SQLServerCE35;
            }
        }

        /// <summary>
        /// Password for the username assigned to the database. Default is blank (no password). 
        /// Set the password if you wish to have the database data encrypted.
        /// </summary>
        public string DbPassword
        {
            get
            {
                return _dbPassword;
            }
            set
            {
                _dbPassword = value;
            }
        }

        /// <summary>
        /// Name for table that will be created in output SQLCE database file.
        /// </summary>
        public string TableName
        {
            get
            {
                return _tableName;
            }
            set
            {
                _tableName = value;
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
            IDesktopDatabaseProvider db = null;

            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            string templateFile = string.Empty;

            try
            {
                if (File.Exists(_outputFileName))
                {
                    if (_replaceExistingFile)
                    {
                        try
                        {
                            File.SetAttributes(_outputFileName, FileAttributes.Normal);
                            File.Delete(_outputFileName);
                        }
                        catch (System.Exception ex)
                        {
                            _msg.Length = 0;
                            _msg.Append("Unable to delete old file. It may be locked by SQLAnywhere local server. Exit this application and try again. This should remove the lock.");
                            _msg.Append(Environment.NewLine);
                            _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                            throw new System.Exception(_msg.ToString());
                        }
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("File exists and ReplaceExistingFile set to False. Write to Output has failed.");
                        throw new System.Exception(_msg.ToString());
                    }
                }

                string oldLogFile = Path.Combine(Path.GetDirectoryName(_outputFileName), Path.GetFileNameWithoutExtension(_outputFileName) + ".Log");
                if (File.Exists(oldLogFile))
                {
                    File.SetAttributes(oldLogFile, FileAttributes.Normal);
                    File.Delete(oldLogFile);
                }

                if (this._desktopDbVersion == enDesktopDbVersion.SQLCE_Version40)
                {
                    templateFile = Path.Combine(_defaultDbTemplatesFolder, _defaultSQLCE40TemplateFile);
                    dbPlatformDesc = DatabasePlatform.SQLServerCE40.ToString();
                }
                else if (this._desktopDbVersion == enDesktopDbVersion.SQLAnywhere)
                {
                    templateFile = Path.Combine(_defaultDbTemplatesFolder, _defaultSQLAnywhereTemplateFile);
                    dbPlatformDesc = DatabasePlatform.SQLAnywhere.ToString();
                }
                else if (this._desktopDbVersion == enDesktopDbVersion.SQLAnywhere_UltraLite)
                {
                    templateFile = Path.Combine(_defaultDbTemplatesFolder, _defaultSQLAnywhereUltraLiteTemplateFile);
                    dbPlatformDesc = DatabasePlatform.SQLAnywhereUltraLite.ToString();
                }
                else
                {
                    templateFile = Path.Combine(_defaultDbTemplatesFolder, _defaultSQLCE35TemplateFile);
                    dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
                }

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];
                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);

                if (_desktopDbVersion == enDesktopDbVersion.SQLCE_Version35
                    || _desktopDbVersion == enDesktopDbVersion.SQLCE_Version40)
                {
                    db.DatabasePath = _outputFileName;
                    connStr = db.ConnectionString;
                    db.CreateDatabase(connStr);
                }
                else if (_desktopDbVersion == enDesktopDbVersion.SQLAnywhere_UltraLite)
                {
                    connStr = OutputFileName;
                    db.CreateDatabase(connStr);
                    connStr = _defaultSQLAnywhereUltraLiteConnectionString.Replace("<filename>", OutputFileName);
                    db.ConnectionString = connStr;
                }
                else if (_desktopDbVersion == enDesktopDbVersion.SQLAnywhere)
                {
                    if (File.Exists(templateFile))
                    {
                        connStr = _defaultSQLAnywhereConnectionString.Replace("<filename>", templateFile);
                        db.ConnectionString = connStr;
                        db.OpenConnection();
                        db.CreateDatabase(OutputFileName);
                        db.CloseConnection();
                        //db.CreateDatabase(OutputFileName, templateFile);   //file copy create not working properly: transaction log lock outs.
                        connStr = _defaultSQLAnywhereConnectionString.Replace("<filename>", OutputFileName);
                        db.ConnectionString = connStr;
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to find template file for SQLAnywhere databases: ");
                        _msg.Append(templateFile);
                        _msg.Append(".");
                        throw new System.Exception(_msg.ToString());
                    }
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid or unexpected desktop database platform: ");
                    _msg.Append(_desktopDbVersion);
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }
                db.OpenConnection();
                db.CreateTable(dt);
                db.CloseConnection();
                db.OpenConnection();
                db.ImportDataFromDataTable(dt);   //this is very slow for SQLAnywhere UltraLite
                db.CloseConnection();
                

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
                if (db != null)
                {
                    if (db.IsConnected)
                    {
                        db.CloseConnection();
                    }
                    db = null;
                }
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
            XmlSerializer ser = new XmlSerializer(typeof(DesktopDatabaseFileOutputProcessor));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of SQLCEDatabaseFileOutputProcessor.</returns>
        public static DesktopDatabaseFileOutputProcessor LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DesktopDatabaseFileOutputProcessor));
            TextReader textReader = new StreamReader(filePath);
            DesktopDatabaseFileOutputProcessor objectInstance;
            objectInstance = (DesktopDatabaseFileOutputProcessor)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(DesktopDatabaseFileOutputProcessor));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of SQLCEDatabaseFileOutputProcessor.</returns>
        public static DesktopDatabaseFileOutputProcessor LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DesktopDatabaseFileOutputProcessor));
            StringReader strReader = new StringReader(xmlString);
            DesktopDatabaseFileOutputProcessor objectInstance;
            objectInstance = (DesktopDatabaseFileOutputProcessor)deserializer.Deserialize(strReader);
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
