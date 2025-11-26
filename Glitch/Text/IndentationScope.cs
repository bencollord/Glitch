namespace Glitch.Text;

public class IndentationScope : IDisposable
{
    private IIndentable inner;

    public IndentationScope(IIndentable inner)
    {
        this.inner = inner;
        inner.Indentation++;
    }

    public void Dispose()
    {
        inner.Indentation--;
    }
}
