namespace Glitch.Functional.Parsing;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, T> Or(Parser<TToken, T> other) => Parsing.Parse.OneOf(this, other);
}