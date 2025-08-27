using Glitch.Functional.Results;
using System.Collections.Immutable;

namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        public static Expected<IEnumerable<T>, E> Traverse<T, E>(this IEnumerable<Expected<T, E>> source)
            => source.Traverse(Identity);

        public static Expected<IEnumerable<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<Expected<T, E>> source, Func<T, TResult> traverse)
            => source.Traverse(opt => opt.Map(traverse));

        public static Expected<IEnumerable<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<Expected<T, E>> source, Func<T, int, TResult> traverse)
            => source.Select((s, i) => s.PartialMap(traverse).Apply(i))
                     .Traverse();

        public static Expected<IEnumerable<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<T> source, Func<T, int, Expected<TResult, E>> traverse)
            => source.Select((s, i) => traverse(s, i))
                     .Traverse();

        public static Expected<IEnumerable<TResult>, E> Traverse<T, E, TResult>(this IEnumerable<T> source, Func<T, Expected<TResult, E>> traverse)
            => source.Aggregate(
                Expected<ImmutableList<TResult>, E>.Okay(ImmutableList<TResult>.Empty),
                (list, item) => list.AndThen(_ => traverse(item), (lst, i) => lst.Add(i)),
                list => list.Map(l => l.AsEnumerable()));

        /// <summary>
        /// Returns a the unwrapped values of all the successful results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<T> Successes<T, E>(this IEnumerable<Expected<T, E>> results)
            => results.Where(IsOkay).Select(r => (T)r);

        /// <summary>
        /// Returns a the unwrapped errors of all the faulted results.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns></returns>
        public static IEnumerable<E> Errors<T, E>(this IEnumerable<Expected<T, E>> results)
            => results.Where(IsFail).Select(r => (E)r);

        public static (IEnumerable<T> Successes, IEnumerable<E> Errors) Partition<T, E>(this IEnumerable<Expected<T, E>> results)
            => (results.Successes(), results.Errors());

        public static Expected<TResult, E> Apply<T, E, TResult>(this Expected<Func<T, TResult>, E> function, Expected<T, E> value)
            => value.Apply(function);

    }
}
