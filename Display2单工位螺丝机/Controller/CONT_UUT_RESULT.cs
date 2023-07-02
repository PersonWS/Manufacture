using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace ScrewMachineManagementSystem.Controller
{
    public class CONT_UUT_RESULT
    {
        static string connectionString = string.Format("server={0};database={1};uid={2};pwd={3}", utility.dSV.ServerName, utility.dSV.DatabaseName, utility.dSV.DB_User, utility.dSV.DB_Password);
        public static int InsertMesData(Model.UUT_RESULT uUT_RESULT)
        {            //IDbConnection由于Dapper ORM的操作实际上是对IDbConnection类的扩展，所有的方法都是该类的扩展方法。

            string insertSql = "insert into UUT_RESULT(ID,STATION_ID, BATCH_SERIAL_NUMBER, TEST_SOCKET_INDEX, UUT_SERIAL_NUMBER,USER_LOGIN_NAME,START_DATE_TIME,EXECUTION_TIME,UUT_STATUS,UUT_ERROR_CODE,UUT_ERROR_MESSAGE) " +
                    "values (@ID,@STATION_ID, @BATCH_SERIAL_NUMBER, @TEST_SOCKET_INDEX, @UUT_SERIAL_NUMBER,@USER_LOGIN_NAME,@START_DATE_TIME,@EXECUTION_TIME,@UUT_STATUS,@UUT_ERROR_CODE,@UUT_ERROR_MESSAGE)";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Execute(insertSql, uUT_RESULT);
            }
        }

    }
}
