using System;
using System.Linq.Expressions;
using System.Reflection;
using Blog.Core.Model.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Blog.Core.Entityframework
{
    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType type)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension).GetMethod(nameof(GetSoftDeleteFilter),
                    BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(type.ClrType);
            var filter = methodToCall?.Invoke(null, new object[] { });
            type.SetQueryFilter((LambdaExpression)filter);
        }
        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity:class,ISoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}