using System;
using System.IO;
using System.Reflection;

namespace sln_TP
{
    public class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");//这里的 loginfo 和 log4net.config 里的名字要一样
        public static readonly log4net.ILog logerror = log4net.LogManager.GetLogger("logerror");//这里的 logerror 和 log4net.config 里的名字要一样
        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info + "用户ID:" + utility.loginUserID);
            }
        }

        public static void WriteLog(string info, Exception ex)
        {
            if (logerror.IsErrorEnabled)
            {
                logerror.Error(info + ",用户ID:" + utility.loginUserID, ex);
            }
        }


        public static bool DeleteLogFilesForOutDate()
        {

            try
            {
                string strBasepath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                string DirPath = strBasepath + "\\log";   //指定一个路径，此处路径为应用程序默认路径
                string[] folders = Directory.GetDirectories(DirPath);
                if (folders.Length > 0)
                {
                    foreach (string folder in folders)
                    {
                        string[] subfolders = Directory.GetDirectories(folder);
                        foreach (string subfolder in subfolders)
                        {
                            string[] ss = subfolder.Split('\\');
                            int fileyear = Convert.ToInt32(ss[ss.Length - 1].Substring(0, 4));
                            int today = DateTime.Now.Year;
                            if (today - fileyear > 1)
                            {
                                DirectoryInfo di = new DirectoryInfo(subfolder);
                                di.Delete(true);
                            }
                        }
                    }
                    LogHelper.WriteLog("自动删除日志文件完成");
                }

                return true;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("删除日志文件出错，", ex);
                return false;
            }
        }

        /* https://www.cnblogs.com/Alex-Mercer/p/12712460.html
         * 5.调用
            a.CMD直接调用

            LogHelper.WriteLog("增加日志");
            b.MVC调用。需要在 Global.asax 的Application_Start 里面注册

              //log4Net 
                        log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Log4NetServe/log4Net.config")));//log4Net.config文件路径
                        LogHelper.WriteLog("IIS开始启动");
         * */
    }
}
