using System.Collections;

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

        public Option<T> TryPeekFront() => Maybe(items.First).Select(x => x.Value);

        public Option<T> TryPeekBack() => Maybe(items.Last).Select(x => x.Value);

        public void Unshift(T item) 
            => items.AddFirst(item ?? throw new ArgumentNullException(nameof(item)));

        public T Shift() => TryShift()
            .ExpectOrElse(EmptyDequeError)
            .Unwrap();

        public Option<T> TryShift() 
            => TryPeekFront().Do(_ => items.RemoveFirst());

        public void Push(T item)
            => items.AddLast(item ?? throw new ArgumentNullException(nameof(item)));

        public T Pop() => TryPop()
            .ExpectOrElse(EmptyDequeError)
            .Unwrap();

        public Option<T> TryPop() 
            => TryPeekBack().Do(_ => items.RemoveLast());

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        public void Clear() => items.Clear();

        void ICollection<T>.Add(T item) => Push(item);

        bool ICollection<T>.Contains(T item) => items.Contains(item);

        void ICollection<T>.CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<T>.Remove(T item) => items.Remove(item);

        private Error EmptyDequeError() => new ApplicationError("Deque is empty");
    }
}
