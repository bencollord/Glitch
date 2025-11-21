
namespace Glitch.Functional.Effects
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

        protected override async Task<TResult> RunIOAsync(IOEnv env)
        {
            var src = await source.RunAsync(env).ConfigureAwait(false);
            var nxt = await next(src).RunAsync(env).ConfigureAwait(false);

            return project(src, nxt);
        }
    }
}