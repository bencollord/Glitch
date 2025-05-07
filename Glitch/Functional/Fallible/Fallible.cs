
namespace Glitch.Functional
{
    public partial class Fallible<T>
    {
        private Func<Result<T>> thunk;

        public Fallible(Func<Result<T>> thunk)
        {
            this.thunk = thunk;
        }

        public static Fallible<T> Okay(T value) => new(() => value);

        public static Fallible<T> Fail(Error error) => new(() => error);

        public static Fallible<T> Lift(Result<T> result) => new(() => result);

        public static Fallible<T> Lift(Func<Result<T>> function) => new(function);

        public static Fallible<T> Lift(Func<T> function) => new(() => function());

        /// <summary>
        /// Applies the supplied function to the wrapped value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public Fallible<TResult> Map<TResult>(Func<T, TResult> map)
            => new(() => thunk().Map(map));

        public Fallible<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        public Fallible<TResult> MapOr<TResult>(Func<T, TResult> map, Error ifFail)
            => new(() => thunk().MapOr(map, ifFail));

        public Fallible<TResult> MapOrElse<TResult>(Func<T, TResult> map, Func<Error, Error> ifFail)
            => new(() => thunk().MapOrElse(map, ifFail));

        public Fallible<T> WithError(Error error)
            => new(() => thunk().WithError(error));

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public Fallible<T> MapError(Func<Error, Error> map)
            => new(() => thunk().MapError(map));

        public Fallible<T> MapError<TError>(Func<TError, Error> map)
            where TError : Error
            => MapError(err => err is TError e ? map(e) : err);

        public Fallible<T> Catch<TException>(Func<TException, T> map)
            where TException : Exception
            => OrElse(err => err.IsException<TException>() ? map((TException)err.AsException()) : err);

