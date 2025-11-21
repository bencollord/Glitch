using Glitch.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Glitch.Json;

public static class JObjectExtensions
{
    extension(JObject json)
    {
        public static JObject Load(FilePath path) =>
            Load(new FileInfo(path));

        public static JObject Load(FileInfo file)
        {
            using var reader = file.OpenText();

            return Load(reader);
        }

        public static JObject Load(TextReader reader)
        {
            using var jsonReader = new JsonTextReader(reader);

            return JObject.Load(jsonReader);
        }
    }
}
