namespace Glitch.Text;

public readonly struct IndentationScope : IDisposable
{
    private readonly IIndentable inner;

    public IndentationScope(IIndentable inner)
    {
        this.inner = inner;
        inner.Indentation++;
    }

    public static IndentationScope Begin(IIndentable indentable) => new(indentable);

    public void End() => Dispose();

    public void Dispose()
    {
        inner.Indentation--;
    }
}
