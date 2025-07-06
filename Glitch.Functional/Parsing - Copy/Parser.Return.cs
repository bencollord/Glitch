namespace Glitch.Functional.Parsing
{
    public static partial class Parser
    {
        public static Parser<TIn, TOut> Return<TIn, TOut>(TOut value)
            => Parser<TIn, TOut>.Return(value);

        public static Parser<TIn, TOut> Error<TIn, TOut>(ParseError error)
            => Parser<TIn, TOut>.Error(error);
    }

    public static partial class Parser<TIn>
    {
        public static Parser<TIn, TOut> Return<TOut>(TOut value)
            => Parser<TIn, TOut>.Return(value);

        public static Parser<TIn, TOut> Error<TOut>(ParseError error)
             => Parser<TIn, TOut>.Error(error);
    }

    public partial class Parser<TIn, TOut>
    {
        public static Parser<TIn, TOut> Return(TOut value)
            => new(input => Result<TOut, ParseError>.Okay(value));

        public static Parser<TIn, TOut> Error(ParseError error)
             => new(input => Result<TOut, ParseError>.Fail(error));
    }
}
