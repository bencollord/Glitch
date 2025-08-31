namespace Glitch.Functional
{
    public readonly record struct Failure<T>(T Error)
    {
        public override string ToString() 
            => $"Fail({Error})";

        public static implicit operator Failure<T>(T error) => new(error);

        public static implicit operator T(Failure<T> failure) => failure.Error;
    }
}
