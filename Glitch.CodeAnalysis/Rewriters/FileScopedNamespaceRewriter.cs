using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace Glitch.CodeAnalysis.Rewriters;

using static CSharpSyntax;

public class FileScopedNamespaceRewriter : CSharpSyntaxRewriter
{
    public override SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
    {
        // TODO Factor this out into generic indent/unindent rewriter
        if (trivia.IsKind(SyntaxKind.WhitespaceTrivia) && trivia.Span.Length >= 4)
        {
            var newIndent = new string(' ', trivia.Span.Length - 4);

            return Whitespace(newIndent);
        }

        return base.VisitTrivia(trivia);
    }

    public override SyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        if (node.DescendantNodes<BaseNamespaceDeclarationSyntax>().Count() > 1)
        {
            Debug.WriteLine("File has more than one namespace declaration. Skipping.");
            return node;
        }

        return base.VisitCompilationUnit(node);
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        var name = node.Name.WithoutTrivia();
        var keyword = node.NamespaceKeyword.WithTrailingTrivia(Space);
        var semicolon = Token(SyntaxKind.SemicolonToken)
            .WithTrailingTrivia(CarriageReturnLineFeed, CarriageReturnLineFeed);

        var members = node.Members.Select(Visit).Cast<MemberDeclarationSyntax>();

        return FileScopedNamespaceDeclaration(name)
            .WithNamespaceKeyword(keyword)
            .WithSemicolonToken(semicolon)
            .WithMembers(List(members))
            .WithAttributeLists(node.AttributeLists)
            .WithModifiers(node.Modifiers)
            .WithExterns(node.Externs)
            .WithUsings(node.Usings);
    }
}
