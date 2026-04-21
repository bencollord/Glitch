using Glitch.Functional;
using Glitch.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Glitch.Json;

public static class JTokenExtensions
{
    public static JArray ToJsonArray(this IEnumerable<JToken> tokens) => new(tokens);

    extension(JToken token)
    {
        public Option<T> ValueOrNone<T>(string key) => Option.Maybe(token.Value<T>(key));

        public Result<T, string> TryValue<T>(string key)
        {
            try
            {
                return token.ValueOrNone<T>(key).OkayOr($"Key {key} not found");
            }
            catch (InvalidCastException)
            {
                return Result.Fail($"Key {key} could not be cast to type {typeof(T)}");
            }
        }

        public Option<TToken> SelectToken<TToken>(string path) =>
            Option.Maybe(token.SelectToken(path)) >>> DynamicCast<TToken>.Try;

        public IEnumerable<TToken> SelectTokens<TToken>(string path)
        {
            var found = token.SelectTokens(path);

            return found.Select(DynamicCast<TToken>.From);
        }

        public Unit WriteTo(FilePath path) => token.WriteTo(new JsonFile(path));

        public Unit WriteTo(FileInfo file) => token.WriteTo(new JsonFile(file));

        public Unit WriteTo(JsonFile file) => file.Write(token);

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
