using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            Guard.NotNull(type, nameof(type));

            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        public static bool Implements<TInterface>(this Type type) => Implements(type, typeof(TInterface));

        public static bool Implements(this Type type, Type interfaceType)
        {
            Guard.NotNull(type, nameof(type));
            Guard.NotNull(interfaceType, nameof(interfaceType));
            Guard.Require<ArgumentException>(interfaceType.IsInterface, $"Type {interfaceType.Name} must be an interface");

            return type.GetInterfaces().Any(i => i.Equals(interfaceType));
        }

        public static MemberInfo[] GetPropertiesAndFields(this Type type) => EnumeratePropertiesAndFields(type, null).ToArray();

        public static MemberInfo[] GetPropertiesAndFields(this Type type, BindingFlags bindingFlags) => EnumeratePropertiesAndFields(type, bindingFlags).ToArray();

        public static bool IsNullable(this Type type)
        {
            if (!type.IsGenericType) return false;

            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static IEnumerable<MemberInfo> EnumeratePropertiesAndFields(Type type, BindingFlags? bindingFlags)
        {
            var flags = bindingFlags.GetValueOrDefault(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);

            return type.GetProperties(flags).Cast<MemberInfo>().Concat(type.GetFields(flags).Cast<MemberInfo>());
        }
    }
}
