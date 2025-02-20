
namespace Glitch.Functional
{
    public static partial class OneOf
    {
        public record Left<TLeft, TRight>(TLeft Value) : OneOf<TLeft, TRight>
        {
            public override bool IsLeft { get; } = true;

            public override bool IsRight { get; } = false;
            public override bool IsLeftAnd(Func<TLeft, bool> predicate) => predicate(Value);

            public override bool IsRightAnd(Func<TRight, bool> predicate) => false;

            public override OneOf<TResult, TRight> MapLeft<TResult>(Func<TLeft, TResult> mapper) 
                => new Left<TResult, TRight>(mapper(Value));

            public override OneOf<TLeft, TResult> MapRight<TResult>(Func<TRight, TResult> _)
                => new Left<TLeft, TResult>(Value);

            public override OneOf<TResult, TRight> AndThen<TResult>(Func<TLeft, OneOf<TResult, TRight>> bind) 
                => bind(Value);

            public override OneOf<TLeft, TResult> OrElse<TResult>(Func<TRight, OneOf<TLeft, TResult>> bind)
                => new Left<TLeft, TResult>(Value);

            public override TResult Match<TResult>(Func<TLeft, TResult> ifLeft, Func<TRight, TResult> _)
                => ifLeft(Value);

            public override string ToString() => $"Left({Value})";
        }
    }
}