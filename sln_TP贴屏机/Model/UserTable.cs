using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sln_TP.Model
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserTable
    {
        /// <summary>
        /// Id int
        /// </summary>
        public int Id { get; set; }
        public string userID { get; set; }

        /// <summary>
        /// 设备地址
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public string permissiones { get; set; }
        /// <summary>
        /// 用户类型，join用
        /// </summary>
        public UserType UserTypes { get; set; }
    }

    public class UserType
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public string TypeID { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
