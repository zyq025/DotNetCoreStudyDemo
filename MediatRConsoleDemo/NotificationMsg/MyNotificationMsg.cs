using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediatRConsoleDemo.NotificationMsg
{
    /// <summary>
    /// 抽象通知消息，实现INotification
    /// </summary>
    public class MyNotificationMsg: INotification
    {
        // 任意定义消息属性
        public string MsgType { get; set; }
        public string MsgContent { get; set; }

    }
}
