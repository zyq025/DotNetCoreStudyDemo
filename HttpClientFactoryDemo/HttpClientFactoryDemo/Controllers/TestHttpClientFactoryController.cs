using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactoryDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestHttpClientFactoryController : ControllerBase
    {
        // 定义变量 
        private IHttpClientFactory _httpClientFactory;

        // 构造函数注入
        public TestHttpClientFactoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        // 新增接口
        [HttpGet("TestHttpClientFactory")]
        public async Task<IActionResult> TestHttpClientFactory()
        {
            // 通过工厂创建出HttpClient
            var client = _httpClientFactory.CreateClient();
            // 访问远端服务， 这里根据需要可以访问接口服务
            var response = await client.GetAsync("http://47.113.204.41/");
            // 解析内容，这里直接打印内容
            return Content($"状态码：{response.StatusCode.ToString()}，内容：{response.Content.ReadAsStringAsync().Result}");
        }

        [HttpGet("TestNamedHttpClient")]
        public async Task<IActionResult> TestNamedHttpClient()
        {
            // 通过工厂创建出HttpClient
            var client = _httpClientFactory.CreateClient("NamedHttpClient");
            // 访问远端服务， 这里根据需要可以访问接口服务
            var response = await client.GetAsync("http://47.113.204.41/");
            // 解析内容，这里直接打印内容
            return Content($"状态码：{response.StatusCode.ToString()}，内容：{response.Content.ReadAsStringAsync().Result}");
        }
    }
}
