using System.Reflection;

namespace Glitch.Reflection
{
    public static partial class Reflect
    {
        public static MethodInfo MethodOf(Action action) => action.Method;
        public static MethodInfo MethodOf<T>(Action<T> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2>(Action<T1, T2> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3>(Action<T1, T2, T3> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> action) => action.Method;
        public static MethodInfo MethodOf<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> action) => action.Method;
    }
}
