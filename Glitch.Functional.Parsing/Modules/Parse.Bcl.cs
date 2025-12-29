using System.Globalization;

namespace Glitch.Functional.Parsing;

public partial class Parse
{
    private static readonly Func<int, Func<string, int>> parseInt = radix => input => Convert.ToInt32(input, radix);
    private static readonly IParser<char, char> allowedForUri = LetterOrDigit | OneOf("-._~:/?#[]@!$&'()*+,;%=");

    public static IParser<char, Uri> Uri =>
        from kind in Literal("http")
            .Lookahead()
            .Match(okay: _ => UriKind.Absolute,
                   fail: _ => UriKind.Relative)

        from text in allowedForUri.AtLeastOnce()

        from uri in System.Uri.TryCreate(text, kind, out var u)
                  ? Return(u)
                  : Error<Uri>($"Invalid URI '{text}")
        
        select uri;


    // UNDONE Other numeric types

    public static IParser<char, bool> Boolean =>
        // TODO Case insensitive
        Literal(bool.TrueString.ToLower()).Return(true) | 
        Literal(bool.FalseString.ToLower()).Return(false);

    public static IParser<char, int> Int =>
        Literal("0x") >> Hex * parseInt(16) |
        Literal("0b") >> Binary * parseInt(2) |
        Numeric * parseInt(10);

    public static IParser<char, decimal> Decimal
    {
        get
        {
            var fractional = from ci in Parse<char>.Return(CultureInfo.CurrentCulture)
                             from dot in Literal(ci.NumberFormat.NumberDecimalSeparator)
                             from num in Numeric
                             select dot + num;

            var whole = from n in Numeric
                        from _ in Not(fractional)
                        select n;

            var both = from n in Numeric
                       from f in fractional
                       select n + f;

            return from num in whole
                             | fractional
                             | both
                   select decimal.Parse(num);
        }
    }

    public static IParser<char, TEnum> Enum<TEnum>()
       where TEnum : struct, Enum =>
       OneOf(System.Enum.GetNames<TEnum>().Select(Literal))
           .Select(System.Enum.Parse<TEnum>);
}
