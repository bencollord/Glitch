namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        /// <inheritdoc cref="SetEquals{T}(IEnumerable{T}, IEnumerable{T}, IEqualityComparer{T})"/>
        public bool SetEquals(IEnumerable<T> other)
            => source.SetEquals(other, EqualityComparer<T>.Default);

        /// <summary>
        /// Returns true if every item in <paramref name="source"/>
        /// is in <paramref name="other"/>, and vice-versa, irrespective
        /// of order or repeated items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public bool SetEquals(IEnumerable<T> other, IEqualityComparer<T> comparer)
            => source.ToHashSet(comparer).SetEquals(other);
    }
}
