
namespace Glitch.Functional.Core;

public record Unexpected<T>(T Value) : Error((int)GlobalErrorCode.Unexpected, $"Unexpected {Value}");
