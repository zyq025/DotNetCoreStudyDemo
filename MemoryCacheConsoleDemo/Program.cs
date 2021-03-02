using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace MemoryCacheConsoleDemo
{
    public class Program
    {
        public static ContainerBuilder containerBuilder = new ContainerBuilder();
        static void Main(string[] args)
        {
            // 注册用户服务
            containerBuilder.RegisterType<UserService>().As<IUserService>()
                .AsImplementedInterfaces().PropertiesAutowired()
                .EnableInterfaceInterceptors();// 开启拦截器功能
            // 注册拦截器服务
            containerBuilder.RegisterType(typeof(MyInterceptor));

            // 注册MemoryCache服务
            containerBuilder.Register(c=>new MemoryCache(new MemoryCacheOptions())).As<IMemoryCache>()
                .AsImplementedInterfaces()
                .SingleInstance();// 单例模式注册

            var container = containerBuilder.Build();


            // 从容器中获取实例
            IUserService userService = container.Resolve<IUserService>();

            Console.WriteLine(userService.GetUser("coderZ"));

            Console.WriteLine(userService.GetUser("coderZ"));
            Console.WriteLine("=========================");
            Console.WriteLine(userService.GetUser1("coderZYQ"));
            Console.WriteLine(userService.GetUser1("coderZYQ"));

            

        }
    }
}
