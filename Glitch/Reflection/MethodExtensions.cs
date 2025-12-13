using Glitch.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Glitch.Reflection;

public static class MethodExtensions
{
    extension(MethodBase method)
    {
        public AccessModifier AccessLevel =>
            method switch
            {
                { IsPublic: true }            => AccessModifier.Public,
                { IsAssembly: true }          => AccessModifier.Internal,
                { IsFamilyOrAssembly: true }  => AccessModifier.ProtectedInternal,
                { IsFamily: true }            => AccessModifier.Protected,
                { IsFamilyAndAssembly: true } => AccessModifier.PrivateProtected,
                { IsPrivate: true }           => AccessModifier.Private,
                _ => GlitchDebug.Fail("Method should always have access defined").ThenReturn(AccessModifier.Private),
            };
    }

    extension(MethodInfo method)
    {
        public bool IsOverride => method.GetBaseDefinition().DeclaringType != method.DeclaringType;
    }
}
