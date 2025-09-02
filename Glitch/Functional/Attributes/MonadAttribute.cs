namespace Glitch.Functional.Attributes
{
    /// <summary>
    /// Specifies that a type is a monad so that it can be found
    /// by code gen scripts when adding extensions that apply to
    /// all monadic types.
    /// </summary>
    /// <remarks>
    /// Since C# unfortunately doesn't support higher-kinded types, a lot
    /// of per monad boilerplate has to be written for things like Traverse
    /// or IIf that work for all of them. Types marked with this attribute
    /// will be included in any generation scripts.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class MonadAttribute : Attribute;
}
