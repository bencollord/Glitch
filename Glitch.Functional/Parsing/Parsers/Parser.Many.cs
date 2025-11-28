using Glitch.Functional;
using Glitch.Functional.Extensions;
using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, IEnumerable<T>> AtLeastOnce() => AtLeast(1);

    public virtual Parser<TToken, IEnumerable<T>> AtLeast(int times) => new RepeatParser<TToken, T>(this, min: times);

    public virtual Parser<TToken, IEnumerable<T>> ZeroOrMoreTimes() => new RepeatParser<TToken, T>(this);

    public virtual Parser<TToken, IEnumerable<T>> Times(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return new RepeatParser<TToken, T>(this, count, count);
    }
}

internal class RepeatParser<TToken, T> : Parser<TToken, IEnumerable<T>>
{
    private readonly Parser<TToken, T> parser;
    private readonly Option<int> min;
    private readonly Option<int> max;

    internal RepeatParser(Parser<TToken, T> parser, int? min = null, int? max = null)
    {
        this.parser = parser;
        this.min = Option.Maybe(min);
        this.max = Option.Maybe(max);
    }

    public override ParseResult<TToken, IEnumerable<T>> Execute(TokenSequence<TToken> input)
    {
        var remaining = input;
        var results = new List<ParseResult<TToken, T>>();

        while (!remaining.IsEnd)
        {
            var result = parser.Execute(remaining);

            if (!result.IsOkay)
            {
                break;
            }

            if (max.IsSomeAnd(m => results.Count + 1 > m))
            {
                return ParseResult<TToken>.Error<IEnumerable<T>>(
                    result.Expectation with 
                    { 
                        Label = $"Expected no more than {max.Unwrap()} items, found {results.Count}" 
                    }, 
                    input);
            }

            results.Add(result);
            remaining = result.Remaining;
        }

        if (min.IsSomeAnd(m => results.Count < m))
        {
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

