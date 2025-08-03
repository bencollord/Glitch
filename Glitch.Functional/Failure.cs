namespace Glitch.Functional
{
    public readonly record struct Failure<T>(T Error)
    {
        public Failure<TResult> Map<TResult>(Func<T, TResult> map) => new(map(Error));

        public Failure<TResult> Apply<TResult>(Failure<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        public Failure<TResult> AndThen<TResult>(Func<T, Failure<TResult>> bind)
            => bind(Error);

        public Failure<TResult> AndThen<TElement, TResult>(Func<T, Failure<TElement>> bind, Func<T, TElement, TResult> project)
            => new(project(Error, bind(Error).Error));

        public Failure<TResult> Cast<TResult>()
            => new((TResult)(dynamic)Error!);

        public override string ToString() 
            => $"Fail({Error})";

        public static implicit operator Failure<T>(T error) => new(error);

        public static implicit operator T(Failure<T> failure) => failure.Error;
    }
}
