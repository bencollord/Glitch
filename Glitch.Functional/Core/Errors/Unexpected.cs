using Glitch.Functional.Errors;

namespace Glitch.Functional.Core.Errors;

public record Unexpected<T>(T Value) : Error((int)GlobalErrorCode.Unexpected, $"Unexpected {Value}");
