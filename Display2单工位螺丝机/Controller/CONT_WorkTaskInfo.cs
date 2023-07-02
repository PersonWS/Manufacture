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


    public class CONT_WorkTaskInfo
    {


        static string sql_insert = "insert into WorkTaskInfo (ProductCode,Customer,taskDate,taskQty,UserID,CID,MKD,TPID,NumberOfScrews,TaskID) values(@ProductCode,@Customer,@taskDate,@taskQty,@UserID,@CID,@MKD,@TPID,@NumberOfScrews,@TaskID)";
        static string sql_update = "update WorkTaskInfo set Customer=@Customer, ProductCode = @ProductCode,taskDate = @taskDate,taskQty = @taskQty,UserID = @UserID,TPID=@TPID ,NumberOfScrews=@NumberOfScrews , taskID=@TaskID where Id=@Id ";
        static string sql_delete = "delete from WorkTaskInfo where Id=@Id";
        static string sql_select = "select * from WorkTaskInfo ";
        static string sql_select_customer = "select  distinct customer  from WorkTaskInfo where customer is not null and customer is not ''";
        static string sql_upload = "update WorkTaskInfo set uploadstatus=1 where uploadstatus=0 ";

        /// <summary>
        /// 返回customer list
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns>List<string></returns>
        public static List<string> LoadCustomerList()
        {
            List<string> customers=new  List<string>();
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select_customer;
                //创建SqlCommand对象
                SQLiteCommand cmd = new SQLiteCommand(sql, (SQLiteConnection)cnn);
                //执行Sql语句
              SQLiteDataReader  dr = cmd.ExecuteReader();
                //判断SQL语句是否执行成功
                if (dr.Read())
                {
                    customers.Add(dr[0].ToString());
                }
                return customers;
            }
        }

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static List<WorkTaskInfo> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + swhere;
                var output = cnn.Query<WorkTaskInfo>(sql);
                return output.ToList();
            }
        }

        public static List<WorkTaskInfo> getMax(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = "select  * from WorkTaskInfo order by taskid desc limit 1";

                var output = cnn.Query<WorkTaskInfo>(sql);
                return output.ToList();
            }
        }

        public static Model.ResultJsonInfo Save(WorkTaskInfo modelTable)
        {
            Model.ResultJsonInfo jr = new ResultJsonInfo();
            try
            {
                using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
                {
                    string sid = ";select last_insert_rowid()";
                    jr.intValue = cnn.Query<int>(sql_insert + sid, modelTable).FirstOrDefault();
                    jr.Successed = true;
                }
                return jr;
            }
            catch (Exception ex)
            {
                jr.Message = "新生产任务单失败！" + ex.Message;
                return jr;
            }

        }

        public static int Update(WorkTaskInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update, modelTable);
            }
        }

        public static int Delete(WorkTaskInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete, modelTable);
            }
        }


    }
}
