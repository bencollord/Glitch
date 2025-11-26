namespace Glitch.Functional.Core;

public abstract partial record OneOf<TLeft, TRight>
{
    public record Left(TLeft Value) : OneOf<TLeft, TRight>
    {
        public override bool IsLeft => true;

        public override bool IsRight => false;

        public override TLeft LeftOr(TLeft fallback) => Value;

        public override TResult Match<TResult>(Func<TLeft, TResult> left, Func<TRight, TResult> right) => left(Value);

        public override TRight RightOr(TRight fallback) => fallback;

        public override OneOf<TResult, TRight> SelectLeft<TResult>(Func<TLeft, TResult> map) =>
            new OneOf<TResult, TRight>.Left(map(Value));

        public override OneOf<TLeft, TResult> SelectRight<TResult>(Func<TRight, TResult> map) =>
            new OneOf<TLeft, TResult>.Left(Value);
        
        public override OneOf<TLeftResult, TRightResult> SelectBoth<TLeftResult, TRightResult>(Func<TLeft, TLeftResult> left, Func<TRight, TRightResult> right) =>
            new OneOf<TLeftResult, TRightResult>.Left(left(Value));
    }
}
