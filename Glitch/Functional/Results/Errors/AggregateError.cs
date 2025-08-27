namespace Glitch.Functional.Results
{
    public record AggregateError : Error
    {
        private readonly IEnumerable<Error> errors;

        public AggregateError(IEnumerable<Error> errors)
            : base(ErrorCodes.Aggregate, string.Empty)
        {
            this.errors = errors;
        }

        public override int Code => errors.Any() ? ErrorCodes.Aggregate : ErrorCodes.None;

        public override string Message => string.Join(", ", errors.Select(e => e.Message));

        public override Option<Error> Inner => Option.None;

        public override bool IsException<T>() 
            => errors.Any(e => e.IsException<T>()) || typeof(T).Equals(typeof(AggregateException));

        public override bool IsCode(int code) => base.IsCode(code) || errors.Any(e => e.IsCode(code));

        public override bool IsError<T>() 
            => errors.Any(e => e.IsError<T>() || typeof(T).IsAssignableTo(typeof(AggregateError)));

        public override Exception AsException() 
            => new AggregateException(errors.Select(e => e.AsException()));

        public override IEnumerable<Error> Iterate() => errors;

        public override IEnumerable<Error> IterateRecursive() 
            => errors.SelectMany(e => e.IterateRecursive());

        private IEnumerable<Error> ErrorsInOrder()
            => errors.OrderBy(e => e.Message);
    }
}
