namespace Glitch.Functional
{
    public readonly record struct Failure<TError>(TError Error) : IEquatable<Failure<TError>>
    {
        public Failure<TResult> Map<TResult>(Func<TError, TResult> map) => new(map(Error));

        public Failure<TResult> OrElse<TResult>(Func<TError, Failure<TResult>> bind)
            => bind(Error);

        public Failure<TResult> OrElse<TElement, TResult>(Func<TError, Failure<TElement>> bind, Func<TError, TElement, TResult> project)
            => new(project(Error, bind(Error).Error));

        public Failure<TResult> Cast<TResult>()
            => new((TResult)(dynamic)Error!);
        
        public override string ToString()
            => $"Error({Error})";

        public static implicit operator Failure<TError>(TError error) => new(error);

        public static implicit operator TError(Failure<TError> failure) => failure.Error;
    }
}