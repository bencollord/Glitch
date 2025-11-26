using Glitch.CodeAnalysis.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Glitch.CodeAnalysis;

public static class SyntaxNodeExtensions
{
    public static IEnumerable<TNode> DescendantNodes<TNode>(this SyntaxNode node)
        where TNode : SyntaxNode
        => node.DescendantNodes().OfType<TNode>();

    public static IEnumerable<SyntaxToken> DescendantTokens(this SyntaxNode node, SyntaxKind kind)
        => node.DescendantTokens().Where(t => t.IsKind(kind));

    public static TNode NormalizeWhitespace<TNode>(this TNode node) 
        where TNode : SyntaxNode
        => (TNode)new WhitespaceNormalizer().Visit(node);

    public static SyntaxList<TNode> ToSyntaxList<TNode>(this IEnumerable<TNode> nodes)
        where TNode : SyntaxNode
        => CSharpSyntax.List(nodes);
}
