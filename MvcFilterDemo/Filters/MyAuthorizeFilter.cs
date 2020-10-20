using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    /// <summary>
    /// 自定义授权过滤器，继承Atrribute表示可以进行特性标注，实现IAuthorizationFilter接口
    /// </summary>
    public class MyAuthorizeFilter : Attribute, IAuthorizationFilter 
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 1. 可以验证是否登录， 比如使用Session，或是Jwt方式

            // 2. 获取请求的路径,与权限进行比对校验
            var strPath = context.HttpContext.Request.Path;

            // 改变控制台字体颜色
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"MyAuthorizeFilter执行:{strPath}");

            // 恢复原有字体颜色
            Console.ForegroundColor = ConsoleColor.Gray;

            // validate 结果是验证结果
            bool validate = false;          

            // 结果如果验证失败，就返回没权限
            if(validate)
            {
                // 任意IActionResult，根据需求定义
                ContentResult contentResult = new ContentResult();
                // 返回的状态码
                contentResult.StatusCode = 401;
                // 返回的内容
                contentResult.Content = "没权限";

                // 直接指定结果返回
                context.Result = contentResult;
            }
        }
    }
}
