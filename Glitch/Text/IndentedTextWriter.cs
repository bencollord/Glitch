using System.Text;

namespace Glitch.Text
{
    public class IndentedTextWriter : TextWriter
    {
        private TextWriter inner;
        private Indentation indentation;
        private bool shouldIndent = false;

        public IndentedTextWriter(TextWriter inner)
            : this(inner, Indentation.Spaces) { }

        public IndentedTextWriter(TextWriter inner, Indentation indentation)
        {
            this.inner = inner is IndentedTextWriter i ? i.inner : inner;
            this.indentation = indentation;
        }

        public override Encoding Encoding => inner.Encoding;

        public Indentation Indentation
        {
            get => indentation;
            set => indentation = value;
        }

        public static IndentedTextWriter Create(TextWriter inner) => new(inner);

        public override void Write(char value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(char[]? buffer)
        {
            ApplyIndent();
            inner.Write(buffer);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            ApplyIndent();
            inner.Write(buffer, index, count);
        }

        public override void Write(ReadOnlySpan<char> buffer)
        {
            ApplyIndent();
            inner.Write(buffer);
        }

        public override void Write(bool value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(int value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(uint value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(long value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(ulong value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(float value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(double value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(decimal value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(string? value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(object? value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(StringBuilder? value)
        {
            ApplyIndent();
            inner.Write(value);
        }

        public override void Write(string format, object? arg0)
        {
            ApplyIndent();
            inner.Write(format, arg0);
        }

        public override void Write(string format, object? arg0, object? arg1)
        {
            ApplyIndent();
            inner.Write(format, arg0, arg1);
        }

        public override void Write(string format, object? arg0, object? arg1, object? arg2)
        {
            ApplyIndent();
            inner.Write(format, arg0, arg1, arg2);
        }

        public override void Write(string format, object?[] arg)
        {
            ApplyIndent();
            inner.Write(format, arg);
        }

        public override void Write(string format, ReadOnlySpan<object?> arg)
        {
            ApplyIndent();
            inner.Write(format, arg);
        }

        public override void WriteLine()
        {
            ApplyIndent();
            inner.WriteLine();
            shouldIndent = false;
        }

        public override void WriteLine(char value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(char[]? buffer)
        {
            ApplyIndent();
            inner.WriteLine(buffer);
            shouldIndent = false;
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            ApplyIndent();
            inner.WriteLine(buffer, index, count);
            shouldIndent = false;
        }

        public override void WriteLine(ReadOnlySpan<char> buffer)
        {
            ApplyIndent();
            inner.WriteLine(buffer);
            shouldIndent = false;
        }

        public override void WriteLine(bool value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(int value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(uint value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(long value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(ulong value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(float value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(double value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(decimal value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(string? value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(StringBuilder? value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(object? value)
        {
            ApplyIndent();
            inner.WriteLine(value);
            shouldIndent = false;
        }

        public override void WriteLine(string format, object? arg0)
        {
            ApplyIndent();
            inner.WriteLine(format, arg0);
            shouldIndent = false;
        }

        public override void WriteLine(string format, object? arg0, object? arg1)
        {
            ApplyIndent();
            inner.WriteLine(format, arg0, arg1);
            shouldIndent = false;
        }

        public override void WriteLine(string format, object? arg0, object? arg1, object? arg2)
        {
            ApplyIndent();
            inner.WriteLine(format, arg0, arg1, arg2);
            shouldIndent = false;
        }

        public override void WriteLine(string format, object?[] arg)
        {
            ApplyIndent();
            inner.WriteLine(format, arg);
            shouldIndent = false;
        }

        public override void WriteLine(string format, ReadOnlySpan<object?> arg)
        {
            ApplyIndent();
            inner.WriteLine(format, arg);
            shouldIndent = false;
        }

        public override async Task WriteAsync(char value)
        {
            ApplyIndent();
            await inner.WriteAsync(value);
        }

        public override async Task WriteAsync(string? value)
        {
            ApplyIndent();
            await inner.WriteAsync(value);
        }

        public override async Task WriteAsync(StringBuilder? value, CancellationToken cancellationToken)
        {
            ApplyIndent();
            await inner.WriteAsync(value, cancellationToken);
        }

        public override async Task WriteAsync(char[] buffer, int index, int count)
        {
            ApplyIndent();
            await inner.WriteAsync(buffer, index, count);
        }

        public override async Task WriteAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken)
        {
            ApplyIndent();
            await inner.WriteAsync(buffer, cancellationToken);
        }

        public override async Task WriteLineAsync(char value)
        {
            ApplyIndent();
            await inner.WriteLineAsync(value);
            shouldIndent = false;
        }

        public override async Task WriteLineAsync(string? value)
        {
            ApplyIndent();
            await inner.WriteLineAsync(value);
            shouldIndent = false;
        }

        public override async Task WriteLineAsync(StringBuilder? value, CancellationToken cancellationToken)
        {
            ApplyIndent();
            await inner.WriteLineAsync(value, cancellationToken);
            shouldIndent = false;
        }

        public override async Task WriteLineAsync(char[] buffer, int index, int count)
        {
            ApplyIndent();
            await inner.WriteLineAsync(buffer, index, count);
            shouldIndent = false;
        }

        public override async Task WriteLineAsync(ReadOnlyMemory<char> buffer, CancellationToken cancellationToken)
        {
            ApplyIndent();
            await inner.WriteLineAsync(buffer, cancellationToken);
            shouldIndent = false;
        }

        public override async Task WriteLineAsync()
        {
            ApplyIndent();
            await inner.WriteLineAsync();
            shouldIndent = false;
        }

        public void WriteIf(bool condition, string? text)
        {
            if (condition)
            {
                Write(text);
            }
        }

        public void WriteLineIf(bool condition)
        {
            if (condition)
            {
                WriteLine();
            }
        }

        public void WriteLineIf(bool condition, string text)
        {
            WriteIf(condition, text);
            WriteLineIf(condition);
        }

        public void Indent()
        {
            Indentation++;
        }

        public void Unindent()
        {
            Indentation--;
        }

        public override void Close()
        {
            inner.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                inner.Dispose();
            }
        }

        public override string ToString() => inner.ToString() ?? string.Empty;

        public override void Flush() => inner.Flush();

        public IDisposable BeginBlock() => new IndentationBlock(this);

        private void ApplyIndent()
        {
            if (shouldIndent)
            {
                inner.Write(Indentation);
                shouldIndent = false;
            }
        }

        private class IndentationBlock : IDisposable
        {
            private IndentedTextWriter inner;

            public IndentationBlock(IndentedTextWriter inner)
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
