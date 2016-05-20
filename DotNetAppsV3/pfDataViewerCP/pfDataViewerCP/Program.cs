using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using AppGlobals;
using PFMessageLogs;

namespace pfDataViewerCP
{
    static class Program
    {
        public static MainForm _mainForm;
        public static MessageLog _messageLog;
        public static bool _showMainForm = true;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
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
            _messageLog.Caption = "pfDataViewerCP Log";
            _messageLog.ShowDatetime = false;
            _messageLog.Font = "Lucida Console";
            _messageLog.FontSize = (float)10.0;
            _messageLog.ShowWindow();

            //MessageBox.Show(Environment.MachineName);


            try
            {
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
    }//end class
}//end namespace
