namespace Glitch.Functional.Core;

/// <summary>
/// An arity 2 discriminated union. 
/// 
/// Can be in one of two states: Left or Right, with mapping and
/// matching capabilities for both. Conceptually similar to Either
/// in other languages, but is not a monad and doesn't bias itself
/// towards one side or the other, so it's not suitable for using
/// it as an error handling monad. For that, use <see cref="Result{T, E}"/>.
/// </summary>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight"></typeparam>
public abstract partial record OneOf<TLeft, TRight>
{
    private OneOf() { }

    public abstract bool IsLeft { get; }

    public abstract bool IsRight { get; }

    public abstract OneOf<TResult, TRight> SelectLeft<TResult>(Func<TLeft, TResult> map);

    public abstract OneOf<TLeft, TResult> SelectRight<TResult>(Func<TRight, TResult> map);

    public abstract OneOf<TLeftResult, TRightResult> SelectBoth<TLeftResult, TRightResult>(Func<TLeft, TLeftResult> left, Func<TRight, TRightResult> right);
    
    public abstract TLeft LeftOr(TLeft fallback);

    public abstract TRight RightOr(TRight fallback);

    public abstract TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right);

    public static implicit operator OneOf<TLeft, TRight>(Left<TLeft> left) => new Left(left.Value);

    public static implicit operator OneOf<TLeft, TRight>(Right<TRight> right) => new Right(right.Value);

    public static implicit operator OneOf<TLeft, TRight>(TLeft left) => new Left(left);

    public static implicit operator OneOf<TLeft, TRight>(TRight right) => new Right(right);

    public static explicit operator TLeft(OneOf<TLeft, TRight> oneOf) => oneOf.Match(Identity, r => throw new InvalidCastException(ErrorMessages.InvalidCast<TLeft>(r)));

    public static explicit operator TRight(OneOf<TLeft, TRight> oneOf) => oneOf.Match(l => throw new InvalidCastException(ErrorMessages.InvalidCast<TLeft>(l)), Identity);
}
