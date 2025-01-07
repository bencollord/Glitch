namespace Glitch
{
    public static class StringExtensions
    {
        public static string Capitalize(this string input) => Char.ToUpper(input[0]) + input.Substring(1);

        public static string Decapitalize(this string input) => Char.ToLower(input[0]) + input.Substring(1);

        public static unsafe string Strip(this string input, char target)
        {
            char* result = stackalloc char[input.Length];
            char* current = result;

            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] != target)
                {
                    *current++ = input[i];
                }
            }

            return new String(result, 0, (int)(current - result));
        }

        public static string Strip(this string input, string target) => input.Replace(target, String.Empty);

        public static string Join(this IEnumerable<string> items) => items.Join(String.Empty);

        public static string Join(this IEnumerable<string> items, string separator)
        {
            return String.Join(separator, items);
        }

        public static string JoinLines(this IEnumerable<string> items)
        {
            return String.Join(Environment.NewLine, items);
        }

        public static string JoinLines(this IEnumerable<string> items, string separator)
        {
            return String.Join(separator + Environment.NewLine, items);
        }

        public static string[] SplitLines(this string input) => input.Split(Environment.NewLine);

        public static string Quote(this string input) => input.Wrap('"');

        public static string Wrap(this string input, char delimiter) => input.Wrap(delimiter, delimiter);
        public static string Wrap(this string input, char left, char right) => $"{left}{input}{right}";

        public static string Wrap(this string input, string delimiter) => input.Wrap(delimiter, delimiter);
        public static string Wrap(this string input, string left, string right) => $"{left}{input}{right}";

        public static string Coalesce(this string input, string fallback)
            => String.IsNullOrEmpty(input) ? fallback : input;

        public static string Slice(this string input, int start, int end)
        {
            return input.Substring(start, end - start);
        }

        public static string TrimStart(this string input, string trim)
            => input.StartsWith(trim) ? input.Substring(trim.Length) : input;

        public static string TrimEnd(this string input, string trim)
            => input.EndsWith(trim) ? input.Substring(0, input.Length - trim.Length) : input;
    }
}
