using Dapper;
using ScrewMachineManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;
namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_ScrewMachineTaskList
    {
        static string sql_insert = "insert into screwmachinetasklist (id,taskid,dvalue,upd) values(@id,@taskid,@dvalue,@upd)";
        static string sql_update = "update screwmachinetasklist set dvalue=@dvalue,upd=@upd,Daddress=@Daddress where taskid=@taskid and id=@id";

        static string sql_select = "select * from screwmachinetasklist ";

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<screwmachinetasklist> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where " + swhere + " order by taskid,id";
                var output = cnn.Query<screwmachinetasklist>(sql);
                return output.ToList();
            }
        }

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<screwmachinetasklist> LoadListDistinct(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();

                var output = cnn.Query<screwmachinetasklist>(sql);
                return output.ToList();
            }
        }

        public static int Insert(List<screwmachinetasklist> list)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                try
                {
                    cnn.Open();
                    IDbTransaction trans = cnn.BeginTransaction();

                    int t = cnn.Execute(sql_insert, list);
                    trans.Commit();
                    return t;
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        public static int Update(screwmachinetasklist modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update, modelTable);
            }
        }

        public static int UpdateLists(List<screwmachinetasklist> modelTables)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update, modelTables);
            }


        }
    }
}
