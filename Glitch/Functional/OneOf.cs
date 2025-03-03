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
        private protected OneOf() { }

        public static OneOf<TLeft, TRight> Left(TLeft value) => new OneOf.Left<TLeft, TRight>(value);

        public static OneOf<TLeft, TRight> Right(TRight value) => new OneOf.Right<TLeft, TRight>(value);

        public abstract bool IsLeft { get; }

        public abstract bool IsRight { get; }

        public abstract bool IsLeftAnd(Func<TLeft, bool> predicate);

        public abstract bool IsRightAnd(Func<TRight, bool> predicate);

        /// <summary>
        /// Maps the left value if it exists.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract OneOf<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> map);

        /// <summary>
        /// Maps the right value if it exists.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public abstract OneOf<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> map);

        public OneOf<Func<T2, TResult>, TRight> PartialMapLeft<T2, TResult>(Func<TLeft, T2, TResult> map)
            => MapLeft(map.Curry());

        public OneOf<TLeft, Func<T2, TResult>> PartialMapRight<T2, TResult>(Func<TRight, T2, TResult> map)
            => MapRight(map.Curry());

        /// <summary>
        /// Maps the left value onto a <see cref="OneOf{TResult, TRight}"/>
        /// and flattens the result to one level.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public abstract OneOf<TResult, TRight> AndThenIfLeft<TResult>(Func<TLeft, OneOf<TResult, TRight>> bind);

        /// <summary>
        /// Maps the right value onto a <see cref="OneOf{TRight, TResult}"/>
        /// and flattens the result to one level.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public abstract OneOf<TLeft, TResult> AndThenIfRight<TResult>(Func<TRight, OneOf<TLeft, TResult>> bind);

        /// <summary>
        /// Resolves the union into a single value using the pair of supplied functions.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifLeft"></param>
        /// <param name="ifRight"></param>
        /// <returns></returns>
        public abstract TResult Match<TResult>(Func<TLeft, TResult> ifLeft, Func<TRight, TResult> ifRight);

        public abstract override string ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual LeftBiasedComputation LeftBiased() => new(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual RightBiasedComputation RightBiased() => new(this);

        public TRight IfLeft(TRight fallback) => IfLeft(() => fallback);

        public TRight IfLeft(Func<TRight> fallback) => Match(_ => fallback(), r => r);

        public TLeft IfRight(TLeft fallback) => IfRight(() => fallback);

        public TLeft IfRight(Func<TLeft> fallback) => Match(l => l, _ => fallback());

        public Option<TLeft> LeftOrNone() => Match(Some, _ => None);

        public Option<TRight> RightOrNone() => Match(_ => None, Some);

        public Result<TLeft> LeftOrFail(Error error) => Match(Okay, _ => error);

        public Result<TRight> RightOrFail(Error error) => Match(_ => error, Okay);

        public Result<TLeft> LeftOrFail(Func<TRight, Error> ifRight) => Match(Okay, r => ifRight(r));

        public Result<TRight> RightOrFail(Func<TLeft, Error> ifLeft) => Match(l => ifLeft(l), Okay);

        public OneOf<TRight, TLeft> Flip() => Match(
            OneOf<TRight, TLeft>.Right,
            OneOf<TRight, TLeft>.Left);

        public static implicit operator OneOf<TLeft, TRight>(TLeft left) => Left(left);

        public static implicit operator OneOf<TLeft, TRight>(TRight right) => Right(right);

        public static implicit operator OneOf<TLeft, TRight>(OneOf.Left<TLeft> left) => Left(left.Value);

        public static implicit operator OneOf<TLeft, TRight>(OneOf.Right<TRight> right) => Right(right.Value);

        public static explicit operator TLeft(OneOf<TLeft, TRight> union)
            => union switch
            {
                OneOf.Left<TLeft, TRight>(var l) => l,
                _ => throw new InvalidCastException($"Cannot cast {union} to {typeof(TLeft)}")
            };

        public static explicit operator TRight(OneOf<TLeft, TRight> union)
            => union switch
            {
                OneOf.Right<TLeft, TRight>(var r) => r,
                _ => throw new InvalidCastException($"Cannot cast {union} to {typeof(TRight)}")
            };

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

            #region IComputation<TLeft>
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
                return MapLeft(l => Some(l).Cast<TResult>())
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
            #endregion
        }

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

            #region IComputation<TRight>
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
                return MapRight(l => Some(l).Cast<TResult>())
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
            #endregion
        }
    }
}
