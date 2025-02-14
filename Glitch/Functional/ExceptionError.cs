namespace Glitch.Functional
{
    public class ExceptionError : Error
    {
        private readonly Exception exception;

        public ExceptionError(Exception exception)
        {
            this.exception = exception;
            Inner = Option.Maybe(exception.InnerException).Map(New);
        }

        public override string Message => exception.Message;

        public override Option<int> Code => exception.HResult;

        public override Option<Error> Inner { get; }

        public override Exception AsException() => exception;
    }
}
