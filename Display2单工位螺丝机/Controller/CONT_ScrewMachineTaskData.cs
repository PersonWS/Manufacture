using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;

namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_ScrewMachineTaskData
    {
        static string sql_select = "select * from ScrewMachineTaskData";
        static string sql_update = "update Screwmachinetaskdata set scopes=@scopes  where Id=@Id ";
        //static string sql_update = "update ScrewMachineTaskData set task00=@task00,task01=@task01,task02=@task02,task03=@task03,task04=@task04,task05=@task05," +
        //    "task06=@task06,task07=@task07,task08=@task08,task09=@task09,task10=@task10,task11=@task11,task12=@task12,task13=@task13,task14=@task14,task15=@task15 " +
        //    " where modeid=@modeid and itemid=@itemid1";

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<Screwmachinetaskdata> LoadList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                var output = cnn.Query<Screwmachinetaskdata>(sql_select);
                return output.ToList();
            }
        }



        public static int Update(Screwmachinetaskdata modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                return cnn.Execute(sql_update, modelTable);
            }
        }

        ///// <summary>
        ///// 更新某一列
        ///// </summary>
        ///// <param name="modelTable"></param>
        ///// <param name="columnname"></param>
        ///// <returns></returns>
        //public static int UpdateColumn(Screwmachinetaskdata modelTable, string columnname)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectString()))
        //    {
        //        cnn.Open();

        //        string sql = "update Screwmachinetaskdata set " + columnname + "=@" + columnname + " where modeid=@modeid and itemid=@itemid ";
        //        return cnn.Execute(sql, modelTable);
        //    }
        //}

        ///// <summary>
        ///// 批量更新
        ///// </summary>
        ///// <param name="modelTable"></param>
        ///// <param name="modeid"></param>
        ///// <param name="itemid"></param>
        ///// <param name="stepid"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static int BatchUpdate(Screwmachinetaskdata modelTable)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectString()))
        //    {
        //        cnn.Open();

        //        return cnn.Execute(sql_update, modelTable);
        //    }
        //}
    }
}
