using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using PFRandomDataForms;
using PFProcessObjects;
using PFMessageLogs;
using PFDataAccessObjects;
using PFRandomDataProcessor;
using PFSQLServerCE35Objects;
using PFCollectionsObjects;
using PFTimers;
using PFAppDataObjects;
using PFRandomValueDataTables;
using PFRandomDataExt;

namespace TestprogRandomDataProcessor
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;
        
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";

        private string _helpFilePath = string.Empty;

        private string _initDataGridExportFolder = @"C:\Temp\RandomizerTests";
        private string _defaultDataGridExportFolder = @"C:\Temp\RandomizerTests";
        private string _defaultOutputDatabaseType = string.Empty;
        private string _defaultOutputDatabaseConnectionString = string.Empty;
        private bool _showInstalledDatabaseProvidersOnly = true;
        //private int _numberOfRandomValueSamples = 100;


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

        }//end method

        private PFDatabase GetPFDatabaseObject(DatabasePlatform dbPlat)
        {
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();
            PFDatabase db = null;
            string connStr = string.Empty;
            string nmSpace = string.Empty;
            string clsName = string.Empty;
            string dllPath = string.Empty;

            dbPlatformDesc = dbPlat.ToString();

            string configValue = AppConfig.GetStringValueFromConfigFile(dbPlatformDesc, string.Empty);
            if (configValue.Trim() == string.Empty)
            {
                _msg.Length = 0;
                _msg.Append("Unable to find config entry for ");
                _msg.Append(dbPlatformDesc);
                throw new System.Exception(_msg.ToString());
            }
            string[] parsedConfig = configValue.Split('|');
            if (parsedConfig.Length != 3)
            {
                _msg.Length = 0;
                _msg.Append("Invalid config entry items for ");
                _msg.Append(dbPlatformDesc);
                _msg.Append(". Number of items after parse: ");
                _msg.Append(parsedConfig.Length.ToString());
                _msg.Append(".");
                throw new System.Exception(_msg.ToString());
            }

            nmSpace = parsedConfig[0];
            clsName = parsedConfig[1];
            dllPath = parsedConfig[2];

            db = new PFDatabase(dbPlatformDesc, dllPath, nmSpace + "." + clsName);


            return db;
        }

        private string GetConnectionString(DatabasePlatform dbPlat)
        {
            string connStr = string.Empty;
            string configKey = "DefaultConnection_";
            string dbPlatformDesc = DatabasePlatform.Unknown.ToString();

            dbPlatformDesc = dbPlat.ToString();
            configKey = configKey + dbPlatformDesc;

            string configValue = AppConfig.GetStringValueFromConfigFile(configKey, string.Empty);
            if (configValue.Trim() == string.Empty)
            {
                _msg.Length = 0;
                _msg.Append("Unable to find config entry for ");
                _msg.Append(configKey);
                throw new System.Exception(_msg.ToString());
            }

            connStr = configValue;

            return connStr;

        }


        public void ColSpecForm()
        {
            DataTable dt = null;
            TEST_DataTableRandomizer dtr = null;
            string dbConnStr = string.Empty;
            DatabasePlatform dbPlat = DatabasePlatform.Unknown;
            PFDatabase db = null;
            PFList<DataTableRandomizerColumnSpec> colSpecs = null;
            RandomNamesAndLocationsDataRequest randomizerNameSpecs = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("ColSpecForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                dbPlat = DatabasePlatform.SQLServerCE35;

                db = GetPFDatabaseObject(dbPlat);
                db.ConnectionString = GetConnectionString(dbPlat);

                randomizerNameSpecs = RandomNamesAndLocationsDataRequest.LoadFromXmlFile(@"C:\Testfiles\Randomizer\CountryRequestPersonsOnly.xml");

                db.OpenConnection();

                string sqlQuery = "select * from RandomNameData";
                //listTable = db.RunQueryDataTable(sqlQuery, CommandType.Text);
                dt = db.GetQueryDataSchema(sqlQuery, CommandType.Text);

                dtr = new TEST_DataTableRandomizer();
                colSpecs = dtr.GetInitColSpecListFromDataTable(dt);

                PFRandomDataForms.DataTableRandomizerColumnSpecForm frm = new PFRandomDataForms.DataTableRandomizerColumnSpecForm(colSpecs);
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    //time to do something
                    _msg.Length = 0;
                    _msg.Append(frm.ColSpecs.ToXmlString());
                    _messageLog.WriteLine(_msg.ToString());
                }
                else
                {
                    _msg.Length = 0;
                    _msg.Append("Column spec edit cancelled.");
                    _messageLog.WriteLine(_msg.ToString());
                }

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... ColSpecForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }



        public void RandomNumbersForm()
        {
            RandomNumbersForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomNumbersForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomNumbersForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomNumbersForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomDateTimesForm()
        {
            RandomDateTimesForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomDateTimesForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomDateTimesForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomDateTimesForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }



        public void RandomBooleansForm()
        {
            RandomBooleansForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomBooleansForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomBooleansForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomBooleansForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }



        public void RandomStringsForm()
        {
            RandomStringsForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomStringsForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomStringsForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomStringsForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomWordsForm()
        {
            RandomWordsForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomWordsForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomWordsForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomWordsForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomBytesForm()
        {
            RandomBytesForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomBytesForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomBytesForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomBytesForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomNamesAndLocationsForm()
        {
            RandomNamesAndLocationsForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomNamesAndLocationsForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomNamesAndLocationsForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();

                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomNamesAndLocationsForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomCustomValuesForm()
        {
            RandomCustomValuesForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomCustomValuesForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomCustomValuesForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomCustomValuesForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomDataElementsForm()
        {
            RandomDataElementsForm frm = null;

            try
            {
                _msg.Length = 0;
                _msg.Append("RandomDataElementsForm started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomDataElementsForm();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomDataElementsForm finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        public void RandomDataFormsManager()
        {
            RandomDataFormsManager frm = null;
            try
            {
                _msg.Length = 0;
                _msg.Append("RandomDataFormsManager started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                frm = new RandomDataFormsManager();
                frm.MessageLogUI = _messageLog;

                DialogResult res = frm.ShowDialog();
                _msg.Length = 0;
                if (res == DialogResult.OK)
                {
                    _msg.Append("OK pressed.");
                }
                else
                {
                    _msg.Append("Cancel pressed.");
                }
                _messageLog.WriteLine(_msg.ToString());

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... RandomDataFormsManager finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }


        private void OutputDataTableToGrid(DataTable tab)
        {
            PFDataOutputGrid.DataOutputGridProcessor grid = new PFDataOutputGrid.DataOutputGridProcessor();
            grid.ShowInstalledDatabaseProvidersOnly = _showInstalledDatabaseProvidersOnly;
            grid.DefaultOutputDatabaseType = _defaultOutputDatabaseType;
            grid.DefaultOutputDatabaseConnectionString = _defaultOutputDatabaseConnectionString;
            if (Directory.Exists(_defaultDataGridExportFolder) == false)
            {
                grid.DefaultGridExportFolder = _initDataGridExportFolder;
            }
            else
            {
                grid.DefaultGridExportFolder = _defaultDataGridExportFolder;
            }
            grid.EnableExportMenu = true;
            grid.WriteDataToGrid(tab);
        }

        
        public void DatesAndTimesTest()
        {
            DataTable dt = new DataTable();
            DataTableRandomizer dtr = new DataTableRandomizer();
            PFList<DataTableRandomizerColumnSpec> colSpecs = new PFList<DataTableRandomizerColumnSpec>();
            int maxOutputRows = 100;

            try
            {
                _msg.Length = 0;
                _msg.Append("DatesAndTimesTest started ...\r\n");
                _messageLog.WriteLine(_msg.ToString());

                DataColumn dc0 = new DataColumn("OrigDateTime", Type.GetType("System.DateTime"));
                DataColumn dc1 = new DataColumn("RandomDateUsingRange", Type.GetType("System.DateTime"));
                DataColumn dc2 = new DataColumn("RandomDateFromRange", Type.GetType("System.DateTime"));
                DataColumn dc3 = new DataColumn("RandomDateToRange", Type.GetType("System.DateTime"));
                DataColumn dc4 = new DataColumn("RandomTodayDate", Type.GetType("System.DateTime"));
                DataColumn dc5 = new DataColumn("RandomTodayDateOffset", Type.GetType("System.Int32"));
                DataColumn dc6 = new DataColumn("RandomDate", Type.GetType("System.DateTime"));
                DataColumn dc7 = new DataColumn("RandomDateOffset", Type.GetType("System.Int32"));

                dt.Columns.Add(dc0);
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);
                dt.Columns.Add(dc6);
                dt.Columns.Add(dc7);


                colSpecs = dtr.GetInitColSpecListFromDataTable(dt);

                colSpecs[1].RandomDataType = enRandomDataType.RandomDatesAndTimes;
                colSpecs[1].RandomDataSource = "Years1930-1939.xml";
                colSpecs[4].RandomDataType = enRandomDataType.RandomDatesAndTimes;
                colSpecs[4].RandomDataSource = "CurrentDateOffsetPlusMinus10Years.xml";
                colSpecs[6].RandomDataType = enRandomDataType.RandomDatesAndTimes;
                colSpecs[6].RandomDataSource = "DatesOffsetPlusMinus10Years.xml";

                
                string outputPath = Path.Combine(_defaultDataGridExportFolder, "DateTimeTestColSpecs.xml");
                colSpecs.SaveToXmlFile(outputPath);

                for (int r = 0; r < maxOutputRows; r++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = Convert.ToDateTime("04/13/1948 00:00:01");
                    dr[1] = Convert.ToDateTime("04/13/1948 00:00:01");
                    dr[2] = Convert.ToDateTime("01/01/1930 00:00:00");
                    dr[3] = Convert.ToDateTime("12/31/1939 23:59:59");
                    dr[4] = DateTime.Now;
                    dr[5] = (int)0;
                    dr[6] = Convert.ToDateTime("04/13/1948 00:00:01");
                    dr[7] = (int)0;
                    dt.Rows.Add(dr);
                }
                
                dtr.RandomizeDataTableValues(dt, colSpecs, 100);

                for (int r = 0; r < maxOutputRows; r++)
                {
                    DataRow dr = dt.Rows[r];
                    DateTime newDateTime = (DateTime)dr[4];
                    TimeSpan ts = newDateTime.Subtract(DateTime.Now);
                    dr[5] = (int)ts.TotalDays;
                    newDateTime = (DateTime)dr[6];
                    ts = newDateTime.Subtract(Convert.ToDateTime("04/13/1948 00:00:01"));
                    dr[7] = (int)ts.TotalDays;
                }
                dt.AcceptChanges();

                OutputDataTableToGrid(dt);

            }
            catch (System.Exception ex)
            {
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("\r\n... DatesAndTimesTest finished.");
                _messageLog.WriteLine(_msg.ToString());

            }
        }



        

    }//end class
}//end namespace
