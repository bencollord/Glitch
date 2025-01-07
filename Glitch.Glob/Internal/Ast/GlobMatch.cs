namespace Glitch.Glob.Internal.Ast
{
    internal class GlobMatch
    {
        internal static readonly GlobMatch Failure = new GlobMatch(false, 0);

        private bool success;
        private int length;

        internal GlobMatch(bool success, int length)
        {
            this.success = success;
            this.length = length;
        }

        internal bool IsSuccess => success;

        internal int Length => length;

        internal static GlobMatch Success(int length) => new GlobMatch(true, length);
    }
}
