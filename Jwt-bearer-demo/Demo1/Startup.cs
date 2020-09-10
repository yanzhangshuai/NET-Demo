using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Demo1
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
            var Issurer = "JWTBearer.Auth";  //发行人
            var Audience = "api.auth";       //受众人
            var secretCredentials = "q2xiARx$4x3TKqBJ";   //密钥

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(builder =>
            {
                builder.TokenValidationParameters = new TokenValidationParameters
                {
                    //    是否验证发行人
                    ValidateIssuer = true,
                    ValidIssuer = Issurer,

                    //    是否验证受众人
                    ValidateAudience = true,
                    ValidAudience = Audience,
                    //是否验证密钥
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretCredentials)),
                    ValidateLifetime = true, //验证生命周期
                    RequireExpirationTime = true //过期时间
                };
            });

            // services.AddAuthorization(options =>
            // {
            //     options.AddPolicy("customizePermisson", policy => policy.Requirements.Add(new PermissionRequirement("admin")));
            // });

            services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("BaseRole", builder => builder.RequireRole("admin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //    开启认证
            app.UseAuthentication();

            //    开启授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}