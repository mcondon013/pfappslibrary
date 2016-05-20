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
using AppGlobals;
using PFTextObjects;
using PFCollectionsObjects;
using PFDataOutputProcessor; 

namespace PFRandomDataProcessor
{
    /// <summary>
    /// Class to generator random lists of data values and their frequencies. Lists can be used to supply random data to front-end calling applications.
    /// </summary>
    /// <remarks>For example, you can use this class to generate a list of all product names in your database and the frequency with which they occur. This list 
    /// can be used to override product name values with randonm vales in a data extract where security requires that the product name values must be hidden.</remarks>
    public class CustomDataListGenerator
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();
        private PFDataImporter _importer = new PFDataImporter();

        private string[,] _dbNamesAndLocations = new string[,]{
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
        private enOutputType _outputType = enOutputType.XMLFile;
        private DatabasePlatform _dbPlatform = DatabasePlatform.Unknown;
        private string _dbPlatformDesc = "Unknown";
        private string _dbNamespace = string.Empty;
        private string _dbClassName = string.Empty;
        private string _dbDllPath = string.Empty;
        private string _connectionString = string.Empty;

        private int _maxTotalFrequencyCount = 1000;


        //constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomDataListGenerator()
        {
            ;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomDataListGenerator(DatabasePlatform dbPlatform)
        {
            InitDbDef(dbPlatform);
        }

        private void InitDbDef(DatabasePlatform dbPlatform)
        {
            int rowInx = 0;
            int maxRowInx = _dbNamesAndLocations.GetLength(0) - 1;

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
        /// Always set to enOutputType.XMLFile constant.
        /// </summary>
        public enOutputType OutputType
        {
            get
            {
                return _outputType;
            }
            set
            {
                _outputType = enOutputType.XMLFile;
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
        /// Max number of entries to generate in the output file.
        /// If sum of all frequencies is greater than this value then the total number of frequencies will be adjusted downward.
        /// </summary>
        public int MaxTotalFrequencyCount
        {
            get
            {
                return _maxTotalFrequencyCount;
            }
            set
            {
                _maxTotalFrequencyCount = value;
            }
        }



        //methods

        /// <summary>
        /// Routine to return a list of table names for the database pointed to by the current connection string.
        /// </summary>
        /// <returns>Object containing list of table definitions.</returns>
        public PFList<PFTableDef> GetTableList(string tablesToIncludePattern, string tablesToExcludePattern)
        {
            PFList<PFTableDef> tableList = null;
            PFDatabase db = null;
            string[] includePatterns = { string.Empty };
            string[] excludePatterns = { string.Empty };

            if (this.DbPlatform == DatabasePlatform.Unknown)
                return null;
            if (this.ConnectionString.Length == 0)
                return null;


            try
            {
                includePatterns[0] = tablesToIncludePattern.Trim();
                excludePatterns[0] = tablesToExcludePattern.Trim();
                db = new PFDatabase(this.DbPlatform, this.DbDllPath, this.DbNamespace + "." + this.DbClassName);
                db.ConnectionString = this.ConnectionString;
                db.OpenConnection();
                tableList = db.GetTableList(includePatterns, excludePatterns);
                db.CloseConnection();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Attempt to retrieve list of table names failed: ");
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
                 
        

            return tableList;
        }

        /// <summary>
        /// Generates a custom random value list based on the criteria supplied in the dataRequest object.
        /// </summary>
        /// <param name="dataRequest">Object containing criteria for generating random data values.</param>
        /// <param name="generateXmlFile">If true, an Xml file will be created containing the generated data values. If false, only the generateSQL and customListDataTableValues will be generated.</param>
        /// <param name="generatedSQL">Will contain the SQL statement used to generrate the random data values.</param>
        /// <param name="customListDataTableValues">DataTable containing the custom data values and their frequencies.</param>
        /// <returns>True if successful.</returns>
        public bool GenerateCustomRandomDataList(RandomCustomValuesDataRequest dataRequest, bool generateXmlFile, out string generatedSQL, out DataTable customListDataTableValues)
        {
            bool success = false;
            PFDatabase db = null;
            string sqlStatement = string.Empty;
            DataTable dt = null;
            int valueInx = 0;
            int frequencyInx = 1;
            int adjustedFrequencyInx = 2;
            int adjustmentNumberInx = 3;

            //_msg.Length = 0;
            //_msg.Append("GenerateCustomRandomDataList not yet implemented.");
            //_msg.Append(Environment.NewLine);
            //_msg.Append("+ " + dataRequest.ListName + Environment.NewLine);
            //_msg.Append("+ " + dataRequest.CustomDataListFolder + Environment.NewLine);
            //_msg.Append("+ " + dataRequest.DbPlatform.ToString() + Environment.NewLine);
            //_msg.Append("+ " + dataRequest.DbConnectionString + Environment.NewLine);
            //_msg.Append("+ " + dataRequest.DbTableName + Environment.NewLine);
            //_msg.Append("+ " + dataRequest.DbFieldName + Environment.NewLine);
            //AppMessages.DisplayAlertMessage(_msg.ToString());

            //connect to database
            //build sql statement (include adjustedfrequency and adjustmentnumber columns)
            //exec sql statement
            //get total from data table for adjustedfrequency
            //start with divide by 10 and continue adding 10 until sum of adjustedfrequency is <=1500
            //           if divide results in zero, set to 1
            //Update adjustedfrequency and adjustmentnumber on each row after sum <= 1500 found
            //write out .xml file with each value repeated ajustedfrequency times
            //save summary table to an access .rdatasum file (xml format) (or an Access .mdb or an text file (delimited or fixedlength)) 
            //save request definition to a .rdatadef file (xml format)

            this.DbPlatform = dataRequest.DbPlatform;
            this.ConnectionString = dataRequest.DbConnectionString;
            
            generatedSQL = string.Empty;
            customListDataTableValues = null;
            
            if (this.DbPlatform == DatabasePlatform.Unknown)
            {
                return false;
            }
            if (this.ConnectionString.Length == 0)
                return false;

            try
            {
                db = new PFDatabase(this.DbPlatform, this.DbDllPath, this.DbNamespace + "." + this.DbClassName);
                db.ConnectionString = this.ConnectionString;
                db.OpenConnection();
                sqlStatement = BuildSQLStatement(dataRequest);
                generatedSQL = sqlStatement;
                dt = db.RunQueryDataTable(sqlStatement, CommandType.Text);
                dt.TableName = dataRequest.ListName;

                if (dt.Rows.Count < 1)
                {
                    _msg.Length = 0;
                    _msg.Append("No rows returned from query for random data: ");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(sqlStatement);
                    _msg.Append(Environment.NewLine);
                    //AppMessages.DisplayWarningMessage(_msg.ToString());
                    throw new System.Exception(_msg.ToString());                    
                }

                AdjustFrequencies(ref dt, valueInx, frequencyInx, adjustedFrequencyInx, adjustmentNumberInx);
                if (generateXmlFile)
                {
                    OutputRandomDataFile(dataRequest, ref dt, sqlStatement, valueInx, frequencyInx, adjustedFrequencyInx, adjustmentNumberInx);
                }

                customListDataTableValues = dt;

                db.CloseConnection();
                success = true;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append("Error occurred while attempting to create custom random data file: ");
                _msg.Append(Environment.NewLine);
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

        private string BuildSQLStatement(RandomCustomValuesDataRequest dataRequest)
        {
            StringBuilder sqlStatement = new StringBuilder();
            string condition = string.Empty;
            string criteria = string.Empty;
            sqlStatement.Length = 0;
            sqlStatement.Append("select ");
            sqlStatement.Append(dataRequest.DbFieldName);
            sqlStatement.Append(", ");
            sqlStatement.Append("count(*) as Frequency, count(*) as AdjustedFrequency, 1 as AdjustmentNumber ");
            sqlStatement.Append(Environment.NewLine);
            sqlStatement.Append("  from ");
            sqlStatement.Append(dataRequest.DbTableName);
            sqlStatement.Append(" ");
            sqlStatement.Append(Environment.NewLine);
            if (dataRequest.SelectionField.Trim() != string.Empty && dataRequest.SelectionField != "<value frequency>")
            {
                if (dataRequest.SelectionCondition.Trim() != string.Empty && dataRequest.SelectionCriteria.Trim() != string.Empty)
                {
                    sqlStatement.Append(" where ");
                    sqlStatement.Append(dataRequest.SelectionField.Trim());
                    sqlStatement.Append(" ");
                    switch (dataRequest.SelectionCondition.Trim())
                    {
                        case "Equal To":
                            condition = "=";
                            break;
                        case "Greater Than":
                            condition = ">";
                            break;
                        case "Less Than":
                            condition = "<";
                            break;
                        case "In":
                            condition = "in";
                            break;
                        case "Like":
                            condition = "like";
                            break;
                        case "Not Equal To":
                            condition = "!=";
                            break;
                        case "Not Greater Than":
                            condition = "<=";
                            break;
                        case "Not Less Than":
                            condition = ">=";
                            break;
                        case "Not In":
                            condition = "not in";
                            break;
                        case "Not Like":
                            condition = "not like";
                            break;
                        default:
                            condition = "=";
                            break;
                    }
                    sqlStatement.Append(condition);
                    sqlStatement.Append(" ");
                    criteria = dataRequest.SelectionCriteria.Replace("\"","'");
                    if (dataRequest.SelectionCondition == "In" || dataRequest.SelectionCondition == "Not In")
                    {
                        if(dataRequest.SelectionCriteria.StartsWith("(") == false)
                        {
                            sqlStatement.Append("(");
                        }
                        else
                        {
                            ; //let it be. User has already supplied the required parentheses.
                        }

                        sqlStatement.Append(criteria);

                        if(dataRequest.SelectionCriteria.StartsWith("(") == false)
                        {
                            sqlStatement.Append(")");
                        }
                        else
                        {
                            ;  //let it be. User has already supplied the required parentheses.
                        }
                    }
                    else
                    {
                        sqlStatement.Append(criteria);
                    }
                    sqlStatement.Append(" ");
                    sqlStatement.Append(Environment.NewLine);
                }
            }
            sqlStatement.Append(" group by ");
            sqlStatement.Append(dataRequest.DbFieldName);
            sqlStatement.Append(" ");
            sqlStatement.Append(Environment.NewLine);
            if (dataRequest.MinimumValueFrequency > 1)
            {
                sqlStatement.Append(" having count(*) >= ");
                sqlStatement.Append(dataRequest.MinimumValueFrequency.ToString());
                sqlStatement.Append(" ");
                sqlStatement.Append(Environment.NewLine);
            }
            if (dataRequest.DbPlatform == DatabasePlatform.MSAccess)
            {
                sqlStatement.Append(" order by count(*) desc ");
            }
            else
            {
                sqlStatement.Append(" order by AdjustedFrequency desc ");
            }


            return sqlStatement.ToString();
        }

        private void AdjustFrequencies(ref DataTable dt, int valueInx, int frequencyInx,  int adjustedFrequencyInx, int adjustmentNumberInx)
        {
            int totalFrequencyCount = 0;
            int valueFrequency = 0;
            int adjustmentNumber = 1;
            int adjustedTotalFrequencyCount = 0;
            int adjustedFrequency = 0;
            int frequency = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                valueFrequency = PFTextProcessor.ConvertStringToInt(dt.Rows[i][adjustedFrequencyInx].ToString());
                totalFrequencyCount += valueFrequency;
            }

            if (totalFrequencyCount > _maxTotalFrequencyCount)
            {
                adjustmentNumber = 1;
                adjustedTotalFrequencyCount = totalFrequencyCount;
                while (adjustedTotalFrequencyCount > _maxTotalFrequencyCount)
                {
                    if(adjustmentNumber < 10)
                        adjustmentNumber += 1;
                    else if (adjustmentNumber < 100)
                        adjustmentNumber += 10;
                    else if (adjustmentNumber < 2000)
                        adjustmentNumber += 100;
                    else if (adjustmentNumber < 30000)
                        adjustmentNumber += 1000;
                    else if (adjustmentNumber < 70000)
                        adjustmentNumber += 2000;
                    else if (adjustmentNumber < 100000)
                        adjustmentNumber += 3000;
                    else if (adjustmentNumber < 150000)
                        adjustmentNumber += 4000;
                    else if (adjustmentNumber < 250000)
                        adjustmentNumber += 5000;
                    else if (adjustmentNumber < 300000)
                        adjustmentNumber += 6000;
                    else if (adjustmentNumber < 350000)
                        adjustmentNumber += 7000;
                    else if (adjustmentNumber < 450000)
                        adjustmentNumber += 8000;
                    else if (adjustmentNumber < 500000)
                        adjustmentNumber += 9000;
                    else
                        adjustmentNumber += 10000;
                    adjustedTotalFrequencyCount = totalFrequencyCount / adjustmentNumber;
                }
            }

            foreach (DataColumn col in dt.Columns)
            {
                col.ReadOnly = false;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                frequency = Convert.ToInt32(dt.Rows[i][frequencyInx].ToString());
                adjustedFrequency = frequency / adjustmentNumber;
                dt.Rows[i][adjustedFrequencyInx] = adjustedFrequency == 0 ? 1 : adjustedFrequency;
                dt.Rows[i][adjustmentNumberInx] = adjustmentNumber;
            }

        }

        private void OutputRandomDataFile(RandomCustomValuesDataRequest dataRequest, ref DataTable dt, string sqlStatement, int valueInx, int frequencyInx, int adjustedFrequencyInx, int adjustmentNumberInx)
        {
            string randomDataFileName = Path.Combine(dataRequest.ListFolder, dataRequest.ListName + ".xml");
            string randomDataSummaryFile = Path.Combine(dataRequest.ListFolder, dataRequest.ListName + ".clistsum");
            string randomDataDefinition = Path.Combine(dataRequest.ListFolder, dataRequest.ListName + ".clistdef");
            string randomDataSQLStatementFile = Path.Combine(dataRequest.ListFolder, dataRequest.ListName + ".sql");
            PFList<string> randomDataList = new PFList<string>();
            int frequency = 0;

            dt.WriteXml(randomDataSummaryFile, XmlWriteMode.WriteSchema);
            dataRequest.SaveToXmlFile(randomDataDefinition);
            System.IO.File.WriteAllText(randomDataSQLStatementFile, sqlStatement);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                frequency = Convert.ToInt32(dt.Rows[i][adjustedFrequencyInx].ToString());
                for (int k = 0; k < frequency; k++)
                {
                    randomDataList.Add(dt.Rows[i][valueInx].ToString());
                }
            }

            randomDataList.SaveToXmlFile(randomDataFileName);
        }

        //class helpers

        /// <summary>
        /// Saves the public property values contained in the current instance to the specified file. Serialization is used for the save.
        /// </summary>
        /// <param name="filePath">Full path for output file.</param>
        public void SaveToXmlFile(string filePath)
        {
            XmlSerializer ser = new XmlSerializer(typeof(CustomDataListGenerator));
            TextWriter tex = new StreamWriter(filePath);
            ser.Serialize(tex, this);
            tex.Close();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance from a file.
        /// </summary>
        /// <param name="filePath">Full path for the input file.</param>
        /// <returns>An instance of CustomDataListGenerator.</returns>
        public static CustomDataListGenerator LoadFromXmlFile(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CustomDataListGenerator));
            TextReader textReader = new StreamReader(filePath);
            CustomDataListGenerator objectInstance;
            objectInstance = (CustomDataListGenerator)deserializer.Deserialize(textReader);
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
            XmlSerializer ser = new XmlSerializer(typeof(CustomDataListGenerator));
            StringWriter tex = new StringWriter();
            ser.Serialize(tex, this);
            return tex.ToString();
        }

        /// <summary>
        /// Creates and initializes an instance of the class by loading a serialized version of the instance stored as a xml formatted string.
        /// </summary>
        /// <param name="xmlString">String containing the xml formatted representation of an instance of this class.</param>
        /// <returns>An instance of CustomDataListGenerator.</returns>
        public static CustomDataListGenerator LoadFromXmlString(string xmlString)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(CustomDataListGenerator));
            StringReader strReader = new StringReader(xmlString);
            CustomDataListGenerator objectInstance;
            objectInstance = (CustomDataListGenerator)deserializer.Deserialize(strReader);
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
