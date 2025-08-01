using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class SequenceParser<TToken, T> : Parser<TToken, IEnumerable<T>>
    {
        private readonly IEnumerable<Parser<TToken, T>> parsers;

        internal SequenceParser(params IEnumerable<Parser<TToken, T>> parsers)
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

                if (!r.WasSuccessful)
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
