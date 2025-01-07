namespace Glitch.IO.Abstractions.Windows
{
    public class WindowsFileSystem : IFileSystem
    {
        public string Name => "Windows";

        public IDirectory GetDirectory(string path) => new WindowsDirectory(path);

        public IFile GetFile(string path) => new WindowsFile(path);
    }
}
