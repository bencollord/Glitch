namespace Glitch.Functional
{
    public static class LinqExtensions
    {
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
            return predicate.Choose(
                filter => Maybe(source.FirstOrDefault(filter)),
                () => Maybe(source.FirstOrDefault()));
        }

        public static Option<T> LastOrNone<T>(this IEnumerable<T> source)
            => source.LastOrNone(None);

        public static Option<T> LastOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.LastOrNone(Some(predicate));

        private static Option<T> LastOrNone<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
        {
            return predicate.Choose(
                filter => Maybe(source.LastOrDefault(filter)),
                () => Maybe(source.LastOrDefault()));
        }

        public static Result<T> TrySingle<T>(this IEnumerable<T> source)
            => source.TrySingle(None);

        public static Result<T> TrySingle<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.TrySingle(Some(predicate));

        private static Result<T> TrySingle<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
        {
            using var iterator = predicate
                .Map(source.Where)
                .IfNone(source)
                .GetEnumerator();

            if (!iterator.MoveNext())
            {
                return Result<T>.Fail("Sequence contains no elements");
            }

            var value = iterator.Current;

            if (iterator.MoveNext())
            {
                return Result<T>.Fail("Sequence contains more than one element");
            }

            return Result<T>.Okay(value);
        }
    }
}
