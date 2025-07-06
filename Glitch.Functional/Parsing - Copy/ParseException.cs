namespace Glitch.Functional.Parsing
{
    public class ParseException : Exception
    {
        public ParseException(ParseError error)
            : base(error.Message)
        {
            Error = error;
        }

        public ParseException(string message) : base(message) 
        {
            Error = new ParseError(message);
        }

        public ParseException(string message, Exception inner) : base(message, inner) 
        {
            Error = new ParseError(message, inner);
        }

        public ParseError Error { get; }
    }
}
