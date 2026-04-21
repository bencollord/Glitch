namespace Glitch.Functional;

public static partial class FN
{
    public static bool AlwaysTrue<T>(T _) => true;

    public static bool AlwaysFalse<T>(T _) => false;

    public static Func<T, bool> Predicate<T>(Func<T, bool> predicate) => predicate;

    public static Func<T, bool> And<T>(Func<T, bool> f, Func<T, bool> g) => x => f(x) && g(x);
    
    public static Func<T, bool> Or<T>(Func<T, bool> f, Func<T, bool> g) => x => f(x) || g(x);
    
    public static Func<T, bool> Not<T>(Func<T, bool> f) => x => !f(x);
}