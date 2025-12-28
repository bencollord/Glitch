using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        public IParser<TToken, TResult> Return<TResult>(TResult value) => source.Then(Parsing.Parse<TToken>.Return(value));

        public IParser<TToken, Unit> Discard() => source.Select(_ => Unit.Value);
    }
}