using Glitch.Functional.Extensions;
using Glitch.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis;

/// <summary>
/// Wrapper around <see cref="SyntaxFactory"/> to allow adding convenience
/// methods while using a unified interface.
/// </summary>
public static partial class CSharpSyntax
{
    // TODO Move the rest of the convenience methods that aren't simple passthroughs to this file instead of .Wrappers
    public static MemberAccessExpressionSyntax MemberAccess(ExpressionSyntax expression, SimpleNameSyntax member)
        => MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, member);
    
    public static RecordDeclarationSyntax RecordDeclaration(SyntaxToken identifier)
        => RecordDeclaration(Token(SyntaxKind.RecordKeyword), identifier);

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxToken separator, params IEnumerable<TNode> nodes)
        where TNode : SyntaxNode
        => nodes.Match(just: SingletonSeparatedList,
                       many: n => SeparatedList(n, Enumerable.Repeat(separator, n.Count() - 1)),
                       none: SeparatedList<TNode>);
}
