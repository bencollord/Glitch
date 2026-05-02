using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

using static Result;
using static Option;

public static partial class LinqExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public Result<T, Error> TrySingle() => source.TrySingle(None);

        public Result<T, Error> TrySingle(Func<T, bool> predicate)
            => source.TrySingle(Some(predicate));

        private Result<T, Error> TrySingle(Option<Func<T, bool>> predicate)
        {
            return source.TrySingleOrNone(predicate)
                         .AndThen(opt => opt.OkayOr(Error.NoElements));
        }

        public Result<Option<T>, Error> TrySingleOrNone() => source.TrySingleOrNone(None);

        public Result<Option<T>, Error> TrySingleOrNone(Func<T, bool> predicate)
            => source.TrySingleOrNone(Some(predicate));

        private Result<Option<T>, Error> TrySingleOrNone(Option<Func<T, bool>> predicate)
        {
            using var iterator = predicate
                .Select(source.Where)
                .IfNone(source)
                .GetEnumerator();

            Result<Option<T>, Error> value = Error.NoElements;

            if (iterator.MoveNext())
            {
                value = Some(iterator.Current);
            }

            if (iterator.MoveNext())
            {
                value = Fail(Error.MoreThanOneElement);
            }

            return value;
        }
    }
}
