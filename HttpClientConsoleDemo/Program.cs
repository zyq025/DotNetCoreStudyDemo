using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientConsoleDemo
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // 使用.NetCore的依赖注入
            var serviceCollection = new ServiceCollection();
            // 注入HttpClient相关服务
            serviceCollection.AddHttpClient();
            // 构建一个容器
            var serviceProvider = serviceCollection.BuildServiceProvider();
            IHttpClientFactory httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

            //下面是使用
            Console.WriteLine("开始访问自己的网站!");
            for (int i = 0; i < 15; i++)
            {
                // 通过HttpClientFactory创建出一个HttpClient
                var client = httpClientFactory.CreateClient();
                // 访问地址
                var response = await client.GetAsync("http://47.113.204.41/");
                Console.WriteLine($"请求返回状态码：{response.StatusCode}");
            }
            Console.WriteLine("访问完成!");
        }

        private static HttpClient Client = new HttpClient();
        private static async Task NewMethod1()
        {
            Console.WriteLine("开始访问自己的网站!");
            // 循环访问多次
            for (int i = 0; i < 15; i++)
            {
                // 这里访问自己云服务器站点，没有做负载，所以方便看测试结果
                var result = await Client.GetAsync("http://47.113.204.41/");
                Console.WriteLine($"请求返回状态码：{result.StatusCode}");
            }
            Console.WriteLine("访问完成!");
        }

        static async Task NewMethod()
        {
            Console.WriteLine("开始访问自己的网站!");
            // 循环访问多次
            for (int i = 0; i < 15; i++)
            {
                // using包裹使用HttpClient
                using (HttpClient httpClient = new HttpClient())
                {
                    // 这里访问自己云服务器站点，没有做负载，所以方便看测试结果
                    var result = await httpClient.GetAsync("http://47.113.204.41/");
                    Console.WriteLine($"请求返回状态码：{result.StatusCode}");
                }
            }
            Console.WriteLine("访问完成!");
        }
    }
}
