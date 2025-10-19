using Glitch.Functional.Results;

namespace Glitch.Functional
{
    using static Option;
    using static Expected;

    public static class LinqExtensions
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
        public static IEnumerable<(Option<TSource> First, Option<TOther> Second)> LongZip<TSource, TOther>(this IEnumerable<TSource> source, IEnumerable<TOther> other)
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
        public static IEnumerable<TResult> LongZip<TSource, TOther, TResult>(this IEnumerable<TSource> source, IEnumerable<TOther> other, Func<Option<TSource>, Option<TOther>, TResult> zipper)
            => source.LongZip(other).Select(x => zipper(x.First, x.Second));

        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<IEnumerable<T>, TResult> many, TResult none)
            => source.Match(many, _ => none);

        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<IEnumerable<T>, TResult> many, Func<Unit, TResult> none)
            => source.Match(many, () => none(default));

        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<IEnumerable<T>, TResult> many, Func<TResult> none) => source.Any() ? many(source) : none();

        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<T, TResult> just, Func<IEnumerable<T>, TResult> many, TResult none)
            => source.Match(just, many, _ => none);

        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<T, TResult> just, Func<IEnumerable<T>, TResult> many, Func<Unit, TResult> none)
            => source.Match(just, many, () => none(default));

        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<T, TResult> just, Func<IEnumerable<T>, TResult> many, Func<TResult> none)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return none();
                }

                var first = enumerator.Current;

                if (!enumerator.MoveNext())
                {
                    return just(first);
                }
            }

            return many(source);
        }

        public static IEnumerable<Func<T2, TResult>> PartialSelect<T, T2, TResult>(this IEnumerable<T> source, Func<T, T2, TResult> selector)
            => source.Select(selector.Curry());

        public static Option<T> ElementAtOrNone<T>(this IEnumerable<T> source, int index)
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }    

            using var enumerator = source.GetEnumerator();

            for (int i = index; index > 0 && enumerator.MoveNext(); i--)
            {
                if (i == 0)
                {
                    return Some(enumerator.Current);
                }
            }

            return None;
        }

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> source)
            => source.FirstOrNone(None);

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.FirstOrNone(Some(predicate));

        private static Option<T> FirstOrNone<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
        {
            return predicate.Match(
                filter => Maybe(source.FirstOrDefault(filter)),
                () => Maybe(source.FirstOrDefault()));
        }

        public static Option<T> LastOrNone<T>(this IEnumerable<T> source)
            => source.LastOrNone(None);

        public static Option<T> LastOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.LastOrNone(Some(predicate));

        private static Option<T> LastOrNone<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
        {
            return predicate.Match(
                filter => Maybe(source.LastOrDefault(filter)),
                () => Maybe(source.LastOrDefault()));
        }

        public static Option<T> SingleOrNone<T>(this IEnumerable<T> source)
            => source.SingleOrNone(None);

        public static Option<T> SingleOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.SingleOrNone(Some(predicate));

        private static Option<T> SingleOrNone<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
            => source.TrySingleOrNone(predicate).Unwrap();

        public static Expected<T> TrySingle<T>(this IEnumerable<T> source)
            => source.TrySingle(None);

        public static Expected<T> TrySingle<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.TrySingle(Some(predicate));

        private static Expected<T> TrySingle<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
        {
            return source.TrySingleOrNone(predicate)
                         .AndThen(opt => opt.ExpectOr(Errors.NoElements));
        }

        public static Expected<Option<T>> TrySingleOrNone<T>(this IEnumerable<T> source)
            => source.TrySingleOrNone(None);

        public static Expected<Option<T>> TrySingleOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.TrySingleOrNone(Some(predicate));

        private static Expected<Option<T>> TrySingleOrNone<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
        {
            using var iterator = predicate
                .Select(source.Where)
                .IfNone(source)
                .GetEnumerator();

            Option<T> value = None;

            if (iterator.MoveNext())
            {
                value = Some(iterator.Current);
            }

            if (iterator.MoveNext())
            {
                return Fail(Errors.MoreThanOneElement);
            }

            return Okay(value);
        }
    }
}
