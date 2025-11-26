using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Glitch.CodeAnalysis;

public static class TypeDeclarationExtensions
{
    // The actual node classes have the "new" modifier on these methods, so we need to write an overload for each derived type
    public static TypeDeclarationSyntax WithMembers(this TypeDeclarationSyntax node, IEnumerable<MemberDeclarationSyntax> members)
        => node.WithMembers(CSharpSyntax.List(members));

    public static ClassDeclarationSyntax WithMembers(this ClassDeclarationSyntax node, IEnumerable<MemberDeclarationSyntax> members)
        => node.WithMembers(CSharpSyntax.List(members));

    public static StructDeclarationSyntax WithMembers(this StructDeclarationSyntax node, IEnumerable<MemberDeclarationSyntax> members)
        => node.WithMembers(CSharpSyntax.List(members));

    public static RecordDeclarationSyntax WithMembers(this RecordDeclarationSyntax node, IEnumerable<MemberDeclarationSyntax> members)
        => node.WithMembers(CSharpSyntax.List(members));

    public static InterfaceDeclarationSyntax WithMembers(this InterfaceDeclarationSyntax node, IEnumerable<MemberDeclarationSyntax> members)
        => node.WithMembers(CSharpSyntax.List(members));
}
