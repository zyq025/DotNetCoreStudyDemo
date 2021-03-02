using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MemoryCacheWebApiDemo
{
    public class ResourceFilter : Attribute, IResourceFilter
    {
        private IMemoryCache _memoryCache;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        public ResourceFilter(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // 执行前
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            // 根据请求生成Key
            string key = GenerateKey(context);
            // 从缓存中获取数据
            var value = _memoryCache.Get(key);
            // 如果取到数据，直接返回，不走下面流程，提高效率
            if(value!=null)
            {
                context.Result = value as IActionResult;
            }
        }

        // 执行完毕
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // 根据请求生成Key
            string key = GenerateKey(context);
            // 将执行结果进行缓存
            _memoryCache.Set(key, context.Result);
        }
        /// <summary>
        /// 根据请求生成Key
        /// </summary>
       private string GenerateKey(FilterContext context)
        {
            // 这里可以取得自定义的Attribute，可以进行判断是否进行缓存，这里就不写啦
            // 逻辑和MemoryCacheConsoleDemo逻辑一致
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var attr = action.MethodInfo.GetCustomAttribute<MyCacheAttribute>();

            // 根据请求生成Key 
            string url = context.HttpContext.Request.Path; //这里可以把参数带上
            string param = context.HttpContext.Request.QueryString.Value; // Url参数
            return url + param;
        }
    }
}
