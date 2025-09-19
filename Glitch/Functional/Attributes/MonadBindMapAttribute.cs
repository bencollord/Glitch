namespace Glitch.Functional.Attributes
{
    /// <summary>
    /// Identifies the BindMap operation, which is the SelectMany
    /// overload necessary to enable Linq query syntax.
    /// I.E. M a (a -> M b) -> (a -> b -> c) -> M c
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)] 
    public class MonadBindMapAttribute : Attribute;
}
