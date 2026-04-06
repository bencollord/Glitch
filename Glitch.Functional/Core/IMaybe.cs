namespace Glitch.Functional;

public interface IMaybe
{
    bool HasValue { get; }
}

public interface IMaybe<out T> : IMaybe
{
    TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none);
}
