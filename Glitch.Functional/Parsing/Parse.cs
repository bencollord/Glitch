using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<TToken, TToken> Satisfy<TToken>(Func<TToken, bool> predicate)
            => new Parser<TToken, TToken>(input =>
                input.IsEnd
                    ? ParseResult.Fail<TToken, TToken>("End of input reached")
                    : ParseResult.Okay(input.Current, input.Advance())
                                 .Guard(predicate, Expectation.Unexpected(input.Current))
            );

        public static Parser<TToken, TToken> Any<TToken>() => Satisfy<TToken>(_ => true);

        public static Parser<TToken, TToken> Token<TToken>(TToken token)
        {
            return Satisfy<TToken>(t => t?.Equals(token) == true);
        }

        public static Parser<TToken, Unit> Not<TToken, T>(Parser<TToken, T> parser)
            => parser.Not();

        public static Parser<TToken, IEnumerable<TToken>> Sequence<TToken>(IEnumerable<Parser<TToken, TToken>> parsers)
        {
            return new(input => 
            {
                var remaining = input;
                var results = ImmutableList.CreateBuilder<TToken>();

                foreach (var p in parsers)
                {
                    var r = p.Execute(remaining);

                    if (!r.WasSuccessful)
                    {
                        return r.Cast<IEnumerable<TToken>>() with
                        {
                            Remaining = input // Backtrack on failure
                        };
                    }

                    results.Add((TToken)r);
                    remaining = r.Remaining;
                }

                return ParseResult.Okay(results.ToImmutableList().AsEnumerable(), remaining);
            });
        }
    }
}
