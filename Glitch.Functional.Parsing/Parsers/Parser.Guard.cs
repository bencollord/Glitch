using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, T> Guard(Func<T, bool> predicate) => source.Guard(predicate, ParseError.Empty);

        public IParser<TToken, T> Guard(Func<T, bool> predicate, ParseError error) => source.Guard(predicate, _ => error);

        public IParser<TToken, T> Guard(Func<T, bool> predicate, Func<T, ParseError> error) => new GuardParser<TToken, T>(source, predicate, error);
    }
}
