using System.Linq.Expressions;
using System.Reflection;

namespace Glitch.Linq;

/// <summary>
    /// Convenience class for holding <see cref="MethodInfo" /> objects
    /// for <see cref="Queryable" /> methods.
    /// </summary>
internal static class QueryableMethods
{
    public static readonly MethodInfo Select = new Func<
            IQueryable<object>, 
            Expression<Func<object, object>>, 
            IQueryable<object>
        >(Queryable.Select).Method
                           .GetGenericMethodDefinition();
}
