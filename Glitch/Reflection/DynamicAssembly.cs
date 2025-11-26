using System.Reflection;
using System.Reflection.Emit;

namespace Glitch.Reflection;

public class DynamicAssembly
{
    private AssemblyBuilder assembly;
    private ModuleBuilder module;

    private DynamicAssembly(AssemblyBuilder assembly, ModuleBuilder module)
    {
        this.assembly = assembly;
        this.module = module;
    }

    public static DynamicAssembly Create(string name)
    {
        var assemblyName = new AssemblyName { Name = name };
        var assembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
        var module = assembly.DefineDynamicModule(assemblyName.Name);

        return new DynamicAssembly(assembly, module);
    }

    public DynamicType DefineType(string name)
    {
        return new DynamicType(module.DefineType(name));
    }
}
