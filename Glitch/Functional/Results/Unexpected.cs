namespace Glitch.Functional.Results
{
    public record Unexpected<T>(T Value) : Error((int)ErrorCode.Unexpected, $"Unexpected {Value}");
}
