using Glitch.IO;

namespace Glitch.Grep
{
    public sealed class FileLine : IComparable<FileLine>, IEquatable<FileLine>
    {
        private FilePath fileName;
        private int lineNumber;
        private string text;

        internal FileLine(FilePath fileName, int lineNumber, string text)
        {
            this.fileName = fileName;
            this.lineNumber = lineNumber;
            this.text = text;
        }

        public int CompareTo(FileLine? other)
        {
            if (ReferenceEquals(other, null)) return 1;
            if (ReferenceEquals(other, this)) return 0;

            int result = fileName.CompareTo(other.fileName);

            if (result == 0)
            {
                result = lineNumber.CompareTo(other.lineNumber);
            }

            if (result == 0)
            {
                result = text.CompareTo(other.text);
            }

            return result;
        }

        public bool Equals(FileLine? other)
        {
            if (ReferenceEquals (other, null)) return false;
            if (ReferenceEquals(other, this)) return true;

            return fileName.Equals(other.fileName)
                && lineNumber.Equals(other.lineNumber)
                && text.Equals(other.text);
        }

        public override bool Equals(object? obj) => Equals(obj as FileLine);

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(fileName.GetHashCode());
            hash.Add(lineNumber.GetHashCode());
            hash.Add(text.GetHashCode());

            return hash.ToHashCode();
        }

        public static bool operator ==(FileLine? left, FileLine? right) => left is null ? right == null : left.Equals(right);

        public static bool operator !=(FileLine? left, FileLine? right) => !(left == right);
    }
}