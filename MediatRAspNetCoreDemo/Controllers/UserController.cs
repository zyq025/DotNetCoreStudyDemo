using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatRAspNetCoreDemo.CommandMsg;
using MediatRAspNetCoreDemo.EventMsg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediatRAspNetCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("AddUser")]
        public async Task<ActionResult> AddUser(string userName,string userAddr)
        {
            // 利用请求消息解耦
            UserAddMsg userAddMsg = new UserAddMsg {
                UserName=userName,
                UserAddr=userAddr
            };
            int nResponse = await _mediator.Send(userAddMsg);
            //新增用户成功时发送消息提醒，比如有微信、短信、邮件
            if(nResponse==1)
            {
                // 利用通知消息解耦
                await _mediator.Publish(new UserAddSuccessMsg { 
                     UserName=userName,
                     UserPwd="123456"
                });
            }
            return Ok($"新增成功!{nResponse}");
        }
    }
}
