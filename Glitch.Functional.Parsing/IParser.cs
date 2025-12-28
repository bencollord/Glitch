namespace Glitch.Functional.Parsing;

public interface IParser<TToken, out T>
{
    /// <summary>
    /// Returns a parser which if successful, dynamically casts its result to <typeparamref name="TResult"/>.
    /// </summary>
    /// <remarks>
    /// Attached to the interface to avoid a bunch of generic parameters.
    /// </remarks>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    virtual IParser<TToken, TResult> Cast<TResult>() => this.Select(DynamicCast<TResult>.From);

    IParseResult<TToken, T> Execute(ITokenSequence<TToken> input);
}
