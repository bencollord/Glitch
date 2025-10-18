namespace Glitch.Functional.Results
{
    public readonly record struct Success<T>(T Value) : Okay<T>
    {
        public override string ToString() => $"Okay({Value})";

        public static implicit operator Success<T>(T value) => new(value);

        public static implicit operator T(Success<T> success) => success.Value;
    }
}
