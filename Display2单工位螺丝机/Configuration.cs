using System;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

namespace PDAMaster
{
    public class ConfigurationKeys
    {


        /// <summary>
        /// 工位数
        /// </summary>
        public static int WorkStationCount = 0;
        /// <summary>
        /// 最大任务数
        /// </summary>
        public static int teachPendantTaskID_Max = 0;
        /// <summary>
        /// 最小任务数，0或者1
        /// </summary>
        public static int TaskIDStartValue = 0;
        /// <summary>
        /// plc ip
        /// </summary>
        public static string PLC_IP = "10.217.17.5";
        /// <summary>
        /// plc port
        /// </summary>
        public static int PLC_Port = 0;
        /// <summary>
        /// 机架
        /// </summary>
        public static short PLC_Rack = 0;
        /// <summary>
        /// 插槽
        /// </summary>
        public static short PLC_Slot = 0;
        /// <summary>
        /// 电批数量
        /// </summary>
        public static int ScrewMachineCount = 0;
        /// <summary>
        /// 电批地址1
        /// </summary>
        public static string ScrewMachineIP1 = "10.217.17.8";
        /// <summary>
        /// 电批端口1
        /// </summary>
        public static int ScrewMachinePort1 = 5000;
        /// <summary>
        /// 电批1标志
        /// </summary>
        public static bool ScrewMachineFlag1 = true;
        /// <summary>
        /// 电批地址2
        /// </summary>
        public static string ScrewMachineIP2 = "192.168.1.100";
        /// <summary>
        /// 电批端口2
        /// </summary>
        public static int ScrewMachinePort2 = 0;
        /// <summary>
        /// 电批标志2
        /// </summary>
        public static bool ScrewMachineFlag2 = false;

        /// <summary>
        /// 上传PLC的SN码首字母
        /// </summary>
        public static string PLC_UpLoad1Code = "P";
        /// <summary>
        /// 上传MES的SN码首字母
        /// </summary>
        public static string MES_UpLoad1Code = "M";

        /// <summary>
        /// 是否检查PLC和MES首字母，0不检查，1检查P，2检查M,3都检查
        /// </summary>
        public static int Checking_PLC_MES = 3;
        /// <summary>
        /// M码长度
        /// </summary>
        //public static int MCodeLength = 28;
    }

    class Configuration
    {
        //public static log4net.l logger = NLog.LogManager.GetLogger("Configuration");

        Hashtable list = new Hashtable();

        private static readonly Configuration instance = new Configuration();
        public static Configuration Instance
        {
            get
            {
                return instance;
            }
        }


        public static bool ModifyXmlInformation(string xmlFilePath, string paramName, string nodeName, string nodeValue)
        {
            try
            {
                XmlDocument myXmlDoc = new XmlDocument();

                myXmlDoc.Load(xmlFilePath);
                string xPath = string.Format("/paramlist/param[@ParamName=\"{0}\"]", paramName);
                XmlNode xn = myXmlDoc.SelectSingleNode(xPath);



                XmlElement xe = (XmlElement)xn;
                xe.GetElementsByTagName(nodeName).Item(0).InnerText = nodeValue;



                myXmlDoc.Save(xmlFilePath);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool autoSave = false;
        public bool AutoSave
        {
            get { return autoSave; }
            set { autoSave = value; }
        }

        string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        private Configuration()
        {
            filePath = GetFilePath();
            CheckUtil.SetFileAttribute(filePath);

            Load();
        }

        public void SetValue(string key, object value)
        {
            // update internal list
            list[key] = value;

            // update settings file
            if (autoSave)
            {
                Save();
            }
        }

        /// <summary>
        /// Return specified settings as string.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            object result = null;
            lock (list.SyncRoot)
            {
                result = list[key];
            }
            return (result == null) ? String.Empty : result.ToString();
        }

        /// <summary>
        /// Return specified settings as integer.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetInt(string key)
        {
            string result = GetString(key);
            return (result == String.Empty) ? 0 : Convert.ToInt32(result);
        }

        /// <summary>
        /// Return specified settings as boolean.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetBool(string key)
        {
            string result = GetString(key);
            return (result == String.Empty) ? false : Convert.ToBoolean(result);
        }

        public void Load()
        {
            XmlTextReader reader = null;
            try
            {
                list.Clear();

                reader = new XmlTextReader(filePath);
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "add"))
                        list[reader.GetAttribute("key")] = reader.GetAttribute("value");
                }
            }
            catch (Exception ex)
            {
                //logger.ErrorException("Load()", ex);
            }
            finally
            {
                if (reader.ReadState != ReadState.Closed)
                {
                    reader.Close();
                }
            }
        }

        public void Save()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                XmlNodeList nodeList = doc.SelectNodes("/configuration/appSettings/add");
                foreach (XmlNode node in nodeList)
                {
                    node.Attributes["value"].Value = GetString(node.Attributes["key"].Value);
                }

                doc.Save(filePath);

            }
            catch (Exception ex)
            {
                //logger.ErrorException("save()", ex);
            }
        }

        private string GetFilePath()
        {
            return Assembly.GetExecutingAssembly().GetName().CodeBase + ".config";
        }

        private static string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        [DllImport("coredll", EntryPoint = "AddFontResource")]
        private static extern int AddFontResource([In, MarshalAs(UnmanagedType.LPWStr)] string fontSource);
        [DllImport("coredll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        private static int installFont1 = AddFontResource(@"FlashDisk/FONTS/msyh.ttf");  //这是字体的安装  返回不为0即成功
        private static int installFont2 = AddFontResource(@"FlashDisk/FONTS/QuartzMS.TTF");


        //QuartzMS
        //"YaHei Consolas Hybrid"

        public static Font getFont(string fontname, float fontsize, FontStyle fontstyle)
        {
            Font rFonts = null;
            //try
            //{
            //枚举字体
            System.Drawing.Text.InstalledFontCollection enumFonts = new System.Drawing.Text.InstalledFontCollection();
            FontFamily[] fonts = enumFonts.Families;
            int ifont = 0;
            for (int i = 0; i < fonts.Length; i++)
            {
                if (fonts[i].Name == fontname)
                {
                    ifont = i;
                    rFonts = new Font(fonts[i], fontsize, fontstyle);
                    break;
                }
            }
            //lab:

            //}
            //catch (Exception ex)
            //{

            //}
            //{
            //    MessageBox.Show(ex.Message);
            //Font rFonts = new Font(enumFonts.Families[ifont], fontsize, fontstyle);

            //rFonts = new Font(enumFonts.Families[ifont], fontsize, fontstyle);
            return rFonts;
            //}

        }
    }


}
