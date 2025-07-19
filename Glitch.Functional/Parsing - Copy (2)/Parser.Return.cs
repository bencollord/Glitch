namespace Glitch.Functional.Parsing
{
    public static partial class Parser
    {
        public static Parser<T> Return<T>(T value)
            => Parser<T>.Return(value);

        public static Parser<T> Error<T>(ParseError error)
            => Parser<T>.Error(error);
    }

    public partial class Parser<T>
    {
        public static Parser<T> Return(T value)
            => new(input => ParseResult.Okay(value));

        public static Parser<T> Error(ParseError error)
             => new(input => ParseResult.Fail<T>(error));
    }
}
