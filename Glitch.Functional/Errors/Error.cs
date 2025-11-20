using Glitch.Functional.Core;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace Glitch.Functional.Errors
{
    public abstract partial record Error : IEquatable<Error>
    {
        public static readonly EmptyError Empty = EmptyError.Value;

        protected static readonly StringComparer MessageComparer = StringComparer.CurrentCultureIgnoreCase;

        protected Error() : this(null, null) { }

        protected Error(int? code, string? message)
        {
            Code = code ?? (int)GlobalErrorCode.Unspecified;
            Message = message ?? $"Error: {GetType()}";
        }

        public virtual int Code { get; }

        public virtual string Message { get; init; }

        public virtual Option<Error> Inner { get; init; }
        
        public virtual bool IsException => false;

        public static Error New(string message) => New(0, message);

        public static Error New(int code, string message) => new ApplicationError(code, message);

        public static Error New<TCode>(TCode code, string message) where TCode : Enum => New(Convert.ToInt32(code), message);

        public static Error New(Exception exception) => new ExceptionError(exception);

        public static Error New(int code, Exception exception) => new ExceptionError(code, exception);

        public static Error New<TCode>(TCode code, Exception exception) where TCode : Enum => New(Convert.ToInt32(code), exception);

        public static Error New(params IEnumerable<Error> errors)
            => errors.ToArray() switch
            {
                { Length: 1 } => errors.Single(),
                { Length: > 1 } => new AggregateError(errors),
                _ => Empty
            };

        /// <summary>
        /// Converts the <paramref name="value"/> into an <see cref="Error"/>
        /// based on the type of <typeparamref name="T"/> using the following rules:
        /// 
        /// <list type="table">
        ///   <item>
        ///     <term><see cref="Error"/></term>
        ///     <description>The error as-is</description>
        ///   </item>
        /// 
        ///   <item>
        ///     <term><see cref="Exception"/></term>
        ///     <description>The exception wrapped in an <see cref="ExceptionError"/></description>
        ///   </item>
        ///   
        ///   <item>
        ///     <term>A <see langword="string"/></term>
        ///     <description>An <see cref="ApplicationError"/> with the string as its message.</description>
        ///   </item>
        ///   
        ///   <item>
        ///     <term><see cref="IEnumerable{Error}">Multiple errors</see></term>
        ///     <description>An <see cref="AggregateError"/>.</description>
        ///   </item>
        ///   
        ///   <item>
        ///     <term><see langword="null"/> or <see cref="Unit"/></term>
        ///     <description>An <see cref="EmptyError"/>.</description>
        ///   </item>
        ///   
        ///   <item>
        ///     <term>Anything else</term>
        ///     <description>An <see cref="Unexpected{T}"/> instance.</description>
        ///   </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// This is an experimental API that may be removed. It does not account for the error
        /// potentially being wrapped in a monad like an <see cref="Option{T}"/> or <see cref="Result{,}"/>.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Error From<T>(T value)
        {
            return value switch
            {
                Error err               => err,
                Exception ex            => new ExceptionError(ex),
                string msg              => new ApplicationError(msg),
                IEnumerable<Error> errs => new AggregateError(errs),
                null or Unit            => Empty,
                var val                 => new Unexpected<T>(val)
            };
        }

        public virtual Exception AsException() => new ErrorException(this);

        public virtual bool IsCode(int code) => Code == code;

        public virtual bool Is<T>() => this is T || AsException() is T;

        public virtual bool Is<T>([NotNullWhen(true)] out T? @as)
        {
            if (this is T derived)
            {
                @as = derived;
                return true;
            }

            if (AsException() is T ex)
            {
                @as = ex;
                return true;
            }

            @as = default;
            return false;
        }

        public virtual Error Add(Error other)
        {
            return New(Iterate().Concat(other.Iterate()));
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
