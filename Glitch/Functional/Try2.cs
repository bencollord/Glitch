namespace Glitch.Functional
{
    public static partial class Try
    {
        public static Try<TArg, TResult> Ok<TArg, TResult>(TResult value) => new(_ => value);

        public static Try<TArg, TResult> Fail<TArg, TResult>(Error error) => new(_ => error);

        public static Try<TArg, TResult> Lift<TArg, TResult>(Result<TResult> result) => new(_ => result);

        public static Try<TArg, TResult> Lift<TArg, TResult>(Func<TArg, Result<TResult>> function) => new(function);

        public static Try<TArg, TResult> Lift<TArg, TResult>(Func<TArg, TResult> function) => new(arg => function(arg));

        public static Try<TArg, TNewResult> Apply<TArg, TResult, TNewResult>(this Try<TArg, Func<TResult, TNewResult>> function, Try<TArg, TResult> value)
            => value.Apply(function);
    }

    public class Try<TArg, TResult>
    {
        private Func<TArg, Result<TResult>> thunk;

        public Try(Func<TArg, Result<TResult>> thunk)
        {
            this.thunk = thunk;
        }

        /// <summary>
        /// Applies the supplied function to the wrapped value.
        /// </summary>
        /// <typeparam name="TNewResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Try<TArg, TNewResult> Map<TNewResult>(Func<TResult, TNewResult> mapper)
            => new(arg => thunk(arg).Map(mapper));

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Try<TArg, TResult> MapError(Func<Error, Error> mapper)
            => new(arg => thunk(arg).MapError(mapper));

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both are successful.
        /// </summary>
        /// <typeparam name="TNewResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Try<TArg, TNewResult> Apply<TNewResult>(Try<TArg, Func<TResult, TNewResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TNewResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Try<TArg, TNewResult> And<TNewResult>(Try<TArg, TNewResult> other)
            => new(arg =>
            {
                var result = thunk(arg);

                return result.IsOkay ? other.thunk(arg) : result.Cast<TNewResult>();
            });

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new <see cref="Try{TNewResult}" /> type.
        /// </summary>
        /// <typeparam name="TNewResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Try<TArg, TNewResult> AndThen<TNewResult>(Func<TResult, Try<TArg, TNewResult>> mapper)
            => new(arg =>
            {
                var result = thunk(arg);

                return result is Result.Okay<TResult> ok ? mapper(ok.Value).thunk(arg) : result.Cast<TNewResult>();
            });

        /// <summary>
        /// Returns the current <see cref="Try{T}"/> if Ok, otherwise returns other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Try<TArg, TResult> Or(Try<TArg, TResult> other)
            => new(arg =>
            {
                var result = thunk(arg);

                return result.IsOkay ? result : other.thunk(arg);
            });

        /// <summary>
        /// Returns the current instance if Ok, otherwise applies the provided
        /// function to the current error and returns the result.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Try<TArg, TResult> OrElse(Func<Error, Try<TArg, TResult>> other)
            => new(arg =>
            {
                var result = thunk(arg);

                return result is Result.Failure<TResult> fail ? other(fail.Error).thunk(arg) : result;
            });

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Try<TArg, TResult> Do(Action<TResult> action) => new(arg => thunk(arg).Do(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Try<TArg, TResult> IfFail(Action action) => new(arg => thunk(arg).IfFail(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Try<TArg, TResult> IfFail(Action<Error> action) => new(arg => thunk(arg).IfFail(action));

        /// <summary>
        /// Casts the wrapped value to <typeparamref name="TNewResult"/> if Ok,
        /// otherwise returns the current error wrapped in a new result type.
        /// </summary>
        /// <typeparam name="TNewResult"></typeparam>
        /// <returns></returns>
        public Try<TArg, TNewResult> Cast<TNewResult>() => new(arg => thunk(arg).Cast<TNewResult>());

        /// <summary>
        /// Executes the provided function, catching any exception
        /// thrown and wrapping it in a <see cref="Result{T}"/>
        /// </summary>
        /// <returns></returns>
        public Result<TResult> Run(TArg arg)
        {
            try
            {
                return thunk(arg);
            }
            catch (Exception ex)
            {
                return Result.Fail<TResult>(ex);
            }
        }

        public static Try<TArg, TResult> operator &(Try<TArg, TResult> x, Try<TArg, TResult> y) => x.And(y);

        public static Try<TArg, TResult> operator |(Try<TArg, TResult> x, Try<TArg, TResult> y) => x.Or(y);

        public static Try<TArg, TResult> operator >>(Try<TArg, TResult> x, Try<TArg, TResult> y) 
            => new(arg =>
            {
                _ = x.thunk(arg);
                return y.thunk(arg);
            });
    }
}
