namespace Glitch.Functional
{
    public static partial class OneOf
    {
        public record One<T>(T Value)
        {
            public OneOf<TLeft, T> AsRightOf<TLeft>() => Right(Value);

            public OneOf<T, TRight> AsLeftOf<TRight>() => Left(Value);
        }

        public record Left<T>(T Value)
        {
            public OneOf<T, TRight> OrRight<TRight>() => Left(Value);
        }

        public record Right<T>(T Value)
        { 
            public OneOf<TLeft, T> OrLeft<TLeft>() => Right(Value);
        }
    }

    /// <summary>
    /// Represents a discriminated union that can be
    /// one value or the other.
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    public abstract partial record OneOf<TLeft, TRight>
    {
        public static OneOf<TLeft, TRight> Left(TLeft value) => new OneOf.Left<TLeft, TRight>(value);
        
        public static OneOf<TLeft, TRight> Right(TRight value) => new OneOf.Right<TLeft, TRight>(value);

        public abstract bool IsLeft { get; }

        public abstract bool IsRight { get; }

        public abstract bool IsLeftAnd(Func<TLeft, bool> predicate);

        public abstract bool IsRightAnd(Func<TRight, bool> predicate);

        /// <summary>
        /// Maps the right value if it exists.
        /// </summary>
        /// <remarks>
        /// If <typeparamref name="TLeft"/> and <typeparamref name="TRight"/>
        /// are the same, disambiguate using <see cref="MapLeft{TResult}"/>
        /// or <see cref="MapRight{TResult}"/>.
        /// </remarks>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public OneOf<TResult, TRight> Map<TResult>(Func<TLeft, TResult> mapper)
            => MapLeft(mapper);

        /// <summary>
        /// Maps the right value if it exists.
        /// </summary>
        /// <remarks>
        /// If <typeparamref name="TLeft"/> and <typeparamref name="TRight"/>
        /// are the same, disambiguate using <see cref="MapLeft{TResult}"/>
        /// or <see cref="MapRight{TResult}"/>.
        /// </remarks>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public OneOf<TLeft, TResult> Map<TResult>(Func<TRight, TResult> mapper)
            => MapRight(mapper);

        /// <summary>
        /// Maps the left value if it exists.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public abstract OneOf<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> mapper);

        /// <summary>
        /// Maps the right value if it exists.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public abstract OneOf<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> mapper);

        /// <summary>
        /// Maps the left value onto a <see cref="OneOf{TResult, TRight}"/>
        /// and flattens the result to one level.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public abstract OneOf<TResult, TRight> AndThen<TResult>(Func<TLeft, OneOf<TResult, TRight>> bind);

        /// <summary>
        /// Maps the right value onto a <see cref="OneOf{TRight, TResult}"/>
        /// and flattens the result to one level.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="bind"></param>
        /// <returns></returns>
        public abstract OneOf<TLeft, TResult> OrElse<TResult>(Func<TRight, OneOf<TLeft, TResult>> bind);

        /// <summary>
        /// Resolves the union into a single value using the pair of supplied functions.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="ifLeft"></param>
        /// <param name="ifRight"></param>
        /// <returns></returns>
        public abstract TResult Match<TResult>(Func<TLeft, TResult> ifLeft, Func<TRight, TResult> ifRight);

        public abstract override string ToString();

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
    }
}
