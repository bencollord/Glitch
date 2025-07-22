namespace Glitch.Functional
{
    internal class EffectContinuation<TInput, TSource, TNext> : IEffect<TInput, TNext>
    {
        private IEffect<TInput, TSource> source;
        private Func<TSource, IEffect<TInput, TNext>> next;

        internal EffectContinuation(IEffect<TInput, TSource> source, Func<TSource, IEffect<TInput, TNext>> next)
        {
            this.source = source;
            this.next = next;
        }

        public IEffect<TInput, TNext> Guard(Func<TNext, bool> predicate, Func<TNext, Error> error)
        {
            return new EffectContinuation<TInput, TSource, TNext>(source, src => next(src).Guard(predicate, error));
        }

        public IEffect<TInput, TResult> Map<TResult>(Func<TNext, TResult> map)
        {
            return new EffectContinuation<TInput, TSource, TResult>(source, src => next(src).Map(map));

        }

        public IEffect<TInput, TNext> MapError(Func<Error, Error> map)
        {
            return new EffectContinuation<TInput, TSource, TNext>(source, src => next(src).MapError(map));
        }

        public IEffect<TInput, TResult> Match<TResult>(Func<TNext, TResult> ifOkay, Func<Error, TResult> ifFail)
        {
            return new EffectContinuation<TInput, TSource, TResult>(source, src => next(src).Match(ifOkay, ifFail));
        }

        public IResult<TNext, Error> Run(TInput input)
        {
            return from src in source.Run(input)
                   from nxt in next(src).Run(input)
                   select nxt;
        }
    }
}