using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

public static partial class LinqExtensions
{
    extension<T>(IEnumerable<T> self)
    {
        public static IEnumerable<T> operator +(IEnumerable<T> x, IEnumerable<T> y) => x.Concat(y);
        public static IEnumerable<T> operator +(T x, IEnumerable<T> y) => y.Prepend(x);
        public static IEnumerable<T> operator +(IEnumerable<T> x, T y) => x.Append(y);
        
        public static IEnumerable<T> operator -(IEnumerable<T> x, IEnumerable<T> y) => x.Except(y);
        public static IEnumerable<T> operator -(IEnumerable<T> x, T y) => x.Except([y]);

        public static IEnumerable<T> operator |(IEnumerable<T> x, IEnumerable<T> y) => x.Union(y);
        public static IEnumerable<T> operator &(IEnumerable<T> x, IEnumerable<T> y) => x.Intersect(y);
        public static IEnumerable<T> operator ^(IEnumerable<T> x, IEnumerable<T> y) => x.Union(y).Except(x.Intersect(y));

        public static IEnumerable<T> operator >>>(IEnumerable<T> x, Func<T, IEnumerable<Unit>> bind) => x.SelectMany(bind, (x, _) => x);
    }

    extension<T, TResult>(IEnumerable<T> self)
    {
        private IEnumerable<TResult> Apply(IEnumerable<Func<T, TResult>> funcs) => 
            from val in self
            from fn in funcs
            select fn(val);

        // Map
        public static IEnumerable<TResult> operator *(IEnumerable<T> x, Func<T, TResult> map) => x.Select(map);
        public static IEnumerable<TResult> operator *(Func<T, TResult> map, IEnumerable<T> x) => x.Select(map);

        // Apply
        public static IEnumerable<TResult> operator *(IEnumerable<T> x, IEnumerable<Func<T, TResult>> apply) => x.Apply(apply);
        public static IEnumerable<TResult> operator *(IEnumerable<Func<T, TResult>> apply, IEnumerable<T> x) => x.Apply(apply);

        // Bind
        public static IEnumerable<TResult> operator >>>(IEnumerable<T> x, Func<T, IEnumerable<TResult>> bind) => x.SelectMany(bind);
    }

    extension<T1, T2, TResult>(IEnumerable<T1> self)
    {
        // Map
        public static IEnumerable<Func<T2, TResult>> operator *(IEnumerable<T1> x, Func<T1, T2, TResult> map) => x * map.Curry();
        public static IEnumerable<Func<T2, TResult>> operator *(Func<T1, T2, TResult> map, IEnumerable<T1> x) => x * map.Curry();

        // Apply
        public static IEnumerable<Func<T2, TResult>> operator *(IEnumerable<T1> x, IEnumerable<Func<T1, T2, TResult>> apply) => x.Apply(apply * Curry);
        public static IEnumerable<Func<T2, TResult>> operator *(IEnumerable<Func<T1, T2, TResult>> apply, IEnumerable<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, TResult>(IEnumerable<T1> self)
    {
        // Map
        public static IEnumerable<Func<T2, Func<T3, TResult>>> operator *(IEnumerable<T1> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();
        public static IEnumerable<Func<T2, Func<T3, TResult>>> operator *(Func<T1, T2, T3, TResult> map, IEnumerable<T1> x) => x * map.Curry();

        // Apply
        public static IEnumerable<Func<T2, Func<T3, TResult>>> operator *(IEnumerable<T1> x, IEnumerable<Func<T1, T2, T3, TResult>> apply) => x.Apply(apply * Curry);
        public static IEnumerable<Func<T2, Func<T3, TResult>>> operator *(IEnumerable<Func<T1, T2, T3, TResult>> apply, IEnumerable<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, TResult>(IEnumerable<T1> self)
    {
        // Map
        public static IEnumerable<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(IEnumerable<T1> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();
        public static IEnumerable<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Func<T1, T2, T3, T4, TResult> map, IEnumerable<T1> x) => x * map.Curry();

        // Apply
        public static IEnumerable<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(IEnumerable<T1> x, IEnumerable<Func<T1, T2, T3, T4, TResult>> apply) => x.Apply(apply * Curry);
        public static IEnumerable<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(IEnumerable<Func<T1, T2, T3, T4, TResult>> apply, IEnumerable<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, TResult>(IEnumerable<T1> self)
    {
        // Map
        public static IEnumerable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(IEnumerable<T1> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();
        public static IEnumerable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Func<T1, T2, T3, T4, T5, TResult> map, IEnumerable<T1> x) => x * map.Curry();

        // Apply
        public static IEnumerable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(IEnumerable<T1> x, IEnumerable<Func<T1, T2, T3, T4, T5, TResult>> apply) => x.Apply(apply * Curry);
        public static IEnumerable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(IEnumerable<Func<T1, T2, T3, T4, T5, TResult>> apply, IEnumerable<T1> x) => x.Apply(apply * Curry);
    }
}
