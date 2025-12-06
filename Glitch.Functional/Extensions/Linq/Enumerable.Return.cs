using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

using static Expected;
using static Option;

public static partial class LinqExtensions
{
    extension(Enumerable)
    {
        public static IEnumerable<T> Singleton<T>(T item) => [item];

        public static IEnumerable<T> Of<T>(params IEnumerable<T> items) => items;

        public static IEnumerable<T> From<T>(IEnumerable<T> items) => items;

        /// <summary>
        /// Unfolds a <paramref name="seed"/> into a sequence using the provided
        /// <paramref name="generator"/> forever.
        /// </summary>
        /// <typeparam name="TSeed"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="seed"></param>
        /// <param name="generator"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateInfinite<TSeed, TResult>(TSeed seed, Func<TSeed, (TSeed Seed, TResult Value)> generator)
        {
            var state = seed;

            while (true)
            {
                (state, var value) = generator(state);

                yield return value;
            }
        }

        /// <summary>
        /// Unfolds a <paramref name="seed"/> into a sequence using the provided
        /// <paramref name="generator"/> until the given <paramref name="predicate"/>
        /// returns true.
        /// </summary>
        /// <remarks>
        /// The "unfold" to <see cref="Enumerable.Aggregate{TSource, TAccumulate}(IEnumerable{TSource}, TAccumulate, Func{TAccumulate, TSource, TAccumulate})"/>'s "fold."
        /// </remarks>
        /// <typeparam name="TSeed"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="seed"></param>
        /// <param name="generator"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateUntil<TSeed, TResult>(TSeed seed, Func<TSeed, (TSeed Seed, TResult Value)> generator, Func<TSeed, bool> predicate) =>
            GenerateWhile(seed, generator, !predicate);

        /// <summary>
        /// Unfolds a <paramref name="seed"/> into a sequence using the provided
        /// <paramref name="generator"/> while the given <paramref name="predicate"/>
        /// returns true.
        /// </summary>
        /// <remarks>
        /// The "unfold" to <see cref="Enumerable.Aggregate{TSource, TAccumulate}(IEnumerable{TSource}, TAccumulate, Func{TAccumulate, TSource, TAccumulate})"/>'s "fold."
        /// </remarks>
        /// <typeparam name="TSeed"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="seed"></param>
        /// <param name="generator"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> GenerateWhile<TSeed, TResult>(TSeed seed, Func<TSeed, (TSeed Seed, TResult Value)> generator, Func<TSeed, bool> predicate) =>
            Generate(seed, generator.Then(res => Some(res).Where(x => predicate(x.Seed))));

        /// <summary>
        /// Unfolds a <paramref name="seed"/> into a sequence using the provided
        /// <paramref name="generator"/> until it returns <see cref="None"/>.
        /// </summary>
        /// <remarks>
        /// The "unfold" to <see cref="Enumerable.Aggregate{TSource, TAccumulate}(IEnumerable{TSource}, TAccumulate, Func{TAccumulate, TSource, TAccumulate})"/>'s "fold."
        /// </remarks>
        /// <typeparam name="TSeed"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="seed"></param>
        /// <param name="generator"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Generate<TSeed, TResult>(TSeed seed, Func<TSeed, Option<(TSeed Seed, TResult Value)>> generator)
        {
            Option<TSeed> state = Some(seed);

            do
            {
                (state, var value) = state >>> generator >> Unzip;

                if (value.IsSome)
                {
                    yield return value.Unwrap();
                }
            }
            while (state.IsSome);
        }
    }
}
