namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<char, int> Int(string input)
            => Digit.AtLeastOnce().Token().Try(int.Parse);

        public static Parser<char, decimal> Decimal(string input)
        {
            var numeric = Digit.AtLeastOnce().Token();
            var fractional = from dot in Char('.')
                             from frac in numeric
                             select dot + frac;

            var token = from whole in numeric
                        from frac in fractional.Maybe()
                        select frac.Match(ifSome: f => whole + f,
                                          ifNone: whole);

            return token.Try(decimal.Parse);
        }

        public static Parser<char, TEnum> Enum<TEnum>(string name)
            where TEnum : struct, Enum
            => Literal(name).Then(n => System.Enum.TryParse<TEnum>(n, out var e)
                                        ? Parser<char>.Return(e)
                                        : Parser<char>.Error<TEnum>($"{n} was not a valid {typeof(TEnum)} value"));
    }
}
