using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Reflection;

namespace Glitch.CodeAnalysis
{
    public class CSharpCompilationBuilder
    {
        private string name;
        private HashSet<SyntaxTree> trees = [];
        private HashSet<MetadataReference> references = [];
        private CSharpCompilationOptions options;

        public CSharpCompilationBuilder(string name, OutputKind outputKind = OutputKind.DynamicallyLinkedLibrary)
        {
            this.name = name;
            options = new CSharpCompilationOptions(outputKind);
        }

        public CSharpCompilationBuilder(CSharpCompilation compilation)
        {
            // TODO Cache the whole instance/build result
            name = compilation.AssemblyName ?? string.Empty;
            trees = compilation.SyntaxTrees.ToHashSet();
            references = compilation.References.ToHashSet();
            options = compilation.Options;
        }

        public CSharpCompilationBuilder AddSyntaxTree(string sourceText)
            => AddSyntaxTree(CSharpSyntaxTree.ParseText(sourceText));

        public CSharpCompilationBuilder AddSyntaxTree(SourceText sourceText)
            => AddSyntaxTree(CSharpSyntaxTree.ParseText(sourceText));

        public CSharpCompilationBuilder AddSyntaxTree(SyntaxTree tree)
        {
            trees.Add(tree);
            return this;
        }

        public CSharpCompilationBuilder AddSyntaxTrees(params SyntaxTree[] trees)
            => AddSyntaxTrees(trees.AsEnumerable());

        public CSharpCompilationBuilder AddSyntaxTrees(IEnumerable<SyntaxTree> trees)
        {
            this.trees.UnionWith(trees);
            return this;
        }

        public CSharpCompilationBuilder AddReferences(Assembly assembly)
        {
            var references = ResolveReferencedAssembliesAndSelf(assembly)
                .Select(e => e.Location)
                .Distinct()
                .Select(e => MetadataReference.CreateFromFile(e));

            return AddReferences(references);

            IEnumerable<Assembly> ResolveReferencedAssembliesAndSelf(Assembly asm)
            {
                var references = asm.GetReferencedAssemblies()
                    .Select(Assembly.Load)
                    .SelectMany(ResolveReferencedAssembliesAndSelf);

                yield return asm;

                foreach (var reference in references)
                {
                    yield return reference;
                }
            }
        }

        public CSharpCompilationBuilder AddReference(MetadataReference reference)
        {
            references.Add(reference);
            return this;
        }

        public CSharpCompilationBuilder AddReferences(params MetadataReference[] references)
            => AddReferences(references.AsEnumerable());

        public CSharpCompilationBuilder AddReferences(IEnumerable<MetadataReference> references)
        {
            this.references.UnionWith(references);
            return this;
        }

        public CSharpCompilationBuilder WithOutputKind(OutputKind outputKind)
            => WithOptions(options.WithOutputKind(outputKind));

        public CSharpCompilationBuilder WithOptions(CSharpCompilationOptions options)
        {
            this.options = options;
            return this;
        }

        public CSharpCompilation Build() => CSharpCompilation.Create(name, trees, references, options);
    }

}
