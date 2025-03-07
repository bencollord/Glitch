using System.Collections.Immutable;

namespace Glitch.Functional
{
    public static partial class Validation
    {
        public static Validation<IEnumerable<T>> Traverse<T>(this IEnumerable<Validation<T>> source)
            => source.Traverse(Identity);

        public static Validation<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<Validation<T>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Validation<IEnumerable<TResult>> Traverse<T, TResult>(this IEnumerable<T> source, Func<T, Validation<TResult>> traverse)
            => source.Aggregate(
                Validation<ImmutableList<TResult>>.Success(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));

        /// <summary>
        /// Returns a the unwrapped values of all the successful results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<T> Successes<T>(this IEnumerable<Validation<T>> results)
            => results.Where(v => v.IsSuccess).Select(r => (T)r);

        /// <summary>
        /// Returns a the unwrapped errors of all the faulted results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<Error> Errors<T>(this IEnumerable<Validation<T>> results)
            => results.Where(v => v.IsFailure).SelectMany(r => r.UnwrapError().Iterate());

        public static (IEnumerable<T> Successes, IEnumerable<Error> Errors) Partition<T>(this IEnumerable<Validation<T>> results)
            => (results.Successes(), results.Errors());

        public static Validation<TResult> Apply<T, TResult>(this Validation<Func<T, TResult>> function, Validation<T> value)
            => value.Apply(function);

        public static Validation<T> Flatten<T>(this Validation<Validation<T>> nested)
            => nested.AndThen(n => n);
    }
}
