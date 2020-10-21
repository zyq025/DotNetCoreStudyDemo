using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    public class MyExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"捕获到异常:{context.Exception.Message+context.Exception.StackTrace},可以记录日志");
            Console.ForegroundColor = ConsoleColor.Gray;
            // 代表异常已经处理 
            context.ExceptionHandled = true;

            // 可以根据需求定义返回格式，ContentResult、JsonResult 等
            ContentResult contentResult = new ContentResult() {
                 StatusCode=200,
                 Content="联系管理员,异常编号10000"
            };
            // 设置返回结果
            context.Result = contentResult;
        }
    }
}
