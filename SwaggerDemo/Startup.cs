using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SwaggerDemo.Permission;
using Swashbuckle.AspNetCore.Filters;

namespace SwaggerDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // 注册Swagger相关组件
            services.AddSwaggerGen(option =>
            {
                // 配置Swagger 文档信息，第一个参数很重要"TestSwagger"，得和后续注册中间件一直
                option.SwaggerDoc("TestSwagger", new OpenApiInfo
                {
                    // 文档版本
                    Version = "TestSwagger-V1",
                    // 标题
                    Title = "TestSwagger 接口文档",
                    // 描述
                    Description = "这是TestSwagger文档，版本为V1",
                    // 联系方式，地址和邮箱配置
                    Contact = new OpenApiContact { Name = "TestSwagger-C", Email = "Test@xxx.com", Url = new Uri("https://www.cnblogs.com/zoe-zyq/") },
                    License = new OpenApiLicense { Name = "TestSwagger-L", Url = new Uri("https://www.cnblogs.com/zoe-zyq/") }
                });
                // 指定Action排序方式，这里是按Action相对路径进行排序
                option.OrderActionsBy(o => o.RelativePath);

                //设置注释提示，主要是指定xml文件路径，从xml中读取相关的信息
                var basePath = AppContext.BaseDirectory;
                // 配置API读取注释
                var apiXml = Path.Combine(basePath, "SwaggerDemo.xml");
                option.IncludeXmlComments(apiXml, true);

                // 配置Model读取注释
                var modelXml = Path.Combine(basePath, "TestSwaggerModel.xml");
                option.IncludeXmlComments(modelXml);

                #region Swagger扩展-增加输入Token即添加小锁功能，清楚看见接口是否安全
                // 方式1
                option.OperationFilter<AddResponseHeadersFilter>();
                option.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // 将Token放在请求头中传递到后台
                option.OperationFilter<SecurityRequirementsOperationFilter>();

                // 指定名称必须为oauth2，因为SecurityRequirementsOperationFilter默认securitySchemaName指定为oauth2
                option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Jwt认证授权,在输入框中输入'Bearer token'(Bearer和token之间有一个空格)",
                    Name = "Authorization",//设置参数名
                    In = ParameterLocation.Header,//在请求头中添加名称Authorization
                    Type = SecuritySchemeType.ApiKey,
                });

                //方式2 
                //option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "Jwt认证授权",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    BearerFormat = "JWT",
                //    Scheme = "bearer"
                //});
                //option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                //    { 
                //      new OpenApiSecurityScheme{ 
                //        Reference = new OpenApiReference
                //        {
                //            Type = ReferenceType.SecurityScheme,
                //            Id="Bearer"
                //        }
                //      },
                //      new List<string>()
                //    }
                //}); 
                #endregion
            });

            #region 集成JWT
            // 将公共的信息提取出来，这里可以放到配置文件中，统一读取;以下直接在程序中写死了
            // 秘钥，这是生成Token需要秘钥，就是理论提及到签名那块的秘钥
            string secret = "TestSecretTestSecretTestSecretTestSecret";
            // 签发者，是由谁颁发的
            string issuer = "TestIssuer";
            // 接受者，是给谁用的
            string audience = "TestAudience";
            // 注册服务，显示指定为Bearer
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    // 配置Jwt信息
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // 是否验证秘钥
                        ValidateIssuerSigningKey = true,
                        // 指定秘钥
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),

                        // 是否验证颁发者
                        ValidateIssuer = true,
                        // 指定颁发者
                        ValidIssuer = issuer,

                        // 是否验证接受者
                        ValidateAudience = true,
                        // 指定接受者
                        ValidAudience = audience,

                        // 设置必须要有超时时间
                        RequireExpirationTime = true,
                        // 设置必须验证超时
                        ValidateLifetime = true,

                        // 将其赋值为0时，即设置有效时间到期，就马上失效
                        ClockSkew = TimeSpan.Zero
                    };
                });
            #endregion


            var permissionRequirement = new PermissionRequirement
            {
                Permissions = new List<PermissionData>()
            };


            //针对授权定义策略
            services.AddAuthorization(option =>
            {
                ////AminPolicy策略就是要求Admin角色才能访问接口
                //option.AddPolicy("AdminPolicy", p => p.RequireRole("Admin").Build());
                ////AdminOrMaintainPolicy策略只要有Admin或Maintain其中一个角色即可访问接口
                //option.AddPolicy("AdminOrMaintainPolicy", p => p.RequireRole("Admin", "Maintain").Build());
                ////AdminOrUserPolicy策略只要有Admin或User其中一个角色即可访问接口
                //option.AddPolicy("AdminOrUserPolicy", p => p.RequireRole("Admin", "User").Build());

                ////AdminAndMaintainPolicy策略必须要同时有Admin和Maintain两个个角色才能访问接口
                //option.AddPolicy("AdminAndMaintainPolicy", p => p.RequireRole("Admin").RequireRole("User").Build());

                // 同样是策略模式，只是基于Requirement进行权限验证，验证逻辑自己写
                option.AddPolicy("Permission", p => p.Requirements.Add(permissionRequirement));
            });
            // 将权限验证的关键类注册，Jwt的策略模式指定为Requirements时就会自动执行该类方法
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            // 将permissionRequirement实例注册为单例模式，保证系统中就一个实例，方便权限数据共享
            services.AddSingleton(permissionRequirement);
            // 注册IHttpContextAccessor，后续可以通过它可以获取HttpContext，操作方便
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 注册Swagger中间件
            app.UseSwagger();
            // 注册Swagger界面中间件
            app.UseSwaggerUI(option =>
            {
                // 配置文档数据来源及名称设置，其中第一个参数中TestSwagger和注册组件时的名称一致
                option.SwaggerEndpoint($"/swagger/TestSwagger/swagger.json", "TestSwaggerCode");
                // 设置为空，代表根路径就可访问到Swagger的主页
                option.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
