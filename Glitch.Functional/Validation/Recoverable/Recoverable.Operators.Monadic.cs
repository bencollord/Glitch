namespace Glitch.Functional.Errors;

public static partial class RecoverableExtensions
{
    extension<T, E>(Recoverable<T, E> self)
    {
        // Unit bind
        public static Recoverable<T, E> operator >>>(Recoverable<T, E> x, Func<T, Recoverable<Unit, E>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, E, TResult>(Recoverable<T, E> self)
    {
        // Map
        public static Recoverable<TResult, E> operator *(Recoverable<T, E> x, Func<T, TResult> map) => x.Select(map);
        public static Recoverable<TResult, E> operator *(Func<T, TResult> map, Recoverable<T, E> x) => x.Select(map);

        // Apply
        public static Recoverable<TResult, E> operator *(Recoverable<T, E> x, Recoverable<Func<T, TResult>, E> apply) => x.Apply(apply);
        public static Recoverable<TResult, E> operator *(Recoverable<Func<T, TResult>, E> apply, Recoverable<T, E> x) => x.Apply(apply);

        // Bind
        public static Recoverable<TResult, E> operator >>>(Recoverable<T, E> x, Func<T, Recoverable<TResult, E>> bind) => x.AndThen(bind);
    }

    extension<T1, T2, E, TResult>(Recoverable<T1, E> self)
    {
        // Map
        public static Recoverable<Func<T2, TResult>, E> operator *(Recoverable<T1, E> x, Func<T1, T2, TResult> map) => x * map.Curry();
        public static Recoverable<Func<T2, TResult>, E> operator *(Func<T1, T2, TResult> map, Recoverable<T1, E> x) => x * map.Curry();
        
        // Apply
        public static Recoverable<Func<T2, TResult>, E> operator *(Recoverable<T1, E> x, Recoverable<Func<T1, T2, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Recoverable<Func<T2, TResult>, E> operator *(Recoverable<Func<T1, T2, TResult>, E> apply, Recoverable<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, E, TResult>(Recoverable<T1, E> self)
    {
        // Map
        public static Recoverable<Func<T2, Func<T3, TResult>>, E> operator *(Recoverable<T1, E> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();
        public static Recoverable<Func<T2, Func<T3, TResult>>, E> operator *(Func<T1, T2, T3, TResult> map, Recoverable<T1, E> x) => x * map.Curry();

        // Apply
        public static Recoverable<Func<T2, Func<T3, TResult>>, E> operator *(Recoverable<T1, E> x, Recoverable<Func<T1, T2, T3, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Recoverable<Func<T2, Func<T3, TResult>>, E> operator *(Recoverable<Func<T1, T2, T3, TResult>, E> apply, Recoverable<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, E, TResult>(Recoverable<T1, E> self)
    {
        // Map
        public static Recoverable<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Recoverable<T1, E> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();
        public static Recoverable<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Func<T1, T2, T3, T4, TResult> map, Recoverable<T1, E> x) => x * map.Curry();

        // Apply
        public static Recoverable<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Recoverable<T1, E> x, Recoverable<Func<T1, T2, T3, T4, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Recoverable<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Recoverable<Func<T1, T2, T3, T4, TResult>, E> apply, Recoverable<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, E, TResult>(Recoverable<T1, E> self)
    {
        // Map
        public static Recoverable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Recoverable<T1, E> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();
        public static Recoverable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Func<T1, T2, T3, T4, T5, TResult> map, Recoverable<T1, E> x) => x * map.Curry();
        
        // Apply
        public static Recoverable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Recoverable<T1, E> x, Recoverable<Func<T1, T2, T3, T4, T5, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Recoverable<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Recoverable<Func<T1, T2, T3, T4, T5, TResult>, E> apply, Recoverable<T1, E> x) => x.Apply(apply * Curry);
    }
}