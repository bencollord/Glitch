using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;
using Glitch.Functional.Extensions;

namespace Glitch.Functional.Parsing;

internal class SeparatedParser<TToken, T, TSeparator, TCollection> : IManyParser<TToken, T, TCollection>
    where TCollection : IEnumerable<T>
{
    private IParser<TToken, T> parser;
    private Func<IEnumerable<T>, TCollection> collector;
    private IParser<TToken, TSeparator> separator;

    internal SeparatedParser(IParser<TToken, T> parser, IParser<TToken, TSeparator> separator, Func<IEnumerable<T>, TCollection> collector)
    {
        this.parser = parser;
        this.separator = separator;
        this.collector = collector;
    }

    public IParser<TToken, TCollection> AtLeast(int times) =>
        Parser.SelectError(
            from once in parser
            from rest in separator
                .Then(parser)
                .AtLeast(times - 1)
            let items = once + rest
            select collector(items),
            err => err with { Expectations = err.Expectations.Times(times, Option.None) });

    public IParser<TToken, TCollection> AtMost(int times) =>
        // TODO Add support for allowing/disallowing a terminating separator
        Parser.SelectError(
            from once in parser.Maybe()
            from rest in separator
                .Then(parser)
                .AtMost(times - 1)
            let items = once.Iterate() + rest
            select collector(items),
            err => err with { Expectations = err.Expectations.Times(Option.None, times) });

    public IParser<TToken, TCollection> ZeroOrMoreTimes() =>
        // TODO Add support for allowing/disallowing a terminating separator
        parser.Before(separator)
              .ZeroOrMoreTimes()
              .Then(parser.Maybe(), (items, lastOpt) => items + lastOpt.Iterate())
              .Select(collector);

    public IParser<TToken, TCollection> Times(int count) =>
         Parser.SelectError(
            from once in parser
            from rest in separator.Then(parser).Times(count - 1)
            select collector(once + rest),
            err => err with { Expectations = err.Expectations.Times(count) });

    public IParseResult<TToken, TCollection> Execute(ITokenSequence<TToken> input)
    {
        return ZeroOrMoreTimes().Execute(input); // Default
    }
}
