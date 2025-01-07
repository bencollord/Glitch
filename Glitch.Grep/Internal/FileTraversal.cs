using Glitch.IO;

namespace Glitch.Grep.Internal
{
    internal class FileTraversal : GrepTraversal
    {
        private readonly FileInfo file;
        private readonly FilePath path;
        private readonly Func<string, bool> filter;

        private StreamReader stream;
        private int lineNumber;
        private TraversalState state;
        private FileLine? current;

        internal FileTraversal(FileInfo file, Func<string, bool> filter)
        {
            this.file = file;
            this.filter = filter;
            stream = StreamReader.Null;
            path = new FilePath(file);
            state = TraversalState.NotStarted;
        }

        public override FileLine Current => current!;

        protected override TraversalState State => state;

        public override bool MoveNext()
        {
            if (state == TraversalState.NotStarted)
            {
                stream = file.OpenText();
                state = TraversalState.InProgress;
            }

            if (state == TraversalState.EndOfFile)
            {
                return false;
            }

            if (state == TraversalState.Disposed)
            {
                throw new ObjectDisposedException(nameof(FileTraversal));
            }

            string? currentLine;

            do
            {
                currentLine = stream!.ReadLine();
                lineNumber++;
            }
            while (currentLine != null && !filter(currentLine));

            if (currentLine is null)
            {
                state = TraversalState.EndOfFile;
                return false;
            }

            current = new FileLine(path, lineNumber, currentLine);
            return true;
        }

        public override void Reset()
        {
            Dispose();
            state = TraversalState.NotStarted;
        }

        public override void Dispose()
        {
            if (state == TraversalState.Disposed)
            {
                return;
            }

            stream.Close();
            stream = StreamReader.Null;
            state = TraversalState.Disposed;
        }
    }
}
