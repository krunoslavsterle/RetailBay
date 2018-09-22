using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RetailBay.Infrastructure.EntityFramework
{
    public static class QueryExtensions
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string propertyName) where TEntity: class
        {
            return source.OrderBy(ToLambda<TEntity>(propertyName));
        }

        public static IOrderedQueryable<TEntity> OrderByDescending<TEntity>(this IQueryable<TEntity> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<TEntity>(propertyName));
        }

        private static Expression<Func<TEntity, object>> ToLambda<TEntity>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(TEntity));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<TEntity, object>>(propAsObject, parameter);
        }
    }
}
