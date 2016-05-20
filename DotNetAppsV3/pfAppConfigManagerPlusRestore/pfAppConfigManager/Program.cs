using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.IO;
using AppGlobals;
using PFMessageLogs;

namespace pfAppConfigManager
{
    static class Program
    {
        private static StringBuilder _msg = new StringBuilder();
        public static ApplicationOptionsForm _appOptionsForm;
        public static string _exeFileName = string.Empty;
        public static string _helpFileName = string.Empty;
        public static string _helpFileTopic = string.Empty;
        public static string _origSettingsFileName = "pfSettingsOrig.xml";

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

            //MessageBox.Show(Environment.MachineName);

            if (ArgsAreValid(args))
            {
                _appOptionsForm = new ApplicationOptionsForm();
                _appOptionsForm.ExeFileName = _exeFileName;
                _appOptionsForm.HelpFileName = _helpFileName;
                _appOptionsForm.HelpFileTopic = _helpFileTopic;
                _appOptionsForm.OrigSettingsFileName = _origSettingsFileName;
                Application.Run(_appOptionsForm);
            }

        }

        private static bool ArgsAreValid(string[] args)
        {
            bool valid = false;

            if (args != null)
            {
                //_msg.Length = 0;
                //for (int i = 0; i < args.Length; i++)
                //{
                //    _msg.Append("Arg[");
                //    _msg.Append(i.ToString());
                //    _msg.Append("] = ");
                //    _msg.Append(args[i].ToString());
                //    _msg.Append(Environment.NewLine);
                //}
                //AppMessages.DisplayAlertMessage(_msg.ToString());

                if (args.Length >= 1)
                {
                    _exeFileName = args[0];
                    valid = true;
                }
                if (args.Length >= 2)
                {
                    _helpFileName = args[1];
                }
                if (args.Length >= 3)
                {
                    _helpFileTopic = args[2];
                }
                //fourth parameter only needed if calling application changed
                //the name of file containing the original settings
                //from pfSettingsOrig.xml to something else.
                if (args.Length >= 4)
                {
                    _origSettingsFileName = args[3];
                }
            }

            if (valid == false)
            {
                _msg.Length = 0;
                _msg.Append("Invalid arguments detected. Configuration Manager terminating with error.");
                _msg.Append(Environment.NewLine);
                _msg.Append("Arguments passed to configuration manager: ");
                if (args == null)
                {
                    _msg.Append("<Null>");
                }
                else if (args.Length == 0)
                {
                    _msg.Append("<blank>");
                }
                else
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        _msg.Append("Arg #");
                        _msg.Append(i.ToString());
                        _msg.Append(": ");
                        _msg.Append(args[i].ToString());
                        _msg.Append(Environment.NewLine);
                    }

                }

                AppMessages.DisplayErrorMessage(_msg.ToString());
            }
            else
            {
                //valid = true up to this point
                //now verify the files specified by the parameters exist
                _msg.Length = 0;
                if (File.Exists(_exeFileName) == false)
                {
                    valid = false;
                    _msg.Append(_exeFileName);
                    _msg.Append(" does not exist.");
                    _msg.Append(Environment.NewLine);
                    AppMessages.DisplayErrorMessage(_msg.ToString());
                }
                if (args.Length >= 2)
                    if (File.Exists(_helpFileName) == false)
                    {
                        _msg.Append(_helpFileName);
                        _msg.Append(" does not exist.");
                        _msg.Append(Environment.NewLine);
                        AppMessages.DisplayWarningMessage(_msg.ToString());
                    }
                if (args.Length >= 4)
                    if (File.Exists(_origSettingsFileName) == false)
                    {
                        _msg.Append(_origSettingsFileName);
                        _msg.Append(" does not exist.");
                        _msg.Append(Environment.NewLine);
                        AppMessages.DisplayWarningMessage(_msg.ToString());
                    }
            }



            return valid;
        }

    }//end class
}//end namespace
