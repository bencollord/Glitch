using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Glitch.CodeAnalysis
{
    public static class SyntaxTokenExtensions
    {
        public static bool IsAccessModifier(this SyntaxToken token)
        {
            return token.IsKind(SyntaxKind.PublicKeyword)
                || token.IsKind(SyntaxKind.InternalKeyword)
                || token.IsKind(SyntaxKind.ProtectedKeyword)
                || token.IsKind(SyntaxKind.PrivateKeyword);
        }
    }
}
