using System.Globalization;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        private static readonly Func<int, Func<string, int>> parseInt = radix => input => Convert.ToInt32(input, radix);

        public static Parser<char, string> Numeric => Digit.AtLeastOnce().AsString();

        public static Parser<char, string> Hex
            => Digit.Or(OneOf("ABCDEF")) // TODO Case-insensitive
                    .Or(OneOf("abcdef"))
                    .AtLeastOnce()
                    .After(Literal("0x").Maybe())
                    .AsString();

        public static Parser<char, int> Int => Numeric.Map(parseInt(10)) | Hex.Map(parseInt(16));

        public static Parser<char, decimal> Decimal
        {
            get
            {
                var fractional = from ci in Parser<char>.Return(CultureInfo.CurrentCulture)
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
    }
}
