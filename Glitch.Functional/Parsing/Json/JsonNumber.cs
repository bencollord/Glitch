using Glitch.Text;

namespace Glitch.Functional.Parsing.Legacy.Json;

public record JsonNumber(double Value) : JsonValue
{
    protected internal override void WriteTo(IndentedStringBuilder buffer)
    {
        buffer.Append(Value);
    }
}
