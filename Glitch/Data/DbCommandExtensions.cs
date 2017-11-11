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

        public static IEnumerable<dynamic> ExecuteDynamic(this DbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return CreateDynamicObject(reader);
                }
            }
        }

        public static IEnumerable<T> ExecuteEntities<T>(this DbCommand command)
            => command.ExecuteEntities(typeof(T)).Cast<T>();

        public static IEnumerable ExecuteEntities(this DbCommand command, Type type)
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return CreateEntity(reader, type);
                }
            }
        }

        private static T CreateEntity<T>(IDataRecord record) => (T)CreateEntity(record, typeof(T));

        private static object CreateEntity(IDataRecord record, Type entityType)
        {
            object entity = FormatterServices.GetUninitializedObject(entityType);

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var properties = entityType.GetProperties(bindingFlags).ToDictionary(p => p.Name);

            PropertyInfo property;
            bool success = false;

            for (int i = 0; i < record.FieldCount; ++i)
            {
                success = properties.TryGetValue(record.GetName(i), out property);

                if (success)
                {
                    property.SetValue(entity, record.GetValue(i));
                }
            }

            return entity;
        }

        private static dynamic CreateDynamicObject(IDataRecord record)
        {
            var obj = new System.Dynamic.ExpandoObject();
            var dict = obj as IDictionary<string, object>;

            for (int i = 0; i < record.FieldCount; ++i)
            {
                dict.Add(record.GetName(i), record.GetValue(i));
            }

            return obj;
        }
    }
}
