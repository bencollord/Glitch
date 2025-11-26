

namespace Glitch.Functional.Parsing.Input;

public record EmptyTokenSequence<TToken> : TokenSequence<TToken>
{
    public static readonly EmptyTokenSequence<TToken> Singleton = new();

    private EmptyTokenSequence() { }

    public override TToken Current => default!;

    public override int Position => 0;

    public override bool IsEnd => true;

    public override TokenSequence<TToken> Advance() => this;

    public override TokenSequence<TToken> Advance(int count) => this;

    public override ReadOnlySpan<TToken> Lookback(int count) 
        => count == 0 ? [] : throw new IndexOutOfRangeException($"Cannot look back {count} tokens");

    public override IEnumerable<TToken> ReadToEnd() => [];

    protected override string DisplayRemainder() => string.Empty;
}
