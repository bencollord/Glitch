using Glitch.Functional;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    public static Parser<TToken, IEnumerable<TToken>> Sequence<TToken>(IEnumerable<Parser<TToken, TToken>> parsers) 
        => new SequenceParser<TToken, TToken>(parsers);

    private class SequenceParser<TToken, T> : Parser<TToken, IEnumerable<T>>
    {
        private readonly IEnumerable<Parser<TToken, T>> parsers;

        internal SequenceParser(IEnumerable<Parser<TToken, T>> parsers)
        {
            this.parsers = parsers;
        }

        public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
        {
            var remaining = input;
            var results = ImmutableList.CreateBuilder<T>();

            foreach (var p in parsers)
            {
                var r = p.Execute(remaining);

                if (!r.IsOkay)
                {
                    return r.Cast<IEnumerable<T>>() with
                    {
                        Remaining = input // Backtrack on failure
                    };
                }

                results.Add((T)r);
                remaining = r.Remaining;
            }

            return ParseResult.Okay(results.ToImmutableList().AsEnumerable(), remaining);
        }
    }

}
