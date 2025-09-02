using Glitch.Functional.Results;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Glitch.Functional
{
    public static class IIfExtensions
    {
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Option<TResult> IIf<T, TResult>(this Option<T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
        
        [return: NotNull]
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] 
        public static Result<TResult> IIf<T, TResult>(this Result<T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
        
        [return: NotNull] 
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expected<TResult, E> IIf<T, E, TResult>(this Expected<T, E> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));

        [return: NotNull] 
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Effect<TResult> IIf<T, TResult>(this Effect<T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
        
        [return: NotNull] 
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Effect<TEnv, TResult> IIf<TEnv, T, TResult>(this Effect<TEnv, T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
        
        [return: NotNull] 
        [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sequence<TResult> IIf<T, TResult>(this Sequence<T> source, Func<T, bool> @if, Func<T, TResult> then, Func<T, TResult> @else) => source.Select(s => @if(s) ? then(s) : @else(s));
    }
}
