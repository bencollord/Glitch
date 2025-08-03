using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing.Parsers
{
    internal class GuardParser<TToken, T> : Parser<TToken, T>
    {
        private readonly Parser<TToken, T> source;
        private readonly Func<T, bool> predicate;
        private readonly Func<T, ParseError<TToken, T>> error;

        internal GuardParser(Parser<TToken, T> source, Func<T, bool> predicate, Func<T, ParseError<TToken, T>> error)
        {
            this.source = source;
            this.predicate = predicate;
            this.error = error;
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
        {
            return source.Execute(input)
                         .Match(ok => predicate(ok.Value) 
                                    ? ok as ParseResult<TToken, T> // The fact that the C# compiler forces this when they're both already derived from this class is ridiculous
                                    : error(ok.Value) with { Remaining = input },
                                err => err);
        }
    }
}
