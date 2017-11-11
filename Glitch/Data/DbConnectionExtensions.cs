using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public static class DbConnectionExtensions
    {
        public static DataTable Query(this DbConnection connection, string sql)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            using (var command = connection.CreateCommand(sql))
            {
                return command.ExecuteTable();
            }
        }

        public static IEnumerable<T> Query<T>(this DbConnection connection, string sql)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            using (var command = connection.CreateCommand(sql))
            {
                return command.ExecuteEntities<T>();
            }
        }

        public static IEnumerable Query(this DbConnection connection, Type type, string sql)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            using (var command = connection.CreateCommand(sql))
            {
                return command.ExecuteEntities(type);
            }
        }

        public static DbCommand CreateCommand(this DbConnection connection, string sql)
        {
            var command = connection.CreateCommand();

            command.CommandText = sql;

            return command;
        }

        public static IEnumerable<ColumnDefinition> GetColumns(this DbConnection connection, Func<ColumnDefinition, bool> predicate)
            => connection.GetColumns().Where(predicate);

        public static IEnumerable<ColumnDefinition> GetColumns(this DbConnection connection)
        {
            if (!(connection is SqlConnection))
            {
                throw new NotSupportedException($"Cannot get schema for {connection.GetType().Name}");
            }

            return GetColumns(connection as SqlConnection);
        }

        private static IEnumerable<ColumnDefinition> GetColumns(SqlConnection connection)
            => connection.Query("SELECT * FROM INFORMATION_SCHEMA.COLUMNS")
                         .AsEnumerable()
                         .Select(r => new ColumnDefinition(r));
    }
}
