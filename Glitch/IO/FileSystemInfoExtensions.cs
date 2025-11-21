namespace Glitch.IO;

public static class FileSystemInfoExtensions
{
    extension(FileSystemInfo node)
    {
        public FilePath Path => new(node.FullName);
    }

    public static void Rename(this FileInfo file, string newName)
    {
        var newPath = file.Path.Directory / newName;

        file.MoveTo(newPath);
    }

    public static void Rename(this DirectoryInfo directory, string newName)
    {
        var newPath = directory.Path.Directory / newName;

        directory.MoveTo(newPath);
    }
}
