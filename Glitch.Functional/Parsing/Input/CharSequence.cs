
namespace Glitch.Functional.Parsing.Input;

public record CharSequence : TokenSequence<char>
{
    private string sourceText;
    private int cursor;

    public CharSequence(string sourceText)
    {
        this.sourceText = sourceText;
        cursor = 0;
    }

    /// <summary>
        /// <inheritdoc />
        /// </summary>
    public override char Current => !IsEnd ? sourceText[cursor] : '\0';

    public override int Position => cursor;

    public override bool IsEnd => cursor >= sourceText.Length;

    public override TokenSequence<char> Advance()
    {
        return !IsEnd ? this with { cursor = cursor + 1 } : this;
    }

    public override TokenSequence<char> Advance(int count)
    {
        var nextPosition = cursor + count;

        return this with { cursor = Math.Min(nextPosition, sourceText.Length) };
    }

    public override ReadOnlySpan<char> Lookback(int count)
    {
        int back = Position - count;

        return sourceText.AsSpan().Slice(back, Position);
    }

    public override IEnumerable<char> ReadToEnd() => sourceText.Substring(cursor);

    protected override string DisplayRemainder() => sourceText.Substring(cursor + 1);
}
