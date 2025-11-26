namespace Glitch.Glob.Internal.Ast;

/*
       [:alnum:]  [:alpha:]  [:blank:]  [:cntrl:]
       [:digit:]  [:graph:]  [:lower:]  [:print:]
       [:punct:]  [:space:]  [:upper:]  [:xdigit:]
     */
internal class NamedCharacterClass : CharacterClass
{
    internal static readonly NamedCharacterClass Alnum = new NamedCharacterClass(NamedCharacterClassType.Alnum);
    internal static readonly NamedCharacterClass Alpha = new NamedCharacterClass(NamedCharacterClassType.Alpha);
    internal static readonly NamedCharacterClass Digit = new NamedCharacterClass(NamedCharacterClassType.Digit);
    internal static readonly NamedCharacterClass Lower = new NamedCharacterClass(NamedCharacterClassType.Lower);
    internal static readonly NamedCharacterClass Punct = new NamedCharacterClass(NamedCharacterClassType.Punct);
    internal static readonly NamedCharacterClass Space = new NamedCharacterClass(NamedCharacterClassType.Space);
    internal static readonly NamedCharacterClass Upper = new NamedCharacterClass(NamedCharacterClassType.Upper);

    private static readonly Dictionary<NamedCharacterClassType, Func<char, bool>> Matchers = new()
    {
        [NamedCharacterClassType.Alnum] = char.IsLetterOrDigit,
        [NamedCharacterClassType.Alpha] = char.IsLetter,
        [NamedCharacterClassType.Digit] = char.IsDigit,
        [NamedCharacterClassType.Lower] = char.IsLower,
        [NamedCharacterClassType.Punct] = char.IsPunctuation,
        [NamedCharacterClassType.Space] = char.IsWhiteSpace,
        [NamedCharacterClassType.Upper] = char.IsUpper,
    };

    private NamedCharacterClassType type;

    private NamedCharacterClass(NamedCharacterClassType type)
        : base()
    {
        this.type = type;
    }

    protected override string RawText => $":{type}:".ToLower();

    protected override bool IsMatch(char c)
    {
        return Matchers[type](c);
    }

    private enum NamedCharacterClassType
    {
        Alnum,
        Alpha,
        Digit,
        Lower,
        Punct,
        Space,
        Upper,
    }
}
