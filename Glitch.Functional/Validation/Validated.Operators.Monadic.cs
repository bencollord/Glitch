namespace Glitch.Functional.Validation;

public static partial class ValidatedExtensions
{
    extension<T, E>(Validated<T, E> self)
    {
        // Unit bind
        public static Validated<T, E> operator >>>(Validated<T, E> x, Func<T, Validated<Unit, E>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, E, TResult>(Validated<T, E> self)
    {
        // Map
        public static Validated<TResult, E> operator *(Validated<T, E> x, Func<T, TResult> map) => x.Select(map);
        public static Validated<TResult, E> operator *(Func<T, TResult> map, Validated<T, E> x) => x.Select(map);

        // Apply
        public static Validated<TResult, E> operator *(Validated<T, E> x, Validated<Func<T, TResult>, E> apply) => x.Apply(apply);
        public static Validated<TResult, E> operator *(Validated<Func<T, TResult>, E> apply, Validated<T, E> x) => x.Apply(apply);

        // Bind
        public static Validated<TResult, E> operator >>>(Validated<T, E> x, Func<T, Validated<TResult, E>> bind) => x.AndThen(bind);
    }

    extension<T1, T2, E, TResult>(Validated<T1, E> self)
    {
        // Map
        public static Validated<Func<T2, TResult>, E> operator *(Validated<T1, E> x, Func<T1, T2, TResult> map) => x * map.Curry();
        public static Validated<Func<T2, TResult>, E> operator *(Func<T1, T2, TResult> map, Validated<T1, E> x) => x * map.Curry();
        
        // Apply
        public static Validated<Func<T2, TResult>, E> operator *(Validated<T1, E> x, Validated<Func<T1, T2, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Validated<Func<T2, TResult>, E> operator *(Validated<Func<T1, T2, TResult>, E> apply, Validated<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, E, TResult>(Validated<T1, E> self)
    {
        // Map
        public static Validated<Func<T2, Func<T3, TResult>>, E> operator *(Validated<T1, E> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();
        public static Validated<Func<T2, Func<T3, TResult>>, E> operator *(Func<T1, T2, T3, TResult> map, Validated<T1, E> x) => x * map.Curry();

        // Apply
        public static Validated<Func<T2, Func<T3, TResult>>, E> operator *(Validated<T1, E> x, Validated<Func<T1, T2, T3, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Validated<Func<T2, Func<T3, TResult>>, E> operator *(Validated<Func<T1, T2, T3, TResult>, E> apply, Validated<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, E, TResult>(Validated<T1, E> self)
    {
        // Map
        public static Validated<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Validated<T1, E> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();
        public static Validated<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Func<T1, T2, T3, T4, TResult> map, Validated<T1, E> x) => x * map.Curry();

        // Apply
        public static Validated<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Validated<T1, E> x, Validated<Func<T1, T2, T3, T4, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Validated<Func<T2, Func<T3, Func<T4, TResult>>>, E> operator *(Validated<Func<T1, T2, T3, T4, TResult>, E> apply, Validated<T1, E> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, E, TResult>(Validated<T1, E> self)
    {
        // Map
        public static Validated<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Validated<T1, E> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();
        public static Validated<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Func<T1, T2, T3, T4, T5, TResult> map, Validated<T1, E> x) => x * map.Curry();
        
        // Apply
        public static Validated<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Validated<T1, E> x, Validated<Func<T1, T2, T3, T4, T5, TResult>, E> apply) => x.Apply(apply * Curry);
        public static Validated<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>, E> operator *(Validated<Func<T1, T2, T3, T4, T5, TResult>, E> apply, Validated<T1, E> x) => x.Apply(apply * Curry);
    }
}