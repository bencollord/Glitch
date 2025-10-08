using Glitch.Functional;
using Glitch.Functional.Results;

namespace Glitch
{
    public static class StringExtensions
    {
        public static string TakeUntil(this string input, char c) => input.Substring(0, input.IndexOf(c));

        public static string Capitalize(this string input) => Char.ToUpper(input[0]) + input.Substring(1);

        public static string Uncapitalize(this string input) => Char.ToLower(input[0]) + input.Substring(1);

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

        public static string Strip(this string input, string target) => input.Replace(target, string.Empty);

        public static string StripStart(this string input, string trim)
            => input.StartsWith(trim) ? input.Substring(trim.Length) : input;

        public static string StripEnd(this string input, string trim)
            => input.EndsWith(trim) ? input.Substring(0, input.Length - trim.Length) : input;

        public static string Join(this IEnumerable<string> items) => items.Join(string.Empty);

        public static string Join(this IEnumerable<string> items, string separator) => string.Join(separator, items);

        public static string Join<T>(this IEnumerable<T> items, string separator) => string.Join(separator, items);

        public static string Join(this IEnumerable<string> items, char separator) => string.Join(separator, items);

        public static string Join<T>(this IEnumerable<T> items, char separator) => string.Join(separator, items);

        public static string JoinLines(this IEnumerable<string> items) => items.Join(Environment.NewLine);

        public static string JoinLines(this IEnumerable<string> items, string separator) => items.Join(separator + Environment.NewLine);

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

        public static T Parse<T>(this string input)
            where T : IParsable<T>
            => input.TryParse<T>().Unwrap();

        public static T Parse<T>(this string input, IFormatProvider? formatProvider)
            where T : IParsable<T>
            => input.TryParse<T>(formatProvider).Unwrap();

        public static Expected<T> TryParse<T>(this string input)
            where T : IParsable<T>
            => input.TryParse<T>(null);

        public static Expected<T> TryParse<T>(this string input, IFormatProvider? formatProvider)
            where T : IParsable<T>
            => Try(() => T.Parse(input, formatProvider)).Run();

        // ------
        public static T ParseEnum<T>(this string input)
            where T : struct, Enum
            => input.TryParseEnum<T>().Unwrap();

        public static T ParseEnum<T>(this string input, bool ignoreCase)
            where T : struct, Enum
            => input.TryParseEnum<T>(ignoreCase).Unwrap();

        public static Expected<T> TryParseEnum<T>(this string input)
            where T : struct, Enum
            => input.TryParseEnum<T>(false);

        public static Expected<T> TryParseEnum<T>(this string input, bool ignoreCase)
            where T : struct, Enum
            => Try(() => Enum.Parse<T>(input, ignoreCase)).Run();
    }
}
