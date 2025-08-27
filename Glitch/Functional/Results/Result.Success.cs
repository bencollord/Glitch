using Glitch.Functional.Results;

namespace Glitch.Functional.Results
{
    public static partial class Result
    {
        public sealed record Success<T>(T Value) : Result<T>(Expected.Okay<T, Error>(Value));
    }
}
