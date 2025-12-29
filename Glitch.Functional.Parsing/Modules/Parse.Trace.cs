namespace Glitch.Functional.Parsing;

public partial class Parse<TToken>
{
    // UNDONE
    public static IParser<TToken, T> Trace<T>(IParser<TToken, T> parser) => parser.Trace();
}
