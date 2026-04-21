using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Glitch.IO;

public static class FileExtensions
{
    extension(FileInfo file)
    {
        public ByteSize Size => new ByteSize(file.Length);

        public void MoveTo(FileInfo destination)
            => file.MoveTo(destination.FullName);

        public string ReadAllText()
        {
            using var stream = file.OpenText();

            return stream.ReadToEnd();
        }

        public async Task<string> ReadAllTextAsync(CancellationToken token = default)
        {
            using var stream = file.OpenText();

            return await stream.ReadToEndAsync(token);
        }

        // TODO Async
        public IEnumerable<string> ReadLines()
        {
            using var stream = file.OpenText();

            for (string? line = stream.ReadLine();
                 !String.IsNullOrEmpty(line);
                 line = stream.ReadLine())
            {
                yield return line;
            }
        }

        public async IAsyncEnumerable<string> ReadLinesAsync([EnumeratorCancellation] CancellationToken token = default)
        {
            using var stream = file.OpenText();

            for (string? line = await stream.ReadLineAsync(token);
                 !String.IsNullOrEmpty(line);
                 line = await stream.ReadLineAsync(token))
            {
                yield return line;
            }
        }

        public string[] ReadAllLines() => file.ReadLines().ToArray();

        public async Task<string[]> ReadAllLinesAsync(CancellationToken token = default) => await file.ReadLinesAsync().ToArrayAsync();

        public byte[] ReadAllBytes()
        {
            using var stream = file.OpenRead();

            byte[] result = new byte[file.Length];

            stream.ReadExactly(result);

            return result;
        }

        public async Task<byte[]> ReadAllBytesAsync(CancellationToken token = default)
        {
            using var stream = file.OpenRead();

            byte[] result = new byte[file.Length];

            await stream.ReadExactlyAsync(result, token);

            return result;
        }

        public void WriteAllText(string text)
        {
            using var stream = file.CreateText();

            stream.Write(text);
        }

        public async Task WriteAllTextAsync(string text)
        {
            using var stream = file.CreateText();

            await stream.WriteAsync(text); // No overload that accepts a cancellation token for some reason
        }

        public void WriteAllLines(IEnumerable<string> lines)
        {
            using var stream = file.CreateText();

            foreach (string line in lines)
            {
                stream.WriteLine(line);
            }
        }

        public async Task WriteAllLinesAsync(IEnumerable<string> lines)
        {
            using var stream = file.CreateText();

            foreach (string line in lines)
            {
                await stream.WriteLineAsync(line); // No overload that accepts a cancellation token for some reason
            }
        }

        public void WriteAllBytes(byte[] bytes)
        {
            using var stream = file.OpenWrite();

            stream.Write(bytes);
        }

        public async Task WriteAllBytesAsync(byte[] bytes, CancellationToken token = default)
        {
            using var stream = file.OpenWrite();

            await stream.WriteAsync(bytes, token);
        }
    }
}
