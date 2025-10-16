using Glitch.Functional.Attributes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Glitch.Functional.Results
{
    public readonly struct OptionNone 
    {
        public static readonly OptionNone Value = new();
    }

    [Monad]
    public readonly partial struct Option<T> : IEquatable<Option<T>>
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

        public object Value => Match<object>(v => v!, _ => OptionNone.Value);

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
        /// Partially applies the value to a 2 arg function and
        /// returns an option of the resulting function.
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<Func<T2, TResult>> PartialSelect<T2, TResult>(Func<T, T2, TResult> map)
            => Select(map.Curry());

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
        public Option<TResult> And<TResult>(TResult other) => And(Option<TResult>.Some(other));

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

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> Choose<TResult>(Func<T, Option<TResult>> bindSome, Func<Option<TResult>> bindNone)
            => Match(bindSome, bindNone);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> Choose<TResult>(Func<T, Option<TResult>> bindSome, Func<Unit, Option<TResult>> bindNone)
            => Match(bindSome, bindNone);

        /// <summary>
        /// Returns the current <see cref="Option{T}"/> if it contains a value. 
        /// Otherwise returns the other <see cref="Option{T}">.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Or(Option<T> other) => IsSome ? this : other;

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
        public Option<(T, TOther)> Zip<TOther>(Option<TOther> other)
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
        /// Executes an impure action against the value if it exists.
        /// No op if none.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Do(Action<T> action)
        {
            if (IsSome)
            {
                action(value!);
            }

            return this;
        }

        /// <summary>
        /// Executes an impure action against the value if it exists.
        /// No op if none.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> Do(Func<T, Unit> action)
        {
            if (IsSome)
            {
                action(value!);
            }

            return this;
        }

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

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Match(Action<T> some, Action none) => Match(some.Return(), none.Return());

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Unit Match(Action<T> some, Action<Unit> none) => Match(some, () => none(default));

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
            // TODO Inconsistent behavior between this and result/expected, which throw if the cast fails.
            // The case for throwing is consistency with Linq, but these types already use a different casting
            // method to support user-defined conversion operators anyway and this method allows Linq expressions
            // like from TResult x in opt, which is very convenient.
        public Option<TResult> Cast<TResult>() 
            => AndThen(v => DynamicCast<TResult>.Try(v).OkayOrNone());

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<TResult> OfType<TResult>()
            where TResult : T
            => Where(val => val is TResult).Select(val => (TResult)val!);

        /// <summary>
        /// Returns the wrapped value if it exists. Otherwise throws an exception.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Unwrap() => IsSome ? value! : throw new InvalidOperationException("Attempted to unwrap an empty option");

        /// <summary>
        /// Returns the wrapped value if it exists, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T UnwrapOr(T fallback) => Match(val => val, () => fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T UnwrapOrElse(Func<T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T UnwrapOrElse(Func<Unit, T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the default value
        /// of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? UnwrapOrDefault() => IsSome ? value : default;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryUnwrap(out T result)
        {
            result = value!;
            return IsSome;
        }

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
        /// Executes an impure action if empty.
        /// No op if some.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> IfNone(Action action)
        {
            if (IsNone)
            {
                action();
            }

            return this;
        }

        /// <summary>
        /// Executes an impure action if empty.
        /// No op if some.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Option<T> IfNone(Action<Unit> action) => IfNone(() => action(Unit.Value));

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the default value
        /// of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? DefaultIfNone() => IsSome ? value : default;

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the fallback.
        /// </summary>
        /// <remarks>
        /// Functions like <see cref="IfNone(T)" />, but allows null for the fallback.
        /// </remarks>
        /// <param name="fallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? DefaultIfNone(Func<T?> fallback) => IsSome ? value : fallback();

        /// <summary>
        /// Yields a singleton sequence if some, otherwise an empty sequence.
        /// </summary>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> Iterate()
        {
            if (IsSome)
            {
                yield return value!;
            }
        }

        /// <summary>
        /// Wraps the value in a <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Expected{T}" /> containing 
        /// the provided error.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> OkayOr(Error error) => IsSome ? Expected.Okay(value!) : Expected.Fail<T>(error);

        /// <summary>
        /// Wraps the value in a <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Expected{T}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <param name="error"></param>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> OkayOrElse(Func<Error> function) => IsSome ? Expected.Okay(value!) : Expected.Fail<T>(function());

        /// <summary>
        /// Wraps the value in a <see cref="Expected{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Expected{T}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <param name="error"></param>
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Expected<T> OkayOrElse(Func<Unit, Error> function) => IsSome ? Expected.Okay(value!) : Expected.Fail<T>(function(default));

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

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Option<T> option) => option.IsSome;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator false(Option<T> option) => option.IsNone;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> operator &(Option<T> x, Option<T> y) => x.And(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> operator ^(Option<T> x, Option<T> y) => x.Xor(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<T> operator |(Option<T> x, Option<T> y) => x.Or(y);
        
        // Coalescing operators
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T operator |(Option<T> x, T y) => x.IfNone(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<T> operator |(Option<T> x, Failure<Error> y) => x.OkayOr(y.Error);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Option<T> option) => option.IsSome;

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Option<T>(T? value) => Maybe(value);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Option<T>(OptionNone _) => new();

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Option<T> x, Option<T> y) => x.Equals(y);

        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Option<T> x, Option<T> y) => !(x == y);
    }
}
