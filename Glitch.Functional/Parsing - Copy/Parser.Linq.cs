namespace Glitch.Functional.Parsing
{
    public static partial class Parser
    {
        public static Parser<TIn, TResult> Select<TIn, TOut, TResult>(this Parser<TIn, TOut> parser, Func<TOut, TResult> selector)
            => parser.Map(selector);

        public static Parser<TIn, TResult> SelectMany<TIn, TOut, TResult>(this Parser<TIn, TOut> parser, Func<TOut, Parser<TIn, TResult>> selector)
            => parser.AndThen(selector);

        public static Parser<TIn, TResult> SelectMany<TIn, TOut, TElement, TResult>(this Parser<TIn, TOut> parser, Func<TOut, Parser<TIn, TElement>> selector, Func<TOut, TElement, TResult> projection)
            => parser.AndThen(selector, projection);

        public static Parser<TIn, TOut> Where<TIn, TOut>(this Parser<TIn, TOut> parser, Func<TOut, bool> predicate)
            => parser.Filter(predicate);
    }
}
