using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryCacheConsoleDemo
{
    [Intercept(typeof(MyInterceptor))]
    public interface IUserService
    {
        string GetUser(string id);
        [MyCache]
        string GetUser1(string id);
        
    }
}
