using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Core;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        /// <summary>
        /// Returns a new parser that returns an <see cref="Option{T}"/>
        /// on success without consuming any input and a successful result
        /// containing <see cref="Option{T}.None"/> on failure.
        /// </summary>
        /// <remarks>
        /// Like <see cref="Maybe()"/>, but does not advance the input further
        /// </remarks>
        /// <returns></returns>
        public virtual Parser<TToken, Option<T>> Lookahead()
            => new LookaheadParser<TToken, T>(this);
    }
}