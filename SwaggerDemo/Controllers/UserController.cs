using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwaggerDemo.Permission;
using TestSwaggerModel;

namespace SwaggerDemo.Controllers
{
    /// <summary>
    /// 用户API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        //通过构造函数注入 PermissionRequirement，注册的时候用的是单例模式
        private PermissionRequirement _permissionRequirement;
        public UserController(PermissionRequirement permissionRequirement)
        {
            _permissionRequirement = permissionRequirement;
        }

        /// <summary>
        /// 登录Api，传入用户名和密码
        /// </summary>
        [HttpPost("Login")]
        public ActionResult Login(UserConditon userConditon)
        {
            // 返回用户信息，模拟从数据库中查询用户得到用户信息
            if(userConditon==null||userConditon.UserName!="Code综艺圈"||userConditon.Pwd!="Zoe")
            {
                return Ok("登录失败~~~");
            }
            // 这里可以根据需要将其权限放在Redis中，每次登陆时都重新存，即登陆获取最新权限
            // 这里模拟通过userId从数据库中获取分配的接口权限信息，这里存在内存中
            _permissionRequirement.Permissions = new List<PermissionData> { 
                new PermissionData{ UserId="Zoe1111",Url="/api/Product/AdminConfigProductData" },
                //new PermissionData{ UserId="Zoe1111",Url="/api/Product/MaintainProductInfo" },
                new PermissionData{ UserId="Zoe1111",Url="/api/Product/UserProductInfo" }
            };
            // 生成Token并返回
            string token = GenerateToke("Zoe1111", "Code综艺圈");
            return Ok(token);
        }
        private string GenerateToke(string userId, string userName)
        {
            // 秘钥，这是生成Token需要秘钥，就是理论提及到签名那块的秘钥
            string secret = "TestSecretTestSecretTestSecretTestSecret";
            // 签发者，是由谁颁发的
            string issuer = "TestIssuer";
            // 接受者，是给谁用的
            string audience = "TestAudience";
            // 指定秘钥
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            // 签名凭据，指定对应的签名算法，结合理论那块看哦~~~
            var sigingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // 自定义payload信息，每一个claim代表一个属性键值对，就类似身份证上的姓名，出生年月一样
            var claims = new Claim[] { new Claim("userId", userId), 
                new Claim("userName", userName),
                // claims中添加角色属性，这里的键一定要用微软封装好的ClaimTypes.Role
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Role,"Maintain")
            };
           
            // 组装生成Token的数据
            SecurityToken securityToken = new JwtSecurityToken(
                    issuer: issuer,// 颁发者
                    audience: audience,// 接受者
                    claims: claims,//自定义的payload信息
                    signingCredentials: sigingCredentials,// 凭据
                    expires: DateTime.Now.AddMinutes(30) // 设置超时时间，30分钟之后过期
                );
            // 生成Token
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        [HttpPost("GetUserInfo")]
        [Authorize]
        public ActionResult GetUserInfo()
        {
            return Ok("获取用户信息成功");
        }
        /// <summary>
        /// 测试接口
        /// </summary>
        [HttpPost("TestNoAuth")]
        public ActionResult TestNoAuth()
        {
            return Content("不需要认证就能访问");
        }

        

    }
}
