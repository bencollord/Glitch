namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> self)
    {
        public static IParser<TToken, T> operator |(IParser<TToken, T> x, IParser<TToken, T> y) => x.Or(y);

        public static IParser<TToken, Unit> operator !(IParser<TToken, T> x) => x.Not();

        public static IParser<TToken, T> operator >>(IParser<TToken, T> x, IParser<TToken, Unit> other) => x.Then(other, (x, _) => x);

        public static IParser<TToken, T> operator >>>(IParser<TToken, T> x, Func<T, IParser<TToken, Unit>> bind) => x.Then(bind, (x, _) => x);
    }

    extension<TToken, T, TResult>(IParser<TToken, T> self)
    {
        // Map
        public static IParser<TToken, TResult> operator *(IParser<TToken, T> x, Func<T, TResult> map) => x.Select(map);
        public static IParser<TToken, TResult> operator *(Func<T, TResult> map, IParser<TToken, T> x) => x.Select(map);

        // Apply
        public static IParser<TToken, TResult> operator *(IParser<TToken, T> x, IParser<TToken, Func<T, TResult>> apply) => x.Apply(apply);
        public static IParser<TToken, TResult> operator *(IParser<TToken, Func<T, TResult>> apply, IParser<TToken, T> x) => x.Apply(apply);

        // Bind
        public static IParser<TToken, TResult> operator >>(IParser<TToken, T> x, IParser<TToken, TResult> y) => x.Then(y);

        public static IParser<TToken, TResult> operator >>>(IParser<TToken, T> x, Func<T, IParser<TToken, TResult>> bind) => x.Then(bind);
    }
}
