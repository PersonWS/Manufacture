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
    internal class CONT_ScrewMachineStepList
    {
        static string sql_insert = "insert into screwmachineSteplist (id,taskid,dvalue,upd) values(@id,@taskid,@dvalue,@upd)";
        
        static string sql_select = "select * from screwmachineSteplist ";

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<screwmachineSteplist> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where " + swhere;
                var output = cnn.Query<screwmachineSteplist>(sql);
                return output.ToList();
            }
        }

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<screwmachineSteplist> LoadListDistinct(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();

                var output = cnn.Query<screwmachineSteplist>(sql);
                return output.ToList();
            }
        }

        public static int Insert(List<screwmachineSteplist> list)
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

        public static int Update(screwmachineSteplist modelTable, string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                string sql_update = "update screwmachineSteplist set dvalue=@dvalue,upd=@upd where id=@id and taskid=@taskid ";
                return cnn.Execute(sql_update , modelTable);
            }
        }
    }
}
