using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using AppGlobals;
using System.IO;
using PFProcessObjects;
using PFMessageLogs;
using PFTimers;
using PFDataAccessObjects;
using PFDataOutputProcessor;
using PFDataOutputGrid;
using PFCollectionsObjects;
using PFRandomDataProcessor;
using PFRandomDataForms;
using PFRandomValueDataTables;

namespace pfDataViewerCPProcessor
{
    /// <summary>
    /// Processing routines for the Data Viewer application.
    /// </summary>
    public class DataViewerProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";
        private string _appConnectionStringManagerExe = @"pfConnectionStringManager.exe";
        private string _appRandomDataManagerExe = @"pfRandomDataSources.exe";

        private string _helpFilePath = string.Empty;


        //private variables for properties
        private string _initDataGridExportFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataViewerCP\DataExports\";
        private string _defaultDataGridExportFolder = string.Empty;
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private bool _showInstalledDatabaseProvidersOnly = true;
        private int _batchSizeForRandomDataGeneration = 50000;


        //constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public DataViewerProcessor()
        {
            InitInstance();
        }

        private void InitInstance()
        {
            if (Directory.Exists(_initDataGridExportFolder) == false)
            {
                Directory.CreateDirectory(_initDataGridExportFolder);
            }
        }

        //properties

        /// <summary>
        /// Set to true to save any error messages to a text app log.
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
        /// Default folder name for data grid exports to files.
        /// </summary>
        public string DefaultDataGridExportFolder
        {
            get
            {
                return _defaultDataGridExportFolder;
            }
            set
            {
                _defaultDataGridExportFolder = value;
            }
        }

        /// <summary>
        /// Default database destination for data grid exports to database tables.
        /// </summary>
        public string DefaultOutputDatabaseType
        {
            get
            {
                return _defaultOutputDatabaseType;
            }
            set
            {
                _defaultOutputDatabaseType = value;
            }
        }

        /// <summary>
        /// Default connection string for default database destination.
        /// </summary>
        public string DefaultOutputDatabaseConnectionString
        {
            get
            {
                return _defaultOutputDatabaseConnectionString;
            }
            set
            {
                _defaultOutputDatabaseConnectionString = value;
            }
        }

        /// <summary>
        /// Set to true if you only want installed database providers shown when prompting for the location of a data grid export table.
        /// </summary>
        public bool ShowInstalledDatabaseProvidersOnly
        {
            get
            {
                return _showInstalledDatabaseProvidersOnly;
            }
            set
            {
                _showInstalledDatabaseProvidersOnly = value;
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
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                proc = null;
            }

        }

