
namespace Glitch.Functional
{
    public interface IComputation<T>
    {
        object? Match();
        IComputation<TResult> AndThen<TElement, TResult>(Func<T, IComputation<TElement>> bind, Func<T, TElement, TResult> project);
        IComputation<TResult> AndThen<TResult>(Func<T, IComputation<TResult>> bind);
        IComputation<TResult> Apply<TResult>(IComputation<Func<T, TResult>> function);
        IComputation<TResult> Cast<TResult>();
        IComputation<T> Filter(Func<T, bool> predicate);
        IComputation<TResult> Map<TResult>(Func<T, TResult> map);
        IComputation<Func<T2, TResult>> PartialMap<T2, TResult>(Func<T, T2, TResult> map);
        IEnumerable<T> Iterate();
    }
}