namespace Glitch.Functional
{
    public class ApplicationError : Error
    {
        public ApplicationError(string message)
            : this(message, None) { }

        public ApplicationError(string message, Error inner)
            : this(message, Some(inner)) { }

        public ApplicationError(string message, Option<Error> inner)
        {
            Message = message;
            Inner = inner;
        }

        public override string Message { get;  }

        public override Option<Error> Inner { get; }

        public override bool IsException<T>() => false;

        public override Exception AsException() 
            => new ApplicationErrorException(this);

        public static implicit operator ApplicationError(string message) => new(message);
    }
}