        /// <summary>
        /// Displays form for defining connection strings.
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
        /// Shows form for building random data definitions.
        /// </summary>
        public void ShowRandomDataManagerForm()
        {
            PFProcess proc = new PFProcess();
            string currAppFolder = AppInfo.CurrentEntryAssemblyDirectory;
            string currAppExePath = AppInfo.CurrentEntryAssembly;
            string appRandomDataManagerApp = Path.Combine(currAppFolder, _appRandomDataManagerExe);
            string mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            bool appRandomDataManagerFound = false;

            try
            {
                //proc.Arguments = currAppExePath + " " + _helpFilePath + " " + "\"Manage Random Data\"";
                proc.Arguments = string.Empty;

                if (File.Exists(appRandomDataManagerApp))
                {
                    appRandomDataManagerFound = true;
                    proc.WorkingDirectory = currAppFolder;
                    proc.ExecutableToRun = appRandomDataManagerApp;
                }
                else
                {
                    string configValue = AppConfig.GetStringValueFromConfigFile("appRandomDataManagerPath", string.Empty);
                    if (configValue.Length > 0)
                    {
                        if (File.Exists(configValue))
                        {
                            appRandomDataManagerFound = true;
                            proc.WorkingDirectory = Path.GetDirectoryName(configValue);
                            proc.ExecutableToRun = configValue;
                        }
                        else
                        {
                            appRandomDataManagerFound = false;
                        }
                    }
                    else
                    {
                        appRandomDataManagerFound = false;

                    }
                }
                if (appRandomDataManagerFound == false)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to find Random Data Manager Application in current app folder: ");
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

        private void WriteMessageToLog(string msg)
        {
            if (_messageLog != null)
            {
                _messageLog.WriteLine(msg);
            }
        }


        /// <summary>
        /// Routine to execute the specified query.
        /// </summary>
        /// <param name="queryDef">File containing the query definition.</param>
        /// <param name="randomizeOutput">Set to true to enable randomizing of output.</param>
        /// <param name="showRowNumber">Set to true to add a row number to the output generated by the query.</param>
        public void RunQuery(pfQueryDef queryDef, bool randomizeOutput, bool showRowNumber)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;
            Stopwatch sw = new Stopwatch();

            try
            {
                _msg.Length = 0;
                _msg.Append("RunQuery started at ");
                _msg.Append(DateTime.Now.ToString());
                WriteMessageToLog(_msg.ToString());

                sw.Start();

                if (queryDef.DatabaseType == DatabasePlatform.Unknown
                    || queryDef.ConnectionString.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both a data source and a connection string for the query.");
                    throw new System.Exception(_msg.ToString());
                }

                if (queryDef.Query.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                dbPlatformDesc = queryDef.DatabaseType.ToString();
                connStr = queryDef.ConnectionString;

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = queryDef.Query;
                db.CommandType = CommandType.Text;

                DataTable tab = db.RunQueryDataTable();
                tab.TableName = queryDef.QueryName;

                if (randomizeOutput)
                {
                    PFList<DataTableRandomizerColumnSpec> colSpecs = queryDef.RandomizerColSpecs;
                    PFList<DataTableRandomizerColumnSpec> saveOrigColSpecs = queryDef.RandomizerColSpecs;
                    SyncColSpecsWithDataSchema(queryDef, ref colSpecs);
                    queryDef.RandomizerColSpecs = colSpecs;
                    DataTableRandomizer dtr = new DataTableRandomizer();
                    dtr.MessageLogUI = this.MessageLogUI;
                    dtr.RandomizeDataTableValues(tab, queryDef.RandomizerColSpecs, this.BatchSizeForRandomDataGeneration);
                    queryDef.RandomizerColSpecs = saveOrigColSpecs;   //get rid of any changes made during runtime: some of the colspecs fields are used as work fields by the randomizer routines.
                }


                sw.Stop();

                _msg.Length = 0;
                _msg.Append("Total time to execute the query: ");
                _msg.Append(sw.FormattedElapsedTime);
                WriteMessageToLog(_msg.ToString());

                OutputResultToGrid(tab, showRowNumber);
                
            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                if(sw.StopwatchIsRunning)
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

        }

        private void OutputResultToGrid(DataTable tab, bool showRowNumber)
        {
            PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
            grid.ShowInstalledDatabaseProvidersOnly = this.ShowInstalledDatabaseProvidersOnly;
            grid.DefaultOutputDatabaseType = this.DefaultOutputDatabaseType;
            grid.DefaultOutputDatabaseConnectionString = this.DefaultOutputDatabaseConnectionString;
            grid.ShowRowNumber = showRowNumber;
            if (Directory.Exists(this.DefaultDataGridExportFolder) == false)
            {
                grid.DefaultGridExportFolder = _initDataGridExportFolder;
            }
            else
            {
                grid.DefaultGridExportFolder = this.DefaultDataGridExportFolder;
            }

            //for (int colSpecsInx = 0; colSpecsInx < tab.Columns.Count; colSpecsInx++)
            //{
            //    string colName = tab.Columns[colSpecsInx].ColumnName;
            //    grid.GridColumnFilters.Add(new PFDataOutputGrid.GridColumnFilter(colName, PFDataOutputGrid.enFilterType.ComboBoxColumnFilter));
            //}
            grid.WriteDataToGrid(tab);
        }

        /// <summary>
        /// Displays the form on which randomizing definitions can be attached to query data columns.
        /// </summary>
        /// <param name="queryDef">File containing the query definition.</param>
        /// <returns>List containing the column reandomization specifications.</returns>
        public PFList<DataTableRandomizerColumnSpec> ShowRandomizerDefinitionForm(pfQueryDef queryDef)
        {
            PFList<DataTableRandomizerColumnSpec> colSpecs = null;
            DataTable dt = null;
            DataTableRandomizer dtr = null;


            try
            {
                _msg.Length = 0;
                _msg.Append("ShowRandomizerDefinitionForm started ... ");
                WriteMessageToLog(_msg.ToString());

                dt = GetQueryDefSchema(queryDef);
                if (dt == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to determine schema for query definition ");
                    _msg.Append(queryDef.QueryName);
                    WriteMessageToLog(_msg.ToString());
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return colSpecs;
                }

                dtr = new DataTableRandomizer();
                colSpecs = dtr.GetInitColSpecListFromDataTable(dt);
                SyncColSpecsWithSavedValues(queryDef, colSpecs);
                SyncColSpecsWithDataSchema(queryDef, ref colSpecs);

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
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
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
        /// Retrieves a DataTable object containing the query schema for the specified query.
        /// </summary>
        /// <param name="queryDef">File containing query definition.</param>
        /// <returns>DataTable object containing the schema information.</returns>
        public DataTable GetQueryDefSchema(pfQueryDef queryDef)
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
                dbPlat = queryDef.DatabaseType;

                if (queryDef.DatabaseType == DatabasePlatform.Unknown
                    || queryDef.ConnectionString.Trim().Length == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("You must specify both a data source and a connection string for the query.");
                    throw new System.Exception(_msg.ToString());
                }

                if (queryDef.Query.Length == 0)
                {
                    throw new System.Exception("You must specify a SQL query to run.");
                }

                dbPlatformDesc = queryDef.DatabaseType.ToString();
                connStr = queryDef.ConnectionString;

                string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
                string[] parsedConfig = configValue.Split('|');
                nmSpace = parsedConfig[0];
                clsName = parsedConfig[1];
                dllPath = parsedConfig[2];

                db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);
                db.ConnectionString = connStr;
                db.OpenConnection();

                db.SQLQuery = queryDef.Query;
                db.CommandType = CommandType.Text;

                dt = db.GetQueryDataSchema(queryDef.Query, CommandType.Text);
                dt.TableName = queryDef.QueryName;

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                WriteMessageToLog(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
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
        /// Synchronizes the saved randomizer specifications in a query definition file with the current column specifications.
        /// </summary>
        /// <param name="queryDef">File containing the query definition.</param>
        /// <param name="colSpecs">List containing the column specifications for the query.</param>
        public void SyncColSpecsWithSavedValues(pfQueryDef queryDef, PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            if (colSpecs == null || queryDef.RandomizerColSpecs == null)
                return;

            for (int cs = 0; cs < colSpecs.Count; cs++)
            {
                for (int qd = 0; qd < queryDef.RandomizerColSpecs.Count; qd++)
                {
                    if (queryDef.RandomizerColSpecs[qd].DataTableColumnName == colSpecs[cs].DataTableColumnName)
                    {
                        colSpecs[cs].RandomDataType = queryDef.RandomizerColSpecs[qd].RandomDataType;
                        colSpecs[cs].RandomDataSource = queryDef.RandomizerColSpecs[qd].RandomDataSource;
                        colSpecs[cs].RandomNamesAndLocationsNumber = queryDef.RandomizerColSpecs[qd].RandomNamesAndLocationsNumber;
                        colSpecs[cs].RandomDataFieldName = queryDef.RandomizerColSpecs[qd].RandomDataFieldName;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Synchronizes the list of column specifications with the current query definition.
        /// </summary>
        /// <param name="queryDef">File containing the current query definitiion.</param>
        /// <param name="colSpecs">List containing the existing column specifications.</param>
        /// <remarks>This routine is used to catch any changes made to a query after a set of column specifications were generated.</remarks>
        public void SyncColSpecsWithDataSchema(pfQueryDef queryDef, ref PFList<DataTableRandomizerColumnSpec> colSpecs)
        {
            PFList<DataTableRandomizerColumnSpec> newColSpecs = new PFList<DataTableRandomizerColumnSpec>();
            DataTable dt = null;

            if (colSpecs == null || queryDef.RandomizerColSpecs == null)
                return;

            try
            {
                dt = GetQueryDefSchema(queryDef);

                if (dt == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to retrieve query schema in SyncColSpecsWithDataSchema routine for ");
                    _msg.Append(queryDef.QueryName);
                    _msg.Append(".");
                    WriteMessageToLog(_msg.ToString());
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
                else if (dt.Columns == null)
                {
                    _msg.Length = 0;
                    _msg.Append("Unable to retrieve query schema column list in SyncColSpecsWithDataSchema routine for ");
                    _msg.Append(queryDef.QueryName);
                    _msg.Append(".");
                    WriteMessageToLog(_msg.ToString());
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
                else if (dt.Columns.Count == 0)
                {
                    _msg.Length = 0;
                    _msg.Append("Query text must specify one or more data columns for SyncColSpecsWithDataSchema routine.");
                    WriteMessageToLog(_msg.ToString());
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                    return;
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("SyncColSpecsWithDataSchema will process queryDef ");
                    _msg.Append(queryDef.QueryName);
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
                    _msg.Append("Query Definition Name:         ");
                    _msg.Append(queryDef.QueryName);
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
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                ;
            }
                 
        
            
        }//end method

    }//end class
}//end namespace
