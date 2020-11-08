using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreTestModel;
using EFCoreTestRespository;
using EFCoreTestService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreTestDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUser")]
        public IActionResult AddUser()
        {
            User u = new User
            {
                UserName = "Zoe",
                UserPwd = "123456",
                Id = "10000",
                UserAddr = "China",
                UserBirth = DateTime.Now.AddYears(-20)
            };
            int nRes = _userService.AddUser(u);
            return Ok(nRes);
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser()
        {
            int nRes = _userService.DeleteUser("10000");
            return Ok(nRes);
        }
    }
}
