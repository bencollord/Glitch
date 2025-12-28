namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success and a successful result containing
        /// <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <returns></returns>
        public IParser<TToken, Option<T>> Maybe() => new MaybeParser<TToken, T>(source);
    }

    extension<TToken, T>(IParser<TToken, Option<T>> source)
    {
        public IParser<TToken, T> IfNone(T fallback) => source.Select(p => p.IfNone(fallback));
        public IParser<TToken, T> IfNone(Func<T> fallback) => source.Select(p => p.IfNone(fallback));
        public IParser<TToken, T> IfNone(Func<Unit, T> fallback) => source.Select(p => p.IfNone(fallback));
    }
}