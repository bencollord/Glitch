using System.Text;

namespace Glitch.Text
{
    public static class StringBuilderExtensions
    {
        public static string Flush(this StringBuilder buffer)
        {
            var text = buffer.ToString();
            buffer.Clear();
            return text;
        }
    }
}
