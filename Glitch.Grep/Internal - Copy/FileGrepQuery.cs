using Glitch.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Grep.Internal
{
    internal class FileGrepQuery : IEnumerable<FileLine>
    {
        private readonly FileInfo file;
        private readonly GrepFilter filter;
        private readonly FilePath filePath;

        internal FileGrepQuery(FileInfo file, GrepFilter filter)
        {
            this.file   = file;
            this.filter = filter;
            filePath    = new FilePath(file.FullName);
        }

        public IEnumerator<FileLine> GetEnumerator() => new FileTraversal(file.OpenText(), filePath, filter);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class FileTraversal : IEnumerator<FileLine>
        {
            private readonly FilePath filePath;
            private readonly GrepFilter filter;

            private StreamReader stream;
            private int lineNumber;
            private bool isDisposed;

            internal FileTraversal(StreamReader stream, FilePath filePath, GrepFilter filter)
            {
                this.stream   = stream;
                this.filePath = filePath;
                this.filter   = filter;
            }

            public FileLine? Current { get; private set; }

            object? IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (stream.EndOfStream)
                {
                    return false;
                }

                string? currentLine;

                do
                {
                    currentLine = stream.ReadLine();
                    lineNumber++;
                }
                while (currentLine != null && !filter.IsMatch(currentLine));

                if (currentLine is null)
                {
                    return false;
                }

                Current = new FileLine(filePath, lineNumber, currentLine);
                return true;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            protected virtual void Dispose(bool disposing)
            {
                
            }

            public void Dispose()
            {
                if (isDisposed)
                {
                    return;
                }

                stream?.Close();
                stream = StreamReader.Null;
                isDisposed = true;
            }
        }
    }
}
