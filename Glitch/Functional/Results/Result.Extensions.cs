using Glitch.Functional.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Results
{
    public static partial class Result
    {
        /// <summary>
        /// Returns a the unwrapped values of all the successful results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<T> Successes<T, E>(this IEnumerable<Result<T, E>> results)
            => results.Where(IsOkay).Select(r => (T)r);

        /// <summary>
        /// Returns a the unwrapped errors of all the faulted results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<E> Errors<T, E>(this IEnumerable<Result<T, E>> results)
            => results.Where(IsFail).Select(r => (E)r);

        public static (IEnumerable<T> Successes, IEnumerable<E> Errors) Partition<T, E>(this IEnumerable<Result<T, E>> results)
            => (results.Successes(), results.Errors());

        public static Result<TResult, E> Apply<T, E, TResult>(this Result<Func<T, TResult>, E> function, Result<T, E> value)
            => value.Apply(function);

    }
}
