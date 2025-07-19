
namespace Glitch.Functional.Parsing
{
    public class ParseError
    {
        private const string DefaultMessage = "Parse failed for input";

        public ParseError() : this(DefaultMessage) { }

        public ParseError(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public static ParseError New(string message) => new(message);
    }
}
