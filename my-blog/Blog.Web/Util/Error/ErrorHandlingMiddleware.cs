using System;
using System.Net;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Web.Util.error
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
 
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var loggerFactory = context.RequestServices.GetService<ILoggerFactory>();
            if (loggerFactory != null)
            {
                var logger = loggerFactory.Create("HandleException");
                logger.Error(exception.Message,exception);
            }
 
            var code = HttpStatusCode.InternalServerError;
            if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var result = exception.Message;
                context.Response.ContentType = "application/json;charset=utf-8";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);
            }
 
            context.Response.ContentType = "text/html;charset=utf-8";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync("服务器错误，错误信息：" + exception.Message);
        }
    }
}