using Glitch.Functional;

namespace Glitch.Functional.Extensions;

public static partial class LinqExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public TResult Match<TResult>(Func<IEnumerable<T>, TResult> many, TResult none)
        => source.Match(many, _ => none);

        public TResult Match<TResult>(Func<IEnumerable<T>, TResult> many, Func<Unit, TResult> none)
            => source.Match(many, () => none(default));

        public TResult Match<TResult>(Func<IEnumerable<T>, TResult> many, Func<TResult> none) => source.Any() ? many(source) : none();

        public TResult Match<TResult>(Func<T, TResult> just, Func<IEnumerable<T>, TResult> many, TResult none)
            => source.Match(just, many, _ => none);

        public TResult Match<TResult>(Func<T, TResult> just, Func<IEnumerable<T>, TResult> many, Func<Unit, TResult> none)
            => source.Match(just, many, () => none(default));

        public TResult Match<TResult>(Func<T, TResult> just, Func<IEnumerable<T>, TResult> many, Func<TResult> none)
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
    }
}
