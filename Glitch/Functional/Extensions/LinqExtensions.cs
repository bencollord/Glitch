using Glitch.Functional.Results;

namespace Glitch.Functional
{
    using static Option;

    public static class LinqExtensions
    {
        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<T, TResult> one, Func<IEnumerable<T>, TResult> many, Func<Unit, TResult> none)
            => source.Match(one, many, () => none(default));

        public static TResult Match<T, TResult>(this IEnumerable<T> source, Func<T, TResult> one, Func<IEnumerable<T>, TResult> many, Func<TResult> none)
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
                    return one(first);
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

        public static Option<T> SingleOrNone<T>(this IEnumerable<T> source)
            => source.SingleOrNone(None);

        public static Option<T> SingleOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.SingleOrNone(Some(predicate));

        private static Option<T> SingleOrNone<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
            => source.TrySingle(predicate) switch
            {
                Result.Success<T>(var value) => Some(value),

                Result.Failure<T>(Error e)
                    when e.IsCode(ErrorCodes.NoElements) => None,

                Result.Failure<T>(Error e) => throw e.AsException(),

                _ => throw new BadDiscriminatedUnionException()
            };
                     

        public static Result<T> TrySingle<T>(this IEnumerable<T> source)
            => source.TrySingle(None);

        public static Result<T> TrySingle<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.TrySingle(Some(predicate));

        private static Result<T> TrySingle<T>(this IEnumerable<T> source, Option<Func<T, bool>> predicate)
        {
            using var iterator = predicate
                .Select(source.Where)
                .IfNone(source)
                .GetEnumerator();

            if (!iterator.MoveNext())
            {
                return Result<T>.Fail(Errors.NoElements);
            }

            var value = iterator.Current;

            if (iterator.MoveNext())
            {
                return Result<T>.Fail(Errors.MoreThanOneElement);
            }

            return Result<T>.Okay(value);
        }
    }
}
