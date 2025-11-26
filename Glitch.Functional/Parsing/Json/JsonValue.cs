namespace Glitch.Functional.Parsing.Json;

public abstract record JsonValue : JsonNode
{
    public override IEnumerable<JsonNode> Children() => [];
}
