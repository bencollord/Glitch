namespace Glitch.Functional.Errors;

public partial record Expected<T>
{
    public record Fail(Error Error) : Expected<T>(Result.Fail<T, Error>(Error));
}