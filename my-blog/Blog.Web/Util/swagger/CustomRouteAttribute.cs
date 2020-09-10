using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Blog.Web.Util.swagger
{
    /// <summary>
    ///     自定义路由 /api/{version}/[controller]/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomRouteAttribute:RouteAttribute,IApiDescriptionGroupNameProvider
    {
        /// <summary>
        ///      自定义路由构造函数，继承基类路由
        /// </summary>
        /// <param name="actionName"></param>
        public CustomRouteAttribute(string actionName  = "[action]") : 
            base("/api/{version}/[controller]/" + actionName)
        {
        }

        public CustomRouteAttribute(ApiVersions version, string actionName = "[action]") : base(
            $"/api/{version.ToString()}/[controller]/{actionName}")
        {
            GroupName = version.ToString();
        }
        

        /// <summary>
        ///     分组名称,是来实现接口 IApiDescriptionGroupNameProvider
        /// </summary>
        public string GroupName { get; }
    }
}