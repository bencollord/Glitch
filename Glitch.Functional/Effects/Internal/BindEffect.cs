namespace Glitch.Functional.Effects.Internal
{
    internal class BindEffect<TInput, T, TNext> : IEffect<TInput, TNext>
    {
        private IEffect<TInput, T> source;
        private Func<T, IEffect<TInput, TNext>> next;

        internal BindEffect(IEffect<TInput, T> source, Func<T, IEffect<TInput, TNext>> next)
        {
            this.source = source;
            this.next = next;
        }

        public TNext Run(TInput input)
        {
            var src = source.Run(input);
            var nxt = next(src);

            return nxt.Run(input);
        }
    }

    internal class BindEffect<TInput, T, TNext, TResult> : IEffect<TInput, TResult>
    {
        private IEffect<TInput, T> source;
        private Func<T, IEffect<TInput, TNext>> next;
        private Func<T, TNext, TResult> project;

        internal BindEffect(IEffect<TInput, T> source, Func<T, IEffect<TInput, TNext>> next, Func<T, TNext, TResult> project)
        {
            this.source = source;
            this.next = next;
            this.project = project;
        }

        public TResult Run(TInput input)
        {
            var src = source.Run(input);
            var nxt = next(src).Run(input);

            return project(src, nxt);
        }
    }
}
