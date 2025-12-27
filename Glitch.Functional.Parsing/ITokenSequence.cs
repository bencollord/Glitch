namespace Glitch.Functional.Parsing;

public interface ITokenSequence<TToken>
{
    /// <summary>
    /// Gets the current token the cursor is on in the stream.
    /// Undefined if the sequence is at its end.
    /// </summary>
    /// <remarks>
    /// The return value of this property is undefined if <see cref="IsEnd"/>
    /// is true. Inheritors are free to return an empty value, null, or throw an
    /// exception depending on the implementation. It's the caller's responsibility
    /// to check the <see cref="IsEnd"/> property.
    /// </remarks>
    TToken Current { get; }

    /// <summary>
    /// Returns true if this token sequence has reached the end of input.
    /// </summary>
    bool IsEnd { get; }

    /// <summary>
    /// Gets the ordinal position of the current token.
    /// </summary>
    int Position { get; }

    /// <summary>
    /// Advances forward one token. No op if EOF.
    /// </summary>
    /// <returns></returns>
    ITokenSequence<TToken> Advance();

    /// <summary>
    /// Advances forward <paramref name="count"/> tokens. If <paramref name="count"/> is greater
    /// than the number of tokens left, advances to the end of the stream.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    ITokenSequence<TToken> Advance(int count);

    /// <summary>
    /// Returns the token <paramref name="count"/> positions ahead of the current token without consuming it.
    /// If <paramref name="count"/> is greater than the number of tokens left, returns the last token.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    TToken Peek(int count = 1);

    /// <summary>
    /// Returns a <see cref="ReadOnlySpan{TToken}"/> of <paramref name="count"/> tokens ahead of current.
    /// If <paramref name="count"/> is greater than the number of tokens left, returns a shortened list 
    /// of the remaining tokens.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    ReadOnlySpan<TToken> Lookahead(int count);

    /// <summary>
    /// Returns a <see cref="ReadOnlySpan{TToken}"/> of <paramref name="count"/> tokens behind current.
    /// If <paramref name="count"/> is greater than the number of previous tokens, returns a shortened 
    /// list of all consumed tokens so far.
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    ReadOnlySpan<TToken> Lookback(int count);

    /// <summary>
    /// Returns an <see cref="IEnumerable{TToken}"/> of all tokens remaining.
    /// </summary>
    /// <returns></returns>
    IEnumerable<TToken> ReadToEnd();
}