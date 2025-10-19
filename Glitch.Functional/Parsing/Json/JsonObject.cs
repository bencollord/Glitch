using Glitch.Functional;
using Glitch.Functional.Results;
using Glitch.Text;
using System.Collections;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing.Json
{
    public record JsonObject : JsonNode, IEnumerable<JsonProperty>
    {
        private ImmutableList<JsonProperty> properties;

        public JsonObject(params IEnumerable<JsonProperty> properties)
        {
            this.properties = properties.ToImmutableList();
        }

        public JsonNode this[string name]
            => Property(name)
                   .Select(p => p.Value)
                   .ExpectOrElse(_ => new KeyNotFoundException(name))
                   .Unwrap();

        public JsonObject Add(string name, JsonNode value) => Add(new JsonProperty(name, value));
        public JsonObject Add(JsonProperty property) => this with { properties = properties.Add(property), };
        public JsonObject Remove(string name) => this with
        {
            properties = properties.FindIndex(p => p.Name == name) switch
            {
                -1 => properties,
                int i => properties.RemoveAt(i)
            }
        };

        public JsonObject Clear() => new([]);

        public Option<JsonProperty> Property(string name) => properties.Find(p => p.Name == name);

        public IEnumerable<JsonProperty> Properties() => properties;

        public override IEnumerable<JsonNode> Children() => properties;

        public IEnumerator<JsonProperty> GetEnumerator() => properties.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected internal override void WriteTo(IndentedStringBuilder buffer)
        {
            buffer.Append('{').AppendLine();

            using (buffer.BeginBlock())
            {
                foreach (var p in properties)
                {
                    p.WriteTo(buffer);
                    buffer.Append(',').AppendLine();
                }
            }

            buffer.Append('}');
        }
    }
}
