using System;
using Blog.Web.Util.swagger;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.V1
{
    public class VersionTestController : Controller
    {
        
        // GET
        [HttpGet]
        [CustomRoute(ApiVersions.V1,"Get")]
        public string Get()
        {
            throw new Exception("我是错误");
            return "我是版本1";
        }
    }
}