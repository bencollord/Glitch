namespace Glitch.Functional;

public static partial class FuncExtensions
{
    // Compose forward, no argument
    extension<T, TResult>(Func<T> self)
    {
        public static Func<TResult> operator >>(Func<T> f, Func<T, TResult> g) => () => g(f());
    }

    // Compose back, no argument
    extension<T, TResult>(Func<T, TResult> self)
    {
        public static Func<TResult> operator <<(Func<T, TResult> f, Func<T> g) => () => f(g());
    }

    // Compose forward
    extension<T1, T2, TResult>(Func<T1, T2> self)
    {
        public static Func<T1, TResult> operator >>(Func<T1, T2> f, Func<T2, TResult> g)
            => x => g(f(x));
    }

    // Compose back
    extension<T1, T2, TResult>(Func<T2, TResult> self)
    {
        public static Func<T1, TResult> operator <<(Func<T2, TResult> f, Func<T1, T2> g)
            => x => f(g(x));
    }
}
