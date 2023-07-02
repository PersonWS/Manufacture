using Dapper;
using sln_TJSJJ.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using static sln_TJSJJ.utility;

namespace sln_TJSJJ.Controller
{
    class CONT_UUT_RESULT
    {
        static string connectionString = $"server={utility.dSV.ServerName};database={utility.dSV.DatabaseName};uid={utility.dSV.DB_User};pwd={utility.dSV.DB_Password}";


        public static int InsertMesData(UUT_RESULT uUT_RESULT)
        {
            string sql = "insert into UUT_RESULT(ID,STATION_ID, BATCH_SERIAL_NUMBER, TEST_SOCKET_INDEX, UUT_SERIAL_NUMBER,USER_LOGIN_NAME,START_DATE_TIME,EXECUTION_TIME,UUT_STATUS,UUT_ERROR_CODE,UUT_ERROR_MESSAGE) values (@ID,@STATION_ID, @BATCH_SERIAL_NUMBER, @TEST_SOCKET_INDEX, @UUT_SERIAL_NUMBER,@USER_LOGIN_NAME,@START_DATE_TIME,@EXECUTION_TIME,@UUT_STATUS,@UUT_ERROR_CODE,@UUT_ERROR_MESSAGE)";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Execute(sql, uUT_RESULT, null, null, null);
            }
        }








    }
}
