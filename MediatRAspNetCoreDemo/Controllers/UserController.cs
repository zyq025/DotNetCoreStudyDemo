using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatRAspNetCoreDemo.CommandMsg;
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

        [HttpGet]
        public async Task<ActionResult> AddUser(string userName,string userAddr)
        {
            UserAddMsg userAddMsg = new UserAddMsg {
                UserName=userName,
                UserAddr=userAddr
            };
            int nResponse = await _mediator.Send(userAddMsg);
            return Ok($"新增成功!{nResponse}");
        }
    }
}
