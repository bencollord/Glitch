using Glitch.Functional.Results;

namespace Glitch.Functional
{
    internal class CatchIO<T> : IO<T>
    {
        private IO<T> source;
        private Func<Error, IO<T>> next;
        private Option<Func<Error, bool>> filter;

        public CatchIO(IO<T> source, Func<Error, IO<T>> next, Func<Error, bool>? filter = null)
        {
            this.source = source;
            this.next = next;
            this.filter = Option.Maybe(filter);
        }

        protected override async Task<T> RunIOAsync(IOEnv env)
        {
            try
            {
                return await source.RunAsync(env).ConfigureAwait(false);
            }
            catch (ApplicationErrorException err) when (IsMatch(err.Error))
            {
                return await next(err.Error).RunAsync(env).ConfigureAwait(false);
            }
            catch (Exception e) when (IsMatch(e))
            {
                return await next(e).RunAsync(env).ConfigureAwait(false);
            }
        }

        private bool IsMatch(Error err) => filter.IsSomeAnd(fn => fn(err)) || filter.IsNone;
    }
}