namespace Glitch.Functional
{
    public class ApplicationError : Error
    {
        public ApplicationError(string message)
            : this(message, Option.None) { }

        public ApplicationError(string message, Error inner)
            : this(message, Option.Some(inner)) { }

        public ApplicationError(string message, Option<Error> inner)
        {
            Message = message;
            Inner = inner;
        }

        public override string Message { get;  }

        public override Option<Error> Inner { get; }

        public override Exception AsException() 
            => new ApplicationErrorException(this);
    }
}
