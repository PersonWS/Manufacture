using Dapper;
using sln_BYSJJ.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static sln_BYSJJ.utility;

namespace sln_BYSJJ.Controller
{
    public class CONT_DSVInterlockingInfo
    {
        static string sql_insert = "insert into IOInfo (TaskID,TaskName) values(@TaskID,@TaskName)";
        static string sql_update = "update DSVInterlockingInfo set workMode=@workMode, DB_Password = @DB_Password,DB_User=@DB_User,DatabaseName=@DatabaseName,ServerName=@ServerName," +
            "LineGroup=@LineGroup,SW_User=@SW_User,stationid=@stationid,Debug=@Debug,ShowWindow=@ShowWindow,PassForNoDB=@PassForNoDB,Function=@Function,UPD=@UPD,oid=@oid,cid=@cid  where ID=@ID ";

        static string sql_select = "select * from  DSVInterlockingInfo";

        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere">带where关键字</param>
        /// <returns></returns>
        public static DSVInterlockingInfo LoadList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                var output = cnn.Query<DSVInterlockingInfo>(sql);
                return output.ToList()[0];
            }
        }

        /// <summary>
        /// 批量insert
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //public static int Insert(List<IOInfo> list)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
        //    {
        //        try
        //        {
        //            cnn.Open();
        //            IDbTransaction trans = cnn.BeginTransaction();

        //            int t = cnn.Execute(sql_insert, list);
        //            trans.Commit();
        //            return t;
        //        }
        //        catch (Exception e)
        //        {

        //            throw;
        //        }
        //    }
        //}

        /// <summary>
        /// 单条insert
        /// </summary>
        /// <param name="modelTable"></param>
        /// <returns></returns>
        public static int Save(DSVInterlockingInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(DSVInterlockingInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update, modelTable);
            }
        }
    }
}
