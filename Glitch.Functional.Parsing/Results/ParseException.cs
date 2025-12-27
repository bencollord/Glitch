namespace Glitch.Functional.Parsing.Results;

public class ParseException : Exception
{
    public ParseException() : this(ParseError.Unknown) 
    {
    }

    public ParseException(string message, Exception? innerException = null) : this(ParseError.New(message), innerException)
    {
    }

    public ParseException(ParseError error, Exception? innerException = null) : base(error.Message, innerException)
    {
        Error = error;
    }

    public ParseError Error { get; }

    public override string Message => Error.Message;
}
