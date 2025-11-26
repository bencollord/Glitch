namespace Glitch.Glob.Internal;

internal abstract class ExecutionStrategy
{
    internal abstract IEnumerable<FileSystemInfo> Execute(DirectoryInfo root);
}
