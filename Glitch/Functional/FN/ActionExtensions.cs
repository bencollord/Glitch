namespace Glitch.Functional
{
    public static class ActionExtensions
    {
        public static Func<Nothing> Return(this Action action) 
            => action.Return(Nothing.Value);

        public static Func<T> Return<T>(this Action action, T value)
        {
            return () =>
            {
                action();
                return value;
            };
        }

        public static Func<T, Nothing> Return<T>(this Action<T> action)
            => action.Return(Nothing.Value);

        public static Func<T, TResult> Return<T, TResult>(this Action<T> action, TResult value)
        {
            return (a) =>
            {
                action(a);
                return value;
            };
        }

        public static Func<T1, T2, Nothing> Return<T1, T2>(this Action<T1, T2> action)
            => action.Return(Nothing.Value);

        public static Func<T1, T2, TResult> Return<T1, T2, TResult>(this Action<T1, T2> action, TResult value)
        {
            return (a1, a2) =>
            {
                action(a1, a2);
                return value;
            };
        }

        public static Func<T1, T2, T3, Nothing> Return<T1, T2, T3>(this Action<T1, T2, T3> action)
            => action.Return(Nothing.Value);

        public static Func<T1, T2, T3, TResult> Return<T1, T2, T3, TResult>(this Action<T1, T2, T3> action, TResult value)
        {
            return (a1, a2, a3) =>
            {
                action(a1, a2, a3);
                return value;
            };
        }

        public static Func<T1, T2, T3, T4, Nothing> Return<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
            => action.Return(Nothing.Value);

        public static Func<T1, T2, T3, T4, TResult> Return<T1, T2, T3, T4, TResult>(this Action<T1, T2, T3, T4> action, TResult value)
        {
            return (a1, a2, a3, a4) =>
            {
                action(a1, a2, a3, a4);
                return value;
            };
        }

        public static Func<T1, T2, T3, T4, T5, Nothing> Return<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
            => action.Return(Nothing.Value);

        public static Func<T1, T2, T3, T4, T5, TResult> Return<T1, T2, T3, T4, T5, TResult>(this Action<T1, T2, T3, T4, T5> action, TResult value)
        {
            return (a1, a2, a3, a4, a5) =>
            {
                action(a1, a2, a3, a4, a5);
                return value;
            };
        }
    }
}