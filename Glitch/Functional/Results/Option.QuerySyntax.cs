using Glitch.Functional.Attributes;

namespace Glitch.Functional.Results
{
    [MonadExtension(typeof(Option<>))]
    public static class OptionQuerySyntax
    {
        public static Option<TResult> SelectMany<T, TResult>(this Option<T> source, Func<T, Option<TResult>> bind)
            => source.AndThen(bind);

        public static Option<TResult> SelectMany<T, TElement, TResult>(this Option<T> source, Func<T, Option<TElement>> bind, Func<T, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Select(e => bindMap(s, e)));

        public static Option<TResult> Join<T, TOther, TKey, TResult>(this Option<T> left, Option<TOther> right, Func<T, TKey> leftKeySelector, Func<TOther, TKey> rightKeySelector, Func<T, TOther, TResult> resultSelector)
            => left.Zip(right,
                    (x, y) => leftKeySelector(x)!.Equals(rightKeySelector(y))
                            ? Option.Some(resultSelector(x, y))
                            : Option<TResult>.None)
                   .Flatten();
    }
}