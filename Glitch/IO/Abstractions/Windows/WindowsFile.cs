using Glitch.Functional;
using System.Security.Cryptography;

namespace Glitch.IO.Abstractions.Windows
{
    public class WindowsFile : WindowsFileNode, IFile
    {
        private readonly FileInfo file;
        private readonly Lazy<string> checksum;

        public WindowsFile(string path)
            : this(new FileInfo(path)) { }

        public WindowsFile(FilePath path)
            : this(new FileInfo(path.ToString())) { }

        public WindowsFile(FileInfo file)
            : base(file)
        {
            this.file = file;
            checksum = new(ComputeHash);
        }

        public string Extension => file.Extension;

        public Option<IDirectory> Directory => Maybe(file.Directory)
                                                   .Map<IDirectory>(d => new WindowsDirectory(d));

        public ByteSize Length => ByteSize.FromBytes(file.Length);

        public Option<string> Stem => Maybe<string>(Path.Stem);

        public TextWriter AppendText() => file.AppendText();

        public string Checksum() => checksum.Value;

        public TextWriter CreateText() => file.CreateText();

        public Stream Open() => file.Open(FileMode.Open);

        public Stream OpenRead() => file.OpenRead();

        public TextReader OpenText() => file.OpenText();

        public Stream OpenWrite() => file.OpenWrite();

        public string[] ReadAllLines() => ReadLines().ToArray();

        public string ReadAllText()
        {
            using var text = OpenText();
            return text.ReadToEnd();
        }

        public IEnumerable<string> ReadLines()
        {
            using var text = OpenText();

            string? line = null;

            do
            {
                line = text.ReadLine();

                yield return line!;
            }
            while (line != null);
        }

        public void WriteAllLines(IEnumerable<string> lines)
        {
            using var stream = CreateText();

            foreach (var line in lines)
            {
                stream.WriteLine(line);
            }
        }

        public void WriteAllText(string text)
        {
            using var stream = CreateText();

            stream.Write(text);
        }

        private string ComputeHash()
        {
            using var stream = OpenRead();
            using var algorithm = MD5.Create();

            var hash = algorithm.ComputeHash(stream);

            return BitConverter.ToString(hash).Strip('-');
        }
    }
}
