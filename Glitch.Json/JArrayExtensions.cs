using Glitch.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Glitch.Json;

public static class JArrayExtensions
{
    extension(JArray json)
    {
        public static JArray Load(FilePath path) =>
            Load(new FileInfo(path));

        public static JArray Load(FileInfo file)
        {
            using var reader = file.OpenText();

            return Load(reader);
        }

        public static JArray Load(TextReader reader)
        {
            using var jsonReader = new JsonTextReader(reader);

            return JArray.Load(jsonReader);
        }
    }
}
