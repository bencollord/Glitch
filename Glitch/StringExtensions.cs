using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class StringExtensions
    {
        public static bool Contains(this string text, string search, StringComparison comparison) => text.IndexOf(search, comparison) >= 0;

        public static bool ContainsIgnoreCase(this string text, string value) => text.Contains(value, StringComparison.OrdinalIgnoreCase);

        public static bool EqualsIgnoreCase(this string text, string value) => text.Equals(value, StringComparison.OrdinalIgnoreCase);

        public static string Capitalize(this string text) => Char.ToUpper(text[0]) + text.Substring(1);

        public static string Decapitalize(this string text) => Char.ToLower(text[0]) + text.Substring(1);
    }
}
