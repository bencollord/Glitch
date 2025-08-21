namespace Glitch.Functional
{
    public static class IOExtensions
    {
        public static IO<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this IO<TEnv, T> source, Func<T, IO<TEnv, TElement>> bind, Func<T, TElement, TResult> project) 
            => source.AndThen(bind, project);

        public static IO<TEnv, TResult> SelectMany<TEnv, T, TElement, TResult>(this IO<TEnv, T> source, Func<T, Result<TElement, Error>> bind, Func<T, TElement, TResult> project)
            => from src in source
               let res = bind(src)
               from io in res.Match(
                   okay: IO.Return<TEnv, TElement>,
                   error: IO.Fail<TEnv, TElement>)
               select project(src, io);

        public static T Run<T>(this IO<Nothing, T> source) => source.Run(Nothing.Value);
    }
}