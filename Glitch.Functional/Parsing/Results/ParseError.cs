using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing.Results
{
    public record ParseError<TToken, T> : ParseResult<TToken, T>
    {
        private const string DefaultMessage = "Parse error";
        
        private Option<string> message;

        public ParseError(string message)
            : this(Some(message), Expectation<TToken>.None, TokenSequence<TToken>.Empty) { }

        public ParseError(Expectation<TToken> expectation)
            : this(expectation, TokenSequence<TToken>.Empty) { }

        public ParseError(Expectation<TToken> expectation, TokenSequence<TToken> remaining)
           : this(None, expectation, remaining) { }

        public ParseError(string message, Expectation<TToken> expectation)
            : this(Some(message), expectation, TokenSequence<TToken>.Empty) { }

        public ParseError(string message, TokenSequence<TToken> remaining)
            : this(message, Expectation<TToken>.None, remaining) { }

        public ParseError(Option<string> message, Expectation<TToken> expectation, TokenSequence<TToken> remaining)
            : base(remaining, expectation)
        {
            this.message = message;
            Expectation = expectation;
        }

        public string Message => message.IfNone(DefaultMessage);

        public override bool WasSuccessful => false;

        public override ParseResult<TToken, TResult> AndThen<TResult>(Func<T, ParseResult<TToken, TResult>> bind) => Cast<TResult>();

        public override ParseResult<TToken, T> OrElse(Func<ParseError<TToken, T>, ParseResult<TToken, T>> bind) => bind(this);

        public override ParseResult<TToken, TResult> Cast<TResult>() => new ParseError<TToken, TResult>(message, Expectation, Remaining);

        public override ParseResult<TToken, TResult> Map<TResult>(Func<T, TResult> _) => Cast<TResult>();

        public override TResult Match<TResult>(Func<ParseSuccess<TToken, T>, TResult> _, Func<ParseError<TToken, T>, TResult> ifFail) => ifFail(this);
        
        public override string ToString() => $"Error: {Message} - {Expectation}, Remaining: {Remaining}";

        public void Deconstruct(out string message, Expectation<TToken> expectation, out TokenSequence<TToken> remaining)
        {
            message = Message;
            expectation = Expectation;
            remaining = Remaining;
        }
    }
}
