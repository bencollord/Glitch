namespace Glitch.Functional.Results
{
    public readonly record struct Failure<E>(E Error)
    {
        public override string ToString() 
            => $"Fail({Error})";

        public Result<T, E> Expected<T>() => Result<T, E>.Fail(Error);

        public static implicit operator Failure<E>(E error) => new(error);

        public static implicit operator E(Failure<E> failure) => failure.Error;
    }
}
