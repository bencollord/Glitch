using Glitch.Functional;
using Glitch.Functional.Results;

namespace Glitch.IO.Abstractions
{
    public interface IFile : IFileSystemNode
    {
        Option<string> Stem { get; }
        string Extension { get; }
        Option<IDirectory> Directory { get; }
        ByteSize Length { get; }

        string Checksum();

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
