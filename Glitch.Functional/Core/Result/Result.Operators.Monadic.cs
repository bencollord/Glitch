using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T, E>(Result<T, E> self)
    {
        public static Result<T, E> operator >>(Result<T, E> x, Func<T, Result<Unit, E>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, E, TResult>(Result<T, E> self)
    {
        // Map
        public static Result<TResult, E> operator *(Result<T, E> x, Func<T, TResult> map) => x.Select(map);
        public static Result<TResult, E> operator *(Func<T, TResult> map, Result<T, E> x) => x.Select(map);

        // Apply
        public static Result<TResult, E> operator *(Result<T, E> x, Result<Func<T, TResult>, E> apply) => x.Apply(apply);
        public static Result<TResult, E> operator *(Result<Func<T, TResult>, E> apply, Result<T, E> x) => x.Apply(apply);

        // Bind
        public static Result<TResult, E> operator >>(Result<T, E> x, Func<T, Result<TResult, E>> bind) => x.AndThen(bind);
    }

    extension<T1, T2, E, TResult>(Result<T1, E> self)
    {
        // Map
        public static Result<Func<T2, TResult>, E> operator *(Result<T1, E> x, Func<T1, T2, TResult> map) => x * map.Curry();
        public static Result<Func<T2, TResult>, E> operator *(Func<T1, T2, TResult> map, Result<T1, E> x) => x * map.Curry();
        
        // Apply
        public static Result<Func<T2, TResult>, E> operator *(Result<T1, E> x, Result<Func<T1, T2, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, TResult>, E> operator *(Result<Func<T1, T2, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, E, TResult>(Result<T1, E> self)
    {
        // Map
        public static Result<Func<T2, Func<T3, TResult>>, E> operator *(Result<T1, E> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();
        public static Result<Func<T2, Func<T3, TResult>>, E> operator *(Func<T1, T2, T3, TResult> map, Result<T1, E> x) => x * map.Curry();

        // Apply
        public static Result<Func<T2, Func<T3, TResult>>, E> operator *(Result<T1, E> x, Result<Func<T1, T2, T3, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, Func<T3, TResult>>, E> operator *(Result<Func<T1, T2, T3, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, E, TResult>(Result<T1, E> self)
    {
        // Map
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Result<T1, E> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Func<T1, T2, T3, T4, TResult> map, Result<T1, E> x) => x * map.Curry();

        // Apply
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Result<T1, E> x, Result<Func<T1, T2, T3, T4, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Result<Func<T1, T2, T3, T4, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, E, TResult>(Result<T1, E> self)
    {
        // Map
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Result<T1, E> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Func<T1, T2, T3, T4, T5, TResult> map, Result<T1, E> x) => x * map.Curry();
        
        // Apply
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Result<T1, E> x, Result<Func<T1, T2, T3, T4, T5, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Result<Func<T1, T2, T3, T4, T5, TResult>, E> apply, Result<T1, E> x) => x.Apply(apply * Curry);
    }
}