namespace Glitch.Functional.Attributes
{
    /// <summary>
    /// Specifies a class contains extension methods which implement monadic operations
    /// for a type specified by the <see cref="TargetType"/> property.
    /// </summary>
    /// 
    /// <remarks>
    /// This attribute can be applied multiple times if the module contains multiple monad implementations.
    /// This is most practical for types like <see cref="Result{T}"/> and <see cref="Result{T,E}"/>, which are
    /// essentially the same type with one of the type parameters set (because C# doesn't support template specialization).
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MonadExtensionAttribute : Attribute
    {
        public MonadExtensionAttribute(Type targetType)
        {
            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        /// <summary>
        /// Gets the generic type definition for the monad being implemented.
        /// </summary>
        public Type TargetType { get; }
    }
}
