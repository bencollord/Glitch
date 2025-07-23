using System.Globalization;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<char, int> Int
           => Digit.AtLeastOnce().Token().Map(int.Parse);

        public static Parser<char, decimal> Decimal
        {
            get
            {
                var fractional = from num in Numeric.Maybe()
                                 let format = CultureInfo.CurrentCulture.NumberFormat
                                 from dot in Literal(format.NumberDecimalSeparator)
                                 from part in Numeric
                                 select num.Match(
                                     ifSome: n => num + dot + part,
                                     ifNone: dot + part);

                return fractional.Or(Numeric).Map(decimal.Parse);
            }
        }
    }
}
