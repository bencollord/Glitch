using Glitch.Linq;
using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional
{
    [SuppressMessage("Style", "IDE0303:Simplify collection initialization", Justification = "May change semantics")]
    public readonly partial struct OneOrMany<T> : IEquatable<OneOrMany<T>>, IEnumerable<T>
    {
        public static OneOrMany<T> One(T value)
            => value is not null ? new OneOrMany<T>(value) : throw new ArgumentNullException(nameof(value));

        public static OneOrMany<T> Many(IEnumerable<T> values) => new(ImmutableArray.CreateRange(values));

        public static OneOrMany<T> Of(params T[] values) => new(ImmutableArray.Create(values));

        private readonly Option<T> one;
        private readonly ImmutableArray<T> many;

        public OneOrMany(T one)
        {
            ArgumentNullException.ThrowIfNull(one);
            this.one = Some(one);
            many = ImmutableArray<T>.Empty;
        }

        public OneOrMany(ImmutableArray<T> many)
        {
            if (!many.Any())
            {
                throw Errors.NoElements.AsException();
            }

            if (many.Count() == 1)
            {
                one = Some(many[0]);
                many = ImmutableArray<T>.Empty;
            }
            else
            {
                this.many = many;
                one = Option<T>.None;
            }
        }

        public Option<T> this[int index]
        {
            get
            {
                if (IsOne)
                {
                    return index == 0 ? one : None;
                }
                
                if (index < many.Length)
                {
                    return many[index];
                }

                return None;
            }
        }

        public int Count => IsOne ? 1 : many.Length;

        public bool IsOne => one.IsSome;

        public bool IsMany => many.Any();

        public bool IsOneAnd(Func<T, bool> predicate)
            => one.IsSomeAnd(predicate);

        public bool IsManyAnd(Func<T, bool> predicate)
            => many.Any(predicate);

        public T First() => this[0].IfNone(_ => Debug.Fail("OneOrMany was somehow empty")).Unwrap(); // There should always be at least one item.

        public Option<T> FirstOrNone(Func<T, bool> predicate)
        {
            return IsOne ? one.Filter(predicate) : many.FirstOrNone(predicate);
        }

        public OneOrMany<TResult> Map<TResult>(Func<T, TResult> map)
            => Match(
                o => OneOrMany<TResult>.One(map(o)),
                m => OneOrMany<TResult>.Many(m.Select(map)));

        public OneOrMany<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map)
            => Map(map.Curry());

        public OneOrMany<TResult> Apply<TResult>(OneOrMany<Func<T, TResult>> function)
            => AndThen(v => function.Map(fn => fn(v)));

        public OneOrMany<TResult> AndThen<TResult>(Func<T, OneOrMany<TResult>> bind)
        {
            if (IsOne)
            {
                return bind(one.Unwrap());
            }

            var many = from item in this.many
                       from bound in bind(item)
                           .Match(o => [o],
                                  m => m)
                       select bound;

            return OneOrMany.Many(many);
        }

        public OneOrMany<TResult> AndThen<TElement, TResult>(Func<T, OneOrMany<TElement>> bind, Func<T, TElement, TResult> project)
            => AndThen(x => bind(x).Map(y => project(x, y)));

        public OneOrMany<T> Do(Action<T> action)
        {
            one.Do(action);
            many.ForEach(action);
            return this;
        }

        /// <summary>
        /// Maps using the provided function if Some.
        /// Otherwise, returns the fallback value.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifOne"></param>
        /// <param name="ifMany"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<T, TResult> ifOne, Func<IEnumerable<T>, TResult> ifMany)
        {
            var self = this; // Freaking C#

            return one.Match(ifOne, _ => ifMany(self.many));
        }

        public Terminal Match(Action<T> ifOne, Action<IEnumerable<T>> ifMany) => Match(ifOne.Return(), ifMany.Return());

        public OneOrMany<TResult> Cast<TResult>()
            => Map(DynamicCast<TResult>.From);

        public IEnumerable<T> Iterate() => Match(one => [one], many => many);

        public IEnumerator<T> GetEnumerator() => Iterate().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(OneOrMany<T> other)
        {
            if (IsOne && other.IsOne)
            {
                return one.Equals(other.one);
            }

            if (IsMany && other.IsMany)
            {
                return many.SequenceEqual(other.many);
            }

            return false;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is OneOrMany<T> other && Equals(other);

        public override int GetHashCode()
            => Match(o => HashCode.Combine(o), m => HashCode.Combine(m));

        public override string ToString()
            => Match(v => v!.ToString()!, m => m.Join(", "));

        // Since this class implements IEnumerable, we'll put the query syntax methods
        // in with its type so we can override some of the behavior
        public OneOrMany<TResult> Select<TResult>(Func<T, TResult> map) => Map(map);
        public OneOrMany<TResult> SelectMany<TResult>(Func<T, OneOrMany<TResult>> bind) => AndThen(bind);
        public OneOrMany<TResult> SelectMany<TElement, TResult>(Func<T, OneOrMany<TElement>> bind, Func<T, TElement, TResult> project) 
            => AndThen(bind, project);

        public bool All(Func<T, bool> predicate)
            => one.IsSomeAnd(predicate) || many.All(predicate);

        public bool Any(Func<T, bool> predicate)
            => one.IsSomeAnd(predicate) || many.Any(predicate);

        public static implicit operator OneOrMany<T>(T value) => One(value);

        public static implicit operator OneOrMany<T>(T[] many) => Many(many);

        public static bool operator ==(OneOrMany<T> x, OneOrMany<T> y) => x.Equals(y);

        public static bool operator !=(OneOrMany<T> x, OneOrMany<T> y) => !(x == y);
    }
}