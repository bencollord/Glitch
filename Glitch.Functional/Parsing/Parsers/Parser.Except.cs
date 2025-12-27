namespace Glitch.Functional.Parsing.Legacy;

public abstract partial class Parser<TToken, T>
{
    public Parser<TToken, T> Except<TOther>(Parser<TToken, TOther> other) => other.Not().Then(this);
}