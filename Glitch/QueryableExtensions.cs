using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ExceptNull<T>(this IQueryable<T> source) => source.Where(s => s != null);

        public static IQueryable<T> ExceptWhere<T>(this IQueryable<T> source, Expression<Func<T, bool>> filter)
        {
            Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(Expression.Not(filter.Body), filter.Parameters);

            return source.Where(predicate);
        }

        public static IOrderedQueryable<T> OrderBySelf<T>(this IQueryable<T> source) => source.OrderBy(s => s);

        public static IOrderedQueryable<T> OrderBySelfDescending<T>(this IQueryable<T> source) => source.OrderByDescending(s => s);

        public static IOrderedQueryable<T> ThenBySelf<T>(this IOrderedQueryable<T> source) => source.ThenBy(s => s);

        public static IOrderedQueryable<T> ThenBySelfDescending<T>(this IOrderedQueryable<T> source) => source.ThenByDescending(s => s);

        public static IQueryable<T> Shuffle<T>(this IQueryable<T> source) => source.OrderBy(s => Guid.NewGuid());
    }
}
