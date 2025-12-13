using System.Reflection;

namespace Glitch.Reflection;

public static class MemberExtensions
{
    extension(MemberInfo member)
    {
        public bool HasCustomAttribute<T>() 
            where T : Attribute => 
            member.GetCustomAttribute<T>() != null;

        public bool HasCustomAttribute(Type attributeType) =>
            member.GetCustomAttribute(attributeType) != null;
    }
}
