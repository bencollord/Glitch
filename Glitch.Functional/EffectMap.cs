namespace Glitch.Functional
{
    internal class EffectMap<TInput, TSource, TNext> : IEffect<TInput, TNext>
    {
        private IEffect<TInput, TSource> source;
        private Func<TSource, TNext> next;

        internal EffectMap(IEffect<TInput, TSource> source, Func<TSource, TNext> next)
        {
            this.source = source;
            this.next = next;
        }

        public IEffect<TInput, TNext> Guard(Func<TNext, bool> predicate, Func<TNext, Error> error)
        {
            throw new NotImplementedException();
        }

        public IEffect<TInput, TResult> Map<TResult>(Func<TNext, TResult> map)
        {
            return new EffectMap<TInput, TSource, TResult>(source, next.Then(map));
        }

        public IEffect<TInput, TNext> MapError(Func<Error, Error> map)
        {
            throw new NotImplementedException();
        }

        public IEffect<TInput, TResult> Match<TResult>(Func<TNext, TResult> ifOkay, Func<Error, TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<TNext, Error> Run(TInput input)
        {
            throw new NotImplementedException();
        }
    }
}