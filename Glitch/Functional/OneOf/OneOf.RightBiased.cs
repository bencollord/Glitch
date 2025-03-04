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
        public record RightBiasedComputation : OneOf<TLeft, TRight>, IComputation<TRight>
        {
            private readonly OneOf<TLeft, TRight> inner;

            internal RightBiasedComputation(OneOf<TLeft, TRight> inner)
            {
                this.inner = inner;
            }

            public override bool IsLeft => inner.IsRight;

            public override bool IsRight => inner.IsRight;

            public override bool IsLeftAnd(Func<TLeft, bool> predicate) => inner.IsLeftAnd(predicate);

            public override bool IsRightAnd(Func<TRight, bool> predicate) => inner.IsRightAnd(predicate);

            public override LeftBiasedComputation LeftBiased() => new(inner);

            public override RightBiasedComputation RightBiased() => this;

            /// <summary>
            /// Maps the right value if it exists.
            /// </summary>
            /// <remarks>
            /// If <typeparamref name="TRight"/> and <typeparamref name="TRight"/>
            /// are the same, disambiguate using <see cref="MapRight{TResult}"/>
            /// or <see cref="MapRight{TResult}"/>.
            /// </remarks>
            /// <typeparam name="TResult"></typeparam>
            /// <param name="map"></param>
            /// <returns></returns>
            public OneOf<TLeft, TResult>.RightBiasedComputation Map<TResult>(Func<TRight, TResult> map)
                => MapRight(map).RightBiased();

            public OneOf<TLeft, Func<T2, TResult>>.RightBiasedComputation PartialMap<T2, TResult>(Func<TRight, T2, TResult> map)
                => PartialMapRight(map).RightBiased();

            public override OneOf<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> map)
                => inner.MapLeft(map).RightBiased();

            public override OneOf<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> map)
                => inner.MapRight(map).RightBiased();

            public override OneOf<TResult, TRight> AndThenIfLeft<TResult>(Func<TLeft, OneOf<TResult, TRight>> bind)
                => inner.AndThenIfLeft(bind).RightBiased();

            public override OneOf<TLeft, TResult> AndThenIfRight<TResult>(Func<TRight, OneOf<TLeft, TResult>> bind)
                => inner.AndThenIfRight(bind).RightBiased();

            public override TResult Match<TResult>(Func<TLeft, TResult> ifLeft, Func<TRight, TResult> ifRight)
                => inner.Match(ifLeft, ifRight);

            object? IComputation<TRight>.Match() => Match<object?>(left => left, right => right);

            IEnumerable<TRight> IComputation<TRight>.Iterate()
            {
                if (this is OneOf.Right<TLeft, TRight>(var right))
                {
                    yield return right;
                }
            }

            IComputation<TResult> IComputation<TRight>.AndThen<TResult>(Func<TRight, IComputation<TResult>> bind)
            {
                return Match(left => OneOf<TLeft, TResult>.Left(left).RightBiased(), right => bind(right));
            }

            IComputation<TResult> IComputation<TRight>.AndThen<TElement, TResult>(Func<TRight, IComputation<TElement>> bind, Func<TRight, TElement, TResult> project)
            {
                return ((IComputation<TRight>)this).AndThen(x => bind(x).Map(y => project(x, y)));
            }

            IComputation<TResult> IComputation<TRight>.Apply<TResult>(IComputation<Func<TRight, TResult>> function)
            {
                return ((IComputation<TRight>)this).AndThen(x => function.Map(fn => fn(x)));
            }

            IComputation<TResult> IComputation<TRight>.Cast<TResult>()
            {
                return MapRight(l => Some(l).CastOrNone<TResult>())
                    .Match(left => OneOf<TLeft, TResult>.Left(left).RightBiased(),
                           right => right.Match<IComputation<TResult>>(
                               val => OneOf<TLeft, TResult>.Right(val).RightBiased(),
                               () => Option<TResult>.None));
            }

            IComputation<TRight> IComputation<TRight>.Filter(Func<TRight, bool> predicate)
            {
                if (IsRightAnd(predicate) || IsRight)
                {
                    return this;
                }

                return Option<TRight>.None;
            }

            IComputation<TResult> IComputation<TRight>.Map<TResult>(Func<TRight, TResult> map)
            {
                return Map(map);
            }

            IComputation<Func<T2, TResult>> IComputation<TRight>.PartialMap<T2, TResult>(Func<TRight, T2, TResult> map)
            {
                return PartialMap(map);
            }
        }
    }
}
