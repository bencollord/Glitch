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
        from once in parser
        from rest in separator
            .Then(parser)
            .AtLeast(times - 1)
        from last in parser.Maybe()
        let items = once + rest + last.Iterate()
        select collector(items);

    public IParser<TToken, TCollection> AtMost(int times) =>
        // TODO Add support for allowing/disallowing a terminating separator
        from items in parser.Before(separator).AtMost(times - 1)
        from last in parser.AtMost(1)
        select collector(items + last);

    public IParser<TToken, TCollection> ZeroOrMoreTimes() =>
        // TODO Add support for allowing/disallowing a terminating separator
        parser.Before(separator)
              .ZeroOrMoreTimes()
              .Then(parser.Maybe(), (items, lastOpt) => items + lastOpt.Iterate())
              .Select(collector);

    public IParser<TToken, TCollection> Times(int count)
        => from once in parser
           from rest in separator.Then(parser).Times(count - 1)
           select collector(once + rest);

    public IParseResult<TToken, TCollection> Execute(ITokenSequence<TToken> input)
    {
        return ZeroOrMoreTimes().Execute(input); // Default
    }
}
