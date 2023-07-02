using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrewMachineManagementSystem.Model;

namespace ScrewMachineManagementSystem.Controller
{
    public class UserTable
    {

        SqLiteHelper sqlite = new SqLiteHelper();
        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public  List<Model.UserTable> LoadUser(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(sqlite.LoadConnectString()))
            {
                cnn.Open();
                string sql = "select * from UserTable";
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where userid='" + swhere + "'";
                var output = cnn.Query<Model.UserTable>(sql);
                return output.ToList();
            }
        }
    }
}
