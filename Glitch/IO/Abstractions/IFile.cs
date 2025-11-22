namespace Glitch.IO.Abstractions
{
    public interface IFile : IFileSystemNode
    {
        string Stem { get; }
        string Extension { get; }
        IDirectory? Directory { get; }
        ByteSize Length { get; }

        Checksum Checksum();

        Stream Open();
        Stream OpenRead();
        Stream OpenWrite();

        TextReader OpenText();
        TextWriter CreateText();
        TextWriter AppendText();

        IEnumerable<string> ReadLines();
        string[] ReadAllLines();
        void WriteAllLines(IEnumerable<string> lines);

        string ReadAllText();
        void WriteAllText(string text);
    }
}
