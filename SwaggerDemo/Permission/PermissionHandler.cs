using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDemo.Permission
{
    /// <summary>
    /// 权限处理的关键类
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 通过IHttpContextAccessor可以获取HttpContext相关信息，但一定要注册服务
        /// </summary>
        private readonly IHttpContextAccessor _accessor;
        /// <summary>
        /// 用于判断请求是否带有凭据和是否登录
        /// </summary>
        public IAuthenticationSchemeProvider Scheme { get; set; }
        /// <summary>
        ///  构造函数注入
        /// </summary>
        public PermissionHandler(IHttpContextAccessor accessor,IAuthenticationSchemeProvider scheme)
        {
            this._accessor = accessor;
            Scheme = scheme;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            try
            {
                //拿到HttpContext就无所不能啦
                var httpContext = _accessor.HttpContext;
                //判断资源数据中权限列表中是否有权限
                if (!requirement.Permissions.Any())
                {
                    //没有直接返回无权限，也可以重新获取权限，实现不退出重新登录就可获取最新权限
                    context.Fail();
                }
                //判读请求是否拥有凭据，即是否登录
                var defaultAuthenticate = await Scheme.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate == null)
                {
                    context.Fail();
                }
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                //不为空代表登录成功,为空登录失败
                if (result?.Principal != null)
                {
                    // 获取生成Token时放在payload里的userId
                    string userId = _accessor.HttpContext.User.FindFirst("userId").Value;
                    // 获取当前请求的地址
                    string requestUrl = httpContext.Request.Path.Value.ToLower();
                    // 从权限表中查找当前用户是否有当前请求地址的权限
                    var permission = requirement.Permissions.FirstOrDefault(a => a.Url.ToLower() == requestUrl && a.UserId == userId);
                    // 如果没找到，代表没有权限
                    if (permission == null)
                    {
                        context.Fail();
                    }
                    // 如果找到，就继续往下执行
                    context.Succeed(requirement);
                }
                else
                {
                    // 获取不到对应值就返回无权限
                    context.Fail();
                }
            }
            catch (Exception ex)
            {
                context.Fail();
            }
        }
    }
}
