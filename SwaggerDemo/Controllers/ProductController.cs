using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {

        /// <summary>
        /// 管理员配置产品相关信息
        /// </summary>
        [HttpPost("AdminConfigProductData")]
        //[Authorize(Roles ="Admin")]
        //[Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "Permission")]
        public ActionResult AdminConfigProductData()
        {
            return Ok("管理员配置产品信息");
        }

        /// <summary>
        /// 维护员提交产品维护记录信息
        /// </summary>
        [HttpPost("MaintainProductInfo")]
        //[Authorize(Roles = "Maintain")]
        //[Authorize(Policy = "AdminAndMaintainPolicy")]
        [Authorize(Policy = "Permission")]
        public ActionResult MaintainProductInfo()
        {
            return Ok("维护员提交产品维护记录信息");
        }

        /// <summary>
        /// 用户访问产品信息
        /// </summary>
        [HttpPost("UserProductInfo")]
        //[Authorize(Roles = "User")]
        //[Authorize(Policy = "AdminAndMaintainPolicy")]
        [Authorize(Policy = "Permission")]
        public ActionResult UserProductInfo()
        {
            return Ok("用户访问产品信息");
        }
    }
}
