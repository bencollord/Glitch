using Glitch.Functional.Errors;

namespace Glitch.Functional;

public partial record Result<T>
{
    public record Fail(Error Error) : Result<T>(Result.Fail<T, Error>(Error));
}