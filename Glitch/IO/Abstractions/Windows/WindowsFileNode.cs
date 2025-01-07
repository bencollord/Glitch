namespace Glitch.IO.Abstractions.Windows
{
    public abstract class WindowsFileNode : IFileSystemNode
    {
        private FileSystemInfo inner;

        protected WindowsFileNode(FileSystemInfo inner)
        {
            this.inner = inner;
            Path = new FilePath(inner.FullName);
        }

        public string Name => inner.Name;

        public FilePath Path { get; }

        public bool Exists => inner.Exists;

        public void Delete() => inner.Delete();

        public sealed override bool Equals(object? obj) 
            => obj is WindowsFileNode node && Path.Equals(node.Path);

        public sealed override int GetHashCode() => Path.GetHashCode();

        public sealed override string ToString() => Path.ToString();
    }
}
