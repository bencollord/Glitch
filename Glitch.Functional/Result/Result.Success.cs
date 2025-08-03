namespace Glitch.Functional
{
    public static partial class Result
    {
        public sealed record Success<T>(T Value) : Result<T>(Okay<T, Error>(Value));
    }
}