        public Fallible<T> Catch<TException>(Func<TException, Error> map)
            where TException : Exception
            => MapError(err => err.IsException<TException>() ? map((TException)err.AsException()) : err);

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both are successful.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Fallible<TResult> Apply<TResult>(Fallible<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Fallible<TResult> And<TResult>(Fallible<TResult> other)
            => new(() =>
            {
                var result = thunk();

                return result.IsOkay ? other.thunk() : result.Cast<TResult>();
            });

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new <see cref="Fallible{TResult}" /> type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Fallible<TResult> AndThen<TResult>(Func<T, Fallible<TResult>> bind)
            => new(() =>
            {
                var result = thunk();

                return result is Result.Okay<T> ok 
                     ? bind(ok.Value).thunk() 
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
        public Fallible<TResult> AndThen<TElement, TResult>(Func<T, Fallible<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Convenience method that allows bind operations to work
        /// with <see cref="Result"/> types.
        /// </summary>
        /// <remarks>
        /// This is here to reflect the common use case of needing
        /// to bind between tries and results. The two types are
        /// closely related anyway, since Try is effectively a lazy
        /// version of result that automatically captures errors,
        /// C# lacks higher kindred types, and its type inference leaves
        /// a lot to be desired when coding in a functional style.
        /// </remarks>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public Fallible<TResult> AndThen<TResult>(Func<T, Result<TResult>> bind)
            => AndThen(e => Fallible<TResult>.Lift(bind(e)));

        /// <summary>
        /// Implements a bind-map operation, similar to
        /// <see cref="Enumerable.SelectMany"/> for results.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Fallible<TResult> AndThen<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public Fallible<TResult> Choose<TResult>(Func<T, Fallible<TResult>> ifOkay, Func<Error, Fallible<TResult>> ifFail)
            => new(() => thunk().Match(ifOkay, ifFail).Run());

        public Fallible<T> Guard(Func<T, bool> predicate, Error error)
            => new(() => thunk().Guard(predicate, error));

        public Fallible<T> Guard(Func<T, bool> predicate, Func<T, Error> error)
            => new(() => thunk().Guard(predicate, error));

        public Fallible<T> Guard(bool condition, Error error)
            => new(() => thunk().Guard(condition, error));

        public Fallible<T> Guard(bool condition, Func<T, Error> error)
            => new(() => thunk().Guard(condition, error));

        /// <summary>
        /// Returns the current <see cref="Fallible{T}"/> if Ok, otherwise returns other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Fallible<T> Or(Fallible<T> other)
            => new(() =>
            {
                var result = thunk();

                return result.IsOkay ? result : other.thunk();
            });

        /// <summary>
        /// Returns the current instance if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Fallible<T> OrElse(Func<Error, Fallible<T>> other)
            => new(() =>
            {
                var result = thunk();

                return result is Result.Fail<T> fail ? other(fail.Error).thunk() : result;
            });

        public Fallible<T> Filter(Func<T, bool> predicate)
            => new(() => thunk().Filter(predicate));

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Fallible<T> Do(Action<T> action) => IfOkay(action); // TODO Not sure how I feel about just aliasing methods like this.

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Fallible<T> IfOkay(Action<T> action) => new(() => thunk().IfOkay(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Fallible<T> IfFail(Action action) => new(() => thunk().IfFail(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Fallible<T> IfFail(Action<Error> action) => new(() => thunk().IfFail(action));

        /// <summary>
        /// Runs and returns the value if success, or the fallback value if fail.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfFail(T fallback) => Run().IfFail(fallback);

        /// <summary>
        /// Runs the result and returns the value if success, or the
        /// result of the fallback function if fail.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfFail(Func<T> fallback) => Run().IfFail(fallback);

        /// <summary>
        /// Runs the result and returns the value if success, or the
        /// result of the fallback function if fail.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T IfFail(Func<Error, T> fallback) => Run().IfFail(fallback);

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the provided fallback value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifFail"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> ifOkay, TResult ifFail)
            => Map(ifOkay).IfFail(ifFail);

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifFail"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<TResult> ifFail)
            => Map(ifOkay).IfFail(ifFail);

        /// <summary>
        /// If Ok, returns the result of the first function to the wrapped value.
        /// Otherwise, returns the result of the second function to the wrapped error.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOkay"></param>
        /// <param name="ifError"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> ifOkay, Func<Error, TResult> ifError)
            => Run().Match(ifOkay, ifError);

        /// <summary>
        /// Casts the wrapped value to <typeparamref name="TResult"/> if Ok,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Fallible<TResult> Cast<TResult>()
            => CastOrElse<TResult>(v => new InvalidCastException($"Cannot cast a value of type {v!.GetType()} to {typeof(TResult)}"));

        public Fallible<TResult> CastOr<TResult>(Error error)
            => MapOr(v => (TResult)(dynamic)v!, error);

        public Fallible<TResult> CastOrElse<TResult>(Func<T, Error> error)
            => from val  in this
               let  cast =  Try(() => DynamicCast<T, TResult>(val))
               let  err  =  Fallible.Lift<TResult>(error(val))
               from res  in cast | err
               select res;

        /// <summary>
        /// Combines another try into a try of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Fallible<(T, TOther)> Zip<TOther>(Fallible<TOther> other)
            => Zip(other, (x, y) => (x, y));

        /// <summary>
        /// Combines two tries using a provided function.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public Fallible<TResult> Zip<TOther, TResult>(Fallible<TOther> other, Func<T, TOther, TResult> zipper)
            => new(() => thunk().Zip(other.thunk(), zipper));

        public Effect<TInput, T> WithInput<TInput>() => Effect<TInput, T>.Lift(this);

        /// <summary>
        /// Executes the provided function, catching any exception
        /// thrown and wrapping it in a <see cref="Result{T}"/>
        /// </summary>
        /// <returns></returns>
        public Result<T> Run()
        {
            // Memoize the thunk
            thunk = thunk.Memo();

            try
            {
                return thunk();
            }
            catch (Exception ex)
            {
                return Result<T>.Fail(ex);
            }
        }

        public T Unwrap() => Run().Unwrap();

        public void ThrowIfFail() => Run().ThrowIfFail();

        public static implicit operator Fallible<T>(Result<T> result) => new(() => result);

        public static explicit operator Result<T>(Fallible<T> @try) => @try.Run();

        public static implicit operator Fallible<T>(T value) => new(() => value);

        public static implicit operator Fallible<T>(Error error) => new(() => error);

        public static explicit operator T(Fallible<T> @try) => @try.Run().Unwrap();

        public static Fallible<T> operator &(Fallible<T> x, Fallible<T> y) => x.And(y);

        public static Fallible<T> operator |(Fallible<T> x, Fallible<T> y) => x.Or(y);

        public static Fallible<T> operator >>(Fallible<T> x, Fallible<T> y)
            => x.AndThen(_ => y);

        public static Fallible<T> operator >>(Fallible<T> x, Fallible<Terminal> y)
            => x.AndThen(v => y.Map(_ => v));

        public static Fallible<T> operator >>(Fallible<T> x, Func<Result<T>> y)
            => new(() =>
            {
                _ = x.thunk();
                return y();
            });

        public static Fallible<T> operator >>(Fallible<T> x, Func<T> y)
            => new(() =>
            {
                _ = x.thunk();
                return y();
            });
    }
}
