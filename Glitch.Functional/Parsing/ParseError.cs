
namespace Glitch.Functional.Parsing
{
    public record ParseError
    {
        public static readonly ParseError Empty = new(None, None);

        private const string DefaultMessage = "Parse error";

        public ParseError() : this(None, None) { }

        public ParseError(string message)
            : this(Some(message), None) { }

        public ParseError(Position position) : this(None, Some(position)) { }

        public ParseError(string message, Position position)
            : this(Some(message), Some(position)) { }

        protected ParseError(Option<string> message, Option<Position> position)
        {
            Message = message.IfNone(DefaultMessage);
            Position = position;
        }

        public virtual string Message { get; }

        public Option<Position> Position { get; }

        public static ParseError<TToken> Unexpected<TToken>(TToken unexpected) => new(new Expectation<TToken>(unexpected));

        public static ParseError<TToken> Expected<TToken>(params IEnumerable<TToken> expected) => new(new Expectation<TToken>(expected));

        public override string ToString()
        {
            return Position.Match(
                ifSome: pos => $"{Message} {pos}", 
                ifNone: Message);
        }
    }
}
