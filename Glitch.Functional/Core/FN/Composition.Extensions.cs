namespace Glitch.Functional;

public static partial class FuncExtensions
{
    // Compose forward, no argument
    extension<T, TResult>(Func<T> self)
    {
        public Func<TResult> Then(Func<T, TResult> g) => () => g(self());
    }

    // Compose back, no argument
    extension<T, TResult>(Func<T, TResult> self)
    {
        public Func<TResult> Before(Func<T> g) => () => self(g());
    }

    // Compose forward
    extension<T1, T2, TResult>(Func<T1, T2> self)
    {
        public Func<T1, TResult> Then(Func<T2, TResult> g)
            => x => g(self(x));
    }

    // Compose back
    extension<T1, T2, TResult>(Func<T2, TResult> self)
    {
        public Func<T1, TResult> Before(Func<T1, T2> g)
            => x => self(g(x));
    }
}
