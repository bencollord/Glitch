using Microsoft.CodeAnalysis;

namespace Glitch.CodeAnalysis;

public static class SyntaxTreeExtensions
{
    extension(SyntaxTree tree)
    {
        public SyntaxTree WithRoot(SyntaxNode root) =>
        tree.WithRootAndOptions(root, tree.Options);
    }
}
