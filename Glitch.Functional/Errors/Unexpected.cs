namespace Glitch.Functional.Errors;

public record Unexpected<T>(T Value) : Error((int)GlobalErrorCode.Unexpected, $"Unexpected {Value}");
