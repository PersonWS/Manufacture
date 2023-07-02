using Dapper;
using ScrewMachineManagementSystem.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static ScrewMachineManagementSystem.utility;
namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_ModbusObject
    {
        static string sql_insert = "insert into ModbusObject (name,baud,comport,isuse,decaddress,Remark,MachineModel) values(@name,@baud,@comport,@isuse,@decaddress,@Remark,@MachineModel)";
        static string sql_update = "update ModbusObject set name=@name, baud = @baud,comport = @comport,isuse = @isuse,decaddress = @decaddress,Remark=@Remark,MachineModel=@MachineModel where orderid=@orderid ";
        static string sql_delete = "delete from ModbusObject where orderid=@orderid";
        static string sql_select = "select * from ModbusObject ";

        /// <summary>
        /// 返回list
        /// </summary>
        /// <returns></returns>
        public static List<ModbusObject> LoadList()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                var output = cnn.Query<ModbusObject>(sql_select + " order by orderid");
                return output.ToList();
            }
        }

        /// <summary>
        /// 返回list
        /// </summary>
        /// <param name="swhere">where带列名</param>
        /// <returns></returns>
        public static List<ModbusObject> LoadListDistinct(string sql)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();

                var output = cnn.Query<ModbusObject>(sql);
                return output.ToList();
            }
        }

        public static int Save(ModbusObject modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(ModbusObject modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update , modelTable);
            }
        }

        public static int Delete(ModbusObject modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete ,  modelTable);
            }
        }
    }
}
