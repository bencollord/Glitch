namespace Glitch.Functional
{
    public class ApplicationError : Error
    {
        public ApplicationError(string message)
            : this(Option.None, message, Option.None) { }

        public ApplicationError(string message, Error inner)
            : this(Option.None, message, inner) { }

        public ApplicationError(int code, string message)
            : this(code, message, Option.None) { }

        public ApplicationError(int code, string message, Error inner)
            : this(Option.Some(code), message, Option.Some(inner)) { }

        private ApplicationError(Option<int> code, string message, Option<Error> inner)
        {
            Code = code;
            Message = message;
            Inner = inner;
        }

        public override string Message { get;  }

        public override Option<int> Code { get; }

        public override Option<Error> Inner { get; }

        public override Exception AsException() 
            => new ApplicationErrorException(this);
    }
}
