namespace Glitch.IO.Abstractions;

public interface IFileSystemNode
{
    string Name { get; }
    FilePath Path { get; }
    bool Exists { get; }

    void Delete();
}
