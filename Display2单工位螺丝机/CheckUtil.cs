
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace PDAMaster
{
    public class CheckUtil
    {
        #region 检查字符中不能含有特殊字符

        public static string IsUsableName(string viewName)
        {
            string strResult = null;
            if (viewName.Length > 0)
            {
                char[] strFiltersChar = { '%', '!', '&', '#', '@', '\\', '?', '<', '*', '"', '>', '|' };
                for (int i = 0; i <= strFiltersChar.Length - 1; i++)
                {
                    if (viewName.IndexOf(strFiltersChar[i].ToString()) > -1)
                    {
                        if (strResult == null)
                        {
                            strResult = "输入字符中含有非法字符:" + strFiltersChar[i].ToString();
                        }
                        else
                        {
                            strResult += "," + strFiltersChar[i].ToString();
                        }
                    }
                }
            }
            return strResult;
        }
        #endregion

        #region 去掉文件只读属性
        /// <summary>
        /// 去掉文件只读属性
        /// </summary>
        /// 
        /// <param name="fullFileName"></param>
        public static void SetFileAttribute(string fullFileName)
        {
            if (File.Exists(fullFileName))
            {
                FileInfo fileInfo = new FileInfo(fullFileName);
                if (fileInfo.Attributes.ToString().IndexOf("ReadOnly") > -1)
                    fileInfo.Attributes = FileAttributes.Normal;
            }
        }
        #endregion

        #region  检查字符是否可用做运单编号

        public static bool IsUsableFormNumber(string str)
        {
            return Regex.IsMatch(str, "[^A-Za-z0-9]");
        }
        #endregion

        #region  检查字符是否可用的URL

        public static bool IsUsableURL(string str)
        {
            //Regex r = new Regex(@"^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/",RegexOptions.Compiled);
            //return r.IsMatch(str);
            return Regex.IsMatch(str, "http://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");
            //return Regex.IsMatch(str, "^http://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");

        }
        #endregion

        #region  是否是正确的 当前日期

        public static bool IsUsableDateTime(string str)
        {
            bool isUnaable = false;

            try
            {
                DateTime curDt = Convert.ToDateTime(str);

                if (curDt.Year == DateTime.Now.Year && curDt.Date == DateTime.Now.Date)
                {
                    isUnaable = true;
                }
                else
                {
                    isUnaable = false;
                }
            }
            catch
            {
                isUnaable = false;
            }
            return isUnaable;
        }
        #endregion

        #region IsInt 是否是整数

        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, "^([0-9]*)$");
        }

        #endregion

        #region IsIp 是否IP地址
        /// <summary>
        /// 判断是否IP地址
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIp(string str)
        {
            IPAddress IP;
            return IPAddress.TryParse(str, out IP); 
        }
        #endregion
     
        #region IsTelNumber 是否电话号码

        public static bool IsTelNumber(string telNumber)
        {
            return Regex.IsMatch(telNumber, "^([0-9()-]*)$");
        }

        #endregion

        #region IsUsable 是否为可用的字符串

        public static bool IsUsable(string str)
        {
            return Regex.IsMatch(str, "^([0-9a-zA-Z_]*)$");
        }

        #endregion

        #region IsIdCard  身份证号码是否符合规范

        /// <summary>
        /// 判断输入对象是否是正确身份证号
        /// </summary>
        /// <param name="o">对象类型值</param>
        /// <returns></returns>
        public static bool IsIdCard(string idCard)
        {
            Hashtable hsAreaCode = new Hashtable();
            hsAreaCode.Add("11", "北京");
            hsAreaCode.Add("12", "天津");
            hsAreaCode.Add("13", "河北");
            hsAreaCode.Add("14", "山西");
            hsAreaCode.Add("15", "内蒙古");
            hsAreaCode.Add("21", "辽宁");
            hsAreaCode.Add("22", "吉林");
            hsAreaCode.Add("23", "黑龙江");
            hsAreaCode.Add("31", "上海");
            hsAreaCode.Add("32", "江苏");
            hsAreaCode.Add("33", "浙江");
            hsAreaCode.Add("34", "安徽");
            hsAreaCode.Add("35", "福建");
            hsAreaCode.Add("36", "江西");
            hsAreaCode.Add("37", "山东");
            hsAreaCode.Add("41", "河南");
            hsAreaCode.Add("42", "湖北");
            hsAreaCode.Add("43", "湖南");
            hsAreaCode.Add("44", "广东");
            hsAreaCode.Add("45", "广西");
            hsAreaCode.Add("46", "海南");
            hsAreaCode.Add("50", "重庆");
            hsAreaCode.Add("51", "四川");
            hsAreaCode.Add("52", "贵州");
            hsAreaCode.Add("53", "云南");
            hsAreaCode.Add("54", "西藏");
            hsAreaCode.Add("61", "陕西");
            hsAreaCode.Add("62", "甘肃");
            hsAreaCode.Add("63", "青海");
            hsAreaCode.Add("64", "宁夏");
            hsAreaCode.Add("65", "新疆");
            hsAreaCode.Add("71", "台湾");
            hsAreaCode.Add("81", "香港");
            hsAreaCode.Add("82", "澳门");
            hsAreaCode.Add("91", "国外");

            if (idCard != null && idCard.Length > 2)
            {
                //地区检验
                bool chkArea = false;
                IEnumerator keys = hsAreaCode.Keys.GetEnumerator();
                for (int i = 0; keys.MoveNext(); i++)
                {
                    if (keys.Current.ToString() == idCard.Substring(0, 2))
                    {
                        chkArea = true;
                        break;
                    }
                }

                if (chkArea == false)
                {
                    return false;
                }
                else
                {

                    bool chkId = false;
                    switch (idCard.Length)
                    {
                        case 15:
                            chkId = Chk15IdCard(idCard);
                            break;
                        case 18:
                            chkId = Chk18IdCard(idCard);
                            break;
                    }
                    if (chkId == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断15位身份证
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        private static bool Chk15IdCard(string idCard)
        {
            int yearNum = int.Parse(idCard.Substring(6, 2)) + 1900;
            string regex = "";
            //判断是否润年
            if (yearNum % 4 == 0 || (yearNum % 100 == 0 && yearNum % 4 == 0))
            {
                regex = "^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}$";
                return Regex.IsMatch(idCard, regex);
            }
            else
            {
                regex = "^[1-9][0-9]{5}[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}$";
                return Regex.IsMatch(idCard, regex);
            }
        }

        /// <summary>
        /// 判断18位身份证
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        private static bool Chk18IdCard(string idCard)
        {
            int yearNum = int.Parse(idCard.Substring(6, 2)) + 1900;
            string regex = "";
            //string[] idcard_array=idCard.Split(' ');
            int Y, S;
            string M, JYM;
            //判断是否润年
            if (yearNum % 4 == 0 || (yearNum % 100 == 0 && yearNum % 4 == 0))
            {
                regex = "^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$";
            }
            else
            {
                regex = "^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$";
            }

            if (Regex.IsMatch(idCard, regex))//判断生日合法性
            {
                //计算校验位
                S = (int.Parse(idCard.Substring(0, 1)) + int.Parse(idCard.Substring(10, 1))) * 7
                    + (int.Parse(idCard.Substring(1, 1)) + int.Parse(idCard.Substring(11, 1))) * 9
                    + (int.Parse(idCard.Substring(2, 1)) + int.Parse(idCard.Substring(12, 1))) * 10
                    + (int.Parse(idCard.Substring(3, 1)) + int.Parse(idCard.Substring(13, 1))) * 5
                    + (int.Parse(idCard.Substring(4, 1)) + int.Parse(idCard.Substring(14, 1))) * 8
                    + (int.Parse(idCard.Substring(5, 1)) + int.Parse(idCard.Substring(15, 1))) * 4
                    + (int.Parse(idCard.Substring(6, 1)) + int.Parse(idCard.Substring(16, 1))) * 2
                    + int.Parse(idCard.Substring(7, 1)) * 1
                    + int.Parse(idCard.Substring(8, 1)) * 6
                    + int.Parse(idCard.Substring(9, 1)) * 3;
                Y = Convert.ToInt32(S % 11);
                M = "F";
                JYM = "10X98765432";
                M = JYM.Substring(Y, 1);//判断校验位

                if (M == idCard.Substring(17, 1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
