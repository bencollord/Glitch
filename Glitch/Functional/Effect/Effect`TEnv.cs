
using Glitch.Functional.Attributes;

namespace Glitch.Functional
{
    /// <summary>
    /// Represents a monadic effect that takes an input and returns a result.
    /// 
    /// Basically like a <see cref="Effect{TOutput}"/> that takes an input parameter.
    /// </summary>
    /// <typeparam name="TEnv"></typeparam>
    /// <typeparam name="T"></typeparam>
    [Monad]
    public partial class Effect<TEnv, T>
    {
        private Func<TEnv, Result<T, Error>> thunk;

        public Effect(Func<TEnv, Result<T, Error>> thunk)
        {
            this.thunk = thunk;
        }

        public static Effect<TEnv, T> Return(T value) => new(_ => Result.Okay(value));

        public static Effect<TEnv, T> Fail(Error error) => new(_ => Result.Fail<T>(error));

        public static Effect<TEnv, T> Return(Result<T, Error> result) => new(_ => result);

        public static Effect<TEnv, T> Lift(Effect<T> effect) => new(_ => effect.Run());

        public static Effect<TEnv, T> Lift(Func<TEnv, Result<T, Error>> function) => new(function);

        public static Effect<TEnv, T> Lift(Func<TEnv, T> function) => new(i => Result.Okay(function(i)));

        public Effect<TNewInput, T> With<TNewInput>(Func<TNewInput, TEnv> map)
            => new(newInput => thunk(map(newInput)));

        /// <summary>
        /// Applies the supplied function to the wrapped value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Map<TResult>(Func<T, TResult> map)
            => new(i => thunk(i).Map(map));

        public Effect<TEnv, Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public Effect<TEnv, T> MapError(Func<Error, Error> map)
            => new(i => thunk(i).MapError(map));

        public Effect<TEnv, T> MapError<TError>(Func<TError, Error> map)
            where TError : Error
            => MapError(err => err is TError e ? map(e) : err);

        public Effect<TEnv, T> Catch<TException>(Func<TException, T> map)
            where TException : Exception
            => OrElse(err => err.IsException<TException>() ? map((TException)err.AsException()) : err);

        public Effect<TEnv, T> Catch<TException>(Func<TException, Error> map)
            where TException : Exception
            => MapError(err => err.IsException<TException>() ? map((TException)err.AsException()) : err);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both are successful.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Apply<TResult>(Effect<TEnv, Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Okay, otherwise returns the current error wrapped
        /// in a new <see cref="Effect{TEnv, TResult}">effect type.</see>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Then<TResult>(Effect<TEnv, TResult> other)
            => new(i =>
            {
                var result = thunk(i);

                return result.IsOkay ? other.thunk(i) : result.Cast<TResult>();
            });

        /// <summary>
        /// If Okay, returns the result of the provided <paramref name="projection"/> applied
        /// to the values of self and <paramref name="other"/>. Otherwise returns the current 
        /// error wrapped in a new <see cref="Effect{TEnv, TResult}">effect type.</see>
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Then<TOther, TResult>(Effect<TEnv, TOther> other, Func<T, TOther, TResult> projection)
            => new(i => thunk(i).AndThen(x => other.thunk(i).Map(projection.Curry()(x))));

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new <see cref="Effect{TResult}" /> type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> AndThen<TResult>(Func<T, Effect<TEnv, TResult>> bind)
            => new(i =>
            {
                var result = thunk(i);

                return result.AndThen(v => bind(v).thunk(i));
            });

        /// <summary>
        /// Implements a bind-map operation, similar to
        /// <see cref="Enumerable.SelectMany"/>.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> AndThen<TElement, TResult>(Func<T, Effect<TEnv, TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Convenience method that allows bind operations to work
        /// with <see cref="Result"/> types.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> AndThen<TResult>(Func<T, Result<TResult>> bind)
            => AndThen(e => Effect<TEnv, TResult>.Return(bind(e)));

        /// <summary>
        /// Implements a bind-map operation, similar to
        /// <see cref="Enumerable.SelectMany"/> for results.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> AndThen<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));

        public Effect<TEnv, TResult> Choose<TResult>(Func<T, Effect<TEnv, TResult>> okay, Func<Error, Effect<TEnv, TResult>> error)
            => new(i => thunk(i).Match(okay, error).Run(i));

        /// <summary>
        /// Convenience method that allows bind operations to work
        /// with <see cref="Effect"/> types.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> AndThen<TResult>(Func<T, Effect<TResult>> bind)
            => AndThen(e => Effect<TEnv, TResult>.Lift(bind(e)));

        /// <summary>
        /// Implements a bind-map operation, similar to
        /// <see cref="Enumerable.SelectMany"/> for results.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> AndThen<TElement, TResult>(Func<T, Effect<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Select(y => project(x, y)));


        public Effect<TEnv, T> Guard(Func<T, bool> predicate, Error error)
            => new(i => thunk(i).Guard(predicate, _ => error));

        public Effect<TEnv, T> Guard(Func<T, bool> predicate, Func<T, Error> error)
            => new(i => thunk(i).Guard(predicate, error));

        public Effect<TEnv, T> Guard(bool condition, Error error)
            => new(i => thunk(i).Guard(_ => condition, _ => error));

        public Effect<TEnv, T> Guard(bool condition, Func<T, Error> error)
            => new(i => thunk(i).Guard(_ => condition, error));

        /// <summary>
        /// Returns the current <see cref="Effect{T}"/> if Ok, otherwise returns other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TEnv, T> Or(Effect<TEnv, T> other)
            => new(i =>
            {
                var result = thunk(i);

                return result.IsOkay ? result : other.thunk(i);
            });

        /// <summary>
        /// Returns the current instance if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TEnv, T> OrElse(Func<Error, Effect<TEnv, T>> other)
            => new(i =>
            {
                var result = thunk(i);

                return result.OrElse(e => other(e).thunk(i));
            });

        public Effect<TEnv, T> Filter(Func<T, bool> predicate)
            => Guard(predicate, _ => Error.Empty);

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TEnv, T> Do(Action<T> action) => IfOkay(action); // TODO Not sure how I feel about just aliasing methods like this.

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TEnv, T> IfOkay(Action<T> action) => new(i => thunk(i).Do(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TEnv, T> IfFail(Action action) => IfFail(_ => action());

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TEnv, T> IfFail(Action<Error> action) => Match(Nop, action);

        /// <summary>
        /// Casts the wrapped value to <typeparamref name="TResult"/> if Ok,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Effect<TEnv, TResult> Cast<TResult>()
            => CastOrElse<TResult>(v => new InvalidCastException($"Cannot cast a value of type {v!.GetType()} to {typeof(TResult)}"));

        public Effect<TEnv, TResult> CastOr<TResult>(Error error)
            => CastOrElse<TResult>(_ => error);

        public Effect<TEnv, TResult> CastOrElse<TResult>(Func<T, Error> error)
            => from val in this
               let cast = Effect<TEnv, TResult>.Lift(_ => DynamicCast<T, TResult>(val))
               let err = Effect<TEnv, TResult>.Fail(error(val))
               from res in cast | err
               select res;

        /// <summary>
        /// Combines another try into a try of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TEnv, (T, TOther)> Zip<TOther>(Effect<TEnv, TOther> other)
            => Zip(other, (x, y) => (x, y));

        /// <summary>
        /// Combines two tries using a provided function.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Zip<TOther, TResult>(Effect<TEnv, TOther> other, Func<T, TOther, TResult> zipper)
            => new(i => thunk(i).Zip(other.thunk(i), zipper));

        public Effect<T> Apply(TEnv input) => Effect<T>.Lift(() => Run(input));

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the provided fallback value wrapped in a new effect.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Match<TResult>(Func<T, TResult> okay, TResult error)
            => Match(okay, _ => error);

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function wrapped in a new effect.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Match<TResult>(Func<T, TResult> okay, Func<TResult> error)
            => Match(okay, _ = error());

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function wrapped in a new effect.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Effect<TEnv, TResult> Match<TResult>(Func<T, TResult> okay, Func<Error, TResult> error) 
            => new(input => Result.Okay(thunk(input).Match(okay, error)));

        /// <summary>
        /// Chooses one of two impure actions to run depending on the success or failure
        /// of the <see cref="Effect{TEnv, T}"/> and returns a new effect with the action
        /// applied.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="okay"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Effect<TEnv, T> Match(Action<T> okay, Action<Error> error)
            => new(input =>
            {
                var result = thunk(input);

                return result.Match(
                    okay: val =>
                    {
                        okay(val);
                        return result;
                    },
                    error: e =>
                    {
                        error(e);
                        return result;
                    });
            });

        /// <summary>
        /// Executes the provided function, catching any exception
        /// thrown and wrapping it in a <see cref="Result{T}"/>
        /// </summary>
        /// <returns></returns>
        public Result<T> Run(TEnv input)
        {
            try
            {
                return thunk(input);
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex);
            }
        }

        public static implicit operator Effect<TEnv, T>(Effect<T> effect) => effect.WithInput<TEnv>();

        public static implicit operator Effect<TEnv, T>(Result<T> result) => new(_ => result);

        public static implicit operator Effect<TEnv, T>(Result<T, Error> result) => new(_ => result);

        public static implicit operator Effect<TEnv, T>(T value) => Return(value);

        public static implicit operator Effect<TEnv, T>(Error error) => Fail(error);

        public static Effect<TEnv, T> operator |(Effect<TEnv, T> x, Effect<TEnv, T> y) => x.Or(y);

        public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<TEnv, T> y)
            => x.Then(y);

        public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<T> y)
            => x.AndThen(_ => Lift(y));

        public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<TEnv, Unit> y)
            => x.AndThen(v => y.Map(_ => v));

        public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Effect<Unit> y)
            => x.AndThen(v => Lift(y.Select(_ => v)));


        public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Func<TEnv, Result<T, Error>> y)
            => new(i =>
            {
                _ = x.thunk(i);
                return y(i);
            });

        public static Effect<TEnv, T> operator >>(Effect<TEnv, T> x, Func<T> y)
            => new(i =>
            {
                _ = x.thunk(i);
                return Result.Okay(y());
            });
    }
}
