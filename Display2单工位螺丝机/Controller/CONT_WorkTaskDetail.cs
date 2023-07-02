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


    public class CONT_WorkTaskDetail
    {


        static string sql_insert = "insert into WorkTaskDetail (datetimeid,taskID,productCode,productSN,productSN_M,xh,CylinderNumber,TorisionValue,Result,FloatingHigh,ScrewLoose,OtherErr,mkd,workStationId,upd,oid) values(@datetimeid,@taskID,@productCode,@productSN,@productSN_M,@xh,@CylinderNumber,@TorisionValue,@Result,@FloatingHigh,@ScrewLoose,@OtherErr,@mkd,@workStationId,@upd,@oid)";
        static string sql_upload = "update WorkTaskDetail set uploadstatus=1 ,upd=@upd,oid=@oid  where uploadstatus=0 ";
        static string sql_delete = "update WorkTaskDetail set deleteflag=1,upd=@upd,oid=@oid where taskID=@taskID and productsn=@productsn and mkd=@mkd";
        static string sql_delete2 = "update WorkTaskDetail set deleteflag=1,upd=@upd,oid=@oid where taskID=@taskID ";
        static string sql_upload_datetimeid = "update WorkTaskDetail set Result=@Result,OtherErr=@OtherErr ,upd=@upd,oid=@oid  where taskID=@taskID and datetimeid=@datetimeid ";
        static string sql_select = "select * from WorkTaskDetail ";
        static string sql_select_count = "select count(taskid) as taskid from WorkTaskDetail ";
        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">条件带列名</param>
        /// <returns></returns>
        public static List<WorkTaskDetail> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where " + swhere ;
                var output = cnn.Query<WorkTaskDetail>(sql);
                return output.ToList();
            }
        }
        /// <summary>
        /// 取汇总数
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static List<WorkTaskDetail> LoadListRecordCount(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select_count;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where " + swhere ;
                var output = cnn.Query<WorkTaskDetail>(sql);
                return output.ToList();
            }
        }

        public static int Save(WorkTaskDetail modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(WorkTaskDetail modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_upload , modelTable);
            }
        }

        public static int Update_DatetimeID(WorkTaskDetail modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_upload_datetimeid, modelTable);
            }
        }
        
        /// <summary>
        /// 批量insert
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int Updates(List<WorkTaskDetail> list)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                try
                {
                    cnn.Open();
                    IDbTransaction trans = cnn.BeginTransaction();

                    int t = cnn.Execute(sql_delete, list);
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
        /// update 删除标记
        /// </summary>
        /// <param name="modelTable"></param>
        /// <returns></returns>
        public static int Delete(WorkTaskDetail modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete , modelTable);
            }
        }
       //按taskid 更新删除标记，重置数量用
        public static int Delete2(WorkTaskDetail modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete2, modelTable);
            }
        }
    }
}
