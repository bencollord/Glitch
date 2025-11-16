namespace Glitch.Functional.Errors
{
    public class ErrorException : Exception
    {
        public ErrorException(Error error)
            : base(error.Message, error.Inner.Select(e => e.AsException()).UnwrapOrDefault())
        {
            Error = error;
        }

        public Error Error { get; }
    }
}
