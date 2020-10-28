using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAspNetCoreDemo.EventMsg
{
    public class UserAddSuccessEMailHandler : INotificationHandler<UserAddSuccessMsg>
    {
        public Task Handle(UserAddSuccessMsg notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"发送邮件：用户添加成功,用户名是{notification.UserName}，" +
                $"密码是{notification.UserPwd}");
            return Task.CompletedTask;
        }
    }
}
