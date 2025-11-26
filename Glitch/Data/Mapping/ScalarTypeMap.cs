using System.Data;
using System.Diagnostics;

namespace Glitch.Data.Mapping;

internal class ScalarTypeMap : ITypeMap
{
    private static readonly Dictionary<DbType, Type> DbTypeToClrTypeMap = new()
    {
        [DbType.AnsiString]        = typeof(string),
        [DbType.Binary]            = typeof(byte[]),
        [DbType.Byte]              = typeof(byte),
        [DbType.Boolean]           = typeof(bool),
        [DbType.Currency]          = typeof(decimal),
        [DbType.Date]              = typeof(DateOnly),
        [DbType.DateTime]          = typeof(DateTime),
        [DbType.Decimal]           = typeof(decimal),
        [DbType.Double]            = typeof(double),
        [DbType.Guid]              = typeof(Guid),
        [DbType.Int16]             = typeof(short),
        [DbType.Int32]             = typeof(int),
        [DbType.Int64]             = typeof(long),
        [DbType.Object]            = typeof(object),
        [DbType.SByte]             = typeof(sbyte),
        [DbType.Single]            = typeof(float),
        [DbType.String]            = typeof(string),
        [DbType.Time]              = typeof(TimeOnly),
        [DbType.UInt16]            = typeof(ushort),
        [DbType.UInt32]            = typeof(uint),
        [DbType.UInt64]            = typeof(ulong),
        [DbType.VarNumeric]        = typeof(decimal),
        [DbType.AnsiStringFixedLength] = typeof(string),
        [DbType.StringFixedLength] = typeof(string),
        [DbType.Xml]               = typeof(string),
        [DbType.DateTime2]         = typeof(DateTime),
        [DbType.DateTimeOffset]    = typeof(DateTimeOffset),
    };

    private static readonly Dictionary<Type, DbType> ClrToDbTypeMap = new()
    {
        [typeof(byte[])]     = DbType.Binary,
        [typeof(byte)]       = DbType.Byte,
        [typeof(bool)]       = DbType.Boolean,
        [typeof(DateOnly)]   = DbType.Date,
        [typeof(DateTime)]   = DbType.DateTime,
        [typeof(decimal)]    = DbType.Decimal,
        [typeof(double)]     = DbType.Double,
        [typeof(Guid)]       = DbType.Guid,
        [typeof(short)]      = DbType.Int16,
        [typeof(int)]        = DbType.Int32,
        [typeof(long)]       = DbType.Int64,
        [typeof(object)]     = DbType.Object,
        [typeof(sbyte)]      = DbType.SByte,
        [typeof(float)]      = DbType.Single,
        [typeof(string)]     = DbType.String,
        [typeof(TimeOnly)]   = DbType.Time,
        [typeof(ushort)]     = DbType.UInt16,
        [typeof(uint)]       = DbType.UInt32,
        [typeof(ulong)]      = DbType.UInt64,
        [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
    };

    internal ScalarTypeMap(Type elementType)
    {
        if (!ClrToDbTypeMap.ContainsKey(elementType))
        {
            throw new ArgumentException($"Invalid scalar type {elementType}");
        }

        ElementType = elementType;
    }

    internal ScalarTypeMap(DbType dbType)
    {
        if (!DbTypeToClrTypeMap.TryGetValue(dbType, out var elementType))
        {
            Debug.Fail($"Invalid DbType {dbType}");
            throw new ArgumentException($"Invalid DbType {dbType}");
        }

        ElementType = elementType;
    }

    public Type ElementType { get; }

    internal static bool IsScalarType(Type type) => ClrToDbTypeMap.ContainsKey(type);

    public bool CanMaterialize(IDataRecord record)
    {
        return record.FieldCount == 1 && record.GetFieldType(0).IsAssignableTo(ElementType);
    }

    public object Materialize(IDataRecord record) => record.GetValue(0);
}
