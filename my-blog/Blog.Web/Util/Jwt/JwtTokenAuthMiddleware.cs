using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Web.utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Blog.Web.Util.jwt
{
    /// <summary>
    ///     自定义身份验证
    /// </summary>
    public class JwtTokenAuthMiddleware
    {
        // 中间件一定要有一个next，将管道可以正常的走下去
        private readonly RequestDelegate _next;
        public JwtTokenAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }

            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            try
            {
                if (tokenHeader.Length >= 128)
                {
                    var tm = JwtHelper.SerializeJwt(tokenHeader);
                    var claimList = new List<Claim>();
                    var claim = new Claim(ClaimTypes.Role, tm.Role);
                    claimList.Add(claim);
                    var identity = new ClaimsIdentity(claimList);
                    var principal = new ClaimsPrincipal(identity);
                    httpContext.User = principal;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} middleware wrong:{e.Message}");
            }

            return _next(httpContext);
        }
    }
}