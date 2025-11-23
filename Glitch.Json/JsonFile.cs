using Glitch.Functional.Core;
using Glitch.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Glitch.Json
{
    /// <summary>
    /// Decorator for a <see cref="FileInfo"/> that adds JSON capabilities.
    /// </summary>
    public class JsonFile : IEquatable<JsonFile>, IComparable<JsonFile>
    {
        protected const Formatting DefaultFormatting = Formatting.Indented;

        private FileInfo file;

        public JsonFile(FilePath path)
            : this(new FileInfo(path)) { }

        public JsonFile(FileInfo file)
        {
            this.file = file;
        }

        protected JsonFile(JsonFile copy)
            : this(copy.file) { }

        public string FileName => file.Name;

        public DirectoryInfo? Directory => file.Directory;

        public FilePath Path => file.Path;

        /// <summary>
        /// Creates a new instance of <see cref="JsonFile"/>
        /// from the provided <paramref name="path"/>
        /// </summary>
        /// <remarks>
        /// Simply calls the constructor under the hood. Mostly for convenience
        /// when using higher-order functions like Linq.
        /// </remarks>
        /// <param name="path"></param>
        /// <returns></returns>
        public static JsonFile Create(FilePath path) => new(path);

        /// <summary>
        /// Creates a new instance of <see cref="JsonFile"/>
        /// from the provided <paramref name="file"/>
        /// </summary>
        /// <remarks>
        /// Simply calls the constructor under the hood. Mostly for convenience
        /// when using higher-order functions like Linq.
        /// </remarks>
        /// <param name="file"></param>
        /// <returns></returns>
        public static JsonFile Create(FileInfo file) => new(file);

        public virtual JsonReader OpenRead() => new JsonTextReader(file.OpenText());
        public virtual JsonWriter OpenWrite() => OpenWrite(DefaultFormatting);
        public virtual JsonWriter OpenWrite(Formatting formatting) => new JsonTextWriter(file.CreateText()) { Formatting = formatting };
        public virtual JsonWriter OpenAppend() => OpenWrite(DefaultFormatting);
        public virtual JsonWriter OpenAppend(Formatting formatting) => new JsonTextWriter(file.AppendText()) { Formatting = formatting };

        public virtual JToken Load()
        {
            using var reader = OpenRead();

            return JToken.Load(reader);
        }

        public TToken Load<TToken>()
            where TToken : JToken
            => (TToken)Load();

        public Unit Write(JToken token) => Write(token, DefaultFormatting);

        public virtual Unit Write(JToken token, Formatting formatting)
        {
            using var writer = OpenWrite(formatting);

            token.WriteTo(writer);

            return Unit.Value;
        }

        public FileInfo ToFileInfo() => file;

        public int CompareTo(JsonFile? other)
        {
            if (ReferenceEquals(other, null)) return 1;
            if (ReferenceEquals(other, this)) return 0;

            return Path.CompareTo(other.Path);
        }

        public bool Equals(JsonFile? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return file.FullName.Equals(other.file.FullName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as JsonFile);

        public override int GetHashCode() => file.GetHashCode();

        public override string ToString() => file.ToString();
        public static bool operator ==(JsonFile? left, JsonFile? right) => ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

        public static bool operator !=(JsonFile? left, JsonFile? right) => !(left == right);

        public static bool operator <(JsonFile? left, JsonFile? right) => ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;

        public static bool operator <=(JsonFile? left, JsonFile? right) => ReferenceEquals(left, null) || left.CompareTo(right) <= 0;

        public static bool operator >(JsonFile? left, JsonFile? right) => !ReferenceEquals(left, null) && left.CompareTo(right) > 0;

        public static bool operator >=(JsonFile? left, JsonFile? right) => ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
    }
}
