using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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

        public DataMember? GetDataMember(string name) =>
            type.GetProperty(name) is PropertyInfo p ?
            DataMember.FromProperty(p) :
            type.GetField(name) is FieldInfo f ?
            DataMember.FromField(f) :
            null;

        public DataMember? GetDataMember(string name, BindingFlags flags) =>
            type.GetProperty(name, flags) is PropertyInfo p ?
            DataMember.FromProperty(p) :
            type.GetField(name, flags) is FieldInfo f ?
            DataMember.FromField(f) :
            null;

        public DataMember[] GetDataMembers() =>
            type.GetFields()
                .Select(DataMember.FromField)
                .Concat(
                    type.GetProperties()
                        .Select(DataMember.FromProperty))
                .ToArray();

        public DataMember[] GetDataMembers(BindingFlags flags) =>
            type.GetFields(flags)
                .Select(DataMember.FromField)
                .Concat(
                    type.GetProperties(flags)
                        .Select(DataMember.FromProperty))
                .ToArray();
    }
}
