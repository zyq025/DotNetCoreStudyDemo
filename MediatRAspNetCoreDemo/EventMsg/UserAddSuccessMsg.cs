using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRAspNetCoreDemo.EventMsg
{
    public class UserAddSuccessMsg:INotification
    {
        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }
}
