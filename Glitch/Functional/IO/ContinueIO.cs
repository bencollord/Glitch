namespace Glitch.Functional
{
    internal class ContinueIO<TEnv, TSource, TNext, TResult> : IO<TEnv, TResult>
    {
        private IO<TEnv, TSource> source;
        private Func<TSource, IO<TEnv, TNext>> next;
        private Func<TSource, TNext, TResult> project;

        internal ContinueIO(IO<TEnv, TSource> source, Func<TSource, IO<TEnv, TNext>> next, Func<TSource, TNext, TResult> project)
        {
            this.source = source;
            this.next = next;
            this.project = project;
        }

        public override TResult Run(TEnv input)
        {
            var src = source.Run(input);
            var nxt = next(src).Run(input);

            return project(src, nxt);
        }
    }
}