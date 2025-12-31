using Glitch.Functional;

namespace Glitch.Functional.Extensions;

using static Option;

public static partial class LinqExtensions
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        /// <summary>
        /// Zips two sequences together into a tuple containing both elements. 
        /// When one sequence is longer than the other, <see cref="None"/> 
        /// is returned for the corresponding other item. The resulting sequence will
        /// be the length of the longest sequence.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public IEnumerable<(Option<TSource> First, Option<TOther> Second)> LongZip<TOther>(IEnumerable<TOther> other)
        {
            using var src = source.GetEnumerator();
            using var otr = other.GetEnumerator();

            while (true)
            {
                var s = src.MoveNext() ? Some(src.Current) : None;
                var o = otr.MoveNext() ? Some(otr.Current) : None;

                if (s.IsNone && o.IsNone)
                {
                    yield break;
                }

                yield return (s, o);
            }
        }

        /// <summary>
        /// Zips two sequences together using the provided <paramref name="zipper"/> function.
        /// If one of the sequences is shorter than the other, <paramref name="zipper"/> will receive 
        /// <see cref="None"/> for the corresponding argument. The resulting sequence will be the 
        /// length of the longest sequence.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="zipper"></param>
        /// <returns></returns>
        public IEnumerable<TResult> LongZip<TOther, TResult>(IEnumerable<TOther> other, Func<Option<TSource>, Option<TOther>, TResult> zipper)
            => source.LongZip(other).Select(x => zipper(x.First, x.Second));
    }
}
