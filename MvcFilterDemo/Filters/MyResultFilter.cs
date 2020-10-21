using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    /// <summary>
    /// 继承Attribute可以进行特性标注，实现IResultFilter
    /// </summary>
    public class MyResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            
            // context.RouteData  获取路由相关信息
            // context.HttpContext 拿到HttpContext 无所不能
            Console.WriteLine("OnResultExecuted方法执行");
            throw new Exception("sd");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            // context.RouteData  获取路由相关信息
            // context.HttpContext 拿到HttpContext 无所不能
            Console.WriteLine("OnResultExecuting方法执行");
            throw new Exception("sd1");

        }
    }
}
