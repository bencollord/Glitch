using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public class ColumnDefinition
    {
        public ColumnDefinition() { }

        public ColumnDefinition(DataRow row)
        {
            TableCatalog = row["TABLE_CATALOG"].ToString();
            TableSchema = row["TABLE_SCHEMA"].ToString();
            TableName = row["TABLE_NAME"].ToString();
            ColumnName = row["COLUMN_NAME"].ToString();
            OrdinalPosition = (int)row["ORDINAL_POSITION"];
            ColumnDefault = row["COLUMN_DEFAULT"].ToString();
            IsNullable = row["IS_NULLABLE"].ToString().Equals("YES", StringComparison.OrdinalIgnoreCase);
            DataType = (SqlDbType)Enum.Parse(typeof(SqlDbType), row["DATA_TYPE"].ToString(), ignoreCase: true);
            CharacterMaximumLength = !row.IsNull("CHARACTER_MAXIMUM_LENGTH") ? (int)row["CHARACTER_MAXIMUM_LENGTH"] : new int?();
            CharacterOctetLength = !row.IsNull("CHARACTER_OCTET_LENGTH") ? (int)row["CHARACTER_OCTET_LENGTH"] : new int?();
            NumericPrecision = !row.IsNull("NUMERIC_PRECISION") ? (byte)row["NUMERIC_PRECISION"] : new byte?();
            NumericPrecisionRadix = !row.IsNull("NUMERIC_PRECISION_RADIX") ? (short)row["NUMERIC_PRECISION_RADIX"] : new short?();
            NumericScale = !row.IsNull("NUMERIC_SCALE") ? (int)row["NUMERIC_SCALE"] : new int?();
            DateTimePrecision = !row.IsNull("DATETIME_PRECISION") ? (short)row["DATETIME_PRECISION"] : new short?();
            CharacterSetCatalog = row["CHARACTER_SET_CATALOG"].ToString();
            CharacterSetSchema = row["CHARACTER_SET_SCHEMA"].ToString();
            CharacterSetName = row["CHARACTER_SET_NAME"].ToString();
            CollationCatalog = row["COLLATION_CATALOG"].ToString();
            CollationSchema = row["COLLATION_SCHEMA"].ToString();
            CollationName = row["COLLATION_NAME"].ToString();
            DomainCatalog = row["DOMAIN_CATALOG"].ToString();
            DomainSchema = row["DOMAIN_SCHEMA"].ToString();
            DomainName = row["DOMAIN_NAME"].ToString();
        }

        public string TableCatalog { get; set; }
        public string TableSchema { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public int? OrdinalPosition { get; set; }
        public object ColumnDefault { get; set; }
        public bool IsNullable { get; set; }
        public SqlDbType DataType { get; set; }
        public int? CharacterMaximumLength { get; set; }
        public int? CharacterOctetLength { get; set; }
        public byte? NumericPrecision { get; set; }
        public short? NumericPrecisionRadix { get; set; }
        public int? NumericScale { get; set; }
        public short? DateTimePrecision { get; set; }
        public string CharacterSetCatalog { get; set; }
        public string CharacterSetSchema { get; set; }
        public string CharacterSetName { get; set; }
        public string CollationCatalog { get; set; }
        public string CollationSchema { get; set; }
        public string CollationName { get; set; }
        public string DomainCatalog { get; set; }
        public string DomainSchema { get; set; }
        public string DomainName { get; set; }
    }
}
