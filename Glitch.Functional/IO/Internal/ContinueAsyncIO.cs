
namespace Glitch.Functional
{
    internal class ContinueAsyncIO<TSource, TNext, TResult> : IO<TResult>
    {
        private IO<TSource> source;
        private Func<TSource, Task<IO<TNext>>> next;
        private Func<TSource, TNext, TResult> project;

        internal ContinueAsyncIO(IO<TSource> source, Func<TSource, Task<IO<TNext>>> next, Func<TSource, TNext, TResult> project)
        {
            this.source = source;
            this.next = next;
            this.project = project;
        }

        protected override async Task<TResult> RunIOAsync(IOEnv env)
        {
            var src = await source.RunAsync(env).ConfigureAwait(false);
            var nio = await next(src).ConfigureAwait(false);
            var nxt = await nio.RunAsync(env).ConfigureAwait(false);

            return project(src, nxt);
        }
    }
}