
using Glitch.Functional.Results;

namespace Glitch.Functional.Results
{
    public static partial class Result
    {
        public sealed record Failure<T>(Error Error) : Result<T>(Expected.Fail<T, Error>(Error));
    }
}
