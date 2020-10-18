using System;

namespace TestSwaggerModel
{/// <summary>
/// 返回的用户信息
/// </summary>
    public class UserModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr { get; set; }
    }
}
