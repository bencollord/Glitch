using System.Diagnostics.CodeAnalysis;

namespace Glitch.IO;

public sealed partial class FilePath : IEquatable<FilePath>, IComparable<FilePath>
{
    private readonly string path;

    public FilePath(string? path)
    {
        this.path = path?.Replace(AltDirectorySeparatorChar, DirectorySeparatorChar) ?? string.Empty;
    }

    public FilePath(FileSystemInfo fileOrDirectory)
        : this(fileOrDirectory.FullName) { }

    public FilePath Directory => new(Path.GetDirectoryName(path));
    public FilePath FileName => new(Path.GetFileName(path));
    public FilePath Stem => new(Path.GetFileNameWithoutExtension(path));
    public FilePath Root => new(Path.GetPathRoot(path));
    public bool EndsInDirectorySeparator => Path.EndsInDirectorySeparator(path);
    public bool Exists => Path.Exists(path);
    public bool IsEmpty => string.IsNullOrEmpty(path);
    public FilePath Extension => new(Path.GetExtension(path));
    public bool HasExtension => Path.HasExtension(path);
    public bool IsFullyQualified => Path.IsPathFullyQualified(path);
    public bool IsRooted => Path.IsPathRooted(path);
    public bool IsValid => !InvalidPathChars.Any(path.Contains) && !InvalidFileNameChars.Any(path.Contains);

    public FilePath[] ToSegments()
        => Array.ConvertAll(
            path.Split([DirectorySeparatorChar, AltDirectorySeparatorChar], StringSplitOptions.RemoveEmptyEntries),
            p => new FilePath(p));

    public FilePath WithExtension(string extension)
        => Extension != extension ? new(Path.ChangeExtension(path, extension)) : this;

    public FilePath TrimEndingDirectorySeparator() => new(Path.TrimEndingDirectorySeparator(path));

    public FilePath ToFullPath() => new(Path.GetFullPath(path));
    public FilePath ToFullPath(string basePath) => new(Path.GetFullPath(path, basePath));
    public FilePath ToFullPath(FilePath basePath) => ToFullPath(basePath.path);

    public FilePath ToRelativePath(string relativeTo) => new(Path.GetRelativePath(relativeTo, path));
    public FilePath ToRelativePath(FilePath relativeTo) => ToRelativePath(relativeTo.path);

    public FilePath Append(string other) => new(Path.Join(path, other));
    public FilePath Append(FilePath other) => Append(other.path);
    public FilePath Append(string path1, string path2) => new(Path.Join(path, path1, path2));
    public FilePath Append(FilePath path1, FilePath path2) => Append(path1.path, path2.path);
    public FilePath Append(string path1, string path2, string path3) => new(Path.Join(path, path1, path2, path3));
    public FilePath Append(FilePath path1, FilePath path2, FilePath path3) => Append(path1.path, path2.path, path3.path);
    public FilePath Append(ReadOnlySpan<string> paths) => Append(Path.Join(paths));
    public FilePath Append(params IEnumerable<string> paths) => new(Path.Join(paths.Prepend(path).ToArray()));
    public FilePath Append(params IEnumerable<FilePath> paths) => Append(paths.Select(p => p.path));

    public FilePath Concat(ReadOnlySpan<char> other)
        => new(string.Concat(path.AsSpan(), other));
    public FilePath Concat(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
        => new(string.Concat(path.AsSpan(), path1, path2));
    public FilePath Concat(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)
        => new(string.Concat(path.AsSpan(), path1, path2, path3));
    public FilePath Concat(string other) => new(string.Concat(path, other));
    public FilePath Concat(FilePath other) => Concat(other.path);
    public FilePath Concat(string path1, string path2) => new(string.Concat(path, path1, path2));
    public FilePath Concat(FilePath path1, FilePath path2) => Concat(path1.path, path2.path);
    public FilePath Concat(string path1, string path2, string path3) => new(string.Concat(path, path1, path2, path3));
    public FilePath Concat(FilePath path1, FilePath path2, FilePath path3) => Concat(path1.path, path2.path, path3.path);
    public FilePath Concat(params IEnumerable<string> paths) => new(string.Concat(paths.Prepend(path).ToArray()));
    public FilePath Concat(params IEnumerable<FilePath> paths) => Concat(paths.Select(p => p.path));

    public int CompareTo(FilePath? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (ReferenceEquals(other, this))
        {
            return 0;
        }

        return path.CompareTo(other.path);
    }

    public bool Equals(FilePath? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(other, this))
        {
            return true;
        }

        return path.Equals(other.path);
    }

    public override bool Equals(object? obj) => Equals(obj as FilePath);

    public override int GetHashCode() => path.GetHashCode();

    public override string ToString() => path;


    public static implicit operator string(FilePath path) => path.path;

    /// <summary>
    /// Implicit conversion operator from string to <see cref="FilePath"/>.
    /// </summary>
    /// <remarks>
    /// The conversion is implicit since the static <see cref="Path"/> class
    /// and all of the types in the System.IO namespace expect a string, so
    /// this will make it easier to work with. 
    /// </remarks>
    /// <param name="path"></param>
    public static implicit operator FilePath(string path) => new(path);

    public static implicit operator FilePath(FileSystemInfo? node) => new(node?.FullName);

    public static bool operator ==(FilePath? left, FilePath? right) => left is null ? right == null : left.Equals(right);

    public static bool operator !=(FilePath? left, FilePath? right) => !(left == right);

    [return: NotNullIfNotNull(nameof(left))]
    [return: NotNullIfNotNull(nameof(right))]
    public static FilePath? operator +(FilePath? left, string? right)
        => right is null ? left : left + new FilePath(right);

    [return: NotNullIfNotNull(nameof(left))]
    [return: NotNullIfNotNull(nameof(right))]
    public static FilePath? operator +(FilePath? left, FilePath? right)
        => (left is not null && right is not null) ? new FilePath(left.path + right.path) : left ?? right;

    [return: NotNullIfNotNull(nameof(left))]
    [return: NotNullIfNotNull(nameof(right))]
    public static FilePath? operator /(FilePath? left, string? right)
        => right is null ? left : left / new FilePath(right);

    [return: NotNullIfNotNull(nameof(left))]
    [return: NotNullIfNotNull(nameof(right))]
    public static FilePath? operator /(FilePath? left, FilePath? right)
        => (left is not null && right is not null) ? left.Append(right) : left ?? right;

    public static bool operator <(FilePath? left, FilePath? right)
        => left is null ? right is not null : left.CompareTo(right) < 0;

    public static bool operator <=(FilePath? left, FilePath? right)
        => left is null || left.CompareTo(right) <= 0;

    public static bool operator >(FilePath? left, FilePath? right)
        => left is not null && left.CompareTo(right) > 0;

    public static bool operator >=(FilePath? left, FilePath? right)
        => left is null ? right is null : left.CompareTo(right) >= 0;
}
