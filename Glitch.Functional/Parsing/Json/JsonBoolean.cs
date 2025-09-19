using Glitch.Text;

namespace Glitch.Functional.Parsing.Json
{
    public record JsonBoolean(bool Value) : JsonValue
    {
        protected internal override void WriteTo(IndentedStringBuilder buffer)
        {
            buffer.Append(Value.ToString().ToLowerInvariant());
        }
    }
}
