using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    /// <summary>
    /// 继承Attribute可以进行特性标注，实现IActionFilter
    /// </summary>
    public class MyActionFilter1 : Attribute, IActionFilter, IOrderedFilter
    {
        public int Order { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // context.RouteData  获取路由相关信息
            Console.WriteLine("OnActionExecuted方法执行====controller");
            //throw new Exception("OnActionExecuted方法有异常");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // context.RouteData  获取路由相关信息
            // context.HttpContext 拿到HttpContext 无所不能
            if(!context.ModelState.IsValid) //如果是模型绑定，可以这样判断参数是否合法
            {
                Console.WriteLine("OnActionExecuting参数不合法");
            }
            Console.WriteLine("OnActionExecuting方法执行====controller");
            //throw new Exception("OnActionExecuting异常了");
        }
    }
}
