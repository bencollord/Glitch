namespace Glitch.Functional
{
    internal class CatchIO<TEnv, T, TError> : IO<TEnv, T>
    {
        private IO<TEnv, T> source;
        private Func<TError, IO<TEnv, T>> next;
        private Option<Func<TError, bool>> filter;

        public CatchIO(IO<TEnv, T> source, Func<TError, IO<TEnv, T>> next, Func<TError, bool>? filter = null)
        {
            this.source = source;
            this.next = next;
            this.filter = Maybe(filter);
        }

        public override T Run(TEnv input)
        {
            try
            {
                return source.Run(input);
            }
            catch (ApplicationErrorException err) when (err.Error is TError er && IsMatch(er))
            {
                return next(er).Run(input);
            }
            catch (Exception e) when (e is TError ex && IsMatch(ex))
            {
                return next(ex).Run(input);
            }
        }

        private bool IsMatch(TError err) => filter.IsSomeAnd(fn => fn(err)) || filter.IsNone;
    }
}