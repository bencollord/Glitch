namespace Glitch.Functional.Parsing;

// Instance
public abstract partial class Parser<TToken, T>
{
}

// Extensions
public static partial class ParserExtensions
{
    extension<TToken, T>(Parser<TToken, T> self)
    {
        public static Parser<TToken, T> operator |(Parser<TToken, T> x, Parser<TToken, T> y) => x.Or(y);
        
        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Parser<TToken, Unit> other) => x.Then(other, (x, _) => x);

        public static Parser<TToken, T> operator >>(Parser<TToken, T> x, Func<T, Parser<TToken, Unit>> bind) => x.Then(bind, (x, _) => x);
    }

    extension<TToken, T, TResult>(Parser<TToken, T> self)
    {
        // Map
        public static Parser<TToken, TResult> operator *(Parser<TToken, T> x, Func<T, TResult> map) => x.Select(map);
        public static Parser<TToken, TResult> operator *(Func<T, TResult> map, Parser<TToken, T> x) => x.Select(map);

        // Apply
        public static Parser<TToken, TResult> operator *(Parser<TToken, T> x, Parser<TToken, Func<T, TResult>> apply) => x.Apply(apply);
        public static Parser<TToken, TResult> operator *(Parser<TToken, Func<T, TResult>> apply, Parser<TToken, T> x) => x.Apply(apply);

        // Bind
        public static Parser<TToken, TResult> operator >>(Parser<TToken, T> x, Func<T, Parser<TToken, TResult>> bind) => x.Then(bind);

        public static Parser<TToken, TResult> operator >>(Parser<TToken, T> x, Parser<TToken, TResult> y) => x.Then(y);
    }
}
