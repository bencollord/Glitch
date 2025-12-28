namespace Glitch.Functional.Parsing;

public partial class Parse<TToken>
{
    public static IParser<TToken, ParseState<TToken>> State => ParseState<TToken>.Get;
}
