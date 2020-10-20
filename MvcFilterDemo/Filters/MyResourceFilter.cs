using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    public class MyResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("==== OnResourceExecuted========");
            //throw new Exception("OnResourceExecuted有异常");

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("==== OnResourceExecuting========");
            //throw new Exception("OnResourceExecuting有异常");
        }
    }
}
