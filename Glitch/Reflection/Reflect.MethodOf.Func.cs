using System.Reflection;

namespace Glitch.Reflection
{
    public static partial class Reflect
    {
        public static MethodInfo MethodOf<TResult>(Func<TResult> func) => func.Method;
        public static MethodInfo MethodOf<T, TResult>(Func<T, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, TResult>(Func<T1, T2, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> func) => func.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> func) => func.Method;
    }
}
