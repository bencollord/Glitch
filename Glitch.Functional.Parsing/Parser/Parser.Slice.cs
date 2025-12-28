using Glitch.Functional.Parsing.Input;

namespace Glitch.Functional.Parsing;

public static partial class Parser
{
    extension<TToken, T>(IParser<TToken, T> source)
    {
        /// <summary>
        /// Runs the current parser with a <paramref name="slice">function</paramref>
        /// that receives a <see cref="ReadOnlySpan{T}"/> of all the matched tokens,
        /// discarding the <typeparamref name="T">result</typeparamref>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="slice"></param>
        /// <returns></returns>
        public IParser<TToken, TResult> Slice<TResult>(Func<ReadOnlySpan<TToken>, TResult> slice) => new SliceParser<TToken, T, TResult>(source, (span, _) => slice(span));

        /// <summary>
        /// Runs the current parser with a <paramref name="slice">function</paramref>
        /// that receives a <see cref="ReadOnlySpan{T}"/> of all the matched tokens, 
        /// along with the current <typeparamref name="T">parsed result</typeparamref>.
        /// </summary>
        /// <remarks>s
        /// This is straight up lifted from Pidgin's method of the same name here:
        /// https://github.com/benjamin-hodgson/Pidgin/blob/main/Pidgin/Parser.Slice.cs
        /// 
        /// According to the documentation, this allows you to write "pattern-style" parsers,
        /// which I personally have never heard of and the docs don't go into much detail about them.
        /// Adding it to my own library to experiment with and see if I can figure it out.
        /// </remarks>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="slice"></param>
        /// <returns></returns>
        public IParser<TToken, TResult> Slice<TResult>(Func<ReadOnlySpan<TToken>, T, TResult> slice) => new SliceParser<TToken, T, TResult>(source, slice);
    }
}
