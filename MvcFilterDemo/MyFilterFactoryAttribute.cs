using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcFilterDemo
{
    /// <summary>
    /// 继承Attribute就可以进行特性标注
    /// </summary>
    public class MyFilterFactoryAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable =>true;

        /// <summary>
        /// 过滤器类型
        /// </summary>
        private Type filterType;

        /// <summary>
        /// 构造函数初始化过滤器类型
        /// </summary>
        public MyFilterFactoryAttribute(Type type)
        {
            filterType = type;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            // 从容器中获取过滤器类型，所以前提过滤器服务需要提前注册
            return (IFilterMetadata)serviceProvider.GetService(filterType);
        }
    }
}
