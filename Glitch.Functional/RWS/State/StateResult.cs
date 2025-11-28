namespace Glitch.Functional;

public record StateResult<S, T>(S State, T Value)
{
    public static implicit operator StateResult<S, T>((S State, T Value) tuple) => new(tuple.State, tuple.Value);
}
