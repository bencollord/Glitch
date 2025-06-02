using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace Glitch.CodeAnalysis
{
    /// <summary>
    /// Wrapper around <see cref="SyntaxFactory"/> to allow adding convenience
    /// methods while using a unified interface.
    /// </summary>
    public static partial class CSharpSyntax
    {
        // TODO Move the rest of the convenience methods that aren't simple passthroughs to this file instead of .Wrappers
        public static RecordDeclarationSyntax RecordDeclaration(SyntaxToken identifier)
            => RecordDeclaration(Token(SyntaxKind.RecordKeyword), identifier);
    }
}
