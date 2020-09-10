using Blog.Web.Util.error;
using Blog.Web.Util.jwt;
using Microsoft.AspNetCore.Builder;

namespace Blog.Web.Middleware
{
    // 这里定义一个中间件Helper，主要作用就是给当前模块的中间件取一个别名
    public static class MiddlewareHelpers
    {
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuthMiddleware>();
        }
        
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}