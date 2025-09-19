namespace Glitch.Functional.Attributes
{
    /// <summary>
    /// Identifies the Map or Select operation of a functor/monad.
    /// I.E. M a (a -> b) -> M b
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)] 
    public class MonadMapAttribute : Attribute;
}
