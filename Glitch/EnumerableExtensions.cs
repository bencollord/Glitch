using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params T[] append)
            => source.Concat(append);

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, params T[] prepend)
            => prepend.Concat(source);

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] exclude) => source.Except(exclude.AsEnumerable());

        public static IEnumerable<T> ExceptWhere<T>(this IEnumerable<T> source, Func<T, bool> filter) => source.Where(s => !filter(s));

        public static IOrderedEnumerable<T> OrderBySelf<T>(this IEnumerable<T> source) => source.OrderBy(s => s);

        public static IOrderedEnumerable<T> OrderBySelfDescending<T>(this IEnumerable<T> source) => source.OrderByDescending(s => s);

        public static IOrderedEnumerable<T> ThenBySelf<T>(this IOrderedEnumerable<T> source) => source.ThenBy(s => s);

        public static IOrderedEnumerable<T> ThenBySelfDescending<T>(this IOrderedEnumerable<T> source) => source.ThenByDescending(s => s);

        public static T Random<T>(this IEnumerable<T> source) => source.Shuffle().First();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) => source.OrderBy(s => Guid.NewGuid());

        public static IEnumerable<T> ExceptNull<T>(this IEnumerable<T> source)
        {
            Guard.NotNull(source, nameof(source));
            return source.Where(s => s != null);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Guard.NotNull(source, nameof(source));
            Guard.NotNull(action, nameof(action));

            foreach (T item in source)
            {
                action.Invoke(item);
            }
        }

        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
            => LeftJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        public static IEnumerable<TResult> LeftJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Guard.NotNull(outer, nameof(outer)); 
            Guard.NotNull(inner, nameof(inner)); 
            Guard.NotNull(outerKeySelector, nameof(outerKeySelector)); 
            Guard.NotNull(innerKeySelector, nameof(innerKeySelector)); 
            Guard.NotNull(resultSelector, nameof(resultSelector)); 
            Guard.NotNull(comparer, nameof(comparer));

            return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, 
                                   (o, i) => new { Outer = o, Inners = i }, comparer)
                        .SelectMany(oi => oi.Inners.DefaultIfEmpty(), (o, i) => resultSelector(o.Outer, i));
        }

        public static IEnumerable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> left, IEnumerable<TInner> right, Func<TOuter, TKey> leftKeySelector, Func<TInner, TKey> rightKeySelector, Func<TOuter, TInner, TResult> resultSelector)
            => RightJoin(left, right, leftKeySelector, rightKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        public static IEnumerable<TResult> RightJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> left, IEnumerable<TInner> right, Func<TOuter, TKey> leftKeySelector, Func<TInner, TKey> rightKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Guard.NotNull(left, nameof(left));
            Guard.NotNull(right, nameof(right));
            Guard.NotNull(leftKeySelector, nameof(leftKeySelector));
            Guard.NotNull(rightKeySelector, nameof(rightKeySelector));
            Guard.NotNull(resultSelector, nameof(resultSelector));
            Guard.NotNull(comparer, nameof(comparer));

            return right.GroupJoin(left, rightKeySelector, leftKeySelector,
                                   (r, l) => new { Right = r, Lefts = l }, comparer)
                        .SelectMany(rl => rl.Lefts.DefaultIfEmpty(), (r, l) => resultSelector(l, r.Right));
        }

        public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
            => FullOuterJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            Guard.NotNull(outer, nameof(outer));
            Guard.NotNull(inner, nameof(inner));
            Guard.NotNull(outerKeySelector, nameof(outerKeySelector));
            Guard.NotNull(innerKeySelector, nameof(innerKeySelector));
            Guard.NotNull(resultSelector, nameof(resultSelector));
            Guard.NotNull(comparer, nameof(comparer));

            var left = outer.LeftJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
            var right = outer.RightJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer);

            return left.Union(right);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source) => ToHashSet(source, EqualityComparer<T>.Default);
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer) => new HashSet<T>(source, comparer);
        public static Queue<T> ToQueue<T>(this IEnumerable<T> source) => new Queue<T>(source);
        public static Stack<T> ToStack<T>(this IEnumerable<T> source) => new Stack<T>(source);
    }
}
