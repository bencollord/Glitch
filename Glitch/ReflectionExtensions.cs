using System.Reflection;

namespace Glitch;

public static class ReflectionExtensions
{
    public static T? GetValue<T>(this FieldInfo field, object? instance)
        => field.GetValue(instance) is object value ? (T)value : default;

    public static T? GetValue<T>(this PropertyInfo property, object? instance)
        => property.GetValue(instance) is object value ? (T)value : default;
}
