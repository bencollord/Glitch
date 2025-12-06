using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

using static Expected;
using static Option;

public static partial class LinqExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public Expected<T> TrySingle() => source.TrySingle(None);

        public Expected<T> TrySingle(Func<T, bool> predicate)
            => source.TrySingle(Some(predicate));

        private Expected<T> TrySingle(Option<Func<T, bool>> predicate)
        {
            return source.TrySingleOrNone(predicate)
                         .AndThen(opt => opt.OkayOr(Error.NoElements));
        }

        private Expected<Option<T>> TrySingleOrNone(Option<Func<T, bool>> predicate)
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
                return Fail(Error.MoreThanOneElement);
            }

            return Okay(value);
        }
    }
}
