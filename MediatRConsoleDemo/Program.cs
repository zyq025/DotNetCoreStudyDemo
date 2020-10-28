using MediatR;
using MediatRConsoleDemo.NotificationMsg;
using MediatRConsoleDemo.RequestMsg;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MediatRConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 使用自带的依赖注入
            var services = new ServiceCollection();
            // 将MediatR相关组件进行注册，这里指明注册的程序集
            services.AddMediatR(typeof(Program).Assembly);

            // 取得容器
            var serviceProvider = services.BuildServiceProvider();

            // 从容器中获取中介者
            var mediator = serviceProvider.GetService<IMediator>();

            // 通过容器发送请求消息，然后IRequestHandler根据消息类型进行处理
            int nResponse = mediator.Send(new MyRequestMsg { RequestMsgType = "Request1" }).Result;

            Console.WriteLine($"Request消息处理完成!!!返回响应为{nResponse}");
            
            Console.WriteLine($"====================以下是通知消息测试==========================");

            mediator.Publish(new MyNotificationMsg { MsgType = "Notification", MsgContent = "TestNotify" }).ConfigureAwait(false);
            Console.WriteLine($"通知消息处理完成!!!");
        }
    }
}
