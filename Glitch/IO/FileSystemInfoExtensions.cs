namespace Glitch.IO
{
    public static class FileSystemInfoExtensions
    {
        public static FilePath ToPath(this FileSystemInfo node) => new(node);

        public static void Rename(this FileInfo file, string newName)
        {
            // TODO Use FilePath class when tested
            var newPath = Maybe(file.Directory)
                .Select(d => Path.Combine(d.FullName, newName))
                .IfNone(newName);

            file.MoveTo(newPath);
        }

        public static void Rename(this DirectoryInfo directory, string newName)
        {
            // TODO Use FilePath class when tested
            var newPath = Maybe(directory.Parent)
                .Select(d => Path.Combine(d.FullName, newName))
                .IfNone(newName);

            directory.MoveTo(newPath);
        }
    }
}
