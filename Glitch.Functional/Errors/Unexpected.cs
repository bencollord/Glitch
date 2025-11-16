namespace Glitch.Functional.Errors
{
    public record Unexpected<T>(T Value) : Error((int)Errors.Code.Unexpected, $"Unexpected {Value}");
}
