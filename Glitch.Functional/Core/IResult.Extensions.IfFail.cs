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
    extension<T, E>(IResult<T, E> self)
    {
        // UNDONE Need a convention for getting errors.
        public T ThrowIfFail() => self.Match(Identity, e => throw new InvalidOperationException(ErrorMessages.BadUnwrap(e)));

        public T IfFail(Func<E, T> fallback) => self.Match(Identity, fallback);

        public T IfFail(T fallback) => self.Match(Identity, fallback);
    }
}
