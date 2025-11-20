using Glitch.Functional.Core;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional.Collections
{
    [Monad]
    public partial class Sequence<T> : IEnumerable<T>
    {
        public static readonly Sequence<T> Empty = new([]);

        private readonly IEnumerable<T> items;

        public Sequence(IEnumerable<T> items)
        {
            this.items = items;
        }

        public Sequence<TResult> Select<TResult>(Func<T, TResult> map)
            => new(items.Select(map));

        public Sequence<TResult> Select<TResult>(Func<T, int, TResult> map)
            => new(items.Select(map));

        public Sequence<(int Index, T Item)> Index() => new(items.Index());

        public Sequence<Func<T2, TResult>> PartialSelect<T2, TResult>(Func<T, T2, TResult> mapper)
            => Select(mapper.Curry());

        public Sequence<TResult> Apply<TResult>(Sequence<Func<T, TResult>> function)
            => AndThen(v => function.Select(fn => fn(v)));

        public Sequence<T> Concat(IEnumerable<T> other)
            => new(items.Concat(other));

        public Sequence<T> Prepend(params T[] items) => new(items.Concat(this.items));

        public Sequence<T> Append(params T[] items) => Concat(items);

        public Sequence<T> Skip(int count) => new(items.Skip(count));

        public Sequence<T> Take(int count) => new(items.Take(count));

        public Sequence<TResult> AndThen<TResult>(Func<T, IEnumerable<TResult>> mapper)
            => new(items.SelectMany(mapper));

        public Sequence<TResult> AndThen<TElement, TResult>(Func<T, IEnumerable<TElement>> bind, Func<T, TElement, TResult> project)
            => new(items.SelectMany(bind, project));

        public Sequence<T> Union(Sequence<T> other) => new(items.Union(other.items));

        public Sequence<T> Intersect(Sequence<T> other) => new(items.Intersect(other.items));
        
        public Sequence<T> Except(Sequence<T> other) => new(items.Except(other.items));

        public Sequence<T> Where(Func<T, bool> predicate)
            => new(items.Where(predicate));

        public Sequence<(T, TOther)> Zip<TOther>(Sequence<TOther> other)
            => Zip(other, (x, y) => (x, y));

        public Sequence<TResult> Zip<TOther, TResult>(Sequence<TOther> other, Func<T, TOther, TResult> zipper)
            => AndThen(x => other, zipper);

        public Sequence<T> ForEach(Action<T> action)
        {
            IEnumerable<T> Iter()
            {
                foreach (var item in items)
                {
                    action(item);
                    yield return item;
                }
            }

            return new(Iter());
        }

        public TResult Match<TResult>(Func<T, TResult> ifSingle, Func<IEnumerable<T>, TResult> ifMultiple, Func<TResult> ifNone)
            => items.Count() switch
            {
                0 => ifNone(),
                1 => ifSingle(items.Single()),
                _ => ifMultiple(items)
            };

        public Sequence<TResult> Cast<TResult>()
            => Select(val => (TResult)(dynamic)val!);

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Sequence<T> IfEmpty(T fallback) => new(items.DefaultIfEmpty(fallback));

        public Sequence<T> IfEmpty(Func<T> fallback)
        {
            IEnumerable<T> Iter()
            {
                using var iter = items.GetEnumerator();

                if (!iter.MoveNext())
                {
                    yield return fallback();
                    yield break;
                }

                do
                {
                    yield return iter.Current;
                }
                while (iter.MoveNext());
            }

            return new(Iter());
        }

        public bool Equals(Sequence<T> other) => items.SequenceEqual(other.items);

        public override bool Equals([NotNullWhen(true)] object? obj) 
            => obj is Sequence<T> other && Equals(other);

        public override int GetHashCode() 
            => HashCode.Combine(items.Select(i => i?.GetHashCode() ?? 0));

        public override string ToString()
            => string.Join(", ", items.Select(i => i?.ToString() ?? "null").DefaultIfEmpty("Empty"));

        public static Sequence<T> operator +(Sequence<T> x, Sequence<T> y) => x.Concat(y);
        public static Sequence<T> operator +(Sequence<T> seq, T item) => seq.Append(item);
        public static Sequence<T> operator +(T item, Sequence<T> seq) => seq.Prepend(item);

        public static bool operator ==(Sequence<T> x, Sequence<T> y) => x.Equals(y);

        public static bool operator !=(Sequence<T> x, Sequence<T> y) => !(x == y);
    }
}
