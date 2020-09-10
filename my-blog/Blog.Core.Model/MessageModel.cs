using System.Collections.Generic;

namespace Blog.Core.Model
{
    /// <summary>
    ///     通用信息类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessageModel<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<T> Data { get; set; }
    }
}