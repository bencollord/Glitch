using Glitch.Functional;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, Unit> Skip() => source.Maybe().Discard();

        public IParser<TToken, Unit> SkipAll() => source.ZeroOrMoreTimes().Discard();

        public IParser<TToken, Unit> SkipUntil<TStop>(IParser<TToken, TStop> stop) => source.Until(stop).Discard();

        // UNDONE SkipMany, Skip(count), SkipWhile(predicate)
    }
}