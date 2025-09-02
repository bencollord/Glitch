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
}
