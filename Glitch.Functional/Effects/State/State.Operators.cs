namespace Glitch.Functional.Effects;

public static partial class StateExtensions
{
    extension<S, T>(State<S, T> self)
    {
        public static State<S, T> operator >>>(State<S, T> x, Func<T, State<S, Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<S, T, TResult>(State<S, T> self)
    {
        // Map
        public static State<S, TResult> operator *(State<S, T> x, Func<T, TResult> map) => x.Select(map);
        public static State<S, TResult> operator *(Func<T, TResult> map, State<S, T> x) => x.Select(map);

        // Apply
        public static State<S, TResult> operator *(State<S, T> x, State<S, Func<T, TResult>> apply) => x.Apply(apply);
        public static State<S, TResult> operator *(State<S, Func<T, TResult>> apply, State<S, T> x) => x.Apply(apply);

        // Bind
        public static State<S, TResult> operator >>>(State<S, T> x, Func<T, State<S, TResult>> bind) => x.AndThen(bind);

        public static State<S, TResult> operator >>>(State<S, T> x, State<S, TResult> y) => x.Then(y);
    }
}
