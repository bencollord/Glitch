namespace Glitch.Functional.Parsing.Input;

public record ArrayTokenSequence<TToken> : TokenSequence<TToken>
{
    private TToken[] tokens;
    private int cursor;

    public ArrayTokenSequence(IEnumerable<TToken> tokens)
    {
        this.tokens = tokens.ToArray();
        cursor = 0;
    }

    /// <summary>
        /// <inheritdoc />
        /// </summary>
    public override TToken Current => !IsEnd ? tokens[cursor] : default!; // Suppress null warnings. It's the caller's responsibility to check the IsEnd property.
    
    public override int Position => cursor;

    public override bool IsEnd => cursor >= tokens.Length;

    public override TokenSequence<TToken> Advance()
    {
        return !IsEnd ? this with { cursor = cursor + 1 } : this;
    }

    public override TokenSequence<TToken> Advance(int count)
    {
        var nextPosition = cursor + count;

        return this with { cursor = Math.Min(nextPosition, tokens.Length) };
    }

    public override ReadOnlySpan<TToken> Lookback(int count)
    {
        int back = Position - count;

        return tokens[back..Position];
    }

    public override IEnumerable<TToken> ReadToEnd() => tokens[cursor..];

    protected override string DisplayRemainder() => string.Join(", ", tokens[(cursor + 1)..]);
}
