namespace Glitch.Functional.Parsing;

public partial class Parse<TToken>
{
    /// <summary>
    /// Private constructor to prevent instances from being created.
    /// </summary>
    /// <remarks>
    /// The <see cref="Parse{TToken}"/> class is for all intents and purposes static,
    /// but it can't be made a static class because we want the non-generic <see cref="Parse"/>
    /// class to be able to inherit from it.
    /// </remarks>
    private protected Parse() { }
}