using System.Security.Cryptography;

namespace Glitch.IO
{
    public static class FileExtensions
    {
        public static void MoveTo(this FileInfo file, FileInfo destination)
            => file.MoveTo(destination.FullName);

        public static string ReadAllText(this FileInfo file)
        {
            using var stream = file.OpenText();

            return stream.ReadToEnd();
        }

        public static IEnumerable<string> ReadLines(this FileInfo file)
        {
            using var stream = file.OpenText();

            for (string? line = stream.ReadLine();
                 !String.IsNullOrEmpty(line);
                 line = stream.ReadLine())
            {
                yield return line;
            }
        }

        public static string[] ReadAllLines(this FileInfo file) => file.ReadLines().ToArray();

        public static byte[] ReadAllBytes(this FileInfo file)
        {
            using var stream = file.OpenRead();

            byte[] result = new byte[file.Length];

            stream.ReadExactly(result);

            return result;
        }

        public static void WriteAllText(this FileInfo file, string text)
        {
            using var stream = file.CreateText();

            stream.Write(text);
        }

        public static void WriteAllLines(this FileInfo file, IEnumerable<string> lines)
        {
            using var stream = file.CreateText();

            foreach (string line in lines)
            {
                stream.WriteLine(line);
            }
        }

        public static void WriteAllBytes(this FileInfo file, byte[] bytes)
        {
            using var stream = file.OpenWrite();

            stream.Write(bytes);
        }

        public static string Checksum(this FileInfo file)
        {
            using var stream = file.OpenRead();
            using var alg = MD5.Create();

            var hash = alg.ComputeHash(stream);

            return BitConverter.ToString(hash).Strip('-');
        }

        public static ByteSize Size(this FileInfo file) => new ByteSize(file.Length);
    }
}
