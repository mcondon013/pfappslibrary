using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Data.Common;
using PFProcessObjects;
using PFSystemObjects;
using PFMessageLogs;
using pfDataExtractorCPObjects;
using PFAppDataObjects;
using PFAppDataForms;
using PFRandomDataProcessor;
using PFRandomDataForms;
using PFRandomValueDataTables;
using PFTextFiles;
using PFTextObjects;
using PFCollectionsObjects;
using PFConnectionObjects;
using PFConnectionStrings;
using PFProviderForms;
using PFSQLBuilderObjects;
using PFDataAccessObjects;
using PFDataOutputProcessor;
using PFDataOutputGrid;
using PFDocumentObjects;
using PFDocumentGlobals;
using PFTimers;
using PFFileSystemObjects;
using PFRandomDataExt;

namespace pfDataExtractorCPProcesxor
{
    /// <summary>
    /// Class contains routines that support the data import and export operations of pfDataExtractorCP.
    /// </summary>
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog = null;
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";
        private string _appConnectionStringManagerExe = @"pfConnectionStringManager.exe";
        //private string _appRandomDataManagerExe = @"pfRandomDataSources.exe";
        private string _helpFilePath = string.Empty;
        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\DataExports\";
        private int _expectedLineLength = -1;
        private string _testOrdersDatabase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\TestOrders.sdf";
        private string _initTestOrdersDatabaseZipFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\pfApps\RandomData\TestOrders.zip";
        private int _maxTempDataTableRows = 50000;
        private bool _runExtractInBatchMode = false;
        private string _defaultDataExtractorDefsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\ExtractorDefs\";
        private string _defaultDataExtactorLogsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\ExtractorLogs\";
        private int _batchSizeForRandomDataGeneration = 50000;
        private int _batchSizeForDataImportsAndExports = 200;

        private string _salesOrderHeaderQuery = ""
                                              + "SELECT [RowNumber]"
                                              + "      ,[SalesOrderID]"
                                              + "      ,[RevisionNumber]"
                                              + "      ,[OrderDate]"
                                              + "      ,[DueDate]"
                                              + "      ,[ShipDate]"
                                              + "      ,[Status]"
                                              + "      ,[OnlineOrderFlag]"
                                              + "      ,[SalesOrderNumber]"
                                              + "      ,[PurchaseOrderNumber]"
                                              + "      ,[AccountNumber]"
                                              + "      ,[CustomerID]"
                                              + "      ,[SalesPersonID]"
                                              + "      ,[TerritoryID]"
                                              + "      ,[BillToAddressID]"
                                              + "      ,[ShipToAddressID]"
                                              + "      ,[ShipMethodID]"
                                              + "      ,[CreditCardID]"
                                              + "      ,[CreditCardApprovalCode]"
                                              + "      ,[CurrencyRateID]"
                                              + "      ,[SubTotal]"
                                              + "      ,[TaxAmt]"
                                              + "      ,[Freight]"
                                              + "      ,[TotalDue]"
                                              + "      ,[SalesComment]"
                                              + "      ,[rowguid]"
                                              + "      ,[ModifiedDate]"
                                              + "  FROM [SalesOrderHeader]";

        private string _salesOrderDetailQuery = ""
                                              + "SELECT [RowNumber]"
                                              + "      ,[SalesOrderID]"
                                              + "      ,[SalesOrderDetailID]"
                                              + "      ,[CarrierTrackingNumber]"
                                              + "      ,[OrderQty]"
                                              + "      ,[ProductID]"
                                              + "      ,[SpecialOfferID]"
                                              + "      ,[UnitPrice]"
                                              + "      ,[UnitPriceDiscount]"
                                              + "      ,[LineTotal]"
                                              + "      ,[rowguid]"
                                              + "      ,[ModifiedDate]"
                                              + "  FROM [SalesOrderDetail]";

        private string _purchaseOrderHeaderQuery = ""
                                                 + "SELECT [RowNumber]"
                                                 + "      ,[PurchaseOrderID]"
                                                 + "      ,[RevisionNumber]"
                                                 + "      ,[Status]"
                                                 + "      ,[EmployeeID]"
                                                 + "      ,[VendorID]"
                                                 + "      ,[ShipMethodID]"
                                                 + "      ,[OrderDate]"
                                                 + "      ,[ShipDate]"
                                                 + "      ,[SubTotal]"
                                                 + "      ,[TaxAmt]"
                                                 + "      ,[Freight]"
                                                 + "      ,[TotalDue]"
                                                 + "      ,[ModifiedDate]"
                                                 + "  FROM [PurchaseOrderHeader]";

        private string _purchaseOrderDetailQuery = ""
                                                 + "SELECT [RowNumber]"
                                                 + "      ,[PurchaseOrderID]"
                                                 + "      ,[PurchaseOrderDetailID]"
                                                 + "      ,[DueDate]"
                                                 + "      ,[OrderQty]"
                                                 + "      ,[ProductID]"
                                                 + "      ,[UnitPrice]"
                                                 + "      ,[LineTotal]"
                                                 + "      ,[ReceivedQty]"
                                                 + "      ,[RejectedQty]"
                                                 + "      ,[StockedQty]"
                                                 + "      ,[ModifiedDate]"
                                                 + "  FROM [PurchaseOrderDetail]";
        private string _factResellerSalesQuery = ""
                                               + "SELECT [RowNumber]"
                                               + "      ,[ProductKey]"
                                               + "      ,[OrderDateKey]"
                                               + "      ,[DueDateKey]"
                                               + "      ,[ShipDateKey]"
                                               + "      ,[ResellerKey]"
                                               + "      ,[EmployeeKey]"
                                               + "      ,[PromotionKey]"
                                               + "      ,[CurrencyKey]"
                                               + "      ,[SalesTerritoryKey]"
                                               + "      ,[SalesOrderNumber]"
                                               + "      ,[SalesOrderLineNumber]"
                                               + "      ,[RevisionNumber]"
                                               + "      ,[OrderQuantity]"
                                               + "      ,[UnitPrice]"
                                               + "      ,[ExtendedAmount]"
                                               + "      ,[UnitPriceDiscountPct]"
                                               + "      ,[DiscountAmount]"
                                               + "      ,[ProductStandardCost]"
                                               + "      ,[TotalProductCost]"
                                               + "      ,[SalesAmount]"
                                               + "      ,[TaxAmt]"
                                               + "      ,[Freight]"
                                               + "      ,[CarrierTrackingNumber]"
                                               + "      ,[CustomerPONumber]"
                                               + "  FROM [FactResellerSales]";

        private string _factResellerSalesQueryMaster = ""
                                                     + "SELECT [RowNumber]"
                                                     + "      ,[SalesOrderNumber]"
                                                     + "  FROM [FactResellerSalesMaster]";

        private string _factInternetSalesQuery = ""
                                               + "SELECT [RowNumber]"
                                               + "      ,[ProductKey]"
                                               + "      ,[OrderDateKey]"
                                               + "      ,[DueDateKey]"
                                               + "      ,[ShipDateKey]"
                                               + "      ,[CustomerKey]"
                                               + "      ,[PromotionKey]"
                                               + "      ,[CurrencyKey]"
                                               + "      ,[SalesTerritoryKey]"
                                               + "      ,[SalesOrderNumber]"
                                               + "      ,[SalesOrderLineNumber]"
                                               + "      ,[RevisionNumber]"
                                               + "      ,[OrderQuantity]"
                                               + "      ,[UnitPrice]"
                                               + "      ,[ExtendedAmount]"
                                               + "      ,[UnitPriceDiscountPct]"
                                               + "      ,[DiscountAmount]"
                                               + "      ,[ProductStandardCost]"
                                               + "      ,[TotalProductCost]"
                                               + "      ,[SalesAmount]"
                                               + "      ,[TaxAmt]"
                                               + "      ,[Freight]"
                                               + "      ,[CarrierTrackingNumber]"
                                               + "      ,[CustomerPONumber]"
                                               + "  FROM [FactInternetSales]";

        private string _factInternetSalesQueryMaster = ""
                                                     + "SELECT [RowNumber]"
                                                     + "      ,[SalesOrderNumber]"
                                                     + "  FROM [FactInternetSalesMaster]";


        private struct stMasterDetailGeneratorResult
        {
            public int NumMasterRowsGenerated;
            public int NumDetailRowsGenerated;

            public stMasterDetailGeneratorResult(int numMasterRows, int numDetailRows)
            {
                NumMasterRowsGenerated = numMasterRows;
                NumDetailRowsGenerated = numDetailRows;
            }
        }

        private enum enTestOrdersType
        {
            Unknown = 0,
            SalesOrder = 1,
            PurchaseOrder = 2,
            InternetSales = 3,
            ResellerSales = 4
        }
        
        //properties

        /// <summary>
        /// Set to true to save any error messages to the default application log file.
        /// </summary>
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

        /// <summary>
        /// ExpectedLineLength Property.
        /// </summary>
        public int ExpectedLineLength
        {
            get
            {
                return _expectedLineLength;
            }
            set
            {
                _expectedLineLength = value;
            }
        }

        /// <summary>
        /// RunExtractInBatchMode Property.
        /// </summary>
        public bool RunExtractInBatchMode
        {
            get
            {
                return _runExtractInBatchMode;
            }
            set
            {
                _runExtractInBatchMode = value;
            }
        }

        /// <summary>
        /// DefaultDataExtractorDefsFolder Property.
        /// </summary>
        public string DefaultDataExtractorDefsFolder
        {
            get
            {
                return _defaultDataExtractorDefsFolder;
            }
            set
            {
                _defaultDataExtractorDefsFolder = value;
            }
        }

        /// <summary>
        /// BatchSizeForRandomDataGeneration Property.
        /// </summary>
        public int BatchSizeForRandomDataGeneration
        {
            get
            {
                return _batchSizeForRandomDataGeneration;
            }
            set
            {
                _batchSizeForRandomDataGeneration = value;
            }
        }

        /// <summary>
        /// BatchSizeForDataImportsAndExports Property.
        /// </summary>
        public int BatchSizeForDataImportsAndExports
        {
            get
            {
                return _batchSizeForDataImportsAndExports;
            }
            set
            {
                _batchSizeForDataImportsAndExports = value;
            }
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        public PFAppProcessor()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            string configValue = string.Empty;

            if (Directory.Exists(_initDataGridExportFolder) == false)
                Directory.CreateDirectory(_initDataGridExportFolder);

            _maxTempDataTableRows = AppConfig.GetIntValueFromConfigFile("MaxTempDataTableRows", 50000);
            //override if below minimum value
            if (_maxTempDataTableRows < 5000)
                _maxTempDataTableRows = 5000;

            configValue = AppConfig.GetStringValueFromConfigFile("TestOrdersDatabase", string.Empty);
            if (configValue != string.Empty)
            {
                if(File.Exists(configValue))
                {
                    _testOrdersDatabase = configValue;
                }
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
                proc.RedirectStandardInput = false;
                proc.CheckIfProcessWaitingForInput = false;
                proc.MaxProcessRunSeconds = (int)0;

                proc.Run();

                System.Configuration.ConfigurationManager.RefreshSection("appSettings");

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                proc = null;
            }

        }//end method

