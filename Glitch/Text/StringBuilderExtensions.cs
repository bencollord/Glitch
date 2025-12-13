using System.Text;

namespace Glitch.Text;

public static class StringBuilderExtensions
{
    extension(StringBuilder buffer)
    {
        public StringBuilder AppendIf(bool condition, string text)
        {
            if (condition)
            {
                buffer.Append(text);
            }

            return buffer;
        }

        public string Flush()
        {
            var text = buffer.ToString();
            buffer.Clear();
            return text;
        }
    }
}
