using Glitch.CodeAnalysis.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Glitch.CodeAnalysis;

public static class SyntaxNodeExtensions
{
    extension(SyntaxNode node)
    {
        public IEnumerable<TNode> DescendantNodes<TNode>()
            where TNode : SyntaxNode
            => node.DescendantNodes().OfType<TNode>();

        public IEnumerable<SyntaxToken> DescendantTokens(SyntaxKind kind) =>
           node.DescendantTokens().Where(t => t.IsKind(kind));
    }

    extension<TNode>(TNode node) where TNode : SyntaxNode
    {
        public TNode NormalizeWhitespace() =>
            (TNode)new WhitespaceNormalizer().Visit(node);
    }

    extension<TNode>(IEnumerable<TNode> nodes) where TNode : SyntaxNode
    {
        public SyntaxList<TNode> ToSyntaxList() =>
            CSharpSyntax.List(nodes);
    }
}
