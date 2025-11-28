namespace Glitch.Functional.Effects;

public record WriteResult<W, T>(T Value, W Output)
    where W : IWritable<W>
{
    public static implicit operator WriteResult<W, T>((T Value, W Output) tuple) => new(tuple.Value, tuple.Output);
}
