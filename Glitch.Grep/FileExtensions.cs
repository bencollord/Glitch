using Glitch.Grep.Internal;

namespace Glitch.Grep
{
    public static class FileExtensions
    {
        public static IEnumerable<FileLine> Grep(this FileInfo file, Func<string, bool> filter)
        {
            using var iterator = new FileTraversal(file, filter);
            
            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }

        public static FileGrepQuery Grep(this FileInfo file, string pattern, GrepOptions options = GrepOptions.None)
        {
            //return new FileGrepQuery(file, GrepFilter.Create(pattern, options));
            throw new NotImplementedException();
        }
    }
}