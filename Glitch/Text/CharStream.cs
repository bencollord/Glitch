namespace Glitch.Text;

public abstract class CharStream : IDisposable
{
    public abstract bool IsEof { get; }

    public static CharStream Create(TextReader stream) => new BufferedCharStream(stream);

    public static CharStream Create(Stream stream) => Create(new StreamReader(stream));

    public static CharStream Create(string text) => Create(new StringReader(text));

    public abstract char Peek();

    public abstract char Read();

    public abstract string ReadToEnd();

    // First parameter enforces at least one character provided to avoid
    // ambiguity with the overload that takes a default count
    public virtual void Consume(char c, params char[] chars)
    {
        while (c == Peek() || chars.Contains(Peek()))
        {
            Read();
        }
    }

    public virtual void Consume(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            _ = Read();
        }
    }

    public abstract void Dispose();
}
