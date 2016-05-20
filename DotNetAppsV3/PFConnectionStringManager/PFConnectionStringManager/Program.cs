using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using AppGlobals;
using PFMessageLogs;
using PFConnectionStrings;

namespace PFConnectionStringManager
{
    static class Program
    {
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

            PFConnectionStringManagerForm frm = new PFConnectionStringManagerForm();
            frm.ShowDialog();
            //don't use Application.Run: program will not close properly


        }
    }//end class
}//end namespace
