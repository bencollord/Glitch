namespace Glitch.Functional.Attributes
{
    /// <summary>
    /// Identifies the Bind or SelectMany operation of a monad.
    /// I.E. M a (a -> M b) -> M b
    /// </summary>
    /// <remarks>
    /// Use this attribute to mark whatever the canonical implementation of the bind operation
    /// is for the given type so that it can be noted in any code gen scripts.
    /// 
    /// There are several competing naming conventions for the monadic
    /// bind operation that are often monad-specific. 
    /// In my arrogant opinion, 'bind' is a really stupid name. It comes from the mathematical
    /// terminology from which the concept of monads comes from, in the first place, but in 
    /// practice, most programmers don't have a background in category theory and the concept
    /// of monads in general is already inscrutable enough to most people as it is. As such, 
    /// I prefer to pick names that make the most sense for the given monad.
    /// 
    /// For example, IEnumerable uses SelectMany, which makes sense for a sequence, but not for
    /// an Option or Result monad which will always have at most one value. Rust, C++, and my own
    /// code here use AndThen for binding on alternative value and computation monads.
    /// Some prefer FlatMap, or some even just use Then (like JS promises and Sprache's
    /// parser combinators). In some places, I even use more than one just to make the
    /// code read more like English when I write it (self-indulgent, I know, but I do all this
    /// coding on my days off, so I write it how I want).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    internal class MonadBindAttribute : Attribute;
}
