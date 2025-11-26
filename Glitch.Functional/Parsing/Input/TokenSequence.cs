using System.Text;

namespace Glitch.Functional.Parsing.Input;

public abstract record TokenSequence<TToken>
{
    public static readonly TokenSequence<TToken> Empty = EmptyTokenSequence<TToken>.Singleton;

    /// <summary>
    /// The current token in the sequence.
    /// Invalid if the sequence is at its end.
    /// </summary>
    /// <remarks>
    /// The return value of this property is undefined if <see cref="IsEnd"/>
    /// is true. Inheritors are free to return an empty value, null, or throw an
    /// exception depending on the implementation. It's the caller's responsibility
    /// to check the <see cref="IsEnd"/> property.
    /// </remarks>
    public abstract TToken Current { get; }

    public abstract int Position { get; }

    public abstract bool IsEnd { get; }

    public virtual TToken Peek(int count = 1) => Advance(count).Current;

    public abstract TokenSequence<TToken> Advance();

    public virtual TokenSequence<TToken> Advance(int count)
    {
        var current = this;

        for (int i = 0; i < count && !current.IsEnd; i++)
        {
            current = current.Advance();
        }

        return current;
    }

    public abstract IEnumerable<TToken> ReadToEnd();

    public abstract ReadOnlySpan<TToken> Lookback(int count);

    public sealed override string ToString()
    {
        return new StringBuilder()
            .Append(IsEnd ? "EOF" : $"Current: {Current}")
            .Append($", Remaining: {DisplayRemainder()}")
            .Append($", Pos: {Position}")
            .ToString();
    }

    public static implicit operator TokenSequence<TToken>(TToken[] tokens) => new ArrayTokenSequence<TToken>(tokens);

    protected abstract string DisplayRemainder();
}
