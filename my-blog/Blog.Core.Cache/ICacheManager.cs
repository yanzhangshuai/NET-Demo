using System;

namespace Blog.Core.Cache
{
    /**简单缓存接口,只有查询和添加*/
    public interface ICacheManager
    {
        /// <summary>
        /// 获取 Reids 缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// 获取值，并序列化
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);

        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime"></param>
        void Set(string key, object value, TimeSpan? cacheTime = null);

        /// <summary>
        ///  判断是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Get(string key);

        /// <summary>
        /// 移除某一个缓存值
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        ///  全部清除
        /// </summary>
        void Clear();
    }
}