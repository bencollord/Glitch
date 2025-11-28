namespace Glitch.Functional;

/// <summary>
/// Unwrapping and coalescing extensions for <see cref="IResult{T, E}"/>.
/// </summary>
/// <remarks>
/// Experimental since I'm trying to decide on two competing naming conventions.
/// It's between this and Unwrap*
/// </remarks>
public static partial class ResultExtensions
{
    extension<T, E>(IEnumerable<IResult<T, E>> self)
    {
        public IEnumerable<T> Successes() => self.Where(r => r.IsOkay).Select(r => r.Unwrap());

        public IEnumerable<E> Errors() => self.Where(r => r.IsFail).Select(r => r.UnwrapError());

        public (IEnumerable<T> Successes, IEnumerable<E> Errors) Partition() => (self.Successes(), self.Errors());
    }
}