        /// <summary>
        /// Displays form for defining and verifying database connection strings.
        /// </summary>
        public void ShowConnectionStringManagerForm()
        {
            PFProcess proc = new PFProcess();
            string currAppFolder = AppInfo.CurrentEntryAssemblyDirectory;
            string currAppExePath = AppInfo.CurrentEntryAssembly;
            string appConnectionStringManagerApp = Path.Combine(currAppFolder, _appConnectionStringManagerExe);
            string mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool appConnectionStringManagerFound = false;

            try
            {
                //proc.Arguments = currAppExePath + " " + _helpFilePath + " " + "\"Manage Connection Strings\"";
                proc.Arguments = string.Empty;

                if (File.Exists(appConnectionStringManagerApp))
                {
                    appConnectionStringManagerFound = true;
                    proc.WorkingDirectory = currAppFolder;
                    proc.ExecutableToRun = appConnectionStringManagerApp;
                }
                else
                {
                    string configValue = AppConfig.GetStringValueFromConfigFile("appConnectionStringManagerPath", string.Empty);
                    if (configValue.Length > 0)
                    {
                        if (File.Exists(configValue))
                        {
                            appConnectionStringManagerFound = true;
                            proc.WorkingDirectory = Path.GetDirectoryName(configValue);
                            proc.ExecutableToRun = configValue;
                        }
                        else
                        {
                            appConnectionStringManagerFound = false;
                        }
                    }
                    else
                    {
                        appConnectionStringManagerFound = false;

                    }
                }
                if (appConnectionStringManagerFound == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find Connection String Manager Application in current app folder: ");
                    _msg.Append(currAppFolder);
                    throw new System.Exception(_msg.ToString());
                }
                proc.CreateNoWindow = true;
                proc.UseShellExecute = true;
                proc.WindowStyle = PFProcessWindowStyle.Normal;
                proc.RedirectStandardOutput = false;
                proc.RedirectStandardError = false;
                proc.RedirectStandardInput = false;
                proc.CheckIfProcessWaitingForInput = false;
                proc.MaxProcessRunSeconds = (int)0;

                proc.Run();


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

        /// <summary>
        /// Routine to generate output based on the current extractor source definition. Data is displayed in a DataGridView form.
        /// </summary>
        /// <param name="dataLocation">Type of database for the source.</param>
        /// <param name="exdef">Extractor definition file.</param>
        /// <param name="maxPreviewRows">Maximum number of rows to show in the output grid.</param>
        /// <param name="showRowNumber">Set to true to add a row number column to the output.</param>
        /// <param name="filterOutput">Set to true if output is be filtered before filling the data grid.</param>
        /// <param name="randomizeOutput">Set to true if one or more columns in the output is to be randomized.</param>
        /// <param name="randomizerBatchSize">Specifies number of random values to generate each time a new set of values the application can use is produced.</param>
        /// <param name="importExportBatchsize">Number of data rows to read and write as a single batch. This number will be overridden by the batch size limits for individual database providers.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Set this to true if you want database platform drop-down lists to only include data providers that are installed on the current system. If you decide to include a provider not installed on the system, you will almost certainly get an error when attempting to use the provider.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing output from data grid exports.</param>
        /// <param name="defaultOutputDatabaseType">Default database platform to display when form with a data destination Database Type drop-down list is first shown.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use when the Default Output Database Type for a data destination is set on one of the application’s data destination drop-down lists.</param>
        public void PreviewData(enExtractorDataLocation dataLocation, 
                                PFExtractorDefinition exdef, 
                                int maxPreviewRows,
                                bool showRowNumber, 
                                bool filterOutput,
                                bool randomizeOutput,
                                int randomizerBatchSize,
                                int importExportBatchsize,
                                bool showInstalledDatabaseProvidersOnly,
                                string defaultDataGridExportFolder,
                                string defaultOutputDatabaseType,
                                string defaultOutputDatabaseConnectionString)
        {
            DataTable dt = null;

            try
            {
                switch (dataLocation)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        dt = GetDataTableForRelationDatabase(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        dt = GetDataTableForAccessDatabaseFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.ExcelDataFile:
                        dt = GetDataTableForExcelDataFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        dt = GetDataTableForDelimitedTextFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        dt = GetDataTableForFixedLengthTextFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.XMLFile:
                        dt = GetDataTableForXmlFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    default:
                        dt = null;
                        _msg.Length = 0;
                        _msg.Append("PreviewData not yet implemented for unknown data location.");
                        WriteMessageToLog(_msg.ToString());
                        DisplayAppAlertMessage(_msg.ToString());
                        break;
                }

                if (dt != null)
                {
                    OutputDataTableToGrid(dt,
                                          maxPreviewRows,
                                          showRowNumber,
                                          filterOutput,
                                          randomizeOutput,
                                          showInstalledDatabaseProvidersOnly,
                                          defaultDataGridExportFolder,
                                          defaultOutputDatabaseType,
                                          defaultOutputDatabaseConnectionString
                                         );
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }
        
        }

        private DataTable GetDataTableForRelationDatabase(PFExtractorDefinition exDef, bool showRowNumber, bool filterOutput, bool randomizeOutput, int randomDataBatchSize)
        {
            DataTable dt = null;
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();
            PFRelationalDatabaseSource relDbSource = exDef.RelationalDatabaseSource;

            try
            {
                _msg.Length = 0;
                _msg.Append("GetDataTableForRelationDatabase started at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());

                sw.Start();

                if (relDbSource.DbPlatform == DatabasePlatform.Unknown
                    || relDbSource.ConnectionString.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both a data source and a connection string for the query.");
                    throw new System.Exception(_msg.ToString());
                }

                if (relDbSource.SqlQuery.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                dbPlatformDesc = relDbSource.DbPlatform.ToString();
                connStr = relDbSource.ConnectionString;

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = relDbSource.SqlQuery;
                db.CommandType = CommandType.Text;

                DataTable tab = db.RunQueryDataTable();
                tab.TableName = exDef.ExtractorName; ;

                //if (filterOutput)
                //{
                //    DataView dv = new DataView(tab);
                //    dv.RowFilter = exDef.RelationalDatabaseSource.OutputOptions.OutputFilter.FilterText;
                //    DataTable dt2 = dv.ToTable();
                //    tab = dt2;
                //}

                if (randomizeOutput)
                {
                    PFList<DataTableRandomizerColumnSpec> colSpecs = relDbSource.OutputOptions.RandomizerColSpecs;
                    PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = relDbSource.OutputOptions.RandomizerColSpecs;
                    SyncColSpecsWithDataSchema(tab, ref colSpecs);
                    relDbSource.OutputOptions.RandomizerColSpecs = colSpecs;
                    DataTableRandomizer dtr = new DataTableRandomizer();
                    dtr.MessageLogUI = this.MessageLogUI;
                    dtr.RandomizeDataTableValues(tab, relDbSource.OutputOptions.RandomizerColSpecs, randomDataBatchSize);
                    relDbSource.OutputOptions.RandomizerColSpecs = saveOrigColSpecs;   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
                }

                if (filterOutput)
                {
                    DataView dv = new DataView(tab);
                    dv.RowFilter = exDef.RelationalDatabaseSource.OutputOptions.OutputFilter.FilterText;
                    DataTable dt2 = dv.ToTable();
                    tab = dt2;
                }


                dt = tab;

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to execute the query and create data table: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                if (db != null)
                {
                    if (db.IsConnected)
                    {
                        db.CloseConnection();
                    }
                }
                db = null;

                _msg.Length = 0;
                _msg.Append("RunQuery ended at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());
            }

            return dt;
        }

        private DataTable GetDataTableForAccessDatabaseFile(PFExtractorDefinition exDef, bool showRowNumber, bool filterOutput, bool randomizeOutput, int randomDataBatchSize)
        {
            DataTable dt = null;
            PFMsAccess db = new PFMsAccess();
            Stopwatch sw = new Stopwatch();
            PFMsAccessSource accDbSource = exDef.MsAccessSource ;

            WriteMessageToLog("GetDataTableForAccessDatabaseFile started ...");

            sw.Start();

            try
            {
                db.DatabasePath = accDbSource.DatabasePath;
                db.DatabaseUsername = accDbSource.DatabaseUsername;
                db.DatabasePassword = accDbSource.DatabasePassword;
                if (accDbSource.SourceIsAccess2003)
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                else
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;


                db.OpenConnection();

                db.SQLQuery = accDbSource.SqlQuery;
                DataTable tab = db.RunQueryDataTable();

                //if (filterOutput)
                //{
                //    DataView dv = new DataView(tab);
                //    dv.RowFilter = exDef.MsAccessSource.OutputOptions.OutputFilter.FilterText;
                //    DataTable dt2 = dv.ToTable();
                //    tab = dt2;
                //}

                if (randomizeOutput)
                {
                    PFList<DataTableRandomizerColumnSpec> colSpecs = accDbSource.OutputOptions.RandomizerColSpecs;
                    PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = accDbSource.OutputOptions.RandomizerColSpecs;
                    SyncColSpecsWithDataSchema(tab, ref colSpecs);
                    accDbSource.OutputOptions.RandomizerColSpecs = colSpecs;
                    DataTableRandomizer dtr = new DataTableRandomizer();
                    dtr.MessageLogUI = this.MessageLogUI;
                    dtr.RandomizeDataTableValues(tab, accDbSource.OutputOptions.RandomizerColSpecs, randomDataBatchSize);
                    accDbSource.OutputOptions.RandomizerColSpecs = saveOrigColSpecs;   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
                }

                if (filterOutput)
                {
                    DataView dv = new DataView(tab);
                    dv.RowFilter = exDef.MsAccessSource.OutputOptions.OutputFilter.FilterText;
                    DataTable dt2 = dv.ToTable();
                    tab = dt2;
                }

                dt = tab;

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to execute the query and create data table: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (db.ConnectionState == ConnectionState.Open)
                    db.CloseConnection();

                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("RunQuery ended at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());

            }



            return dt;
        }

        private DataTable GetDataTableForExcelDataFile(PFExtractorDefinition exDef, bool showRowNumber, bool filterOutput, bool randomizeOutput, int randomDataBatchSize)
        {
            DataTable dt = null;
            Stopwatch sw = new Stopwatch();

            WriteMessageToLog("GetDataTableForExcelDataFile started ...");

            sw.Start();

            try
            {
                DataTable tab = LoadExcelDataTable(exDef);

                if (tab == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to load Excel data into DataTable.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return null;
                }

                //if (filterOutput)
                //{
                //    DataView dv = new DataView(tab);
                //    dv.RowFilter = exDef.MsExcelSource.OutputOptions.OutputFilter.FilterText;
                //    DataTable dt2 = dv.ToTable();
                //    tab = dt2;
                //}

                if (randomizeOutput)
                {
                    PFList<DataTableRandomizerColumnSpec> colSpecs = exDef.MsExcelSource.OutputOptions.RandomizerColSpecs;
                    PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = exDef.MsExcelSource.OutputOptions.RandomizerColSpecs;
                    SyncColSpecsWithDataSchema(tab, ref colSpecs);
                    exDef.MsExcelSource.OutputOptions.RandomizerColSpecs = colSpecs;
                    DataTableRandomizer dtr = new DataTableRandomizer();
                    dtr.MessageLogUI = this.MessageLogUI;
                    dtr.RandomizeDataTableValues(tab, exDef.MsExcelSource.OutputOptions.RandomizerColSpecs, randomDataBatchSize);
                    exDef.MsExcelSource.OutputOptions.RandomizerColSpecs = saveOrigColSpecs;   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
                }

                if (filterOutput)
                {
                    DataView dv = new DataView(tab);
                    dv.RowFilter = exDef.MsExcelSource.OutputOptions.OutputFilter.FilterText;
                    DataTable dt2 = dv.ToTable();
                    tab = dt2;
                }

                dt = tab;
                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to retrieve Excel data and create data table: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("Data retrieval ended at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());

            }
                 
        
            return dt;
        }

        private DataTable LoadExcelDataTable(PFExtractorDefinition exDef)
        {
            DataTable dt = null;
            PFExcelDocument excelDoc = null;
            enExcelOutputFormat excelFormat = enExcelOutputFormat.NotSpecified;

            if (exDef.MsExcelSource.SourceIsExcel2007Format)
                excelFormat = enExcelOutputFormat.Excel2007;
            else if (exDef.MsExcelSource.SourceIsExcel2003Format)
                excelFormat = enExcelOutputFormat.Excel2003;
            else if (exDef.MsExcelSource.SourceIsExcelCsvFormat)
                excelFormat = enExcelOutputFormat.CSV;
            else
                excelFormat = enExcelOutputFormat.NotSpecified;

            if (excelFormat == enExcelOutputFormat.NotSpecified)
            {
                _msg.Length = 0;
                _msg.Append("Invalid Excel file format: ");
                _msg.Append(excelFormat.ToString());
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
                return null;
            }

            excelDoc = new PFExcelDocument(excelFormat,
                                           exDef.MsExcelSource.DocumentFilePath,
                                           exDef.MsExcelSource.SheetName);

            if (exDef.MsExcelSource.DataLocationInNamedRangeFormat)
            {
                dt = excelDoc.ExportExcelDataToDataTable(exDef.MsExcelSource.RangeName, exDef.MsExcelSource.ColumnNamesInFirstRow);
            }
            else if (exDef.MsExcelSource.DataLocationInRowColFormat)
            {
                //Excel library uses 0 based indexing while Excel uses R1C1 row/col addressing format
                int startRow = (exDef.MsExcelSource.StartRow - 1) >= 0 ? exDef.MsExcelSource.StartRow - 1 : exDef.MsExcelSource.StartRow;
                int startCol = (exDef.MsExcelSource.StartCol - 1) >= 0 ? exDef.MsExcelSource.StartCol - 1 : exDef.MsExcelSource.StartCol;
                int endRow = (exDef.MsExcelSource.EndRow - 1) >= 0 ? exDef.MsExcelSource.EndRow - 1 : exDef.MsExcelSource.EndRow;
                int endCol = (exDef.MsExcelSource.EndCol - 1) >= 0 ? exDef.MsExcelSource.EndCol - 1 : exDef.MsExcelSource.EndCol;
                dt = excelDoc.ExportExcelDataToDataTable(startRow,
                                                         startCol,
                                                         endRow,
                                                         endCol,
                                                         exDef.MsExcelSource.ColumnNamesInFirstRow);
            }
            else
            {
                _msg.Length = 0;
                _msg.Append("Invalid data location type specified: You must specify either named range or row/col format.");
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
                return null;
            }



            return dt;
        }

        private DataTable GetDataTableForDelimitedTextFile(PFExtractorDefinition exDef, bool showRowNumber, bool filterOutput, bool randomizeOutput, int randomDataBatchSize)
        {
            DataTable dt = null;
            PFDataImporter dataImporter = new PFDataImporter();
            PFDelimitedDataLine delimitedLineDef = null;
            Stopwatch sw = new Stopwatch();

            sw.Start();

            try
            {
                if (exDef.DelimitedTextFileSource.TextFilePath.Trim() == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to file containing delimited text data.\r\n Current source file path is blank.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return null;
                }

                delimitedLineDef = dataImporter.CreateDelimitedLineDefinitionFromTextFile(exDef.DelimitedTextFileSource.TextFilePath,
                                                                                          exDef.DelimitedTextFileSource.GetColumnDelimiter(),
                                                                                          exDef.DelimitedTextFileSource.GetLineTerminator(),
                                                                                          exDef.DelimitedTextFileSource.ColumnNamesOnFirstLine,
                                                                                          exDef.DelimitedTextFileSource.StringValuesSurroundedWithQuotationMarks);
                
                DataTable tab = dataImporter.ImportDelimitedTextFileToDataTable(exDef.DelimitedTextFileSource.TextFilePath, delimitedLineDef);

                if (tab == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to load Delimited Text File data into DataTable.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return null;
                }

                //if (filterOutput)
                //{
                //    DataView dv = new DataView(tab);
                //    dv.RowFilter = exDef.DelimitedTextFileSource.OutputOptions.OutputFilter.FilterText;
                //    DataTable dt2 = dv.ToTable();
                //    tab = dt2;
                //}

                if (randomizeOutput)
                {
                    PFList<DataTableRandomizerColumnSpec> colSpecs = exDef.DelimitedTextFileSource.OutputOptions.RandomizerColSpecs;
                    PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = exDef.DelimitedTextFileSource.OutputOptions.RandomizerColSpecs;
                    SyncColSpecsWithDataSchema(tab, ref colSpecs);
                    exDef.DelimitedTextFileSource.OutputOptions.RandomizerColSpecs = colSpecs;
                    DataTableRandomizer dtr = new DataTableRandomizer();
                    dtr.MessageLogUI = this.MessageLogUI;
                    dtr.RandomizeDataTableValues(tab, exDef.DelimitedTextFileSource.OutputOptions.RandomizerColSpecs, randomDataBatchSize);
                    exDef.DelimitedTextFileSource.OutputOptions.RandomizerColSpecs = saveOrigColSpecs;   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
                }

                if (filterOutput)
                {
                    DataView dv = new DataView(tab);
                    dv.RowFilter = exDef.DelimitedTextFileSource.OutputOptions.OutputFilter.FilterText;
                    DataTable dt2 = dv.ToTable();
                    tab = dt2;
                }

                dt = tab;

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to retrieve delimited text data and create data table: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("Data retrieval ended at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());
            }
                 
        

            return dt;
        }

        private DataTable GetDataTableForFixedLengthTextFile(PFExtractorDefinition exDef, bool showRowNumber, bool filterOutput, bool randomizeOutput, int randomDataBatchSize)
        {
            DataTable dt = null;
            PFDataImporter dataImporter = new PFDataImporter();
            PFFixedLengthDataLine fixlenLineDef = new PFFixedLengthDataLine(5);
            Stopwatch sw = new Stopwatch();

            sw.Start();

            try
            {
                if (exDef.FixedLengthTextFileSource.TextFilePath.Trim() == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to file containing fixed length text data.\r\n Current source file path is blank.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return null;
                }

                fixlenLineDef = CreateSourceFixedLengthLineDefinition(exDef);

                DataTable tab = dataImporter.ImportFixedLengthTextFileToDataTable(exDef.FixedLengthTextFileSource.TextFilePath, fixlenLineDef);

                if (tab == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to load Fixed Length Text File data into DataTable.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return null;
                }

                //if (filterOutput)
                //{
                //    DataView dv = new DataView(tab);
                //    dv.RowFilter = exDef.FixedLengthTextFileSource.OutputOptions.OutputFilter.FilterText;
                //    DataTable dt2 = dv.ToTable();
                //    tab = dt2;
                //}

                if (randomizeOutput)
                {
                    PFList<DataTableRandomizerColumnSpec> colSpecs = exDef.FixedLengthTextFileSource.OutputOptions.RandomizerColSpecs;
                    PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = exDef.FixedLengthTextFileSource.OutputOptions.RandomizerColSpecs;
                    SyncColSpecsWithDataSchema(tab, ref colSpecs);
                    exDef.FixedLengthTextFileSource.OutputOptions.RandomizerColSpecs = colSpecs;
                    DataTableRandomizer dtr = new DataTableRandomizer();
                    dtr.MessageLogUI = this.MessageLogUI;
                    dtr.RandomizeDataTableValues(tab, exDef.FixedLengthTextFileSource.OutputOptions.RandomizerColSpecs, randomDataBatchSize);
                    exDef.FixedLengthTextFileSource.OutputOptions.RandomizerColSpecs = saveOrigColSpecs;   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
                }

                if (filterOutput)
                {
                    DataView dv = new DataView(tab);
                    dv.RowFilter = exDef.FixedLengthTextFileSource.OutputOptions.OutputFilter.FilterText;
                    DataTable dt2 = dv.ToTable();
                    tab = dt2;
                }

                dt = tab;

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to retrieve fixed length text data and create data table: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("Data retrieval ended at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());
            }
                 
        
            return dt;
        }

        private PFFixedLengthDataLine CreateSourceFixedLengthLineDefinition(PFExtractorDefinition exDef)
        {
            PFFixedLengthDataLine fixlenLineDef = null;
            int numCols = exDef.FixedLengthTextFileSource.ColumnDefinitions.ColumnDefinition.Length;
            PFColumnDefinitionsExt columnDefinitions = exDef.FixedLengthTextFileSource.ColumnDefinitions;
            int totLineLen = 0;
            int maxColLen = 0;

            fixlenLineDef = new PFFixedLengthDataLine(numCols);
            fixlenLineDef.ColumnDefinitions.NumberOfColumns = numCols;
            fixlenLineDef.AllowDataTruncation = true;
            fixlenLineDef.ColumnNamesOnFirstLine = exDef.FixedLengthTextFileSource.ColumnNamesOnFirstLineOfFile;


            for (int i = 0; i < numCols; i++)
            {
                fixlenLineDef.ColumnDefinitions.ColumnDefinition[i].ColumnName = columnDefinitions.ColumnDefinition[i].OutputColumnName;
                fixlenLineDef.ColumnDefinitions.ColumnDefinition[i].ColumnLength = columnDefinitions.ColumnDefinition[i].OutputColumnLength;
                fixlenLineDef.ColumnDefinitions.ColumnDefinition[i].ColumnDataAlignment = columnDefinitions.ColumnDefinition[i].OutputColumnDataAlignment;
                totLineLen += columnDefinitions.ColumnDefinition[i].OutputColumnLength;
                if(columnDefinitions.ColumnDefinition[i].OutputColumnLength > maxColLen)
                    maxColLen = columnDefinitions.ColumnDefinition[i].OutputColumnLength;
            }

            fixlenLineDef.DefaultStringColumnLength = maxColLen;
            fixlenLineDef.MaxColumnLengthOverride = maxColLen;
            fixlenLineDef.UseLineTerminator = exDef.FixedLengthTextFileSource.LineTerminatorAppendedToEachLine;
            fixlenLineDef.LineTerminator = exDef.FixedLengthTextFileSource.GetLineTerminator();
            

            return fixlenLineDef;
        }

        private DataTable GetDataTableForXmlFile(PFExtractorDefinition exDef, bool showRowNumber, bool filterOutput, bool randomizeOutput, int randomDataBatchSize)
        {
            DataTable dt = null;
            PFDataImporter dataImporter = new PFDataImporter();
            Stopwatch sw = new Stopwatch();

            sw.Start();

            try
            {
                DataTable tab = null;

                if (exDef.XmlFileSource.XmlFilePath.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to file containing XML data.\r\n Current source file path is blank.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return null;
                }

                if (exDef.XmlFileSource.SourceXmlSchemaInSeparateXsdFile)
                {
                    if (exDef.XmlFileSource.SourceXsdFilePath.Trim().Length == 0)
                    {
                        _msg.Length = 0;
                        _msg.Append("You must specify path to file that contains the XSD data for the source file.\r\n Current source XSD file path is blank and you specified using XSD stored in a separate file.");
                        WriteMessageToLog(_msg.ToString());
                        DisplayAppErrorMessage(_msg.ToString());
                        return null;
                    }
                }



                if (exDef.XmlFileSource.SourceXmlNoSchema || exDef.XmlFileSource.SourceXmlSchemaInXmlFile)
                {
                    tab = dataImporter.ImportXmlFileToDataTable(exDef.XmlFileSource.XmlFilePath);
                }

                if (exDef.XmlFileSource.SourceXmlSchemaInSeparateXsdFile)
                {
                    tab = dataImporter.ImportXmlFileToDataTable(exDef.XmlFileSource.XmlFilePath, exDef.XmlFileSource.SourceXsdFilePath);
                }


                if (tab == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to generate data table for the source XML file. Processing will terminate on error.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return null;
                }


                //if (filterOutput)
                //{
                //    DataView dv = new DataView(tab);
                //    dv.RowFilter = exDef.XmlFileSource.OutputOptions.OutputFilter.FilterText;
                //    DataTable dt2 = dv.ToTable();
                //    tab = dt2;
                //}

                //fix MaxLength property of data columns for string values
                for (int i = 0; i < tab.Columns.Count; i++)
                {
                    DataColumn dc = tab.Columns[i];
                    if (dc.DataType == typeof(System.String))
                    {
                        if (dc.MaxLength < 1)
                            dc.MaxLength = int.MaxValue;
                    }
                }

                if (randomizeOutput)
                {
                    PFList<DataTableRandomizerColumnSpec> colSpecs = exDef.XmlFileSource.OutputOptions.RandomizerColSpecs;
                    PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = exDef.XmlFileSource.OutputOptions.RandomizerColSpecs;
                    SyncColSpecsWithDataSchema(tab, ref colSpecs);
                    exDef.XmlFileSource.OutputOptions.RandomizerColSpecs = colSpecs;
                    DataTableRandomizer dtr = new DataTableRandomizer();
                    dtr.MessageLogUI = this.MessageLogUI;
                    dtr.RandomizeDataTableValues(tab, exDef.XmlFileSource.OutputOptions.RandomizerColSpecs, randomDataBatchSize);
                    exDef.XmlFileSource.OutputOptions.RandomizerColSpecs = saveOrigColSpecs;   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
                }

                if (filterOutput)
                {
                    DataView dv = new DataView(tab);
                    dv.RowFilter = exDef.XmlFileSource.OutputOptions.OutputFilter.FilterText;
                    DataTable dt2 = dv.ToTable();
                    tab = dt2;
                }

                dt = tab;

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to retrieve fixed length text data and create data table: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("Data retrieval ended at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());
            }
                 
        

            return dt;
        }


        /// <summary>
        /// Routine to randomize data values in specified columns.
        /// </summary>
        /// <param name="exdef">Extractor definition object.</param>
        /// <param name="colSpecs">Column specifications include whether or not (and how) to randomize data in a data column.</param>
        public void RunRandomizer(PFExtractorDefinition exdef, ref PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            DataTable dt = null;
            switch(exdef.ExtractorSource)
            {
                case enExtractorDataLocation.RelationalDatabase:
                    dt = GetQueryDefSchema(exdef.RelationalDatabaseSource, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.AccessDatabaseFile:
                    dt = GetQueryDefSchema(exdef.MsAccessSource, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.ExcelDataFile:
                    dt = GetExcelQueryDefSchema(exdef, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.DelimitedTextFile:
                    dt = GetDelimitedTextFileTableSchema(exdef, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.FixedLengthTextFile:
                    dt = GetFixedLengthTextFileTableSchema(exdef, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.XMLFile:
                    dt = GetXMLFileTableSchema(exdef, exdef.ExtractorName);
                    break;
                default:
                    break;
            }
            colSpecs = ShowRandomizerDefinitionForm(dt, colSpecs);
        }

        /// <summary>
        /// Retrieves a list of randomizer column specifications for the specified extractor definition.
        /// </summary>
        /// <param name="exdef">Object containing the extractor definition.</param>
        /// <returns>List of randomizer column specifications for the table.</returns>
        public PFList<DataTableRandomizerColumnSpec> GetInitialColSpecList(PFExtractorDefinition exdef)
        {
            DataTable dt = null;
            PFList<DataTableRandomizerColumnSpec> colSpecs = null;
            //DataTable dt = null;
            DataTableRandomizer dtr = null;

            switch (exdef.ExtractorSource)
            {
                case enExtractorDataLocation.RelationalDatabase:
                    dt = GetQueryDefSchema(exdef.RelationalDatabaseSource, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.AccessDatabaseFile:
                    dt = GetQueryDefSchema(exdef.MsAccessSource, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.ExcelDataFile:
                    dt = GetExcelQueryDefSchema(exdef, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.DelimitedTextFile:
                    dt = GetDelimitedTextFileTableSchema(exdef, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.FixedLengthTextFile:
                    dt = GetFixedLengthTextFileTableSchema(exdef, exdef.ExtractorName);
                    break;
                case enExtractorDataLocation.XMLFile:
                    dt = GetXMLFileTableSchema(exdef, exdef.ExtractorName);
                    break;
                default:
                    break;
            }

            dtr = new DataTableRandomizer();
            colSpecs = dtr.GetInitColSpecListFromDataTable(dt);

            return colSpecs;
        }

        /// <summary>
        /// Displays form for defining a filter to be applied to the extractor output.
        /// </summary>
        /// <param name="exdef">Extractor definition object.</param>
        /// <param name="filter">Object containing filter definition. This object can be modified by the filter form routines.</param>
        public void SetFilter(PFExtractorDefinition exdef, ref PFFilter filter)
        {
            DataTable dt = null;
            string queryText = string.Empty;

            switch (exdef.ExtractorSource)
            {
                case enExtractorDataLocation.RelationalDatabase:
                    dt = GetQueryDefSchema(exdef.RelationalDatabaseSource, exdef.ExtractorName);
                    queryText = exdef.RelationalDatabaseSource.SqlQuery;
                    break;
                case enExtractorDataLocation.AccessDatabaseFile:
                    dt = GetQueryDefSchema(exdef.MsAccessSource, exdef.ExtractorName);
                    queryText = exdef.MsAccessSource.SqlQuery;
                    break;
                case enExtractorDataLocation.ExcelDataFile:
                    dt = GetExcelQueryDefSchema(exdef, exdef.ExtractorName);
                    queryText = string.Empty;
                    break;
                case enExtractorDataLocation.DelimitedTextFile:
                    dt = GetDelimitedTextFileTableSchema(exdef, exdef.ExtractorName);
                    queryText = string.Empty;
                    break;
                case enExtractorDataLocation.FixedLengthTextFile:
                    dt = GetFixedLengthTextFileTableSchema(exdef, exdef.ExtractorName);
                    queryText = string.Empty;
                    break;
                case enExtractorDataLocation.XMLFile:
                    dt = GetXMLFileTableSchema(exdef, exdef.ExtractorName);
                    queryText = string.Empty;
                    break;
                default:
                    break;
            }

            //queryText will be blank for excel, delimited, fixedlength and xml file options
            filter = ShowFilterForm(dt, filter, queryText);
        }


        private DataTable AddRowNumberToDataTable(DataTable tab)
        {
            DataTable dt = null;

            //dt = tab.Copy();
            dt = tab;
            DataColumn dc = new DataColumn();
            dc.ColumnName = "RowNumber";
            dc.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(dc);
            dc.SetOrdinal(0);
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                dt.Rows[r][0] = r + 1;
            }

            return dt;
        }

        /// <summary>
        /// Generates extractor output from source to destination specified in the extractor definition.
        /// </summary>
        /// <param name="exdef">Extractor definition object.</param>
        /// <param name="maxPreviewRows">Not used by RunExtract routine.</param>
        /// <param name="showRowNumber">Set to true to add row number to data output.</param>
        /// <param name="filterOutput">Set to true to filter the data output before it is written to the destination.</param>
        /// <param name="randomizeOutput">Set to true to randomize the data output before it is written to the destination.</param>
        /// <param name="randomizerBatchSize">Specifies number of random values to generate each time a new set of values the application can use is produced.</param>
        /// <param name="importExportBatchsize">Number of data rows to read and write as a single batch. This number will be overridden by the batch size limits for individual database providers.</param>
        /// <returns>Returns 0 if success; non-zero if there was one or more errors.</returns>
        public int RunExtract(PFExtractorDefinition exdef,
                              int maxPreviewRows,
                              bool showRowNumber,
                              bool filterOutput,
                              bool randomizeOutput,
                              int randomizerBatchSize,
                              int importExportBatchsize)
        {
            DataTable dt = null;
            Stopwatch sw = new Stopwatch();
            int retcode = 0;
            
            try
            {
                sw.Start();

                switch (exdef.ExtractorSource)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        dt = GetDataTableForRelationDatabase(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        dt = GetDataTableForAccessDatabaseFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.ExcelDataFile:
                        dt = GetDataTableForExcelDataFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        dt = GetDataTableForDelimitedTextFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        dt = GetDataTableForFixedLengthTextFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    case enExtractorDataLocation.XMLFile:
                        dt = GetDataTableForXmlFile(exdef, showRowNumber, filterOutput, randomizeOutput, randomizerBatchSize);
                        break;
                    default:
                        retcode = 1;
                        dt = null;
                        _msg.Length = 0;
                        _msg.Append("PreviewData not implemented for unknown data location.");
                        WriteMessageToLog(_msg.ToString());
                        DisplayAppAlertMessage(_msg.ToString());
                        break;
                }

                if (dt == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to get data from ");
                    _msg.Append(exdef.ExtractorSource.ToString());
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return 1;
                }

                if (showRowNumber)
                {
                    dt = AddRowNumberToDataTable(dt);
                }


                switch (exdef.ExtractorDestination)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        OutputDataTableToRelationalDatabase(dt, exdef, importExportBatchsize);
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        OutputDataTableToAccessDatabaseFile(dt, exdef, importExportBatchsize);
                        break;
                    case enExtractorDataLocation.ExcelDataFile:
                        OutputDataTableToExcelDataFile(dt, exdef, importExportBatchsize);
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        OutputDataTableToDelimitedTextFile(dt, exdef, importExportBatchsize);
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        OutputDataTableToFixedLengthTextFile(dt, exdef, importExportBatchsize);
                        break;
                    case enExtractorDataLocation.XMLFile:
                        OutputDataTableToXmlFile(dt, exdef, importExportBatchsize);
                        break;
                    default:
                        retcode = 1;
                        dt = null;
                        _msg.Length = 0;
                        _msg.Append("OutputData not implemented for unknown data destination.");
                        WriteMessageToLog(_msg.ToString());
                        DisplayAppAlertMessage(_msg.ToString());
                        break;
                }

                sw.Stop();

            }
            catch (System.Exception ex)
            {
                retcode = 1;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("RunExtract finished. Elapsed time for extracting process: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());
                DisplayAppInfoMessage(_msg.ToString());

            }

            return retcode;

        }

        /// <summary>
        /// Routine for outputting data from a table to a DataGridView object.
        /// </summary>
        /// <param name="tab">Table containing data to display.</param>
        /// <param name="maxPreviewRows">Maximum number of rows to display on the grid.</param>
        /// <param name="showRowNumber">Set to true to display row number in the output.</param>
        /// <param name="filterOutput">Set to true if filter will be applied to the data before it is displayed. Ignored by this routine. Routine assumes data is already filtered.</param>
        /// <param name="randomizeOutput">Set to true to randomize one or more of the table columns. Ignored by this routine. Routine assumes data is already randomized.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Set to true if database platform drop-down lists to only include data providers that are installed on the current system. This parameter will be used if an export of the grid data to a database platform is requested.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing output from data grid exports.  This parameter will be used if an export of the grid data to a database platform is requested.</param>
        /// <param name="defaultOutputDatabaseType">Default database platform to display when form with a data destination Database Type drop-down list is first shown. This parameter will be used if an export of the grid data to a database platform is requested.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use when the Default Output Database Type for a data destination is set on one of the application’s data destination drop-down lists. This parameter will be used if an export of the grid data to a database platform is requested.</param>
        public void OutputDataTableToGrid(DataTable tab, 
                                          int maxPreviewRows,
                                          bool showRowNumber,
                                          bool filterOutput,
                                          bool randomizeOutput,
                                          bool showInstalledDatabaseProvidersOnly,
                                          string defaultDataGridExportFolder,
                                          string defaultOutputDatabaseType,
                                          string defaultOutputDatabaseConnectionString)
        {
            PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
            grid.ShowInstalledDatabaseProvidersOnly = showInstalledDatabaseProvidersOnly;
            grid.DefaultOutputDatabaseType = defaultOutputDatabaseType;
            grid.DefaultOutputDatabaseConnectionString = defaultOutputDatabaseConnectionString;
            grid.ShowRowNumber = showRowNumber;
            if (Directory.Exists(defaultDataGridExportFolder) == false)
            {
                grid.DefaultGridExportFolder = _initDataGridExportFolder;
            }
            else
            {
                grid.DefaultGridExportFolder = defaultDataGridExportFolder;
            }

            if (maxPreviewRows < 1)
            {
                grid.WriteDataToGrid(tab);
            }
            else
            {
                DataTable tab2 = tab.AsEnumerable().Skip(0).Take(maxPreviewRows).CopyToDataTable();
                grid.WriteDataToGrid(tab2);
            }
        }

        private void OutputDataTableToRelationalDatabase(DataTable dt,
                                                         PFExtractorDefinition exdef,
                                                         int importExportBatchSize)
        {
            DatabaseOutputProcessor dbout = new DatabaseOutputProcessor(exdef.RelationalDatabaseDestination.DbPlatform);
            Stopwatch sw = new Stopwatch();

            if (exdef.RelationalDatabaseDestination.DbPlatform == DatabasePlatform.Unknown
                || exdef.RelationalDatabaseDestination.ConnectionString.Trim().Length == 0
                || exdef.RelationalDatabaseDestination.TableName.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must specify the database platform, connection string and table name for the relational database output.\r\n");
                _msg.Append("One or more of these parameters not supplied. Output has been cancelled.");
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
                return;
            }


            try
            {
                sw.Start();

                dbout.ConnectionString = exdef.RelationalDatabaseDestination.ConnectionString;
                dbout.ReplaceExistingTable = exdef.RelationalDatabaseDestination.OverwriteTableIfExists;
                dbout.TableName = exdef.RelationalDatabaseDestination.TableName;
                dbout.OutputBatchSize = importExportBatchSize;
                dbout.WriteDataToOutput(dt);

                sw.Stop();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("Output data table to relational database finished. Elapsed time for output process: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());

            }
                 
        
        }

        private void OutputDataTableToAccessDatabaseFile(DataTable dt,
                                                         PFExtractorDefinition exdef,
                                                         int importExportBatchSize)
        {
            AccessDatabaseFileOutputProcessor dbout = new AccessDatabaseFileOutputProcessor();
            Stopwatch sw = new Stopwatch();

            if (exdef.MsAccessDestination.DatabasePath.Trim().Length == 0
                || exdef.MsAccessDestination.OutputTableName.Trim().Length == 0)
            {
                _msg.Length = 0;
                _msg.Append("You must specify the database file path and table name for the MS Access database output.\r\n");
                _msg.Append("One or both these parameters not supplied. Output has been cancelled.");
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
                return;
            }

            try
            {
                sw.Start();

                dbout.DbPlatform = DatabasePlatform.MSAccess;
                dbout.OutputFileName = exdef.MsAccessDestination.DatabasePath;
                dbout.AccessVersion = exdef.MsAccessDestination.DestinationIsAccess2003 ? enAccessVersion.Access2003 : enAccessVersion.Access2007;
                dbout.DbUsername = exdef.MsAccessDestination.DatabaseUsername;
                dbout.DbPassword = exdef.MsAccessDestination.DatabasePassword;
                dbout.TableName = exdef.MsAccessDestination.OutputTableName;
                dbout.ReplaceExistingFile = exdef.MsAccessDestination.OverwriteDatabaseFileIfExists;
                dbout.ReplaceExistingTable = exdef.MsAccessDestination.OverwriteTableIfExists;

                dbout.WriteDataToOutput(dt);

                sw.Stop();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("Output data table to Access database finished. Elapsed time for output process: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());
            }
                 
        
        }

        private void OutputDataTableToExcelDataFile(DataTable dt,
                                                    PFExtractorDefinition exdef,
                                                    int importExportBatchSize)
        {
            ExcelDocumentFileOutputProcessor exOut = null;
            Stopwatch sw = new Stopwatch();
            enExcelVersion excelVersion = enExcelVersion.NotSpecified;

            sw.Start();
            try
            {

                if (exdef.MsExcelDestination.DestinationIsExcel2007Format)
                    excelVersion = enExcelVersion.Excel2007;
                else if (exdef.MsExcelDestination.DestinationIsExcel2003Format)
                    excelVersion = enExcelVersion.Excel2003;
                else if (exdef.MsExcelDestination.DestinationIsExcelCsvFormat)
                    excelVersion = enExcelVersion.CSV;
                else
                    excelVersion = enExcelVersion.NotSpecified;

                if (excelVersion == enExcelVersion.NotSpecified)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid Excel file format: ");
                    _msg.Append(excelVersion.ToString());
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }

                //fix extension, if necessary
                string fileExtension = Path.GetExtension(exdef.MsExcelDestination.DocumentFilePath);
                string outputFilePath = exdef.MsExcelDestination.DocumentFilePath;
                if (excelVersion == enExcelVersion.Excel2007 && fileExtension.ToLower() != ".xlsx")
                {
                    outputFilePath = Path.ChangeExtension(exdef.MsExcelDestination.DocumentFilePath, ".xlsx");
                }
                else if (excelVersion == enExcelVersion.Excel2003 && fileExtension.ToLower() != ".xls")
                {
                    outputFilePath = Path.ChangeExtension(exdef.MsExcelDestination.DocumentFilePath, ".xls");
                }
                else if (excelVersion == enExcelVersion.CSV && fileExtension.ToLower() != ".csv")
                {
                    outputFilePath = Path.ChangeExtension(exdef.MsExcelDestination.DocumentFilePath, ".csv");
                }
                else
                {
                    outputFilePath = exdef.MsExcelDestination.DocumentFilePath; 
                }

                exOut = new ExcelDocumentFileOutputProcessor(excelVersion, outputFilePath, exdef.MsExcelDestination.OverwriteFileIfExists, exdef.MsExcelDestination.SheetName, exdef.MsExcelDestination.OverwriteSheetIfExists);
                exOut.WriteDataToOutputExt(dt);

                sw.Stop();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("Output data table to Excel document finished. Elapsed time for output process: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());
            }
                 
        
        }

        private void OutputDataTableToDelimitedTextFile(DataTable dt,
                                                        PFExtractorDefinition exdef,
                                                        int importExportBatchSize)
        {
            DelimitedTextFileOutputProcessor texout = null;
            string colDelimiter = ",";
            string lineTerminator = Environment.NewLine;

            try
            {
                if (exdef.DelimitedTextFileDestination.TextFilePath.Trim() == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to file that will contain delimited text data. Current destination file path is blank.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }
                colDelimiter = exdef.DelimitedTextFileDestination.GetColumnDelimiter();
                lineTerminator = exdef.DelimitedTextFileDestination.GetLineTerminator();


                texout = new DelimitedTextFileOutputProcessor(exdef.DelimitedTextFileDestination.TextFilePath,
                                                              exdef.DelimitedTextFileDestination.OverwriteFileIfExists,
                                                              colDelimiter,
                                                              exdef.DelimitedTextFileDestination.ColumnNamesOnFirstLine,
                                                              lineTerminator,
                                                              exdef.DelimitedTextFileDestination.StringValuesSurroundedWithQuotationMarks);

                texout.WriteDataToOutput(dt);
                                                              
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }
                 
        
        }

        private void OutputDataTableToFixedLengthTextFile(DataTable dt,
                                                          PFExtractorDefinition exdef,
                                                          int importExportBatchSize)
        {
            FixedLengthTextFileOutputProcessor texout = null;
            string lineTerminator = Environment.NewLine;
            int maxColLen = 1024;
            PFFixedLengthDataLine fxlDataLine = null;

            try
            {
                if (exdef.FixedLengthTextFileDestination.TextFilePath.Trim() == string.Empty)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to file that will contain FixedLength text data. Current destination file path is blank.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }

                maxColLen = GetMaxColLenInDataTable (exdef.FixedLengthTextFileDestination.ColumnDefinitions);

                lineTerminator = exdef.FixedLengthTextFileDestination.GetLineTerminator();


                texout = new FixedLengthTextFileOutputProcessor(exdef.FixedLengthTextFileDestination.TextFilePath,
                                                                exdef.FixedLengthTextFileDestination.OverwriteFileIfExists,
                                                                exdef.FixedLengthTextFileDestination.ColumnNamesOnFirstLineOfFile,
                                                                exdef.FixedLengthTextFileDestination.AllowDataTruncation,
                                                                exdef.FixedLengthTextFileDestination.AppendLineTerminatorToEachLine,
                                                                lineTerminator,
                                                                (int)255,
                                                                maxColLen);

                fxlDataLine = CreateDestinationFixedLengthLineDefinition(exdef);

                texout.WriteDataToOutput(dt, fxlDataLine);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

        }

        private int GetMaxColLenInDataTable(PFColumnDefinitionsExt colDefs)
        {
            int maxColLen = 1024;

            for (int i = 0; i < colDefs.ColumnDefinition.Length; i++)
            {
                if (colDefs.ColumnDefinition[i].OutputColumnLength > maxColLen)
                    maxColLen = colDefs.ColumnDefinition[i].OutputColumnLength;
            }

            return maxColLen;
        }

        private PFFixedLengthDataLine CreateDestinationFixedLengthLineDefinition(PFExtractorDefinition exDef)
        {
            PFFixedLengthDataLine fixlenLineDef = null;
            int numCols = exDef.FixedLengthTextFileDestination.ColumnDefinitions.ColumnDefinition.Length;
            PFColumnDefinitionsExt columnDefinitions = exDef.FixedLengthTextFileDestination.ColumnDefinitions;
            int totLineLen = 0;
            int maxColLen = 0;

            fixlenLineDef = new PFFixedLengthDataLine(numCols);
            fixlenLineDef.ColumnDefinitions.NumberOfColumns = numCols;
            fixlenLineDef.AllowDataTruncation = true;
            fixlenLineDef.ColumnNamesOnFirstLine = exDef.FixedLengthTextFileDestination.ColumnNamesOnFirstLineOfFile;


            for (int i = 0; i < numCols; i++)
            {
                fixlenLineDef.ColumnDefinitions.ColumnDefinition[i].ColumnName = columnDefinitions.ColumnDefinition[i].OutputColumnName;
                fixlenLineDef.ColumnDefinitions.ColumnDefinition[i].ColumnLength = columnDefinitions.ColumnDefinition[i].OutputColumnLength;
                fixlenLineDef.ColumnDefinitions.ColumnDefinition[i].ColumnDataAlignment = columnDefinitions.ColumnDefinition[i].OutputColumnDataAlignment;
                totLineLen += columnDefinitions.ColumnDefinition[i].OutputColumnLength;
                if (columnDefinitions.ColumnDefinition[i].OutputColumnLength > maxColLen)
                    maxColLen = columnDefinitions.ColumnDefinition[i].OutputColumnLength;
            }

            fixlenLineDef.DefaultStringColumnLength = maxColLen;
            fixlenLineDef.MaxColumnLengthOverride = maxColLen;
            fixlenLineDef.UseLineTerminator = exDef.FixedLengthTextFileDestination.AppendLineTerminatorToEachLine;
            fixlenLineDef.LineTerminator = exDef.FixedLengthTextFileDestination.GetLineTerminator();


            return fixlenLineDef;
        }


        private void OutputDataTableToXmlFile(DataTable dt,
                                              PFExtractorDefinition exdef,
                                              int importExportBatchSize)
        {
            XMLFileOutputProcessor xmlOut = new XMLFileOutputProcessor();

            try
            {
                if (exdef.XmlFileDestination.DestXmlFilePath.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to file that will contain XML data. Current destination file path is blank.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }

                if (exdef.XmlFileDestination.DestXmlSchemaInSeparateXsdFile)
                {
                    if (exdef.XmlFileDestination.DestXsdFilePath.Trim().Length == 0)
                    {
                        _msg.Length = 0;
                        _msg.Append("You must specify path to file that will contain XSD data.\r\n Current destination XSD file path is blank and you specified saving XSD in a separate file.");
                        WriteMessageToLog(_msg.ToString());
                        DisplayAppErrorMessage(_msg.ToString());
                        return;
                    }
                }

                if (exdef.XmlFileDestination.DestXmlNoSchema || exdef.XmlFileDestination.DestXmlSchemaInXmlFile)
                {
                    xmlOut.OutputFileName = exdef.XmlFileDestination.DestXmlFilePath;
                    xmlOut.ReplaceExistingFile = exdef.XmlFileDestination.ReplaceExistingXmlFile;
                    xmlOut.XMLOutputType = exdef.XmlFileDestination.DestXmlNoSchema ? enXMLOutputType.DataOnly : enXMLOutputType.DataPlusSchema;
                    xmlOut.WriteDataToOutput(dt);                                    
                }

                if (exdef.XmlFileDestination.DestXmlSchemaInSeparateXsdFile)
                {
                    xmlOut.OutputFileName = exdef.XmlFileDestination.DestXmlFilePath;
                    xmlOut.ReplaceExistingFile = exdef.XmlFileDestination.ReplaceExistingXmlFile;
                    xmlOut.XMLOutputType = enXMLOutputType.DataOnly;
                    xmlOut.WriteDataToOutput(dt);

                    xmlOut.OutputFileName = exdef.XmlFileDestination.DestXsdFilePath;
                    xmlOut.ReplaceExistingFile = exdef.XmlFileDestination.ReplaceExistingXsdFile;
                    xmlOut.XMLOutputType = enXMLOutputType.SchemaOnly;
                    xmlOut.WriteDataToOutput(dt);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }
                 
        
        }

        /// <summary>
        /// Displays the form used to define whether or not and how individual columns in a DataTable are to be randomized.
        /// </summary>
        /// <param name="dt">DataTable containing columns to be considered for randomization.</param>
        /// <param name="extractDefinitionColSpecs">Column specifications for the extract definition.</param>
        /// <returns>List of randomizer column specifications for the data table.</returns>
        public PFList<DataTableRandomizerColumnSpec> ShowRandomizerDefinitionForm(DataTable dt, PFList<DataTableRandomizerColumnSpec> extractDefinitionColSpecs)
        {
            PFList<DataTableRandomizerColumnSpec> colSpecs = null;
            //DataTable dt = null;
            DataTableRandomizer dtr = null;


            try
            {
                _msg.Length = 0;
                _msg.Append("ShowRandomizerDefinitionForm started ... ");
                WriteMessageToLog(_msg.ToString());

                if (dt == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to determine schema for randomizer definition.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return colSpecs;
                }

                dtr = new DataTableRandomizer();
                colSpecs = dtr.GetInitColSpecListFromDataTable(dt);
                SyncColSpecsWithSavedValues(extractDefinitionColSpecs, colSpecs);
                SyncColSpecsWithDataSchema(dt, ref colSpecs);

                PFRandomDataForms.DataTableRandomizerColumnSpecForm frm = new PFRandomDataForms.DataTableRandomizerColumnSpecForm(colSpecs);
                frm.ColSpecs = colSpecs;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    colSpecs = frm.ColSpecs;
                    _msg.Length = 0;
                    _msg.Append(frm.ColSpecs.ToXmlString());
                    WriteMessageToLog(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Column spec edit cancelled.");
                    WriteMessageToLog(_msg.ToString());
                }



            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... ShowRandomizerDefinitionForm finished.");
                WriteMessageToLog(_msg.ToString());
            }

            return colSpecs;
        }

        /// <summary>
        /// Retrieves column schemas for the specified relational data source.
        /// </summary>
        /// <param name="relDbDef">Relational source to be used.</param>
        /// <param name="extractorName">Name of the extractor definition. This will be used to name the return DataTable.</param>
        /// <returns>DataTable containing the column schemas.</returns>
        public DataTable GetQueryDefSchema(PFRelationalDatabaseSource  relDbDef, string extractorName)
        {
            DataTable dt = null;
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            string dbConnStr = string.Empty;
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            try
            {
                dbPlat = relDbDef.DbPlatform;

                if (relDbDef.DbPlatform == DatabasePlatform.Unknown
                    || relDbDef.ConnectionString.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both a data source and a connection string for the query.");
                    throw new System.Exception(_msg.ToString());
                }

                if (relDbDef.SqlQuery.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                dbPlatformDesc = relDbDef.DbPlatform.ToString();
                connStr = relDbDef.ConnectionString;

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = relDbDef.SqlQuery;
                db.CommandType = CommandType.Text;

                dt = db.GetQueryDataSchema(relDbDef.SqlQuery, CommandType.Text);
                dt.TableName = extractorName;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (db != null)
                {
                    if (db.IsConnected)
                    {
                        db.CloseConnection();
                    }
                }
                db = null;

            }

            return dt;
        }

        /// <summary>
        /// Retrieves column schemas for an MS Access data source.
        /// </summary>
        /// <param name="accDbDef">MS Access data source.</param>
        /// <param name="extractorName">Name of the extractor definition. This will be used to name the return DataTable.</param>
        /// <returns>DataTable containing the column schemas.</returns>
        public DataTable GetQueryDefSchema(PFMsAccessSource accDbDef, string extractorName)
        {
            DataTable dt = null;
            //string dbPlatformDesc = DatabasePlatform.MSAccess.ToString();
            //DatabasePlatform dbPlat = DatabasePlatform.MSAccess;
            PFMsAccess db = new PFMsAccess();
            
            try
            {

                if (accDbDef.DatabasePath.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify path to Access database file.");
                    throw new System.Exception(_msg.ToString());
                }

                if (accDbDef.SqlQuery.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }


                db.DatabasePath = accDbDef.DatabasePath;
                db.DatabaseUsername = accDbDef.DatabaseUsername;
                db.DatabasePassword = accDbDef.DatabasePassword;
                if (accDbDef.SourceIsAccess2003)
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftJetOLEDB_4_0;
                else
                    db.OleDbProvider = PFAccessOleDbProvider.MicrosoftACEOLEDB_12_0;


                db.OpenConnection();

                db.SQLQuery = accDbDef.SqlQuery;
                dt = db.GetQueryDataSchema(accDbDef.SqlQuery, CommandType.Text);
                dt.TableName = extractorName;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (db != null)
                {
                    if (db.IsConnected)
                    {
                        db.CloseConnection();
                    }
                }
                db = null;

            }

            return dt;
        }

        /// <summary>
        /// Retrieves column schemas for an Excel query definition.
        /// </summary>
        /// <param name="exDef">Extractor definition containing the Excel data source.</param>
        /// <param name="extractorName">Name of the extractor definition. This will be used to name the return DataTable.</param>
        /// <returns>DataTable containing the column schemas.</returns>
        public DataTable GetExcelQueryDefSchema(PFExtractorDefinition exDef, string extractorName)
        {
            DataTable dt = null;

            try
            {
                dt = LoadExcelDataTable(exDef);
                dt.TableName = extractorName;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;
        }

        /// <summary>
        /// Retrieves column schemas for delimited text file data source.
        /// </summary>
        /// <param name="exDef">Extractor definition containing the delimited text file data source.</param>
        /// <param name="extractorName">Name of the extractor definition. This will be used to name the return DataTable.</param>
        /// <returns>DataTable containing the column schemas.</returns>
        public DataTable GetDelimitedTextFileTableSchema(PFExtractorDefinition exDef, string extractorName)
        {
            DataTable dt = null;
            PFDataImporter dataImporter = new PFDataImporter();

            try
            {
                dt = dataImporter.GetDelimitedTextFileSchemaTable(exDef.DelimitedTextFileSource.TextFilePath,
                                                                  exDef.DelimitedTextFileSource.GetColumnDelimiter(),
                                                                  exDef.DelimitedTextFileSource.GetLineTerminator(),
                                                                  exDef.DelimitedTextFileSource.ColumnNamesOnFirstLine);
                dt.TableName = extractorName;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;
        }

        /// <summary>
        /// Retrieves column schemas for fixed length text file data source.
        /// </summary>
        /// <param name="exDef">Extractor definition containing the fixed length text file data source.</param>
        /// <param name="extractorName">Name of the extractor definition. This will be used to name the return DataTable.</param>
        /// <returns>DataTable containing the column schemas.</returns>
        public DataTable GetFixedLengthTextFileTableSchema(PFExtractorDefinition exDef, string extractorName)
        {
            DataTable dt = null;
            PFDataImporter dataImporter = new PFDataImporter();
            PFFixedLengthDataLine fixlenLineDef = new PFFixedLengthDataLine(5);

            try
            {
                fixlenLineDef = CreateSourceFixedLengthLineDefinition(exDef);
                dt = dataImporter.GetFixedLengthTextFileSchemaTable(fixlenLineDef,
                                                                    exDef.FixedLengthTextFileSource.TextFilePath);
                dt.TableName = extractorName;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;
        }

        /// <summary>
        /// Retrieves column schemas for XML formatted text file data source.
        /// </summary>
        /// <param name="exDef">Extractor definition containing the XML formatted text file data source.</param>
        /// <param name="extractorName">Name of the extractor definition. This will be used to name the return DataTable.</param>
        /// <returns>DataTable containing the column schemas.</returns>
        public DataTable GetXMLFileTableSchema(PFExtractorDefinition exDef, string extractorName)
        {
            DataTable dt = null;
            PFDataImporter dataImporter = new PFDataImporter();

            try
            {
                if (exDef.XmlFileSource.SourceXmlSchemaInXmlFile)
                {
                    dt = dataImporter.ImportXmlSchemaToDataTable(exDef.XmlFileSource.XmlFilePath);
                }
                else if (exDef.XmlFileSource.SourceXmlSchemaInSeparateXsdFile)
                {
                    dt = dataImporter.ImportXmlSchemaToDataTable(exDef.XmlFileSource.SourceXsdFilePath);
                }
                else
                {
                    //no schema associated with xml data
                    dt = dataImporter.ImportXmlSchemaToDataTable(exDef.XmlFileSource.XmlFilePath);
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return dt;
        }

        /// <summary>
        /// Synchronizes column specifications with a set of earlier saved specifications. Used before showing randomizer form.
        /// </summary>
        /// <param name="extractDefinitionColSpecs">List of column specifications for an extractor definition.</param>
        /// <param name="colSpecs">List of randomizer column specifications.</param>
        public void SyncColSpecsWithSavedValues(PFList<DataTableRandomizerColumnSpec> extractDefinitionColSpecs, PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            if (colSpecs == null || extractDefinitionColSpecs == null)
                return;

            for (int cs = 0; cs < colSpecs.Count; cs++)
            {
                for (int qd = 0; qd < extractDefinitionColSpecs.Count; qd++)
                {
                    if (extractDefinitionColSpecs[qd].DataTableColumnName == colSpecs[cs].DataTableColumnName)
                    {
                        colSpecs[cs].RandomDataType = extractDefinitionColSpecs[qd].RandomDataType;
                        colSpecs[cs].RandomDataSource = extractDefinitionColSpecs[qd].RandomDataSource;
                        colSpecs[cs].RandomNamesAndLocationsNumber = extractDefinitionColSpecs[qd].RandomNamesAndLocationsNumber;
                        colSpecs[cs].RandomDataFieldName = extractDefinitionColSpecs[qd].RandomDataFieldName;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Synchronizes column specifications with a set of DataTable column specifications. Used before showing randomizer form.
        /// </summary>
        /// <param name="dt">DataTable containing columns.</param>
        /// <param name="colSpecs">List of randomizer column specifications that will be updated by this routine.</param>
        public void SyncColSpecsWithDataSchema(DataTable dt, ref PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            PFList<DataTableRandomizerColumnSpec> newColSpecs = new PFList<DataTableRandomizerColumnSpec>();

            if (colSpecs == null)
                return;

            try
            {
                if (dt == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to retrieve data table schema in SyncColSpecsWithDataSchema routine for data table.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }
                else if (dt.Columns == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to retrieve data table schema column list in SyncColSpecsWithDataSchema routine for ");
                    _msg.Append(dt.TableName);
                    _msg.Append(".");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }
                else if (dt.Columns.Count == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("Query text must specify one or more data columns for SyncColSpecsWithDataSchema routine.");
                    WriteMessageToLog(_msg.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("SyncColSpecsWithDataSchema will process data table ");
                    _msg.Append(dt.TableName);
                    WriteMessageToLog(_msg.ToString());
                }

                for (int dtColInx = 0; dtColInx < dt.Columns.Count; dtColInx++)
                {
                    DataTableRandomizerColumnSpec newColSpec = new DataTableRandomizerColumnSpec();
                    for (int colSpecsInx = 0; colSpecsInx < colSpecs.Count; colSpecsInx++)
                    {
                        if (dt.Columns[dtColInx].ColumnName.ToLower() == colSpecs[colSpecsInx].DataTableColumnName.ToLower())
                        {
                            newColSpec.DataTableColumnName = colSpecs[colSpecsInx].DataTableColumnName;
                            newColSpec.DataTableColumnIndex = colSpecs[colSpecsInx].DataTableColumnIndex;
                            newColSpec.DataTableColumnDataType = colSpecs[colSpecsInx].DataTableColumnDataType;
                            newColSpec.CurrentValueIndex = colSpecs[colSpecsInx].CurrentValueIndex;
                            newColSpec.RandomDataFieldColumnIndex = colSpecs[colSpecsInx].RandomDataFieldColumnIndex;
                            newColSpec.RandomDataFieldName = colSpecs[colSpecsInx].RandomDataFieldName;
                            newColSpec.RandomDataFileName = colSpecs[colSpecsInx].RandomDataFileName;
                            newColSpec.RandomDataListIndex = colSpecs[colSpecsInx].RandomDataListIndex;
                            newColSpec.RandomDataSource = colSpecs[colSpecsInx].RandomDataSource;
                            newColSpec.RandomDataType = colSpecs[colSpecsInx].RandomDataType;
                            newColSpec.RandomDataTypeProcessorIndex = colSpecs[colSpecsInx].RandomDataTypeProcessorIndex;
                            newColSpec.RandomNamesAndLocationsNumber = colSpecs[colSpecsInx].RandomNamesAndLocationsNumber;
                            break;
                        }
                    }//end for loop colSpecInx
                    newColSpecs.Add(newColSpec);
                }//end for loop dtColInx

                if (dt.Columns.Count != newColSpecs.Count)
                {
                    _msg.Length = 0;
                    _msg.Append("SyncColSpecsWithDataSchema has failed: DataTable column count does not match count for new column specs list.");
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Data Table Column count:       ");
                    _msg.Append(dt.Columns.Count.ToString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append("New Column Specs Column count: ");
                    _msg.Append(newColSpecs.Count.ToString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Extractor Definition Name:         ");
                    _msg.Append(dt.TableName);
                    WriteMessageToLog(_msg.ToString());
                    throw new System.Exception(_msg.ToString());
                }

                colSpecs = newColSpecs;
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }



        }//end method

        /// <summary>
        /// Displays the form used to define filters.
        /// </summary>
        /// <param name="dt">DataTable containing the data to be filtered.</param>
        /// <param name="extractorDefinitionFilter">Filter definition object.</param>
        /// <param name="queryText">Text of the SQL query used to produce the DataTable.</param>
        /// <returns>Object containing filter definition.</returns>
        public PFFilter ShowFilterForm(DataTable dt, PFFilter extractorDefinitionFilter, string queryText)
        {
            PFFilter filter = null;
            PFFilterForm frm = new PFFilterForm();
            DialogResult res = DialogResult.None;


            try
            {
                frm.QueryText = queryText;
                frm.QueryDataTable = dt;
                frm.Filter = extractorDefinitionFilter;

                frm.MessageLogUI = _messageLog;
                res = frm.ShowDialog();

                filter = frm.Filter;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            return filter;
        }

        /// <summary>
        /// Shows form for defining fixed len texs line column widths.
        /// </summary>
        /// <param name="colDefs">List of column definitions. This will be updated by the form.</param>
        /// <param name="defaultSourceFolder">Default path for folder containing fixed length text files.</param>
        /// <param name="fixedLengthTextFilePath">Selected path to file to be processed.</param>
        /// <param name="lineTerminatorUsed">Set to true if a line terminator is used in the text file.</param>
        /// <param name="columnNamesOnFirstLine">Set to true if first line of file contains column names.</param>
        /// <param name="expectedLineLength">Used to determine how many butes to read when file is first loaded and lines to be parsed are setup.</param>
        /// <remarks>Columns can be added or deleted on this form.</remarks>
        public void ShowFixedLenInputColSpecForm(ref PFColumnDefinitionsExt colDefs,
                                                 string defaultSourceFolder,
                                                 string fixedLengthTextFilePath,
                                                 bool lineTerminatorUsed,
                                                 bool columnNamesOnFirstLine,
                                                 int expectedLineLength)
        {
            PFFixedLenColDefsInputForm frm = new PFFixedLenColDefsInputForm();
            DialogResult res = DialogResult.None;


            try
            {
                frm.ColDefs = colDefs;

                frm.MessageLogUI = _messageLog;

                frm.DefaultSourceFolder = defaultSourceFolder;
                frm.FixedLengthTextFilePath = fixedLengthTextFilePath;
                frm.LineTerminatorUsed = lineTerminatorUsed;
                frm.ColumnNamesOnFirstLine = columnNamesOnFirstLine;
                frm.ExpectedLineLength = expectedLineLength;
                this.ExpectedLineLength = expectedLineLength;

                res = frm.ShowDialog();

                _msg.Length = 0;
                _msg.Append("Fixed Length Column Definitions Form closed with DialogResult = ");
                _msg.Append(res.ToString());
                WriteMessageToLog(_msg.ToString());

                if (res == DialogResult.OK)
                {
                    this.ExpectedLineLength = frm.ExpectedLineLength;
                    colDefs = frm.ColDefs;
                    _msg.Length = 0;
                    _msg.Append(Environment.NewLine);
                    _msg.Append("COLUMN DEFINITIONS:\r\n\r\n");
                    _msg.Append(frm.ColDefs.ToXmlString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append(Environment.NewLine);
                    WriteMessageToLog(_msg.ToString());
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (frm != null)
                {
                    if (FormIsOpen(frm.Name))
                        frm.Close();
                }
                frm = null;
            }
                 
        
        }

        /// <summary>
        /// Shows form for displaying fixed len texs line column widths.
        /// </summary>
        /// <param name="exdef">Extractor definition object.</param>
        /// <param name="colDefs">Object containing column definitions. These definitions can be updated from the form.</param>
        /// <remarks>Columns cannot be added or deleted on this form but their lengths can be changed.</remarks>
        public void ShowFixedLenOutputColSpecForm(PFExtractorDefinition exdef, ref PFColumnDefinitionsExt colDefs)
        {
            PFFixedLenColDefsOutputForm frm = new PFFixedLenColDefsOutputForm();
            DialogResult res = DialogResult.None;
            DataTable dt = null;
            PFColumnDefinitionsExt colDefsFromDataTable = null;
            PFColumnDefinitionsExt colDefsSynched = null;

            try
            {
                switch (exdef.ExtractorSource)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        dt = GetQueryDefSchema(exdef.RelationalDatabaseSource, exdef.ExtractorName);
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        dt = GetQueryDefSchema(exdef.MsAccessSource, exdef.ExtractorName);
                        break;
                    //added September 2015
                    case enExtractorDataLocation.ExcelDataFile:
                        dt = GetExcelQueryDefSchema(exdef, exdef.ExtractorName);
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        dt = GetDelimitedTextFileTableSchema(exdef, exdef.ExtractorName);
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        dt = GetFixedLengthTextFileTableSchema(exdef, exdef.ExtractorName);
                        break;
                    case enExtractorDataLocation.XMLFile:
                        dt = GetXMLFileTableSchema(exdef, exdef.ExtractorName);
                        break;
                    //end added September 2015
                    default:
                        break;
                }


                
                if (dt == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to retrieve a DataTable with schema information for ");
                    _msg.Append(exdef.ExtractorName);
                    _msg.Append(" ");
                    _msg.Append(exdef.ExtractorSource.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }

                if (dt.Columns == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to retrieve a data columns schema information for ");
                    _msg.Append(exdef.ExtractorName);
                    _msg.Append(" ");
                    _msg.Append(exdef.ExtractorSource.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }

                if (dt.Columns.Count == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to retrieve any data columns information for ");
                    _msg.Append(exdef.ExtractorName);
                    _msg.Append(" ");
                    _msg.Append(exdef.ExtractorSource.ToString());
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }

                colDefsFromDataTable = PFColumnDefinitionsExt.GetColumnDefinitionsExt(dt);
                colDefsSynched = SyncFixedLengthOutputColDefs(colDefs, colDefsFromDataTable);


                frm.ColDefs = colDefsSynched;

                frm.MessageLogUI = _messageLog;

                
                res = frm.ShowDialog();

                _msg.Length = 0;
                _msg.Append("Fixed Length Column Definitions Form closed with DialogResult = ");
                _msg.Append(res.ToString());
                WriteMessageToLog(_msg.ToString());

                if (res == DialogResult.OK)
                {
                    colDefs = frm.ColDefs;
                    _msg.Length = 0;
                    _msg.Append(Environment.NewLine);
                    _msg.Append("COLUMN DEFINITIONS:\r\n\r\n");
                    _msg.Append(frm.ColDefs.ToXmlString());
                    _msg.Append(Environment.NewLine);
                    _msg.Append(Environment.NewLine);
                    WriteMessageToLog(_msg.ToString());
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (frm != null)
                {
                    if (FormIsOpen(frm.Name))
                        frm.Close();
                }
                frm = null;
            }

        }

        private PFColumnDefinitionsExt SyncFixedLengthOutputColDefs(PFColumnDefinitionsExt colDefs, PFColumnDefinitionsExt colDefsFromDataTable)
        {
            PFColumnDefinitionsExt colDefsSynched = new PFColumnDefinitionsExt(colDefsFromDataTable.NumberOfColumns);
            bool syncValueFound = false;

            for (int i = 0; i < colDefsSynched.NumberOfColumns; i++)
            {
                syncValueFound = false;
                for (int cd = 0; cd < colDefs.NumberOfColumns; cd++)
                {
                    if (colDefsFromDataTable.ColumnDefinition[i].SourceColumnName == colDefs.ColumnDefinition[cd].SourceColumnName)
                    {
                        colDefsSynched.ColumnDefinition[i] = colDefs.ColumnDefinition[cd];
                        syncValueFound = true;
                        break;
                    }
                }
                if (syncValueFound == false)
                {
                    colDefsSynched.ColumnDefinition[i] = colDefsFromDataTable.ColumnDefinition[i];
                }
            }

            return colDefsSynched;
        }



        //test data generator routines

        /// <summary>
        /// Previews test data generator output using a grid.
        /// </summary>
        /// <param name="dataLocation">Database type.</param>
        /// <param name="exdef">Extractor definition object.</param>
        /// <param name="maxPreviewRows">Maximum number of rows to show on the output grid.</param>
        /// <param name="showRowNumber">Set to true to add a row number column to each row.</param>
        /// <param name="filterOutput">Set to true if the output on the grid is to be filtered.</param>
        /// <param name="randomizeOutput">Set to true if one or more of the output columns is to be reandomized before display on the grid.</param>
        /// <param name="randomizerBatchSize">Specifies number of random values to generate each time a new set of values the application can use is produced.</param>
        /// <param name="importExportBatchsize">Number of data rows to read and write as a single batch. This number will be overridden by the batch size limits for individual database providers.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Set this to true if you want database platform drop-down lists to only include data providers that are installed on the current system. If you decide to include a provider not installed on the system, you will almost certainly get an error when attempting to use the provider.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing output from data grid exports.</param>
        /// <param name="defaultOutputDatabaseType">Default database platform to display when form with a data destination Database Type drop-down list is first shown.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use when the Default Output Database Type for a data destination is set on one of the application’s data destination drop-down lists.</param>

        public void PreviewTestData(enExtractorDataLocation dataLocation,
                                    PFExtractorDefinition exdef,
                                    int maxPreviewRows,
                                    bool showRowNumber,
                                    bool filterOutput,
                                    bool randomizeOutput,
                                    int randomizerBatchSize,
                                    int importExportBatchsize,
                                    bool showInstalledDatabaseProvidersOnly,
                                    string defaultDataGridExportFolder,
                                    string defaultOutputDatabaseType,
                                    string defaultOutputDatabaseConnectionString)
        {
            DataTable tabSchema = null;
            PFList<DataTableRandomizerColumnSpec> colSpecs = null;
            
            try
            {
                switch (exdef.ExtractorSource)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        tabSchema = GetQueryDefSchema(exdef.RelationalDatabaseSource, exdef.ExtractorName);
                        colSpecs = exdef.RelationalDatabaseSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        tabSchema = GetQueryDefSchema(exdef.MsAccessSource, exdef.ExtractorName);
                        colSpecs = exdef.MsAccessSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.ExcelDataFile:
                        tabSchema = GetExcelQueryDefSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.MsExcelSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        tabSchema = GetDelimitedTextFileTableSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.DelimitedTextFileSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        tabSchema = GetFixedLengthTextFileTableSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.FixedLengthTextFileSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.XMLFile:
                        tabSchema = GetXMLFileTableSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.XmlFileSource.OutputOptions.RandomizerColSpecs;
                        break;
                    default:
                        break;
                }

                if (colSpecs == null)
                {
                    _msg.ToString();
                    _msg.Append("You must specify randomizer values for the test data columns.");
                    throw new System.Exception(_msg.ToString());
                }

                if (colSpecs.Count < 1)
                {
                    colSpecs = GetInitialColSpecList(exdef);
                    if (colSpecs == null)
                    {
                        _msg.ToString();
                        _msg.Append("Unable to create randomizer values for the test data columns.");
                        throw new System.Exception(_msg.ToString());
                    }
                    if (colSpecs.Count < 1)
                    {
                        _msg.ToString();
                        _msg.Append("Unable to create entries for initial column spec list.");
                        throw new System.Exception(_msg.ToString());
                    }
                }

                DataTable testData = CreateTestDataRows(tabSchema, maxPreviewRows);

                if (testData != null)
                {
                    RandomizeTestData(testData, colSpecs, randomizerBatchSize);

                    OutputDataTableToGrid(testData,
                                          maxPreviewRows,
                                          showRowNumber,
                                          filterOutput,
                                          randomizeOutput,
                                          showInstalledDatabaseProvidersOnly,
                                          defaultDataGridExportFolder,
                                          defaultOutputDatabaseType,
                                          defaultOutputDatabaseConnectionString
                                         );
                }
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

        }

        private void RandomizeTestData(DataTable testData,  PFList<DataTableRandomizerColumnSpec> colSpecs, int randomizerBatchSize)
        {
            PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = colSpecs.Copy();
            SyncColSpecsWithDataSchema(testData, ref colSpecs);
            DataTableRandomizer dtr = new DataTableRandomizer();
            dtr.MessageLogUI = this.MessageLogUI;
            dtr.RandomizeDataTableValues(testData, colSpecs, randomizerBatchSize);
            colSpecs = saveOrigColSpecs.Copy();   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
        }


        private DataTable CreateTestDataRows(DataTable tabSchema, int numTestDataRows)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            DataTable testData = tabSchema.Clone();


            try
            {
                for (int c = 0; c < testData.Columns.Count; c++)
                {
                    if (testData.Columns[c].ReadOnly == true)
                    {
                        testData.Columns[c].ReadOnly = false;
                    }

                    if (testData.Columns[c].AutoIncrement == true)
                    {
                        testData.Columns[c].AutoIncrement = false;
                    }

                    if (testData.PrimaryKey != null)
                    {
                        testData.PrimaryKey = null;
                    }

                    if (testData.Columns[c].Unique == true)
                    {
                        testData.Columns[c].Unique = false;
                    }

                    if (PFSystemTypeInfo.DataTypeIsString(testData.Columns[c].DataType))
                    {
                        if (testData.Columns[c].MaxLength < 5)
                        {
                            testData.Columns[c].DefaultValue = "X";
                        }
                        else
                        {
                            testData.Columns[c].DefaultValue = "XXXXX";
                        }
                    }
                    else if (PFSystemTypeInfo.DataTypeIsInteger(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = 0;
                    }
                    else if (PFSystemTypeInfo.DataTypeIsFloatingPoint(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = 0.0;
                    }
                    else if (PFSystemTypeInfo.DataTypeIsDecimal(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = 0.0;
                    }
                    else if (PFSystemTypeInfo.DataTypeIsBoolean(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = false;
                    }
                    else if (PFSystemTypeInfo.DataTypeIsDateTime(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = DateTime.Now;
                    }
                    else if (PFSystemTypeInfo.DataTypeIsByte(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = 0;
                    }
                    else if (PFSystemTypeInfo.DataTypeIsChar(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = 'A';
                    }
                    else if (PFSystemTypeInfo.DataTypeIsGuid(testData.Columns[c].DataType))
                    {
                        testData.Columns[c].DefaultValue = Guid.NewGuid();
                    }
                    else
                    {
                        if (testData.Columns[c].AllowDBNull)
                        {
                            testData.Columns[c].DefaultValue = DBNull.Value;
                        }
                        else
                        {
                            ;
                        }
                    }
                }//end for

                for (int r = 0; r < numTestDataRows; r++)
                {
                    DataRow dr = testData.NewRow();

                    for (int c = 0; c < testData.Columns.Count; c++)
                    {
                        if (testData.Columns[c].ReadOnly == false && testData.Columns[c].AutoIncrement == false && testData.Columns[c].AutoIncrement == false)
                        {
                            dr[c] = testData.Columns[c].DefaultValue;
                        }
                    }

                    testData.Rows.Add(dr);

                }//end for

                sw.Stop();
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append(numTestDataRows.ToString());
                _msg.Append(" test data rows generated. Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(".");
                WriteMessageToLog(_msg.ToString());
            }
                 
            return testData;
        }


        /// <summary>
        /// Routine to generate test data to destination specified in the extractor definition.
        /// </summary>
        /// <param name="exdef">Extractor definition object.</param>
        /// <param name="numRowsToGenerate">Number of rows with test data to generate.</param>
        /// <param name="showRowNumber">Set to true to add a row number column to each row.</param>
        /// <param name="filterOutput">Set to true if the output is to be filtered before being written to destination.</param>
        /// <param name="randomizeOutput">Set to true if one or more of the output columns is to be reandomized before output to destination.</param>
        /// <param name="randomizerBatchSize">Specifies number of random values to generate each time a new set of values the application can use is produced.</param>
        /// <param name="importExportBatchsize">Number of data rows to read and write as a single batch. This number will be overridden by the batch size limits for individual database providers.</param>
        public void GenerateTestData(PFExtractorDefinition exdef,
                                     int numRowsToGenerate,
                                     bool showRowNumber,
                                     bool filterOutput,
                                     bool randomizeOutput,
                                     int randomizerBatchSize,
                                     int importExportBatchsize)
        {
            DataTable tabSchema = null;
            PFList<DataTableRandomizerColumnSpec> colSpecs = null;
            Stopwatch sw = new Stopwatch();
            string outputObjectName = string.Empty;

            try
            {
                sw.Start();

                switch (exdef.ExtractorSource)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        tabSchema = GetQueryDefSchema(exdef.RelationalDatabaseSource, exdef.ExtractorName);
                        colSpecs = exdef.RelationalDatabaseSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        tabSchema = GetQueryDefSchema(exdef.MsAccessSource, exdef.ExtractorName);
                        colSpecs = exdef.MsAccessSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.ExcelDataFile:
                        tabSchema = GetExcelQueryDefSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.MsExcelSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        tabSchema = GetDelimitedTextFileTableSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.DelimitedTextFileSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        tabSchema = GetFixedLengthTextFileTableSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.FixedLengthTextFileSource.OutputOptions.RandomizerColSpecs;
                        break;
                    case enExtractorDataLocation.XMLFile:
                        tabSchema = GetXMLFileTableSchema(exdef, exdef.ExtractorName);
                        colSpecs = exdef.XmlFileSource.OutputOptions.RandomizerColSpecs;
                        break;
                    default:
                        break;
                }

                if (colSpecs == null)
                {
                    _msg.ToString();
                    _msg.Append("You must specify randomizer values for the test data columns.");
                    throw new System.Exception(_msg.ToString());
                }

                if (colSpecs.Count < 1)
                {
                    colSpecs = GetInitialColSpecList(exdef);
                    if (colSpecs == null)
                    {
                        _msg.ToString();
                        _msg.Append("Unable to create randomizer values for the test data columns.");
                        throw new System.Exception(_msg.ToString());
                    }
                    if (colSpecs.Count < 1)
                    {
                        _msg.ToString();
                        _msg.Append("Unable to create entries for initial column spec list.");
                        throw new System.Exception(_msg.ToString());
                    }
                }


                DataTable dt = CreateTestDataRows(tabSchema, numRowsToGenerate);

                if (dt != null)
                {
                    RandomizeTestData(dt, colSpecs, randomizerBatchSize);
                }


                if (showRowNumber)
                {
                    dt = AddRowNumberToDataTable(dt);
                }


                switch (exdef.ExtractorDestination)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        OutputDataTableToRelationalDatabase(dt, exdef, importExportBatchsize);
                        outputObjectName = exdef.RelationalDatabaseDestination.TableName;
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        OutputDataTableToAccessDatabaseFile(dt, exdef, importExportBatchsize);
                        outputObjectName = exdef.MsAccessDestination.DatabasePath;
                        break;
                    case enExtractorDataLocation.ExcelDataFile:
                        OutputDataTableToExcelDataFile(dt, exdef, importExportBatchsize);
                        outputObjectName = exdef.MsExcelDestination.DocumentFilePath;
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        OutputDataTableToDelimitedTextFile(dt, exdef, importExportBatchsize);
                        outputObjectName = exdef.DelimitedTextFileDestination.TextFilePath;
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        OutputDataTableToFixedLengthTextFile(dt, exdef, importExportBatchsize);
                        outputObjectName = exdef.FixedLengthTextFileDestination.TextFilePath;
                        break;
                    case enExtractorDataLocation.XMLFile:
                        OutputDataTableToXmlFile(dt, exdef, importExportBatchsize);
                        outputObjectName = exdef.XmlFileDestination.DestXmlFilePath;
                        break;
                    default:
                        dt = null;
                        _msg.Length = 0;
                        _msg.Append("OutputData not implemented for unknown data destination.");
                        WriteMessageToLog(_msg.ToString());
                        DisplayAppAlertMessage(_msg.ToString());
                        break;
                }

                sw.Stop();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("GenerateTestData finished. Elapsed time for extracting process: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("Output location: ");
                _msg.Append(outputObjectName);
                WriteMessageToLog(_msg.ToString());
                DisplayAppInfoMessage(_msg.ToString());

            }

        }


        /// <summary>
        /// Routine to generate realistic looking sales order tansactions.
        /// </summary>
        /// <param name="maxTxsToGenerate">Number of sales orders to generate.</param>
        /// <param name="randomOrdersDefinition">Object containing definition for the random order generation.</param>
        /// <param name="generatorMessages">StringBuilder that will contain messages generated during processing.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Set this to true if you want database platform drop-down lists to only include data providers that are installed on the current system. If you decide to include a provider not installed on the system, you will almost certainly get an error when attempting to use the provider.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing output from data grid exports.</param>
        /// <param name="defaultOutputDatabaseType">Default database platform to display when form with a data destination Database Type drop-down list is first shown.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use when the Default Output Database Type for a data destination is set on one of the application’s data destination drop-down lists.</param>
        /// <remakrs>Final four parameters are only used for preview processing.</remakrs>
        public void GenerateSalesOrderTransactions(int maxTxsToGenerate, 
                                                   PFRandomOrdersDefinition randomOrdersDefinition,
                                                   StringBuilder generatorMessages,
                                                   //following parameters used only by Preview processing
                                                   bool showInstalledDatabaseProvidersOnly,
                                                   string defaultDataGridExportFolder,
                                                   string defaultOutputDatabaseType,
                                                   string defaultOutputDatabaseConnectionString)

        {
            DataTable salesHeaderTable = null;
            DataTable salesDetailTable = null;
            DataColumn[] headerKeys = new DataColumn[1];
            DataColumn[] detailKeys = new DataColumn[2];
            PFList<string> masterDataTableList = new PFList<string>();
            PFList<string> detailDataTableList = new PFList<string>();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                bool testDbExists = VerifyTestOrdersDatabaseExists();
                if (testDbExists == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to run GenerateSalesOrderTransactions. Database with test data does not exist: ");
                    _msg.Append(_testOrdersDatabase);
                    throw new System.Exception(_msg.ToString());
                }

                //load SalesOrderHeader to DataTable
                salesHeaderTable = GetTransactionTemplateDataTable(_salesOrderHeaderQuery, "SalesOrderHeader");
                //load SalesOrderDetail to DataTable
                headerKeys[0] = salesHeaderTable.Columns[0];
                salesHeaderTable.PrimaryKey = headerKeys;
                //load SalesOrderDetail to DataTable
                salesDetailTable = GetTransactionTemplateDataTable(_salesOrderDetailQuery, "SalesOrderDetail");
                //detailKeys[0] = salesDetailTable.Columns[1];
                //detailKeys[1] = salesDetailTable.Columns[2];
                if (randomOrdersDefinition.OutputDatabasePlatform == DatabasePlatform.SQLAnywhereUltraLite.ToString())
                {
                    //only one primary key column allowed for UltraLite database
                    detailKeys = null;
                    detailKeys = new DataColumn[1];
                    detailKeys[0] = salesDetailTable.Columns[0];
                }
                else
                {
                    detailKeys[0] = salesDetailTable.Columns[1];
                    detailKeys[1] = salesDetailTable.Columns[2];
                }
                salesDetailTable.PrimaryKey = detailKeys;

                //generate test orders tables
                stMasterDetailGeneratorResult generateResult = GenerateMasterDetailTransactionTables(masterDataTableList, 
                                                                                                     detailDataTableList, 
                                                                                                     salesHeaderTable, 
                                                                                                     salesDetailTable, 
                                                                                                     enTestOrdersType.SalesOrder, 
                                                                                                     (int)1,    //master table key column number
                                                                                                     maxTxsToGenerate, 
                                                                                                     randomOrdersDefinition);
                if (masterDataTableList.Count > 0)
                {
                    if (maxTxsToGenerate > 0)
                    {
                        //preview transactions was requested
                        PreviewTestOrdersDataTableList(masterDataTableList,
                                                       maxTxsToGenerate,
                                                       showInstalledDatabaseProvidersOnly,
                                                       defaultDataGridExportFolder,
                                                       defaultOutputDatabaseType,
                                                       defaultOutputDatabaseConnectionString);
                        PreviewTestOrdersDataTableList(detailDataTableList,
                                                       maxTxsToGenerate,
                                                       showInstalledDatabaseProvidersOnly,
                                                       defaultDataGridExportFolder,
                                                       defaultOutputDatabaseType,
                                                       defaultOutputDatabaseConnectionString);
                    }
                    else
                    {
                        //save generated data to destination
                        SaveDataTableListToDestination(masterDataTableList,
                                                       randomOrdersDefinition.TableSchema,
                                                       randomOrdersDefinition.SalesOrderHeadersTableName,
                                                       randomOrdersDefinition);
                        SaveDataTableListToDestination(detailDataTableList,
                                                       randomOrdersDefinition.TableSchema,
                                                       randomOrdersDefinition.SalesOrderDetailsTableName,
                                                       randomOrdersDefinition);

                        _msg.Length = 0;
                        _msg.Append("Number of sales order headers generated: ");
                        _msg.Append(generateResult.NumMasterRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Number of sales order details generated: ");
                        _msg.Append(generateResult.NumDetailRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Sales order headers table Name:          ");
                        _msg.Append(GetFullTableName(randomOrdersDefinition.TableSchema, randomOrdersDefinition.SalesOrderHeadersTableName));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Sales order details table Name:          ");
                        _msg.Append(GetFullTableName(randomOrdersDefinition.TableSchema, randomOrdersDefinition.SalesOrderDetailsTableName));
                        WriteMessageToLog(_msg.ToString());
                        _msg.Append(Environment.NewLine);
                        generatorMessages.Append(_msg.ToString());
                    }
                }
                else
                {
                    //masterDataTableList.Count <= 0
                    _msg.Length = 0;
                    _msg.Append("No transaction data was generated: GenerateSalesOrderTransactions");
                    _msg.Append(Environment.NewLine);
                    WriteMessageToLog(_msg.ToString());
                    _msg.Append(Environment.NewLine);
                    generatorMessages.Append(_msg.ToString());
                }

                sw.Stop();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (masterDataTableList != null)
                {
                    if (masterDataTableList.Count > 0)
                    {
                        DeleteTempFiles(masterDataTableList);
                    }
                }
                if (detailDataTableList != null)
                {
                    if (detailDataTableList.Count > 0)
                    {
                        DeleteTempFiles(detailDataTableList);
                    }
                }

                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("GenerateSalesOrderTransactions finished.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());
                _msg.Append(Environment.NewLine);
                generatorMessages.Append(_msg.ToString());
            }


        }

        /// <summary>
        /// Routine to generate realistic looking purchase order tansactions.
        /// </summary>
        /// <param name="maxTxsToGenerate">Number of sales orders to generate.</param>
        /// <param name="randomOrdersDefinition">Object containing definition for the random order generation.</param>
        /// <param name="generatorMessages">StringBuilder that will contain messages generated during processing.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Set this to true if you want database platform drop-down lists to only include data providers that are installed on the current system. If you decide to include a provider not installed on the system, you will almost certainly get an error when attempting to use the provider.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing output from data grid exports.</param>
        /// <param name="defaultOutputDatabaseType">Default database platform to display when form with a data destination Database Type drop-down list is first shown.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use when the Default Output Database Type for a data destination is set on one of the application’s data destination drop-down lists.</param>
        /// <remakrs>Final four parameters are only used for preview processing.</remakrs>
        public void GeneratePurchaseOrderTransactions(int maxTxsToGenerate, 
                                                      PFRandomOrdersDefinition randomOrdersDefinition,
                                                      StringBuilder generatorMessages,
                                                      //following parameters used only by Preview processing
                                                      bool showInstalledDatabaseProvidersOnly,
                                                      string defaultDataGridExportFolder,
                                                      string defaultOutputDatabaseType,
                                                      string defaultOutputDatabaseConnectionString)
        {

            DataTable purchaseHeaderTable = null;
            DataTable purchaseDetailTable = null;
            DataColumn[] headerKeys = new DataColumn[1];
            DataColumn[] detailKeys = new DataColumn[2];
            PFList<string> masterDataTableList = new PFList<string>();
            PFList<string> detailDataTableList = new PFList<string>();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                bool testDbExists = VerifyTestOrdersDatabaseExists();
                if (testDbExists == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to run GeneratePurchaseOrderTransactions. Database with test data does not exist: ");
                    _msg.Append(_testOrdersDatabase);
                    throw new System.Exception(_msg.ToString());
                }

                //load PurchaseOrderHeader to DataTable
                purchaseHeaderTable = GetTransactionTemplateDataTable(_purchaseOrderHeaderQuery, "PurchaseOrderHeader");
                //load PurchaseOrderDetail to DataTable
                headerKeys[0] = purchaseHeaderTable.Columns[0];
                purchaseHeaderTable.PrimaryKey = headerKeys;
                //load PurchaseOrderDetail to DataTable
                purchaseDetailTable = GetTransactionTemplateDataTable(_purchaseOrderDetailQuery, "PurchaseOrderDetail");
                //detailKeys[0] = purchaseDetailTable.Columns[1];
                //detailKeys[1] = purchaseDetailTable.Columns[2];
                if (randomOrdersDefinition.OutputDatabasePlatform == DatabasePlatform.SQLAnywhereUltraLite.ToString())
                {
                    //only one primary key column allowed for UltraLite database
                    detailKeys = null;
                    detailKeys = new DataColumn[1];
                    detailKeys[0] = purchaseDetailTable.Columns[0];
                }
                else
                {
                    detailKeys[0] = purchaseDetailTable.Columns[1];
                    detailKeys[1] = purchaseDetailTable.Columns[2];
                }
                purchaseDetailTable.PrimaryKey = detailKeys;

                //generate test orders tables
                stMasterDetailGeneratorResult generateResult = GenerateMasterDetailTransactionTables(masterDataTableList,
                                                                                                     detailDataTableList,
                                                                                                     purchaseHeaderTable,
                                                                                                     purchaseDetailTable,
                                                                                                     enTestOrdersType.PurchaseOrder,
                                                                                                     (int)1,    //master table key column number
                                                                                                     maxTxsToGenerate,
                                                                                                     randomOrdersDefinition);
                if (masterDataTableList.Count > 0)
                {
                    if (maxTxsToGenerate > 0)
                    {
                        //preview transactions was requested
                        PreviewTestOrdersDataTableList(masterDataTableList,
                                                       maxTxsToGenerate,
                                                       showInstalledDatabaseProvidersOnly,
                                                       defaultDataGridExportFolder,
                                                       defaultOutputDatabaseType,
                                                       defaultOutputDatabaseConnectionString);
                        PreviewTestOrdersDataTableList(detailDataTableList,
                                                       maxTxsToGenerate,
                                                       showInstalledDatabaseProvidersOnly,
                                                       defaultDataGridExportFolder,
                                                       defaultOutputDatabaseType,
                                                       defaultOutputDatabaseConnectionString);
                    }
                    else
                    {
                        //save generated data to destination
                        SaveDataTableListToDestination(masterDataTableList,
                                                       randomOrdersDefinition.TableSchema,
                                                       randomOrdersDefinition.PurchaseOrderHeadersTableName,
                                                       randomOrdersDefinition);
                        SaveDataTableListToDestination(detailDataTableList,
                                                       randomOrdersDefinition.TableSchema,
                                                       randomOrdersDefinition.PurchaseOrderDetailsTableName,
                                                       randomOrdersDefinition);

                        _msg.Length = 0;
                        _msg.Append("Number of purchase order headers generated: ");
                        _msg.Append(generateResult.NumMasterRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Number of purchase order details generated: ");
                        _msg.Append(generateResult.NumDetailRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Purchase order headers table Name:          ");
                        _msg.Append(GetFullTableName(randomOrdersDefinition.TableSchema, randomOrdersDefinition.PurchaseOrderHeadersTableName));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Purchase order details table Name:          ");
                        _msg.Append(GetFullTableName(randomOrdersDefinition.TableSchema, randomOrdersDefinition.PurchaseOrderDetailsTableName));
                        WriteMessageToLog(_msg.ToString());
                        _msg.Append(Environment.NewLine);
                        generatorMessages.Append(_msg.ToString());
                    }
                }
                else
                {
                    //masterDataTableList.Count <= 0
                    _msg.Length = 0;
                    _msg.Append("No transaction data was generated: GeneratePurchaseOrderTransactions");
                    WriteMessageToLog(_msg.ToString());
                    _msg.Append(Environment.NewLine);
                    generatorMessages.Append(_msg.ToString());
                }

                sw.Stop();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (masterDataTableList != null)
                {
                    if (masterDataTableList.Count > 0)
                    {
                        DeleteTempFiles(masterDataTableList);
                    }
                }
                if (detailDataTableList != null)
                {
                    if (detailDataTableList.Count > 0)
                    {
                        DeleteTempFiles(detailDataTableList);
                    }
                }

                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("GeneratePurchaseOrderTransactions finished.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());
                _msg.Append(Environment.NewLine);
                generatorMessages.Append(_msg.ToString());
            }

        }


        private stMasterDetailGeneratorResult GenerateMasterDetailTransactionTables(PFList<string> masterDataTableList,
                                                                                    PFList<string> detailDataTableList,
                                                                                    DataTable masterTableTemplate,
                                                                                    DataTable detailTableTemplate,
                                                                                    enTestOrdersType testOrdersType,
                                                                                    int masterKeyColNum,
                                                                                    int maxTxsToGenerate, 
                                                                                    PFRandomOrdersDefinition randomOrdersDefinition)
        {
            stMasterDetailGeneratorResult ret = new stMasterDetailGeneratorResult(0, 0);
            int numMasterRowsGenerated = 0;
            int numDetailRowsGenerated = 0;
            int minNumMasterRowsPerDate = 0;
            int maxNumMasterRowsPerDate = 0;
            DateTime earliestTransactionDate = DateTime.MinValue;
            DateTime latestTransactionDate = DateTime.MaxValue;
            DateTime currentTransactionDate = DateTime.MaxValue;
            DateTime currentModifiedDate = DateTime.MinValue;
            TimeSpan earliestTimePerDate = TimeSpan.MinValue;
            TimeSpan latestTimePerDate = TimeSpan.MaxValue;
            TimeSpan currentModifiedTime = TimeSpan.MaxValue;
            bool includeWeekendDays = true;
            RandomNumber rn = new RandomNumber();
            //RandomString rs = new RandomString();
            DataTable outputMasterTable = null;
            DataTable outputDetailTable = null;
            string masterPrimaryKeyColName = string.Empty;
            string masterTableName = string.Empty;
            string detailTableName = string.Empty;

            try
            {
                if (testOrdersType != enTestOrdersType.SalesOrder && testOrdersType != enTestOrdersType.PurchaseOrder)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid or unexpected test order type submitted to GenerateMasterDetailTransactionTables routine.");
                    throw new System.Exception(_msg.ToString());
                }
                earliestTransactionDate = PFTextProcessor.ConvertStringToDateTime(randomOrdersDefinition.EarliestTransactionDate, new DateTime(2000, 1, 1));
                latestTransactionDate = PFTextProcessor.ConvertStringToDateTime(randomOrdersDefinition.LatestTransactionDate, new DateTime(2014, 12, 31));
                earliestTimePerDate = PFTextProcessor.ConvertStringToTimeSpan(randomOrdersDefinition.MinTimePerDate, new TimeSpan(2,0,0));
                latestTimePerDate = PFTextProcessor.ConvertStringToTimeSpan(randomOrdersDefinition.MaxTimePerDate, new TimeSpan(20,0,0));
                includeWeekendDays = randomOrdersDefinition.IncludeWeekendDays;
                if(testOrdersType == enTestOrdersType.SalesOrder)
                {
                    minNumMasterRowsPerDate = PFTextProcessor.ConvertStringToInt(randomOrdersDefinition.MinNumSalesOrdersPerDate, 0);
                    maxNumMasterRowsPerDate = PFTextProcessor.ConvertStringToInt(randomOrdersDefinition.MaxNumSalesOrdersPerDate, 5);
                }
                else
                {
                    minNumMasterRowsPerDate = PFTextProcessor.ConvertStringToInt(randomOrdersDefinition.MinNumPurchaseOrdersPerDate, 0);
                    maxNumMasterRowsPerDate = PFTextProcessor.ConvertStringToInt(randomOrdersDefinition.MaxNumPurchaseOrdersPerDate, 2);
                }

                currentTransactionDate = earliestTransactionDate;
                masterPrimaryKeyColName = masterTableTemplate.Columns[masterKeyColNum].ColumnName;
                DataView detailView = new DataView(detailTableTemplate, "", masterPrimaryKeyColName, DataViewRowState.CurrentRows);
                outputMasterTable = CreateOutputMasterTable(masterTableTemplate, testOrdersType, randomOrdersDefinition);
                outputDetailTable = CreateOutputDetailTable(detailTableTemplate, testOrdersType, randomOrdersDefinition);
                masterTableName = testOrdersType == enTestOrdersType.SalesOrder ? randomOrdersDefinition.SalesOrderHeadersTableName : randomOrdersDefinition.PurchaseOrderHeadersTableName;
                detailTableName = testOrdersType == enTestOrdersType.SalesOrder ? randomOrdersDefinition.SalesOrderDetailsTableName : randomOrdersDefinition.PurchaseOrderDetailsTableName;
                int outputOrderId = 100000;
                int outputOrderDetailId = 0;
                int outputMasterRowNumber = 0;
                int outputDetailRowNumber = 0;
                DateTime dueDate = DateTime.MaxValue;
                DateTime shipDate = DateTime.MaxValue;
                int previousMasterTableCheckpoint = 0;
                int previousDetailTableCheckpoint = 0;
                int minSeconds = Convert.ToInt32(earliestTimePerDate.TotalSeconds);
                int maxSeconds = Convert.ToInt32(latestTimePerDate.TotalSeconds);
                int totSeconds = 0;
                int maxOutputTransactions =  maxTxsToGenerate > 0 ? maxTxsToGenerate : int.MaxValue;

                while (currentTransactionDate <= latestTransactionDate)
                {
                    if (includeWeekendDays == false)
                    {
                        if (currentTransactionDate.DayOfWeek == DayOfWeek.Saturday
                            || currentTransactionDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            currentTransactionDate = currentTransactionDate.AddDays(1.0);
                            continue;
                        }
                    }

                    dueDate = currentTransactionDate.AddDays(7);
                    if (includeWeekendDays == false)
                    {
                        if (dueDate.DayOfWeek == DayOfWeek.Saturday
                            || dueDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            dueDate = currentTransactionDate.AddDays(2.0);
                            continue;
                        }
                    }

                    shipDate = currentTransactionDate.AddDays(5);
                    if (includeWeekendDays == false)
                    {
                        if (shipDate.DayOfWeek == DayOfWeek.Saturday
                            || shipDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            shipDate = currentTransactionDate.AddDays(2.0);
                            continue;
                        }
                    }

                    int numMasterRowsForDate = rn.GenerateRandomInt(minNumMasterRowsPerDate, maxNumMasterRowsPerDate);
                    if (maxTxsToGenerate > 0)
                    {
                        //see if exceeded preview limit
                        if ((numMasterRowsGenerated + numMasterRowsForDate) > maxOutputTransactions)
                        {
                            numMasterRowsForDate = maxOutputTransactions - numMasterRowsGenerated;
                        }
                    }

                    for (int masterInx = 0; masterInx < numMasterRowsForDate; masterInx++)
                    {
                        int masterKey = rn.GenerateRandomInt(1, masterTableTemplate.Rows.Count);
                        DataRow masterRow = masterTableTemplate.Rows.Find(masterKey);
                        DataRow newMasterRow = outputMasterTable.NewRow();

                        numMasterRowsGenerated++;
                        outputOrderId++;
                        outputMasterRowNumber++;


                        totSeconds = rn.GenerateRandomInt(minSeconds, maxSeconds);
                        currentModifiedTime = new TimeSpan(0, 0, totSeconds);
                        currentModifiedDate = new DateTime(currentTransactionDate.Year, currentTransactionDate.Month, currentTransactionDate.Day,
                                                           currentModifiedTime.Hours, currentModifiedTime.Minutes, currentModifiedTime.Seconds);

                        for (int i = 0; i < outputMasterTable.Columns.Count; i++)
                        {
                            newMasterRow[i] = masterRow[i];
                        }
                        if (testOrdersType == enTestOrdersType.SalesOrder)
                        {
                            newMasterRow["dgvRowNumber"] = outputMasterRowNumber;
                            newMasterRow["SalesOrderId"] = outputOrderId;
                            newMasterRow["OrderDate"] = currentModifiedDate;
                            newMasterRow["DueDate"] = dueDate;
                            newMasterRow["ShipDate"] = shipDate;
                            //newMasterRow["SalesComment"] = rs.GetRandomSyllablesUCLC(rn.GenerateRandomInt(1, 3));
                            newMasterRow["rowguid"] = Guid.NewGuid();
                            newMasterRow["ModifiedDate"] = currentModifiedDate;
                        }
                        else //if (testOrdersType == enTestOrdersType.PurchaseOrder)
                        {
                            newMasterRow["dgvRowNumber"] = outputMasterRowNumber;
                            newMasterRow["PurchaseOrderId"] = outputOrderId;
                            newMasterRow["OrderDate"] = currentModifiedDate;
                            newMasterRow["ShipDate"] = shipDate;
                            newMasterRow["ModifiedDate"] = currentModifiedDate;
                        }

                        outputMasterTable.Rows.Add(newMasterRow);

                        DataRowView[] detailRows = detailView.FindRows(masterRow[1]);
                        for (int detailInx = 0; detailInx < detailRows.Length; detailInx++)
                        {
                            outputDetailRowNumber++;
                            outputOrderDetailId++;
                            numDetailRowsGenerated++;

                            DataRow detailRow = detailRows[detailInx].Row;
                            DataRow newDetailRow = outputDetailTable.NewRow();
                            for (int i = 0; i < outputDetailTable.Columns.Count; i++)
                            {
                                newDetailRow[i] = detailRow[i];
                            }
                            if (testOrdersType == enTestOrdersType.SalesOrder)
                            {
                                newDetailRow["dgvRowNumber"] = outputDetailRowNumber;
                                newDetailRow["SalesOrderId"] = outputOrderId;
                                newDetailRow["SalesOrderDetailId"] = outputOrderDetailId;
                                newDetailRow["rowguid"] = Guid.NewGuid();
                                newDetailRow["ModifiedDate"] = currentModifiedDate;
                            }
                            else //if (testOrdersType == enTestOrdersType.PurchaseOrder)
                            {
                                newDetailRow["dgvRowNumber"] = outputDetailRowNumber;
                                newDetailRow["PurchaseOrderId"] = outputOrderId;
                                newDetailRow["PurchaseOrderDetailId"] = outputOrderDetailId;
                                newDetailRow["DueDate"] = dueDate;
                                newDetailRow["ModifiedDate"] = currentModifiedDate;
                            }

                            outputDetailTable.Rows.Add(newDetailRow);
                        }
                    }

                    if ((numMasterRowsGenerated - previousMasterTableCheckpoint) > _maxTempDataTableRows)
                    {
                        //save existing master tables and add file names to list of master data tables
                        SaveDataTableToList(masterDataTableList, outputMasterTable, randomOrdersDefinition.TableSchema, masterTableName, ".pfdexShdr");

                        previousMasterTableCheckpoint = numMasterRowsGenerated;

                        //reset table containing output rows
                        outputMasterTable.Dispose();
                        outputMasterTable = null;
                        outputMasterTable = CreateOutputMasterTable(masterTableTemplate, testOrdersType, randomOrdersDefinition);
                    }

                    if ((numDetailRowsGenerated - previousDetailTableCheckpoint) > _maxTempDataTableRows)
                    {
                        //save existing detail tables and add file names to list of detail data tables
                        SaveDataTableToList(detailDataTableList, outputDetailTable, randomOrdersDefinition.TableSchema, detailTableName, ".pfdexSdtl");
                        
                        previousDetailTableCheckpoint = numDetailRowsGenerated;

                        //reset table containing output rows
                        outputDetailTable.Dispose();
                        outputDetailTable = null;
                        outputDetailTable = CreateOutputDetailTable(detailTableTemplate, testOrdersType, randomOrdersDefinition);
                    }

                    if (numMasterRowsGenerated < maxOutputTransactions)
                    {
                        currentTransactionDate = currentTransactionDate.AddDays(1.0);
                    }
                    else
                    {
                        //end the loop: this will occurs for a preview request
                        currentTransactionDate = latestTransactionDate.AddDays(1.0);
                    }
                }

                //save any remaining rows in master and detail tables and add file names to lists of data tables
                if (outputMasterTable.Rows.Count > 0)
                {
                    SaveDataTableToList(masterDataTableList, outputMasterTable, randomOrdersDefinition.TableSchema, masterTableName, ".pfdexShdr");
                    outputMasterTable.Dispose();
                    outputMasterTable = null;
                }

                if (outputDetailTable.Rows.Count > 0)
                {
                    SaveDataTableToList(detailDataTableList, outputDetailTable, randomOrdersDefinition.TableSchema, detailTableName, ".pfdexSdtl");
                    outputDetailTable.Dispose();
                    outputDetailTable = null;
                }
                
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            ret.NumMasterRowsGenerated = numMasterRowsGenerated;
            ret.NumDetailRowsGenerated = numDetailRowsGenerated;
            return ret;
        }

        private void SaveDataTableToList(PFList<string> dtList, DataTable dtSource, string schemaName, string tableName, string tempFileExtension)
        {
            string tempFolder = Path.GetTempPath();
            string tempFileName = Guid.NewGuid().ToString().ToUpper() + tempFileExtension;
            string fileName = System.IO.Path.GetFileNameWithoutExtension(tempFileName);
            string tempFullPath = Path.Combine(tempFolder, tempFileName);

            dtSource.TableName = GetFullTableName(schemaName, tableName);
            dtSource.WriteXml(tempFullPath, XmlWriteMode.WriteSchema);

            dtList.Add(tempFullPath);
        }


        private DataTable CreateOutputMasterTable(DataTable masterTableTemplate, enTestOrdersType testOrdersType, PFRandomOrdersDefinition randomOrdersDefinition)
        {
            DataTable outputMasterTable = masterTableTemplate.Clone();
            outputMasterTable.Columns[0].ColumnName = "dgvRowNumber";
            if (testOrdersType == enTestOrdersType.SalesOrder)
            {
                outputMasterTable.TableName = randomOrdersDefinition.SalesOrderHeadersTableName;
            }
            else //if (testOrdersType == enTestOrdersType.PurchaseOrder)
            {
                outputMasterTable.TableName = randomOrdersDefinition.PurchaseOrderHeadersTableName;
            }
            //else if (testOrdersType == enTestOrdersType.InternetSales)
            //{
            //    outputMasterTable.TableName = randomOrdersDefinition.DwInternetSalesTableName + "Master";
            //}
            //else //if (testOrdersType == enTestOrdersType.ResellerSales)
            //{
            //    outputMasterTable.TableName = randomOrdersDefinition.DwResellerSalesTableName + "Master";
            //}
            return outputMasterTable;
        }

        private DataTable CreateOutputDetailTable(DataTable detailTableTemplate, enTestOrdersType testOrdersType, PFRandomOrdersDefinition randomOrdersDefinition)
        {
            DataTable outputDetailTable = detailTableTemplate.Clone();
            outputDetailTable.Columns[0].ColumnName = "dgvRowNumber";
            if (testOrdersType == enTestOrdersType.SalesOrder)
            {
                outputDetailTable.TableName = randomOrdersDefinition.SalesOrderDetailsTableName;
            }
            else if (testOrdersType == enTestOrdersType.PurchaseOrder)
            {
                outputDetailTable.TableName = randomOrdersDefinition.PurchaseOrderDetailsTableName;
            }
            else if (testOrdersType == enTestOrdersType.InternetSales)
            {
                outputDetailTable.TableName = randomOrdersDefinition.DwInternetSalesTableName;
            }
            else //if (testOrdersType == enTestOrdersType.ResellerSales)
            {
                outputDetailTable.TableName = randomOrdersDefinition.DwResellerSalesTableName;
            }
            return outputDetailTable;
        }

        private void PreviewTestOrdersDataTableList(PFList<string> dtList,
                                                    int maxPreviewRows,
                                                    bool showInstalledDatabaseProvidersOnly,
                                                    string defaultDataGridExportFolder,
                                                    string defaultOutputDatabaseType,
                                                    string defaultOutputDatabaseConnectionString)
        {
            if (dtList.Count == 0)
                return;

            DataTable dt = new DataTable();

            PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
            grid.ShowInstalledDatabaseProvidersOnly = showInstalledDatabaseProvidersOnly;
            grid.DefaultOutputDatabaseType = defaultOutputDatabaseType;
            grid.DefaultOutputDatabaseConnectionString = defaultOutputDatabaseConnectionString;
            grid.ShowRowNumber = false;
            grid.MessageLogUI = this.MessageLogUI;
            if (Directory.Exists(defaultDataGridExportFolder) == false)
            {
                grid.DefaultGridExportFolder = _initDataGridExportFolder;
            }
            else
            {
                grid.DefaultGridExportFolder = defaultDataGridExportFolder;
            }

            dt.Rows.Clear();
            dt.ReadXml(dtList[0]);
            grid.WriteDataToGrid(dt);


        }

        private void SaveDataTableListToDestination(PFList<string> dataTableList, string tableSchema, string tableName,  PFRandomOrdersDefinition randomOrdersDefinition)
        {
            if (dataTableList.Count == 0)
                return;
            DatabasePlatform dbPlatform = DatabasePlatform.Unknown;
            enAccessVersion accVersion = enAccessVersion.NotSpecified;
            string tabName = string.Empty;

            try
            {
                tabName = GetFullTableName(tableSchema, tableName);
                if (randomOrdersDefinition.OutputDatabaseLocation == ExtractorDataTypeList.ExtractorDataLocations[ExtractorDataTypeList.RelationalDatabaseListIndex])
                {
                    dbPlatform = (DatabasePlatform)Enum.Parse(typeof(DatabasePlatform), randomOrdersDefinition.OutputDatabasePlatform);
                    ExportToDatabaseTable(dataTableList, dbPlatform, randomOrdersDefinition.OutputDatabaseConnection, tabName, randomOrdersDefinition.DatabaseOutputBatchSize, randomOrdersDefinition.ReplaceExistingTables);
                }
                else if (randomOrdersDefinition.OutputDatabaseLocation == ExtractorDataTypeList.ExtractorDataLocations[ExtractorDataTypeList.AccessDatabaseFileListIndex])
                {
                    accVersion = (enAccessVersion)Enum.Parse(typeof(enAccessVersion), randomOrdersDefinition.OutputDatabasePlatform);
                    //do not replace an existing file: this might be the second, third or more pass through this routine:  Let routines add tables
                    ExportToAccessFile(dataTableList, randomOrdersDefinition.OutputDatabaseConnection, tabName, false, accVersion, randomOrdersDefinition.AccessOutputUsername, randomOrdersDefinition.AccessOutputPassword);
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid or unsupported destination for test sales orders was specified: ");
                    _msg.Append(randomOrdersDefinition.OutputDatabaseLocation);
                    _msg.Append(".");
                    throw new System.Exception(_msg.ToString());
                }
            }
            catch //(System.Exception ex)
            {
                throw;
            }
            finally
            {
                ;
            }
        }

        private string GetFullTableName(string tableSchema, string tableName)
        {
            string fullTableName = string.Empty;

            if (tableSchema.Trim().Length > 0)
            {
                fullTableName = tableSchema + "." + tableName;
            }
            else
            {
                fullTableName = tableName;
            }

            return fullTableName;
        }

        private void ExportToDatabaseTable(PFList<string> dtList, DatabasePlatform dbPlat, string connectionString, string outputTableName, int outputBatchSize, bool replaceExistingTable)
        {
            DatabaseOutputProcessor dbout = new DatabaseOutputProcessor();
            dbout.DbPlatform = dbPlat;
            dbout.ConnectionString = connectionString;
            dbout.TableName = outputTableName;
            dbout.OutputBatchSize = outputBatchSize;
            dbout.ReplaceExistingTable = replaceExistingTable;
            dbout.WriteDataToOutput(dtList);
            dbout = null;
        }

        private void ExportToAccessFile(PFList<string> dtList, string documentFilePath, string tableName, bool replaceExistingFile, enAccessVersion accessVersion, string username, string password)
        {
            AccessDatabaseFileOutputProcessor tabOut = new AccessDatabaseFileOutputProcessor(documentFilePath, accessVersion, username, password);
            tabOut.ReplaceExistingFile = replaceExistingFile;
            tabOut.TableName = tableName;
            tabOut.WriteDataToOutput(dtList);
            tabOut = null;
        }



        private void DeleteTempFiles(PFList<string> dtList)
        {
            for (int i = 0; i < dtList.Count; i++)
            {
                string filename = dtList[i];
                try
                {
                    File.Delete(filename);
                }
                catch (System.Exception ex)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to delete temp file: ");
                    _msg.Append(filename);
                    _msg.Append(Environment.NewLine);
                    _msg.Append("Make sure you have sufficient security rights to delete from your user temp folder.");
                    _msg.Append(Environment.NewLine);
                    _msg.Append(AppMessages.FormatErrorMessage(ex));
                    DisplayAppErrorMessage(_msg.ToString());
                    return;
                }
            }
        }

        //routine will attemp to create test orders database if it does not already exist
        private bool VerifyTestOrdersDatabaseExists()
        {
            bool testDbExists = false;


            if (File.Exists(_testOrdersDatabase))
                return true;

            //test orders database not found: try to create it
            if (File.Exists(_initTestOrdersDatabaseZipFile) == false)
                return false;

            try
            {
                //use zip file to create test orders database
                ZipArchive za = new ZipArchive(_initTestOrdersDatabaseZipFile);
                string destinationFolder = Path.GetDirectoryName(_testOrdersDatabase);
                za.ExtractAll(destinationFolder);
                za = null;
                testDbExists = true;
            }
            catch (System.Exception ex)
            {
                testDbExists = false;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
                 

            return testDbExists;
        }

        private DataTable GetTransactionTemplateDataTable(string sqlQuery, string tableName)
        {
            DataTable dt = null;
            string dbPlatformDesc = DatabasePlatform.SQLServerCE35.ToString();
            //DatabasePlatform dbPlat = DatabasePlatform.SQLServerCE35;
            string dbConnStr = string.Empty;
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("Load ");
                _msg.Append(tableName);
                _msg.Append(" transaction template started at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());

                sw.Start();


                connStr = "data source='" + _testOrdersDatabase + "';";
                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = sqlQuery;
                db.CommandType = CommandType.Text;

                dt = db.RunQueryDataTable();
                dt.TableName = tableName;

                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to load ");
                _msg.Append(tableName);
                _msg.Append(": ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (sw.StopwatchIsRunning)
                    sw.Stop();

                if (db != null)
                {
                    if (db.IsConnected)
                    {
                        db.CloseConnection();
                    }
                }
                db = null;

                _msg.Length = 0;
                _msg.Append("Load ended at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());

            }
                 
        

            return dt;
        }


        /// <summary>
        /// Routine to generate realistic looking internet sales tansactions.
        /// </summary>
        /// <param name="maxTxsToGenerate">Number of sales transactions to generate.</param>
        /// <param name="randomOrdersDefinition">Object containing definition for the random sales generation.</param>
        /// <param name="generatorMessages">StringBuilder that will contain messages generated during processing.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Set this to true if you want database platform drop-down lists to only include data providers that are installed on the current system. If you decide to include a provider not installed on the system, you will almost certainly get an error when attempting to use the provider.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing output from data grid exports.</param>
        /// <param name="defaultOutputDatabaseType">Default database platform to display when form with a data destination Database Type drop-down list is first shown.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use when the Default Output Database Type for a data destination is set on one of the application’s data destination drop-down lists.</param>
        /// <remakrs>Final four parameters are only used for preview processing.</remakrs>
        public void GenerateInternetSalesTransactions(int maxTxsToGenerate,
                                                      PFRandomOrdersDefinition randomOrdersDefinition,
                                                      StringBuilder generatorMessages,
                                                      //following parameters used only by Preview processing
                                                      bool showInstalledDatabaseProvidersOnly,
                                                      string defaultDataGridExportFolder,
                                                      string defaultOutputDatabaseType,
                                                      string defaultOutputDatabaseConnectionString)
        {
            DataTable dwSalesHeaderTable = null;
            DataTable dwSalesDetailTable = null;
            DataColumn[] headerKeys = new DataColumn[1];
            DataColumn[] detailKeys = new DataColumn[2];
            PFList<string> detailDataTableList = new PFList<string>();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                bool testDbExists = VerifyTestOrdersDatabaseExists();
                if (testDbExists == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to run GenerateInternetSalesTransactions. Database with test data does not exist: ");
                    _msg.Append(_testOrdersDatabase);
                    throw new System.Exception(_msg.ToString());
                }

                //load SalesOrderHeader to DataTable
                dwSalesHeaderTable = GetTransactionTemplateDataTable(_factInternetSalesQueryMaster, "FactInternetSalesMaster");
                //load SalesOrderDetail to DataTable
                headerKeys[0] = dwSalesHeaderTable.Columns[0];
                dwSalesHeaderTable.PrimaryKey = headerKeys;
                //load SalesOrderDetail to DataTable
                dwSalesDetailTable = GetTransactionTemplateDataTable(_factInternetSalesQuery, "FactInternetSalesDetail");
                //detailKeys[0] = dwSalesDetailTable.Columns[9];
                //detailKeys[1] = dwSalesDetailTable.Columns[10];
                if (randomOrdersDefinition.OutputDatabasePlatform == DatabasePlatform.SQLAnywhereUltraLite.ToString())
                {
                    //only one primary key column allowed for UltraLite database
                    detailKeys = null;
                    detailKeys = new DataColumn[1];
                    detailKeys[0] = dwSalesDetailTable.Columns[0];
                }
                else
                {
                    detailKeys[0] = dwSalesDetailTable.Columns[9];
                    detailKeys[1] = dwSalesDetailTable.Columns[10];
                }
                dwSalesDetailTable.PrimaryKey = detailKeys;

                //generate test orders tables
                stMasterDetailGeneratorResult generateResult = GenerateDwDetailTransactionTable(detailDataTableList,
                                                                                                dwSalesHeaderTable,
                                                                                                dwSalesDetailTable,
                                                                                                enTestOrdersType.InternetSales,
                                                                                                (int)1,    //master table key column number
                                                                                                maxTxsToGenerate,
                                                                                                randomOrdersDefinition);
                if (detailDataTableList.Count > 0)
                {
                    if (maxTxsToGenerate > 0)
                    {
                        //preview transactions was requested
                        PreviewTestOrdersDataTableList(detailDataTableList,
                                                       maxTxsToGenerate,
                                                       showInstalledDatabaseProvidersOnly,
                                                       defaultDataGridExportFolder,
                                                       defaultOutputDatabaseType,
                                                       defaultOutputDatabaseConnectionString);
                    }
                    else
                    {
                        //save generated data to destination
                        SaveDataTableListToDestination(detailDataTableList,
                                                       randomOrdersDefinition.TableSchema,
                                                       randomOrdersDefinition.DwInternetSalesTableName,
                                                       randomOrdersDefinition);

                        _msg.Length = 0;
                        _msg.Append("Number of internet sales orders generated:        ");
                        _msg.Append(generateResult.NumMasterRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Number of internet sales order details generated: ");
                        _msg.Append(generateResult.NumDetailRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Internet order data warehouse table Name:         ");
                        _msg.Append(GetFullTableName(randomOrdersDefinition.TableSchema, randomOrdersDefinition.DwInternetSalesTableName));
                        WriteMessageToLog(_msg.ToString());
                        _msg.Append(Environment.NewLine);
                        generatorMessages.Append(_msg.ToString());
                    }
                }
                else
                {
                    //masterDataTableList.Count <= 0
                    _msg.Length = 0;
                    _msg.Append("No transaction data was generated: GenerateInternetSalesTransactions");
                    WriteMessageToLog(_msg.ToString());
                    _msg.Append(Environment.NewLine);
                    generatorMessages.Append(_msg.ToString());
                }

                sw.Stop();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (detailDataTableList != null)
                {
                    if (detailDataTableList.Count > 0)
                    {
                        DeleteTempFiles(detailDataTableList);
                    }
                }

                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("GenerateInternetSalesTransactions finished.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());
                _msg.Append(Environment.NewLine);
                generatorMessages.Append(_msg.ToString());
            }

        }

        /// <summary>
        /// Routine to generate realistic looking reseller sales tansactions.
        /// </summary>
        /// <param name="maxTxsToGenerate">Number of reseller sales transactions to generate.</param>
        /// <param name="randomOrdersDefinition">Object containing definition for the random sales generation.</param>
        /// <param name="generatorMessages">StringBuilder that will contain messages generated during processing.</param>
        /// <param name="showInstalledDatabaseProvidersOnly">Set this to true if you want database platform drop-down lists to only include data providers that are installed on the current system. If you decide to include a provider not installed on the system, you will almost certainly get an error when attempting to use the provider.</param>
        /// <param name="defaultDataGridExportFolder">Default folder for storing output from data grid exports.</param>
        /// <param name="defaultOutputDatabaseType">Default database platform to display when form with a data destination Database Type drop-down list is first shown.</param>
        /// <param name="defaultOutputDatabaseConnectionString">Default connection string to use when the Default Output Database Type for a data destination is set on one of the application’s data destination drop-down lists.</param>
        /// <remakrs>Final four parameters are only used for preview processing.</remakrs>
        public void GenerateResellerSalesTransactions(int maxTxsToGenerate,
                                                      PFRandomOrdersDefinition randomOrdersDefinition,
                                                      StringBuilder generatorMessages,
                                                      //following parameters used only by Preview processing
                                                      bool showInstalledDatabaseProvidersOnly,
                                                      string defaultDataGridExportFolder,
                                                      string defaultOutputDatabaseType,
                                                      string defaultOutputDatabaseConnectionString)
        {
            DataTable dwSalesHeaderTable = null;
            DataTable dwSalesDetailTable = null;
            DataColumn[] headerKeys = new DataColumn[1];
            DataColumn[] detailKeys = new DataColumn[2];
            PFList<string> detailDataTableList = new PFList<string>();
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();

                bool testDbExists = VerifyTestOrdersDatabaseExists();
                if (testDbExists == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to run GenerateResellerSalesTransactions. Database with test data does not exist: ");
                    _msg.Append(_testOrdersDatabase);
                    throw new System.Exception(_msg.ToString());
                }

                //load SalesOrderHeader to DataTable
                dwSalesHeaderTable = GetTransactionTemplateDataTable(_factResellerSalesQueryMaster, "FactResellerSalesMaster");
                //load SalesOrderDetail to DataTable
                headerKeys[0] = dwSalesHeaderTable.Columns[0];
                dwSalesHeaderTable.PrimaryKey = headerKeys;
                //load SalesOrderDetail to DataTable
                dwSalesDetailTable = GetTransactionTemplateDataTable(_factResellerSalesQuery, "FactResellerSalesDetail");
                //detailKeys[0] = dwSalesDetailTable.Columns[10];
                //detailKeys[1] = dwSalesDetailTable.Columns[11];
                if (randomOrdersDefinition.OutputDatabasePlatform == DatabasePlatform.SQLAnywhereUltraLite.ToString())
                {
                    //only one primary key column allowed for UltraLite database
                    detailKeys = null;
                    detailKeys = new DataColumn[1];
                    detailKeys[0] = dwSalesDetailTable.Columns[0];
                }
                else
                {
                    detailKeys[0] = dwSalesDetailTable.Columns[10];
                    detailKeys[1] = dwSalesDetailTable.Columns[11];
                }
                dwSalesDetailTable.PrimaryKey = detailKeys;

                //generate test orders tables
                stMasterDetailGeneratorResult generateResult = GenerateDwDetailTransactionTable(detailDataTableList,
                                                                                                dwSalesHeaderTable,
                                                                                                dwSalesDetailTable,
                                                                                                enTestOrdersType.ResellerSales,
                                                                                                (int)1,    //master table key column number
                                                                                                maxTxsToGenerate,
                                                                                                randomOrdersDefinition);
                if (detailDataTableList.Count > 0)
                {
                    if (maxTxsToGenerate > 0)
                    {
                        //preview transactions was requested
                        PreviewTestOrdersDataTableList(detailDataTableList,
                                                       maxTxsToGenerate,
                                                       showInstalledDatabaseProvidersOnly,
                                                       defaultDataGridExportFolder,
                                                       defaultOutputDatabaseType,
                                                       defaultOutputDatabaseConnectionString);
                    }
                    else
                    {
                        //save generated data to destination
                        SaveDataTableListToDestination(detailDataTableList,
                                                       randomOrdersDefinition.TableSchema,
                                                       randomOrdersDefinition.DwResellerSalesTableName,
                                                       randomOrdersDefinition);

                        _msg.Length = 0;
                        _msg.Append("Number of reseller sales orders generated:        ");
                        _msg.Append(generateResult.NumMasterRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Number of reseller sales order details generated: ");
                        _msg.Append(generateResult.NumDetailRowsGenerated.ToString("#,##0"));
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Reseller order data warehouse table Name:         ");
                        _msg.Append(GetFullTableName(randomOrdersDefinition.TableSchema, randomOrdersDefinition.DwResellerSalesTableName));
                        WriteMessageToLog(_msg.ToString());
                        _msg.Append(Environment.NewLine);
                        generatorMessages.Append(_msg.ToString());
                    }
                }
                else
                {
                    //masterDataTableList.Count <= 0
                    _msg.Length = 0;
                    _msg.Append("No transaction data was generated: GenerateResellerSalesTransactions");
                    WriteMessageToLog(_msg.ToString());
                    _msg.Append(Environment.NewLine);
                    generatorMessages.Append(_msg.ToString());
                }

                sw.Stop();

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessageWithStackTrace(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                if (detailDataTableList != null)
                {
                    if (detailDataTableList.Count > 0)
                    {
                        DeleteTempFiles(detailDataTableList);
                    }
                }

                if (sw.StopwatchIsRunning)
                    sw.Stop();

                _msg.Length = 0;
                _msg.Append("GenerateResellerSalesTransactions finished.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Elapsed time: ");
                _msg.Append(sw.FormattedElapsedTime);
                _msg.Append(Environment.NewLine);
                WriteMessageToLog(_msg.ToString());
                _msg.Append(Environment.NewLine);
                generatorMessages.Append(_msg.ToString());
            }

        }

        private stMasterDetailGeneratorResult GenerateDwDetailTransactionTable(PFList<string> dwDetailDataTableList,
                                                                               DataTable masterTableTemplate,
                                                                               DataTable detailTableTemplate,
                                                                               enTestOrdersType testOrdersType,
                                                                               int masterKeyColNum,
                                                                               int maxTxsToGenerate,
                                                                               PFRandomOrdersDefinition randomOrdersDefinition)
        {
            stMasterDetailGeneratorResult ret = new stMasterDetailGeneratorResult(0, 0);
            int numSalesOrdersGenerated = 0;
            int numDetailRowsGenerated = 0;
            int minNumSalesOrdersPerDate = 0;
            int maxNumSalesOrdersPerDate = 0;
            DateTime earliestTransactionDate = DateTime.MinValue;
            DateTime latestTransactionDate = DateTime.MaxValue;
            DateTime currentTransactionDate = DateTime.MaxValue;
            DateTime currentModifiedDate = DateTime.MinValue;
            TimeSpan earliestTimePerDate = TimeSpan.MinValue;
            TimeSpan latestTimePerDate = TimeSpan.MaxValue;
            TimeSpan currentModifiedTime = TimeSpan.MaxValue;
            bool includeWeekendDays = true;
            RandomNumber rn = new RandomNumber();
            RandomString rs = new RandomString();
            DataTable outputDetailTable = null;
            string masterPrimaryKeyColName = string.Empty;
            string detailTableName = string.Empty;

            try
            {
                if (testOrdersType != enTestOrdersType.InternetSales && testOrdersType != enTestOrdersType.ResellerSales)
                {
                    _msg.Length = 0;
                    _msg.Append("Invalid or unexpected test order type submitted to GenerateDwDetailTransactionTable routine.");
                    throw new System.Exception(_msg.ToString());
                }
                earliestTransactionDate = PFTextProcessor.ConvertStringToDateTime(randomOrdersDefinition.EarliestTransactionDate, new DateTime(2000, 1, 1));
                latestTransactionDate = PFTextProcessor.ConvertStringToDateTime(randomOrdersDefinition.LatestTransactionDate, new DateTime(2014, 12, 31));
                earliestTimePerDate = PFTextProcessor.ConvertStringToTimeSpan(randomOrdersDefinition.MinTimePerDate, new TimeSpan(2, 0, 0));
                latestTimePerDate = PFTextProcessor.ConvertStringToTimeSpan(randomOrdersDefinition.MaxTimePerDate, new TimeSpan(20, 0, 0));
                includeWeekendDays = randomOrdersDefinition.IncludeWeekendDays;
                minNumSalesOrdersPerDate = PFTextProcessor.ConvertStringToInt(randomOrdersDefinition.MinNumSalesOrdersPerDate, 0);
                maxNumSalesOrdersPerDate = PFTextProcessor.ConvertStringToInt(randomOrdersDefinition.MaxNumSalesOrdersPerDate, 5);

                currentTransactionDate = earliestTransactionDate;
                masterPrimaryKeyColName = masterTableTemplate.Columns[masterKeyColNum].ColumnName;
                DataView detailView = new DataView(detailTableTemplate, "", masterPrimaryKeyColName, DataViewRowState.CurrentRows);
                outputDetailTable = CreateOutputDetailTable(detailTableTemplate, testOrdersType, randomOrdersDefinition);
                detailTableName = testOrdersType == enTestOrdersType.InternetSales ? randomOrdersDefinition.DwInternetSalesTableName : randomOrdersDefinition.DwResellerSalesTableName;
                int outputOrderId = 100000;
                int outputOrderDetailId = 0;
                int outputDetailRowNumber = 0;
                DateTime dueDate = DateTime.MaxValue;
                DateTime shipDate = DateTime.MaxValue;
                int previousDetailTableCheckpoint = 0;
                int minSeconds = Convert.ToInt32(earliestTimePerDate.TotalSeconds);
                int maxSeconds = Convert.ToInt32(latestTimePerDate.TotalSeconds);
                int totSeconds = 0;
                int maxOutputTransactions = maxTxsToGenerate > 0 ? maxTxsToGenerate : int.MaxValue;

                while (currentTransactionDate <= latestTransactionDate)
                {
                    if (includeWeekendDays == false)
                    {
                        if (currentTransactionDate.DayOfWeek == DayOfWeek.Saturday
                            || currentTransactionDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            currentTransactionDate = currentTransactionDate.AddDays(1.0);
                            continue;
                        }
                    }

                    dueDate = currentTransactionDate.AddDays(7);
                    if (includeWeekendDays == false)
                    {
                        if (dueDate.DayOfWeek == DayOfWeek.Saturday
                            || dueDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            dueDate = currentTransactionDate.AddDays(2.0);
                            continue;
                        }
                    }

                    shipDate = currentTransactionDate.AddDays(5);
                    if (includeWeekendDays == false)
                    {
                        if (shipDate.DayOfWeek == DayOfWeek.Saturday
                            || shipDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            shipDate = currentTransactionDate.AddDays(2.0);
                            continue;
                        }
                    }

                    int numSalesOrdersForDate = rn.GenerateRandomInt(minNumSalesOrdersPerDate, maxNumSalesOrdersPerDate);
                    if (maxTxsToGenerate > 0)
                    {
                        //see if exceeded preview limit
                        if ((numSalesOrdersGenerated + numSalesOrdersForDate) > maxOutputTransactions)
                        {
                            numSalesOrdersForDate = maxOutputTransactions - numSalesOrdersGenerated;
                        }
                    }

                    for (int masterInx = 0; masterInx < numSalesOrdersForDate; masterInx++)
                    {
                        int masterKey = rn.GenerateRandomInt(1, masterTableTemplate.Rows.Count);
                        DataRow masterRow = masterTableTemplate.Rows.Find(masterKey);
                        //DataRow newMasterRow = outputMasterTable.NewRow();

                        numSalesOrdersGenerated++;
                        outputOrderId++;


                        totSeconds = rn.GenerateRandomInt(minSeconds, maxSeconds);
                        currentModifiedTime = new TimeSpan(0, 0, totSeconds);
                        currentModifiedDate = new DateTime(currentTransactionDate.Year, currentTransactionDate.Month, currentTransactionDate.Day,
                                                           currentModifiedTime.Hours, currentModifiedTime.Minutes, currentModifiedTime.Seconds);


                        DataRowView[] detailRows = detailView.FindRows(masterRow[1]);
                        for (int detailInx = 0; detailInx < detailRows.Length; detailInx++)
                        {
                            outputDetailRowNumber++;
                            outputOrderDetailId++;
                            numDetailRowsGenerated++;

                            DataRow detailRow = detailRows[detailInx].Row;
                            DataRow newDetailRow = outputDetailTable.NewRow();
                            for (int i = 0; i < outputDetailTable.Columns.Count; i++)
                            {
                                newDetailRow[i] = detailRow[i];
                            }
                            if (testOrdersType == enTestOrdersType.InternetSales)
                            {
                                newDetailRow["dgvRowNumber"] = outputDetailRowNumber;
                                newDetailRow["OrderDateKey"] = Convert.ToInt32(currentModifiedDate.ToString("yyyyMMdd"));
                                newDetailRow["DueDateKey"] = Convert.ToInt32(dueDate.ToString("yyyyMMdd"));
                                newDetailRow["ShipDateKey"] = Convert.ToInt32(shipDate.ToString("yyyyMMdd"));
                                newDetailRow["SalesOrderNumber"] = "SO" + outputOrderId.ToString("0");
                            }
                            else //if (testOrdersType == enTestOrdersType.ResellerSales)
                            {
                                newDetailRow["dgvRowNumber"] = outputDetailRowNumber;
                                newDetailRow["OrderDateKey"] = Convert.ToInt32(currentModifiedDate.ToString("yyyyMMdd"));
                                newDetailRow["DueDateKey"] = Convert.ToInt32(dueDate.ToString("yyyyMMdd"));
                                newDetailRow["ShipDateKey"] = Convert.ToInt32(shipDate.ToString("yyyyMMdd"));
                                newDetailRow["SalesOrderNumber"] = "SO" + outputOrderId.ToString("0");
                            }

                            outputDetailTable.Rows.Add(newDetailRow);
                        }
                    }

                    if ((numDetailRowsGenerated - previousDetailTableCheckpoint) > _maxTempDataTableRows)
                    {
                        //save existing detail tables and add file names to list of detail data tables
                        SaveDataTableToList(dwDetailDataTableList, outputDetailTable, randomOrdersDefinition.TableSchema, detailTableName, ".pfdexdwdtl");

                        previousDetailTableCheckpoint = numDetailRowsGenerated;

                        //reset table containing output rows
                        outputDetailTable.Dispose();
                        outputDetailTable = null;
                        outputDetailTable = CreateOutputDetailTable(detailTableTemplate, testOrdersType, randomOrdersDefinition);
                    }

                    if (numSalesOrdersGenerated < maxOutputTransactions)
                    {
                        currentTransactionDate = currentTransactionDate.AddDays(1.0);
                    }
                    else
                    {
                        //end the loop: this will occurs for a preview request
                        currentTransactionDate = latestTransactionDate.AddDays(1.0);
                    }
                }

                //save any remaining rows in detail table and add file names to lists of data tables
                if (outputDetailTable.Rows.Count > 0)
                {
                    SaveDataTableToList(dwDetailDataTableList, outputDetailTable, randomOrdersDefinition.TableSchema, detailTableName, ".pfdexdwdtl");
                    outputDetailTable.Dispose();
                    outputDetailTable = null;
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                DisplayAppErrorMessage(_msg.ToString());
            }
            finally
            {
                ;
            }

            ret.NumMasterRowsGenerated = numSalesOrdersGenerated;
            ret.NumDetailRowsGenerated = numDetailRowsGenerated;
            return ret;
        }


        /// <summary>
        /// Routine to run an existing extractor definition without displaying any UI elements.
        /// </summary>
        /// <param name="extractDefinitionFile">File containing the extractor definition.</param>
        /// <param name="outputLogFile">Name of the file to contain output messages generated during the batch processing.</param>
        /// <returns>Returns 0 if success; non-zero if there was one or more errors.</returns>
        public int RunExtractAsBatch(string extractDefinitionFile,
                                     string outputLogFile)
        {
             int retcode = 0;
            _messageLog.ShowDatetime = true;
            Stopwatch sw = new Stopwatch();
            string dir = string.Empty;
            string extractDefinitionFilePath = string.Empty;
            string logFileName = string.Empty;
            string logFilePath = string.Empty;
            bool outputLogFolderCreated = false;
            PFExtractorDefinition exdef = null;
            PFExtractorOutputOptions outputOptions = null;

            try
            {
                WriteMessageToLog("Batch processing started.");
                sw.Start();

                //check if extract definition file exists
                if (File.Exists(extractDefinitionFile))
                {
                    extractDefinitionFilePath=extractDefinitionFile;
                }
                else
                {
                    string filepath = Path.Combine(_defaultDataExtractorDefsFolder, extractDefinitionFile);
                    if (File.Exists(filepath))
                    {
                        extractDefinitionFilePath = filepath;
                    }
                    else
                    {
                        _msg.Length = 0;
                        _msg.Append("Unable to find initial extract definition to load at ");
                        _msg.Append(extractDefinitionFile);
                        throw new ArgumentException(_msg.ToString());
                    }
                }


                //(optional)third must be valid log file name or path plus name
                if (outputLogFile.Trim().Length == 0)
                {
                    logFileName = Path.GetFileNameWithoutExtension(extractDefinitionFile);
                    logFilePath = Path.Combine(_defaultDataExtactorLogsFolder, logFileName + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".log");
                }
                else
                {
                    if (Path.GetDirectoryName(outputLogFile).Length == 0)
                    {
                        string fname = Path.GetFileNameWithoutExtension(outputLogFile);
                        if (fname.Length > 0)
                        {
                            logFilePath = Path.Combine(_defaultDataExtactorLogsFolder, fname + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".log");
                        }
                        else
                        {
                            logFileName = Path.GetFileNameWithoutExtension(extractDefinitionFile);
                            logFilePath = Path.Combine(_defaultDataExtactorLogsFolder, logFileName + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".log");
                        }
                    }
                    else
                    {
                        if (Path.GetFileName(outputLogFile).Length == 0)
                        {
                            dir = Path.GetDirectoryName(outputLogFile);
                            logFileName = Path.GetFileNameWithoutExtension(extractDefinitionFile);
                            logFilePath = Path.Combine(dir, logFileName + DateTime.Now.ToString("_yyyyMMdd_HHmmss") + ".log");
                        }
                        else
                        {
                            logFilePath = outputLogFile;
                        }
                    }
                }

                dir = Path.GetDirectoryName(logFilePath);
                if (dir.Length > 0)
                {
                    if (Directory.Exists(dir) == false)
                    {
                        Directory.CreateDirectory(dir);
                        outputLogFolderCreated = true;
                    }
                }

                _msg.Length = 0;
                _msg.Append("Data extract definition file: ");
                _msg.Append(extractDefinitionFilePath);
                WriteMessageToLog(_msg.ToString());
                _msg.Length = 0;
                _msg.Append("Data extract log file:        ");
                _msg.Append(logFilePath);
                WriteMessageToLog(_msg.ToString());
                if (outputLogFolderCreated)
                {
                    _msg.Length = 0;
                    _msg.Append("Log folder was created for this extract run.");
                    WriteMessageToLog(_msg.ToString());
                }

                //run the extract
                exdef = PFExtractorDefinition.LoadFromXmlFile(extractDefinitionFilePath);

                //retrieve key parameters from extractor definition
                switch (exdef.ExtractorSource)
                {
                    case enExtractorDataLocation.RelationalDatabase:
                        outputOptions = exdef.RelationalDatabaseSource.OutputOptions;
                        break;
                    case enExtractorDataLocation.AccessDatabaseFile:
                        outputOptions = exdef.MsAccessSource.OutputOptions;
                        break;
                    case enExtractorDataLocation.ExcelDataFile:
                        outputOptions = exdef.MsExcelSource.OutputOptions;
                        break;
                    case enExtractorDataLocation.DelimitedTextFile:
                        outputOptions = exdef.DelimitedTextFileSource.OutputOptions;
                        break;
                    case enExtractorDataLocation.FixedLengthTextFile:
                        outputOptions = exdef.FixedLengthTextFileSource.OutputOptions;
                        break;
                    case enExtractorDataLocation.XMLFile:
                        outputOptions = exdef.XmlFileSource.OutputOptions;
                        break;
                    default:
                        _msg.Length = 0;
                        _msg.Append("PreviewData not implemented for unknown data location.");
                        throw new System.Exception(_msg.ToString());
                        //break;
                }



                //Run the extract routines
                retcode = RunExtract(exdef,
                                     (int)-1,
                                     outputOptions.AddRowNumberToOutput,
                                     outputOptions.FilterOutput,
                                     outputOptions.RandomizeOutput,
                                     this.BatchSizeForRandomDataGeneration,
                                     this.BatchSizeForDataImportsAndExports
                                    );

            }
            catch (System.Exception ex)
            {
                retcode = 1;
                _msg.Length = 0;
                _msg.Append(AppGlobals.ConsoleMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                ConsoleMessages.DisplayErrorMessage(_msg.ToString());
            }
            finally
            {
                sw.Stop();
                WriteMessageToLog("Batch processing finished.");
                WriteMessageToLog("Return code:  " + retcode.ToString());
                WriteMessageToLog("Elapsed Time: " + sw.FormattedElapsedTime);
                SaveMessageLog(logFilePath);
            }
                 
            return retcode;
        
       }

        
        //Helper routines

        /// <summary>
        /// Writes messae to the message log.
        /// </summary>
        /// <param name="msg">Text of message to write.</param>
        public void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }

        /// <summary>
        /// Saves existing message log to a file.
        /// </summary>
        /// <param name="outputLogFile">Path for log file.</param>
        public void SaveMessageLog(string outputLogFile)
        {
            if (_messageLog != null)
            {
                _messageLog.SaveFile(outputLogFile);
            }
        }

        private bool FormIsOpen(string name)
        {
            bool retval = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == name)
                {
                    retval = true;
                    break;
                }
            }
            return retval;
        }

        private void DisplayAppErrorMessage(string msg)
        {
            if (this.RunExtractInBatchMode == false)
            {
                AppMessages.DisplayErrorMessage(msg, _saveErrorMessagesToAppLog);
            }
        }

        private void DisplayAppAlertMessage(string msg)
        {
            if (this.RunExtractInBatchMode == false)
            {
                AppMessages.DisplayAlertMessage(msg, _saveErrorMessagesToAppLog);
            }
        }

        private void DisplayAppInfoMessage(string msg)
        {
            if (this.RunExtractInBatchMode == false)
            {
                AppMessages.DisplayInfoMessage(msg, _saveErrorMessagesToAppLog);
            }
        }

    }//end class
}//end namespace
