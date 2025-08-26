namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<char, string> Literal(string text)
        {
            return text.Select(Char)
                       .PipeInto(Sequence)
                       .Select(chars => new string([.. chars]));
        }
    }

    public static partial class ParserExtensions
    {
        public static Parser<char, string> AsString(this Parser<char, IEnumerable<char>> parser)
            => parser.Select(chars => new string([..chars]));
    }
}
