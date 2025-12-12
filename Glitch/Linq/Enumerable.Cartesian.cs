namespace Glitch.Linq;

public static partial class EnumerableExtensions
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        public IEnumerable<TResult> Cartesian<TResult>(Func<TSource, TSource, TResult> projection) => source.CrossJoin(source, projection);

        public IEnumerable<(TSource First, TOther Second)> CrossJoin<TOther, TResult>(IEnumerable<TOther> other) => source.CrossJoin(other, (x, y) => (x, y));
        
        public IEnumerable<TResult> CrossJoin<TOther, TResult>(IEnumerable<TOther> other, Func<TSource, TOther, TResult> projection) => source.SelectMany(_ => other, projection);
    }
}
