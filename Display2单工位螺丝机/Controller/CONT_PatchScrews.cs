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
    public class CONT_PatchScrews
    {
        //  https://blog.csdn.net/cplvfx/article/details/109849118
        //  dapper使用

        static string sql_insert = "insert into PatchScrews (sn,lineid,oid,stationid,taskid,mkd,flag,upd) " +
            "                                   values(@sn,@lineid,@oid,@stationid,@taskid,@mkd,@flag,@upd)";
        static string sql_update = "update PatchScrews set oid=@oid,stationid=@stationid,flag=@flag,upd=@upd where sn=@sn";
        static string sql_delete = "delete from PatchScrews where ID=@Id";
        static string sql_select = "select * from PatchScrews ";

        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static List<PatchScrews> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where " + swhere;
                var output = cnn.Query<PatchScrews>(sql);
                return output.ToList();
            }
        }

        public static int Save(PatchScrews modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(PatchScrews modelTable, string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update, modelTable);
            }
        }

        public static int Delete(PatchScrews modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete, modelTable);
            }
        }
    }
}
