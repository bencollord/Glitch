namespace Glitch.Functional.Core;

public static class OneOf
{
    public static Left<TLeft> Left<TLeft>(TLeft left) => new(left);
    public static Right<TRight> Right<TRight>(TRight right) => new(right);

    public static OneOf<TLeft, TRight> Left<TLeft, TRight>(TLeft left) => new OneOf<TLeft, TRight>.Left(left);
    public static OneOf<TLeft, TRight> Right<TLeft, TRight>(TRight right) => new OneOf<TLeft, TRight>.Right(right);
}