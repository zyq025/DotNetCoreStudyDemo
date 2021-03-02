using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryCacheConsoleDemo
{
    public class UserService : IUserService
    {
        private  IMemoryCache _memoryCache;

        public UserService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public string GetUser(string id)
        {
            var v = _memoryCache.Get(id);
            if(v==null)
            {
                // 直接调用方法设置即可，其他方法也一样
                _memoryCache.Set(id, id + ":Zoe");
                return id + ":Zoe";
            }
            else
            {
                return "Cache:" + v.ToString();
            }
        }
        
        public string GetUser1(string id)
        {
            return id+":Zoe1";
        }
    }
}