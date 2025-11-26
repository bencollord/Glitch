namespace Glitch.IO.Abstractions;

public interface IDirectory : IFileSystemNode
{
    IDirectory? Parent { get; }

    IDirectory CreateSubdirectory(string path);

    IFile CreateFile(string name);

    IEnumerable<IFileSystemNode> Children();

    IEnumerable<IFileSystemNode> Descendants();

    IEnumerable<IDirectory> Directories();

    IEnumerable<IFile> Files();
}
