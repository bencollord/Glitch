namespace Glitch.Functional
{
    public static class IOExtensions
    {
        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, IO<TElement>> bind, Func<T, TElement, TResult> project) 
            => source.AndThen(bind, project);

        public static IO<TResult> SelectMany<T, TElement, TResult>(this IO<T> source, Func<T, Result<TElement, Error>> bind, Func<T, TElement, TResult> project)
            => from src in source
               let res = bind(src)
               from io in res.Match(
                   okay: IO.Return<TElement>,
                   error: IO.Fail<TElement>)
               select project(src, io);
    }
}