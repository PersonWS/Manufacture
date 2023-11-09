using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Configuration;

namespace ScrewMachineManagementSystem
{
    public delegate void LogHappenEventHandler(string log);
    public class LogUtility
    {
        private static readonly object obj = new object();

        public static event LogHappenEventHandler LogHappenEvent;

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">0:LogInfo   1:LogError  255:不记录</param>
        /// <param name="message"></param>
        public static void RecordLog(int level, string message)
        {
            switch (level)
            {
                case 1:
                    ErrorLog_custom(message, "LogError\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
                    break;
                case 255:
                    break;
                default:
                    ErrorLog_custom(message, "LogInfo\\" + DateTime.Now.ToString("yyyy-MM") + "\\");
                    break;
            }
        }

        /// <summary>
        /// 异常日志工具类
        /// </summary>
        /// <param name="ex"></param>
        public static void ErrorLog(Exception ex, string tag = "")
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (LogHappenEvent != null)
                {
                    LogHappenEvent(o.ToString());
                }
            }, ex.ToString() + tag);

            try
            {
                lock (obj)
                {

                    using (System.IO.FileStream file = new System.IO.FileStream(GetFileName(), FileMode.Append, FileAccess.Write))
                    {
                        System.IO.StreamWriter sw = new StreamWriter(file);
                        sw.WriteLine("当前时间：" + DateTime.Now.ToString());
                        sw.WriteLine("异常信息：" + ex.Message);
                        sw.WriteLine("异常对象：" + ex.Source);
                        sw.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
                        sw.WriteLine("触发方法：" + ex.TargetSite);
                        sw.WriteLine("Tag：" + tag);
                        sw.WriteLine();
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog(e.Message, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// 用户自定义错误代码 2017-10-17 add by ws
        /// </summary>
        /// <param name="error_custom"></param>
        public static void ErrorLog_custom(string error_custom)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (LogHappenEvent != null)
                {
                    LogHappenEvent(o.ToString());
                }
            }, error_custom);
            try
            {
                lock (obj)
                {
                    using (System.IO.FileStream file = new System.IO.FileStream(GetFileName(), FileMode.Append, FileAccess.Write))
                    {
                        System.IO.StreamWriter sw = new StreamWriter(file);
                        sw.WriteLine("当前时间：" + DateTime.Now.ToString());
                        sw.WriteLine("提示信息：" + error_custom);
                        sw.WriteLine();
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog(e.Message, EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// 在当前运行目录下./log/，指定的文件名来记录日志
        /// </summary>
        /// <param name="fileNameOnCurrentDir"></param>
        /// <param name="error_custom"></param>
        public static void ErrorLog_custom(string error_custom, string fileNameOnCurrentDir)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (LogHappenEvent != null)
                {
                    LogHappenEvent(o.ToString());
                }
            }, error_custom);
            try
            {
                lock (obj)
                {
                    string path = Assembly.GetExecutingAssembly().Location;
                    path = System.IO.Path.Combine(path.Substring(0, path.LastIndexOf('\\')), "Log");
                    System.IO.Directory.CreateDirectory(path);
                    string fileName = "";
                    if (string.IsNullOrEmpty(fileNameOnCurrentDir))
                    {
                        fileName = path + "\\Log" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
                    }
                    else
                    {
                        fileName = path + "\\" + fileNameOnCurrentDir + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
                    }


                    using (System.IO.FileStream file = new System.IO.FileStream(fileName, FileMode.Append, FileAccess.Write))
                    {
                        System.IO.StreamWriter sw = new StreamWriter(file);
                        sw.WriteLine("当前时间：" + DateTime.Now.ToString());
                        sw.WriteLine("提示信息：" + error_custom);
                        sw.WriteLine();
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog(e.Message, EventLogEntryType.Error);
            }
        }


        private static string GetFileName()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = System.IO.Path.Combine(path.Substring(0, path.LastIndexOf('\\')), "Log");
            System.IO.Directory.CreateDirectory(path);
            string fileName = path + "\\Log" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
            return fileName;

        }

        private static string GetUsrLogFileName()
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = System.IO.Path.Combine(path.Substring(0, path.LastIndexOf('\\')), "Log");
            System.IO.Directory.CreateDirectory(path);
            string fileName = path + "\\UsrLog" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
            return fileName;

        }


        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="strMessag"></param>
        /// <param name="type"></param>
        public static void WriteLog(string strMessag, EventLogEntryType type)
        {

            //Warning转义为调试信息
            //if (type == EventLogEntryType.Warning)
            //{
            //    return;
            //}
            EventLog eventLog = new EventLog();
            try
            {
                if (!EventLog.SourceExists("Display3"))
                {
                    EventLog.CreateEventSource("Display3", "");

                }
                eventLog.Source = "Display3";
                eventLog.WriteEntry(strMessag, type);
                eventLog.Close();
            }
            catch
            { }


        }

        /// <summary>
        /// 清理日志
        /// </summary>
        public static void ClearLog()
        {


            EventLog eventLog = new EventLog();
            if (!EventLog.SourceExists("Display3"))
            {
                EventLog.CreateEventSource("Display3", "");

            }
            eventLog.Source = "Display3";
            eventLog.Close();

        }

        public static void ErrorLog_filllog(string error_custom, string fileNameOnCurrentDir = null)
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                if (LogHappenEvent != null)
                {
                    LogHappenEvent(o.ToString());
                }
            }, error_custom);
            try
            {
                lock (obj)
                {
                    string path = Assembly.GetExecutingAssembly().Location;
                    path = System.IO.Path.Combine(path.Substring(0, path.LastIndexOf('\\')), "Log");
                    System.IO.Directory.CreateDirectory(path);
                    string fileName = "";
                    if (string.IsNullOrEmpty(fileNameOnCurrentDir))
                    {
                        fileName = path + "\\Log" + DateTime.Today.ToString("yyyy-MM-dd") + "FillLog" + ".txt";
                    }
                    else
                    {
                        fileName = path + "\\" + fileNameOnCurrentDir + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
                    }


                    using (System.IO.FileStream file = new System.IO.FileStream(fileName, FileMode.Append, FileAccess.Write))
                    {
                        System.IO.StreamWriter sw = new StreamWriter(file);
                        sw.WriteLine("当前时间：" + DateTime.Now.ToString());
                        sw.WriteLine("提示信息：" + error_custom);
                        sw.WriteLine();
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                WriteLog(e.Message, EventLogEntryType.Error);
            }
        }


        public static void WriteConfiguration(string key, string value)
        {
            //增加的内容写在appSettings段下 <add key="RegCode" value="0"/>  
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件   
        }

        public static string ReadConfiguration(string key)
        {
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 是否能ping通指定的主机
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>true 通过 false 不通</returns>
        public static bool Ping(string ip)
        {
            try
            {
                System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
                options.DontFragment = true;
                string data = "test data!";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 4000;
                System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
                return (reply.Status == System.Net.NetworkInformation.IPStatus.Success) ? true : false;
            }
            catch (Exception e)
            {
                return false;
            }
        }


    }
}
