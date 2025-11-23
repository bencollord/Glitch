using Glitch.Functional;

namespace Glitch.Functional.Errors
{
    using static Option;

    public record ApplicationError : Error
    {
        private const string DefaultMessage = "An unknown error occurred not related to an exception";

        public ApplicationError(string message)
            : this(None, message, None) { }

        public ApplicationError(int code, string message)
            : this(code, message, None) { }

        public ApplicationError(string message, Error inner)
            : this(None, message, inner) { }

        public ApplicationError(int code, string message, Error inner)
            : this(code, message, Some(inner)) { }

        public ApplicationError(string message, Option<Error> inner)
            : this(None, message, inner) { }

        public ApplicationError(int code, string message, Option<Error> inner)
            : this(Some(code), Some(message), inner)
        {
            Inner = inner;
        }

        protected ApplicationError(Option<int> code, Option<string> message, Option<Error> inner)
            : base(code.IfNone(0), message.IfNone(DefaultMessage))
        {
            Inner = inner;
        }

        public override Option<Error> Inner { get; init; }

        public override bool Is<T>() => false;

        public override Exception AsException() 
            => new ErrorException(this);

        public static implicit operator ApplicationError(string message) => new(0, message);
    }
}
