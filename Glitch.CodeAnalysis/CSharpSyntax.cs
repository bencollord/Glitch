using Glitch.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis
{
    /// <summary>
    /// Wrapper around <see cref="SyntaxFactory"/> to allow adding convenience
    /// methods while using a unified interface.
    /// </summary>
    public static partial class CSharpSyntax
    {
        public static SyntaxTree Parse(string code) => CSharpSyntaxTree.ParseText(code);

        public static SyntaxTree Load(string path) => Load(new FileInfo(path));

        public static SyntaxTree Load(FileInfo file) => Parse(file.ReadAllText()).WithFilePath(file.FullName);

        // TODO Move the rest of the convenience methods that aren't simple passthroughs to this file instead of .Wrappers
        public static RecordDeclarationSyntax RecordDeclaration(SyntaxToken identifier)
            => RecordDeclaration(Token(SyntaxKind.RecordKeyword), identifier);

        public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode> nodes, SyntaxToken separator)
            where TNode : SyntaxNode
            => SeparatedList(nodes, Enumerable.Repeat(separator, nodes.Count() - 1));
    }
}
