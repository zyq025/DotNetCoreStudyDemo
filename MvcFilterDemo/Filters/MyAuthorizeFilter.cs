using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    public class MyAuthorizeFilter : Attribute, IAuthorizationFilter 
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // 1. 可以验证是否登录， 比如使用Session，或是Jwt方式

            // 2. 获取请求的路径,与权限进行比对校验
            var strPath = context.HttpContext.Request.Path;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"MyAuthorizeFilter执行:{strPath}");

            Console.ForegroundColor = ConsoleColor.Gray;

            bool validate = false;
            if(validate)
            {
                ContentResult contentResult = new ContentResult();
                contentResult.StatusCode = 401;
                contentResult.Content = "没权限";
                context.Result = contentResult;
            }
         
        }
    }
}
