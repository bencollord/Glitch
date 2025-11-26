using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Glitch.Reflection;

public class DynamicType
{
    private const string       BackingFieldTemplate= "m_{0}";
    private const string       GetterTemplate      = "get_{0}";
    private const MethodAttributes DefaultMethodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig;
    private const MethodAttributes GetterAttributes    = DefaultMethodAttributes | MethodAttributes.SpecialName;
    private const MethodAttributes OperatorAttributes  = DefaultMethodAttributes | MethodAttributes.Static | MethodAttributes.SpecialName;
    private const MethodAttributes VirtualMethodAttributes = DefaultMethodAttributes | MethodAttributes.Virtual;

    private static readonly MethodInfo ObjectEquals  = typeof(object).GetMethod("Equals", BindingFlags.Public | BindingFlags.Instance)!;
    private static readonly MethodInfo ObjectGetHashCode = typeof(object).GetMethod("GetHashCode")!;
    private static readonly MethodInfo ObjectToString= typeof(object).GetMethod("ToString")!;

    private TypeBuilder typeBuilder;
    private List<DynamicProperty> properties = new();

    internal DynamicType(TypeBuilder type)
    {
        typeBuilder = type;
    }

    public DynamicType Property<T>(string name)
        => Property(name, typeof(T));

    public DynamicType Property(string name, Type type)
    {
        var field = typeBuilder.DefineField(
            string.Format(BackingFieldTemplate, name),
            type,
            FieldAttributes.Private
        );

        var property = typeBuilder.DefineProperty(name, PropertyAttributes.None, type, null);
        var getter = typeBuilder.DefineMethod(
            string.Format(GetterTemplate, name),
            GetterAttributes,
            type,
            null
        );

        var il = getter.GetILGenerator();

        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Ldfld, field);
        il.Emit(OpCodes.Ret);

        property.SetGetMethod(getter);

        properties.Add(new DynamicProperty(property, field));

