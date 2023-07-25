using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ScrewMachineManagementSystem.CenterControl;

namespace ScrewMachineManagementSystem
{


    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]


        static void Main()
        {
            //Application.Run(new CenterDemo());return;

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

            SplashScreen.SplashScreen.ShowSplashScreen(utility.SystemName);
            Application.DoEvents();

     

            try
            {

                LogHelper.DeleteLogFilesForOutDate();
                utility.getParamList();

                SplashScreen.SplashScreen.SetStatus("加载系统参数...");

                utility.dSV = Controller.CONT_DSVInterlockingInfo.LoadList();
                utility.stationId=utility.dSV.stationId;    
                S7NetPlus.alarmPoints = Controller.CONT_AlarmPoints.LoadList("");
                //PLC报警信息，按DB块字节读取，每个字节包含8个位
                foreach (Model.AlarmPoints alarmPoint in S7NetPlus.alarmPoints)
                {
                    S7NetPlus.PLC_DB_Address plcdb = new S7NetPlus.PLC_DB_Address();
                    plcdb.db = alarmPoint.DB;
                    plcdb.address = alarmPoint.Address;
                    int aindex = S7NetPlus.plc_DB_Alarm.FindIndex(a => a.db == alarmPoint.DB && a.address == alarmPoint.Address);
                    if (aindex < 0)
                        S7NetPlus.plc_DB_Alarm.Add(plcdb);
                }
                //Thread.Sleep(1000);

                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取配置文件出错" + ex.Message, "系统提示");
                return;
            }

            FormLogin f = new FormLogin();
            if (f.ShowDialog() == DialogResult.OK)
            {
                f.Dispose();
                f.Close();
                Application.Run(new FormMain());

            }
            // Application.Run(new FormLogin());
            //界面转换
            //FormLogin loginForm = new FormLogin();
            //loginForm.ShowDialog();

            //if (loginForm.DialogResult == DialogResult.OK)
            //{
            //    loginForm.Dispose();
            //    Application.Run(new FormMain());
            //}
            //else if (loginForm.DialogResult == DialogResult.Cancel)
            //{
            //    loginForm.Dispose();
            //    return;
            //}




        }
    }
}
