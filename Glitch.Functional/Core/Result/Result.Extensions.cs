namespace Glitch.Functional;

public static partial class ResultExtensions
{
    public static Result<T, E> Flatten<T, E>(this Result<Result<T, E>, E> source) => source.AndThen(Identity);
}