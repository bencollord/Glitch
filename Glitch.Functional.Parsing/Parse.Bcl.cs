using Glitch.Functional.Parsing.Legacy;
using System.Globalization;

namespace Glitch.Functional.Parsing;

public static partial class Parse
{
    private static readonly Func<int, Func<string, int>> parseInt = radix => input => Convert.ToInt32(input, radix);

    // UNDONE Other numeric types and boolean

    public static IParser<char, int> Int => Numeric.Select(parseInt(10)) | Hex.Select(parseInt(16));

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
