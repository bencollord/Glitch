using System.Runtime.ExceptionServices;

namespace Glitch.Functional.Results
{
    public abstract record Error : IEquatable<Error>
    {
        public static readonly EmptyError Empty = EmptyError.Value;

        protected static readonly StringComparer MessageComparer = StringComparer.CurrentCultureIgnoreCase;

        protected Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public virtual int Code { get; }

        public virtual string Message { get; }

        public virtual Option<Error> Inner { get; }

        public static Error New(string message) => New(0, message);

        public static Error New(int code, string message) => new ApplicationError(code, message);

        public static Error New(Exception exception) => new ExceptionError(exception);

        public static Error New(int code, Exception exception) => new ExceptionError(code, exception);

        public static Error New(params IEnumerable<Error> errors) 
            => errors.Match(just: Identity,
                            many: _ => new AggregateError(errors),
                            none: _ => Empty);

        public abstract Exception AsException();

        public virtual bool IsCode(int code) => Code == code;

        public virtual bool IsError<T>() where T : Error => this is T;

        public bool IsException() => IsException<Exception>();

        public virtual bool IsException<T>() where T : Exception => false;

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

            return Code != 0 ? Code == other.Code : MessageComparer.Equals(Message, other.Message);
        }

        public override int GetHashCode()
            => Code != 0 ? Code.GetHashCode() : MessageComparer.GetHashCode(Message);

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
        public static implicit operator Error((int code, string message) pair) => New(pair.code, pair.message);

        public static implicit operator Error(Exception exception) => New(exception);

        public static Error operator +(Error? x, Error? y) => (y is null ? x : x?.Combine(y) ?? y) ?? Empty;
    }
}
