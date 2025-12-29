using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, TResult> SelectMany<TElement, TResult>(Func<T, IParser<TToken, TElement>> bind, Func<T, TElement, TResult> project) => source.Then(bind, project);

        public IParser<TToken, T> Where(Func<T, bool> predicate) => source.Guard(predicate, ParseError.Empty);
    }
}