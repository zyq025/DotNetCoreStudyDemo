using EFCoreTestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreTestService
{
    public interface IUserService
    {
        int AddUser(User user);
        int DeleteUser(string userId);
        int UpdateUser(User user);
        User GetUser();
    }
}
