namespace Glitch.Functional
{
    public class ApplicationError : Error
    {
        public ApplicationError(string message)
            : this(0, message) { }

        public ApplicationError(int code, string message)
            : this(code, message, FN.None) { }

        public ApplicationError(string message, Error inner)
            : this(0, message, inner) { }

        public ApplicationError(int code, string message, Error inner)
            : this(code, message, Some(inner)) { }

        public ApplicationError(string message, Option<Error> inner)
            : this(0, message, inner) { }

        public ApplicationError(int code, string message, Option<Error> inner)
            : base(code)
        {
            Message = message;
            Inner = inner;
        }

        public override string Message { get; }

        public override Option<Error> Inner { get; }

        public override bool IsException<T>() => false;

        public override Exception AsException() 
            => new ApplicationErrorException(this);

        public static implicit operator ApplicationError(string message) => new(0, message);
    }
}
