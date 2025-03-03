
namespace Glitch.Functional.ComputationQuery
{
    public static class Computation
    {
        public static readonly NoElementsError NoElements = new();

        public static readonly MoreThanOneElementError MoreThanOneElement = new();

        public static IComputation<T> AsComputation<T>(this IComputation<T> source) => source;

        public static IComputation<TResult> Select<T, TResult>(this IComputation<T> source, Func<T, TResult> map)
            => source.Map(map);

        public static IComputation<TResult> SelectMany<T, TResult>(this IComputation<T> source, Func<T, IComputation<TResult>> bind)
            => source.AndThen(bind);

        public static IComputation<TResult> SelectMany<T, TElement, TResult>(this IComputation<T> source, Func<T, IComputation<TElement>> bind, Func<T, TElement, TResult> project)
            => source.AndThen(bind, project);

        public static IComputation<T> Where<T>(this IComputation<T> source, Func<T, bool> predicate)
            => source.Filter(predicate);

        public static IComputation<TResult> Join<TLeft, TRight, TKey, TResult>(
            this IComputation<TLeft> left, 
            IComputation<TRight> right, 
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector, 
            Func<TLeft, TRight, TResult> resultSelector)
            => left.Join(right, leftKeySelector, rightKeySelector, resultSelector, EqualityComparer<TKey>.Default);

        public static IComputation<TResult> Join<TLeft, TRight, TKey, TResult>(
            this IComputation<TLeft> left,
            IComputation<TRight> right,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
            => left.AndThen(_ => right, (left, right) => new { left, right })
                   .Filter(pair => comparer.Equals((leftKeySelector(pair.left), rightKeySelector(pair.right))))
                   .Map(pair => resultSelector(pair.left, pair.right));

        // TODO Decide if these work better as extension methods or on the interface
        public static Sequence<T> ToSequence<T>(this IComputation<T> source)
            => source is Sequence<T> seq ? seq : source.Iterate().AsSequence();

        // TODO Uncomment or remove
        //public static Option<T> ToOption<T>(this IComputation<T> source)
        //    => source switch
        //    {
        //        Option<T> opt => opt,
        //        Identity<T> identity => Some(identity.Value),
        //        Sequence<T> seq => seq.Match(
        //            ifSingle: Some,
        //            ifMultiple: _ => throw MoreThanOneElement.AsInvalidOperationException(),
        //            ifNone: Option.None<T>),

        //        _ => source.Run()
        //                   .MapError(err => err == MoreThanOneElement 
        //                                 ? throw err.AsInvalidOperationException() 
        //                                 : err)
        //                   .UnwrapOrNone()
        //    };

        public static Result<T> Run<T>(this IComputation<T> source)
            => source switch
            {
                Result<T> res => res,
                Fallible<T> fal => fal.Run(),
                Option<T> opt => opt.OkayOr(NoElements),
                Identity<T> identity => Okay(identity.Value),
                Sequence<T> seq => seq.Match(
                    ifSingle: Okay,
                    ifMultiple: _ => Fail<T>(MoreThanOneElement),
                    ifNone: () => Fail<T>(NoElements)),

                // Fallback to custom implementations
                _ => source.Match() switch
                {
                    T val => Okay(val),
                    Error err => Fail<T>(err),
                    IEnumerable<T> _ => Fail<T>(MoreThanOneElement),
                    _ => Fail<T>(NoElements)
                }
            };

        public static Fallible<T> TryRun<T>(this IComputation<T> source)
            => source is Fallible<T> fallible ? fallible : Fallible.Lift(source.Run());

        private static InvalidOperationException AsInvalidOperationException(this Error error) => new(error.Message);

        public class NoElementsError : ApplicationError
        {
            internal NoElementsError() : base("Computation contains no elements")
            {
            }
        }

        public class MoreThanOneElementError : ApplicationError
        {
            internal MoreThanOneElementError() : base("Computation contains more than one element")
            {
            }
        }
    }
}