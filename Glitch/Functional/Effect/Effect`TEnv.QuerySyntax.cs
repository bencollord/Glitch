namespace Glitch.Functional
{
    public static partial class EffectExtensions
    {
        public static Effect<TInput, TResult> Select<TInput, TOutput, TResult>(this Effect<TInput, TOutput> source, Func<TOutput, TResult> mapper)
            => source.Map(mapper);

        public static Effect<TInput, TResult> SelectMany<TInput, TOutput, TResult>(this Effect<TInput, TOutput> source, Func<TOutput, Effect<TInput, TResult>> bind)
            => source.AndThen(bind);

        public static Effect<TInput, TResult> SelectMany<TInput, TOutput, TElement, TResult>(this Effect<TInput, TOutput> source, Func<TOutput, Effect<TInput, TElement>> bind, Func<TOutput, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Effect<TInput, TResult> SelectMany<TInput, TOutput, TResult>(this Effect<TInput, TOutput> source, Func<TOutput, Result<TResult>> bind)
            => source.AndThen(bind);

        public static Effect<TInput, TResult> SelectMany<TInput, TOutput, TElement, TResult>(this Effect<TInput, TOutput> source, Func<TOutput, Result<TElement>> bind, Func<TOutput, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Effect<TInput, TResult> SelectMany<TInput, TOutput, TResult>(this Effect<TInput, TOutput> source, Func<TOutput, Effect<TResult>> bind)
           => source.AndThen(bind);

        public static Effect<TInput, TResult> SelectMany<TInput, TOutput, TElement, TResult>(this Effect<TInput, TOutput> source, Func<TOutput, Effect<TElement>> bind, Func<TOutput, TElement, TResult> bindMap)
            => source.AndThen(s => bind(s).Map(e => bindMap(s, e)));

        public static Effect<TInput, TOutput> Where<TInput, TOutput>(this Effect<TInput, TOutput> source, Func<TOutput, bool> predicate)
            => source.Filter(predicate);
    }
}
