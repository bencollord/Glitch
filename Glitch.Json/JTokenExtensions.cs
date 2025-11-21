using Glitch.Functional;
using Glitch.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Glitch.Json;

public static class JTokenExtensions
{
    public static TToken? SelectToken<TToken>(this JToken token, string path)
    {
        var found = token.SelectToken(path);
        
        return found is not null ? DynamicCast<TToken>.From(found) : default;
    }

    public static IEnumerable<TToken> SelectTokens<TToken>(this JToken token, string path)
    {
        var found = token.SelectTokens(path);

        return found.Select(DynamicCast<TToken>.From);
    }

    public static JArray ToJsonArray(this IEnumerable<JToken> tokens) => new(tokens);

    public static Unit WriteTo(this JToken token, FilePath path) => token.WriteTo(new JsonFile(path));

    public static Unit WriteTo(this JToken token, FileInfo file) => token.WriteTo(new JsonFile(file));

    public static Unit WriteTo(this JToken token, JsonFile file) => file.Write(token);

    extension(JToken json)
    {
        public static JToken Load(FilePath path) =>
            Load(new FileInfo(path));

        public static JToken Load(FileInfo file)
        {
            using var reader = file.OpenText();

            return Load(reader);
        }

        public static JToken Load(TextReader reader)
        {
            using var jsonReader = new JsonTextReader(reader);

            return JToken.Load(jsonReader);
        }
    }
}
