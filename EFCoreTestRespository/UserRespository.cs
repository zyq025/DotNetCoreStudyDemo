using EFCoreTestModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace EFCoreTestRespository
{
    public class UserRespository : IUserRespository
    {
        private readonly DbContext _dbContext;

        public UserRespository(MyTestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int AddUser(User user)
        {
            //新增单个用户，这里主要是绑定关系及更改追踪状态，即更改为添加状态
            var tempUser = _dbContext.Set<User>().Add(user);

            //新增多个用户，用AddRange，参数传list或可变参数都行
            //_dbContext.Set<User>().AddRange(users);

            // 真正提交保存都是SaveChanges,上面只是改了状态而已
            return _dbContext.SaveChanges();
        }

        public int DeleteUser(string userId)
        {
            _dbContext.Set<User>().Remove(new User() { Id = userId });
            //_dbContext.Set<User>().RemoveRange(users);
            return _dbContext.SaveChanges();
        }

        public User GetUser()
        {
            // 方式1
            //return _dbContext.Set<User>().Where(a => a.Id == "10000").FirstOrDefault();
            // 方式2 Linq , from、where、select是一个linq最基本的关键字，必须要
            var res = from u in _dbContext.Set<User>()
                      where u.Id == "10000"
                      select u;

            return res.FirstOrDefault();
        }

        public int UpdateUser(User user)
        {
            _dbContext.Set<User>().Update(user);
            //_dbContext.Set<User>().UpdateRange(users);
            return _dbContext.SaveChanges();
        }
    }
}
