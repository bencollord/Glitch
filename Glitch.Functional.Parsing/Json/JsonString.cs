using Glitch.Text;

namespace Glitch.Functional.Parsing.Json;

public record JsonString(string Value) : JsonValue
{
    protected internal override void WriteTo(IndentedStringBuilder buffer)
    {
        buffer.Append('"').Append(Value).Append('"');
    }
}
