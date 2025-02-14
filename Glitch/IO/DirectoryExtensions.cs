namespace Glitch.IO
{
    public static class DirectoryExtensions
    {
        public static DirectoryInfo GetSubdirectory(this DirectoryInfo directory, string name)
            => new DirectoryInfo(Path.Combine(directory.FullName, name));

        public static FileInfo CreateFile(this DirectoryInfo directory, string fileName)
        {
            var file = directory.GetOrCreateFile(fileName);

            if (file.Exists)
            {
                throw new IOException($"File {fileName} already exists");
            }

            return file;
        }

        public static FileInfo GetFile(this DirectoryInfo directory, string fileName)
        {
            var file = directory.GetOrCreateFile(fileName);

            if (!file.Exists)
            {
                throw new FileNotFoundException($"File {fileName} does not exist");
            }

            return file;
        }

        public static FileInfo GetOrCreateFile(this DirectoryInfo directory, string fileName)
        {
            return new FileInfo(Path.Combine(directory.FullName, fileName));
        }

        public static IEnumerable<FileSystemInfo> SafeTraverse(this DirectoryInfo directory, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var result = SafeTraverseFlat(directory);

            if (searchOption == SearchOption.TopDirectoryOnly)
            {
                return result;
            }

            return result.SelectMany(SafeTraverseRecursive);
        }

        private static IEnumerable<FileSystemInfo> SafeTraverseRecursive(FileSystemInfo node)
        {
            yield return node;

            if (node is DirectoryInfo root)
            {
                foreach (var child in SafeTraverseFlat(root).SelectMany(SafeTraverseRecursive))
                {
                    yield return child;
                }
            }
        }

        private static IEnumerable<FileSystemInfo> SafeTraverseFlat(DirectoryInfo directory)
        {
            try
            {
                return directory.EnumerateFileSystemInfos();
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<FileSystemInfo>();
            }
        }
    }
}
