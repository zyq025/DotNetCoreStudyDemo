using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    public class MyActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // context.RouteData  获取路由相关信息
            Console.WriteLine("OnActionExecuted方法执行");
            throw new Exception("Action方法有异常");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // context.RouteData  获取路由相关信息
            // context.HttpContext 拿到HttpContext 无所不能
            Console.WriteLine("OnActionExecuting方法执行");
        }
    }
}
