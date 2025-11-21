using Glitch.Functional.Parsing.Parsers;

namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, T> Or(Parser<TToken, T> other)
        => new OneOfParser<TToken, T>(this, other);
}