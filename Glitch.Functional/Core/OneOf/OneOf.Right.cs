namespace Glitch.Functional.Core;

public abstract partial record OneOf<TLeft, TRight>
{
    public record Right(TRight Value) : OneOf<TLeft, TRight>
    {
        public override bool IsLeft => false;

        public override bool IsRight => true;

        public override TRight RightOr(TRight fallback) => Value;

        public override TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right) => right(Value);

        public override TLeft LeftOr(TLeft fallback) => fallback;
    
        public override OneOf<TResult, TRight> SelectLeft<TResult>(Func<TLeft, TResult> map) =>
            new OneOf<TResult, TRight>.Right(Value);

        public override OneOf<TLeft, TResult> SelectRight<TResult>(Func<TRight, TResult> map) =>
            new OneOf<TLeft, TResult>.Right(map(Value));

        public override OneOf<TLeftResult, TRightResult> SelectBoth<TLeftResult, TRightResult>(Func<TLeft, TLeftResult> left, Func<TRight, TRightResult> right) =>
            new OneOf<TLeftResult, TRightResult>.Right(right(Value));
    }
}
