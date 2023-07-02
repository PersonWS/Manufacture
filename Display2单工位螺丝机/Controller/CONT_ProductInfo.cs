using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;
namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_ProductInfo
    {


        static string sql_insert = "insert into ProductInfo (checkPrefixCode,PrefixCode,ProductCode,MachineModel,Customer,ProjectPhase,RunTime,MKD,TPTaskID,NumberOfScrews,DefaultWorkStation,SNCodeLenght) values(@checkPrefixCode,@PrefixCode,@ProductCode,@MachineModel,@Customer,@ProjectPhase,@RunTime,@MKD,@TPTaskID,@NumberOfScrews,@DefaultWorkStation,@SNCodeLenght)";
        static string sql_update = "update ProductInfo set checkPrefixCode=@checkPrefixCode, PrefixCode=@PrefixCode,ProductCode=@ProductCode, MachineModel = @MachineModel,Customer = @Customer,ProjectPhase = @ProjectPhase,RunTime = @RunTime,TPTaskID=@TPTaskID,NumberOfScrews=@NumberOfScrews,DefaultWorkStation=@DefaultWorkStation,SNCodeLenght=@SNCodeLenght where Id=@Id";
        static string sql_delete = "delete from ProductInfo where Id=@Id";
       static string sql_select = "select * from ProductInfo ";

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">条件需要where关键字</param>
        /// <returns></returns>
        public static List<ProductInfo> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql_select +   swhere ;
                var output = cnn.Query<ProductInfo>(sql);
                return output.ToList();
            }
        }

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<ProductInfo> LoadListDistinct(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                
                var output = cnn.Query<ProductInfo>(sql);
                return output.ToList();
            }
        }

        public static int Save(ProductInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return  cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(ProductInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
              int dd=  cnn.Execute(sql_update , modelTable);
                return dd;
            }
        }

        public static int Delete(ProductInfo modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete , modelTable);
            }
        }

        
    }
}
