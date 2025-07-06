namespace Glitch.Functional
{
    public static partial class OneOrMany
    {
        public static OneOrMany<TResult> Invoke<TResult>(this OneOrMany<Func<TResult>> function) => function.Map(fn => fn());
        public static OneOrMany<TResult> Invoke<T, TResult>(this OneOrMany<Func<T, TResult>> function, T value) => function.Map(fn => fn(value));
        public static OneOrMany<TResult> Invoke<T1, T2, TResult>(this OneOrMany<Func<T1, T2, TResult>> function, T1 arg1, T2 arg2) => function.Map(fn => fn(arg1, arg2));
        public static OneOrMany<TResult> Invoke<T1, T2, T3, TResult>(this OneOrMany<Func<T1, T2, T3, TResult>> function, T1 arg1, T2 arg2, T3 arg3) => function.Map(fn => fn(arg1, arg2, arg3));
        public static OneOrMany<TResult> Invoke<T1, T2, T3, T4, TResult>(this OneOrMany<Func<T1, T2, T3, T4, TResult>> function, T1 arg1, T2 arg2, T3 arg3, T4 arg4) => function.Map(fn => fn(arg1, arg2, arg3, arg4));

        public static OneOrMany<Func<T2, TResult>> Invoke<T1, T2, TResult>(this OneOrMany<Func<T1, T2, TResult>> function, T1 arg) => function.Map(fn => fn.Curry()(arg));
        public static OneOrMany<Func<T2, Func<T3, TResult>>> Invoke<T1, T2, T3, TResult>(this OneOrMany<Func<T1, T2, T3, TResult>> function, T1 arg) => function.Map(fn => fn.Curry()(arg));
        public static OneOrMany<Func<T2, Func<T3, Func<T4, TResult>>>> Invoke<T1, T2, T3, T4, TResult>(this OneOrMany<Func<T1, T2, T3, T4, TResult>> function, T1 arg) => function.Map(fn => fn.Curry()(arg));

        public static OneOrMany<TResult> Apply<T, TResult>(this OneOrMany<Func<T, TResult>> function, OneOrMany<T> value)
            => value.Apply(function);
    }
}