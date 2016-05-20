using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using AppGlobals;
using PFMessageLogs;
using pfDataExtractorCPProcesxor;
using PFSQLServerCE35Objects;

namespace pfDataExtractorCP
{
    static class Program
    {
        public static MainForm _mainForm;
        public static MessageLog _messageLog;
        public static bool _runInBatchMode;
        private static string _defaultDataExtactorLogsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\pfApps\pfDataExtractorCP\ExtractorLogs\";
        public static bool _sqlCE35Found = true;
        private static PFSQLServerCE35 _db = null;
        private static bool _showMainForm = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new CMainForm());


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CAppGlobalErrorHandler.WriteToAppLog = true;
            CAppGlobalErrorHandler.WriteToEventLog = false;
            CAppGlobalErrorHandler.CancelApplicationOnGlobalError = false;

            Application.ThreadException += new ThreadExceptionEventHandler(CAppGlobalErrorHandler.GlobalErrorHandler);

            _messageLog = new MessageLog();
            _messageLog.Caption = "pfDataExtractorCP Application";
            _messageLog.ShowDatetime = false;
            _messageLog.Font = "Lucida Console";
            _messageLog.FontSize = (float)10.0;

            _runInBatchMode = false;

            try
            {
                //make sure SQL CE 3.5 is installed and registered with .NET 4.0
                _db = new PFSQLServerCE35();
            }
            catch (System.Exception ex)
            {
                System.Text.StringBuilder _msg = new System.Text.StringBuilder();
                _msg.Length = 0;
                _msg.Append("Unable to instantiate SQL Server Compact Edition 3.5 SP2 object.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Make sure all application pre-requisites are installed:\r\n");
                _msg.Append("SQL CE 3.5 SP2, ACE OLEDB 12.0, .NET 4.0.");
                _msg.Append(Environment.NewLine);
                _msg.Append("If necessary, uninstall and re-install the application.");
                _msg.Append(Environment.NewLine);
                _msg.Append(Environment.NewLine);
                _msg.Append("ERROR MESSAGE: \r\n");
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Program._messageLog.WriteLine(_msg.ToString());
                AppMessages.DisplayErrorMessage(_msg.ToString(), true);
                _sqlCE35Found = false;
            }
            finally
            {
                _db = null;
            }


            if (_sqlCE35Found)
            {
                if (args != null)
                {
                    if (args.Length == 1)
                    {
                        _mainForm.InitExtractDefinitionToOpen = args[0];
                    }
                    if (args.Length > 1)
                    {
                        _runInBatchMode = true;
                    }
                }

                if (_runInBatchMode)
                {
                    int retcode = RunExtractAsBatch(args);
                    Environment.ExitCode = retcode;
                }
                else
                {
                    //show Windows user interface
                    _messageLog.ShowWindow();
                    try
                    {
                        //make sure main form can be instantiated
                        _mainForm = new MainForm();
                    }
                    catch (System.Exception ex)
                    {
                        System.Text.StringBuilder _msg = new System.Text.StringBuilder();
                        _msg.Length = 0;
                        _msg.Append("Unable to instantiate main form object.");
                        _msg.Append(Environment.NewLine);
                        _msg.Append("Make sure all application pre-requisites are installed:\r\n");
                        _msg.Append("SQL CE 3.5 SP2, ACE OLEDB 12.0, .NET 4.0.");
                        _msg.Append(Environment.NewLine);
                        _msg.Append("If necessary, uninstall and re-install the application.");
                        _msg.Append(Environment.NewLine);
                        _msg.Append(Environment.NewLine);
                        _msg.Append("ERROR MESSAGE: \r\n");
                        _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                        Program._messageLog.WriteLine(_msg.ToString());
                        AppMessages.DisplayErrorMessage(_msg.ToString(), true);
                        _showMainForm = false;
                    }
                    finally
                    {
                        ;
                    }
                    if (_showMainForm)
                    {
                        Application.Run(_mainForm);
                    }
                }
            }



        }//end main

        private static int RunExtractAsBatch(string[] args)
        {
            int retcode = 0;
            StringBuilder msg = new StringBuilder();
            string extractDefinitionFile = string.Empty;
            string outputLogFile = string.Empty;

            try
            {
                ConsoleMessages.DisplayInfoMessage("Data extract batch processing started.");

                if (args[0].ToLower() != "noui" && args[0].ToLower() != "batch")
                {
                    msg.Length = 0;
                    msg.Append("Invalid first parameter on command line.\r\n");
                    msg.Append("First parameter must be NOUI or BATCH for a batch processing request.");
                    throw new ArgumentException(msg.ToString());
                }
                PFAppProcessor appProcessor = new PFAppProcessor();

                _messageLog.ShowDatetime = true;

                appProcessor.RunExtractInBatchMode = true;
                appProcessor.MessageLogUI = _messageLog;
                string configValue = string.Empty;
                configValue = Properties.Settings.Default.DefaultExtractorDefinitionsSaveFolder.Trim();
                if (configValue.Trim() != string.Empty)
                    appProcessor.DefaultDataExtractorDefsFolder = configValue;
                appProcessor.BatchSizeForRandomDataGeneration = Properties.Settings.Default.BatchSizeForRandomDataGeneration;
                appProcessor.BatchSizeForDataImportsAndExports = Properties.Settings.Default.BatchSizeForDataImportsAndExports;

                extractDefinitionFile = args[1];

                if (args.Length > 2)
                    outputLogFile = args[2];

                retcode = appProcessor.RunExtractAsBatch(extractDefinitionFile, outputLogFile);

            }
            catch (System.Exception ex)
            {
                retcode = 1;
                msg.Length = 0;
                msg.Append(AppGlobals.ConsoleMessages.FormatErrorMessage(ex));
                _messageLog.WriteLine(msg.ToString());
                ConsoleMessages.DisplayErrorMessage(msg.ToString());
            }
            finally
            {
                //ConsoleMessages.DisplayInfoMessage("Data extract batch processing finished.");
                Console.WriteLine("Data extract batch processing finished.");

            }

            if (retcode != 0)
            {
                if (System.IO.Directory.Exists(_defaultDataExtactorLogsFolder) == false)
                {
                    System.IO.Directory.CreateDirectory(_defaultDataExtactorLogsFolder);
                }
                string logFilePath = System.IO.Path.Combine(_defaultDataExtactorLogsFolder, "DataExtractErrors_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log");
                _messageLog.SaveFile(logFilePath);
            }

            return retcode;
        }


    }//end class
}//end namespace
