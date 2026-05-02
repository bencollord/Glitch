using System.Collections.Immutable;

namespace Glitch.IO;

public sealed partial class FilePath : IEquatable<FilePath>, IComparable<FilePath>
{
    public static readonly FilePath Empty = new(string.Empty);

    public static readonly char DirectorySeparator    = Path.DirectorySeparatorChar;
    public static readonly char AltDirectorySeparator = Path.AltDirectorySeparatorChar;

    public static readonly ImmutableArray<char> DirectorySeparators  = [DirectorySeparator, AltDirectorySeparator];
    public static readonly ImmutableArray<char> InvalidPathChars     = Path.GetInvalidPathChars().ToImmutableArray();
    public static readonly ImmutableArray<char> InvalidFileNameChars = Path.GetInvalidFileNameChars().ToImmutableArray();

    public static FilePath Create(string path) => new(path);
    public static FilePath Create(FileSystemInfo node) => new(node.FullName);

    public static bool IsNullOrEmpty(FilePath? path) => string.IsNullOrEmpty(path?.path);

    public static FilePath GetTempPath() => new(Path.GetTempPath());
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
