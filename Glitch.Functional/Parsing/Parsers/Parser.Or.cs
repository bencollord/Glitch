namespace Glitch.Functional.Parsing.Legacy;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, T> Or(Parser<TToken, T> other) => Legacy.Parse.OneOf(this, other);
}