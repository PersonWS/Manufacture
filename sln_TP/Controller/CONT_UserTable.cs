using Dapper;
using sln_TP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using static sln_TP.utility;

namespace sln_TP.Controller
{
    public class CONT_UserTable
    {

        //  dapper使用

        static string sql_insert = "insert into UserTable (userID,userName,password,typeid,remark) values(@userID,@userName,@password,@typeid,@remark)";
        static string sql_update = "update UserTable set UserID=@UserID,userName = @userName,password = @password,remark = @remark ,typeid=@typeid where Id=@Id";
        static string sql_delete = "delete from UserTable where ID=@Id";
        static string sql_select = "select * from UserTable ";
        static string sql_select_join = "select u.id, u.UserID,u.UserName,u.TypeID ,u.password,u.permissiones,t.typeName from UserTable as u join usertype as t on u.typeid=t.typeid ";

        static string sql_selecttype = "select * from UserType";

        /// <summary>
        /// Join查询-列表
        /// usertable Join userType,用户类型查询
        /// </summary>
        public static List<UserTable> UserJoinTypeSelect(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                string sql = sql_select_join;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where " + swhere;

                var productsList = cnn.Query<UserTable, UserType, UserTable>
                        (sql, (usertable, usertype) =>
                        {
                            usertable.UserTypes = usertype;//关联表
                            return usertable;
                        }, splitOn: "typeid"//建立关系的字段
                    ).ToList();

                //Console.WriteLine("//ProductJoinTypeSelect查询//");
                //foreach (var item in productsList)
                //{
                //    Console.WriteLine($"ID:{item.Id}\nName:{item.userName}\nTypeName:{item.UserTypes.TypeName}");
                //}
                return productsList.ToList();
            }

        }

        /// <summary>
        /// 返回UserType
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static Dictionary<string, string> DictionaryUserType()
        {
            Dictionary<string, string> myDic = new Dictionary<string, string>();

            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();

                var output = cnn.Query<UserType>(sql_selecttype);
                foreach (var item in output)
                {
                    myDic.Add(item.TypeID, item.TypeName);
                }
                return myDic;
            }
        }

        /// <summary>
        /// 返回userinfo
        /// </summary>
        /// <param name="swhere"></param>
        /// <returns></returns>
        public static List<UserTable> LoadList(string swhere)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                cnn.Open();
                string sql = sql_select;
                if (!string.IsNullOrEmpty(swhere))
                    sql = sql + " where " + swhere;
                var output = cnn.Query<UserTable>(sql);
                return output.ToList();
            }
        }

        public static int Save(UserTable modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_insert, modelTable);
            }
        }

        public static int Update(UserTable modelTable, string id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_update, modelTable);
            }
        }

        public static int Delete(UserTable modelTable)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadAppConfigString()))
            {
                return cnn.Execute(sql_delete, modelTable);
            }
        }
    }
}
