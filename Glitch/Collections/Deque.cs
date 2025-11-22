using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Collections
{
    public class Deque<T> : ICollection<T>
    {
        private LinkedList<T> items;

        public Deque()
        {
            items = new LinkedList<T>();
        }

        public Deque(IEnumerable<T> items)
        {
            this.items = new LinkedList<T>(items);
        }

        public int Count => items.Count;

        bool ICollection<T>.IsReadOnly => false;

        public bool TryPeekFront([NotNullWhen(true)] out T? value) => TryGetNodeValue(items.First, out value);

        public bool TryPeekBack([NotNullWhen(true)] out T? value) => TryGetNodeValue(items.Last, out value);

        public void Unshift(T item) 
            => items.AddFirst(item ?? throw new ArgumentNullException(nameof(item)));

        public T Shift() => TryShift(out T? value) ? value : throw EmptyDequeError();

        public bool TryShift([NotNullWhen(true)] out T? value)
        {
            if (TryPeekFront(out value))
            {
                items.RemoveFirst();
                return true;
            }

            return false;
        }

        public void Push(T item)
            => items.AddLast(item ?? throw new ArgumentNullException(nameof(item)));

        public T Pop() => TryPop(out T? value) ? value : throw EmptyDequeError();

        public bool TryPop([NotNullWhen(true)] out T? value)
        {
            if (TryPeekBack(out value))
            {
                items.RemoveLast();
                return true;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        public void Clear() => items.Clear();

        void ICollection<T>.Add(T item) => Push(item);

        bool ICollection<T>.Contains(T item) => items.Contains(item);

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<T>.Remove(T item) => items.Remove(item);

        private Exception EmptyDequeError() => new InvalidOperationException("Deque is empty");

        private bool TryGetNodeValue(LinkedListNode<T>? node, [NotNullWhen(true)] out T? value)
        {
            if (node is null || node.Value is null)
            {
                value = default;
                return false;
            }

            value = node.Value;

            return true;
        }
    }
}
