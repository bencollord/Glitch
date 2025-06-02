using Microsoft.CodeAnalysis;

namespace Glitch.CodeAnalysis.Builders
{
    public abstract class SyntaxBuilder<TNode>
        where TNode : SyntaxNode
    {
        public abstract TNode Build();
    }
}
