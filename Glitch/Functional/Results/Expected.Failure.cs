
using Glitch.Functional.Results;

namespace Glitch.Functional.Results
{
    public static partial class Expected
    {
        public sealed record Failure<T>(Error Error) : Expected<T>(Result.Fail<T, Error>(Error));
    }
}
