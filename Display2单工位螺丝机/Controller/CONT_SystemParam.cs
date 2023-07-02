using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_SystemParam
    {
        //  https://blog.csdn.net/cplvfx/article/details/109849118
        //  dapper使用


        static string sql_update = "update SystemParam set Name = @Name,ParamValue=@ParamValue,DataType=@DataType,DecimalDigit=@DecimalDigit,Describe=@Describe where Id=@ID";
        static string sql_insert = "insert into SystemParam(name,paramvalue,datatype,DecimalDigit,Describe) values(@name,@paramvalue,@datatype,@DecimalDigit,@Describe)";
        static string sql_select = "select * from SystemParam ";

        static string connstring = utility.LoadAppConfigString();




        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static List<SystemParam> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(connstring))
            {
                cnn.Open();
                string sql = sql_select;
                var output = cnn.Query<SystemParam>(sql);
                return output.ToList();
            }
        }


        public static int Update(SystemParam modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(connstring))
            {
                return cnn.Execute(sql_update , modelTable);
            }
        }

    }
}
