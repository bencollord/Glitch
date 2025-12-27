using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<T>(IParser<char, T> source)
    {
        public T Parse(string text) => source.Parse(CharSequence.From(text));

        public T Parse(IEnumerable<char> chars) => source.Parse(CharSequence.From(chars));
    }

    extension<T>(IParser<byte, T> source)
    {
        public T Parse(byte[] bytes) => source.Parse(ByteSequence.From(bytes));

        public T Parse(IEnumerable<byte> bytes) => source.Parse(ByteSequence.From(bytes));
    }

    extension<TToken, T>(IParser<TToken, T> source)
    {
        public T Parse(IEnumerable<TToken> tokens) => source.Parse(new ArrayTokenSequence<TToken>(tokens));

        public T Parse(ITokenSequence<TToken> input) =>
            source.Execute(input)
                  .Match(okay: FN.Identity,
                         fail: err => err.Throw<T>());
    }
}
