using System.Data;

namespace Glitch.Data;

public record ColumnInfo
{
    private const string TableCatalogColumn       = "TABLE_CATALOG";
    private const string TableSchemaColumn        = "TABLE_SCHEMA";
    private const string TableNameColumn          = "TABLE_NAME";
    private const string ColumnNameColumn         = "COLUMN_NAME";
    private const string OrdinalPositionColumn    = "ORDINAL_POSITION";
    private const string ColumnDefaultColumn      = "COLUMN_DEFAULT";
    private const string IsNullableColumn         = "IS_NULLABLE";
    private const string DataTypeColumn           = "DATA_TYPE";
    private const string CharacterMaximumLengthColumn = "CHARACTER_MAXIMUM_LENGTH";
    private const string CharacterOctetLengthColumn   = "CHARACTER_OCTET_LENGTH";
    private const string NumericPrecisionColumn   = "NUMERIC_PRECISION";
    private const string NumericPrecisionRadixColumn  = "NUMERIC_PRECISION_RADIX";
    private const string NumericScaleColumn       = "NUMERIC_SCALE";
    private const string DateTimePrecisionColumn  = "DATETIME_PRECISION";
    private const string CharacterSetCatalogColumn= "CHARACTER_SET_CATALOG";
    private const string CharacterSetSchemaColumn = "CHARACTER_SET_SCHEMA";
    private const string CharacterSetNameColumn   = "CHARACTER_SET_NAME";
    private const string CollationCatalogColumn   = "COLLATION_CATALOG";
    private const string CollationSchemaColumn    = "COLLATION_SCHEMA";
    private const string CollationNameColumn      = "COLLATION_NAME";
    private const string DomainCatalogColumn      = "DOMAIN_CATALOG";
    private const string DomainSchemaColumn       = "DOMAIN_SCHEMA";
    private const string DomainNameColumn         = "DOMAIN_NAME";

    public string? TableCatalog { get; init; }
    public string? TableSchema { get; init; }
    public string? TableName { get; init; }
    public string? ColumnName { get; init; }
    public int? OrdinalPosition { get; init; }
    public object? ColumnDefault { get; init; }
    public bool? IsNullable { get; init; }
    public SqlDbType? DataType { get; init; }
    public int? CharacterMaximumLength { get; init; }
    public int? CharacterOctetLength { get; init; }
    public byte? NumericPrecision { get; init; }
    public short? NumericPrecisionRadix { get; init; }
    public int? NumericScale { get; init; }
    public short? DateTimePrecision { get; init; }
    public string? CharacterSetCatalog { get; init; }
    public string? CharacterSetSchema { get; init; }
    public string? CharacterSetName { get; init; }
    public string? CollationCatalog { get; init; }
    public string? CollationSchema { get; init; }
    public string? CollationName { get; init; }
    public string? DomainCatalog { get; init; }
    public string? DomainSchema { get; init; }
    public string? DomainName { get; init; }

    public static ColumnInfo FromDataRow(DataRow row)
    {
        return new ColumnInfo
        {
            TableCatalog        = row[TableCatalogColumn]?.ToString(),
            TableSchema         = row[TableSchemaColumn]?.ToString(),
            TableName           = row[TableNameColumn]?.ToString(),
            ColumnName          = row[ColumnNameColumn]?.ToString(),
            OrdinalPosition     = (int)row[OrdinalPositionColumn],
            ColumnDefault       = row[ColumnDefaultColumn]?.ToString(),
            IsNullable          = row[IsNullableColumn]?.ToString()?.Equals("YES", StringComparison.OrdinalIgnoreCase),
            DataType            = Enum.TryParse<SqlDbType>(row[DataTypeColumn]?.ToString(), ignoreCase: true, out var result) ? result : null,
            CharacterMaximumLength  = !row.IsNull(CharacterMaximumLengthColumn) ? (int)row[CharacterMaximumLengthColumn] : new int?(),
            CharacterOctetLength= !row.IsNull(CharacterOctetLengthColumn) ? (int)row[CharacterOctetLengthColumn] : new int?(),
            NumericPrecision    = !row.IsNull(NumericPrecisionColumn) ? (byte)row[NumericPrecisionColumn] : new byte?(),
            NumericPrecisionRadix   = !row.IsNull(NumericPrecisionRadixColumn) ? (short)row[NumericPrecisionRadixColumn] : new short?(),
            NumericScale        = !row.IsNull(NumericScaleColumn) ? (int)row[NumericScaleColumn] : new int?(),
            DateTimePrecision   = !row.IsNull(DateTimePrecisionColumn) ? (short)row[DateTimePrecisionColumn] : new short?(),
            CharacterSetCatalog = row[CharacterSetCatalogColumn]?.ToString(),
            CharacterSetSchema  = row[CharacterSetSchemaColumn]?.ToString(),
            CharacterSetName    = row[CharacterSetNameColumn]?.ToString(),
            CollationCatalog    = row[CollationCatalogColumn]?.ToString(),
            CollationSchema     = row[CollationSchemaColumn]?.ToString(),
            CollationName       = row[CollationNameColumn]?.ToString(),
            DomainCatalog       = row[DomainCatalogColumn]?.ToString(),
            DomainSchema        = row[DomainSchemaColumn]?.ToString(),
            DomainName          = row[DomainNameColumn]?.ToString(),
        };
    }
}
