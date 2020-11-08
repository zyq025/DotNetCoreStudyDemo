using EFCoreTestModel;
using EFCoreTestRespository;
using System;

namespace EFCoreTestService
{
    public class UserService: IUserService
    {
        private readonly IUserRespository _user;

        public UserService(IUserRespository user)
        {
            _user = user;
        }

        public int AddUser(User user)
        {
            return _user.AddUser(user);
        }

        public int DeleteUser(string userId)
        {
            return _user.DeleteUser(userId);
        }

        public User GetUser()
        {
            throw new NotImplementedException();
        }

        public int UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
