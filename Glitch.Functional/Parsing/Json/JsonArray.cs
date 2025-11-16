using System.Collections;
using System.Collections.Immutable;

namespace Glitch.Functional.Parsing.Json
{
    public record JsonArray : JsonNode, IEnumerable<JsonNode>
    {
        private ImmutableArray<JsonNode> items;

        public JsonArray(params IEnumerable<JsonNode> items)
        {
            this.items = [.. items];
        }

        public JsonNode this[int i] => items[i];

        public override IEnumerable<JsonNode> Children() => items;

        public JsonArray Add(JsonNode node) => new(items.Add(node));
        public JsonArray AddRange(IEnumerable<JsonNode> nodes) => new(items.AddRange(nodes));
        public JsonArray Remove(JsonNode node) => new(items.Remove(node));
        public JsonArray Clear() => new([]);

        public IEnumerator<JsonNode> GetEnumerator()
        {
            foreach (var item in items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected internal override void WriteTo(IndentedStringBuilder buffer)
        {
            buffer.Append('[').AppendLine();

            using (buffer.BeginBlock())
            {
                foreach (var i in items)
                {
                    i.WriteTo(buffer);
                    buffer.Append(',').AppendLine();
                }
            }

            buffer.Append(']');
        }
    }
}
