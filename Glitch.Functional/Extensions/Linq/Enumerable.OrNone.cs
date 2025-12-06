using Glitch.Functional;
using Glitch.Functional.Errors;

namespace Glitch.Functional.Extensions;

using static Option;

public static partial class LinqExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public Option<T> ElementAtOrNone(int index)
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

        public Option<T> FirstOrNone()
            => source.FirstOrNone(None);

        public Option<T> FirstOrNone(Func<T, bool> predicate)
            => source.FirstOrNone(Some(predicate));

        private Option<T> FirstOrNone(Option<Func<T, bool>> predicate)
        {
            return predicate.Match(
                filter => Maybe(source.FirstOrDefault(filter)),
                () => Maybe(source.FirstOrDefault()));
        }

        public Option<T> LastOrNone()
            => source.LastOrNone(None);

        public Option<T> LastOrNone(Func<T, bool> predicate)
            => source.LastOrNone(Some(predicate));

        private Option<T> LastOrNone(Option<Func<T, bool>> predicate)
        {
            return predicate.Match(
                filter => Maybe(source.LastOrDefault(filter)),
                () => Maybe(source.LastOrDefault()));
        }

        public Option<T> SingleOrNone()
            => source.SingleOrNone(None);

        public Option<T> SingleOrNone(Func<T, bool> predicate)
            => source.SingleOrNone(Some(predicate));

        private Option<T> SingleOrNone(Option<Func<T, bool>> predicate)
            => source.TrySingleOrNone(predicate).Unwrap();
    }
}
