using Glitch.Functional.Parsing.Parsers;
using Glitch.Functional.Parsing.Results;

namespace Glitch.Functional.Parsing
{
    public static partial class Parse
    {
        public static Parser<TToken, T> Reference<TToken, T>(Func<Parser<TToken, T>> function) => Reference(new Lazy<Parser<TToken, T>>(function));
        
        public static Parser<TToken, T> Reference<TToken, T>(Lazy<Parser<TToken, T>> lazy) => new LazyParser<TToken, T>(lazy);
        
        /// <summary>
        /// Returns a parser that self-references, allowing recursion.
        /// </summary>
        /// <remarks>
        /// Much like the similar method in Pidgin that inspired this code,
        /// essentially acts as the Y combinator.
        /// </remarks>
        /// <typeparam name="TToken"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public static Parser<TToken, T> Recursive<TToken, T>(Func<Parser<TToken, T>, Parser<TToken, T>> function)
        {
            Parser<TToken, T> result = null!;

            result = Reference(() => function(result));

            return result;
        }
    }
}
