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

        public virtual int Code { get; init; }

        public virtual string Message { get; init; }

        public virtual Option<Error> Inner { get; init; }
        
        public virtual bool IsException => false;

        public static Error New(string message) => New(0, message);

        public static Error New(int code, string message) => new ApplicationError(code, message);

        public static Error New<TCode>(TCode code, string message) where TCode : Enum => New(Convert.ToInt32(code), message);

        public static Error New(Exception exception) => new ExceptionError(exception);

        public static Error New(int code, Exception exception) => new ExceptionError(code, exception);

        public static Error New<TCode>(TCode code, Exception exception) => New(Convert.ToInt32(code), exception);

        public static Error New(params IEnumerable<Error> errors) 
            => errors.Match(just: Identity,
                            many: _ => new AggregateError(errors),
                            none: _ => Empty);

        public abstract Exception AsException();

        public virtual bool IsCode(int code) => Code == code;

        public virtual bool Is<T>() => this is T || AsException() is T;

        public virtual Error Add(Error other)
        {
            return Iterate().Concat(other.Iterate())
                .Match(just: Identity,
                       many: x => new AggregateError(x),
                       none: Empty);
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

        public static Error operator +(Error? x, Error? y) => (y is null ? x : x?.Add(y) ?? y) ?? Empty;
    }
}
