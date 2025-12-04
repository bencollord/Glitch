namespace Glitch.Functional;

public static partial class FuncExtensions
{
    public static Func<T, bool> And<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) && other(t);

    public static Func<T, bool> AndNot<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) && !other(t);

    public static Func<T, bool> Or<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) || other(t);

    public static Func<T, bool> OrNot<T>(this Func<T, bool> self, Func<T, bool> other) => t => self(t) && !other(t);

    public static Func<T, bool> Not<T>(this Func<T, bool> self) => t => !self(t);
}
