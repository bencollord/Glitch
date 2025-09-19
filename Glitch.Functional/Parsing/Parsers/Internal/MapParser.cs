using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class MapParser<TToken, T, TResult> : Parser<TToken, TResult>
    {
        private readonly Parser<TToken, T> source;
        private readonly Func<T, TResult> projection;

        internal MapParser(Parser<TToken, T> source, Func<T, TResult> projection)
        {
            this.source = source;
            this.projection = projection;
        }

        public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
        {
            return source.Execute(input).Map(projection);
        }
    }
}
