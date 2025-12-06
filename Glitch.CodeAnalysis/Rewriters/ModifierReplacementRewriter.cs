using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis.Rewriters;

using static SyntaxFactory;
using static Option;

public class ModifierReplacementRewriter : CSharpSyntaxRewriter
{
    private Dictionary<SyntaxKind, ModifierAddEntry> add = [];
    private Dictionary<SyntaxKind, SyntaxKind> replace = [];
    private HashSet<SyntaxKind> remove = [];

    public ModifierReplacementRewriter Add(params IEnumerable<SyntaxKind> modifierKinds)
    {
        foreach (var kind in modifierKinds)
        {
            Add(kind, InsertType.After);
        }

        return this;
    }

    public ModifierReplacementRewriter AddBefore(SyntaxKind kind, SyntaxKind newKind)
    {
        return Add(newKind, InsertType.Before, kind);

    }

    public ModifierReplacementRewriter AddAfter(SyntaxKind kind, SyntaxKind newKind)
    {
        return Add(newKind, InsertType.After, kind);
    }

    public ModifierReplacementRewriter Remove(params IEnumerable<SyntaxKind> modifierKinds)
    {
        remove.UnionWith(modifierKinds);
        return this;
    }

    public ModifierReplacementRewriter Replace(SyntaxKind replace, SyntaxKind with)
    {
        this.replace[replace] = with;
        return this;
    }

    public override SyntaxNode? VisitFieldDeclaration(FieldDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitFieldDeclaration(node);
    }

    public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitConstructorDeclaration(node);
    }

    public override SyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitPropertyDeclaration(node);
    }

    public override SyntaxNode? VisitEventDeclaration(EventDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitEventDeclaration(node);
    }

    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitMethodDeclaration(node);
    }

    public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitClassDeclaration(node);
    }

    public override SyntaxNode? VisitRecordDeclaration(RecordDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitRecordDeclaration(node);
    }

    public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitStructDeclaration(node);
    }

    public override SyntaxNode? VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
    {
        node = node.WithModifiers(UpdateModifiers(node.Modifiers));
        return base.VisitInterfaceDeclaration(node);
    }

    private ModifierReplacementRewriter Add(SyntaxKind kind, InsertType insertType, Option<SyntaxKind> nextTo = default)
    {
        add[kind] = new ModifierAddEntry(kind, nextTo, insertType);
        return this;
    }

    private SyntaxTokenList UpdateModifiers(SyntaxTokenList modifiers)
    {
        var updated = modifiers
            .Where(m => !remove.Contains(m.Kind()))
            .Select(m => replace
                .TryGetValue(m.Kind())
                .Select(kind => Token(kind).WithTriviaFrom(m))
                .IfNone(m))
            .ToTokenList();

        return add.Values
            .Select(a => new
            {
                Modifier = Token(a.Modifier),
                InsertIndex = from kind in a.InsertBy
                              let idx = updated.IndexOf(kind)
                              where idx > -1
                              select idx,
                a.InsertType
            })
            .Select(a => new
            {
                Modifier = a.InsertIndex
                    .Select(i => a.Modifier.WithTriviaFrom(updated[i]))
                    .IfNone(a.Modifier.WithTrailingTrivia(Space)),

                InsertIndex = a.InsertIndex
                    .Select(x => a.InsertType == InsertType.After ? x + 1 : x)
                    .OrElse(_ => a.InsertType == InsertType.Before ? Some(0) : None)
            })
            .Aggregate(
                updated,
                (lst, a) => a.InsertIndex
                    .Match(some: idx => lst.Insert(idx, a.Modifier),
                           none: ___ => lst.Add(a.Modifier)));
    }

    private enum InsertType { Before, After }

    private record struct ModifierAddEntry(SyntaxKind Modifier, Option<SyntaxKind> InsertBy = default, InsertType InsertType = InsertType.Before)
    {
        public bool Equals(ModifierAddEntry other) => Modifier == other.Modifier;

        public override int GetHashCode() => Modifier.GetHashCode();
    }
}
