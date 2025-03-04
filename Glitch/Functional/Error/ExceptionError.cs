namespace Glitch.Functional
{
    public class ExceptionError : Error
    {
        private readonly Exception exception;

        public ExceptionError(Exception exception)
        {
            this.exception = exception;
            Inner = Maybe(exception.InnerException).Map(New);
        }

        public override string Message => exception.Message;

        public override Option<Error> Inner { get; }

        public override bool IsException<T>() => exception is T;

        public override Exception AsException() => exception;
    }
}
