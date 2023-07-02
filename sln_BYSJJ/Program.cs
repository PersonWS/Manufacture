using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
namespace sln_BYSJJ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.Length > 1)
            {
                MessageBox.Show("此软件已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Thread.Sleep(1000);
                Environment.Exit(1);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SplashScreen.SplashScreen.ShowSplashScreen();
            Application.DoEvents();

            utility.dSV = Controller.CONT_DSVInterlockingInfo.LoadList();

            frmLogin f = new frmLogin();
            if (f.ShowDialog() == DialogResult.OK)
            {
                utility.dSV.SW_User = utility.loginUserID;
                Application.Run(new frmMain());
                f.Dispose();
                f.Close();
            }
        }
    }
}
