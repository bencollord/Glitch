namespace Glitch.Functional.Results
{
    public class Guard<T, E> : IResult<T, E>
    {
        private IResult<T, E> inner;
        private Func<T, bool> predicate;
        private Func<T, E> error;

        internal Guard(IResult<T, E> inner, Func<T, bool> predicate, Func<T, E> error)
        {
            this.inner = inner;
            this.predicate = predicate;
            this.error = error;
        }

        public bool IsOkay => inner.IsOkayAnd(predicate);

        public bool IsError => inner.IsErrorOr(predicate.Not());

        public TResult Match<TResult>(Func<T, TResult> okay, Func<E, TResult> error)
        {
            throw new NotImplementedException();
        }
    }
}
