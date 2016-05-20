using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using AppGlobals;
using PFMessageLogs;
using PFRandomDataForms;

namespace pfRandomDataSources
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CAppGlobalErrorHandler.WriteToAppLog = true;
            CAppGlobalErrorHandler.WriteToEventLog = false;
            CAppGlobalErrorHandler.CancelApplicationOnGlobalError = false;

            Application.ThreadException += new ThreadExceptionEventHandler(CAppGlobalErrorHandler.GlobalErrorHandler);

            //MessageBox.Show(Environment.MachineName);

            RandomDataFormsManager frm = new RandomDataFormsManager();
            frm.ShowDialog();
            //don't use Application.Run: program will not close properly
        }
    }//end class
}//end namespace
