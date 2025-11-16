namespace Glitch.Functional.Core
{
    public static partial class FuncExtensions
    {
        public static Func<TResult> Apply<T, TResult>(this Func<T, TResult> func, T arg) => () => func(arg);
        public static Func<T2, TResult> Apply<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 arg1) => (arg2) => func(arg1, arg2);
        public static Func<T2, T3, TResult> Apply<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 arg1) => (arg2, arg3) => func(arg1, arg2, arg3);
        public static Func<T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 arg1) => (arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4);
        public static Func<T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T1 arg1) => (arg2, arg3, arg4, arg5) => func(arg1, arg2, arg3, arg4, arg5);
        public static Func<T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func, T1 arg1) => (arg2, arg3, arg4, arg5, arg6) => func(arg1, arg2, arg3, arg4, arg5, arg6);
        public static Func<T2, T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T1 arg1) => (arg2, arg3, arg4, arg5, arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        public static Func<T2, T3, T4, T5, T6, T7, T8, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T1 arg1) => (arg2, arg3, arg4, arg5, arg6, arg7, arg8) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        public static Func<T2, T3, T4, T5, T6, T7, T8, T9, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, T1 arg1) => (arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        public static Func<T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, T1 arg1) => (arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

        public static Func<T1, TResult> ApplyBack<T1, T2, TResult>(this Func<T1, T2, TResult> func, T2 arg2) => (arg1) => func(arg1, arg2);
        public static Func<T1, T2, TResult> ApplyBack<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T3 arg3) => (arg1, arg2) => func(arg1, arg2, arg3);
        public static Func<T1, T2, T3, TResult> ApplyBack<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T4 arg4) => (arg1, arg2, arg3) => func(arg1, arg2, arg3, arg4);
        public static Func<T1, T2, T3, T4, TResult> ApplyBack<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> func, T5 arg5) => (arg1, arg2, arg3, arg4) => func(arg1, arg2, arg3, arg4, arg5);
        public static Func<T1, T2, T3, T4, T5, TResult> ApplyBack<T1, T2, T3, T4, T5, T6, TResult>(this Func<T1, T2, T3, T4, T5, T6, TResult> func, T6 arg6) => (arg1, arg2, arg3, arg4, arg5) => func(arg1, arg2, arg3, arg4, arg5, arg6);
        public static Func<T1, T2, T3, T4, T5, T6, TResult> ApplyBack<T1, T2, T3, T4, T5, T6, T7, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, T7 arg7) => (arg1, arg2, arg3, arg4, arg5, arg6) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> ApplyBack<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func, T8 arg8) => (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> ApplyBack<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func, T9 arg9) => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> ApplyBack<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func, T10 arg10) => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
    }
}
