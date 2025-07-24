namespace Glitch.Functional
{
    public static class FuncExtensions
    {
        public static Func<T2, T1, TResult> Flip<T1, T2, TResult>(this Func<T1, T2, TResult> func) => (a2, a1) => func(a1, a2);

        public static Func<T1, Func<T2, R>> Curry<T1, T2, R>(this Func<T1, T2, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, R>>> Curry<T1, T2, T3, R>(this Func<T1, T2, T3, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, Func<T4, R>>>> Curry<T1, T2, T3, T4, R>(this Func<T1, T2, T3, T4, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> Curry<T1, T2, T3, T4, T5, R>(this Func<T1, T2, T3, T4, T5, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> Curry<T1, T2, T3, T4, T5, T6, R>(this Func<T1, T2, T3, T4, T5, T6, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, R>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, R>(this Func<T1, T2, T3, T4, T5, T6, T7, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, R>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, R>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, R>>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> func) => FN.Curry(func);

        public static Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, R>>>>>>>>>> Curry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> func) => FN.Curry(func);

        public static Func<T1, T2, R> Uncurry<T1, T2, R>(this Func<T1, Func<T2, R>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, R> Uncurry<T1, T2, T3, R>(this Func<T1, Func<T2, Func<T3, R>>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, T4, R> Uncurry<T1, T2, T3, T4, R>(this Func<T1, Func<T2, Func<T3, Func<T4, R>>>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, T4, T5, R> Uncurry<T1, T2, T3, T4, T5, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, R>>>>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, T4, T5, T6, R> Uncurry<T1, T2, T3, T4, T5, T6, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, R>>>>>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, T4, T5, T6, T7, R> Uncurry<T1, T2, T3, T4, T5, T6, T7, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, R>>>>>>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, R> Uncurry<T1, T2, T3, T4, T5, T6, T7, T8, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, R>>>>>>>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, R> Uncurry<T1, T2, T3, T4, T5, T6, T7, T8, T9, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, R>>>>>>>>> func) => FN.Uncurry(func);

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R> Uncurry<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, R>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Func<T10, R>>>>>>>>>> func) => FN.Uncurry(func);
    }
}
