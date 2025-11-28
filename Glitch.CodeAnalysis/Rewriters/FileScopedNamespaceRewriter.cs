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

        var newNode = FileScopedNamespaceDeclaration(name)
            .WithNamespaceKeyword(keyword)
            .WithSemicolonToken(semicolon)
            .WithMembers(List(node.Members))
            .WithAttributeLists(node.AttributeLists)
            .WithModifiers(node.Modifiers)
            .WithExterns(node.Externs)
            .WithUsings(node.Usings)

            // For some stupid reason, the final two close brace tokens in 
            // classes with extension blocks are considered trailing trivia of the
            // the namespace declaration. They're attached to the syntax nodes just fine,
            // but calling ToString or WriteTo without doing this will render the code
            // with the braces missing.
            .WithTrailingTrivia(node.GetTrailingTrivia());

        return base.VisitFileScopedNamespaceDeclaration(newNode);
    }
}