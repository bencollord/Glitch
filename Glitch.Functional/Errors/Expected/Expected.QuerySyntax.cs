namespace Glitch.Functional.Errors;

public static partial class ExpectedExtensions
{
    extension<T, TElement, TResult>(Expected<T> self)
    {
        public Expected<TResult> SelectMany(Func<T, Expected<TElement>> bind, Func<T, TElement, TResult> project) =>
            self.AndThen(bind, project);
    }
}
