using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppGlobals;

namespace InitConsoleApp
{
    class Program
    {
        private static StringBuilder _msg = new StringBuilder();
        private static int _exitCode = 0;
        private static string _mydocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string _messageLogFile = AppConfig.GetStringValueFromConfigFile("ApplicationLogFileName", "InitConsoleAppLog.txt");
        private static bool _saveErrorMessagesToAppLog = true;

        static void Main(string[] args)
        {

            try
            {
                if (System.IO.Path.GetDirectoryName(_messageLogFile).Length == 0)
                    _messageLogFile = _mydocumentsFolder + @"\" + _messageLogFile;
                ConsoleMessages.AppLogFilename = _messageLogFile;

                _msg.Length = 0;
                _msg.Append("AppLogFilename = ");
                _msg.Append(_messageLogFile);
                Console.WriteLine(_msg.ToString());

                _exitCode = RunTests();

            }
            catch (System.Exception ex)
            {
                _exitCode = 1;
                _msg.Length = 0;
                _msg.Append(AppGlobals.ConsoleMessages.FormatErrorMessage(ex));
                Console.WriteLine(_msg.ToString());
                ConsoleMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("Exit code is ");
                _msg.Append(_exitCode.ToString());
                Console.WriteLine(_msg.ToString());
            }

            Console.WriteLine("Press Enter Key to exit the program:");
            Console.ReadLine();

            Environment.ExitCode = _exitCode;
       

        }//end Main

        private static int RunTests()
        {
            int ret = 0;


            try
            {
                _msg.Length = 0;
                _msg.Append("RunTests started ...");
                Console.WriteLine(_msg.ToString());

                Tests.Test1();

                ret = 0;
            }
            catch (System.Exception ex)
            {
                ret = 1;
                _msg.Length = 0;
                _msg.Append(AppGlobals.AppMessages.FormatErrorMessage(ex));
                Console.WriteLine(_msg.ToString());
                ConsoleMessages.DisplayErrorMessage(_msg.ToString(), _saveErrorMessagesToAppLog);
            }
            finally
            {
                _msg.Length = 0;
                _msg.Append("... RunTests finished.");
                Console.WriteLine(_msg.ToString());
            }

            return ret;
        }




    }//end class

}//end namespace
