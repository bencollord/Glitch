using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glitch.Functional.Attributes
{
    /// <summary>
    /// Identifies the Return or Pure operation of a functor/monad,
    /// like Result.Okay() or Option.Some()
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)] 
    internal class MonadReturnAttribute : Attribute;

    /// <summary>
    /// Identifies the Map or Select operation of a functor/monad.
    /// I.E. M a (a -> b) -> M b
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)] 
    internal class MonadMapAttribute : Attribute;

    /// <summary>
    /// Identifies the Bind or SelectMany operation of a monad.
    /// I.E. M a (a -> M b) -> M b
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    internal class MonadBindAttribute : Attribute;
    
    /// <summary>
    /// Identifies the BindMap operation, which is the SelectMany
    /// overload necessary to enable Linq query syntax.
    /// I.E. M a (a -> M b) -> (a -> b -> c) -> M c
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)] 
    internal class MonadBindMapAttribute : Attribute;

    /// <summary>
    /// Identifies the Apply operation of an applicative/monad,
    /// which essentially is like Map, but with the function wrapped
    /// in a containing monadic type.
    /// </summary>
    /// 
    /// <remarks>
    /// For the types in this library, since C# can't have partially built generics, the order
    /// of operations for an applicative is reversed for instance Apply methods
    /// I.E. M a -> M (a -> b) -> M b, not M (a -> b) -> M a -> M b.
    /// 
    /// Extension methods are provided for the reverse
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    internal class MonadApplyAttribute : Attribute;

    /// <summary>
    /// Identifies the Filter or Where operation of a filterable monad.
    /// </summary>
    /// <remarks>
    /// Not all monads are filterable, since they don't always have sensible empty values.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    internal class MonadFilterAttribute : Attribute;
}
