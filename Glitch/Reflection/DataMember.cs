using System.Reflection;

namespace Glitch.Reflection;

/// <summary>
/// Represents a <see cref="PropertyInfo">property</see> or <see cref="FieldInfo">field</see>
/// of a type and allows reflective getting/setting of the value irrespective of which one it is.
/// </summary>
public abstract class DataMember
{
    private MemberInfo member;

    protected DataMember(MemberInfo member)
    {
        this.member = member; 
    }

    public string Name => member.Name;

    public abstract Type DataType { get; }
    public abstract AccessModifier AccessLevel { get; }
    public abstract bool CanRead { get; }
    public abstract bool CanWrite { get; }
    public abstract bool IsStatic { get; }

    public MemberTypes MemberType => member.MemberType;

    public static DataMember FromField(FieldInfo field) => new FieldDataMember(field);

    public static DataMember FromProperty(PropertyInfo property) => new PropertyDataMember(property);

    public abstract object? GetValue(object? obj);

    public virtual T? GetValue<T>(object? obj) =>
        GetValue(obj) switch
        {
            T down => down,
            null => default,
            object bad => throw new InvalidCastException($"Cannot cast {bad.GetType()} to {typeof(T)}")
        };

    public abstract void SetValue(object? obj, object? value);

    public bool Equals(DataMember? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(other, this)) return true;

        return this.member.Equals(other.member);
    }

    public override bool Equals(object? obj) => Equals(obj as DataMember);

    public override int GetHashCode() => member.GetHashCode();

    public override string? ToString() => member.ToString();

    public static bool operator ==(DataMember? left, DataMember? right) =>
        (left, right) switch
        {
            (null, null) => true,
            (DataMember x, DataMember y) => x.Equals(y),
            _ => false,
        };

    public static bool operator !=(DataMember? left, DataMember? right) => !(left == right);

    public static implicit operator DataMember(FieldInfo field) => FromField(field);

    public static implicit operator DataMember(PropertyInfo property) => FromProperty(property);
}
