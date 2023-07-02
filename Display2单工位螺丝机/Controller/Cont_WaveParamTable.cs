using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_WaveParamTable
    {
        static string sql_update = "update SystemParam set Name = @Name,ParamValue=@ParamValue,DataType=@DataType,DecimalDigit=@DecimalDigit,Describe=@Describe where Id=@ID";
        static string sql_insert = "insert into SystemParam(name,paramvalue,datatype,DecimalDigit,Describe) values(@name,@paramvalue,@datatype,@DecimalDigit,@Describe)";
        static string sql_select = "select * from WaveParamTable ";

        static string connstring = utility.LoadAppConfigString();




        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static List<Model.WaveParamTable> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(connstring))
            {
                cnn.Open();
                string sql = sql_select;
                var output = cnn.Query<WaveParamTable>(sql);
                return output.ToList();
            }
        }
    }
}