        return this;
    }

    public Type CreateType()
    {
        GenerateConstructor();

        var typedEquals = BuildTypedEquals();

        typeBuilder.AddInterfaceImplementation(typeof(IEquatable<>).MakeGenericType(typeBuilder));

        typeBuilder.DefineMethodOverride(BuildEqualsOverride(typedEquals), ObjectEquals);
        typeBuilder.DefineMethodOverride(BuildGetHashCodeOverride(), ObjectGetHashCode);
        typeBuilder.DefineMethodOverride(BuildToStringOverride(), ObjectToString);

        GenerateEqualityOperators(typedEquals);

        return typeBuilder.CreateType();
    }

    private void GenerateConstructor()
    {
        var ctor = typeBuilder.DefineConstructor(
            DefaultMethodAttributes,
            CallingConventions.HasThis,
            properties.Select(p => p.Type).ToArray()
        );

        var il = ctor.GetILGenerator();

        // Call base object constructor
        var baseCtor = typeof(object).GetConstructor(Type.EmptyTypes)!;

        il.Emit(OpCodes.Ldarg_0);
        il.Emit(OpCodes.Call, baseCtor);

        for (int i = 0; i < properties.Count; ++i)
        {
            var field = properties[i].BackingField;

            il.Emit(OpCodes.Ldarg_0);  // Load "this" onto the stack.
            il.Emit(OpCodes.Ldarg, i + 1); // Load ctor arg onto the stack.
            il.Emit(OpCodes.Stfld, field); // Set field to value at head of the stack, IE our argument.
        }

        il.Emit(OpCodes.Ret);          // Return
    }

    private MethodInfo BuildEqualsOverride(MethodInfo typedEquals)
    {
        var method = typeBuilder.DefineMethod(
            "Equals",
            VirtualMethodAttributes,
            CallingConventions.HasThis,
            typeof(bool),
            [typeof(object)]
        );

        var il = method.GetILGenerator();

        var isEqual = il.DeclareLocal(typeof(bool));
        var endOfMethod = il.DefineLabel();

        il.Emit(OpCodes.Ldarg_1);            // Load obj onto the stack
        il.Emit(OpCodes.Isinst, typeBuilder);// Check obj is OurType
        il.Emit(OpCodes.Stloc, isEqual);     // Store result into local variable
        il.Emit(OpCodes.Ldloc, isEqual);     // Load the local result back onto the stack
        il.Emit(OpCodes.Brfalse, endOfMethod);   // If result is false, jump to the end.

        il.Emit(OpCodes.Ldarg_0);            // Stack: [this]
        il.Emit(OpCodes.Ldarg_1);            // Stack: [obj, this]
        il.Emit(OpCodes.Castclass, typeBuilder); // Cast other to Type
        il.Emit(OpCodes.Call, typedEquals);  // Stack: [true|false]
        il.Emit(OpCodes.Stloc, isEqual);     // Store result of call in local variable

        il.MarkLabel(endOfMethod);
        il.Emit(OpCodes.Ldloc, isEqual);     // Load variable onto the stack as return value
        il.Emit(OpCodes.Ret);

        return method;
    }

    private MethodInfo BuildTypedEquals()
    {
        var method = typeBuilder.DefineMethod(
            "Equals",
            VirtualMethodAttributes,
            CallingConventions.HasThis,
            typeof(bool),
            [typeBuilder]
        );

        var il = method.GetILGenerator();
        var isEqual = il.DefineLabel();
        var notEqual = il.DefineLabel();
        var endMethod = il.DefineLabel();

        // Return false if other is null
        // ===============================================================================
        il.Emit(OpCodes.Ldarg_1);               // Load other onto the eval stack
        il.Emit(OpCodes.Ldnull);                // Load null onto the stack
        il.Emit(OpCodes.Ceq);                   // Pop both args off the stack and check equality with "=="
        il.Emit(OpCodes.Brtrue, notEqual);      // Jump to NOT EQUAL if other is null

        // Return true if ReferenceEquals(other, this)
        // ===============================================================================
        il.Emit(OpCodes.Ldarg_0);               // Load "this" into the stack
        il.Emit(OpCodes.Ldarg_1);               // Load "other" onto the stack
        il.Emit(OpCodes.Ceq);                   // Equality check. 
        il.Emit(OpCodes.Brtrue, isEqual);       // If references are equal, jump to IS EQUAL

        foreach (var p in properties)
        {
            var (defaultGetter, equalsMethod, _) = EqualityComparerInfo.Get(p.Type);

            il.Emit(OpCodes.Call, defaultGetter);// Get EqualityComparer<T>.Default and place on stack
            il.Emit(OpCodes.Ldarg_0);            // Load this onto stack
            il.Emit(OpCodes.Call, p.GetMethod);  // Call getter and store result at top of stack
            il.Emit(OpCodes.Ldarg_1);            // Load other onto the stack
            il.Emit(OpCodes.Call, p.GetMethod);  // Call getter on "other" and store result at top of stack
            il.Emit(OpCodes.Callvirt, equalsMethod); // Check equality
            il.Emit(OpCodes.Brfalse, notEqual);  // If the check fails, jump to NOT EQUAL
        }

        il.MarkLabel(isEqual);
        il.Emit(OpCodes.Ldc_I4_1);              // Load 1 for "true"
        il.Emit(OpCodes.Br, endMethod);         // Jump to return

        il.MarkLabel(notEqual);
        il.Emit(OpCodes.Ldc_I4_0);              // Load 0 for "false" and fall through to return

        il.MarkLabel(endMethod);
        il.Emit(OpCodes.Ret);                   // Return!

        // Return the method. We're not done with it.
        return method;
    }

    private MethodInfo BuildGetHashCodeOverride()
    {
        var method = typeBuilder.DefineMethod(
            "GetHashCode",
            VirtualMethodAttributes,
            CallingConventions.HasThis,
            typeof(int),
            Type.EmptyTypes
        );

        var il = method.GetILGenerator();
        int seed = 676810049;
        int prime = 159485351;

        il.Emit(OpCodes.Ldc_I4, seed);             // Load the seed onto the stack to start the algorithm

        foreach (var property in properties)
        {
            var (defaultGetter, _, getHashCodeMethod) = EqualityComparerInfo.Get(property.Type);

            il.Emit(OpCodes.Ldc_I4, prime);        // Load prime. Stack state: [prime, hash]
            il.Emit(OpCodes.Mul);                  // Multiply accumulated hash by prime. Stack: [hash]
            il.Emit(OpCodes.Call, defaultGetter);  // Push EqualityComparer<T>.Defaul. Stack [comparer, hash]
            il.Emit(OpCodes.Ldarg_0);              // Push this. Stack: [this, comparer, hash]
            il.Emit(OpCodes.Call, property.GetMethod); // Call property getter. Stack: [propertyValue, comparer, hash]
            il.Emit(OpCodes.Call, getHashCodeMethod);  // Call comparer.GetHashCode() on property value. Stack: [propertyHash, hash]
            il.Emit(OpCodes.Add);                  // Add hash code to running hash. Stack: [hash]
        }

        il.Emit(OpCodes.Ret);                      // Return

        // Return the built method. We're not done with it
        return method;
    }

    private MethodInfo BuildToStringOverride()
    {
        var method = typeBuilder.DefineMethod(
            "ToString",
            VirtualMethodAttributes,
            CallingConventions.HasThis,
            typeof(string),
            Type.EmptyTypes
        );

        var stringBuilderCtor = typeof(StringBuilder).GetConstructor(Type.EmptyTypes)!;
        var appendMethod      = typeof(StringBuilder).GetMethod("Append", [typeof(string)])!;
        var stringBuilderToString = typeof(StringBuilder).GetMethod("ToString", Type.EmptyTypes)!;

        var il = method.GetILGenerator();

        il.Emit(OpCodes.Newobj, stringBuilderCtor); // Create StringBuilder to be our buffer. Stack: [buffer]
        il.Emit(OpCodes.Ldstr, typeBuilder.Name);   // Push type name. Stack: ["Type", buffer]
        il.Emit(OpCodes.Call, appendMethod);    // Call StringBuilder.Append. Append has a fluent interface, 
                                                    // so our buffer will be at the top of the stack. Stack: [buffer]

        // Stay DRY
        void EmitPrintProperty(DynamicProperty property)
        {
            il.Emit(OpCodes.Ldstr, $"{property.Name} = "); // Stack: ["Property = ", buffer]
            il.Emit(OpCodes.Call, appendMethod);       // Stack: [buffer]
            il.Emit(OpCodes.Ldarg_0);                  // Stack: [this, buffer]
            il.Emit(OpCodes.Call, property.GetMethod); // Stack: [PropertyValue, buffer]

            if (property.Type.IsValueType)
            {
                il.Emit(OpCodes.Box, property.Type);   // Box structs
            }

            il.Emit(OpCodes.Castclass, typeof(object));// Cast classes to System.Object

            il.Emit(OpCodes.Callvirt, ObjectToString); // Stringify. Stack: ["PropertyValue", buffer]
            il.Emit(OpCodes.Call, appendMethod);       // Stack: [buffer]
        }

        // Jump out if there are no properties to print
        if (properties.Count == 0)
        {
            // We're coding in IL and doing jumps all over the place anyway, why not?
            goto END_OF_METHOD;
        }

        il.Emit(OpCodes.Ldstr, " { ");
        il.Emit(OpCodes.Call, appendMethod);

        // Print the first property with no separator
        EmitPrintProperty(properties[0]);

        for (int i = 1; i < properties.Count; ++i)
        {
            il.Emit(OpCodes.Ldstr, ", ");
            il.Emit(OpCodes.Call, appendMethod);

            EmitPrintProperty(properties[i]);
        }

        il.Emit(OpCodes.Ldstr, " }");
        il.Emit(OpCodes.Call, appendMethod);

    END_OF_METHOD:

        il.Emit(OpCodes.Callvirt, stringBuilderToString);
        il.Emit(OpCodes.Ret);

        return method;
    }

    private void GenerateEqualityOperators(MethodInfo typedEquals)
    {
        var equality = typeBuilder.DefineMethod(
            "op_Equality",
            OperatorAttributes,
            CallingConventions.Standard,
            typeof(bool),
            [typeBuilder, typeBuilder]
        );

        var il = equality.GetILGenerator();

        il.Emit(OpCodes.Ldarg_0);           // Load lhs
        il.Emit(OpCodes.Ldarg_1);           // Load rhs
        il.Emit(OpCodes.Callvirt, typedEquals); // Call Equals
        il.Emit(OpCodes.Ret);

        var inequality = typeBuilder.DefineMethod(
            "op_Inequality",
            OperatorAttributes,
            CallingConventions.Standard,
            typeof(bool),
            [typeBuilder, typeBuilder]
        );

        il = inequality.GetILGenerator();

        il.Emit(OpCodes.Ldarg_0);    // Load lhs
        il.Emit(OpCodes.Ldarg_1);    // Load rhs
        il.Emit(OpCodes.Call, equality); // Call Equality operator
        il.Emit(OpCodes.Neg);
        il.Emit(OpCodes.Ret);
    }

    private record EqualityComparerInfo(MethodInfo DefaultGetter, MethodInfo EqualsMethod, MethodInfo GetHashCodeMethod)
    {
        internal static EqualityComparerInfo Get(Type type)
        {
            var comparerType = typeof(EqualityComparer<>).MakeGenericType(type);
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            return new EqualityComparerInfo
            (
                DefaultGetter: comparerType.GetProperty("Default")!.GetMethod!,
                EqualsMethod: comparerType.GetMethod("Equals", bindingFlags, [type, type])!,
                GetHashCodeMethod: comparerType.GetMethod("GetHashCode", bindingFlags, [type])!
            );
        }
    }

    private class DynamicProperty : IEquatable<DynamicProperty>
    {
        private PropertyBuilder property;
        private FieldBuilder backingField;

        internal DynamicProperty(PropertyBuilder property, FieldBuilder backingField)
        {
            this.property = property;
            this.backingField = backingField;
        }

        internal string Name => property.Name;

        internal Type Type => property.PropertyType;

        internal FieldInfo BackingField => backingField;

        internal MethodInfo GetMethod => property.GetMethod!;

        public bool Equals(DynamicProperty? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(other, this)) return true;

            return Name.Equals(other.Name) && Type.Equals(other.Type);
        }

        public override int GetHashCode() 
            => HashCode.Combine(Name.GetHashCode(), Type.GetHashCode());

        public override bool Equals(object? obj) 
            => Equals(obj as DynamicProperty);

        public static bool operator ==(DynamicProperty x, DynamicProperty y)
            => x is null ? y is null : x.Equals(y);

        public static bool operator !=(DynamicProperty x, DynamicProperty y)
            => !(x == y);
    }
}
