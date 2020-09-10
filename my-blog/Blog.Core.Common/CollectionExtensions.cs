using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.Core.Common
{
    public static class AbpCollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> source) => source == null || source.Count <= 0;

        public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
        {
            if (source.Contains(item))
                return false;
            source.Add(item);
            return true;
        }

        public static IEnumerable<T> AddIfNotContains<T>(
            this ICollection<T> source,
            IEnumerable<T> items)
        {
            var objList = new List<T>();
            foreach (var obj in items)
            {
                if (source.Contains(obj)) continue;
                source.Add(obj);
                objList.Add(obj);
            }
            return objList;
        }

        public static bool AddIfNotContains<T>(
            this ICollection<T> source,
            Func<T, bool> predicate,
            Func<T> itemFactory)
        {
            if (source.Any(predicate))
                return false;
            source.Add(itemFactory());
            return true;
        }

        public static IList<T> RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            var list = source.Where(predicate).ToList();
            foreach (T obj in list)
                source.Remove(obj);
            return list;
        }
    }
}