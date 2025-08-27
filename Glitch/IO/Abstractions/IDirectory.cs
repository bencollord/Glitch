using Glitch.Functional;
using Glitch.Functional.Results;

namespace Glitch.IO.Abstractions
{
    public interface IDirectory : IFileSystemNode
    {
        Option<IDirectory> Parent { get; }

        IDirectory CreateSubdirectory(string path);

        IFile CreateFile(string name);

        IEnumerable<IFileSystemNode> Children();

        IEnumerable<IFileSystemNode> Descendants();

        IEnumerable<IDirectory> Directories();

        IEnumerable<IFile> Files();
    }
}
