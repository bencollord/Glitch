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
public static partial class SyntaxFactoryExtensions
{
    extension(SyntaxFactory)
    {
        public static ParameterSyntax Parameter(TypeSyntax type, string identifier)
            => Parameter(type, SyntaxFactory.Identifier(identifier));

        public static ParameterSyntax Parameter(TypeSyntax type, SyntaxToken identifier)
            => SyntaxFactory.Parameter(identifier).WithType(type);

        public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, string identifier)
            => VariableDeclaration(type, SyntaxFactory.Identifier(identifier));

        public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SyntaxToken identifier)
            => VariableDeclaration(type, SyntaxFactory.VariableDeclarator(identifier));

        public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, VariableDeclaratorSyntax variable)
            => SyntaxFactory.VariableDeclaration(type, SyntaxFactory.SingletonSeparatedList(variable));

        public static MemberAccessExpressionSyntax MemberAccess(ExpressionSyntax expression, SimpleNameSyntax member)
            => SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expression, member);

        public static RecordDeclarationSyntax RecordDeclaration(SyntaxToken identifier)
            => SyntaxFactory.RecordDeclaration(SyntaxFactory.Token(SyntaxKind.RecordKeyword), identifier);

        public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxToken separator, params IEnumerable<TNode> nodes)
            where TNode : SyntaxNode
            => nodes.Match(just: SyntaxFactory.SingletonSeparatedList,
                           many: n => SyntaxFactory.SeparatedList(n, Enumerable.Repeat(separator, n.Count() - 1)),
                           none: SyntaxFactory.SeparatedList<TNode>);
    }
}
