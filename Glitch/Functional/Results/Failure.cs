namespace Glitch.Functional.Results
{
    public readonly record struct Failure<E>(E Error) : Fail<E>
    {
        public override string ToString() => $"Fail({Error})";

        public static implicit operator Failure<E>(E error) => new(error);

        public static implicit operator E(Failure<E> failure) => failure.Error;
    }
}
