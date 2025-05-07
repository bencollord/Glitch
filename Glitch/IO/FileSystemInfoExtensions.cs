namespace Glitch.IO
{
    public static class FileSystemInfoExtensions
    {
        public static FilePath ToPath(this FileSystemInfo node) => new(node);

        public static void Rename(this FileInfo file, string newName)
        {
            var path = new FilePath(newName);

            if (path.IsFullyQualified)
            {
                file.MoveTo(path.ToFullPath(file.Directory ?? FilePath.Empty));
            }
        }

        public static void Rename(this DirectoryInfo directory, string newName)
        {
            var path = new FilePath(newName);

            if (path.IsFullyQualified)
            {
                directory.MoveTo(path.ToFullPath(directory.Parent ?? FilePath.Empty));
            }
        }
    }
}
