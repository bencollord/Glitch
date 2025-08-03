using Glitch.Functional.Parsing.Parsers;

namespace Glitch.Functional.Parsing
{
    public abstract partial class Parser<TToken, T>
    {
        public virtual Parser<TToken, TResult> Map<TResult>(Func<T, TResult> selector)
            => new MapParser<TToken, T, TResult>(this, selector);
    }
}