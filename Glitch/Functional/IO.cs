namespace Glitch.Functional
{
    public static partial class IO
    {
        public static IO<T> Ok<T>(T value) => new(() => value);

        public static IO<T> Fail<T>(Error error) => new(() => error);

        public static IO<T> Lift<T>(Result<T> result) => new(() => result);

        public static IO<T> Lift<T>(Func<Result<T>> function) => new(function);

        public static IO<T> Lift<T>(Func<T> function) => new(() => function());

        public static IO<TResult> Apply<T, TResult>(this IO<Func<T, TResult>> function, IO<T> value)
            => value.Apply(function);
    }

    public class IO<T>
    {
        private Func<Result<T>> thunk;

        public IO(Func<Result<T>> thunk)
        {
            this.thunk = thunk;
        }

        /// <summary>
        /// Applies the supplied function to the wrapped value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public IO<TResult> Map<TResult>(Func<T, TResult> mapper)
            => new(() => thunk().Map(mapper));

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public IO<T> MapError(Func<Error, Error> mapper)
            => new(() => thunk().MapError(mapper));

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both are successful.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public IO<TResult> Apply<TResult>(IO<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public IO<TResult> And<TResult>(IO<TResult> other)
            => new(() =>
            {
                var result = thunk();

                return result.IsOk ? other.thunk() : result.Cast<TResult>();
            });

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new <see cref="IO{TResult}" /> type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public IO<TResult> AndThen<TResult>(Func<T, IO<TResult>> mapper)
            => new(() =>
            {
                var result = thunk();

                return result is Result<T>.Ok ok ? mapper(ok.Value).thunk() : result.Cast<TResult>();
            });

        /// <summary>
        /// Returns the current <see cref="IO{T}"/> if Ok, otherwise returns other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public IO<T> Or(IO<T> other)
            => new(() =>
            {
                var result = thunk();

                return result.IsOk ? result : other.thunk();
            });

        /// <summary>
        /// Returns the current instance if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public IO<T> OrElse(Func<Error, IO<T>> other)
            => new(() =>
            {
                var result = thunk();

                return result is Result<T>.Fail fail ? other(fail.Error).thunk() : result;
            });

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IO<T> Do(Action<T> action) => new(() => thunk().Do(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IO<T> IfFail(Action action) => new(() => thunk().IfFail(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IO<T> IfFail(Action<Error> action) => new(() => thunk().IfFail(action));

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
        public IO<TResult> Cast<TResult>() => new(() => thunk().Cast<TResult>());

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise throws the wrapped error
        /// as an exception.
        /// </summary>
        /// <returns></returns>
        public T Unwrap() => Run().Unwrap();

        /// <summary>
        /// Returns the wrapped value if Ok, otherwise returns the fallback value.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T UnwrapOr(T fallback) => Run().UnwrapOr(fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T UnwrapOrElse(Func<T> fallback) => Run().UnwrapOrElse(fallback);

        /// <summary>
        /// Returns the wrapped value if Ok. Otherwise, returns the result
        /// of the fallback function applied to the wrapped error.
        /// </summary>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T UnwrapOrElse(Func<Error, T> fallback) => Run().UnwrapOrElse(fallback);

        /// <summary>
        /// Executes the provided function, catching any exception
        /// thrown and wrapping it in a <see cref="Result{T}"/>
        /// </summary>
        /// <returns></returns>
        public Result<T> Run()
        {
            try
            {
                return thunk();
            }
            catch (Exception ex)
            {
                return Result.Fail<T>(ex);
            }
        }

        public static IO<T> operator &(IO<T> x, IO<T> y) => x.And(y);

        public static IO<T> operator |(IO<T> x, IO<T> y) => x.Or(y);

        public static IO<T> operator >>(IO<T> x, IO<T> y) 
            => new(() =>
            {
                _ = x.thunk();
                return y.thunk();
            });

        public static IO<T> operator >>(IO<T> x, Func<Result<T>> y)
            => new(() =>
            {
                _ = x.thunk();
                return y();
            });

        public static IO<T> operator >>(IO<T> x, Func<T> y)
            => new(() =>
            {
                _ = x.thunk();
                return y();
            });
    }
}
