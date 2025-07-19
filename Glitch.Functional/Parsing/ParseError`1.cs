
namespace Glitch.Functional.Parsing
{
    public record ParseError<TToken> : ParseError
    {
        public static readonly new ParseError<TToken> Empty = new(None, None, None);

        private Option<string> message;

        public ParseError() : this(None, None, None) { }

        public ParseError(string message)
            : this(Some(message), None, None) { }

        public ParseError(Expectation<TToken> expectation)
            : this(None, Some(expectation), None) { }

        public ParseError(Position position) : this(None, None, Some(position)) { }

        public ParseError(string message, Expectation<TToken> expectation)
            : this(Some(message), Some(expectation), None) { }

        public ParseError(string message, Position position)
            : this(Some(message), None, Some(position)) { }

        public ParseError(string message, Expectation<TToken> expectation, Position position)
            : this(Some(message), Some(expectation), Some(position)) { }

        // TODO Add support for codes
        private ParseError(Option<string> message, Option<Expectation<TToken>> expectation, Option<Position> position)
            : base(message, position)
        {
            this.message = message;
            Expectation = expectation;
        }

        public override string Message => message.OrElse(() => Expectation.ToString())
                                                 .IfNone(base.Message);

        public Option<Expectation<TToken>> Expectation { get; init; }

        public static implicit operator ParseError<TToken>(string message) => new(message);
    }
}
