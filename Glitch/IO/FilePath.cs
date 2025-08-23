using Glitch.Functional;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.IO
{
    public sealed class FilePath : IEquatable<FilePath>, IComparable<FilePath>
    {
        public static readonly FilePath Empty = new(string.Empty);

        private readonly string path;

        public FilePath(string? path)
        {
            this.path = Normalize(path ?? string.Empty);
        }

        public FilePath(FileSystemInfo fileOrDirectory)
            : this(fileOrDirectory.FullName) { }

        public static char[] InvalidFileNameChars => Path.GetInvalidFileNameChars();
        public static char[] InvalidPathChars => Path.GetInvalidPathChars();
        public static FilePath TempPath => new(Path.GetTempPath());
        public static char DirectorySeparatorChar => Path.DirectorySeparatorChar;
        public static char AltDirectorySeparatorChar => Path.AltDirectorySeparatorChar;

        public FilePath Directory => new(Path.GetDirectoryName(path));
        public FilePath FileName => new(Path.GetFileName(path));
        public FilePath Stem => new(Path.GetFileNameWithoutExtension(path));
        public FilePath Root => new(Path.GetPathRoot(path));
        public bool EndsInDirectorySeparator => Path.EndsInDirectorySeparator(path);
        public bool Exists => Path.Exists(path);
        public bool IsEmpty => string.IsNullOrEmpty(path);
        public bool IsFile => !FileName.IsEmpty;
        public bool IsDirectory => !IsEmpty && FileName.IsEmpty;
        public FilePath Extension => new(Path.GetExtension(path));
        public bool HasExtension => Path.HasExtension(path);
        public bool IsFullyQualified => Path.IsPathFullyQualified(path);
        public bool IsRooted => Path.IsPathRooted(path);

        public static FilePath GetRandomFileName() => new(Path.GetRandomFileName());
        public static FilePath GetTempFileName() => new(Path.GetTempFileName());

        public static FilePath Combine(string path1, string path2) => new(Path.Combine(path1, path2));
        public static FilePath Combine(FilePath path1, FilePath path2) => Combine(path1.path, path2.path);
        public static FilePath Combine(string path1, string path2, string path3) => new(Path.Combine(path1, path2, path3));
        public static FilePath Combine(FilePath path1, FilePath path2, FilePath path3) => Combine(path1.path, path2.path, path3.path);
        public static FilePath Combine(params string[] paths) => new(Path.Combine(paths));
        public static FilePath Combine(IEnumerable<string> paths) => Combine(paths.ToArray());
        public static FilePath Combine(IEnumerable<FilePath> paths) => Combine(paths.Select(p => p.path));
        public static FilePath Combine(params FilePath[] paths) => Combine(paths.AsEnumerable());

        public FilePath WithExtension(string extension)
            => Extension != extension ? new(Path.ChangeExtension(path, extension)) : this;

        public FilePath TrimEndingDirectorySeparator() => new(Path.TrimEndingDirectorySeparator(path));

        public FilePath ToFullPath() => new(Path.GetFullPath(path));
        public FilePath ToFullPath(string basePath) => new(Path.GetFullPath(path, basePath));
        public FilePath ToFullPath(FilePath basePath) => ToFullPath(basePath.path);

        public FilePath ToRelativePath(string relativeTo) => new(Path.GetRelativePath(relativeTo, path));
        public FilePath ToRelativePath(FilePath relativeTo) => ToRelativePath(relativeTo.path);

        public FilePath Append(string other) => new(Path.Combine(path, other));
        public FilePath Append(FilePath other) => Append(other.path);
        public FilePath Append(string path1, string path2) => new(Path.Combine(path, path1, path2));
        public FilePath Append(FilePath path1, FilePath path2) => Append(path1.path, path2.path);
        public FilePath Append(string path1, string path2, string path3) => new(Path.Combine(path, path1, path2, path3));
        public FilePath Append(FilePath path1, FilePath path2, FilePath path3) => Append(path1.path, path2.path, path3.path);
        public FilePath Append(params IEnumerable<string> paths) => new(Path.Combine(paths.Prepend(path).ToArray()));
        public FilePath Append(params IEnumerable<FilePath> paths) => Append(paths.Select(p => p.path));

        public FilePath Concat(ReadOnlySpan<char> other)
            => new(Path.Join(path.AsSpan(), other));
        public FilePath Concat(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
            => new(Path.Join(path.AsSpan(), path1, path2));
        public FilePath Concat(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)
            => new(Path.Join(path.AsSpan(), path1, path2, path3));
        public FilePath Concat(string other) => new(Path.Join(path, other));
        public FilePath Concat(FilePath other) => Concat(other.path);
        public FilePath Concat(string path1, string path2) => new(Path.Join(path, path1, path2));
        public FilePath Concat(FilePath path1, FilePath path2) => Concat(path1.path, path2.path);
        public FilePath Concat(string path1, string path2, string path3) => new(Path.Join(path, path1, path2, path3));
        public FilePath Concat(FilePath path1, FilePath path2, FilePath path3) => Concat(path1.path, path2.path, path3.path);
        public FilePath Concat(params IEnumerable<string> paths) => new(Path.Join(paths.Prepend(path).ToArray()));
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

        private static string Normalize(string path)
        {
            // TODO: Use algorithm below

            // Algorithm taken from https://en.cppreference.com/w/cpp/filesystem/path
            // If the path is empty, stop (normal form of an empty path is an empty path).
            // Replace each directory-separator (which may consist of multiple slashes) with a single path::preferred_separator.
            // Remove each dot and any immediately following directory-separator.
            // Remove each non-dot-dot filename immediately followed by a directory-separator and a dot-dot, along with any immediately following directory-separator.
            // If there is root-directory, remove all dot-dots and any directory-separators immediately following them.
            // If the last filename is dot-dot, remove any trailing directory-separator.
            // If the path is empty, add a dot (normal form of ./ is .).

            return path.Replace(AltDirectorySeparatorChar, DirectorySeparatorChar);
        }

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

        public static implicit operator FileSystemInfo?(FilePath? path)
            => path switch
            {
                FilePath p when p.IsFile => new FileInfo(p),
                FilePath p when p.IsDirectory => new DirectoryInfo(p),
                _ => null
            };

        public static explicit operator DirectoryInfo(FilePath path) => path.IsDirectory ? new(path) : throw new InvalidCastException($"Path '{path}' is not a valid directory");

        public static explicit operator FileInfo(FilePath path) => path.IsFile ? new(path) : throw new InvalidCastException($"Path '{path}' is not a valid file");

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

        [return: NotNullIfNotNull(nameof(node))]
        [return: NotNullIfNotNull(nameof(path))]
        public static FileSystemInfo? operator +(FileSystemInfo? node, FilePath? path) => node?.ToPath() / path;

        [return: NotNullIfNotNull(nameof(node))]
        [return: NotNullIfNotNull(nameof(path))]
        public static FileSystemInfo? operator /(FileSystemInfo? node, FilePath? path) => node?.ToPath() / path;

        public static bool operator <(FilePath? left, FilePath? right) 
            => left is null ? right is not null : left.CompareTo(right) < 0;

        public static bool operator <=(FilePath? left, FilePath? right) 
            => left is null || left.CompareTo(right) <= 0;

        public static bool operator >(FilePath? left, FilePath? right) 
            => left is not null && left.CompareTo(right) > 0;

        public static bool operator >=(FilePath? left, FilePath? right) 
            => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
