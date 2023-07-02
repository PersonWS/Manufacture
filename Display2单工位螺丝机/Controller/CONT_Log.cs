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
    public class CONT_Log
    {
        static string sql_insert = "insert into Log (LogDatetime,LogType,LogTitle,UserID) values(@LogDatetime,@LogType,@LogTitle,@UserID)";
        
        static string sql_select = "select * from Log ";

        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere">带where关键字</param>
        /// <returns></returns>
        public static List<Log> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + swhere;
                var output = cnn.Query<Log>(sql);
                return output.ToList();
            }
        }

        /// <summary>
        /// 批量insert
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Insert(List<Log> list)
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

        /// <summary>
        /// 单条insert
        /// </summary>
        /// <param name="modelTable"></param>
        /// <returns></returns>
        public static int Save(Log modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

       
    }
}
