using System.Diagnostics;
using System.Reflection;

namespace Glitch.Reflection;

internal class FieldDataMember : DataMember
{
    private readonly FieldInfo member;

    public FieldDataMember(FieldInfo member) : base(member)
    {
        this.member = member;
    }

    public override Type DataType => member.FieldType;

    public override AccessModifier AccessLevel =>
        member switch
        {
            { IsPublic: true }            => AccessModifier.Public,
            { IsAssembly: true }          => AccessModifier.Internal,
            { IsFamilyOrAssembly: true }  => AccessModifier.ProtectedInternal,
            { IsFamily: true }            => AccessModifier.Protected,
            { IsFamilyAndAssembly: true } => AccessModifier.PrivateProtected,
            { IsPrivate: true }           => AccessModifier.Private,
            _                             => throw new UnreachableException("Field should always have access defined")
        };

    public override bool CanRead => true;

    public override bool CanWrite => !member.IsInitOnly;

    public override bool IsStatic => member.IsStatic;

    public override object? GetValue(object? obj) => member.GetValue(obj);

    public override void SetValue(object? obj, object? value) => member.SetValue(obj, value);
}
