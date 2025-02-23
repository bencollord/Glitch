using System.Collections;

namespace Glitch.Collections
{
    public class RingBuffer<T> : IEnumerable<T>
    {
        private T[] buffer;
        private int head;
        private int tail;

        public RingBuffer(int size)
        {
            buffer = new T[size];
            head = 0;
            tail = -1;
        }

        public RingBuffer(T[] buffer)
        {
            this.buffer = buffer;
            head = 0;
            tail = buffer.Length - 1;
        }

        private RingBuffer(RingBuffer<T> copy)
        {
            buffer = new T[copy.buffer.Length];
            copy.buffer.CopyTo(buffer, 0);
            head = copy.head;
            tail = copy.tail;
        }

        public int Count => tail - head + 1;
        public int Capacity => buffer.Length;
        public bool IsEmpty => tail < head;
        public bool IsFull => Count == Capacity;
        public T this[int index] => buffer[Translate(head + index)];

        private int Read => Translate(head);
        private int Write => Translate(tail + 1);

        public void Add(T item)
        {
            if (IsFull)
            {
                head++;
            }

            buffer[Translate(++tail)] = item;
        }

        public void Add(T[] items)
        {
            // If there's an overflow, just replace our buffer
            // with enough characters to fill it, starting from the right,
            // as if the entire array had been added one-by-one.
            if (items.Length >= Capacity)
            {
                int startIndex = items.Length - Capacity;
                Array.Copy(items, startIndex, buffer, 0, Capacity);
                head = 0;
                tail = Capacity - 1;
                return;
            }

            int firstWriteCount = Capacity - Write;
            int freeSpace = Capacity - Count;
            int overflow = items.Length - freeSpace;

            if (firstWriteCount >= items.Length)
            {
                Array.Copy(items, 0, buffer, Write, items.Length);
            }
            else
            {
                // Copy first segment starting at Write
                Array.Copy(items, 0, buffer, Write, firstWriteCount);

                // Copy remaining characters starting at the beginning of the buffer
                Array.Copy(items, firstWriteCount, buffer, 0, items.Length - firstWriteCount);
            }

            tail += items.Length;

            if (overflow > 0)
            {
                head += overflow;
            }
        }

        public T Take()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Buffer is empty");
            }

            return buffer[Translate(head++)];
        }

        public T Take(int lookahead)
        {
            if (lookahead > Count)
            {
                throw new ArgumentOutOfRangeException(nameof(lookahead));
            }

            head += lookahead;
            return Take();
        }

        public void Clear()
        {
            head = 0;
            tail = -1;
        }

        public void EnsureCapacity(int size)
        {
            if (size > Capacity)
            {
                // Expand
                var newBuffer = new T[size];

                CopyTo(newBuffer, 0);

                // Reset the head and tail
                tail -= head;
                head = 0;
                buffer = newBuffer;
            }
        }

        public RingBuffer<T> Clone() => new RingBuffer<T>(this);

        public void CopyTo(T[] array, int index)
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

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; ++i)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int Translate(int index) => index % Capacity;
    }
}
