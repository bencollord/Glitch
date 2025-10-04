using Glitch.Functional;

namespace Glitch.IO
{
    public sealed partial class FilePath : IEquatable<FilePath>, IComparable<FilePath>
    {
        public static readonly FilePath Empty = new(string.Empty);

        public static char[] InvalidFileNameChars => Path.GetInvalidFileNameChars();
        public static char[] InvalidPathChars => Path.GetInvalidPathChars();
        public static FilePath TempPath => new(Path.GetTempPath());
        public static char DirectorySeparatorChar => Path.DirectorySeparatorChar;
        public static char AltDirectorySeparatorChar => Path.AltDirectorySeparatorChar;

        public static FilePath Create(string path) => new(path);
        public static FilePath Create(FileSystemInfo node) => new(node.FullName);

        public static FilePath GetRandomFileName() => new(Path.GetRandomFileName());
        public static FilePath GetTempFileName() => new(Path.GetTempFileName());

        public static FilePath Combine(string path1, string path2) => new(Path.Combine(path1, path2));
        public static FilePath Combine(FilePath path1, FilePath path2) => Combine(path1.path, path2.path);
        public static FilePath Combine(string path1, string path2, string path3) => new(Path.Combine(path1, path2, path3));
        public static FilePath Combine(FilePath path1, FilePath path2, FilePath path3) => Combine(path1.path, path2.path, path3.path);
        public static FilePath Combine(params string[] paths) => new(Path.Combine(paths));
        public static FilePath Combine(ReadOnlySpan<string> paths) => new(Path.Combine(paths));
        public static FilePath Combine(IEnumerable<string> paths) => Combine(paths.ToArray());
        public static FilePath Combine(IEnumerable<FilePath> paths) => Combine(paths.Select(p => p.path));
        public static FilePath Combine(params FilePath[] paths) => Combine(paths.AsEnumerable());
    }
}
