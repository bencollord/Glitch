using Glitch.Functional.Parsing.Input;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    internal class TraceParser<TToken, T> : Parser<TToken, T>
    {
        private readonly Parser<TToken, T> parser;
        private readonly Func<T, string> formatter;

        internal TraceParser(Parser<TToken, T> parser, Func<T, string> formatter)
        {
            this.parser = parser;
            this.formatter = formatter;
        }

        public override ParseResult<TToken, T> Execute(TokenSequence<TToken> input)
        {
            return parser.Execute(input)
                         .Match<ParseResult<TToken, T>>(
                             ok =>
                             {
                                 Console.WriteLine("Success {0} {1}, Remaining: {2}", formatter(ok.Value), ok.Expectation, ok.Remaining);
                                 return ok;
                             },
                             err =>
                             {
                                 Console.WriteLine("Error {0} {1}, Remaining: {2}", err.Message, err.Expectation, err.Remaining);
                                 return err;
                             });
        }
    }
}
