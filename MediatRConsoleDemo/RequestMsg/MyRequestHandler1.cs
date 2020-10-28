using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRConsoleDemo.RequestMsg
{
    /// <summary>
    /// 实现IRequestHandler代表是请求消息处理类，根据传入的消息类型智能处理对应的消息
    /// MyRequestMsg是请求消息类，int是指返回类型
    /// </summary>
    public class MyRequestHandler1 : IRequestHandler<MyRequestMsg, int>
    {
        public Task<int> Handle(MyRequestMsg request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MyRequestHandler1开始处理消息了，消息类型为：{request.RequestMsgType}");
            return Task.FromResult(1);
        }
    }
}
