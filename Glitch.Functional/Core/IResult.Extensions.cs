namespace Glitch.Functional
{
    public static partial class ResultExtensions
    {
        extension<T, E>(IResult<T, E> self)
        {
            public bool IsOkayAnd(Func<T, bool> predicate) => self.Match(predicate, false);

            public bool IsFailOr(Func<T, bool> predicate) => self.Match(predicate, true);
        }
    }
}