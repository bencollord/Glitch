using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Glitch.Functional.Parsing;

namespace Glitch.Functional.Parsing.Json;

using static Parse;

public static class JsonParse
{
    private static Parser<char, string> EscapeChar => Char('\\').Select(c => new string(Enumerable.Repeat(c, 2).ToArray()));

    public static Parser<char, char> SingleQuote => Char('\'');

    public static Parser<char, char> DoubleQuote => Char('"');

    public static Parser<char, string> QuotedString
        => from quote in SingleQuote | DoubleQuote
           from text in AnyChar.Except(quote)
                               .ZeroOrMoreTimes()
                               .AsString()
           from unquote in Parse.Token(quote)
           select text;

    public static Parser<char, char> ArrayStart => Char('[') >>> SkipWhitespace;
    public static Parser<char, char> ArrayEnd => Char(']') >>> SkipWhitespace;
    public static Parser<char, char> ObjectStart => Char('{') >>> SkipWhitespace;
    public static Parser<char, char> ObjectEnd => Char('}') >>> SkipWhitespace;
    public static Parser<char, char> Comma => Char(',') >>> SkipWhitespace;

    public static Parser<char, JsonValue> String =>
        QuotedString.Select<JsonValue>(s => new JsonString(s));

    public static Parser<char, JsonValue> Number =>
        Parse.Numeric
             .Select(double.Parse)
             .Select<JsonValue>(d => new JsonNumber(d));

    public static Parser<char, JsonValue> True => Literal("true").Return<JsonValue>(new JsonBoolean(false));
    public static Parser<char, JsonValue> False => Literal("false").Return<JsonValue>(new JsonBoolean(false));

    public static Parser<char, JsonValue> Boolean => True | False;

    public static Parser<char, JsonValue> Null =>
        Literal("null")
            .Select<JsonValue>(_ => JsonNull.Value);

    public static Parser<char, JsonValue> Primitive =>
        String | Number | Boolean | Null;

    public static Parser<char, JsonNode> Value =>
        Primitive.Cast<JsonNode>() |
        Reference(() => Object).Cast<JsonNode>() |
        Reference(() => Array).Cast<JsonNode>();

    public static Parser<char, JsonArray> Array =>
        from open in ArrayStart
        from items in Value.SeparatedBy(Comma).ZeroOrMoreTimes()
        from close in ArrayEnd
        select new JsonArray(items);

    public static Parser<char, JsonProperty> Property =>
        from name in QuotedString
        from _ in Token(':')
        from value in Value
        select new JsonProperty(name, value);

    public static Parser<char, JsonObject> Object =>
        from _x in ObjectStart
        from properties in Property.SeparatedBy(Comma).ZeroOrMoreTimes()
        from _ in SkipWhitespace
        from _y in ObjectEnd
        select new JsonObject(properties);

    private static Parser<char, Unit> SkipWhitespace
        => Whitespace.ZeroOrMoreTimes().IgnoreResult();

    private static Parser<char, char> Token(char c) =>
        from _0 in SkipWhitespace
        from x in Char(c)
        from _1 in SkipWhitespace
        select x;
}
