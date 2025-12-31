using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

using static Result;
using static Option;

public static partial class LinqExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public Result<T> TrySingle() => source.TrySingle(None);

        public Result<T> TrySingle(Func<T, bool> predicate)
            => source.TrySingle(Some(predicate));

        private Result<T> TrySingle(Option<Func<T, bool>> predicate)
        {
            return source.TrySingleOrNone(predicate)
                         .AndThen(opt => opt.OkayOr(Error.NoElements));
        }

        private Result<Option<T>> TrySingleOrNone(Option<Func<T, bool>> predicate)
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
                return Fail<Option<T>>(Error.MoreThanOneElement);
            }

            return Okay(value);
        }
    }
}
