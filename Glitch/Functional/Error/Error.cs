using System.Runtime.ExceptionServices;

namespace Glitch.Functional
{
    public abstract class Error : IEquatable<Error>
    {
        public static readonly EmptyError Empty = EmptyError.Value;

        protected static readonly StringComparer MessageComparer = StringComparer.CurrentCultureIgnoreCase;

        public abstract string Message { get; }

        public abstract Option<Error> Inner { get; }

        public static Error New(string message) => new ApplicationError(message);

        public static Error New(Exception exception) => new ExceptionError(exception);

        public static Error New(IEnumerable<Error> errors) => new AggregateError(errors);

        public static Error New(params Error[] errors) => new AggregateError(errors);

        public abstract Exception AsException();

        public bool Is<T>() where T : Error => this is T;

        public bool IsException() => IsException<Exception>();

        public abstract bool IsException<T>() where T : Exception;

        public virtual Error Combine(Error other)
        {
            var errors = Iterate().Concat(other.Iterate());

            return errors.Count() > 1 
                 ? new AggregateError(errors) 
                 : errors.Single();
        }

        public virtual bool Equals(Error? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return MessageComparer.Equals(Message, other.Message);
        }

        public sealed override bool Equals(object? obj) => Equals(obj as Error);

        public override int GetHashCode() 
            => HashCode.Combine(GetType().GetHashCode(), MessageComparer.GetHashCode(Message));

        public override string ToString() => Message;

        public void Throw() => ExceptionDispatchInfo.Capture(AsException()).Throw();

        public T Throw<T>()
        {
            Throw();
            return default!;
        }

        public virtual IEnumerable<Error> Iterate()
        {
            yield return this;
        }

        public virtual IEnumerable<Error> IterateRecursive()
        {
            Option<Error> current = this;

            while (current.IsSome)
            {
                yield return current.Unwrap();
                current = current.AndThen(c => c.Inner);
            }
        }

        public static implicit operator Error(string message) => New(message);

        public static implicit operator Error(Exception exception) => New(exception);

        public static bool operator ==(Error? x, Error? y) => x is null ? y is null : x.Equals(y);

        public static bool operator !=(Error? x, Error? y) => !(x == y);

        public static Error operator +(Error? x, Error? y) => (y is null ? x : x?.Combine(y) ?? y) ?? Empty;
    }
}
