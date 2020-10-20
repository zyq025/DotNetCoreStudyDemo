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
            Console.WriteLine($"捕获到异常:{context.Exception.Message},可以记录日志");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
