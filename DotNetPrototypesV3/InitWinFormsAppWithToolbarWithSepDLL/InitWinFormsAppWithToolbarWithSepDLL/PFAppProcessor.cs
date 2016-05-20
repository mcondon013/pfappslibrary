using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;
using System.IO;
using PFPrinterObjects;
using PFProcessObjects;
using PFMessageLogs;

namespace InitWinFormsAppWithToolbarWithSepDLL
{
    public class PFAppProcessor
    {
        private StringBuilder _msg = new StringBuilder();
        private StringBuilder _str = new StringBuilder();
        private bool _saveErrorMessagesToAppLog = false;

        private MessageLog _messageLog;
        private string _appConfigManagerExe = @"pfAppConfigManager.exe";

        private string _helpFilePath = string.Empty;

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

        //tests
        public void RunShowDateTimeTest(string testDescription)
        {
            try
            {
                _msg.Length = 0;
                _msg.Append("RunShowDateTimeTest started ...");
                Program._messageLog.WriteLine(_msg.ToString());

                _msg.Length = 0;
                _msg.Append(testDescription);
                _msg.Append(": \r\n");
                _msg.Append("Current date/time is ");
                _msg.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                _msg.Append("\r\n");
                Program._messageLog.WriteLine(_msg.ToString());
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
                _msg.Append("... RunShowDateTimeTest finished.");
                Program._messageLog.WriteLine(_msg.ToString());

            }
        }


        public void FileOpenMruTest(MruStripMenu msm, string filename)
        {

            try
            {
                msm.AddFile(filename);
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


        }

        /// <summary>
        /// Routine to display the application options configuration manager.
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
                //proc.Arguments = @"C:\ProFast\Projects\DotNetAppsV3\pfFolderSize\pfFolderSize\bin\Release\pfFolderSize.exe C:\ProFast\Projects\DotNetAppsV3\pfFolderSize\pfFolderSize\pfFolderSize.chm ""Change an Application Setting""";
                proc.Arguments = currAppExePath + " " + _helpFilePath + " " + "\"Change an Application Setting\"";

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


    }//end class
}//end namespace
