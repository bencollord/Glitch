using Glitch.Functional;
using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

using static Option;

internal record ManyParser<TToken, T, TCollection> : IManyParser<TToken, T, TCollection>, IParser<TToken, TCollection>
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

    public IParser<TToken, TCollection> Times(int count) => this with { min = count, max = count };

    public IParser<TToken, TCollection> ZeroOrMoreTimes() => this with { min = None, max = None };

    public IParseResult<TToken, TCollection> Execute(ITokenSequence<TToken> input)
    {
        var remaining = input;
        var results = new List<IParseResult<TToken, T>>();

        while (!remaining.IsEnd)
        {
            var result = parser.Execute(remaining);

            if (!result.IsOkay)
            {
                break;
            }

            if (max.IsSomeAnd(m => results.Count + 1 > m))
            {
                return ParseResult<TToken>.Error<TCollection>(ParseError.New($"Expected no more than {max.Unwrap()} items, found {results.Count}"), input);
            }

            results.Add(result);
            remaining = result.Remaining;
        }

        if (min.IsSomeAnd(m => results.Count < m))
        {
            // TODO Find a way to get label for successful result
            var messageTemplate = $"Expected '{{0}}' {min.Unwrap()} times, found only {results.Count} times";

            var expectation = results
                .LastOrNone()
                .Match(some: r => r.Expectation with 
                             { 
                                 Label = string.Format(messageTemplate, r.Expectation.Label) 
                             },
                       none: _ => Expectation.Labeled<TToken>(
                           "Unexpected end of input. " + 
                           string.Format(messageTemplate, typeof(T))));

            return ParseResult<TToken>.Error<IEnumerable<T>>(expectation, remaining);
        }

        return ParseResult.Okay(
            results.Cast<ParseSuccess<TToken, T>>() 
                   .Select(r => r.Value), 
            remaining);
    }
}

