
using System;
using System.Linq;
using Blog.Core.Cache;
using Castle.DynamicProxy;

namespace Blog.Web.utils.aop
{
    public class BlogCacheAop:IInterceptor
    {
        private readonly ICacheManager _cacheManagerManager;
        public BlogCacheAop(ICacheManager cacheManager)
        {
            _cacheManagerManager = cacheManager;
        }
        public void Intercept(IInvocation invocation)
        {
            //获取自定义缓存键
            var cacheKey = CustomCacheKey(invocation);
            //根据key获取相应的缓存值
            var cacheValue = _cacheManagerManager.Get(cacheKey);
            if (cacheValue != null)
            {
                //将当前获取到的缓存值，赋值给当前执行方法
                invocation.ReturnValue = cacheValue;
                return;
            }
            //去执行当前的方法
            invocation.Proceed();
            //存入缓存
            if (!string.IsNullOrWhiteSpace(cacheKey))
            {
                _cacheManagerManager.Set(cacheKey, invocation.ReturnValue);
            }
        }

        private string CustomCacheKey(IInvocation invocation)
        {
            var typeName = invocation.TargetType.Name;
            var methodName = invocation.Method.Name;
            var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();//获取参数列表，我最多需要三个即可
            var  key = $"{typeName}:{methodName}:";
            key = methodArguments.Aggregate(key, (current, param) => current + $"{param}:");

            return key.TrimEnd(':');
        }

        private string GetArgumentValue(object arg)
        {
            switch (arg)
            {
                case int:
                case long:
                case string:
                    return arg.ToString();
                case DateTime time:
                    return time.ToString("yyyyMMddHHmmss");
                default:
                    return "";
            }
        }
    }
}