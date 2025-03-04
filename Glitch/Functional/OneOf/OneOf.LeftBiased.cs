using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    /// <summary>
    /// Represents a discriminated union that can be
    /// one value or the other.
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public abstract partial record OneOf<TLeft, TRight>
    {
        public record LeftBiasedComputation : OneOf<TLeft, TRight>, IComputation<TLeft>
        {
            private OneOf<TLeft, TRight> inner;
            internal LeftBiasedComputation(OneOf<TLeft, TRight> inner)
            {
                this.inner = inner;
            }

            public override bool IsLeft => inner.IsLeft;

            public override bool IsRight => inner.IsRight;

            public override bool IsLeftAnd(Func<TLeft, bool> predicate) => inner.IsLeftAnd(predicate);

            public override bool IsRightAnd(Func<TRight, bool> predicate) => inner.IsRightAnd(predicate);

            public override LeftBiasedComputation LeftBiased() => this;

            public override RightBiasedComputation RightBiased() => new(inner);

            /// <summary>
            /// Maps the right value if it exists.
            /// </summary>
            /// <remarks>
            /// If <typeparamref name="TLeft"/> and <typeparamref name="TRight"/>
            /// are the same, disambiguate using <see cref="MapLeft{TResult}"/>
            /// or <see cref="MapRight{TResult}"/>.
            /// </remarks>
            /// <typeparam name="TResult"></typeparam>
            /// <param name="map"></param>
            /// <returns></returns>
            public OneOf<TResult, TRight>.LeftBiasedComputation Map<TResult>(Func<TLeft, TResult> map)
                => MapLeft(map).LeftBiased();

            public OneOf<Func<T2, TResult>, TRight>.LeftBiasedComputation PartialMap<T2, TResult>(Func<TLeft, T2, TResult> map)
                => PartialMapLeft(map).LeftBiased();

            public override OneOf<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> map)
                => inner.MapLeft(map).LeftBiased();

            public override OneOf<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> map)
                => inner.MapRight(map).LeftBiased();

            public override OneOf<TResult, TRight> AndThenIfLeft<TResult>(Func<TLeft, OneOf<TResult, TRight>> bind)
                => inner.AndThenIfLeft(bind).LeftBiased();

            public override OneOf<TLeft, TResult> AndThenIfRight<TResult>(Func<TRight, OneOf<TLeft, TResult>> bind)
                => inner.AndThenIfRight(bind).LeftBiased();

            public override TResult Match<TResult>(Func<TLeft, TResult> ifLeft, Func<TRight, TResult> ifRight)
                => inner.Match(ifLeft, ifRight);

            object? IComputation<TLeft>.Match() => Match<object?>(left => left, right => right);

            IEnumerable<TLeft> IComputation<TLeft>.Iterate()
            {
                if (this is OneOf.Left<TLeft, TRight>(var left))
                {
                    yield return left;
                }
            }

            IComputation<TResult> IComputation<TLeft>.AndThen<TResult>(Func<TLeft, IComputation<TResult>> bind)
            {
                return Match(left => bind(left), right => OneOf<TResult, TRight>.Right(right).LeftBiased());
            }

            IComputation<TResult> IComputation<TLeft>.AndThen<TElement, TResult>(Func<TLeft, IComputation<TElement>> bind, Func<TLeft, TElement, TResult> project)
            {
                return ((IComputation<TLeft>)this).AndThen(x => bind(x).Map(y => project(x, y)));
            }

            IComputation<TResult> IComputation<TLeft>.Apply<TResult>(IComputation<Func<TLeft, TResult>> function)
            {
                return ((IComputation<TLeft>)this).AndThen(x => function.Map(fn => fn(x)));
            }

            IComputation<TResult> IComputation<TLeft>.Cast<TResult>()
            {
                return MapLeft(l => Some(l).CastOrNone<TResult>())
                    .Match(
                        left => left.Match<IComputation<TResult>>(
                                        val => OneOf<TResult, TRight>.Left(val).LeftBiased(),
                                        () => Option<TResult>.None),
                        right => OneOf<TResult, TRight>.Right(right).LeftBiased());
            }

            IComputation<TLeft> IComputation<TLeft>.Filter(Func<TLeft, bool> predicate)
            {
                if (IsLeftAnd(predicate) || IsRight)
                {
                    return this;
                }

                return Option<TLeft>.None;
            }

            IComputation<TResult> IComputation<TLeft>.Map<TResult>(Func<TLeft, TResult> map)
            {
                return Map(map);
            }

            IComputation<Func<T2, TResult>> IComputation<TLeft>.PartialMap<T2, TResult>(Func<TLeft, T2, TResult> map)
            {
                return PartialMap(map);
            }
        }
    }
}
