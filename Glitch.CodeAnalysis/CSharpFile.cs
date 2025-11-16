using Glitch.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

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

        private State state;

        public CSharpFile(FilePath path)
            : this(new UnloadedFileState(path)) { }

        public CSharpFile(FileInfo file)
            : this(new UnloadedFileState(file)) { }

        private CSharpFile(State state)
        {
            this.state = state;
        }

        public FilePath Path => state.Path;

        public FileInfo File => new FileInfo(Path);

        public bool HasUnsavedChanges => state is UnsavedFileState;

        public static CSharpFile Load(FilePath path) => new(path);

        public static CSharpFile Load(FileInfo file) => new(file);

        public static CSharpFile Create(FilePath path, SyntaxNode root) => new(new UnsavedFileState(path, root));

        public static CSharpFile Create(FileInfo file, SyntaxNode root) => Create(file.FullName, root);

        public CompilationUnitSyntax GetRoot()
        {
            SetState(state.LoadTree(out var tree));

            return tree.GetCompilationUnitRoot();
        }

        public CSharpFile CopyTo(FilePath path) => WithState(state.CopyTo(path));

        public CSharpFile Rewrite(CSharpSyntaxRewriter visitor) => Rewrite(visitor.Visit);

        public CSharpFile Rewrite(Func<SyntaxNode, SyntaxNode> updater) => SetState(state.Rewrite(updater));

        public CSharpFile ReplaceRoot(SyntaxNode newRoot) => SetState(state.WithRoot(newRoot));

        public CSharpFile Revert() => SetState(state.Revert());

        public Unit SaveChanges() => SetState(state.SaveChanges()).Ignore();

        public override string ToString() => state.ToString();

        private CSharpFile SetState(State state)
        {
            this.state = state;
            return this;
        }

        private CSharpFile WithState(State state) => new(state);

        private abstract class State
        {
            public abstract FilePath Path { get; }

            public abstract State CopyTo(FilePath path);

            public abstract State LoadTree(out SyntaxTree tree);

            public State Rewrite(CSharpSyntaxRewriter visitor)
                => Rewrite(visitor.Visit);

            public virtual State WithRoot(SyntaxNode newRoot)
                => Rewrite(_ => newRoot);

            public abstract State Rewrite(Func<SyntaxNode, SyntaxNode> updater);

            public abstract State Revert();

            public abstract State SaveChanges();

            public abstract override string ToString();
        }

        private class UnloadedFileState : State
        {
            private FilePath path;

            public UnloadedFileState(FilePath path)
            {
                this.path = path;
            }

            public override FilePath Path => path;

            public override State CopyTo(FilePath path) => new UnloadedFileState(path);

            public override State LoadTree(out SyntaxTree tree)
            {
                var next = Load();

                tree = next.Tree;

                return next;
            }

            public override State Revert()
            {
                return this;
            }

            public override State Rewrite(Func<SyntaxNode, SyntaxNode> updater)
            {
                return Load().Rewrite(updater);
            }

            public override State SaveChanges()
            {
                return this;
            }

            public override string ToString() => $"Unloaded: {Path}";

            private LoadedState Load()
            {
                using var stream = new FileStream(path, FileMode.Open);

                var text = SourceText.From(stream);
                var tree = CSharpSyntaxTree.ParseText(text);

                return new LoadedState(path, tree);
            }
        }

        private class LoadedState : State
        {
            public LoadedState(FilePath path, SyntaxTree tree)
            {
                Path = path;
                Tree = tree;
            }

            public override FilePath Path { get; }

            public SyntaxTree Tree { get; }

            public override State CopyTo(FilePath path) => new LoadedState(path, Tree);

            public override State LoadTree(out SyntaxTree tree)
            {
                tree = Tree;
                return this;
            }

            public override State Revert()
            {
                return this;
            }

            public override State Rewrite(Func<SyntaxNode, SyntaxNode> updater)
            {
                var root = Tree.GetRoot();
                var updated = updater(root);

                if (root == updated)
                {
                    // No change
                    return this;
                }

                return new ModifiedState(Path, root, updater(root));
            }

            public override string ToString() => $"Loaded: {Path}";

            public override State SaveChanges()
            {
                return this;
            }
        }

        private class UnsavedFileState : State
        {
            public UnsavedFileState(FilePath path, SyntaxNode updated)
            {
                Path = path;
                UnsavedRoot = updated;
            }

            public override FilePath Path { get; }

            public SyntaxNode UnsavedRoot { get; }

            public override State CopyTo(FilePath path) => new UnsavedFileState(path, UnsavedRoot);

            public override State LoadTree(out SyntaxTree tree)
            {
                tree = CreateTree(UnsavedRoot);
                return this;
            }

            public override State Revert() => this;

            public override State Rewrite(Func<SyntaxNode, SyntaxNode> updater)
            {
                return new UnsavedFileState(Path, updater(UnsavedRoot));
            }

            public override State SaveChanges()
            {
                using var stream = new StreamWriter(Path);

                UnsavedRoot.WriteTo(stream);

                return new LoadedState(Path, CreateTree(UnsavedRoot));
            }

            public override string ToString() => $"Unsaved: {Path}";

            protected SyntaxTree CreateTree(SyntaxNode root) => CSharpSyntaxTree.Create((CSharpSyntaxNode)root, path: Path);
        }

        private class ModifiedState : UnsavedFileState
        {
            public ModifiedState(FilePath path, SyntaxNode original, SyntaxNode updated)
                : base(path, updated)
            {
                OriginalRoot = original;
            }

            public SyntaxNode OriginalRoot { get; }

            public override State Revert()
            {
                return new LoadedState(Path, CreateTree(OriginalRoot));
            }

            public override string ToString() => $"Modified: {Path}";

            public override State Rewrite(Func<SyntaxNode, SyntaxNode> updater)
            {
                return new ModifiedState(Path, OriginalRoot, updater(UnsavedRoot));
            }
        }
    }
}
