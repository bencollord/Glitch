namespace Glitch.IO.Abstractions
{
    public interface IFileSystem
    {
        string Name { get; }

        IFile GetFile(string path);

        IDirectory GetDirectory(string path);
    }
}
