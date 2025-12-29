namespace Glitch.Functional.Parsing.Json;

using static Parse;

public static class JsonParse
{
    public static IParser<char, char> SingleQuote => Char('\'');

    public static IParser<char, char> DoubleQuote => Char('"');

    public static IParser<char, string> QuotedString
        => from quote in SingleQuote | DoubleQuote
           from text in AnyChar.Except(quote)
                               .ZeroOrMoreTimes()
           from unquote in Char(quote)
           select text;

    public static IParser<char, char> ArrayStart  => Char('[') >> SkipWhitespace;
    public static IParser<char, char> ArrayEnd    => Char(']') >> SkipWhitespace;
    public static IParser<char, char> ObjectStart => Char('{') >> SkipWhitespace;
    public static IParser<char, char> ObjectEnd   => Char('}') >> SkipWhitespace;
    public static IParser<char, char> Comma       => Char(',') >> SkipWhitespace;

    public static IParser<char, JsonValue> String =>
        QuotedString.Select(s => new JsonString(s));

    public static IParser<char, JsonValue> Number =>
        Numeric.Select(double.Parse)
               .Select(d => new JsonNumber(d));

    public static IParser<char, JsonValue> True => Literal("true").Return(new JsonBoolean(false));
    public static IParser<char, JsonValue> False => Literal("false").Return(new JsonBoolean(false));

    public static IParser<char, JsonValue> Boolean => True | False;

    public static IParser<char, JsonValue> Null =>
        Literal("null").Return(JsonNull.Value);

    public static IParser<char, JsonValue> Primitive =>
        String | Number | Boolean | Null;

    public static IParser<char, JsonNode> Value =>
        Primitive.Cast<JsonNode>() |
        Ref(() => Object).Cast<JsonNode>() |
        Ref(() => Array).Cast<JsonNode>();

    public static IParser<char, JsonArray> Array =>
        from open in ArrayStart
        from items in Value.SeparatedBy(Comma).ZeroOrMoreTimes()
        from close in ArrayEnd
        select new JsonArray(items);

    public static IParser<char, JsonProperty> Property =>
        from name in QuotedString
        from _ in Token(':')
        from value in Value
        select new JsonProperty(name, value);

    public static IParser<char, JsonObject> Object =>
        from _x in ObjectStart
        from properties in Property.SeparatedBy(Comma).ZeroOrMoreTimes()
        from _ in SkipWhitespace
        from _y in ObjectEnd
        select new JsonObject(properties);

    private static IParser<char, Unit> SkipWhitespace
        => Whitespace.ZeroOrMoreTimes().Discard();

    private static IParser<char, char> Token(char c) =>
        from _0 in SkipWhitespace
        from x in Char(c)
        from _1 in SkipWhitespace
        select x;
}
