namespace Glitch.Functional.Parsing
{
    public static class Parse
    {
        public static Parser<char> Letter => Char(char.IsLetter, "letter");
        public static Parser<char> Numeric => Char(char.IsDigit, "numeric");
        public static Parser<char> Alphanumeric => Char(char.IsLetterOrDigit, "alphanumeric");
        public static Parser<char> Whitespace => Char(char.IsWhiteSpace, "whitespace");

        public static Parser<char> Char(char c) => Char(x => x == c, c.ToString());

        public static Parser<char> Char(Func<char, bool> predicate, string message)
            => new Parser<char>(input =>
                string.IsNullOrEmpty(input)
                    ? ParseResult.Fail<char>("End of input reached")
                    : predicate(input[0])
                    ? ParseResult.Okay(input[0], input[1..])
                    : ParseResult.Fail<char>($"Expected {message}, found {input[0]}", input[1..])
            );
    }
}
