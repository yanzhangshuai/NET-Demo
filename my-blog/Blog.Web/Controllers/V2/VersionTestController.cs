using Blog.Web.Util.swagger;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers.V2
{
    public class VersionTestController : Controller
    {
        // GET
        [HttpGet]
        [CustomRoute(ApiVersions.V2,"Get")]
        public string Get()
        {
            return "我是版本2";
        }
    }
}