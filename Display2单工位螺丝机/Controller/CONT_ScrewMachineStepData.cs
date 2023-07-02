using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;

namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_ScrewMachineStepData
    {
        
            
        static string sql_select = "select * from ScrewMachineStepData";
        //static string sql_update = "update ScrewMachineStepData set task00=@task00,task01=@task01,task02=@task02,task03=@task03,task04=@task04,task05=@task05," +
        //    "task06=@task06,task07=@task07,task08=@task08,task09=@task09,task10=@task10,task11=@task11,task12=@task12,task13=@task13,task14=@task14,task15=@task15 " +
        //    " where modeid=@modeid and itemid=@itemid1 and stepid=@stepid";

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<ScrewMachineStepData> LoadList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                
                var output = cnn.Query<ScrewMachineStepData>(sql_select);
                return output.ToList();
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="modelTable"></param>
        ///// <param name="modeid">工作模式id</param>
        ///// <param name="itemid">step内顺序</param>
        ///// <param name="stepid">stepid</param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static int Update(ScrewMachineStepData modelTable, int modeid,int itemid,int stepid,ushort value)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectString()))
        //    {
        //        cnn.Open();
        //        string sql = "update ScrewMachineStepData set task" + stepid.ToString().PadLeft(2,'0')+"=" + value + " where modeid=1 and itemid=1 and stepid=0";
        //        return cnn.Execute(sql , modelTable);
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
        //public static int BatchUpdate(ScrewMachineStepData modelTable)
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(LoadConnectString()))
        //    {
        //        cnn.Open();

        //        return cnn.Execute(sql_update, modelTable);
        //    }
        //}


    }
}
