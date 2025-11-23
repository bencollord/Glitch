using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    /// <summary>
    /// Represents a value that may or may not exist and
    /// provide monadic operations. Effectively a <see cref="Result{T, E}"/>
    /// where the failure case is <see cref="Option.None"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Monad]
    public readonly partial struct Option<T> : IEquatable<Option<T>>, IComparable<Option<T>>
    {
        public static readonly Option<T> None = new();

        public static Option<T> Some(T value)
            => value is not null ? new Option<T>(value) : throw new ArgumentNullException(nameof(value));

        public static Option<T> Maybe(T? value) => value != null ? Some(value) : None;

        private readonly T? value = default;
        private readonly bool hasValue = false;

        public Option(T? value)
        {
            this.value = value;
            hasValue = value != null;
        }

        public bool IsSome => hasValue;

        public bool IsNone => !hasValue;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSomeAnd(Func<T, bool> predicate)
            => Select(predicate).IfNone(false);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNoneOr(Func<T, bool> predicate)
            => Select(predicate).IfNone(true);

        /// <summary>
        /// If the <see cref="Option{T}"/> has a value, applies the provided
        /// to the value and returns it wrapped in a new <see cref="Option{TResult}" />. 
        /// Otherwise returns a new empty option.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> Select<TResult>(Func<T, TResult> map)
            => IsSome ? new Option<TResult>(map(value!)) : new Option<TResult>();

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both exist.
        /// Otherwise, returns an empty <see cref="Option{TResult}" />.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> Apply<TResult>(Option<Func<T, TResult>> function)
            => AndThen(v => function.Select(fn => fn(v)));

        /// <summary>
        /// Returns other if some. Otherwise, returns an empty <see cref="Option{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> And<TResult>(Option<TResult> other)
            => IsSome ? other : new Option<TResult>();

        /// <summary>
        /// If some, applies the function to the wrapped value. Otherwise, returns
        /// an empty <see cref="Option{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> AndThen<TResult>(Func<T, Option<TResult>> bind)
            => IsSome ? bind(value!) : new Option<TResult>();

        /// <summary>
        /// Applies the element selector to the wrapped value
        /// and then a projection over both the value and the result
        /// of the first function. A BindMap operation.
        /// 
        /// If the current <see cref="Option{T}"/> is none or the provided
        /// <paramref name="bind"/> function returns none, the result will be none.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> AndThen<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));

        /// <inheritdoc cref="AndThen{TElement, TResult}(Func{T, Option{TElement}}, Func{T, TElement, TResult})"/>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> SelectMany<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));

        /// <summary>
        /// Returns the current <see cref="Option{T}"/> if it contains a value. 
        /// Otherwise returns the other <see cref="Option{T}">.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Or(Option<T> other) => IsSome ? this : other;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> Or<E>(Result<T, E> other) => Match(Result.Okay<T, E>, other);

        /// <summary>
        /// Returns this <see cref="Option{T}"/> if it has a value and the other does not.
        /// Returns <paramref name="other"/> if this option is empty and the other has a value.
        /// Otherwise, returns an empty <see cref="Option{T}"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> OrElse(Func<Option<T>> other)
            => IsSome ? this : other();

        /// <summary>
        /// Returns the current <see cref="Option{T}"/> if it contains a value.
        /// Otherwise returns the result of the provided function.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> OrElse(Func<Unit, Option<T>> other)
            => IsSome ? this : other(default);

        /// <summary>
        /// If this <see cref="Option{T}"/> contains a value, checks
        /// the value against the provided <paramref name="predicate"/>
        /// and returns an empty <see cref="Option{T}" /> if it returns false.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Where(Func<T, bool> predicate)
        {
            if (IsSomeAnd(predicate))
            {
                return this;
            }

            return None;
        }

        /// <summary>
        /// Combines another option into an option of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<(T Left, TOther Right)> Zip<TOther>(Option<TOther> other)
            => Zip(other, (x, y) => (x, y));

        /// <summary>
        /// Combines two options using a provided function.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> Zip<TOther, TResult>(Option<TOther> other, Func<T, TOther, TResult> zipper)
            => AndThen(x => other.Select(y => zipper(x, y)));

        /// <summary>
        /// Maps using the provided function if Some.
        /// Otherwise, returns the fallback value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> some, TResult none)
            => Select(some).IfNone(none);

        /// <summary>
        /// If this <see cref="Option{T}"/> contains a value, returns the result of the first function 
        /// applied to the wrapped value.Otherwise, returns the result of the second function.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
            => IsSome ? some(value!) : none();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TResult Match<TResult>(Func<T, TResult> some, Func<Unit, TResult> none)
            => Match(some, () => none(default));

        /// <summary>
        /// If the current <see cref="Option{T}"/> contains a value, casts it to 
        /// <typeparamref name="TResult"/>. Otherwise, returns an empty <see cref="Option{TResult}"/>.
        /// If the cast fails, returns and empty <see cref="Option{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> CastOrNone<TResult>() 
            => AndThen(v => DynamicCast<TResult>.Try(v, out var r) ? Option.Some(r) : Option.None);

        /// <summary>
        /// Dynamically casts the contained value, if it exists, to <typeparamref name="TResult"/>.
        /// Casting is done such that any custom conversion operators will be used, but will throw
        /// an <see cref="InvalidCastException"/> on failure. Use <see cref="CastOrNone{TResult}"/> to get back 
        /// <see cref="Option.None"/> on failure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> Cast<TResult>()
            => Select(DynamicCast<TResult>.From);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> OfType<TResult>()
            => AndThen(v => v is TResult r ? Option.Some(r) : Option.None);

        /// <summary>
        /// Returns the wrapped value if it exists. Otherwise throws an exception.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Unwrap() => IsSome ? value! : throw new InvalidOperationException("Attempted to unwrap an empty option");

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the default value
        /// of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? UnwrapOrDefault() => IsSome ? value : default;

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the fallback.
        /// </summary>
        /// <remarks>
        /// Functions like <see cref="IfNone(T)" />, but allows null for the fallback.
        /// </remarks>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? UnwrapOrDefault(T? fallback) => IsSome ? value : fallback;

        /// <summary>
        /// Returns the wrapped value if it exists, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T IfNone(T fallback) => Match(val => val, () => fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T IfNone(Func<T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T IfNone(Func<Unit, T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Wraps the value in a <see cref="Result{T, E}" /> if it exists,
        /// otherwise returns an errored <see cref="Result{T, E}" /> containing 
        /// the provided error.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> OkayOr<E>(E error) => IsSome ? Result.Okay(value!) : Result.Fail(error);

        /// <summary>
        /// Wraps the value in a <see cref="Result{T, E}" /> if it exists,
        /// otherwise returns an errored <see cref="Result{T, E}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <param name="error"></param>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> OkayOrElse<E>(Func<E> function) => IsSome ? Result.Okay(value!) : Result.Fail(function());

        /// <summary>
        /// Wraps the value in a <see cref="Result{T, E}" /> if it exists,
        /// otherwise returns an errored <see cref="Result{T, E}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <param name="error"></param>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Result<T, E> OkayOrElse<E>(Func<Unit, E> function) => IsSome ? Result.Okay(value!) : Result.Fail(function(default));

        public bool Equals(Option<T> other)
        {
            if (IsNone) return other.IsNone;

            if (other.IsNone) return false;

            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is Option<T> other && Equals(other);

        public override int GetHashCode()
            => IsSome ? value!.GetHashCode() : 0;

        public override string ToString()
            => Match(v => $"Some({v})", () => "None");

        public int CompareTo(Option<T> other)
        {
            var self = this;

            return Zip(other)
                .AndThen(pair => pair.Left switch
                {
                    IComparable<T> c => Option<int>.Some(c.CompareTo(pair.Right)),
                    IComparable c => Option<int>.Some(c.CompareTo(pair.Right)),
                    _ => Option<int>.None
                })
                .IfNone(_ => self.IsSome.CompareTo(other.IsSome));
        }
    }
}
