namespace Glitch.Functional.Results
{
    public class ApplicationErrorException : Exception
    {
        public ApplicationErrorException(ApplicationError error)
            : base(error.Message, error.Inner.Map(e => e.AsException()).DefaultIfNone())
        {
            Error = error;
        }

        public ApplicationError Error { get; }
    }
}
