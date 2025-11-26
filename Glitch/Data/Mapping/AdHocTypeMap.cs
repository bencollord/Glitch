using System.Data;

namespace Glitch.Data.Mapping;

internal class AdHocTypeMap : ITypeMap
{
    internal AdHocTypeMap(Type elementType)
    {
        ElementType = elementType;
    }

    public Type ElementType { get; }

    // UNDONE Need to actually implement
    public bool CanMaterialize(IDataRecord record) => true;

    public object Materialize(IDataRecord record)
    {
        // TODO Naive implementation. Need to verify matches and make sure required fields are set.
        var properties = from p in ElementType.GetProperties()
                         join n in record.GetFieldNames()
                         on p.Name.ToLowerInvariant()
                         equals n.ToLowerInvariant()
                         select (p, record.GetOrdinal(n));

        // TODO Constructor matching
        var obj = Activator.CreateInstance(ElementType) ?? throw new InvalidOperationException($"Activation of type {ElementType} failed");

        foreach (var (property, ordinal) in properties)
        {
            property.SetValue(obj, record.GetValue(ordinal));
        }

        return obj;
    }
}
