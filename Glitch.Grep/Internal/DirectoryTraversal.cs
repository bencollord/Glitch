using Glitch.IO;
using System.Collections;

namespace Glitch.Grep.Internal
{
    internal class DirectoryTraversal : GrepTraversal
    {
        private readonly DirectoryInfo directory;
        private readonly string searchPattern;
        private readonly Func<string, bool> filter;

        private TraversalState state;

        private IEnumerator<FileInfo>? files;
        private FileTraversal? currentFile;

        internal DirectoryTraversal(DirectoryInfo directory, Func<string, bool> filter)
            : this(directory, "*", filter) { }

        internal DirectoryTraversal(DirectoryInfo directory, string searchPattern, Func<string, bool> filter)
        {
            this.directory = directory;
            this.searchPattern = searchPattern;
            this.filter = filter;
            state = TraversalState.NotStarted;
        }

        public override FileLine Current => currentFile?.Current!;

        protected override TraversalState State => state;

        public override bool MoveNext()
        {
            if (state == TraversalState.NotStarted)
            {
                files = directory.EnumerateFiles(searchPattern).GetEnumerator();
                state = TraversalState.InProgress;
                
                return MoveNextFile();
            }

            if (state == TraversalState.EndOfFile)
            {
                return false;
            }

            if (state == TraversalState.Disposed)
            {
                throw new ObjectDisposedException(nameof(FileTraversal));
            }

            if (currentFile!.MoveNext())
            {
                return true;
            }

            currentFile.Dispose();

            return MoveNextFile();
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

            currentFile?.Dispose();
            state = TraversalState.Disposed;
        }

        private bool MoveNextFile()
        {
            if (!files!.MoveNext())
            {
                state = TraversalState.EndOfFile;
                return false;
            }

            currentFile = new FileTraversal(files.Current, filter);
            
            return currentFile.MoveNext();
        }
    }
}
