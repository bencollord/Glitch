using Glitch.IO;
using System.Collections;

namespace Glitch.Grep.Internal
{
    internal class RecursiveDirectoryTraversal : GrepTraversal
    {
        private readonly DirectoryInfo root;
        private readonly string searchPattern;
        private readonly Func<string, bool> filter;

        private TraversalState state;

        private IEnumerator<DirectoryInfo>? directories;
        private DirectoryTraversal? currentDirectory;

        internal RecursiveDirectoryTraversal(DirectoryInfo root, Func<string, bool> filter)
            : this(root, "*", filter) { }

        internal RecursiveDirectoryTraversal(DirectoryInfo root, string searchPattern, Func<string, bool> filter)
        {
            this.root = root;
            this.searchPattern = searchPattern;
            this.filter = filter;
            state = TraversalState.NotStarted;
        }

        public override FileLine Current => currentDirectory?.Current!;

        protected override TraversalState State => state;

        public override bool MoveNext()
        {
            //if (state == TraversalState.NotStarted)
            //{
            //    files = root.EnumerateFiles(searchPattern).GetEnumerator();
            //    state = TraversalState.InProgress;

            //    return MoveNextFile();
            //}

            //if (state == TraversalState.EndOfFile)
            //{
            //    return false;
            //}

            //if (state == TraversalState.Disposed)
            //{
            //    throw new ObjectDisposedException(nameof(FileTraversal));
            //}

            //if (currentDirectory!.MoveNext())
            //{
            //    return true;
            //}

            //currentDirectory.Dispose();

            //return MoveNextFile();
            throw new NotImplementedException();
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

            currentDirectory?.Dispose();
            state = TraversalState.Disposed;
        }

        private bool MoveNextFile()
        {
            //if (!files!.MoveNext())
            //{
            //    state = TraversalState.EndOfFile;
            //    return false;
            //}

            //currentDirectory = new FileTraversal(files.Current, filter);

            //return currentDirectory.MoveNext();
            throw new NotImplementedException();
        }
    }
}
