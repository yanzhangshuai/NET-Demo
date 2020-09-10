using System;
using Blog.Core.Common;
using StackExchange.Redis;

namespace Blog.Core.Cache.redis
{
    public class RedisCacheManager:ICacheManager
    {
        private readonly string _redisConnenctionString;
        private volatile ConnectionMultiplexer _redisConnection;
        private readonly object _redisConnectionLock = new object();
        public RedisCacheManager()
        {
            var redisConfiguration = Appsettings.App( "RedisCaching", "ConnectionString");//获取连接字符串

            if (string.IsNullOrWhiteSpace(redisConfiguration))
            {
                throw new ArgumentException("redis config is empty", nameof(redisConfiguration));
            }
            _redisConnenctionString = redisConfiguration;
            _redisConnection = GetRedisConnection();
        }

        private ConnectionMultiplexer GetRedisConnection()
        {
            //如果已经连接实例，直接返回
            if (_redisConnection != null && _redisConnection.IsConnected)
                return _redisConnection;

            lock (_redisConnectionLock)
            {
                //释放redis连接
                _redisConnection?.Dispose();
                try
                {
                    _redisConnection = ConnectionMultiplexer.Connect(_redisConnenctionString);
                }
                catch (Exception)
                {
                    throw new Exception("Redis服务未启用，请开启该服务");
                }
            }
            return _redisConnection;
        }

        public string GetValue(string key)
        {
            return _redisConnection.GetDatabase().StringGet(key);
        }

        public TEntity Get<TEntity>(string key)
        {
            var value = GetValue(key);
            return value is null ? default : SerializeHelper.Deserialize<TEntity>(value);
        }

        public void Set(string key, object value, TimeSpan? cacheTime = null)
        {
            if (value != null)
            {
                //序列化，将object值生成RedisValue
                _redisConnection.GetDatabase().StringSet(key, SerializeHelper.Serialize(value), cacheTime);
            }
        }

        public bool SetValue(string key, byte[] value)
        {
            return _redisConnection.GetDatabase().StringSet(key, value, TimeSpan.FromSeconds(120));
        }
        
        public bool Get(string key)
        {
            return _redisConnection.GetDatabase().KeyExists(key);
        }

        public void Remove(string key)
        {
            _redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Clear()
        {
            foreach (var endPoint in GetRedisConnection().GetEndPoints())
            {
                var server = GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    _redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }
    }
}