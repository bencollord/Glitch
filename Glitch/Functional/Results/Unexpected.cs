namespace Glitch.Functional.Results
{
    public record Unexpected<T>(T Value) : Error((int)Errors.Code.Unexpected, $"Unexpected {Value}");
}
