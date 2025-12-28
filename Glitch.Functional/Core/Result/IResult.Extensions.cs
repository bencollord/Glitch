using System.Diagnostics.CodeAnalysis;

namespace Glitch.Functional;

public static partial class ResultExtensions
{
    extension<T, E>(IResult<T, E> self)
    {
        public bool IsOkayAnd(Func<T, bool> predicate) => self.Match(predicate, false);

        public bool IsFailOr(Func<T, bool> predicate) => self.Match(predicate, true);

        public bool IsOkay([MaybeNullWhen(false)] out T value)
        {
            value = self.UnwrapOrDefault();
            return self.IsOkay;
        }

        public bool IsFail([MaybeNullWhen(false)] out E error)
        {
            error = self.UnwrapErrorOrDefault();
            return self.IsFail;
        }
    }
}