using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRConsoleDemo.NotificationMsg
{
    /// <summary>
    /// 通知消息处理类，实现INotificationHandler接口，泛型参数只处理的消息类
    /// </summary>
    public class MyNotificationHandler : INotificationHandler<MyNotificationMsg>
    {
        public Task Handle(MyNotificationMsg notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyNotificationHandler处理消息，" +
                $"消息类型为:{notification.MsgType}，消息内容为{notification.MsgContent}");
            return Task.CompletedTask;
        }
    }
}
