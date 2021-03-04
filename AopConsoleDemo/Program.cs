using AopModel;
using AopService;
using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace AopConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========原始需求=========");
            User user = new User { Name = "Zoe", Age = 18 };
            IUserService userService = new UserService();
            // 模拟增加一个用户
            userService.AddUser(user);

            Console.WriteLine("========动态代理 实现新需求=========");
            //1. 创建代理对象
            IUserService userService1 = DispatchProxy.Create<IUserService, MyProxy>();
            //2. 因为调用的是实例方法，需要传提具体类型
            ((MyProxy)userService1).TargetClass = new UserService();
            userService1.AddUser(user);

            Console.WriteLine("=============Castle.Core方式==============");
            //先实例化一个代理类生成器
            ProxyGenerator generator = new ProxyGenerator();
            //通过代理类生成器创建
            var u = generator.CreateInterfaceProxyWithTarget<IUserService>(new UserService(), new MyIntercept());
            u.AddUser(user);
            Console.ReadLine();

        }
    }
}
