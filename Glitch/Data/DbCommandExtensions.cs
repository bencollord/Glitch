using Glitch.Data.Mapping;
using System.Data;
using System.Data.Common;

namespace Glitch.Data
{
    public static class DbCommandExtensions
    {
        public static DbParameter CreateParameter(this DbCommand command, object value)
            => value as DbParameter ?? command.CreateParameter($"@p{command.Parameters.Count}", value);

        public static DbParameter CreateParameter(this DbCommand command, string name, object value)
        {
            DbParameter parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        public static DbParameter CreateParameter(this DbCommand command, string name, DbType dbType)
        {
            DbParameter parameter = command.CreateParameter(name);
            parameter.DbType = dbType;
            return parameter;
        }

        public static DbParameter CreateParameter(this DbCommand command, string name, DbType dbType, object value)
        {
            DbParameter parameter = command.CreateParameter(name, dbType);
            parameter.Value = value;
            return parameter;
        }

        public static DataTable ExecuteTable(this DbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var table = new DataTable();
                table.Load(reader);
                return table;
            }
        }

        public static ResultStream<dynamic> ExecuteDynamic(this DbCommand command)
        {
            return new ResultStream<dynamic>(command.ExecuteReader(), new ExpandoObjectMap());
        }

        public static ResultStream<T> Execute<T>(this DbCommand command)
            => command.Execute(typeof(T)).Cast<T>();

        public static ResultStream Execute(this DbCommand command, Type type)
        {
            ITypeMap map = ScalarTypeMap.IsScalarType(type) ? new ScalarTypeMap(type) : new AdHocTypeMap(type);

            return new ResultStream(command.ExecuteReader(), map);
        }
    }
}
