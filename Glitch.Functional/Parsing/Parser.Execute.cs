using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public partial class Parser<TToken, T>
    {
        public ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
            => parser(input);

        public Result<T, ParseError> TryParse(TokenSequence<TToken> input)
            => Execute(input).Match(ok => Result.Okay<T, ParseError>(ok.Value),
                                    err => Result.Fail<T, ParseError>(new ParseError(err.Message, typeof(T)))); // TODO Unify ParseError types

        public T Parse(TokenSequence<TToken> input)
            => Execute(input).Match(ok => ok.Value,
                                    err => throw ParseException.FromError(err));
    }

    public static partial class ParserExtensions
    {
        public static ParseResult<char, T> Execute<T>(this Parser<char, T> parser, string input)
            => parser.Execute(new CharSequence(input));

        public static Result<T, ParseError> TryParse<T>(this Parser<char, T> parser, string input)
            => parser.TryParse(new CharSequence(input));

        public static T Parse<T>(this Parser<char, T> parser, string input)
            => parser.Parse(new CharSequence(input));

    }
}
