using System;

namespace EFCoreTestModel
{
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 用户地址
        /// </summary>
        public string UserAddr { get; set; }

        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime UserBirth { get; set; }
    }
}
