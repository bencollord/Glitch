using Glitch.Functional.Parsing.Results;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Parsing;

using static Parse;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, T> Before([DisallowNull] TToken token) => source.Before(Parse<TToken>.Token(token));

        public IParser<TToken, T> Before<TOther>(IParser<TToken, TOther> parser) => source.Then(parser, (me, _) => me);

        public IParser<TToken, T> After([DisallowNull] TToken token) => source.After(Parse<TToken>.Token(token));

        public IParser<TToken, T> After<TOther>(IParser<TToken, TOther> parser) => parser.Then(source, (_, me) => me);

        public IParser<TToken, T> Between<TSeparator>(TToken separator) => source.Between(separator, separator);

        public IParser<TToken, T> Between(TToken start, TToken stop) => source.Between(Parse<TToken>.Token(start), Parse<TToken>.Token(stop));

        public IParser<TToken, T> Between<TSeparator>(IParser<TToken, TSeparator> separator) => source.Between(separator, separator);

        public IParser<TToken, T> Between<TStart, TStop>(IParser<TToken, TStart> start, IParser<TToken, TStop> stop) =>
            from s in start
            from x in source
            from e in stop
            select x;
    }
}