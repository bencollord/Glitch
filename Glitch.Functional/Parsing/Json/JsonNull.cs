using Glitch.Text;

namespace Glitch.Functional.Parsing.Json
{
    public record JsonNull : JsonValue
    {
        public static readonly JsonNull Value = new();

        private JsonNull() { }

        protected internal override void WriteTo(IndentedStringBuilder buffer)
        {
            buffer.Append("null");
        }
    }
}
