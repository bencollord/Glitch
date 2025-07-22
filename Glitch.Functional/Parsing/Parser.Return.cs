namespace Glitch.Functional.Parsing
{
    public static partial class Parser
    {
        public static Parser<TToken, T> Return<TToken, T>(T value)
            => Parser<TToken, T>.Return(value);

        public static Parser<TToken, T> Error<TToken, T>(ParseError<TToken, T> error)
            => Parser<TToken, T>.Error(error);
    }

    public static partial class Parser<TToken>
    {
        public static Parser<TToken, T> Return<T>(T value)
            => Parser<TToken, T>.Return(value);

        public static Parser<TToken, T> Error<T>(ParseError<TToken, T> error)
            => Parser<TToken, T>.Error(error);
    }

    public partial class Parser<TToken, T>
    {
        public static Parser<TToken, T> Return(T value)
            => new(input => ParseResult.Okay<TToken, T>(value));

        public static Parser<TToken, T> Error(ParseError<TToken, T> error)
            => new(input => error);
    }
}
