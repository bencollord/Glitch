namespace Glitch.Functional.Results
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
            Inner = Option.Maybe(exception.InnerException).Map(New);
        }

        public override string Message => exception.Message;

        public string StackTrace => exception.StackTrace ?? string.Empty;

        public override Option<Error> Inner { get; }

        public override bool IsException<T>() => exception is T;

        public override Exception AsException() => exception;
    }
}
