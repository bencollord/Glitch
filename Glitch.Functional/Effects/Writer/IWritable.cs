namespace Glitch.Functional.Effects;

public interface IWritable<W>
    where W : IWritable<W>
{
    public static abstract W Empty { get; }

    W Append(W output);

    public static virtual W operator +(IWritable<W> writable, W output) => writable.Append(output);
}
