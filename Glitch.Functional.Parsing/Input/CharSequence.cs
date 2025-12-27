
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

    public static CharSequence From(string sourceText) => new CharSequence(sourceText);
    public static CharSequence From(ReadOnlySpan<char> sourceText) => From(new string(sourceText));
    public static CharSequence From(char[] sourceText) => From(new string(sourceText));
    public static CharSequence From(IEnumerable<char> sourceText) => From([.. sourceText]);

    public override CharSequence Advance()
    {
        return !IsEnd ? this with { cursor = cursor + 1 } : this;
    }

    public override CharSequence Advance(int count)
    {
        var nextPosition = cursor + count;

        return this with { cursor = Math.Min(nextPosition, sourceText.Length) };
    }

    public override ReadOnlySpan<char> Lookahead(int count)
    {
        int ahead = Math.Min(Position + count, sourceText.Length - 1) - Position;

        return sourceText.AsSpan().Slice(Position + 1, ahead);
    }

    public override ReadOnlySpan<char> Lookback(int count)
    {
        int back = Math.Max(Position - count, 0);

        return sourceText.AsSpan().Slice(back, Position);
    }

    public override string ReadToEnd() => sourceText.Substring(cursor);

    protected override string DisplayRemainder() 
        => cursor == sourceText.Length 
         ? "(EOF)" 
         : sourceText.Substring(cursor + 1);
}
