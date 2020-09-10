using System;
using Blog.Core.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Core.Cache.memory
{
    public class MemoryCacheManager:ICacheManager
    {
        private readonly IMemoryCache _cache;
        //还是通过构造函数的方法，获取
        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GetValue(string key)
        {
            return _cache.TryGetValue(key, out var val) ? val.ToString() : "1";
        }

        public TEntity Get<TEntity>(string key)
        {
            var data = GetValue(key);
            return SerializeHelper.Deserialize<TEntity>(data);
        }

        public void Set(string key, object value, TimeSpan? cacheTime = null)
        {
            if (cacheTime is null)
                _cache.Set(key, value);
            else
                _cache.Set(key, value, cacheTime.Value);
        }


        public  bool Get(string key)
        {
            return _cache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Clear()
        {
           // _cache.Dispose();
        }

    }
}