using Glitch.Diagnostics;
using System.Reflection;

namespace Glitch.Reflection;

public static class FieldExtensions
{
    extension(FieldInfo field)
    {
        public AccessModifier AccessLevel =>
            @field switch
            {
                { IsPublic:            true } => AccessModifier.Public,
                { IsAssembly:          true } => AccessModifier.Internal,
                { IsFamilyOrAssembly:  true } => AccessModifier.ProtectedInternal,
                { IsFamily:            true } => AccessModifier.Protected,
                { IsFamilyAndAssembly: true } => AccessModifier.PrivateProtected,
                { IsPrivate:           true } => AccessModifier.Private,
                _ => GlitchDebug.Fail("Method should always have access defined").ThenReturn(AccessModifier.Private),
            };
    }
}
