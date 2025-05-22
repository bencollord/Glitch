using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis
{
    public class UsingDirectiveRewriter : CSharpSyntaxRewriter
    {
        private readonly ISet<string> directivesToRemove;
        private readonly IDictionary<string, string> directivesToReplace = new Dictionary<string, string>();

        public UsingDirectiveRewriter(params HashSet<string> directivesToRemove)
        {
            this.directivesToRemove = directivesToRemove;
        }

        public UsingDirectiveRewriter(ISet<string> directivesToRemove, IDictionary<string, string> directivesToReplace)
        {
            this.directivesToRemove = directivesToRemove;
            this.directivesToReplace = directivesToReplace;
        }

        public UsingDirectiveRewriter Remove(string directive)
        {
            directivesToReplace.Remove(directive);
            directivesToRemove.Add(directive);
            return this;
        }

        public UsingDirectiveRewriter Replace(string oldDirective, string newDirective)
        {
            directivesToRemove.Remove(oldDirective);
            directivesToReplace.Add(oldDirective, newDirective);
            return this;
        }

        public override SyntaxNode? VisitUsingDirective(UsingDirectiveSyntax node)
        {
            if (node is null)
            {
                return null;
            }

            if (directivesToReplace.TryGetValue(node.Name?.ToString() ?? string.Empty, out string? newDirective))
            {
                return SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(newDirective))
                    .WithTriviaFrom(node);
            }

            if (directivesToRemove.Contains(node.Name?.ToString() ?? string.Empty))
            {
                return null;
            }

            return base.VisitUsingDirective(node);
        }
    }
}
