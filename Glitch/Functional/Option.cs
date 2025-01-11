using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public readonly struct OptionNone
    {
        public static readonly OptionNone Value = new();
    }

    public static partial class Option
    {
        public static readonly OptionNone None = OptionNone.Value;

        public static Option<T> Some<T>(T value) 
            => value is not null ? new Option<T>(value) : throw new ArgumentNullException(nameof(value));

        public static Option<T> Optional<T>(T? value) => value != null ? Some(value) : None;

        public static Option<TResult> Apply<T, TResult>(this Option<Func<T, TResult>> function, Option<T> value)
            => value.Apply(function);
    }

    public readonly struct Option<T> : IEquatable<Option<T>>
    {
        private readonly T? value = default;
        private readonly bool hasValue = false;

        public Option(T? value)
        {
            this.value = value;
            hasValue = value != null;
        }

        public bool IsSome => hasValue;

        public bool IsNone => !hasValue;

        /// <summary>
        /// If the <see cref="Option{T}"/> has a value, applies the provided
        /// to the value and returns it wrapped in a new <see cref="Option{TResult}" />. 
        /// Otherwise returns a new empty option.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Option<TResult> Map<TResult>(Func<T, TResult> mapper)
            => IsSome ? new Option<TResult>(mapper(value!)) : new Option<TResult>();

        public Option<TResult> BiMap<TResult>(Func<T, TResult> ifSome, Func<TResult> ifNone)
            => Match(ifSome, ifNone);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns an empty <see cref="Option{TResult}" />.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Option<TResult> Apply<TResult>(Option<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if some. Otherwise, returns an empty <see cref="Option{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Option<TResult> And<TResult>(Option<TResult> other)
            => IsSome ? other : new Option<TResult>();

        /// <summary>
        /// If some, applies the function to the wrapped value. Otherwise, returns
        /// an empty <see cref="Option{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Option<TResult> AndThen<TResult>(Func<T, Option<TResult>> mapper)
            => IsSome ? mapper(value!) : new Option<TResult>();

        /// <summary>
        /// Returns the current <see cref="Option{T}"/> if it contains a value. 
        /// Otherwise returns the other <see cref="Option{T}">.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Option<T> Or(Option<T> other) => IsSome ? this : other;

        /// <summary>
        /// Returns this <see cref="Option{T}"/> if it has a value and the other does not.
        /// Returns <paramref name="other"/> if this option is empty and the other has a value.
        /// Otherwise, returns an empty <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Option<T> Xor(Option<T> other)
        {
            if (IsSome && other.IsNone)
            {
                return this;
            }

            if (IsNone && other.IsSome)
            {
                return other;
            }

            return new Option<T>();
        }

        /// <summary>
        /// Returns the current <see cref="Option{T}"/> if it contains a value.
        /// Otherwise returns the result of the provided function.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Option<T> OrElse(Func<Option<T>> other)
            => IsSome ? this : other();

        /// <summary>
        /// If this <see cref="Option{T}"/> contains a value, checks
        /// the value against the provided <paramref name="predicate"/>
        /// and returns an empty <see cref="Option{T}" /> if it returns false.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Option<T> Filter(Func<T, bool> predicate)
        {
            if (IsNone) return this;

            if (predicate(value!))
            {
                return this;
            }

            return Option.None;
        }

        /// <summary>
        /// Executes an impure action against the value if it exists.
        /// No op if none.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Option<T> Do(Action<T> action)
        {
            if (IsSome)
            {
                action(value!);
            }

            return this;
        }

        /// <summary>
        /// Executes an impure action if empty.
        /// No op if some.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Option<T> IfNone(Action action)
        {
            if (IsNone)
            {
                action();
            }

            return this;
        }

        /// <summary>
        /// If this <see cref="Option{T}"/> contains a value, returns the result of the first function 
        /// applied to the wrapped value.Otherwise, returns the result of the second function.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifSome"></param>
        /// <param name="ifNone"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> ifSome, Func<TResult> ifNone)
            => IsSome ? ifSome(value!) : ifNone();

        /// <summary>
        /// If the current <see cref="Option{T}"/> contains a value, casts it to 
        /// <typeparamref name="TResult"/>. Otherwise, returns an empty <see cref="Option{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Option<TResult> Cast<TResult>()
            => Map(val => (TResult)(dynamic)val!);

        /// <summary>
        /// Returns the wrapped value if it exists. Otherwise throws an exception.
        /// </summary>
        /// <returns></returns>
        public T Unwrap() => IsSome ? value! : throw new InvalidOperationException("Attempted to unwrap an empty option");

        /// <summary>
        /// Returns the wrapped value if it exists, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T UnwrapOr(T fallback) => Match(val => val, () => fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T UnwrapOrElse(Func<T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the default value
        /// of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        public T? UnwrapOrDefault() => IsSome ? value : default;

        /// <summary>
        /// Returns the wrapped value if it exists, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfNone(T fallback) => Match(val => val, () => fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfNone(Func<T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the default value
        /// of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        public T? DefaultIfNone() => IsSome ? value : default;

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the fallback.
        /// </summary>
        /// <remarks>
        /// Functions like <see cref="IfNone(T)" />, but allows null for the fallback.
        /// </remarks>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T? DefaultIfNone(T? fallback) => IsSome ? value : fallback;

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <remarks>
        /// Functions like <see cref="IfNone(Func{T})" />, but allows null for the fallback.
        /// </remarks>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T? DefaultIfNone(Func<T?> fallback) => IsSome ? value : fallback();

        /// <summary>
        /// Yields a singleton sequence if some, otherwise an empty sequence.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Iterate()
        {
            if (IsSome)
            {
                yield return value!;
            }
        }

        /// <summary>
        /// Wraps the value in a <see cref="Result{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Result{T}" /> containing 
        /// the provided error.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public Result<T> OkOr(Error error) => IsSome ? Result.Ok(value!) : Result.Fail<T>(error);

        /// <summary>
        /// Wraps the value in a <see cref="Result{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Result{T}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <param name="error"></param>
        public Result<T> OkOrElse(Func<Error> function) => IsSome ? Result.Ok(value!) : Result.Fail<T>(function());

        public bool Equals(Option<T> other)
        {
            if (IsNone) return other.IsNone;

            if (other.IsNone) return false;

            return value!.Equals(other.value);
        }

        public override bool Equals([NotNullWhen(true)] object? obj) 
            => obj is Option<T> other && Equals(other);

        public override int GetHashCode() 
            => IsSome ? value!.GetHashCode() : 0;

        public override string ToString() 
            => Match(v => $"Some({v})", () => "None");

        public static bool operator true(Option<T> option) => option.IsSome;

        public static bool operator false(Option<T> option) => option.IsNone;

        public static Option<T> operator &(Option<T> x, Option<T> y) => x.And(y);

        public static Option<T> operator |(Option<T> x, Option<T> y) => x.Or(y);

        public static Option<T> operator ^(Option<T> x, Option<T> y) => x.Xor(y);

        public static implicit operator bool(Option<T> option) => option.IsSome;

        public static implicit operator Option<T>(T? value) => Option.Optional(value);

        public static implicit operator Option<T>(OptionNone _) => new();

        public static bool operator ==(Option<T> x, Option<T> y) => x.Equals(y);

        public static bool operator !=(Option<T> x, Option<T> y) => !(x == y);
    }
}
