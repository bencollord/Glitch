using System.Collections.Immutable;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    /// <summary>
    /// Repeats the current <see cref="Parser{TToken, T}"/> until <paramref name="stop"/>
    /// succeeds, discarding the result of <paramref name="stop"/>.
    /// </summary>
    /// <typeparam name="TStop"></typeparam>
    /// <param name="stop"></param>
    /// <returns></returns>
    public static IParser<TToken, IEnumerable<T>> Until<TToken, T, TStop>(IParser<TToken, T> parser, IParser<TToken, TStop> stop) => parser.Until(stop);

    public static IManyParser<TToken, T, IEnumerable<T>> SeparatedBy<TToken, T, TSeparator>(IParser<TToken, TSeparator> separator, IParser<TToken, T> parser) => parser.SeparatedBy(separator);

    public static IManyParser<TToken, T, IEnumerable<T>> Many<TToken, T>(IParser<TToken, T> parser) => parser.Many();

    public static IParser<TToken, IEnumerable<T>> Once<TToken, T>(IParser<TToken, T> parser) => parser.Once();

    public static IParser<TToken, IEnumerable<T>> AtLeastOnce<TToken, T>(IParser<TToken, T> parser) => parser.AtLeastOnce();

    public static IParser<TToken, IEnumerable<T>> AtLeast<TToken, T>(int times, IParser<TToken, T> parser) => parser.AtLeast(times);

    public static IParser<TToken, IEnumerable<T>> AtMost<TToken, T>(int times, IParser<TToken, T> parser) => parser.AtMost(times);

    public static IParser<TToken, IEnumerable<T>> ZeroOrMoreTimes<TToken, T>(IParser<TToken, T> parser) => parser.ZeroOrMoreTimes();

    public static IParser<TToken, IEnumerable<T>> Times<TToken, T>(int count, IParser<TToken, T> parser) => parser.Times(count);
}

