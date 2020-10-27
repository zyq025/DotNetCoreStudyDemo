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
    /// </summary>
    public class MyRequestHandler : IRequestHandler<MyRequestMsg, int>
    {
        public Task<int> Handle(MyRequestMsg request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"开始处理消息了，消息类型为：{request.RequestMsgType}");
            return Task.FromResult(1);
        }
    }
}
