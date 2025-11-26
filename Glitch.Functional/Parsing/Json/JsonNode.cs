using Glitch.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Functional.Parsing.Json;

public abstract record JsonNode
{
    public abstract IEnumerable<JsonNode> Children();

    public IEnumerable<JsonNode> Descendants()
    {
        foreach (var child in Children())
        {
            yield return child;

            foreach (var descendant in child.Descendants())
            {
                yield return descendant;
            }
        }
    }

    public sealed override string ToString()
    {
        var buffer = new IndentedStringBuilder();

        WriteTo(buffer);

        return buffer.ToString();
    }

    protected internal abstract void WriteTo(IndentedStringBuilder buffer);
}
