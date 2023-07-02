using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;

namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_IOInfo2
    {
        static string sql_select = "select case when flag=1 then '输出' else '输入' end IOstatuses, * from IOInfo2 ";

        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere">带where关键字</param>
        /// <returns></returns>
        public static List<IOInfo2> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + swhere;
                var output = cnn.Query<IOInfo2>(sql);
                return output.ToList();
            }
        }
    }
}
