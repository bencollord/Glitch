namespace Glitch.IO
{
    public static class FileSystemInfoExtensions
    {
        public static FilePath ToPath(this FileSystemInfo node) => new(node);

        public static void Rename(this FileInfo file, string newName)
        {
            var path = file.ToPath();
            var newPath = path.Directory / newName;

            file.MoveTo(newPath);
        }

        public static void Rename(this DirectoryInfo directory, string newName)
        {
            var path = directory.ToPath();
            var newPath = path.Directory / newName;

            directory.MoveTo(newPath);
        }
    }
}
