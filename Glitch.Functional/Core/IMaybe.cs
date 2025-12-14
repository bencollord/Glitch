namespace Glitch.Functional;

public interface IMaybe<out T>
{
    public abstract bool HasValue { get; }

    public abstract TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none);
}
