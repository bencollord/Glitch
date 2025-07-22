
namespace Glitch.Functional
{
    /// <summary>
    /// Represents a monadic effect that takes an input and returns a result.
    /// 
    /// Basically like a <see cref="Fallible{TOutput}"/> that takes an input parameter.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    public partial class Effect<TInput, TOutput>
    {
        private Func<TInput, Result<TOutput>> thunk;

        public Effect(Func<TInput, Result<TOutput>> thunk)
        {
            this.thunk = thunk;
        }

        public static Effect<TInput, TOutput> Okay(TOutput value) => new(_ => value);

        public static Effect<TInput, TOutput> Fail(Error error) => new(_ => error);

        public static Effect<TInput, TOutput> Lift(Result<TOutput> result) => new(_ => result);

        public static Effect<TInput, TOutput> New(Fallible<TOutput> fallible) => new(_ => fallible.Run());

        public static Effect<TInput, TOutput> New(Func<TInput, Result<TOutput>> function) => new(function);

        public static Effect<TInput, TOutput> New(Func<TInput, TOutput> function) => new(i => function(i));

        /// <summary>
        /// Applies the supplied function to the wrapped value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> Map<TResult>(Func<TOutput, TResult> map)
            => new(i => thunk(i).Map(map));

        public Effect<TInput, Func<T2, TResult>> PartialMap<T2, TResult>(Func<TOutput, T2, TResult> map)
            => Map(map.Curry());

        public Effect<TInput, TResult> MapOr<TResult>(Func<TOutput, TResult> map, Error ifFail)
            => new(i => thunk(i).MapOr(map, ifFail));

        public Effect<TInput, TResult> MapOrElse<TResult>(Func<TOutput, TResult> map, Func<Error, Error> ifFail)
            => new(i => thunk(i).MapOrElse(map, ifFail));

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public Effect<TInput, TOutput> MapError(Func<Error, Error> map)
            => new(i => thunk(i).MapError(map));

        public Effect<TInput, TOutput> MapError<TError>(Func<TError, Error> map)
            where TError : Error
            => MapError(err => err is TError e ? map(e) : err);

        public Effect<TInput, TOutput> Catch<TException>(Func<TException, TOutput> map)
            where TException : Exception
            => OrElse(err => err.IsException<TException>() ? map((TException)err.AsException()) : err);

        public Effect<TInput, TOutput> Catch<TException>(Func<TException, Error> map)
            where TException : Exception
            => MapError(err => err.IsException<TException>() ? map((TException)err.AsException()) : err);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both are successful.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> Apply<TResult>(Effect<TInput, Func<TOutput, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> And<TResult>(Effect<TInput, TResult> other)
            => new(i =>
            {
                var result = thunk(i);

                return result.IsOkay ? other.thunk(i) : result.Cast<TResult>();
            });

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new <see cref="Fallible{TResult}" /> type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> AndThen<TResult>(Func<TOutput, Effect<TInput, TResult>> bind)
            => new(i =>
            {
                var result = thunk(i);

                return result is Result.Success<TOutput>(var ok)
                     ? bind(ok).thunk(i)
                     : result.Cast<TResult>();
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
        public Effect<TInput, TResult> AndThen<TElement, TResult>(Func<TOutput, Effect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Convenience method that allows bind operations to work
        /// with <see cref="Result"/> types.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> AndThen<TResult>(Func<TOutput, Result<TResult>> bind)
            => AndThen(e => Effect<TInput, TResult>.Lift(bind(e)));

        /// <summary>
        /// Implements a bind-map operation, similar to
        /// <see cref="Enumerable.SelectMany"/> for results.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> AndThen<TElement, TResult>(Func<TOutput, Result<TElement>> bind, Func<TOutput, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public Effect<TInput, TResult> Choose<TResult>(Func<TOutput, Effect<TInput, TResult>> ifOkay, Func<Error, Effect<TInput, TResult>> ifFail)
            => new(i => thunk(i).Match(ifOkay, ifFail).Run(i));

        /// <summary>
        /// Convenience method that allows bind operations to work
        /// with <see cref="Fallible"/> types.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> AndThen<TResult>(Func<TOutput, Fallible<TResult>> bind)
            => AndThen(e => Effect<TInput, TResult>.New(bind(e)));

        /// <summary>
        /// Implements a bind-map operation, similar to
        /// <see cref="Enumerable.SelectMany"/> for results.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> AndThen<TElement, TResult>(Func<TOutput, Fallible<TElement>> bind, Func<TOutput, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));


        public Effect<TInput, TOutput> Guard(Func<TOutput, bool> predicate, Error error)
            => new(i => thunk(i).Guard(predicate, error));

        public Effect<TInput, TOutput> Guard(Func<TOutput, bool> predicate, Func<TOutput, Error> error)
            => new(i => thunk(i).Guard(predicate, error));

        public Effect<TInput, TOutput> Guard(bool condition, Error error)
            => new(i => thunk(i).Guard(condition, error));

        public Effect<TInput, TOutput> Guard(bool condition, Func<TOutput, Error> error)
            => new(i => thunk(i).Guard(condition, error));

        /// <summary>
        /// Returns the current <see cref="Fallible{T}"/> if Ok, otherwise returns other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TInput, TOutput> Or(Effect<TInput, TOutput> other)
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
        public Effect<TInput, TOutput> OrElse(Func<Error, Effect<TInput, TOutput>> other)
            => new(i =>
            {
                var result = thunk(i);

                return result is Result.Failure<TOutput> fail ? other(fail.Error).thunk(i) : result;
            });

        public Effect<TInput, TOutput> Filter(Func<TOutput, bool> predicate)
            => new(i => thunk(i).Filter(predicate));

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TInput, TOutput> Do(Action<TOutput> action) => IfOkay(action); // TODO Not sure how I feel about just aliasing methods like this.

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TInput, TOutput> IfOkay(Action<TOutput> action) => new(i => thunk(i).Do(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TInput, TOutput> IfFail(Action action) => new(i => thunk(i).IfFail(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Effect<TInput, TOutput> IfFail(Action<Error> action) => new(i => thunk(i).IfFail(action));

        /// <summary>
        /// Casts the wrapped value to <typeparamref name="TResult"/> if Ok,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Effect<TInput, TResult> Cast<TResult>()
            => CastOrElse<TResult>(v => new InvalidCastException($"Cannot cast a value of type {v!.GetType()} to {typeof(TResult)}"));

        public Effect<TInput, TResult> CastOr<TResult>(Error error)
            => MapOr(v => (TResult)(dynamic)v!, error);

        public Effect<TInput, TResult> CastOrElse<TResult>(Func<TOutput, Error> error)
            => from val in this
               let cast = Effect<TInput, TResult>.New(_ => DynamicCast<TOutput, TResult>(val))
               let err = Effect<TInput, TResult>.Lift(error(val))
               from res in cast | err
               select res;

        /// <summary>
        /// Combines another try into a try of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Effect<TInput, (TOutput, TOther)> Zip<TOther>(Effect<TInput, TOther> other)
            => Zip(other, (x, y) => (x, y));

        /// <summary>
        /// Combines two tries using a provided function.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public Effect<TInput, TResult> Zip<TOther, TResult>(Effect<TInput, TOther> other, Func<TOutput, TOther, TResult> zipper)
            => new(i => thunk(i).Zip(other.thunk(i), zipper));

        public Fallible<TOutput> Apply(TInput input) => Fallible<TOutput>.New(() => Run(input));

        public Effect<TInput, TResult> Match<TResult>(Func<TOutput, TResult> ifOkay, Func<Error, TResult> ifFail)
        {
            return new(input => thunk(input).Match(ifOkay, ifFail));
        }

        /// <summary>
        /// Executes the provided function, catching any exception
        /// thrown and wrapping it in a <see cref="Result{T}"/>
        /// </summary>
        /// <returns></returns>
        public Result<TOutput> Run(TInput input)
        {
            try
            {
                return thunk(input);
            }
            catch (Exception ex)
            {
                return Result<TOutput>.Fail(ex);
            }
        }

        public static implicit operator Effect<TInput, TOutput>(Result<TOutput> result) => new(_ => result);

        public static implicit operator Effect<TInput, TOutput>(TOutput value) => new(_ => value);

        public static implicit operator Effect<TInput, TOutput>(Error error) => new(_ => error);

        public static Effect<TInput, TOutput> operator &(Effect<TInput, TOutput> x, Effect<TInput, TOutput> y) => x.And(y);

        public static Effect<TInput, TOutput> operator |(Effect<TInput, TOutput> x, Effect<TInput, TOutput> y) => x.Or(y);

        public static Effect<TInput, TOutput> operator >>(Effect<TInput, TOutput> x, Effect<TInput, TOutput> y)
            => x.AndThen(_ => y);

        public static Effect<TInput, TOutput> operator >>(Effect<TInput, TOutput> x, Fallible<TOutput> y)
            => x.AndThen(_ => New(y));

        public static Effect<TInput, TOutput> operator >>(Effect<TInput, TOutput> x, Effect<TInput, Unit> y)
            => x.AndThen(v => y.Map(_ => v));

        public static Effect<TInput, TOutput> operator >>(Effect<TInput, TOutput> x, Fallible<Unit> y)
            => x.AndThen(v => New(y.Map(_ => v)));

        public static Effect<TInput, TOutput> operator >>(Effect<TInput, TOutput> x, Func<TInput, Result<TOutput>> y)
            => new(i =>
            {
                _ = x.thunk(i);
                return y(i);
            });

        public static Effect<TInput, TOutput> operator >>(Effect<TInput, TOutput> x, Func<TOutput> y)
            => new(i =>
            {
                _ = x.thunk(i);
                return y();
            });
    }
}
