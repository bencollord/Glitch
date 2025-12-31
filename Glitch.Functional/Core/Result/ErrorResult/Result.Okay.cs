
namespace Glitch.Functional;

public partial record Result<T>
{
    public record Okay(T Value) : Result<T>(Result.Okay<T, Error>(Value));
}