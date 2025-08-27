using Glitch.Functional.Results;

namespace Glitch.Functional
{
    internal class CatchIO<T, TError> : IO<T>
    {
        private IO<T> source;
        private Func<TError, IO<T>> next;
        private Option<Func<TError, bool>> filter;

        public CatchIO(IO<T> source, Func<TError, IO<T>> next, Func<TError, bool>? filter = null)
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
            catch (ApplicationErrorException err) when (err.Error is TError er && IsMatch(er))
            {
                return next(er).Run(env);
            }
            catch (Exception e) when (e is TError ex && IsMatch(ex))
            {
                return next(ex).Run(env);
            }
        }

        private bool IsMatch(TError err) => filter.IsSomeAnd(fn => fn(err)) || filter.IsNone;
    }
}