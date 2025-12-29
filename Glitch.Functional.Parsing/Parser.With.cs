using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, T> WithLabel(string label) => source.SelectError(err => err with { Label = label });

        public IParser<TToken, T> WithExpected(string expected) => source.SelectError(err => err with { Expectations = new([expected]) });

        public IParser<TToken, T> WithExpected(Expectation expected) => source.SelectError(err => err with { Expectations = expected });
    }
}