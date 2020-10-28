using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatRAspNetCoreDemo.CommandMsg
{
    public class UserAddMsg:IRequest<int>
    {
        public string UserName { get; set; }
        public string UserAddr { get; set; }

    }
}
