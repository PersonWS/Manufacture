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
    public class CONT_TPTaskInfo
    {
        static string sql_insert = "insert into TPTaskInfo (TaskID,TaskName) values(@TaskID,@TaskName)";
        static string sql_update = "update TPTaskInfo set TaskName = @TaskName where TaskId=@TaskID";
        
        static string sql_select = "select * from TPTaskInfo ";

        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static List<TPTaskInfo> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                
                var output = cnn.Query<TPTaskInfo>(sql_select);
                return output.ToList();
            }
        }

        /// <summary>
        /// 批量insert
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Insert(List<TPTaskInfo> list)
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
        public static int Save(TPTaskInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(TPTaskInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update , modelTable);
            }
        }
    }
}
