using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class BindParser<TToken, TSource, TNext, TResult> : Parser<TToken, TResult>
    {
        private readonly Parser<TToken, TSource> source;
        private readonly Func<TSource, Parser<TToken, TNext>> next;
        private readonly Func<TSource, TNext, TResult> projection;

        internal BindParser(Parser<TToken, TSource> source, Func<TSource, Parser<TToken, TNext>> next, Func<TSource, TNext, TResult> projection)
        {
            this.source = source;
            this.next = next;
            this.projection = projection;
        }

        public override ParseResult<TToken, TResult> Execute(TokenSequence<TToken> input)
        {
            var result = source.Execute(input);

            return result.Match(
                okay => next(okay.Value)
                    .Execute(okay.Remaining)
                    .Map(nxt => projection(okay.Value, nxt)),
                err => err.Cast<TResult>());
        }
    }
}
