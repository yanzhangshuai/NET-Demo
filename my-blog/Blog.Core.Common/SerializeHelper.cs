using System.Text;
using Newtonsoft.Json;

namespace Blog.Core.Common
{
    public class SerializeHelper
    {
        /**
         * 序列化
         */
        public static byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        /**
         * 反序列化
         */
        public static TEntity Deserialize<TEntity>(byte[] value)
        {
            if (value == null)
                return default;
            var jsonString = Encoding.UTF8.GetString(value);
            return JsonConvert.DeserializeObject<TEntity>(jsonString);
        }
        
        public static TEntity Deserialize<TEntity>(string value)
        {
            return value == null ? default : JsonConvert.DeserializeObject<TEntity>(value);
        }
    }
}