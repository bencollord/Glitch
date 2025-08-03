
namespace Glitch.Functional
{
    public static partial class Result
    {
        public sealed record Failure<T>(Error Error) : Result<T>(Fail<T, Error>(Error));
    }
}
