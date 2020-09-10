using System.Reflection;
using AutoMapper;
using Blog.Core.Entityframework;
using Blog.Web.Middleware;
using Blog.Web.Util.Consts;
using Blog.Web.Util.Setup;
using Blog.Web.utils;
using Blog.Web.utils.setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.Web
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
            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("Db"));
            });
            services.AddCors(options =>
            {
                options.AddPolicy(CorsConsts.Web,
                    builder => builder.WithOrigins("http://localhost:8081")
                );
            
            });

            services.AddAutoMapper(Assembly.Load("Blog.Core.IApplication"));//这是AutoMapper的2.0新特性
            
            services.AddSwaggerSetup()
                .AddAuthorizationSetup()
                .AddDependencyInjectionSetup()
                .AddAuthenticationSetup(Configuration);
            services.AddControllers();
            
            

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwaggerSetup();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            //    自定义jwt验证
            // app.UseJwtTokenAuth();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseErrorHandler();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}