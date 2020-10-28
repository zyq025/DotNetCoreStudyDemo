using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAspNetCoreDemo.CommandMsg
{
    public class UserAddMsgHandler : IRequestHandler<UserAddMsg, int>
    {
        public Task<int> Handle(UserAddMsg request, CancellationToken cancellationToken)
        {
            //这里可以调用服务层直接添加用户，根据需求添加
            Console.WriteLine($"添加用户成功,用户名：{request.UserName}," +
                $"地址：{request.UserAddr}");
            return Task.FromResult(1);
        }
    }
}
