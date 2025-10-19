namespace Glitch.Functional.Results
{
    public readonly record struct Okay<T>(T Value)
    {
        public Result<T, E> ToResult<E>() => Result.Okay<T, E>(Value);

        public override string ToString() => $"Okay({Value})";

        public static implicit operator Okay<T>(T value) => new(value);

        public static implicit operator T(Okay<T> success) => success.Value;
    }
}
