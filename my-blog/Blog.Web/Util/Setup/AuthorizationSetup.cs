using Microsoft.Extensions.DependencyInjection;

namespace Blog.Web.utils
{
    public static  class AuthorizationSetup
    {
        public static IServiceCollection AddAuthorizationSetup(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConst.Client, policy => policy.RequireRole("Client").Build());
                options.AddPolicy(PolicyConst.Admin, policy => policy.RequireRole("Admin").Build());
                options.AddPolicy(PolicyConst.SystemAndAdmin, policy => policy.RequireRole("Admin").RequireRole("System").Build());
                options.AddPolicy(PolicyConst.SystemOrAdmin, policy => policy.RequireRole("Admin", "System").Build());
            });
            return services;
        }
    }
    
    
}