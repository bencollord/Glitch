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

        public override T Run(IOEnv env)
        {
            try
            {
                return source.Run(env);
            }
            catch (ApplicationErrorException err) when (IsMatch(err.Error))
            {
                return next(err.Error).Run(env);
            }
            catch (Exception e) when (IsMatch(e))
            {
                return next(e).Run(env);
            }
        }

        private bool IsMatch(Error err) => filter.IsSomeAnd(fn => fn(err)) || filter.IsNone;
    }
}