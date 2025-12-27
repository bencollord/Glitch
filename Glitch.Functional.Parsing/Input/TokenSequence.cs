using System.Text;

namespace Glitch.Functional.Parsing.Input;

public abstract record TokenSequence<TToken> : ITokenSequence<TToken>
{
    public static readonly TokenSequence<TToken> Empty = EmptyTokenSequence<TToken>.Singleton;

    /// <inheritdoc />
    public abstract TToken Current { get; }

    /// <inheritdoc />
    public abstract int Position { get; }

    /// <inheritdoc />
    public abstract bool IsEnd { get; }

    /// <inheritdoc />
    public virtual TToken Peek(int count = 1) => Advance(count).Current;

    /// <inheritdoc />
    public abstract TokenSequence<TToken> Advance();

    /// <inheritdoc />
    public virtual TokenSequence<TToken> Advance(int count)
    {
        var current = this;

        for (int i = 0; i < count && !current.IsEnd; i++)
        {
            current = current.Advance();
        }

        return current;
    }

    /// <inheritdoc />
    public abstract IEnumerable<TToken> ReadToEnd();

    /// <inheritdoc />
    public abstract ReadOnlySpan<TToken> Lookahead(int count);

    /// <inheritdoc />
    public abstract ReadOnlySpan<TToken> Lookback(int count);

    /// <summary>
    /// Returns a string representation of this <see cref="TokenSequence{TToken}"/>,
    /// including the current token, or "EOF" if <see cref="IsEnd"/> is true,
    /// the current position, and a string representation of the remaining tokens.
    /// </summary>
    /// <returns></returns>
    public sealed override string ToString()
    {
        return new StringBuilder()
            .Append(IsEnd ? "EOF" : $"Current: {Current}")
            .Append($", Pos: {Position}")
            .Append($", Remaining: {DisplayRemainder()}")
            .ToString();
    }

    public static implicit operator TokenSequence<TToken>(TToken[] tokens) => new ArrayTokenSequence<TToken>(tokens);

    /// <inheritdoc />
    ITokenSequence<TToken> ITokenSequence<TToken>.Advance() => Advance();

    /// <inheritdoc />
    ITokenSequence<TToken> ITokenSequence<TToken>.Advance(int count) => Advance(count);

    /// <summary>
    /// Returns a string representation of the remaining tokens for debugging.
    /// </summary>
    /// <returns></returns>
    protected abstract string DisplayRemainder();
}
