using System.Data;

namespace Glitch.Data.Mapping;

public interface ITypeMap
{
    abstract Type ElementType { get; }

    bool CanMaterialize(IDataRecord record);

    object Materialize(IDataRecord record);
}
