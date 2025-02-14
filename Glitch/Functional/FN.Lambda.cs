namespace Glitch.Functional
{
    public static partial class FN
    {
        public static Func<T> Func<T>(Func<T> func) => func;
        public static Func<T, TResult> Func<T, TResult>(Func<T, TResult> func) => func;
        public static Func<T1, T2, TResult> Func<T1, T2, TResult>(Func<T1, T2, TResult> func) => func;
        public static Func<T1, T2, T3, TResult> Func<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func) => func;
        public static Func<T1, T2, T3, T4, TResult> Func<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func) => func;

        public static Func<TResult> Then<T, TResult>(this Func<T> f, Func<T, TResult> g)
            => () => g(f());

        public static Func<T1, T3> Then<T1, T2, T3>(this Func<T1, T2> f, Func<T2, T3> g)
            => x => g(f(x));

        public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(this Func<T1, T2, R> f)
            => (T1 a) => (T2 b) => f(a, b);
        public static Func<T1, Func<T2, Func<T3, R>>> Curry<T1, T2, T3, R>(this Func<T1, T2, T3, R> f)
            => (T1 a) => (T2 b) => (T3 c) => f(a, b, c);
        public static Func<T1, Func<T2, Func<T3, Func<T4, R>>>> Curry<T1, T2, T3, T4, R>(this Func<T1, T2, T3, T4, R> f)
            => (T1 a) => (T2 b) => (T3 c) => (T4 d) => f(a, b, c, d);
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> Curry<T1, T2, T3, T4, T5, R>(this Func<T1, T2, T3, T4, T5, R> f)
            => (T1 a) => (T2 b) => (T3 c) => (T4 d) => (T5 e) => f(a, b, c, d, e);
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> Curry<T1, T2, T3, T4, T5, T6, R>(this Func<T1, T2, T3, T4, T5, T6, R> func)
            => (T1 a) => (T2 b) => (T3 c) => (T4 d) => (T5 e) => (T6 f) => func(a, b, c, d, e, f);
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, R>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, R>(this Func<T1, T2, T3, T4, T5, T6, T7, R> func)
            => (T1 a) => (T2 b) => (T3 c) => (T4 d) => (T5 e) => (T6 f) => (T7 g) => func(a, b, c, d, e, f, g);
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, R>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, R>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, R> func)
            => (T1 a) => (T2 b) => (T3 c) => (T4 d) => (T5 e) => (T6 f) => (T7 g) => (T8 h) => func(a, b, c, d, e, f, g, h);
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, R>>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> func)
            => (T1 a) => (T2 b) => (T3 c) => (T4 d) => (T5 e) => (T6 f) => (T7 g) => (T8 h) => (T9 i) => func(a, b, c, d, e, f, g, h, i);
        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, R>>>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> func)
            => (T1 a) => (T2 b) => (T3 c) => (T4 d) => (T5 e) => (T6 f) => (T7 g) => (T8 h) => (T9 i) => (T10 j) => func(a, b, c, d, e, f, g, h, i, j);
    }
}
