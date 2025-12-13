namespace Glitch.Reflection;

public static class TypeExtensions
{
    extension(Type type)
    {
        public AccessModifier AccessLevel =>
            type switch
            {
                { IsPublic:            true } => AccessModifier.Public,
                { IsNestedFamORAssem:  true } => AccessModifier.ProtectedInternal,
                { IsNestedFamily:      true } => AccessModifier.Protected,
                { IsNestedFamANDAssem: true } => AccessModifier.PrivateProtected,
                { IsNestedPrivate:     true } => AccessModifier.Private,
                _                             => AccessModifier.Internal,
            };
    }
}
