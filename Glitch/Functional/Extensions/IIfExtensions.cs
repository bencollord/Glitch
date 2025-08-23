namespace Glitch.Functional
{
    public static class IIfExtensions
    {
        public static Option<TResult> IIf<T, TResult>(this Option<T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
        public static Result<TResult> IIf<T, TResult>(this Result<T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => ResultQuerySyntax.Select(source, s => @if(s) ? then(s) : @else(s));
        public static Result<TResult, E> IIf<T, E, TResult>(this Result<T, E> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
        public static Effect<TResult> IIf<T, TResult>(this Effect<T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
        public static Effect<TEnv, TResult> IIf<TEnv, T, TResult>(this Effect<TEnv, T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
    }
}
