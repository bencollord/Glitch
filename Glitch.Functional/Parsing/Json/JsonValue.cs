namespace Glitch.Functional.Parsing.Legacy.Json;

public abstract record JsonValue : JsonNode
{
    public override IEnumerable<JsonNode> Children() => [];
}
