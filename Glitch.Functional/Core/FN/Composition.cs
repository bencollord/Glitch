namespace Glitch.Functional;

public static partial class FN
{
    public static Func<TResult> Compose<T, TResult>(Func<T> f, Func<T, TResult> g)
        => () => g(f());

    public static Func<T1, T3> Compose<T1, T2, T3>(Func<T1, T2> f, Func<T2, T3> g) => x => g(f(x));

    public static Func<T1, T4> Compose<T1, T2, T3, T4>(Func<T1, T2> f1, Func<T2, T3> f2, Func<T3, T4> f3) => x => f3(f2(f1(x)));

    public static Func<T1, T5> Compose<T1, T2, T3, T4, T5>(Func<T1, T2> f1, Func<T2, T3> f2, Func<T3, T4> f3, Func<T4, T5> f4) => x => f4(f3(f2(f1(x))));

    public static Func<T1, T6> Compose<T1, T2, T3, T4, T5, T6>(Func<T1, T2> f1, Func<T2, T3> f2, Func<T3, T4> f3, Func<T4, T5> f4, Func<T5, T6> f5) => x => f5(f4(f3(f2(f1(x)))));
}