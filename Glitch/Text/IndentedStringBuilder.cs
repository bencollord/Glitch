using System.Text;

namespace Glitch.Text
{
    public class IndentedStringBuilder
    {
        private StringBuilder inner;
        private Indentation indentation;
        private bool shouldIndent = false;

        public IndentedStringBuilder()
            : this(Indentation.Spaces) { }

        public IndentedStringBuilder(Indentation indentation)
            : this(new StringBuilder(), indentation) { }

        public IndentedStringBuilder(string text)
            : this(text, Indentation.Spaces) { }

        public IndentedStringBuilder(string text, Indentation indentation)
            : this(new StringBuilder(text), indentation) { }

        public IndentedStringBuilder(StringBuilder inner)
            : this(inner, Indentation.Spaces) { }

        public IndentedStringBuilder(StringBuilder inner, Indentation indentation)
        {
            this.inner = inner;
            this.indentation = indentation;
        }

        public char this[int index]
        {
            get => inner[index];
            set => inner[index] = value;
        }

        public Indentation Indentation
        {
            get => indentation;
            set => indentation = value;
        }

        public IndentedStringBuilder Append(char value)
        {
            if (shouldIndent)
            {
                inner.Append(Indentation);
            }

            inner.Append(value);
            return this;
        }

        public IndentedStringBuilder Append(string text)
        {
            if (shouldIndent)
            {
                inner.Append(indentation);
                shouldIndent = false;
            }

            inner.Append(text);

            return this;
        }

        public IndentedStringBuilder AppendLine()
        {
            inner.AppendLine();
            shouldIndent = true;
            return this;
        }

        public IndentedStringBuilder AppendLine(string text)
            => Append(text).AppendLine();

        public IndentedStringBuilder AppendIf(bool condition, string text)
        {
            if (condition)
            {
                Append(text);
            }

            return this;
        }

        public IndentedStringBuilder AppendLineIf(bool condition)
        {
            if (condition)
            {
                AppendLine();
            }

            return this;
        }

        public IndentedStringBuilder AppendLineIf(bool condition, string text)
            => AppendIf(condition, text).AppendLineIf(condition);

        public IndentedStringBuilder Indent()
        {
            Indentation++;
            return this;
        }

        public IndentedStringBuilder Unindent()
        {
            Indentation--;
            return this;
        }

        public IndentedStringBuilder Clear()
        {
            inner.Clear();
            return this;
        }

        public override string ToString() => inner.ToString();

        public string Flush()
        {
            var output = ToString();
            Clear();
            return output;
        }

        public IDisposable BeginBlock() => new IndentationBlock(this);

        private class IndentationBlock : IDisposable
        {
            private IndentedStringBuilder inner;

            public IndentationBlock(IndentedStringBuilder inner)
            {
                this.inner = inner;
                inner.Indentation++;
            }

            public void Dispose()
            {
                inner.Indentation--;
            }
        }
    }
}
