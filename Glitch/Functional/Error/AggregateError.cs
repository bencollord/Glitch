namespace Glitch.Functional
{
    public class AggregateError : Error
    {
        private readonly IEnumerable<Error> errors;

        public AggregateError(IEnumerable<Error> errors)
            : base()
        {
            this.errors = errors;
        }

        public override string Message => errors.Select(e => e.Message).Join(", ");

        public override Option<Error> Inner => None;

        public override bool IsException<T>() => typeof(T).Equals(typeof(AggregateException));

        public override Exception AsException() 
            => new AggregateException(errors.Select(e => e.AsException()));

        public override IEnumerable<Error> Iterate() => errors;

        public override IEnumerable<Error> IterateRecursive() 
            => errors.SelectMany(e => e.IterateRecursive());

        public override bool Equals(Error? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            if (other is AggregateError aggregate)
            {
                return ErrorsInOrder()
                    .SequenceEqual(aggregate.ErrorsInOrder());
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var error in ErrorsInOrder())
            {
                hashCode.Add(error.GetHashCode());
            }

            return hashCode.ToHashCode();
        }

        private IEnumerable<Error> ErrorsInOrder()
            => errors.OrderBy(e => e.Message);
    }
}
