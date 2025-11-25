namespace Glitch.Functional.Errors;

public partial record Expected<T>
{
    public record Okay(T Value) : Expected<T>(Result.Okay<T, Error>(Value));
}
