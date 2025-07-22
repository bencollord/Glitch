namespace Glitch.Functional
{
    internal class EffectRecovery<TInput, TSource> : IEffect<TInput, TSource>
    {
        private IEffect<TInput, TSource> source;
        private Func<Error, IEffect<TInput, TSource>> next;

        internal EffectRecovery(IEffect<TInput, TSource> source, Func<Error, IEffect<TInput, TSource>> next)
        {
            this.source = source;
            this.next = next;
        }

        public IEffect<TInput, TSource> Guard(Func<TSource, bool> predicate, Func<TSource, Error> error)
        {
            throw new NotImplementedException();
        }

        public IEffect<TInput, TResult> Map<TResult>(Func<TSource, TResult> map)
        {
            throw new NotImplementedException();
        }

        public IEffect<TInput, TSource> MapError(Func<Error, Error> map)
        {
            throw new NotImplementedException();
        }

        public IEffect<TInput, TResult> Match<TResult>(Func<TSource, TResult> ifOkay, Func<Error, TResult> ifFail)
        {
            throw new NotImplementedException();
        }

        public IResult<TSource, Error> Run(TInput input)
        {
            throw new NotImplementedException();
        }
    }
}