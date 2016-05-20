//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2016
//
//****************************************************************************************************
using System;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using PFDataAccessObjects;
using AppGlobals;
using PFCollectionsObjects;

namespace PFDataOutputProcessor
{
    /// <summary>
    /// Class for managing output to a relational database table.
    /// </summary>
    public class DatabaseOutputProcessor
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataImporter _importer = new PFDataImporter();

        private string[,] _dbNamesAndLocations = new string [,]{
                                                {"MSSQLServer", "PFDataAccessObjects","PFSQLServer", System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFDataAccessObjects.dll")},
                                                {"SQLServerCE35", "PFSQLServerCE35Objects","PFSQLServerCE35",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFSQLServerCE35Objects.dll")},
                                                {"SQLServerCE40", "PFSQLServerCE40Objects","PFSQLServerCE40",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFSQLServerCE40Objects.dll")},
                                                {"MSAccess", "PFDataAccessObjects","PFMsAccess",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFDataAccessObjects.dll")},
                                                {"MSOracle", "PFDataAccessObjects","PFMsOracle",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFDataAccessObjects.dll")},
                                                {"OracleNative", "PFOracleObjects","PFOracle",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFOracleObjects.dll")},
                                                {"MySQL", "PFMySQLObjects","PFMySQL",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFMySQLObjects.dll")},
                                                {"DB2", "PFDB2Objects","PFDB2",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFDB2Objects.dll")},
                                                {"Informix", "PFInformixObjects","PFInformix",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFInformixObjects.dll")},
                                                {"Sybase", "PFSybaseObjects","PFSybase",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFSybaseObjects.dll")},
                                                {"SQLAnywhere", "PFSQLAnywhereObjects","PFSQLAnywhere",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFSQLAnywhereObjects.dll")},
                                                {"SQLAnywhereUltraLite", "PFSQLAnywhereULObjects","PFSQLAnywhereUL",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFSQLAnywhereULObjects.dll")},
                                                {"ODBC", "PFDataAccessObjects","PFOdbc",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFDataAccessObjects.dll")},
                                                {"OLEDB", "PFDataAccessObjects","PFOleDb",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFDataAccessObjects.dll")},
                                                {"Unknown", "PFDataAccessObjects","PFDatabase",System.IO.Path.Combine(AppInfo.CurrentEntryAssemblyDirectory,"PFDataAccessObjects.dll")}
                                                };

        private int _dbPlatformDescInx = 0;
        private int _dbNamespaceInx = 1;
        private int _dbClassNameInx = 2;
        private int _dbDllPathInx = 3;

        //private variables for properties
        private enOutputType _outputType = enOutputType.DatabaseTable;
        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;
        private string _dbPlatformDesc = "Unknown";
        private string _dbNamespace = string.Empty;
        private string _dbClassName = string.Empty;
        private string _dbDllPath = string.Empty;
        private string _connectionString = string.Empty;

        private string _tableName = "OutputTable";
        private bool _replaceExistingTable = true;
        private int _outputBatchSize = 1;

        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseOutputProcessor()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseOutputProcessor(DatabasePlatform dbPlatform)
        {
            InitDbDef(dbPlatform);
        }

        private void InitDbDef(DatabasePlatform dbPlatform)
        {
            int rowInx = 0;
            int maxRowInx = _dbNamesAndLocations.GetLength(0)-1;

            _dbPlatform = dbPlatform;
            _dbPlatformDesc = dbPlatform.ToString();

            string configValue = AppConfig.GetStringValueFromConfigFile(_dbPlatformDesc, string.Empty);
            if (configValue.Length > 0)
            {
                string[] parsedConfig = configValue.Split('|');
                _dbNamespace = parsedConfig[0];
                _dbClassName = parsedConfig[1];
                _dbDllPath = parsedConfig[2];
            }
            else
            {
                for (rowInx = 0; rowInx <= maxRowInx; rowInx++)
                {
                    if (_dbNamesAndLocations[rowInx, _dbPlatformDescInx].ToLower() == _dbPlatformDesc.ToLower())
                    {
                        _dbNamespace = _dbNamesAndLocations[rowInx, _dbNamespaceInx];
                        _dbClassName = _dbNamesAndLocations[rowInx, _dbClassNameInx];
                        _dbDllPath = _dbNamesAndLocations[rowInx, _dbDllPathInx];
                    }
                }
            }

        }

        //properties

        /// <summary>
        /// Always set to enOutputType.DatabaseTable constant.
        /// </summary>
        public enOutputType OutputType
        {
            get
            {
                return _outputType;
            }
            set
            {
                _outputType = enOutputType.DatabaseTable;
            }
        }

        /// <summary>
        /// Set to the DatabasePlatform enumeration that represents output for this instance.
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
                InitDbDef(_dbPlatform);
            }
        }

        /// <summary>
        /// String description of the DBPlatform value.
        /// </summary>
        public string DbPlatformDesc
        {
            get
            {
                return _dbPlatformDesc;
            }
            set
            {
                _dbPlatformDesc = value;
            }
        }

        /// <summary>
        /// Namespace for the class that manages data access for this instance's DbPlatform value..
        /// </summary>
        public string DbNamespace
        {
            get
            {
                return _dbNamespace;
            }
            set
            {
                _dbNamespace = value;
            }
        }

        /// <summary>
        /// Name of the class that manages data access for this instance's DbPlatform value.
        /// </summary>
        public string DbClassName
        {
            get
            {
                return _dbClassName;
            }
            set
            {
                _dbClassName = value;
            }
        }

        /// <summary>
        /// Full path to the DLL that manages data access for this instance's DbPlatform value.
        /// </summary>
        public string DbDllPath
        {
            get
            {
                return _dbDllPath;
            }
            set
            {
                _dbDllPath = value;
            }
        }

        /// <summary>
        /// Database ConnectionString Property.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        /// <summary>
        /// Name of table that will store the data.
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

        /// <summary>
        /// If true and table exists, table will be deleted and then recreated. Otherwise data will be imported into the existing table..
        /// </summary>
        /// <remarks>If importing data to an existing table and the schema for the table does not match the incoming data, an error exception will be thrown.w</remarks>
        public bool ReplaceExistingTable
        {
            get
            {
                return _replaceExistingTable;
            }
            set
            {
                _replaceExistingTable = value;
            }
        }

        /// <summary>
        /// Specifies the number of data rows to include in a single data output update/write operation.
        /// </summary>
        /// <remarks>This number is ignoed by some data providers. In these cases, output batch size is always 1 row for each output update/write operation.</remarks>
        public int OutputBatchSize
        {
            get
            {
                return _outputBatchSize;
            }
            set
            {
                _outputBatchSize = value;
            }
        }


        //methods

        /// <summary>
        /// Writes data contained in XML string to table stored in TableName property.
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
        /// Writes data contained in XML string to table stored in TableName property.
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
        /// Writes data contained in DataTable to table stored in TableName property.
        /// </summary>
        /// <param name="dt">DataTable object containing data to be output.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        public bool WriteDataToOutput(DataTable dt)
        {
            bool success = true;
            PFDatabase db = null;
            bool createTable = false;
            DataTable saveDtSchema = null;
            string tableName = string.Empty;
            bool isOracleOdbc = false;
            bool isOracleOledb = false;

            try
            {
                //save column names that may be changed during export processing so that they can be restored in finally block below
                saveDtSchema = dt.Clone();

                db = new PFDatabase(this.DbPlatform, this.DbDllPath, this.DbNamespace + "." + this.DbClassName);
                db.ConnectionString = this.ConnectionString;
                
                tableName = this.TableName;

                //workaround for oracle odbc and oledb driver problems with inserts
                if (this.DbPlatform == DatabasePlatform.ODBC)
                {
                    PFOdbc odbcDb = new PFOdbc();
                    odbcDb.ConnectionString = this.ConnectionString;
                    DatabasePlatform odbcDbPlat = odbcDb.GetDatabasePlatform();
                    if (odbcDbPlat == DatabasePlatform.OracleNative || odbcDbPlat == DatabasePlatform.MSOracle)
                        isOracleOdbc = true;
                    odbcDb = null;
                }

                if (this.DbPlatform == DatabasePlatform.OLEDB)
                {
                    PFOleDb oledbDb = new PFOleDb();
                    oledbDb.ConnectionString = this.ConnectionString;
                    DatabasePlatform oledbDbPlat = oledbDb.GetDatabasePlatform();
                    if (oledbDbPlat == DatabasePlatform.OracleNative || oledbDbPlat == DatabasePlatform.MSOracle)
                        isOracleOledb = true;
                    oledbDb = null;
                }

                //set table and column names to upper case if this is an Oracle ODBC driver or OLEDB provider
                if (isOracleOdbc || isOracleOledb)
                {
                    tableName = this.TableName.ToUpper();
                    for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
                    {
                        dt.Columns[colInx].ColumnName = dt.Columns[colInx].ColumnName.ToUpper();
                    }
                }

                //end workaround for oracle odbc and oledb

                dt.TableName = tableName;

                if (db.TableExists(tableName))
                {
                    if (this.ReplaceExistingTable)
                    {
                        db.OpenConnection();
                        db.DropTable(tableName);
                        db.CloseConnection();
                        createTable = true;
                    }
                    else
                    {
                        createTable = false;  //existing table will be imported into
                    }
                }
                else
                {
                    createTable = true;
                }

                if (createTable)
                {
                    db.OpenConnection();
                    string createScript = string.Empty;
                    string errorMessages = string.Empty;
                    bool tabCreated = db.CreateTable(dt, out createScript, out errorMessages);
                    if (tabCreated == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to create table: ");
                        _msg.Append(tableName);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Error Messages: ");
                        _msg.Append(errorMessages);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Create Script: ");
                        _msg.Append(createScript);
                        throw new System.Exception(_msg.ToString());
                    }
                    db.CloseConnection();
                }

                db.OpenConnection();
                db.ImportDataFromDataTable(dt, this.OutputBatchSize);
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
                //resstore any column names that may have been changed during export processing
                for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
                {
                    dt.Columns[colInx].ColumnName = saveDtSchema.Columns[colInx].ColumnName;
                }
            }

            return success;
        }


        /// <summary>
        /// Writes data contained in DataTable to table stored in TableName property.
        /// </summary>
        /// <param name="dtList">List of temp file names containing data tables with grid rows to be output.</param>
        /// <returns>True if output operation is successful. False if write fails.</returns>
        public bool WriteDataToOutput(PFList<string> dtList)
        {
            bool success = true;
            PFDatabase db = null;
            bool createTable = false;
            DataTable dt = new DataTable();
            string tableName = string.Empty;
            bool isOracleOdbc = false;
            bool isOracleOledb = false;

            try
            {
                if (dtList.Count == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("WriteDataToOutput for list of temp files has failed.");
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Temp file name list is empty.");
                    throw new System.Exception(_msg.ToString());
                }

                //Load first temp file in list to get data table schema
                dt.Rows.Clear();
                dt.ReadXml(dtList[0]);

                db = new PFDatabase(this.DbPlatform, this.DbDllPath, this.DbNamespace + "." + this.DbClassName);
                db.ConnectionString = this.ConnectionString;

                tableName = this.TableName;

                //workaround for oracle odbc and oledb driver problems with inserts
                if (this.DbPlatform == DatabasePlatform.ODBC)
                {
                    PFOdbc odbcDb = new PFOdbc();
                    odbcDb.ConnectionString = this.ConnectionString;
                    DatabasePlatform odbcDbPlat = odbcDb.GetDatabasePlatform();
                    if (odbcDbPlat == DatabasePlatform.OracleNative || odbcDbPlat == DatabasePlatform.MSOracle)
                        isOracleOdbc = true;
                    odbcDb = null;
                }

                if (this.DbPlatform == DatabasePlatform.OLEDB)
                {
                    PFOleDb oledbDb = new PFOleDb();
                    oledbDb.ConnectionString = this.ConnectionString;
                    DatabasePlatform oledbDbPlat = oledbDb.GetDatabasePlatform();
                    if (oledbDbPlat == DatabasePlatform.OracleNative || oledbDbPlat == DatabasePlatform.MSOracle)
                        isOracleOledb = true;
                    oledbDb = null;
                }

                //set table and column names to upper case if this is an Oracle ODBC driver or OLEDB provider
                if (isOracleOdbc || isOracleOledb)
                {
                    tableName = this.TableName.ToUpper();
                    for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
                    {
                        dt.Columns[colInx].ColumnName = dt.Columns[colInx].ColumnName.ToUpper();
                    }
                }
                else
                {
                    tableName = this.TableName;
                }

                //end workaround for oracle odbc and oledb



                dt.TableName = tableName;

                if (db.TableExists(tableName))
                {
                    if (this.ReplaceExistingTable)
                    {
                        db.OpenConnection();
                        db.DropTable(tableName);
                        db.CloseConnection();
                        createTable = true;
                    }
                    else
                    {
                        createTable = false;  //existing table will be imported into
                    }
                }
                else
                {
                    createTable = true;
                }

                if (createTable)
                {
                    db.OpenConnection();
                    string createScript = string.Empty;
                    string errorMessages = string.Empty;
                    bool tabCreated = db.CreateTable(dt, out createScript, out errorMessages);
                    if (tabCreated == false)
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to create table: ");
                        _msg.Append(tableName);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Error Messages: ");
                        _msg.Append(errorMessages);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Create Script: ");
                        _msg.Append(createScript);
                        throw new System.Exception(_msg.ToString());
                    }
                    db.CloseConnection();
                }

                for (int dtInx = 0; dtInx < dtList.Count; dtInx++)
                {
                    _msg.Length = 0;
                    _msg.Append("List # ");
                    _msg.Append(dtInx.ToString());
                    Console.WriteLine(_msg.ToString());
                    dt = new DataTable();
                    dt.TableName = this.TableName;  //original table name should be in xml file definition
                    dt.Rows.Clear();
                    dt.ReadXml(dtList[dtInx]);
                    db.OpenConnection();
                    //set table and column names to upper case if this is an Oracle ODBC driver or OLEDB provider
                    if (isOracleOdbc || isOracleOledb)
                    {
                        dt.TableName = this.TableName.ToUpper();
                        for (int colInx = 0; colInx < dt.Columns.Count; colInx++)
                        {
                            dt.Columns[colInx].ColumnName = dt.Columns[colInx].ColumnName.ToUpper();
                        }
                    }
                    db.ImportDataFromDataTable(dt, this.OutputBatchSize);
                    db.CloseConnection();
                    dt = null;
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
            XmlSerializer ser = new XmlSerializer(typeof(DatabaseOutputProcessor));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of DatabaseTableOutputDef.</returns>
        public static DatabaseOutputProcessor LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DatabaseOutputProcessor));
            TextReader textReader = new StreamReader(filePath);
            DatabaseOutputProcessor objectInstance;
            objectInstance = (DatabaseOutputProcessor)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(DatabaseOutputProcessor));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of DatabaseTableOutputDef.</returns>
        public static DatabaseOutputProcessor LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DatabaseOutputProcessor));
            StringReader strReader = new StringReader(xmlString);
            DatabaseOutputProcessor objectInstance;
            objectInstance = (DatabaseOutputProcessor)deserializer.Deserialize(strReader);
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
