using Microsoft.CodeAnalysis;

namespace Glitch.CodeAnalysis;

public static class SyntaxTreeExtensions
{
    public static SyntaxTree WithRoot(this SyntaxTree tree, SyntaxNode root)
        => tree.WithRootAndOptions(root, tree.Options);
}
