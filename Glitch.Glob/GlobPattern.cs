namespace Glitch.Glob
{
    public class GlobPattern
    {
        private readonly string pattern;
        private readonly GlobOptions options;

        public GlobPattern(string pattern, GlobOptions options = GlobOptions.None)
        {
            this.pattern = pattern;
            this.options = options;
        }

        public GlobOptions Options => options;

        public IEnumerable<FileSystemInfo> Execute(DirectoryInfo root)
        {
            throw new NotImplementedException();
        }
    }
}
