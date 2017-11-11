using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public class EntityMap<T>
    {
        private const string BackingFieldFormat = "<{0}>k__BackingField";
        private const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;


        private Dictionary<string, MemberBinding> bindings;

        public EntityMap()
        {
            this.bindings = this.EntityType
                    .GetProperties(DefaultBindingFlags)
                    .Cast<MemberInfo>()
                    .Concat(this.EntityType.GetFields(DefaultBindingFlags).Cast<MemberInfo>())
                    .Select(m => MemberBinding.Create(m))
                    .ToDictionary(m => m.MemberName);
        }

        private Type EntityType => typeof(T);


        public T Translate(IDataRecord record)
        {
            Guard.NotNull(record, nameof(record));
            object entity = FormatterServices.GetUninitializedObject(this.EntityType);

            record.GetFieldNames()
                  .Select(n => new { Name = n, Value = record[n] })
                  .Join(this.bindings.Values, n => n.Name, b => b.MemberName, (n, b) => new { Binding = b, Value = n.Value })
                  .ForEach(pair => pair.Binding.SetValue(entity, pair.Value));

            return (T)entity;
        }

        public T Translate(DataRow row)
        {
            Guard.NotNull(row, nameof(row));
            object entity = FormatterServices.GetUninitializedObject(this.EntityType);

            var matches = from col in row.Table.Columns.Cast<DataColumn>()
                          join bnd in this.bindings.Values
                            on col.ColumnName equals bnd.MemberName
                          select new
                          {
                              Binding = bnd,
                              Value = row[col.ColumnName]
                          };

            matches.ForEach(m => m.Binding.SetValue(entity, m.Value));

            return (T)entity;
        }

        public IEnumerable<T> Translate(IEnumerable<DataRow> rows)
        {
            Guard.NotNull(rows, nameof(rows));

            return rows.Select(r => Translate(r));
        }

        public IEnumerable<T> Translate(DataTable table)
        {
            return Translate(table.AsEnumerable());
        }

        private abstract class MemberBinding
        {
            private MemberInfo member;
            private string columnName;

            protected MemberBinding(MemberInfo member)
                : this(member, null) { }

            protected MemberBinding(MemberInfo member, string columnName)
            {
                Guard.NotNull(member, nameof(member));

                this.member = member;
                this.columnName = (!String.IsNullOrWhiteSpace(columnName)) ? columnName : null;
            }

            public static MemberBinding Create(MemberInfo member) => Create(member, null);
            public static MemberBinding Create(MemberInfo member, string columnName)
            {
                Guard.NotNull(member, nameof(member));

                if (member is PropertyInfo)
                {
                    return new PropertyBinding((PropertyInfo)member, columnName);
                }
                if (member is FieldInfo)
                {
                    return new FieldBinding((FieldInfo)member, columnName);
                }

                throw new ArgumentException($"Cannot create binding from member type {member.GetType().Name}.");
            }

            public virtual string MemberName => this.member.Name;

            public string ColumnName => this.columnName ?? this.MemberName;

            public abstract Type MemberType { get; }

            public void SetValue(object entity, object value)
            {
                Guard.NotNull(entity, nameof(entity));

                if (value.Equals(DBNull.Value))
                {
                    SetMemberValue(entity, null);
                    return;
                }

                SetMemberValue(entity, value);
            }

            protected abstract void SetMemberValue(object entity, object value);
        }

        private class FieldBinding : MemberBinding
        {
            private FieldInfo field;

            public FieldBinding(FieldInfo field)
                : this(field, null) { }

            public FieldBinding(FieldInfo field, string columnName)
                : base(field, columnName)
            {
                this.field = field;
            }

            public override Type MemberType => this.field.FieldType;

            protected override void SetMemberValue(object entity, object value) => this.field.SetValue(entity, value);
        }

        private class PropertyBinding : MemberBinding
        {
            private PropertyInfo property;

            public PropertyBinding(PropertyInfo property)
                : this(property, null) { }

            public PropertyBinding(PropertyInfo property, string columnName)
                : base(property, columnName)
            {
                this.property = property;
            }

            public override Type MemberType => this.property.PropertyType;

            protected override void SetMemberValue(object entity, object value)
            {
                if (!this.property.CanWrite)
                {
                    FieldInfo backingField = this.MemberType.GetField(String.Format(BackingFieldFormat, this.MemberName), DefaultBindingFlags);

                    if (backingField == null)
                    {
                        throw new InvalidOperationException($"Cannot set property {this.MemberName} as it is read only and no backing field could be found.");
                    }

                    backingField.SetValue(entity, value);
                }
                else
                {
                    this.property.SetValue(entity, value);
                }
            }
        }
    }
}
