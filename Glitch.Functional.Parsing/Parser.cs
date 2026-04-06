using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

/// <summary>
/// Extensions for parsing to a result and throwing on failure.
/// </summary>
/// <remarks>
/// This is semantically part of <see cref="Parser"/>, but the naming collision
/// with <see cref="Parsing.Parse{TToken}"/> is making the rest of the methods in that
/// class a giant pain in the neck to implement. I could always qualify the name, but
/// I'd rather not.
/// </remarks>
public static partial class ParserExecuteExtensions
{
    extension<T>(IParser<char, T> source)
    {
        public T Parse(string text) => source.Parse(CharSequence.From(text));

        public T Parse(IEnumerable<char> chars) => source.Parse(CharSequence.From(chars));

        public Result<T, ParseError> TryParse(string text) => source.TryParse(CharSequence.From(text));

        public Result<T, ParseError> TryParse(IEnumerable<char> chars) => source.TryParse(CharSequence.From(chars));

        public IParseResult<char, T> Execute(string text) => source.Execute(CharSequence.From(text));

        public IParseResult<char, T> Execute(IEnumerable<char> chars) => source.Execute(CharSequence.From(chars));
    }

    extension<T>(IParser<byte, T> source)
    {
        public T Parse(byte[] bytes) => source.Parse(ByteSequence.From(bytes));

        public T Parse(IEnumerable<byte> bytes) => source.Parse(ByteSequence.From(bytes));

        public Result<T, ParseError> TryParse(byte[] bytes) => source.TryParse(ByteSequence.From(bytes));

        public Result<T, ParseError> TryParse(IEnumerable<byte> bytes) => source.TryParse(ByteSequence.From(bytes));

        public IParseResult<byte, T> Execute(byte[] bytes) => source.Execute(ByteSequence.From(bytes));

        public IParseResult<byte, T> Execute(IEnumerable<byte> bytes) => source.Execute(ByteSequence.From(bytes));
    }

    extension<TToken, T>(IParser<TToken, T> source)
    {
        public T Parse(IEnumerable<TToken> tokens) => source.Parse(new ArrayTokenSequence<TToken>(tokens));

        public Result<T, ParseError> TryParse(IEnumerable<TToken> tokens) => source.TryParse(new ArrayTokenSequence<TToken>(tokens));

        public IParseResult<TToken, T> Execute(IEnumerable<TToken> tokens) => source.Execute(new ArrayTokenSequence<TToken>(tokens));

        public Result<T, ParseError> TryParse(ITokenSequence<TToken> input) =>
            source.Execute(input)
                  .Match(okay: Result.Okay<T, ParseError>,
                         fail: Result.Fail<T, ParseError>);

        public T Parse(ITokenSequence<TToken> input) =>
            source.TryParse(input).IfFail(err => throw err);
    }
}
