namespace Glitch.Functional
{
    public static partial class Try
    {
        public static Try<TResult> Apply<T, TResult>(this Try<Func<T, TResult>> function, Try<T> value)
            => value.Apply(function);

        public static Try<T> Flatten<T>(this Try<Try<T>> nested)
            => nested.AndThen(n => n);
    }

    public class Try<T>
    {
        private Func<Result<T>> thunk;

        public Try(Func<Result<T>> thunk)
        {
            this.thunk = thunk;
        }

        public static Try<T> Okay(T value) => new(() => value);

        public static Try<T> Fail(Error error) => new(() => error);

        public static Try<T> Lift(Result<T> result) => new(() => result);

        public static Try<T> Lift(Func<Result<T>> function) => new(function);

        public static Try<T> Lift(Func<T> function) => new(() => function());

        /// <summary>
        /// Applies the supplied function to the wrapped value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Try<TResult> Map<TResult>(Func<T, TResult> mapper)
            => new(() => thunk().Map(mapper));

        /// <summary>
        /// If the result is a failure, returns a new result with the mapping function
        /// applied to the wrapped error. Otherwise, returns self.
        /// </summary>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Try<T> MapError(Func<Error, Error> mapper)
            => new(() => thunk().MapError(mapper));

        /// <summary>
        /// Applies a wrapped function to the wrapped value if both are successful.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public Try<TResult> Apply<TResult>(Try<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        /// <summary>
        /// Returns other if Ok, otherwise returns the current error wrapped
        /// in a new result type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Try<TResult> And<TResult>(Try<TResult> other)
            => new(() =>
            {
                var result = thunk();

                return result.IsOkay ? other.thunk() : result.Cast<TResult>();
            });

        /// <summary>
        /// If Okay, applies the function to the wrapped value. Otherwise, returns
        /// the current error wrapped in a new <see cref="Try{TResult}" /> type.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Try<TResult> AndThen<TResult>(Func<T, Try<TResult>> mapper)
            => new(() =>
            {
                var result = thunk();

                return result is Result.Okay<T> ok 
                     ? mapper(ok.Value).thunk() 
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
        public Try<TResult> AndThen<TElement, TResult>(Func<T, Try<TElement>> bind, Func<T, TElement, TResult> project)
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
        /// <param name="mapper"></param>
        /// <returns></returns>
        public Try<TResult> AndThen<TResult>(Func<T, Result<TResult>> mapper)
            => AndThen(e => Try<TResult>.Lift(mapper(e)));

        /// <summary>
        /// Implements a bind-map operation, similar to
        /// <see cref="Enumerable.SelectMany"/> for results.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public Try<TResult> AndThen<TElement, TResult>(Func<T, Result<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        /// <summary>
        /// Returns the current <see cref="Try{T}"/> if Ok, otherwise returns other.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Try<T> Or(Try<T> other)
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
        public Try<T> OrElse(Func<Error, Try<T>> other)
            => new(() =>
            {
                var result = thunk();

                return result is Result.Fail<T> fail ? other(fail.Error).thunk() : result;
            });

        public Try<T> Filter(Func<T, bool> predicate)
            => new(() => thunk().Filter(predicate));

        /// <summary>
        /// Executes an impure action against the value if Ok.
        /// No op if fail.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Try<T> IfOkay(Action<T> action) => new(() => thunk().IfOkay(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Try<T> IfFail(Action action) => new(() => thunk().IfFail(action));

        /// <summary>
        /// Executes an impure action if failed.
        /// No op if Ok.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Try<T> IfFail(Action<Error> action) => new(() => thunk().IfFail(action));

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
        public Try<TResult> Cast<TResult>() => new(() => thunk().Cast<TResult>());

        /// <summary>
        /// Combines another try into a try of a tuple.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public Try<(T, TOther)> Zip<TOther>(Try<TOther> other)
            => ZipWith(other, (x, y) => (x, y));

        /// <summary>
        /// Combines two tries using a provided function.
        /// </summary>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public Try<TResult> ZipWith<TOther, TResult>(Try<TOther> other, Func<T, TOther, TResult> zipper)
            => AndThen(x => other.Map(y => zipper(x, y)));

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
                return Result<T>.Fail(ex);
            }
        }

        public static implicit operator Try<T>(Result<T> result) => new(() => result);

        public static explicit operator Result<T>(Try<T> @try) => @try.Run();

        public static implicit operator Try<T>(T value) => new(() => value);

        public static explicit operator T(Try<T> @try) => @try.Run().Unwrap();

        public static Try<T> operator &(Try<T> x, Try<T> y) => x.And(y);

        public static Try<T> operator |(Try<T> x, Try<T> y) => x.Or(y);

        public static Try<T> operator >>(Try<T> x, Try<T> y) 
            => new(() =>
            {
                _ = x.thunk();
                return y.thunk();
            });

        public static Try<T> operator >>(Try<T> x, Func<Result<T>> y)
            => new(() =>
            {
                _ = x.thunk();
                return y();
            });

        public static Try<T> operator >>(Try<T> x, Func<T> y)
            => new(() =>
            {
                _ = x.thunk();
                return y();
            });
    }
}
