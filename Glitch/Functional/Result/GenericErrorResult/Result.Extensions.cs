using System.Collections.Immutable;

namespace Glitch.Functional
{
    public static partial class Result
    {
        public static Result<IEnumerable<TOkay>, TError> Traverse<TOkay, TError>(this IEnumerable<Result<TOkay, TError>> source)
            => source.Traverse(Identity);

        public static Result<IEnumerable<TResult>, TError> Traverse<TOkay, TError, TResult>(this IEnumerable<Result<TOkay, TError>> source, Func<TOkay, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Result<IEnumerable<TResult>, TError> Traverse<TOkay, TError, TResult>(this IEnumerable<Result<TOkay, TError>> source, Func<TOkay, int, TResult> traverse)
            => source.Select((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Result<IEnumerable<TResult>, TError> Traverse<TOkay, TError, TResult>(this IEnumerable<TOkay> source, Func<TOkay, int, Result<TResult, TError>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        // TODO Clean up syntax
        public static Result<IEnumerable<TResult>, TError> Traverse<TOkay, TError, TResult>(this IEnumerable<TOkay> source, Func<TOkay, Result<TResult, TError>> traverse)
            => source.Aggregate(
                Result<ImmutableList<TResult>, TError>.Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));

        /// <summary>
        /// Returns a the unwrapped values of all the successful results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<TOkay> Successes<TOkay, TError>(this IEnumerable<Result<TOkay, TError>> results)
            => results.Where(IsOkay).Select(r => (TOkay)r);

        /// <summary>
        /// Returns a the unwrapped errors of all the faulted results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<TError> Errors<TOkay, TError>(this IEnumerable<Result<TOkay, TError>> results)
            => results.Where(IsFail).Select(r => (TError)r);

        public static (IEnumerable<TOkay> Successes, IEnumerable<TError> Errors) Partition<TOkay, TError>(this IEnumerable<Result<TOkay, TError>> results)
            => (results.Successes(), results.Errors());

        public static Result<TResult, TError> Apply<TOkay, TError, TResult>(this Result<Func<TOkay, TResult>, TError> function, Result<TOkay, TError> value)
            => value.Apply(function);

    }
}
