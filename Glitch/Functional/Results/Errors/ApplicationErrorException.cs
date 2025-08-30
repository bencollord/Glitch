namespace Glitch.Functional.Results
{
    public class ApplicationErrorException : Exception
    {
        public ApplicationErrorException(ApplicationError error)
            : base(error.Message, error.Inner.Select(e => e.AsException()).DefaultIfNone())
        {
            Error = error;
        }

        public ApplicationError Error { get; }
    }
}
