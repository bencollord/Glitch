using Glitch.Text;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Glitch.Reflection;

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

    private static readonly Regex CamelCap = new(@"(?!^)(?=[A-Z])");

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
            
            return $"{StripGenericCount(type.Name)}<{args}>";
        }

        return type.Name;
    }

    public static string Signature(this FieldInfo field)
    {
        var output = new StringBuilder();

        output.Append(PrintAccessModifier(field.AccessLevel))
              .Append(' ');

        if (field.IsStatic)
        {
            output.Append(field.IsLiteral ? "const " : "static ");
        }

        output.AppendIf(field.IsInitOnly, "readonly ")
              .Append(field.FieldType.Signature())
              .Append(' ')
              .Append(field.Name);

        return output.ToString();
    }

    public static string Signature(this PropertyInfo property)
    {
        var output = new StringBuilder();

        var method = property.GetMethod ?? property.SetMethod;

        if (method != null)
        {
            PrintMethodModifiers(output, method);
        }

        output.Append(property.PropertyType.Signature())
              .Append(' ')
              .Append(property.Name);

        var parameters = property.GetMethod?.GetParameters()
                      ?? property.SetMethod?.GetParameters()[..^1];

        if (parameters != null && parameters.Length > 0)
        {
            output.Append($"[{parameters.Select(p => p.Signature()).Join(", ")}]");
        }

        output.AppendIf(method != null, " { ")
              .AppendIf(property.GetMethod != null, "get; ")
              .AppendIf(
                  property.GetMethod != null && 
                  property.SetMethod != null && 
                  property.GetMethod.AccessLevel != property.SetMethod.AccessLevel,
                  PrintAccessModifier(property.SetMethod!.AccessLevel) + ' ')
              .AppendIf(property.SetMethod != null, "set; ")
              .Append('}')
              .AppendLine();

        return output.ToString();
    }

    public static string Signature(this ConstructorInfo constructor)
    {
        var output = new StringBuilder();

        if (constructor.IsStatic)
        {
            output.Append("static ");
        }
        else
        {
            output.Append(PrintAccessModifier(constructor.AccessLevel))
                  .Append(' ');
        }

        output.Append(StripGenericCount(constructor.DeclaringType!.Name));

        var parameters = constructor.GetParameters()
            .Select(p => p.Signature())
            .ToList();

        output.Append($"({parameters.Join(", ")})");

        return output.ToString();
    }

    public static string Signature(this MethodInfo method)
    {
        var output = new StringBuilder();

        PrintMethodModifiers(output, method);

        output.Append(method.ReturnType.Signature())
              .Append(' ')
              .Append(method.Name);

        var parameters = method.GetParameters()
            .Select(p => p.Signature())
            .ToList();

        if (parameters.Count > 0 && method.HasCustomAttribute<ExtensionAttribute>())
        {
            parameters[0] = $"this {parameters[0]}";
        }

        output.Append($"({parameters.Join(", ")})");

        return output.ToString();
    }

    public static string Signature(this ParameterInfo parameter)
    {
        if (parameter.ParameterType.IsByRef)
        {
            var unwrappedType = parameter.ParameterType.GetElementType();

            var keyword = parameter switch
            {
                { IsIn: true } => "in",
                { IsOut: true } => "out",
                _ => "ref"
            };

            return $"{keyword} {unwrappedType!.Signature()} {parameter.Name}";
        }

        return $"{parameter.ParameterType.Signature()} {parameter.Name}";
    }
    
    private static string StripGenericCount(string typeName) => typeName[..typeName.IndexOf('`')];

    private static void PrintMethodModifiers(StringBuilder output, MethodInfo method)
    {
        output.Append(PrintAccessModifier(method.AccessLevel))
              .Append(' ')
              .AppendIf(method.IsStatic, "static ")
              .AppendIf(method.IsAbstract, "abstract ")
              .AppendIf(method.IsFinal, "sealed ")
              .AppendIf(method.IsOverride, "override ")
              .AppendIf(method.IsVirtual && !method.IsAbstract && !method.IsOverride, "virtual ")
              .AppendIf(method.HasCustomAttribute<AsyncStateMachineAttribute>(), "async ");
    }

    private static string PrintAccessModifier(AccessModifier access) => CamelCap.Replace(access.ToString(), " ").ToLower();
}
