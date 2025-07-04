namespace Glitch.Functional
{
    public record Failure<T>(T Error)
    {
        public Failure<TResult> Map<TResult>(Func<T, TResult> map) => new(map(Error));

        public Failure<TResult> AndThen<TResult>(Func<T, Failure<TResult>> bind) => bind(Error);
    }
}
