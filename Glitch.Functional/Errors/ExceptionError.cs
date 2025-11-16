using Glitch.Functional.Core;

namespace Glitch.Functional.Errors
{
    public record ExceptionError : Error
    {
        private readonly Exception exception;

        public ExceptionError(Exception exception)
            : this(exception.HResult, exception) { }

        public ExceptionError(int code, Exception exception)
            : base(code, exception.Message)
        {
            this.exception = exception;
            Inner = Option.Maybe(exception.InnerException).Select(New);
        }

        public override string Message => exception.Message;

        public string StackTrace => exception.StackTrace ?? string.Empty;

        public Exception Exception => exception;

        public override Option<Error> Inner { get; init; }

        public override bool Is<T>() => exception is T;

        public override Exception AsException() => exception;
    }
}
