using AopModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AopService
{
    public interface IUserService
    {
        bool AddUser(User user);
    }
}
