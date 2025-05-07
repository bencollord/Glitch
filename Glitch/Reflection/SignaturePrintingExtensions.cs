using Glitch.Functional;

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
            var builtIn = SpecialTypeNames.TryGetValue(type);
            
            var nullable = from t in Maybe(Nullable.GetUnderlyingType(type))
                           let s = t.Signature() + "?"
                           select s;

            var array = from t in Some(type)
                        where t.IsArray
                        from e in Maybe(t.GetElementType())
                        select e.Signature() + "[]";

            var pointer = from t in Some(type)
                          where t.IsPointer
                          from e in Maybe(t.GetElementType())
                          select e.Signature() + "*";

            var generic = from t in Some(type)
                          where t.IsGenericType
                          let args = t.GetGenericArguments()
                                      .Select(a => a.Signature())
                                      .Join(", ")
                          let endName = t.Name.IndexOf('`')
                          select $"{t.Name.Substring(0, endName)}<{args}>";

            return builtIn | nullable | array | pointer | generic | type.Name;
        }
    }
}
