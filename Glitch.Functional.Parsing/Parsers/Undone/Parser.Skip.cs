using Glitch.Functional;

namespace Glitch.Functional.Parsing.Legacy;

public abstract partial class Parser<TToken, T>
{
    public virtual Parser<TToken, Unit> Skip() => Maybe().IgnoreResult();
}