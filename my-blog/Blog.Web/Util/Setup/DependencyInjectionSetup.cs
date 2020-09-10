using Blog.Core.Application;
using Blog.Core.Cache;
using Blog.Core.Cache.redis;
using Blog.Core.IApplication;
using Blog.Core.IApplication.Advertisement;
using Blog.Core.IRepository.Base;
using Blog.Core.Repository.Base.EntityFramework;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Web.utils.setup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection AddDependencyInjectionSetup(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryCache, MemoryCache>();//记得把缓存注入！！！
            services.AddScoped<ICacheManager, RedisCacheManager>();//记得把缓存注入！！！
            services.AddScoped<IDbContextProvider, DbContextProvider>();
            services.AddTransient(typeof(IRepository<>), typeof(EfCoreRepository<>))
                .AddTransient(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
            services.AddScoped<IAdvertisementService, AdvertisementService>();

            return services;
        }
    }
}