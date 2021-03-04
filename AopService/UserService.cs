using AopModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AopService
{
    public class UserService : IUserService
    {
        public bool AddUser(User user)
        {
            Console.WriteLine("用户添加成功");
            return true;
        }
    }
}
