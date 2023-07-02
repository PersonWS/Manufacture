using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;

namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_CylinderNumberRatios
    {
        static string sql_select = "select * from CylinderNumberRatios";
        //static string sql_update = "update ScrewMachineTaskData set task00=@task00,task01=@task01,task02=@task02,task03=@task03,task04=@task04,task05=@task05," +
        //    "task06=@task06,task07=@task07,task08=@task08,task09=@task09,task10=@task10,task11=@task11,task12=@task12,task13=@task13,task14=@task14,task15=@task15 " +
        //    " where modeid=@modeid and itemid=@itemid1";

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<Model.CylinderNumberRatios> LoadList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                var output = cnn.Query<Model.CylinderNumberRatios>(sql_select);
                return output.ToList();
            }
        }
    }
}
