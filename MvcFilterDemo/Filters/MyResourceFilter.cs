using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo.Filters
{
    /// <summary>
    /// 资源过滤器，继承Attribute，可以进行特性标注，实现IResourceFilter接口
    /// </summary>
    public class MyResourceFilter : Attribute, IResourceFilter
    {
        /// <summary>
        /// 资源过滤器前置方法，授权过滤器通过就执行
        /// </summary>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // 1. 可以在这做缓存，如果是页面可以做页面缓存
            // 如果是数据，可以做数据缓存，比如从Redis中取，得到之后就返回，后面逻辑不执行
            Console.WriteLine("==== OnResourceExecuting========");
            //throw new Exception("OnResourceExecuting有异常");
        }
        /// <summary>
        /// 资源过滤器后置方法，当所有处理返回时执行，然后返回的中间件管道
        /// </summary>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("==== OnResourceExecuted========");
            //throw new Exception("OnResourceExecuted有异常");
        }  
    }
}
