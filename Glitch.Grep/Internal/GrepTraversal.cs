using Glitch.IO;
using System.Collections;

namespace Glitch.Grep.Internal
{
    internal abstract class GrepTraversal : IEnumerator<FileLine>
    {
        public abstract FileLine Current { get; }

        object? IEnumerator.Current => Current;

        protected abstract TraversalState State { get; }

        public abstract bool MoveNext();

        public virtual void Reset()
        {
            throw new NotSupportedException();
        }

        public abstract void Dispose();

        protected enum TraversalState
        {
            NotStarted,
            InProgress,
            EndOfFile,
            Disposed
        }
    }
}
