using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDemo.Permission
{
    /// <summary>
    /// 资源数据的载体，可以根据需求自定义任何数据
    /// 需要实现IAuthorizationRequirement接口进行标识
    /// </summary>
    public class PermissionRequirement:IAuthorizationRequirement
    {
        /// <summary>
        /// 存放系统中所有权限
        /// </summary>
        public List<PermissionData> Permissions { get; set; }

    }

    /// <summary>
    /// 定义了一个权限数据存储类，包含用户ID和对应的权限访问的Url地址
    /// </summary>
    public class PermissionData
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户对应的 Url地址
        /// </summary>
        public string Url { get; set; }
    }
}
