using Glitch.Functional.Results;
using Glitch.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Glitch.CodeAnalysis
{
    public class CSharpFile
    {
        private static readonly Prism<CompilationUnitSyntax, NamespaceDeclarationSyntax> NamespaceInRootPrism = Prism<CompilationUnitSyntax, NamespaceDeclarationSyntax>.New
        (
            r => r.Members.TrySingle(x => x is NamespaceDeclarationSyntax)
                          .Cast<NamespaceDeclarationSyntax>()
                          .OkayOrNone(),
            (r, o, t) => r.WithMembers(r.Members.Replace(o, t))
        );

        private static readonly Prism<NamespaceDeclarationSyntax, TypeDeclarationSyntax> TypeInNamespacePrism = Prism<NamespaceDeclarationSyntax, TypeDeclarationSyntax>.New
        (
            r => r.Members.TrySingle(x => x is TypeDeclarationSyntax)
                          .Cast<TypeDeclarationSyntax>()
                          .OkayOrNone(),
            (r, o, t) => r.WithMembers(r.Members.Replace(o, t))
        );

        public static readonly Prism<SyntaxNode, TypeDeclarationSyntax> SingleTypePrism =
            Prism<SyntaxNode, CompilationUnitSyntax>.New(n => Maybe(n as CompilationUnitSyntax), (s, n) => n)
                .Compose(NamespaceInRootPrism)
                .Compose(TypeInNamespacePrism);

        private FileInfo file;
        private Lazy<SyntaxTree> tree;
        private Option<SyntaxTree> updated = default;

        public CSharpFile(string path)
            : this(new FileInfo(path)) { }

        public CSharpFile(FileInfo file)
        {
            this.file = file;
            tree = new(() => CSharpSyntaxTree.ParseText(file.ReadAllText()));
        }

        public FileInfo File => file;

        public static CSharpFile Load(string path) => new(path);

        public static CSharpFile Load(FileInfo file) => new(file);

        public SyntaxTree GetTree() => updated | tree.Value;

        public CompilationUnitSyntax GetRoot() => GetTree().GetCompilationUnitRoot();

        public CSharpFile Rewrite(CSharpSyntaxRewriter visitor)
            => ReplaceRoot(visitor.Visit);

        public CSharpFile ReplaceRoot(Func<SyntaxNode, SyntaxNode> updater)
        {
            return ReplaceRoot(updater(GetRoot()));
        }

        public CSharpFile ReplaceRoot(SyntaxNode newRoot)
        {
            updated = tree.Value.WithRootAndOptions(newRoot, tree.Value.Options);
            return this;
        }

        public CSharpFile ReplaceTree(CSharpSyntaxTree newTree)
        {
            updated = newTree;
            return this;
        }

        public CSharpFile Revert()
        {
            updated = None;
            return this;
        }

        public void SaveChanges()
        {
            using var stream = file.CreateText();
            GetRoot().WriteTo(stream);
        }
    }
}
