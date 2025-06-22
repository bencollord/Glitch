using Glitch.Grep.Internal;
using System.Collections;

namespace Glitch.Grep
{
    public class FileGrepQuery : GrepQuery<FileGrepQuery>
    {
        private readonly FileInfo file;

        internal FileGrepQuery(FileInfo file, GrepFilter filter)
            : base(filter)
        {
            this.file = file;
        }

        protected override IEnumerable<FileInfo> EnumerateFiles()
        {
            yield return file;
        }

        private protected override FileGrepQuery WithFilter(GrepFilter filter) => new(file, filter);
    }
}