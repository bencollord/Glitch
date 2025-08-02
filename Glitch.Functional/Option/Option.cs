using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    public readonly struct OptionNone 
    {
        public static readonly OptionNone Value = new();
    }

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

        public bool IsSomeAnd(Func<T, bool> predicate)
            => Map(predicate).IfNone(false);

        public bool IsNoneOr(Func<T, bool> predicate)
            => Map(predicate).IfNone(true);

        /// <summary>
        /// If the <see cref="Option{T}"/> has a value, applies the provided
        /// to the value and returns it wrapped in a new <see cref="Option{TResult}" />. 
        /// Otherwise returns a new empty option.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Option<TResult> Map<TResult>(Func<T, TResult> map)
            => IsSome ? new Option<TResult>(map(value!)) : new Option<TResult>();

        /// <summary>
        /// Partially applies the value to a 2 arg function and
        /// returns an option of the resulting function.
        /// </summary>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Option<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

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
        /// <param name="bind"></param>
        /// <returns></returns>
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
        public Option<TResult> AndThen<TElement, TResult>(Func<T, Option<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public Option<TResult> Choose<TResult>(Func<T, Option<TResult>> bindSome, Func<Option<TResult>> bindNone)
            => Match(bindSome, bindNone);

        public Option<TResult> Choose<TResult>(Func<T, Option<TResult>> bindSome, Func<Unit, Option<TResult>> bindNone)
            => Match(bindSome, bindNone);

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
        /// Returns the current <see cref="Option{T}"/> if it contains a value.
        /// Otherwise returns the result of the provided function.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Option<T> OrElse(Func<Unit, Option<T>> other)
            => IsSome ? this : other(default);

        /// <summary>
        /// If this <see cref="Option{T}"/> contains a value, checks
        /// the value against the provided <paramref name="predicate"/>
        /// and returns an empty <see cref="Option{T}" /> if it returns false.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Option<T> Filter(Func<T, bool> predicate)
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
        public Option<TResult> Zip<TOther, TResult>(Option<TOther> other, Func<T, TOther, TResult> zipper)
            => AndThen(x => other.Map(y => zipper(x, y)));

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
        /// Maps using the provided function if Some.
        /// Otherwise, returns the fallback value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> some, TResult none)
            => Map(some).IfNone(none);

        public Unit Match(Action<T> some, Action none) => Match(some.Return(), none.Return());

        public Unit Match(Action<T> some, Action<Unit> none) => Match(some, () => none(default));

        /// <summary>
        /// If this <see cref="Option{T}"/> contains a value, returns the result of the first function 
        /// applied to the wrapped value.Otherwise, returns the result of the second function.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="some"></param>
        /// <param name="none"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
            => IsSome ? some(value!) : none();

        public TResult Match<TResult>(Func<T, TResult> some, Func<Unit, TResult> none)
            => Match(some, () => none(default));

        /// <summary>
        /// If the current <see cref="Option{T}"/> contains a value, casts it to 
        /// <typeparamref name="TResult"/>. Otherwise, returns an empty <see cref="Option{TResult}"/>.
        /// If the cast fails, returns and empty <see cref="Option{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Option<TResult> Cast<TResult>()
            => AndThen(v => DynamicCast<TResult>.TryFrom(v).Run().OkayOrNone());

        public Option<TResult> As<TResult>()
            where TResult : class
            => AndThen(v => FN.Maybe(v as TResult));

        public Option<TResult> OfType<TResult>()
            where TResult : T
            => Filter(val => val is TResult).Map(val => (TResult)val!);

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
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T UnwrapOrElse(Func<Unit, T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the default value
        /// of <typeparamref name="T"/>.
        /// </summary>
        /// <returns></returns>
        public T? UnwrapOrDefault() => IsSome ? value : default;

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
        public T IfNone(T fallback) => Match(val => val, () => fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfNone(Func<T> fallback) => Match(val => val, fallback);

        /// <summary>
        /// Returns the wrapped value if exists. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfNone(Func<Unit, T> fallback) => Match(val => val, fallback);

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
        /// Executes an impure action if empty.
        /// No op if some.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Option<T> IfNone(Action<Unit> action) => IfNone(() => action(Unit.Value));

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

        public IfSomeFluent<TResult> IfSome<TResult>(TResult value) => new(this, _ => value);

        public IfSomeFluent<TResult> IfSome<TResult>(Func<T, TResult> map) => new(this, map);

        public IfSomeActionFluent IfSome<TResult>(Func<T, Unit> action) => new(this, action);

        public IfSomeActionFluent IfSome(Action<T> action) => new(this, action);

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
        public Result<T> OkayOr(Error error) => IsSome ? Okay(value!) : Fail(error);

        /// <summary>
        /// Wraps the value in a <see cref="Result{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Result{T}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <param name="error"></param>
        public Result<T> OkayOrElse(Func<Error> function) => IsSome ? Okay(value!) : Fail(function());

        /// <summary>
        /// Wraps the value in a <see cref="Result{T}" /> if it exists,
        /// otherwise returns an errored <see cref="Result{T}" /> containing 
        /// the result of the provided error function.
        /// </summary>
        /// <param name="error"></param>
        public Result<T> OkayOrElse(Func<Unit, Error> function) => IsSome ? Okay(value!) : Fail(function(default));

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

        public static bool operator true(Option<T> option) => option.IsSome;

        public static bool operator false(Option<T> option) => option.IsNone;

        public static Option<T> operator &(Option<T> x, Option<T> y) => x.And(y);

        public static Option<T> operator ^(Option<T> x, Option<T> y) => x.Xor(y);

        public static Option<T> operator |(Option<T> x, Option<T> y) => x.Or(y);
        
        // Coalescing operators
        public static T operator |(Option<T> x, T y) => x.IfNone(y);

        public static Result<T> operator |(Option<T> x, Error y) => x.OkayOr(y);

        public static implicit operator bool(Option<T> option) => option.IsSome;

        public static implicit operator Option<T>(T? value) => Maybe(value);

        public static implicit operator Option<T>(OptionNone _) => new();

        public static bool operator ==(Option<T> x, Option<T> y) => x.Equals(y);

        public static bool operator !=(Option<T> x, Option<T> y) => !(x == y);

        public class IfSomeFluent<TResult>
        {
            private readonly Option<T> option;
            private readonly Func<T, TResult> ifSome;

            internal IfSomeFluent(Option<T> option, Func<T, TResult> ifSome)
            {
                this.option = option;
                this.ifSome = ifSome;
            }

            public TResult IfNone(TResult ifNone) => option.Match(ifSome, ifNone);

            public TResult IfNone(Func<TResult> ifNone) => option.Match(ifSome, ifNone);

            public TResult IfNone(Func<Unit, TResult> ifNone) => option.Match(ifSome, ifNone);

            public TResult? DefaultIfNone() => default;

            public Option<TResult> OtherwiseContinue() => option.Map(ifSome);
        }

        public class IfSomeActionFluent
        {
            private readonly Option<T> option;
            private readonly Action<T> ifSome;

            internal IfSomeActionFluent(Option<T> option, Func<T, Unit> ifSome)
                : this(option, new Action<T>(t => ifSome(t))) { }

            internal IfSomeActionFluent(Option<T> option, Action<T> ifSome)
            {
                this.option = option;
                this.ifSome = ifSome;
            }

            public IfSomeActionFluent Then(Action<T> action) => new(option, ifSome + action);

            public IfSomeActionFluent Then(Func<T, Unit> action) => Then(new Action<T>(t => action(t)));

            public Unit OtherwiseDoNothing() => Otherwise(_ => { /* Nop */ });

            public Unit Otherwise(Action<Unit> ifNone) => Otherwise(() => ifNone(default));

            public Unit Otherwise(Action ifNone)
            {
                if (option.IsSome)
                {
                    ifSome(option.value!);
                }
                else
                {
                    ifNone();
                }

                return End;
            }
        }
    }
}
