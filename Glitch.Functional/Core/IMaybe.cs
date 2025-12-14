namespace Glitch.Functional;

public interface IMaybe<T>
{
    public abstract bool HasValue { get; }

    public abstract T UnwrapOrElse(Func<T> fallback);
}
