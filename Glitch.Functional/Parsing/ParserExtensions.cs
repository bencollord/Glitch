namespace Glitch.Functional.Parsing
{
    public static partial class ParserExtensions
    {
        // TODO This name might be a little confusing because it's overloaded.
        // Perhaps "Text", "String", or "AsString" would be better?
        public static Parser<char, string> Token(this Parser<char, IEnumerable<char>> parser)
            => parser.Map(chars => new string([..chars]));
    }
}
