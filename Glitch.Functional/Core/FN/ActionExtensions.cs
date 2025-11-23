namespace Glitch.Functional
{
    public static class ActionExtensions
    {
        public static Func<Unit> Return(this Action action) 
            => action.Return(Unit.Value);

        public static Func<T> Return<T>(this Action action, T value)
        {
            return () =>
            {
                action();
                return value;
            };
        }

        public static Func<T, Unit> Return<T>(this Action<T> action)
            => action.Return(Unit.Value);

        public static Func<T, TResult> Return<T, TResult>(this Action<T> action, TResult value)
        {
            return (a) =>
            {
                action(a);
                return value;
            };
        }

        public static Func<T1, T2, Unit> Return<T1, T2>(this Action<T1, T2> action)
            => action.Return(Unit.Value);

        public static Func<T1, T2, TResult> Return<T1, T2, TResult>(this Action<T1, T2> action, TResult value)
        {
            return (a1, a2) =>
            {
                action(a1, a2);
                return value;
            };
        }

        public static Func<T1, T2, T3, Unit> Return<T1, T2, T3>(this Action<T1, T2, T3> action)
            => action.Return(Unit.Value);

        public static Func<T1, T2, T3, TResult> Return<T1, T2, T3, TResult>(this Action<T1, T2, T3> action, TResult value)
        {
            return (a1, a2, a3) =>
            {
                action(a1, a2, a3);
                return value;
            };
        }

        public static Func<T1, T2, T3, T4, Unit> Return<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action)
            => action.Return(Unit.Value);

        public static Func<T1, T2, T3, T4, TResult> Return<T1, T2, T3, T4, TResult>(this Action<T1, T2, T3, T4> action, TResult value)
        {
            return (a1, a2, a3, a4) =>
            {
                action(a1, a2, a3, a4);
                return value;
            };
        }

        public static Func<T1, T2, T3, T4, T5, Unit> Return<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action)
            => action.Return(Unit.Value);

        public static Func<T1, T2, T3, T4, T5, TResult> Return<T1, T2, T3, T4, T5, TResult>(this Action<T1, T2, T3, T4, T5> action, TResult value)
        {
            return (a1, a2, a3, a4, a5) =>
            {
                action(a1, a2, a3, a4, a5);
                return value;
            };
        }

        public static Action ReturnVoid<T>(this Func<T> action)
        {
            return () =>
            {
                action();
            };
        }

        public static Action<T> ReturnVoid<T, TResult>(this Func<T, TResult> action)
        {
            return (a) =>
            {
                action(a);
            };
        }

        public static Action<T1, T2> ReturnVoid<T1, T2, TResult>(this Func<T1, T2, TResult> action)
        {
            return (a1, a2) =>
            {
                action(a1, a2);
            };
        }

        public static Action<T1, T2, T3> ReturnVoid<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> action)
        {
            return (a1, a2, a3) =>
            {
                action(a1, a2, a3);
            };
        }

        public static Action<T1, T2, T3, T4> ReturnVoid<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> action)
        {
            return (a1, a2, a3, a4) =>
            {
                action(a1, a2, a3, a4);
            };
        }

        public static Action<T1, T2, T3, T4, T5> ReturnVoid<T1, T2, T3, T4, T5, TResult>(this Func<T1, T2, T3, T4, T5, TResult> action)
        {
            return (a1, a2, a3, a4, a5) =>
            {
                action(a1, a2, a3, a4, a5);
            };
        }
    }
}