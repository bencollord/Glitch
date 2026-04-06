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
                         .IfNone(Error.NoElements);
        }

        public Option<Expected<T>> TrySingleOrNone() => source.TrySingleOrNone(None);

        public Option<Expected<T>> TrySingleOrNone(Func<T, bool> predicate)
            => source.TrySingleOrNone(Some(predicate));

        private Option<Expected<T>> TrySingleOrNone(Option<Func<T, bool>> predicate)
        {
            using var iterator = predicate
                .Select(source.Where)
                .IfNone(source)
                .GetEnumerator();

            Option<Expected<T>> value = None;

            if (iterator.MoveNext())
            {
                value = Okay(iterator.Current);
            }

            if (iterator.MoveNext())
            {
                value = Fail<T>(Error.MoreThanOneElement);
            }

            return value;
        }
    }
}
