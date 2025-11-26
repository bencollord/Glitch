namespace Glitch.Functional.Parsing;

using static Parse;

public abstract partial class Parser<TToken, T>
{
    public virtual SeparatedByParser<TToken, T, TToken> SeparatedBy(TToken token)
        => SeparatedBy(Token(token));

    public virtual SeparatedByParser<TToken, T, TSeparator> SeparatedBy<TSeparator>(Parser<TToken, TSeparator> separator)
        => new(this, separator);
}
