namespace Glitch.Functional;

public static partial class ExpectedExtensions
{
    extension<T>(Result<T> self)
    {
        public static Result<T> operator >>>(Result<T> x, Func<T, Result<Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<T, TResult>(Result<T> self)
    {
        // Map
        public static Result<TResult> operator *(Result<T> x, Func<T, TResult> map) => x.Select(map);
        public static Result<TResult> operator *(Func<T, TResult> map, Result<T> x) => x.Select(map);

        // Apply
        public static Result<TResult> operator *(Result<T> x, Result<Func<T, TResult>> apply) => x.Apply(apply);
        public static Result<TResult> operator *(Result<Func<T, TResult>> apply, Result<T> x) => x.Apply(apply);

        // Bind
        public static Result<TResult> operator >>>(Result<T> x, Func<T, Result<TResult>> bind) => x.AndThen(bind);
    }

    extension<T1, T2, TResult>(Result<T1> self)
    {
        // Map
        public static Result<Func<T2, TResult>> operator *(Result<T1> x, Func<T1, T2, TResult> map) => x * map.Curry();
        public static Result<Func<T2, TResult>> operator *(Func<T1, T2, TResult> map, Result<T1> x) => x * map.Curry();

        // Apply
        public static Result<Func<T2, TResult>> operator *(Result<T1> x, Result<Func<T1, T2, TResult>> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, TResult>> operator *(Result<Func<T1, T2, TResult>> apply, Result<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, TResult>(Result<T1> self)
    {
        // Map
        public static Result<Func<T2, Func<T3, TResult>>> operator *(Result<T1> x, Func<T1, T2, T3, TResult> map) => x * map.Curry();
        public static Result<Func<T2, Func<T3, TResult>>> operator *(Func<T1, T2, T3, TResult> map, Result<T1> x) => x * map.Curry();

        // Apply
        public static Result<Func<T2, Func<T3, TResult>>> operator *(Result<T1> x, Result<Func<T1, T2, T3, TResult>> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, Func<T3, TResult>>> operator *(Result<Func<T1, T2, T3, TResult>> apply, Result<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, TResult>(Result<T1> self)
    {
        // Map
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Result<T1> x, Func<T1, T2, T3, T4, TResult> map) => x * map.Curry();
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Func<T1, T2, T3, T4, TResult> map, Result<T1> x) => x * map.Curry();

        // Apply
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Result<T1> x, Result<Func<T1, T2, T3, T4, TResult>> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, Func<T3, Func<T4, TResult>>>> operator *(Result<Func<T1, T2, T3, T4, TResult>> apply, Result<T1> x) => x.Apply(apply * Curry);
    }

    extension<T1, T2, T3, T4, T5, TResult>(Result<T1> self)
    {
        // Map
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Result<T1> x, Func<T1, T2, T3, T4, T5, TResult> map) => x * map.Curry();
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Func<T1, T2, T3, T4, T5, TResult> map, Result<T1> x) => x * map.Curry();

        // Apply
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Result<T1> x, Result<Func<T1, T2, T3, T4, T5, TResult>> apply) => x.Apply(apply * Curry);
        public static Result<Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> operator *(Result<Func<T1, T2, T3, T4, T5, TResult>> apply, Result<T1> x) => x.Apply(apply * Curry);
    }
}
