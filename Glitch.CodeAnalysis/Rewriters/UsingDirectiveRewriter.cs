using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis.Rewriters;

using static CSharpSyntax;

public class NamespaceRewriter : CSharpSyntaxRewriter
{
    private readonly ISet<string> remove;
    private readonly IDictionary<string, string> replace = new Dictionary<string, string>();

    public NamespaceRewriter(params HashSet<string> remove)
    {
        this.remove = remove;
    }

    public NamespaceRewriter(ISet<string> remove, IDictionary<string, string> replace)
    {
        this.remove = remove;
        this.replace = replace;
    }

    public override SyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        if (remove.Contains(node.Name.ToString()))
        {
            throw new InvalidOperationException($"Cannot remove name that is being used in a declaration. Name: {node}");
        }

        if (replace.ContainsKey(node.Name.ToString()))
        {
            node = node.WithName(
                ParseName(replace[node.Name.ToString()])
                    .WithTriviaFrom(node.Name));
        }

        return base.VisitNamespaceDeclaration(node);
    }

    public override SyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        if (remove.Contains(node.Name.ToString()))
        {
            throw new InvalidOperationException($"Cannot remove name that is being used in a declaration. Name: {node}");
        }

        if (replace.ContainsKey(node.Name.ToString()))
        {
            node = node.WithName(
                ParseName(replace[node.Name.ToString()])
                    .WithTriviaFrom(node.Name));
        }

        return base.VisitFileScopedNamespaceDeclaration(node);
    }

    public NamespaceRewriter Remove(string name)
    {
        replace.Remove(name);
        remove.Add(name);
        return this;
    }

    public NamespaceRewriter Replace(string oldName, string newName)
    {
        remove.Remove(oldName);
        replace.Add(oldName, newName);
        return this;
    }

    public override SyntaxNode? VisitUsingDirective(UsingDirectiveSyntax node)
    {
        if (node is null)
        {
            return null;
        }

        if (replace.TryGetValue(node.Name?.ToString() ?? string.Empty, out string? newDirective))
        {
            return node.WithName(
                ParseName(newDirective)
                    .WithTriviaFrom(node.Name!));
        }

        if (remove.Contains(node.Name?.ToString() ?? string.Empty))
        {
            return null;
        }

        return base.VisitUsingDirective(node);
    }
}