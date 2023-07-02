
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
        #region ����ַ��в��ܺ��������ַ�

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
                            strResult = "�����ַ��к��зǷ��ַ�:" + strFiltersChar[i].ToString();
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

        #region ȥ���ļ�ֻ������
        /// <summary>
        /// ȥ���ļ�ֻ������
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

        #region  ����ַ��Ƿ�������˵����

        public static bool IsUsableFormNumber(string str)
        {
            return Regex.IsMatch(str, "[^A-Za-z0-9]");
        }
        #endregion

        #region  ����ַ��Ƿ���õ�URL

        public static bool IsUsableURL(string str)
        {
            //Regex r = new Regex(@"^(?<proto>\w+)://[^/]+?(?<port>:\d+)?/",RegexOptions.Compiled);
            //return r.IsMatch(str);
            return Regex.IsMatch(str, "http://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");
            //return Regex.IsMatch(str, "^http://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?");

        }
        #endregion

        #region  �Ƿ�����ȷ�� ��ǰ����

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

        #region IsInt �Ƿ�������

        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, "^([0-9]*)$");
        }

        #endregion

        #region IsIp �Ƿ�IP��ַ
        /// <summary>
        /// �ж��Ƿ�IP��ַ
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIp(string str)
        {
            IPAddress IP;
            return IPAddress.TryParse(str, out IP); 
        }
        #endregion
     
        #region IsTelNumber �Ƿ�绰����

        public static bool IsTelNumber(string telNumber)
        {
            return Regex.IsMatch(telNumber, "^([0-9()-]*)$");
        }

        #endregion

        #region IsUsable �Ƿ�Ϊ���õ��ַ���

        public static bool IsUsable(string str)
        {
            return Regex.IsMatch(str, "^([0-9a-zA-Z_]*)$");
        }

        #endregion

        #region IsIdCard  ���֤�����Ƿ���Ϲ淶

        /// <summary>
        /// �ж���������Ƿ�����ȷ���֤��
        /// </summary>
        /// <param name="o">��������ֵ</param>
        /// <returns></returns>
        public static bool IsIdCard(string idCard)
        {
            Hashtable hsAreaCode = new Hashtable();
            hsAreaCode.Add("11", "����");
            hsAreaCode.Add("12", "���");
            hsAreaCode.Add("13", "�ӱ�");
            hsAreaCode.Add("14", "ɽ��");
            hsAreaCode.Add("15", "���ɹ�");
            hsAreaCode.Add("21", "����");
            hsAreaCode.Add("22", "����");
            hsAreaCode.Add("23", "������");
            hsAreaCode.Add("31", "�Ϻ�");
            hsAreaCode.Add("32", "����");
            hsAreaCode.Add("33", "�㽭");
            hsAreaCode.Add("34", "����");
            hsAreaCode.Add("35", "����");
            hsAreaCode.Add("36", "����");
            hsAreaCode.Add("37", "ɽ��");
            hsAreaCode.Add("41", "����");
            hsAreaCode.Add("42", "����");
            hsAreaCode.Add("43", "����");
            hsAreaCode.Add("44", "�㶫");
            hsAreaCode.Add("45", "����");
            hsAreaCode.Add("46", "����");
            hsAreaCode.Add("50", "����");
            hsAreaCode.Add("51", "�Ĵ�");
            hsAreaCode.Add("52", "����");
            hsAreaCode.Add("53", "����");
            hsAreaCode.Add("54", "����");
            hsAreaCode.Add("61", "����");
            hsAreaCode.Add("62", "����");
            hsAreaCode.Add("63", "�ຣ");
            hsAreaCode.Add("64", "����");
            hsAreaCode.Add("65", "�½�");
            hsAreaCode.Add("71", "̨��");
            hsAreaCode.Add("81", "���");
            hsAreaCode.Add("82", "����");
            hsAreaCode.Add("91", "����");

            if (idCard != null && idCard.Length > 2)
            {
                //��������
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
        /// �ж�15λ���֤
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>
        private static bool Chk15IdCard(string idCard)
        {
            int yearNum = int.Parse(idCard.Substring(6, 2)) + 1900;
            string regex = "";
            //�ж��Ƿ�����
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
        /// �ж�18λ���֤
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
            //�ж��Ƿ�����
            if (yearNum % 4 == 0 || (yearNum % 100 == 0 && yearNum % 4 == 0))
            {
                regex = "^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$";
            }
            else
            {
                regex = "^[1-9][0-9]{5}19[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$";
            }

            if (Regex.IsMatch(idCard, regex))//�ж����պϷ���
            {
                //����У��λ
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
                M = JYM.Substring(Y, 1);//�ж�У��λ

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
