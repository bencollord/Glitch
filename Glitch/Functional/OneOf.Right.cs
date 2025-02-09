
namespace Glitch.Functional
{
    public abstract partial record OneOf<TLeft, TRight>
    {
        public record Right(TRight Value) : OneOf<TLeft, TRight>
        {
            public override bool IsLeft { get; } = false;

            public override bool IsRight { get; } = true;

            public override bool IsLeftAnd(Func<TLeft, bool> predicate) => false;

            public override bool IsRightAnd(Func<TRight, bool> predicate) => predicate(Value);

            public override OneOf<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> _)
                => new OneOf<TResult, TRight>.Right(Value);

            public override OneOf<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> mapper)
                => new OneOf<TLeft, TResult>.Right(mapper(Value));

            public override OneOf<TResult, TRight> AndThen<TResult>(Func<TLeft, OneOf<TResult, TRight>> bind)
                => new OneOf<TResult, TRight>.Right(Value);

            public override OneOf<TLeft, TResult> OrElse<TResult>(Func<TRight, OneOf<TLeft, TResult>> bind)
                => bind(Value);

            public override TResult Match<TResult>(Func<TLeft, TResult> _, Func<TRight, TResult> ifRight)
                => ifRight(Value);
        }
    }
}