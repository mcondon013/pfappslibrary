using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Data;
using System.Data.Common;
using PFProcessObjects;
using PFMessageLogs;
using PFDataAccessObjects;
using PFTimers;

namespace TestprogLoadAWDW
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";

        private string _helpFilePath = string.Empty;

        private struct stTableInfo
        {
            public string tableName;
            public int numRows;

            public stTableInfo(string pTableName, int pNumRows)
            {
                tableName = pTableName;
                numRows = pNumRows;
            }
        }

        private stTableInfo[] _tableInfo =
                                        {
                                        new stTableInfo("DimAccount", 99),
                                        new stTableInfo("DimCurrency", 105),
                                        new stTableInfo("DimCustomer", 18484),
                                        new stTableInfo("DimDate", 1188),
                                        new stTableInfo("DimDepartmentGroup", 7),
                                        new stTableInfo("DimEmployee", 296),
                                        new stTableInfo("DimGeography", 655),
                                        new stTableInfo("DimOrganization", 14),
                                        new stTableInfo("DimProduct", 606),
                                        new stTableInfo("DimProductCategory", 4),
                                        new stTableInfo("DimProductSubcategory", 37),
                                        new stTableInfo("DimPromotion", 16),
                                        new stTableInfo("DimReseller", 701),
                                        new stTableInfo("DimSalesReason", 10),
                                        new stTableInfo("DimSalesTerritory", 11),
                                        new stTableInfo("DimScenario", 3),
                                        new stTableInfo("FactAdditionalInternationalProductDescription", 15168),
                                        new stTableInfo("FactCallCenter", 120),
                                        new stTableInfo("FactCurrencyRate", 14264),
                                        new stTableInfo("FactFinance", 39409),
                                        new stTableInfo("FactInternetSales", 60398),
                                        new stTableInfo("FactResellerSales", 60855),
                                        new stTableInfo("FactInternetSalesReason", 64515),
                                        new stTableInfo("FactSalesQuota", 163),
                                        new stTableInfo("FactSurveyResponse", 2727),
                                        new stTableInfo("ProspectiveBuyer", 2059),
                                        new stTableInfo("AdventureWorksDWBuildVersion", 1)
                                        };
        private string _sourceQuery = @"select * from dbo.<tablename>";


        //properties
        public bool SaveErrorMessagesToAppLog
        {
            get
            {
                return _saveErrorMessagesToAppLog;
            }
            set
            {
                _saveErrorMessagesToAppLog = value;
            }
        }

        /// <summary>
        /// Message log window manager.
        /// </summary>
        public MessageLog MessageLogUI
        {
            get
            {
                return _messageLog;
            }
            set
            {
                _messageLog = value;
            }
        }

        /// <summary>
        /// Path to application help file.
        /// </summary>
        public string HelpFilePath
        {
            get
            {
                return _helpFilePath;
            }
            set
            {
                _helpFilePath = value;
            }
        }


        //application routines

        /// <summary>
        /// Routine to add context menu item for pfFolderSize to Windows Explorer.
        /// </summary>
        public void ShowAppConfigManager()
        {
            PFProcess proc = new PFProcess();
            string currAppFolder = AppInfo.CurrentEntryAssemblyDirectory;
            string currAppExePath = AppInfo.CurrentEntryAssembly;
            string appConfigManagerApp = Path.Combine(currAppFolder, _appConfigManagerExe);
            string mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool appConfigManagerFound = false;

            try
            {
                proc.Arguments = "\"" + currAppExePath + "\" \"" + _helpFilePath + "\" " + "\"Change an Application Setting\"";

                if (File.Exists(appConfigManagerApp))
                {
                    appConfigManagerFound = true;
                    proc.WorkingDirectory = currAppFolder;
                    proc.ExecutableToRun = appConfigManagerApp;
                }
                else
                {
                    string configValue = AppConfig.GetStringValueFromConfigFile("appConfigManagerPath", string.Empty);
                    if (configValue.Length > 0)
                    {
                        if (File.Exists(configValue))
                        {
                            appConfigManagerFound = true;
                            proc.WorkingDirectory = Path.GetDirectoryName(configValue);
                            proc.ExecutableToRun = configValue;
                        }
                        else
                        {
                            appConfigManagerFound = false;
                        }
                    }
                    else
                    {
                        appConfigManagerFound = false;

                    }
                }
                if (appConfigManagerFound == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find Application Configuration Manager Application in current app folder: ");
                    _msg.Append(currAppFolder);
                    throw new System.Exception(_msg.ToString());
                }
                proc.CreateNoWindow = true;
                proc.UseShellExecute = true;
                proc.WindowStyle = PFProcessWindowStyle.Normal;
                proc.RedirectStandardOutput = false;
                proc.RedirectStandardError = false;
                proc.CheckIfProcessWaitingForInput = false;
                proc.MaxProcessRunSeconds = (int)0;

                proc.Run();

                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                proc = null;
            }

        }


        public void CopyAdventureWorksDW(DatabasePlatform sourceDbPlatform, string sourceConnection,
                                         DatabasePlatform destinationDbPlatorm, string destinationConnection,
                                         bool replaceExistingTables, int batchSizeForDataWrites, string outputTablesSchema)
        {
            PFDatabase sourceDb = null;
            PFDatabase destinationDb = null;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("CopyAdventureWorksDW started ...\r\n");
                Program._messageLog.WriteLine(_msg.ToString());

                sw.Start();

                sourceDb = GetDbObject(sourceDbPlatform, sourceConnection);
                if (sourceDb.IsConnected == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to connect to source datatabase ");
                    _msg.Append(sourceDbPlatform.ToString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Connection String: ");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(sourceConnection);
                    throw new System.Exception(_msg.ToString());
                }

                destinationDb = GetDbObject(destinationDbPlatorm, destinationConnection);
                if (destinationDb.IsConnected == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to connect to destination datatabase ");
                    _msg.Append(destinationDbPlatorm.ToString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Connection String: ");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(destinationConnection);
                    throw new System.Exception(_msg.ToString());
                }

                _msg.Length = 0;
                _msg.Append("Time to connect to databases: ");
                _msg.Append(sw.FormattedElapsedTime);
                Program._messageLog.WriteLine(_msg.ToString());

                CopyTables(sourceDb, destinationDb, replaceExistingTables, batchSizeForDataWrites, outputTablesSchema);


            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if (sourceDb.IsConnected)
                {
                    sourceDb.CloseConnection();
                }
                sourceDb = null;

                if (destinationDb.IsConnected)
                {
                    destinationDb.CloseConnection();
                }
                destinationDb = null;

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("\r\n... CopyAdventureWorksDW finished.");
                Program._messageLog.WriteLine(_msg.ToString());
                _msg.Length = 0;
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed copy time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }

        private PFDatabase GetDbObject(DatabasePlatform dbPlatform, string dbConnectionString)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            try
            {
                dbPlatformDesc = dbPlatform.ToString();
                connStr = dbConnectionString;

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }

            return db;
        }

        private void CopyTables(PFDatabase sourceDb, PFDatabase destinationDb, bool replaceExistingTables, int batchSizeForDataWrites, string outputTablesSchema)
        {
            string sourceQuery = string.Empty;
            string destinationTableName = string.Empty;
            int numTableCreateErrors = 0;
            int numImportErrors = 0;
            Stopwatch sw = new Stopwatch();

            try
            {
                if (sourceDb.DbPlatform == DatabasePlatform.SQLServerCE35)
                    sourceQuery = _sourceQuery.Replace("dbo.", string.Empty);
                else
                    sourceQuery = _sourceQuery;

                _msg.Length = 0;
                _msg.Append("Output Database Platform: ");
                _msg.Append(destinationDb.DbPlatform.ToString());
                _msg.Append(Environment.NewLine);
                Program._messageLog.WriteLine(_msg.ToString());

                
                sw.Start();
                foreach (stTableInfo tabInfo in _tableInfo)
                {
                    //load data from source database into DataTable
                    string qry = sourceQuery.Replace(@"<tablename>", tabInfo.tableName);
                    string configValue = AppConfig.GetStringValueFromConfigFile(destinationDb.DbPlatform.ToString() + "_" + tabInfo.tableName, string.Empty);
                    if (configValue.Length > 0)
                        qry = configValue;
                    sourceDb.SQLQuery = qry; 
                    sourceDb.CommandType = CommandType.Text;
                    DataTable dt = sourceDb.RunQueryDataTable();
                    _msg.Length = 0;
                    _msg.Append("Query text: ");
                    _msg.Append(sourceDb.SQLQuery);
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Expected number of rows: ");
                    _msg.Append(tabInfo.numRows.ToString("#,##0"));
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Number of rows returned: ");
                    _msg.Append(dt.Rows.Count.ToString("#,##0"));
                    Program._messageLog.WriteLine(_msg.ToString());

                    //import data from DataTable into destination database
                    if (outputTablesSchema.Trim().Length > 0)
                        destinationTableName = outputTablesSchema + "." + tabInfo.tableName;
                    else
                        destinationTableName = tabInfo.tableName;

                    //workaround to fix issue with Oracle limit on identifier lengths
                    if (destinationDb.DbPlatform == DatabasePlatform.OracleNative || destinationDb.DbPlatform == DatabasePlatform.MSOracle)
                    {
                        if (tabInfo.tableName == "FactAdditionalInternationalProductDescription")
                        {
                            if (outputTablesSchema.Trim().Length > 0)
                                destinationTableName = outputTablesSchema + "." + "FactProductDescriptionExt";
                            else
                                destinationTableName = "FactProductDescriptionExt";
                        }
                    }
                    
                    dt.TableName = destinationTableName;

                    _msg.Length = 0;
                    _msg.Append(destinationTableName);
                    if (replaceExistingTables)
                    {
                        if (destinationDb.TableExists(destinationTableName))
                        {
                            destinationDb.DropTable(destinationTableName);
                            if (destinationDb.TableExists(destinationTableName) == false)
                                _msg.Append(" dropped.");
                            else
                                _msg.Append(" drop failed.");
                        }
                        else
                        {
                            _msg.Append(" does not exist.");
                        }
                    }
                    else
                    {
                        _msg.Append(": No check was made to determine if table already existed.");
                    }

                    Program._messageLog.WriteLine(_msg.ToString());

                    Program._messageLog.WriteLine("Creating table in the database ...");

                    //create the table
                    string createScript = string.Empty;
                    string errorMessages = string.Empty;
                    bool createSucceeded = true;
                    createSucceeded = destinationDb.CreateTable(dt, out createScript, out errorMessages);

                    _msg.Length = 0;
                    _msg.Append("Create table result: ");
                    _msg.Append(createSucceeded.ToString());
                    _msg.Append(Environment.NewLine);
                    if (errorMessages.Trim().Length > 0)
                    {
                        _msg.Append("Error Messages: ");
                        _msg.Append(Environment.NewLine);
                        _msg.Append(errorMessages);
                        _msg.Append(Environment.NewLine);
                    }
                    _msg.Append("Create table statement: ");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(createScript);
                    _msg.Append(Environment.NewLine);
                    Program._messageLog.WriteLine(_msg.ToString());

                    if (createSucceeded == false)
                    {
                        numTableCreateErrors++;
                    }
                    else
                    {
                        Program._messageLog.WriteLine("Importing data table to the database ...");
                        _msg.Length = 0;
                        _msg.Append("Data Write batch size: ");
                        _msg.Append(batchSizeForDataWrites.ToString());
                        Program._messageLog.WriteLine(_msg.ToString());

                        try
                        {
                            if (batchSizeForDataWrites == 1)
                                destinationDb.ImportDataFromDataTable(dt);
                            else
                                destinationDb.ImportDataFromDataTable(dt, batchSizeForDataWrites);

                            _msg.Length = 0;
                            _msg.Append("Table imported: ");
                            _msg.Append(dt.TableName);
                            _msg.Append(Environment.NewLine);
                            Program._messageLog.WriteLine(_msg.ToString());
                        }
                        catch (System.Exception ex)
                        {
                            numImportErrors++;
                            _msg.Length = 0;
                            _msg.Append("ERROR: Data import failed:");
                            _msg.Append(Environment.NewLine);
                            _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                            Program._messageLog.WriteLine(_msg.ToString());
                        }
                        finally
                        {
                            ;
                        }

                    }


                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                if (numTableCreateErrors > 0)
                {
                    _msg.Append("One or more table create errors were reported: ");
                    _msg.Append(numTableCreateErrors.ToString());
                    _msg.Append(Environment.NewLine);
                }
                if (numImportErrors > 0)
                {
                    _msg.Append("One or more data import errors were reported: ");
                    _msg.Append(numImportErrors.ToString());
                    _msg.Append(Environment.NewLine);
                }
                if (_msg.Length > 0)
                {
                    Program._messageLog.WriteLine(_msg.ToString());
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                }
            }
                 
        
        }


    }//end class
}//end namespace
