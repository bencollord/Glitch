namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<char, TEnum> Enum<TEnum>()
            where TEnum : struct, Enum
            => OneOf(System.Enum.GetNames<TEnum>().Select(Literal))
                   .Select(System.Enum.Parse<TEnum>);
    }
}
