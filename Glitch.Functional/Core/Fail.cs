namespace Glitch.Functional.Core
{
    public readonly record struct Fail<E>(E Error)
    {
        public Result<T, E> ToResult<T>() => Result.Fail<T, E>(Error);

        public override string ToString() => $"Fail({Error})";

        public static implicit operator Fail<E>(E error) => new(error);

        public static implicit operator E(Fail<E> failure) => failure.Error;
    }
}
