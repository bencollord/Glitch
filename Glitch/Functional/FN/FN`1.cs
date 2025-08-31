namespace Glitch.Functional
{
    public static class FN<T>
    {
        public static T Identity(T x) => x;

        public static Func<T, TResult> Constant<TResult>(TResult value) => _ => value;

        public static void Nop(T _) { /* Do nothing */ }

        public static Func<T> Func(Func<T> func) => func;
        
        public static Func<T, TResult> Func<TResult>(Func<T, TResult> func) => func;
        
        public static Func<T, T2, TResult> Func<T2, TResult>(Func<T, T2, TResult> func) => func;
        
        public static Func<T, T2, T3, TResult> Func<T2, T3, TResult>(Func<T, T2, T3, TResult> func) => func;
        
        public static Func<T, T2, T3, T4, TResult> Func<T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> func) => func;

        public static Action<T> Action(Action<T> action) => action;

        public static Action<T, T2> Action<T2>(Action<T, T2> action) => action;

        public static Action<T, T2, T3> Action<T2, T3>(Action<T, T2, T3> action) => action;

        public static Action<T, T2, T3, T4> Action<T2, T3, T4>(Action<T, T2, T3, T4> action) => action;
    }
}
