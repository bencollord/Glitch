namespace Glitch.Functional.Parsing.Extensions;

public static class StringExtensions
{
    public static string Join(this IEnumerable<string> list, string separator)
        => string.Join(separator, list);
}
