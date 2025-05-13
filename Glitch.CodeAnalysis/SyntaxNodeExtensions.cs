using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Glitch.CodeAnalysis
{
    public static class SyntaxNodeExtensions
    {
        public static IEnumerable<TNode> DescendantNodes<TNode>(this SyntaxNode node)
            where TNode : SyntaxNode
            => node.DescendantNodes().OfType<TNode>();

        public static IEnumerable<SyntaxToken> DescendantTokens(this SyntaxNode node, SyntaxKind kind)
            => node.DescendantTokens().Where(t => t.IsKind(kind));
    }
}
