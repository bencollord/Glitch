using Glitch.Text;

namespace Glitch.Functional.Parsing.Json;

public record JsonProperty(string Name, JsonNode Value) : JsonNode
{
    public override IEnumerable<JsonNode> Children()
    {
        yield return Value;
    }

    protected internal override void WriteTo(IndentedStringBuilder buffer)
    {
        buffer.Append("\"")
              .Append(Name)
              .Append('"')
              .Append(':')
              .Append(' ');

        Value.WriteTo(buffer);
    }
}
