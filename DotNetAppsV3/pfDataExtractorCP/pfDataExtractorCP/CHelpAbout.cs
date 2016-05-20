using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AppGlobals;
using PFSystemObjects;
using System.Reflection;

namespace pfDataExtractorCP
{
#pragma warning disable 1591
    public partial class HelpAboutForm : Form
    {
        public HelpAboutForm()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void CHelpAbout_Load(object sender, EventArgs e)
        {
            StringBuilder sAppInfo = new StringBuilder();
            StringBuilder sSystemInfo = new StringBuilder();
            StringBuilder sVersionInfo = new StringBuilder();
            StringBuilder sRegistrationInfo = new StringBuilder();
            Version oVersion = AppInfo.AssemblyVersionEx;


            sVersionInfo.Length = 0;
            sVersionInfo.Append("Version ");
            sVersionInfo.Append(oVersion.Major.ToString());
            sVersionInfo.Append(".");
            sVersionInfo.Append(oVersion.Minor.ToString());
            this.lblVersion.Text = sVersionInfo.ToString();

            txtApplicationInfo.WordWrap = false;
            txtApplicationInfo.Multiline = true;
            txtApplicationInfo.ReadOnly = true;
            txtApplicationInfo.ScrollBars = ScrollBars.Both;

            sAppInfo.Length = 0;
            sAppInfo.Append("Product: ");
            sAppInfo.Append(AppInfo.AssemblyProduct);
            sAppInfo.Append("\r\n");
            sAppInfo.Append("Assembly: ");
            sAppInfo.Append(AppInfo.AssemblyName);
            sAppInfo.Append("\r\n");
            sAppInfo.Append("Version: ");
            sAppInfo.Append(AppInfo.AssemblyVersion);
            sAppInfo.Append("\r\n");
            sAppInfo.Append(AppInfo.AssemblyCopyright);
            sAppInfo.Append("\r\n");
            sAppInfo.Append("Description: ");
            sAppInfo.Append(AppInfo.AssemblyDescription);
            sAppInfo.Append("\r\n");
            sAppInfo.Append("Execution path: ");
            sAppInfo.Append(new Uri(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);
            txtApplicationInfo.Text = sAppInfo.ToString();

            txtRegistrationInfo.WordWrap = false;
            txtRegistrationInfo.Multiline = true;
            txtRegistrationInfo.ReadOnly = true;
            txtRegistrationInfo.ScrollBars = ScrollBars.None;

            sRegistrationInfo.Length = 0;
            sRegistrationInfo.Append("License Type: ");
            sRegistrationInfo.Append("N/A");  //not register, trial or permanent
            sRegistrationInfo.Append("\r\n");
            sRegistrationInfo.Append("License not required for this application.");
            sRegistrationInfo.Append("\r\n");
            sRegistrationInfo.Append("UserName: ");
            sRegistrationInfo.Append(Environment.UserName);
            sRegistrationInfo.Append("\r\n");
            sRegistrationInfo.Append("UserDomain: ");
            sRegistrationInfo.Append(Environment.UserDomainName);
            txtRegistrationInfo.Text = sRegistrationInfo.ToString();



            txtSystemInfo.WordWrap = false;
            txtSystemInfo.Multiline = true;
            txtSystemInfo.ReadOnly = true;
            txtSystemInfo.ScrollBars = ScrollBars.Both;

            sSystemInfo.Length = 0;
            sSystemInfo.Append("Computer: ");
            sSystemInfo.Append(Environment.MachineName);
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("OS Version: ");
            sSystemInfo.Append(Environment.OSVersion);
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append(".NET version:  ");
            sSystemInfo.Append(System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion());
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Memory (total): ");
            sSystemInfo.Append(SysInfo.TotalMemory.ToString("#,##0"));
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Memory (free):  ");
            sSystemInfo.Append(SysInfo.TotalFreeMemory.ToString("#,##0"));
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Memory (committed):  ");
            sSystemInfo.Append(SysInfo.TotalCommittedMemory.ToString("#,##0"));
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Memory (% used):  ");
            sSystemInfo.Append(SysInfo.PercentMemoryUsage.ToString("#,##0"));
            sSystemInfo.Append("%");
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Current active user:  ");
            sSystemInfo.Append(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Current Logon user:  ");
            sSystemInfo.Append(Environment.UserName);
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Windows folder:  ");
            sSystemInfo.Append(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("Application data folder:  ");
            sSystemInfo.Append(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData));
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("User documents folder:  ");
            sSystemInfo.Append(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            if (Environment.GetEnvironmentVariable("Temp") != null)
            {
                sSystemInfo.Append("\r\n");
                sSystemInfo.Append("Temp folder:  ");
                sSystemInfo.Append(Environment.GetEnvironmentVariable("Temp"));
            }
            sSystemInfo.Append("\r\n");
            sSystemInfo.Append("User profile folder:  ");
            sSystemInfo.Append(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            this.txtSystemInfo.Text = sSystemInfo.ToString();
            this.txtSystemInfo.Select(0, 0);

        }

    }
#pragma warning restore 1591
}