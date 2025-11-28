namespace Glitch.Functional.Effects;

public static partial class StateExtensions
{
    extension<S, T>(IStateful<S, T> self)
    {
        public static IStateful<S, T> operator >>>(IStateful<S, T> x, Func<T, IStateful<S, Unit>> bind) => x.AndThen(bind, (x, _) => x);
    }

    extension<S, T, TResult>(IStateful<S, T> self)
    {
        // Map
        public static IStateful<S, TResult> operator *(IStateful<S, T> x, Func<T, TResult> map) => x.Select(map);
        public static IStateful<S, TResult> operator *(Func<T, TResult> map, IStateful<S, T> x) => x.Select(map);

        // Apply
        public static IStateful<S, TResult> operator *(IStateful<S, T> x, IStateful<S, Func<T, TResult>> apply) => x.Apply(apply);
        public static IStateful<S, TResult> operator *(IStateful<S, Func<T, TResult>> apply, IStateful<S, T> x) => x.Apply(apply);

        // Bind
        public static IStateful<S, TResult> operator >>>(IStateful<S, T> x, Func<T, IStateful<S, TResult>> bind) => x.AndThen(bind);

        public static IStateful<S, TResult> operator >>>(IStateful<S, T> x, IStateful<S, TResult> y) => x.Then(y);
    }
}
