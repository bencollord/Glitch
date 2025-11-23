using Glitch.Functional;
using Glitch.Functional;

namespace Glitch.Functional.Errors
{
    public record AggregateError : Error
    {
        private readonly IEnumerable<Error> errors;

        public AggregateError(IEnumerable<Error> errors)
            : base((int)GlobalErrorCode.Aggregate, string.Empty)
        {
            this.errors = errors;
        }

        public override int Code => errors.Any() ? (int)GlobalErrorCode.Aggregate : (int)GlobalErrorCode.None;

        public override string Message => string.Join(", ", errors.Select(e => e.Message));

        public override Option<Error> Inner => Option.None;

        public int Count => errors.Count();

        public override bool Is<T>()
            => errors.Any(e => e.Is<T>())
            || typeof(T).IsAssignableTo(typeof(AggregateException))
            || typeof(T).IsAssignableTo(typeof(AggregateError));

        public override bool IsCode(int code) => base.IsCode(code) || errors.Any(e => e.IsCode(code));

        public override Exception AsException() 
            => new AggregateException(errors.Select(e => e.AsException()));

        public override IEnumerable<Error> Iterate() => errors;

        public override IEnumerable<Error> IterateRecursive() 
            => errors.SelectMany(e => e.IterateRecursive());
    }
}
