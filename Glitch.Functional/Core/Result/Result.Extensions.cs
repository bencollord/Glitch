namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T, E>(Result<Result<T, E>, E> source)
    {
        public Result<T, E> Flatten() => source.AndThen(Identity);
    }

    extension<T, E>(Result<Option<T>, E> nested)
    {
        public Option<Result<T, E>> Invert() =>
            nested.Match(
                okay: opt => opt * Result.Okay<T, E>,
                fail: err => Option.Some(err) * Result.Fail<T, E>);
    }

    extension<T, E>(IEnumerable<Result<T, E>> self)
    {
        public IEnumerable<T> Successes() => self.Where(r => r.IsOkay).Select(r => r.Unwrap());

        public IEnumerable<E> Errors() => self.Where(r => r.IsFail).Select(r => r.UnwrapError());

        public (IEnumerable<T> Successes, IEnumerable<E> Errors) Partition() => self.Partition((oks, errs) => (oks, errs));

        public TResult Partition<TResult>(Func<IEnumerable<T>, IEnumerable<E>, TResult> partition) => partition(self.Successes(), self.Errors());
    }
}