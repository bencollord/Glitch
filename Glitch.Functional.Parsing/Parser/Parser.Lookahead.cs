using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success without consuming any input and a successful result
        /// containing <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <remarks>
        /// Like <see cref="Maybe{TToken, T}(IParser{TToken, T})"/>, but does not advance the input further
        /// </remarks>
        /// <returns></returns>
        public IParser<TToken, Option<T>> Peek() => source.Lookahead().Maybe();

        /// <summary>
        /// Returns a new parser that executes without consuming any input.
        /// </summary>
        /// <remarks>
        /// Succeeds or fails like normal. Use <see cref="Peek{TToken, T}(IParser{TToken, T})"/> to lookahead without failure.
        /// </remarks>
        /// <returns></returns>
        public IParser<TToken, T> Lookahead() => new LookaheadParser<TToken, T>(source);
    }
}
