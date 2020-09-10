using System;
using System.IO;
using System.Linq;
using Blog.Web.Util.swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Blog.Web.Util.Setup
{
    public static class SwaggerSetup
    {
        public static string ApiName { get; set; } = "Blog.Core";

        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                
                //    单版本
                // options.SwaggerDoc("V1", new OpenApiInfo
                // {
                //     Version = "V1",
                //     Title = $"{ApiName} HTTP API V1",
                //     Contact = new OpenApiContact
                //     {
                //         Name = ApiName, Email = "Blog.Core@xxx.com",
                //         Url = new Uri("https://www.jianshu.com/u/94102b59cc2a")
                //     },
                //     License = new OpenApiLicense
                //         {Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a")}
                // });
                
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version =version,
                        Title = $"{ApiName} HTTP API"+version,
                        Contact = new OpenApiContact
                        {
                            Name = ApiName, Email = "Blog.Core@xxx.com",
                            Url = new Uri("https://www.jianshu.com/u/94102b59cc2a")
                        },
                        License = new OpenApiLicense
                            {Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a")}
                    });
                });
              
                options.OrderActionsBy(o=>o.RelativePath);

                #region 添加swagger注释
                //    添加swagger注释
                var xmlPath = Path.Combine(basePath, "Blog.Web.xml");
                options.IncludeXmlComments(xmlPath,true);
                xmlPath = Path.Combine(basePath, "Blog.Core.Model.xml");
                options.IncludeXmlComments(xmlPath,true);

                #endregion

                #region 验证
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
               
                options.AddSecurityDefinition("oauth2",new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",
                    In=ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion
               
                    
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();
            
            
            app.UseSwaggerUI(options =>
            {
                //    单版本
                // options.SwaggerEndpoint($"/swagger/V1/swagger.json", $"{ApiName} V1");
                // //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                // options.RoutePrefix = "";
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e=>e).ToList().ForEach(version =>
                {
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                options.RoutePrefix = "";
            });
            return app;
        }
    }
}