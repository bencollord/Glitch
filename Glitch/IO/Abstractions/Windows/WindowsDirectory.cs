using Glitch.Functional;
using System.Diagnostics;

namespace Glitch.IO.Abstractions.Windows
{
    public class WindowsDirectory : WindowsFileNode, IDirectory
    {
        private readonly DirectoryInfo directory;

        public WindowsDirectory(string path)
            : this(new DirectoryInfo(path)) { }

        public WindowsDirectory(DirectoryInfo directory)
            : base(directory)
        {
            this.directory = directory;
        }

        public Option<IDirectory> Parent => Option.Optional(directory.Parent)
                                                  .Map<IDirectory>(e => new WindowsDirectory(e));

        public IEnumerable<IFileSystemNode> Children() 
            => directory.EnumerateFileSystemInfos().Select(Wrap);

        public IEnumerable<IFileSystemNode> Descendants()
        {
            foreach (var node in Children())
            {
                yield return node;

                if (node is IDirectory dir)
                {
                    foreach (var desc in dir.Descendants())
                    {
                        yield return desc;
                    }
                }
            }
        }

        public IDirectory CreateSubdirectory(string path)
            => Wrap(directory.CreateSubdirectory(path));

        public IFile CreateFile(string name) 
            => new WindowsFile(Path.Append(name));

        public IEnumerable<IDirectory> Directories() 
            => directory.EnumerateDirectories().Select(Wrap);

        public IEnumerable<IFile> Files() 
            => directory.EnumerateFiles().Select(Wrap);

        private static IFile Wrap(FileInfo file) => new WindowsFile(file);

        private static IDirectory Wrap(DirectoryInfo directory) => new WindowsDirectory(directory);

        private static IFileSystemNode Wrap(FileSystemInfo node)
        {
            switch (node)
            {
                case FileInfo f:
                    return Wrap(f);
                case DirectoryInfo d:
                    return Wrap(d);
                default:
                    Debug.Fail("Found FileSystemInfo that is not a file or directory");
                    throw new InvalidOperationException($"Bad file node: {node}");
            }
        }
    }
}
