using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace Glitch.CodeAnalysis.Builders;

using static SyntaxFactory;

public class PropertySyntaxBuilder : SyntaxBuilder<PropertyDeclarationSyntax>
{
    private TypeSyntax type;
    private SyntaxToken identifier;

    public PropertySyntaxBuilder(TypeSyntax type, string name)
    {
        this.type = type;
        identifier = Identifier(name);
    }

    public PropertySyntaxBuilder(PropertyDeclarationSyntax property)
    {
        identifier = property.Identifier;
        type = property.Type;
    }

    public override PropertyDeclarationSyntax Build()
    {
        return PropertyDeclaration(type, identifier);
    }
}
