namespace Glitch.Text
{
    public class CharStream : IDisposable
    {
        private const int DefaultBufferSize = 5;
        private const char NullChar = '\0';

        private TextReader stream;
        private Buffer buffer;

        public CharStream(TextReader stream)
        {
            this.stream = stream;
            buffer = new Buffer(DefaultBufferSize);
        }

        public bool IsEof { get; private set; }

        public static CharStream Create(TextReader stream) => new CharStream(stream);

        public static CharStream Create(Stream stream) => Create(new StreamReader(stream));

        public static CharStream Create(string text) => Create(new StringReader(text));

        public char Peek()
        {
            if (!buffer.IsEmpty)
            {
                return buffer[0];
            }

            char next = ReadNextChar();

            if (!IsEof)
            {
                buffer.Add(next);
            }

            return next;
        }

        public char Read()
        {
            if (!buffer.IsEmpty)
            {
                return buffer.Take();
            }

            return ReadNextChar();
        }

        public void Dispose()
        {
            stream.Dispose();
        }

        private char ReadNextChar()
        {
            if (IsEof)
            {
                return NullChar;
            }

            int next = stream.Read();

            if (next < 0)
            {
                IsEof = true;
                return NullChar;
            }

            return (char)next;
        }

        private class Buffer
        {
            private char[] buffer;
            private int head;
            private int tail;

            internal Buffer(int size)
            {
                buffer = new char[size];
                head = 0;
                tail = -1;
            }

            private Buffer(Buffer copy)
            {
                buffer = new char[copy.buffer.Length];
                copy.buffer.CopyTo(buffer, 0);
                head = copy.head;
                tail = copy.tail;
            }

            internal int Count => tail - head + 1;
            internal int Capacity => buffer.Length;
            internal bool IsEmpty => tail < head;
            internal bool IsFull => Count == Capacity;
            internal char this[int index] => buffer[Translate(head + index)];

            private int Read => Translate(head);
            private int Write => Translate(tail + 1);

            internal void Add(char c)
            {
                if (IsFull)
                {
                    head++;
                }

                buffer[Translate(++tail)] = c;
            }

            internal void Add(char[] characters)
            {
                // If there's an overflow, just replace our buffer
                // with enough characters to fill it, starting from the right,
                // as if the entire array had been added one-by-one.
                if (characters.Length >= Capacity)
                {
                    int startIndex = characters.Length - Capacity;
                    Array.Copy(characters, startIndex, buffer, 0, Capacity);
                    head = 0;
                    tail = Capacity - 1;
                    return;
                }

                int firstWriteCount = Capacity - Write;
                int freeSpace = Capacity - Count;
                int overflow = characters.Length - freeSpace;

                if (firstWriteCount >= characters.Length)
                {
                    Array.Copy(characters, 0, buffer, Write, characters.Length);
                }
                else
                {
                    // Copy first segment starting at Write
                    Array.Copy(characters, 0, buffer, Write, firstWriteCount);

                    // Copy remaining characters starting at the beginning of the buffer
                    Array.Copy(characters, firstWriteCount, buffer, 0, characters.Length - firstWriteCount);
                }

                tail += characters.Length;

                if (overflow > 0)
                {
                    head += overflow;
                }
            }

            internal char Take()
            {
                if (IsEmpty)
                {
                    throw new InvalidOperationException("Buffer is empty");
                }

                return buffer[Translate(head++)];
            }

            internal char Take(int lookahead)
            {
                if (lookahead > Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(lookahead));
                }

                head += lookahead;
                return Take();
            }

            internal void Clear()
            {
                head = 0;
                tail = -1;
            }

            internal void EnsureCapacity(int size)
            {
                if (size > Capacity)
                {
                    // Expand
                    var newBuffer = new char[size];

                    CopyTo(newBuffer, 0);

                    // Reset the head and tail
                    tail -= head;
                    head = 0;
                    buffer = newBuffer;
                }
            }

            internal Buffer Clone() => new Buffer(this);

            internal void CopyTo(char[] array, int index)
            {
                if (IsEmpty)
                {
                    return;
                }

                if (Read < Write)
                {
                    Array.Copy(buffer, Read, array, index, Count);
                }
                else
                {
                    int firstCopyCount = Capacity - Read;

                    Array.Copy(buffer, Read, array, index, firstCopyCount);
                    Array.Copy(buffer, 0, array, index + firstCopyCount, Count - firstCopyCount);
                }
            }

            private int Translate(int index) => index % Capacity;

            private IEnumerable<ArraySegment<char>> GetSegments()
            {
                if (IsEmpty)
                {
                    yield return new ArraySegment<char>(buffer, 0, 0);
                    yield break;
                }

                if (Read < Write)
                {
                    yield return new ArraySegment<char>(buffer, Read, Count);
                    yield break;
                }

                int firstSegmentCount = Capacity - Read;

                yield return new ArraySegment<char>(buffer, Read, firstSegmentCount);

                int nextCount = Count - firstSegmentCount;

                // Edge case where an empty segment will be yielded if both pointers are at 0
                if (nextCount > 0)
                {
                    yield return new ArraySegment<char>(buffer, 0, nextCount);
                }
            }
        }
    }
}
