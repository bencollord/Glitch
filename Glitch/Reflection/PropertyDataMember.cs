using System.Diagnostics;
using System.Reflection;

namespace Glitch.Reflection;

internal class PropertyDataMember : DataMember
{
    private readonly PropertyInfo property;

    public PropertyDataMember(PropertyInfo property) : base(property)
    {
        this.property = property;
    }

    public override Type DataType => property.PropertyType;

    public override AccessModifier AccessLevel =>
        property.GetMethod?.AccessLevel ?? property.SetMethod?.AccessLevel ?? throw new UnreachableException();

    public override bool CanRead => property.GetMethod is not null;

    public override bool CanWrite => property.SetMethod is not null;

    public override bool IsStatic => property.GetMethod?.IsStatic ?? property.SetMethod?.IsStatic ?? false;

    public override object? GetValue(object? obj) => property.GetValue(obj);

    public override void SetValue(object? obj, object? value) => property.SetValue(obj, value);
}
