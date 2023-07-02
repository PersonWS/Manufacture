using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace sln_TJSJJ
{
    public class utility
    {
        /// <summary>
        /// 登录状态，T已登录，F已注销
        /// </summary>
        public static bool isLogin;
        /// <summary>
        /// 工作模式
        /// </summary>
        public static bool LoginModeEngineer = false;
        /// <summary>
        /// 登录用户信息
        /// </summary>
        public static string loginUserID;
        /// <summary>
        /// 本机信息
        /// </summary>
        public static string stationId;
        /// <summary>
        /// 互锁信息
        /// </summary>
        public static Model.DSVInterlockingInfo dSV = new Model.DSVInterlockingInfo();
        /// <summary>
        /// 数据库路径   app.config connectionStrings
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string LoadAppConfigString(string id = "sqlDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        /// <summary>
        /// 显示消息,interval=0不自动关闭
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="interval">超时关闭，0不自动关闭</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        //public static DialogResult ShowMessage(string msg, int interval = 0, string title = "系统提示")
        //{
        //    MessageDialog messageDlg = new MessageDialog(msg, interval, title);
        //    return messageDlg.ShowDialog();
        //}
    }
}
