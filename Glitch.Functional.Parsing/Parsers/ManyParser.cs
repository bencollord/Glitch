using Glitch.Functional;
using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing;

using static Option;

internal record ManyParser<TToken, T, TCollection> : IManyParser<TToken, T, TCollection>
    where TCollection : IEnumerable<T>
{
    private readonly IParser<TToken, T> parser;
    private readonly Func<IEnumerable<T>, TCollection> collector;
    private Option<int> min;
    private Option<int> max;

    internal ManyParser(IParser<TToken, T> parser, Func<IEnumerable<T>, TCollection> collector, int? min = null, int? max = null)
    {
        this.parser = parser;
        this.collector = collector;
        this.min = Maybe(min);
        this.max = Maybe(max);
    }

    public IParser<TToken, TCollection> AtLeast(int times) => this with { min = times, max = None };

    public IParser<TToken, TCollection> AtMost(int times) => this with { min = None, max = times };

    public IParser<TToken, TCollection> Times(int count) => this with { min = count, max = count };

    public IParser<TToken, TCollection> ZeroOrMoreTimes() => this with { min = None, max = None };

    public IParseResult<TToken, TCollection> Execute(ITokenSequence<TToken> input)
    {
        var remaining = input;
        var items = new List<T>();
        var lastError = Option<ParseError>.None;

        while (!remaining.IsEnd)
        {
            var result = parser.Execute(remaining);

            if (!result.IsOkay(out var value))
            {
                lastError = result.ErrorOrNone();
                break;
            }

            if (max.IsSomeAnd(m => items.Count + 1 > m))
            {
                var error = ParseError.Unexpected(value, expected: [$"No more than {max.Unwrap()} items"]);

                return ParseResult<TToken>.Error<TCollection>(error, input);
            }

            items.Add(value);
            remaining = result.Remaining;
        }

        if (min.IsSomeAnd(m => items.Count < m))
        {
            // TODO Find a way to get label for successful result for better error messages.
            var expected = lastError.Iterate()
                .SelectMany(x => x.Expectations)
                .Prepend($"At least {min.Unwrap()} times, found only {items.Count}")
                .ToImmutableArray();

            var error = lastError.IfNone(_ => ParseError.Unexpected("End of input")) with
            {
                Expectations = expected
            };

            return ParseResult<TToken>.Error<TCollection>(error, input);
        }

        return ParseResult.Okay(collector(items), remaining);
    }
}

