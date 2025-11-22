namespace Glitch.Reflection
{
    public static class SignaturePrintingExtensions
    {
        private static Dictionary<Type, string> SpecialTypeNames = new()
        {
            [typeof(void)]    = "void",
            [typeof(object)]  = "object",
            [typeof(bool)]    = "bool",
            [typeof(char)]    = "char",
            [typeof(string)]  = "string",
            [typeof(byte)]    = "byte",
            [typeof(sbyte)]   = "sbyte",
            [typeof(short)]   = "short",
            [typeof(ushort)]  = "ushort",
            [typeof(int)]     = "int",
            [typeof(uint)]    = "uint",
            [typeof(long)]    = "long",
            [typeof(ulong)]   = "ulong",
            [typeof(nint)]    = "nint",
            [typeof(nuint)]   = "nuint",
            [typeof(float)]   = "float",
            [typeof(double)]  = "double",
            [typeof(decimal)] = "decimal",
        };

        public static string Signature(this Type type)
        {
            if (SpecialTypeNames.TryGetValue(type, out var name))
            {
                return name; 
            }
            
            if (Nullable.GetUnderlyingType(type) is Type t)
            {
                return t.Signature() + "?";
            }

            if (type.IsArray)
            {
                return type.GetElementType() + "[]";
            }

            if (type.IsPointer)
            {
                return type.GetElementType()!.Signature() + "*";
            }

            if (type.IsGenericType)
            {
                var args = type.GetGenericArguments()
                               .Select(a => a.Signature())
                               .Join(", ");
                var endName = type.Name.IndexOf('`');

                return $"{type.Name.Substring(0, endName)}<{args}>";
            }

            return type.Name;
        }
    }
}
