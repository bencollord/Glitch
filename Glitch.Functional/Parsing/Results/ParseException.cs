namespace Glitch.Functional.Parsing.Results;

public class ParseException : Exception
{
    public ParseException(string message) : base(message) 
    {
    }

    public ParseException(string message, Exception inner) : base(message, inner) 
    {
    }

    internal static ParseException FromError<TToken, T>(ParseError<TToken, T> error) => new(error.ToString());
}
