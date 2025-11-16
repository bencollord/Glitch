namespace Glitch.Functional.Errors
{
    public class ErrorException : Exception
    {
        public ErrorException(Error error)
            : base(error.Message, error.Inner.Select(e => e.AsException()).DefaultIfNone())
        {
            Error = error;
        }

        public Error Error { get; }
    }
}
