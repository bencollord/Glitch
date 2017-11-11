using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Glitch
{
    public class PropertyDefinition
    {
        private string name;
        private Type type;

        public PropertyDefinition(string name, Type type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name => this.name;

        public Type Type => this.type;

        public static PropertyDefinition Define(PropertyInfo property) => new PropertyDefinition(property.Name, property.PropertyType);

        public static PropertyDefinition Define(FieldInfo field) => new PropertyDefinition(field.Name, field.FieldType);

        public bool Equals(PropertyDefinition other)
        {
            if (ReferenceEquals(other, null)) return false;

            return this.name.Equals(other.name, StringComparison.Ordinal) && this.type.Equals(other.type);
        }

        public override bool Equals(object obj) => Equals(obj as PropertyDefinition);

        public override int GetHashCode()
        {
            int seed = 19;
            int prime = 31;
            int hash = seed;

            unchecked
            {
                hash = hash * prime + this.name.GetHashCode();
                hash = hash * prime + this.type.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            string typeName = this.type.IsNullable() ? this.type.GetGenericArguments().Single().Name + "?" : this.type.Name;

            return $"public {typeName} {this.name} {{ get; set; }}";
        }
    }
}
