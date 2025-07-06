
namespace Glitch.Functional.Parsing
{
    public class ParseError : ApplicationError
    {
        private const string DefaultMessage = "Parse failed for input";

        public ParseError() : this(DefaultMessage) { }

        public ParseError(string message) : base(message)
        {
        }

        public ParseError(string message, Error inner) : base(message, inner)
        {
        }

        public override Exception AsException() => new ParseException(this);
    }
}
