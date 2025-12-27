
namespace Glitch.Functional.Parsing;

public interface ITokenParser<TToken> : IParser<TToken, TToken>
{
    /// <summary>
    /// Marks this <see cref="ITokenParser{TToken}"/> with the provided <paramref name="label"/>.
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    ITokenParser<TToken> WithLabel(string label);

    /// <summary>
    /// Excludes <paramref name="token"/>, causing the parser to fail when encountered.
    /// </summary>
    /// <remarks>
    /// <inheritdoc cref="Except(Func{TToken, bool})"/>.
    /// </remarks>
    /// <param name="token"></param>
    /// <returns></returns>
    ITokenParser<TToken> Except(TToken token);

    /// <summary>
    /// Excludes any tokens matching <paramref name="predicate"/>, causing the parser to fail when encounterd.
    /// </summary>
    /// <remarks>
    /// Can be used to filter out tokens when the underlying parser would otherwise match.
    /// This is only possible at the token level when dealing with greedy matchers, such as
    /// <see cref="Parse.AnyChar"/>.
    /// </remarks>
    /// <param name="predicate"></param>
    /// <returns></returns>
    ITokenParser<TToken> Except(Func<TToken, bool> predicate);
}