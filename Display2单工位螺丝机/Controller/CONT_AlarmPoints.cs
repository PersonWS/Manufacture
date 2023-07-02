using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;


namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_AlarmPoints
    {
        static string sql_insert = "insert into AlarmPoints (alarminfo,db,address,bits,processmode,diaplayinfo,MKD,oid) values(@alarminfo,@db,@address,@bits,@processmode,@diaplayinfo,@MKD,@oid)";
        static string sql_update = "update AlarmPoints set alarminfo=@alarminfo,db=@db,address=@address,bits=@bits,processmode=@processmode,diaplayinfo=@diaplayinfo,MKD=@MKD,oid=@oid where Id=@Id";
        static string sql_delete = "delete from AlarmPoints where Id=@Id";
        static string sql_select = "select * from AlarmPoints ";
        

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">条件需要where关键字</param>
        /// <returns></returns>
        public static List<AlarmPoints> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql_select + swhere;
                var output = cnn.Query<AlarmPoints>(sql);
                return output.ToList();
            }
        }

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<AlarmPoints> LoadListDistinct(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();

                var output = cnn.Query<AlarmPoints>(sql);
                return output.ToList();
            }
        }

        public static int Save(AlarmPoints modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(AlarmPoints modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                int dd = cnn.Execute(sql_update, modelTable);
                return dd;
            }
        }

        public static int Delete(AlarmPoints modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete, modelTable);
            }
        }
    }
}
