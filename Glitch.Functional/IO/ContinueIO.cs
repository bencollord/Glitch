namespace Glitch.Functional
{
    internal class ContinueIO<TSource, TNext, TResult> : IO<TResult>
    {
        private IO<TSource> source;
        private Func<TSource, IO<TNext>> next;
        private Func<TSource, TNext, TResult> project;

        internal ContinueIO(IO<TSource> source, Func<TSource, IO<TNext>> next, Func<TSource, TNext, TResult> project)
        {
            this.source = source;
            this.next = next;
            this.project = project;
        }

        public override TResult Run(IOEnv env)
        {
            var src = source.Run(env);
            var nxt = next(src).Run(env);

            return project(src, nxt);
        }
    }
}